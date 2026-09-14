namespace Kavosh.Domain.Entities
{
    public class DefinitiveAccount : BaseEntity
    {
        /// <summary>
        /// به صورت خودکار اضافه شود و از کاربر گرفته نشود
        /// </summary>
        public long Code { get; set; }

        /// <summary>
        /// ذخیره شماره فاکتور فروش
        /// </summary>
        public long DocNumber { get; set; }
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
        public DateTime DateCustom { get; set; }
        public long Price { get; set; }
            
        /// <summary>
        /// بدهکار یا بستانکار
        /// </summary>
        public bool Debtor { get; set; }
        public string? Description { get; set; }
        // 👇 جدید
        public bool IsCheck { get; set; }                        // این بدهی ناشی از چکه یا نه

        public Guid? SettledFromId { get; set; }                 // اگه این رکورد، خنثی‌کننده‌ی یه بدهی چک قبلیه
        public virtual DefinitiveAccount SettledFrom { get; set; }
        public Guid? HowToPayId { get; set; }
        public virtual HowToPay HowToPay { get; set; }
    }
}