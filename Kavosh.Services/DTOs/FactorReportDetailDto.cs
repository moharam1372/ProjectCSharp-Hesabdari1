namespace Kavosh.Services.DTOs
{
    public class FactorReportDetailDto
    {
        public string ProductTitle { get; set; }
        public float Count { get; set; }
        public long PriceUnit { get; set; }
    }

    public class FactorReportDto
    {
        // اطلاعات هدر فاکتور
        public string Header { get; set; }
        public string Num { get; set; }
        public string Date { get; set; }
        public string Buyer { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public List<FactorReportDetailDto> FactorDetails { get; set; } = new();

        // مبالغ
        public long Discount { get; set; }
        public long PriceTotal { get; set; }
        public long TaxAmount { get; set; }
        public long PreviousDebt { get; set; }     // فعلاً 0 — پایین توضیح میدم چرا
        public long PayableAmount { get; set; }

        // اطلاعات فروشگاه
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ShabaNumber { get; set; }
        public byte[]? Logo { get; set; }
        public byte[]? Mohr { get; set; }
    }
}