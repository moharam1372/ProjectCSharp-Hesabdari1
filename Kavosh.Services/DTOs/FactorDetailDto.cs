namespace Kavosh.Services.DTOs
{
    public class FactorDetailDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public float Count { get; set; }
        public long PriceUnit { get; set; }
        public long LineTotal => (long)(Count * PriceUnit);
    }
}