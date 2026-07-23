namespace Kavosh.Services.DTOs
{
    public class ProductKardexRowDto
    {
        public DateTime Date { get; set; }
        public long FactorCode { get; set; }
        public bool Type { get; set; }   // true = فروش (خروجی) / false = خرید (ورودی)
        public float Input { get; set; }
        public float Output { get; set; }
    }
}