using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public static class ClassRemote
    {
        public static void EnableRemoteDesktop()
        {
            // فعال‌سازی RDP
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", true);
            key.SetValue("fDenyTSConnections", 0, RegistryValueKind.DWord);
            key.Close();
        }
        public static void EnableNLA()
        {
            RegistryKey nlaKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", true);
            nlaKey.SetValue("UserAuthentication", 1, RegistryValueKind.DWord);
            nlaKey.Close();
        }

        public static void EnableFirewallRule()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "netsh advfirewall firewall set rule group=\"remote desktop\" new enable=Yes",
                Verb = "runas", // اجرای با دسترسی ادمین
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden // اجرای مخفی

            });
        }

        public static void Run()
        {
            EnableRemoteDesktop();
            EnableNLA();
            EnableFirewallRule();
        }
    }
}
