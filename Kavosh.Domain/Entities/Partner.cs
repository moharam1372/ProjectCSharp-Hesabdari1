namespace Kavosh.Domain.Entities
{
    public class Partner : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<PartnerExpense> PartnerExpenses { get; set; } = new List<PartnerExpense>();
    }
}