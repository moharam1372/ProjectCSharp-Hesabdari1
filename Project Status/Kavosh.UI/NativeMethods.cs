using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Kavosh.UI
{
    internal static class NativeMethods
    {
        public const int HWND_BROADCAST = 0xFFFF;

        // یک پیام سفارشی و منحصربه‌فرد در سیستم ثبت می‌شود
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME_KAVOSH_UI_APP");

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);
    }
}
