using DevExpress.Skins;
using DevExpress.UserSkins;
using Kavosh.DataAccess;
using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Interfaces;
using Kavosh.Services;
using Kavosh.UI.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourApp.UI;

namespace Kavosh.UI
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            ServiceProvider = services.BuildServiceProvider();

            // ============= مدیریت دیتابیس =============
            InitializeDatabase();

            // اگر از DevExpress استفاده می‌کنید، این خط را هم اضافه کنید:
            // DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("The Bezier");

            ApplicationConfiguration.Initialize(); // برای net8 WinForms template
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            //var mainForm = ServiceProvider.GetRequiredService<CustomerForm>();
            var mainForm = ServiceProvider.GetRequiredService<FrmMain>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductUnitRepository, ProductUnitRepository>();
            services.AddScoped<IFactorHeaderRepository, FactorHeaderRepository>();
            services.AddScoped<IDefinitiveAccountRepository, DefinitiveAccountRepository>();


            // Services
            services.AddScoped<CustomerService>();
            services.AddScoped<PersonService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductGroupService>();
            services.AddScoped<ProductUnitService>();
            services.AddScoped<PersonService>();
            services.AddScoped<FactorHeaderService>();
            services.AddScoped<PaymentTypeService>();
            services.AddScoped<DefinitiveAccountService>();

            // Forms
            services.AddTransient<FrmMain>();
            services.AddTransient<FrmProduct>();
            services.AddTransient<FrmPerson>();
            services.AddTransient<FrmFactor>();
            services.AddTransient<FrmFactorList>();
            services.AddTransient<FrmDefinitiveAccount>();
            //services.AddTransient<CustomerForm>();
        }
        private static void InitializeDatabase()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // روش ۱: فقط Migration‌ها را اعمال کن (دیتابیس را هم ایجاد می‌کند)
                    dbContext.Database.Migrate();
                    Console.WriteLine("✅ Database migration applied successfully!");

                    // 👇 جدید: چک/ساخت رکوردهای ثابت نوع پرداخت
                    Kavosh.DataAccess.Seeders.PaymentTypeSeeder.SeedAsync(dbContext).GetAwaiter().GetResult();
                    Console.WriteLine("✅ PaymentType seed check completed!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"خطا در راه‌اندازی دیتابیس:\n\n{ex.Message}",
                        "خطای دیتابیس",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    Environment.Exit(1);
                }
            }
        }
        public static T CreateScopedForm<T>() where T : Form
        {
            var scope = ServiceProvider.CreateScope();
            var form = scope.ServiceProvider.GetRequiredService<T>();
            form.FormClosed += (s, e) => scope.Dispose();
            return form;
        }
    }
}
