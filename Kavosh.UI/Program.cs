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

            //services.AddScoped<ProductUnitService>();
            //services.AddScoped<ProductGroupService>();
            //services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Services
            services.AddScoped<CustomerService>();
            services.AddScoped<PersonService>();
            services.AddScoped<ProductService>();

            // Forms
            services.AddTransient<FrmMain>();
            services.AddTransient<FrmProduct>();                           // 👈 جدید
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"خطا در راه‌اندازی دیتابیس:\n\n{ex.Message}",
                        "خطای دیتابیس",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // در صورت خطا، برنامه را ادامه نده
                    Environment.Exit(1);
                }
            }
        }
    }
}
