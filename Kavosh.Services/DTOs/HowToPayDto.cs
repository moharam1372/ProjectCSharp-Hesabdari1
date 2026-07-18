// HowToPayDto.cs
namespace Kavosh.Services.DTOs
{
    public class HowToPayDto
    {
        public Guid Id { get; set; }
        public Guid PaymentTypeId { get; set; }
        public string PaymentTypeTitle { get; set; }
        public long Price { get; set; }
        public string CheckNumber { get; set; }
        public DateTime CheckDate { get; set; }
        public bool Settlement { get; set; }
        public string Description { get; set; }
    }
}