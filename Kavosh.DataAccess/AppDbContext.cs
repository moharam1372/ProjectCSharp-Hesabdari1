using Kavosh.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Kavosh.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        //[Table(name: "DefinitiveAccount", Schema = "Account")]
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DefinitiveAccount> DefinitiveAccounts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<FactorHeader> FactorHeaders { get; set; }
        public DbSet<FactorDetail> FactorDetails { get; set; }
        /// <summary>
        /// نحوه پرداخت
        /// </summary>
        public DbSet<HowToPay> HowToPays { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }


        // TODO: به ازای هر یک از ۱۵ جدول، یک DbSet مشابه اینجا اضافه کنید
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<Product> Products { get; set; }


        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

                // فقط برای Migration - مستقیم Connection String رو اینجا بنویس
                optionsBuilder.UseSqlServer("Data Source=MOJTABAPC\\MOJTABA;Initial Catalog=Account1;Integrated Security=false;MultipleActiveResultSets=true;TrustServerCertificate=True;User ID=sa;Password=Moaz1370110;");

                return new AppDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

                entity.HasOne(p => p.ProductGroup)
                    .WithMany(g => g.Products)
                    .HasForeignKey(p => p.ProductGroupId)
                    .OnDelete(DeleteBehavior.Cascade);   //  حذف زنجیره‌ای
                    //.OnDelete(DeleteBehavior.Restrict);   // جلوگیری از حذف زنجیره‌ای

                entity.HasOne(p => p.ProductUnit)
                    .WithMany(u => u.Products)
                    .HasForeignKey(p => p.ProductUnitId)
                    .OnDelete(DeleteBehavior.Cascade);

             
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("ProductGroups");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<ProductUnit>(entity =>
            {
                entity.ToTable("ProductUnits");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
