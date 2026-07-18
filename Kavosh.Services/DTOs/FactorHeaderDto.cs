// FactorHeaderDto.cs — یه پراپرتی جدید اضافه شد
using Kavosh.Services.DTOs;

public class FactorHeaderDto
{
    public Guid Id { get; set; }
    public long Code { get; set; }
    public Guid PersonId { get; set; }
    public string PersonName { get; set; }
    public bool Type { get; set; }
    public DateTime DateFactor { get; set; } = DateTime.Now;
    public long Discount { get; set; }
    public long PriceTotal { get; set; }
    public List<FactorDetailDto> Details { get; set; } = new();
    public List<HowToPayDto> HowToPays { get; set; } = new();   // 👈 جدید
}