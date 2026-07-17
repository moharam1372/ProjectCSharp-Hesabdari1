// FactorHeaderDto.cs
namespace Kavosh.Services.DTOs
{
    public class FactorHeaderDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public bool Type { get; set; }   // فروش = true / خرید = false
        public DateTime DateFactor { get; set; } = DateTime.Now;
        public long Discount { get; set; }
        public long PriceTotal { get; set; }
        public List<FactorDetailDto> Details { get; set; } = new();
    }
}