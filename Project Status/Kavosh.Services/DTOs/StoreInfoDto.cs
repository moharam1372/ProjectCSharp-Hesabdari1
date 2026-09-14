namespace Kavosh.Services.DTOs
{
    public class StoreInfoDto
    {
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ShabaNumber { get; set; }
        public float TaxPercent { get; set; }
        public byte[]? Logo { get; set; }
        public byte[]? Mohr { get; set; }
    }
}