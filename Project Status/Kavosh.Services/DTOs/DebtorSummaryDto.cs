namespace Kavosh.Services.DTOs
{
    public class DebtorSummaryDto
    {
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public string Mobile { get; set; }
        public DateTime LastDebtDate { get; set; }
        public long CheckDebt { get; set; }      // مانده‌ی بدهی ناشی از چک (خودکار صفر میشه وقتی وصول بشه)
        public long OtherDebt { get; set; }      // مانده‌ی بدهی غیرچکی
        public long TotalDebt => CheckDebt + OtherDebt;
    }
}