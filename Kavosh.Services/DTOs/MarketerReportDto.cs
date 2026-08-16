namespace Kavosh.Services.DTOs
{
    public class MarketerReportDto
    {
        public Guid MarketerId { get; set; }
        public string MarketerFullName { get; set; }
        public string PhoneNumber { get; set; }
        public int FactorCount { get; set; }
        public int CustomerCount { get; set; }
        public long TotalSales { get; set; }   // جمع فاکتورهای فروش (Type = true)
    }
}   