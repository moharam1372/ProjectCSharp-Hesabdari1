namespace Kavosh.Domain.Entities
{
    public class CashDocument : BaseEntity
    {
        /// <summary>
        /// شماره سند - خودکار
        /// </summary>
        public long Code { get; set; }

        /// <summary>
        /// فروش = true / خرید = false
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// شناسه فاکتورهای شرکت‌کننده در این سند - با کاما جدا شده
        /// </summary>
        public string FactorHeaderIds { get; set; }

        public long TotalAmount { get; set; }
        public DateTime DateDocument { get; set; } = DateTime.Now;
        public string Description { get; set; }
        /// <summary>
        /// وقتی true بشه، یعنی این سند تسویه/قطعی شده و در محاسبه‌ی سود لحاظ می‌شود.
        /// دیگر قابل حذف نیست.
        /// </summary>
        public bool IsSettled { get; set; } = false;   // 👈 جدید
    }
}