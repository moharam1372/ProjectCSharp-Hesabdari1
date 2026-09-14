namespace Kavosh.Domain.Entities
{
    public class SettlementRecord : BaseEntity
    {
        public long Code { get; set; }
        public DateTime DateSettlement { get; set; } = DateTime.Now;

        public long TotalSales { get; set; }
        public long TotalPurchases { get; set; }
        public long Profit { get; set; }

        public int DocumentCount { get; set; }

        /// <summary>
        /// شناسه‌ی سندهای صندوقی که در این تسویه لحاظ شده‌اند - با کاما جدا شده
        /// </summary>
        public string CashDocumentIds { get; set; }

        public string Description { get; set; }
    }
}