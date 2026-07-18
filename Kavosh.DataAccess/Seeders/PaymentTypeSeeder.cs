using Kavosh.Domain;
using Kavosh.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Seeders
{
    public static class PaymentTypeSeeder
    {
        // 👇 اگه اسم‌ها یا تعدادشون فرق داره، فقط همین آرایه رو عوض کنید
        private static readonly (Guid Id, string Title)[] DefaultItems =
        {
            (PaymentTypeIds.Cash, "نقدی"),
            (PaymentTypeIds.Check, "چک"),
            (PaymentTypeIds.CardToCard, "کارت به کارت")
        };

        public static async Task SeedAsync(AppDbContext context)
        {
            foreach (var (id, title) in DefaultItems)
            {
                var exists = await context.PaymentTypes.AnyAsync(p => p.Id == id);
                if (!exists)
                {
                    await context.PaymentTypes.AddAsync(new PaymentType
                    {
                        Id = id,
                        Title = title,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    });
                }
            }

            await context.SaveChangesAsync();
        }

    }
}