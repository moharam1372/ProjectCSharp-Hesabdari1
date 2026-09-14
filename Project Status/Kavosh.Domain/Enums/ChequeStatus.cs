namespace Kavosh.Domain.Enums
{
    public enum ChequeStatus
    {
        Pending = 0,   // در جریان (وصول نشده)
        Cleared = 1,   // پاس شده / وصول شده
        Bounced = 2    // برگشتی
    }
}