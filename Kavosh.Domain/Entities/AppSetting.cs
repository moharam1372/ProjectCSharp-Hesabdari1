namespace Kavosh.Domain.Entities
{
    public class AppSetting : BaseEntity
    {
        /// <summary>
        /// اگر فعال باشد، هنگام ثبت فاکتور فروش اجازه‌ی خروج بیشتر از موجودی داده نمی‌شود
        /// </summary>
        public bool PreventNegativeInventory { get; set; }

        // 👇 فیلدهای بعدی تنظیمات برنامه، به مرور همین‌جا اضافه می‌شوند
    }
}