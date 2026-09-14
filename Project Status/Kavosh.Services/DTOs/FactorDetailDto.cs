namespace Kavosh.Services.DTOs
{
    public class FactorDetailDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public float Count { get; set; }

        /// <summary>
        /// مبلغ خرید (بهای تمام‌شده)
        /// </summary>
        public long PriceUnit { get; set; }

        /// <summary>
        /// مبلغ فروش — قابل ویرایش، مبنای محاسبه‌ی جمع و مبنای چاپ فاکتور
        /// </summary>
        public long SellPrice { get; set; }

        public Guid UnitId { get; set; }

        // 👇 جمع بر اساس مبلغ فروش محاسبه می‌شود (چون این مبلغیه که مشتری پرداخت می‌کند)
        public long LineTotal => (long)(Count * SellPrice);
    }
}