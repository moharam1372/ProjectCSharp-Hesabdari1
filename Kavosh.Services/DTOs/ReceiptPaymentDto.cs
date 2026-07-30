namespace Kavosh.Services.DTOs
{
    public class ReceiptPaymentDto
    {
        public Guid PersonId { get; set; }
        public DateTime DateCustom { get; set; } = DateTime.Now;
        public long Price { get; set; }

        /// <summary>
        /// true = دریافت از مشتری (بدهی او کم می‌شود)
        /// false = پرداخت به مشتری (بدهی او زیاد می‌شود)
        /// </summary>
        public bool IsReceipt { get; set; } = true;

        public bool IsCheckPayment { get; set; }
        public string CheckNumber { get; set; }
        public string Description { get; set; }
    }
}