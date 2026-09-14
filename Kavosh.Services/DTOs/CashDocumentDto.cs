namespace Kavosh.Services.DTOs
{
    public class CashDocumentDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public bool Type { get; set; }
        public DateTime DateDocument { get; set; }
        public long TotalAmount { get; set; }
        public int FactorCount { get; set; }
        public string Description { get; set; }
        public bool IsSettled { get; set; }   // 👈 جدید
    }
}