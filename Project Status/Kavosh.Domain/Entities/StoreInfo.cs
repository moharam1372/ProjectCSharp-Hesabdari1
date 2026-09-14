namespace Kavosh.Domain.Entities
{
    public class StoreInfo : BaseEntity
    {
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// جهت چاپ در گوشه فاکتور
        /// </summary>
        public byte[]? Logo { get; set; } 
        /// <summary>
        /// تصویر مهر یا امضای فروشنده
        /// </summary>
        public byte[]? Mohr { get; set; } 
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ShabaNumber { get; set; }

        public float TaxPercent { get; set; }   // برای فیلد «مالیات» توی گزارش
    }
}