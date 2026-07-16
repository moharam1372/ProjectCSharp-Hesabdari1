namespace Kavosh.Domain.Entities
{
    public class PaymentType : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<HowToPay> HowToPays { get; set; } = new List<HowToPay>();
    }
}   