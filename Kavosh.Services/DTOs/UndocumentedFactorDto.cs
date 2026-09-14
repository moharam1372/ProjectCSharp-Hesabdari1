namespace Kavosh.Services.DTOs
{
    // برای گرید تب‌های 1 و 2 (فاکتورهای سندنخورده) - همراه با CheckBox انتخاب توسط شما در UI
    public class UndocumentedFactorDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string PersonName { get; set; }
        public DateTime DateFactor { get; set; }
        public long PriceTotal { get; set; }
    }
}