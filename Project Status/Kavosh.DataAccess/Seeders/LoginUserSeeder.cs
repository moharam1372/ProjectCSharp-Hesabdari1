using Kavosh.Domain.Constants;
using Kavosh.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Seeders
{
    public static class LoginUserSeeder
    {


        public static async Task SeedAsync(AppDbContext context)
        {
            var exists = await context.LoginUsers.AnyAsync(p => p.Username.ToLower() == "admin");
            if (!exists)
            {
                await context.LoginUsers.AddAsync(new LoginUser
                {
                    FullName = "کاربر اصلی",
                    Username = "admin",
                    Password = "admin",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                });
                await context.SaveChangesAsync();
            }

          
        }

    }
}