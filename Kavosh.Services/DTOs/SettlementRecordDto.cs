namespace Kavosh.Services.DTOs
{
    public class SettlementRecordDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public DateTime DateSettlement { get; set; }
        public long TotalSales { get; set; }
        public long TotalPurchases { get; set; }
        public long Profit { get; set; }
        public int DocumentCount { get; set; }
        public string Description { get; set; }
    }
}