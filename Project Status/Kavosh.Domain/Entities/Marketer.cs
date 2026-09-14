namespace Kavosh.Domain.Entities
{
    public class Marketer : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<FactorHeader> FactorHeaders { get; set; } = new List<FactorHeader>();
    }
}   