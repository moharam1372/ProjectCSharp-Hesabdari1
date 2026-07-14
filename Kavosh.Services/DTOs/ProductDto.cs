using System;

namespace Kavosh.Services.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public long ProductCode { get; set; }

        public Guid ProductGroupId { get; set; }
        public string ProductGroupTitle { get; set; }

        public string Title { get; set; }

        public Guid ProductUnitId { get; set; }
        public string ProductUnitTitle { get; set; }

        /// <summary>
        /// موجودی اولیه محصول در انبار، که در زمان ایجاد محصول مشخص می‌شود و بعداً با تراکنش‌های انبار تغییر می‌کند.
        /// </summary>
        public float InitialInventory { get; set; }
        public long UnitPrice { get; set; }
        public long SellPrice { get; set; }
        public string Description { get; set; }

        // TODO: بعد از پیاده‌سازی کاردکس، از تراکنش‌های انبار محاسبه بشن
        public float InputStock { get; set; }
        public float OutputStock { get; set; }
        public float CurrentStock { get; set; }
    }
}