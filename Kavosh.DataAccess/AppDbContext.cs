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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
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
            //modelBuilder.Entity<Customer>(entity =>
            //{
                
            //    entity.ToTable("Customers");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            //    entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            //    entity.Property(e => e.Email).HasMaxLength(150);
            //    entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
            //    entity.Property(e => e.CreatedBy).HasMaxLength(100);
            //    entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
