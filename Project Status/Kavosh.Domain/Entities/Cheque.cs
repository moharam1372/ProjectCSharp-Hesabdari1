using Kavosh.Domain.Enums;

namespace Kavosh.Domain.Entities
{
    public class Cheque : BaseEntity
    {
        public string ChequeNumber { get; set; }

        /// <summary>
        /// تاریخ سررسید چک - همیشه میلادی ذخیره می‌شود، فقط موقع نمایش شمسی می‌شود
        /// </summary>
        public DateTime DueDate { get; set; }

        public long Price { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        /// <summary>
        /// true = چک دریافتی (از مشتری - معمولاً بابت فاکتور فروش)
        /// false = چک صادرشده (به تامین‌کننده - معمولاً بابت فاکتور خرید)
        /// </summary>
        public bool IsReceived { get; set; }

        public ChequeStatus Status { get; set; } = ChequeStatus.Pending;

        public string Description { get; set; }

        /// <summary>
        /// اگه چک از طریق نحوه‌ی پرداخت یک فاکتور ثبت شده باشه
        /// </summary>
        public Guid? HowToPayId { get; set; }
        public virtual HowToPay HowToPay { get; set; }

        /// <summary>
        /// اگه چک از طریق دریافت/پرداخت دستی ثبت شده باشه
        /// </summary>
        public Guid? DefinitiveAccountId { get; set; }
        public virtual DefinitiveAccount DefinitiveAccount { get; set; }

        /// <summary>
        /// آخرین باری که آلارم این چک نمایش داده شده (جلوگیری از تکرار در همون روز)
        /// </summary>
        public DateTime? LastAlarmShownAt { get; set; }
    }
}