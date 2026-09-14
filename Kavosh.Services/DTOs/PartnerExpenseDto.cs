namespace Kavosh.Services.DTOs
{
    public class PartnerExpenseDto
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string PartnerFullName { get; set; }
        public Guid? ExpenseTypeId { get; set; }
        public string ExpenseTypeTitle { get; set; }
        public long Amount { get; set; }
        public bool IsPayment { get; set; }
        public DateTime DateCustom { get; set; }
        public string Description { get; set; }
    }
}