namespace Kavosh.Services.DTOs
{
    public class CashProfitReportDto
    {
        public long TotalSales { get; set; }
        public long TotalPurchases { get; set; }
        public long GrossProfit => TotalSales - TotalPurchases;
    }
}