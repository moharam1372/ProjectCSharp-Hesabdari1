using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using Kavosh.DataAccess;
using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Interfaces;
using Kavosh.Services;
using Kavosh.UI.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kavosh.UI
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            // حتماً قبل از ایجاد فرم، اسکین یا کنترل
            WindowsFormsSettings.SetPerMonitorDpiAware();

            // فعال بودن DPI Scaling در DevExpress
            WindowsFormsSettings.AllowDpiScale = true;


            // =========================================================
            // تنظیم DPI
            // باید قبل از ایجاد هر Form یا Control اجرا شود
            // =========================================================
            //Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // =========================================================
            // ثبت DevExpress Skin
            // =========================================================
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();

            // در صورت نیاز:
            // DevExpress.LookAndFeel.UserLookAndFeel.Default
            //     .SetSkinStyle("The Bezier");

            // =========================================================
            // Configuration
            // =========================================================
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile(
                        "appsettings.json",
                        optional: false,
                        reloadOnChange: true)
                    .Build();

            // =========================================================
            // Dependency Injection
            // =========================================================
            var services = new ServiceCollection();

            ConfigureServices(
                services,
                configuration);

            ServiceProvider =
                services.BuildServiceProvider();

            // =========================================================
            // راه‌اندازی دیتابیس
            // =========================================================
            InitializeDatabase();

            // =========================================================
            // اجرای فرم اصلی
            // =========================================================
            var mainForm =
                ServiceProvider.GetRequiredService<FrmMain>();

            Application.Run(mainForm);
        }

        private static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString =
                configuration.GetConnectionString(
                    "DefaultConnection");

            // =========================================================
            // Database
            // =========================================================
            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(connectionString));

            // =========================================================
            // Repositories
            // =========================================================
            services.AddScoped(
                typeof(IRepository<>),
                typeof(Repository<>));

            services.AddScoped<
                ICustomerRepository,
                CustomerRepository>();

            services.AddScoped<
                IProductRepository,
                ProductRepository>();

            services.AddScoped<
                IProductGroupRepository,
                ProductGroupRepository>();

            services.AddScoped<
                IProductUnitRepository,
                ProductUnitRepository>();

            services.AddScoped<
                IFactorHeaderRepository,
                FactorHeaderRepository>();

            services.AddScoped<
                IDefinitiveAccountRepository,
                DefinitiveAccountRepository>();

            // =========================================================
            // Services
            // =========================================================
            services.AddScoped<CustomerService>();

            services.AddScoped<PersonService>();

            services.AddScoped<ProductService>();

            services.AddScoped<ProductGroupService>();

            services.AddScoped<ProductUnitService>();

            services.AddScoped<FactorHeaderService>();

            services.AddScoped<PaymentTypeService>();

            services.AddScoped<DefinitiveAccountService>();

            services.AddScoped<StoreInfoService>();

            services.AddScoped<DatabaseBackupService>();

            // =========================================================
            // Forms
            // =========================================================
            services.AddTransient<FrmMain>();

            services.AddTransient<FrmProduct>();

            services.AddTransient<FrmPerson>();

            services.AddTransient<FrmFactor>();

            services.AddTransient<FrmFactorList>();

            services.AddTransient<FrmDefinitiveAccount>();

            services.AddTransient<FrmDebtorsList>();

            services.AddTransient<FrmProductKardex>();

            services.AddTransient<FrmStoreInfo>();

            services.AddTransient<FrmBackup>();

            services.AddTransient<FrmBackupProgress>();
        }

        private static void InitializeDatabase()
        {
            using var scope =
                ServiceProvider.CreateScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

            try
            {
                // اجرای Migrationها
                dbContext.Database.Migrate();

                Console.WriteLine(
                    "Database migration applied successfully!");

                // Seed نوع پرداخت
                Kavosh.DataAccess.Seeders
                    .PaymentTypeSeeder
                    .SeedAsync(dbContext)
                    .GetAwaiter()
                    .GetResult();

                Console.WriteLine(
                    "PaymentType seed check completed!");
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

        public static T CreateScopedForm<T>()
            where T : Form
        {
            var scope =
                ServiceProvider.CreateScope();

            var form =
                scope.ServiceProvider
                    .GetRequiredService<T>();

            form.FormClosed +=
                (s, e) =>
                {
                    scope.Dispose();
                };

            return form;
        }
    }


}
