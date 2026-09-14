namespace Kavosh.Domain.Entities
{
    public class ExpenseType : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<PartnerExpense> PartnerExpenses { get; set; } = new List<PartnerExpense>();
    }
}