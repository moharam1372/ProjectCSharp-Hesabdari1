namespace Kavosh.Domain.Entities
{
    public class FactorDetail : BaseEntity
    {
        public Guid FactorHeaderId { get; set; }
        public virtual FactorHeader FactorHeader { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }


        public float Count { get; set; }

        /// <summary>
        /// مبلغ خرید (بهای تمام‌شده) — اطلاعاتی، در چاپ فاکتور نمایش داده نمی‌شود
        /// </summary>
        public long PriceUnit { get; set; }

        /// <summary>
        /// مبلغ فروش — قابل ویرایش توسط کاربر، همین مبلغ در چاپ فاکتور نمایش داده می‌شود
        /// </summary>
        public long SellPrice { get; set; }
    }
}