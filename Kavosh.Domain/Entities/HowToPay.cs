
namespace Kavosh.Domain.Entities
{
    public class HowToPay : BaseEntity
    {

        /// <summary>
        /// باید جدول بشه و از کاربر گرفته شود
        /// مثال: نقدی، کارت-حساب، چک و حساب دفتری
        /// </summary>
        public Guid PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        
        /// <summary>
        /// مبلغ پرداختی
        /// </summary>
        public long Price { get; set; }

        /// <summary>
        /// شماره چک در صورت پرداخت با چک
        /// </summary>
        public string CheckNumber { get; set; }
        /// <summary>
        /// تاریخ سررسید چک در صورت پرداخت با چک
        /// </summary>
        public DateTime CheckDate { get; set; }


        /// <summary>
        /// وضعیت تسویه حساب
        /// </summary>
        public bool Settlement { get; set; }
        public string Description { get; set; }

        
        public ICollection<DefinitiveAccount> DefinitiveAccounts { get; set; } = new List<DefinitiveAccount>();

    }   
}    