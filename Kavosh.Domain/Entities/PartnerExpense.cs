namespace Kavosh.Domain.Entities
{
    public class PartnerExpense : BaseEntity
    {
        public Guid PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public Guid? ExpenseTypeId { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }

        public long Amount { get; set; }

        /// <summary>
        /// true = شریک از جیب خودش برای کسب‌وکار پرداخت کرده (هزینه‌ی واقعی کسب‌وکار)
        /// false = تسویه/برداشت شریک از صندوق
        /// </summary>
        public bool IsPayment { get; set; }

        public DateTime DateCustom { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}