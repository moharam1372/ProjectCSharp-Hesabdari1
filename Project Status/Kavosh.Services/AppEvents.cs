namespace Kavosh.Services
{
    // یه پل ساده بین فرم‌هایی که Save/Settle انجام می‌دن و FrmMain که باید داشبورد رو رفرش کنه
    public static class AppEvents
    {
        public static event Action DataChanged;

        public static void RaiseDataChanged()
        {
            DataChanged?.Invoke();
        }
    }
}