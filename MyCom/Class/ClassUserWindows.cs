using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
//using NetFwTypeLib;

namespace MyCom.Class
{
    public static class ClassUserWindows
    {
        public static void ConfigNetwork1()
        {
            ExecuteCmd("reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\LanmanWorkstation\\Parameters\" /v AllowInsecureGuestAuth /t REG_DWORD /d 1 /f");

        }

        static void ExecutePowerShellScript(string script)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Unrestricted -Command \"{script}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            //process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            Console.WriteLine("Output: " + output);
            Console.WriteLine("Error: " + error);

        }
        public static void SetAutoLogin(bool enable, string username = "", string password = "")
        {

            try
            {
                // مسیر رجیستری برای تنظیمات ورود خودکار
                string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                {
                    if (key != null)
                    {
                        if (enable)
                        {
                            // فعال کردن ورود خودکار
                            key.SetValue("DefaultUserName", username);
                            key.SetValue("DefaultPassword", password);
                            key.SetValue("AutoAdminLogon", "1");
                            Console.WriteLine("Auto-login enabled successfully.");
                        }
                        else
                        {
                            // غیرفعال کردن ورود خودکار
                            key.DeleteValue("DefaultPassword", false);
                            key.SetValue("AutoAdminLogon", "0");
                            Console.WriteLine("Auto-login disabled successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to open the registry key.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while setting auto-login: " + ex.Message);
            }


        }
        public static void SetPassUser(string password)
        {
            //  string userName = "Ali"; // نام کاربر
            //   string newPassword = "1234"; // رمز عبور جدید
            var getThisUser = Environment.UserName;
            if (getThisUser.ToLower() == "mojtaba")
            {
                return;
            }
            try
            {
                string machineName = Environment.MachineName;

                //using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine, machineName))
                {
                    //ClassMessageBox.ShowMSG("Name: "+context.Name, "", ClassMessageBox.enumIcon.دسترسی);
                    //ClassMessageBox.ShowMSG("UserName: " + context.UserName, "", ClassMessageBox.enumIcon.دسترسی);
                    UserPrincipal user = null;
                    try
                    {
                  
                        user = UserPrincipal.FindByIdentity(context, getThisUser);
                      //  ClassMessageBox.ShowMSG(user.Name, "", ClassMessageBox.enumIcon.دسترسی);
                    }
                    catch (Exception e1)
                    {
                       // ClassMessageBox.ShowMSG(e1.Message, "", ClassMessageBox.enumIcon.دسترسی);
                    }
               
                    //ClassMessageBox.ShowMSG(user.Name, "", ClassMessageBox.enumIcon.دسترسی);
                    if (user != null && password.Length > 0 && password != null)
                    {
                        user.SetPassword(password);
                        user.Save();
                        Console.WriteLine("Password changed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
              //  ClassMessageBox.ShowMSG(ex.Message, "", ClassMessageBox.enumIcon.دسترسی);
                Console.WriteLine($"Error changing password: {ex.Message}");
            }
        }



        public static void ConfigShare2Policy()
        {
            ExecuteCmd("reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Lsa\" /v ForceGuest /t REG_DWORD /d 0 /f");

            // تنظیم سطح احراز هویت LAN Manager
            ExecuteCmd("reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Lsa\" /v LmCompatibilityLevel /t REG_DWORD /d 4 /f");

            // تنظیم سطح دسترسی کاربران به شبکه
            ExecuteCmd("reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Lsa\" /v RestrictAnonymous /t REG_DWORD /d 0 /f");



        }

        public static void ActiveZeroOneAllUser()
        {
            if (File.Exists("C:\\Windows\\ZeroOne\\ZeroOne Client.exe"))
            {
                ExecuteCmd("reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v Shell /t REG_SZ /d \"C:\\Windows\\ZeroOne\\ZeroOne Loader.exe\" /f");
            }
        }
        public static void ActiveZeroOneOnlyUser()
        {
            if (File.Exists("C:\\Windows\\ZeroOne\\ZeroOne Client.exe"))
            {
                ExecuteCmd("reg add \"HKCU\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v Shell /t REG_SZ /d \"C:\\Windows\\ZeroOne\\ZeroOne Loader.exe\" /f");
            }
        }

    





        public static void ConfigSharePolicy()
        {
            try
            {
                using (DirectoryEntry localPolicy = new DirectoryEntry("WinNT://./Local Policies/Security Options"))
                {
                    foreach (DirectoryEntry policy in localPolicy.Children)
                    {
                        if (policy.Name == "Accounts: Limit local account use of blank passwords to console logon only")
                        {
                            policy.Properties["OptionValue"].Value = 0; // 0 برای غیرفعال کردن، 1 برای فعال کردن
                            policy.CommitChanges();
                            Console.WriteLine("Group Policy تنظیمات با موفقیت تغییر کرد!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing Group Policy: {ex.Message}");
            }
            try
            {
                Console.WriteLine("در حال تغییر تنظیمات Group Policy...");

                using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Lsa", true))
                {
                    if (regKey != null)
                    {
                        regKey.SetValue("LimitBlankPasswordUse", 0, RegistryValueKind.DWord);
                        Console.WriteLine("تنظیمات Group Policy برای حساب‌های بدون رمز عبور اعمال شد!");
                    }
                    else
                    {
                        Console.WriteLine("خطا: کلید رجیستری یافت نشد.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا در تغییر تنظیمات Group Policy: {ex.Message}");
            }
        }
        public static void ChangeNetworkSettings()
        {
            try
            {
                // دسترسی به تنظیمات شبکه از طریق WMI
                using (ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration"))
                {
                    foreach (ManagementObject managementObject in managementClass.GetInstances())
                    {
                        if ((bool)managementObject["IPEnabled"])
                        {
                            // خاموش کردن اشتراک‌گذاری محافظت شده با رمز عبور
                            managementObject.InvokeMethod("SetAccessControl", new object[] { "Everyone", "Full" });

                            // خاموش کردن حالت رمز عبور
                            var advancedSharing = new ManagementObjectSearcher("root\\CIMV2",
                                "SELECT * FROM Win32_Share WHERE Name = 'All Networks'");
                            foreach (ManagementObject queryObj in advancedSharing.Get())
                            {
                                queryObj.SetPropertyValue("PasswordProtectedSharing", false);
                                queryObj.Put();
                            }
                        }
                    }
                }

                Console.WriteLine("Password protected sharing turned off successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing network settings: {ex.Message}");
            }

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c netsh advfirewall set allprofiles state off";
                process.StartInfo.Verb = "runas";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Network settings changed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing network settings: {ex.Message}");
            }
        }
        public static void ShareFolder(string folderPath, string plusName)
        {
            try
            {
                // ایجاد پوشه اگر وجود نداشت
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // تنظیم دسترسی‌ها
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                DirectorySecurity dirSecurity = directoryInfo.GetAccessControl();
                dirSecurity.AddAccessRule(new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow));
                directoryInfo.SetAccessControl(dirSecurity);

                // اشتراک‌گذاری پوشه با استفاده از دستور cmd
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c net share \"{plusName} Games Shared\"=\"{folderPath}\" /grant:Everyone,full",
                        Verb = "runas"
                    }
                };
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Folder shared successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sharing folder: {ex.Message}");
            }
        }
        public static void AddFolderToDefender(string folderPath)
        {
            // string folderPath = @"C:\YourFolderToExclude";

            // دستور PowerShell برای افزودن استثنا به ویندوز دیفندر
            string powershellCommand = $"Add-MpPreference -ExclusionPath \"{folderPath}\"";

            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"{powershellCommand}\"",
                        Verb = "runas",
                        UseShellExecute = true,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };

                process.Start();
                process.WaitForExit();

                Console.WriteLine("Folder added to Windows Defender exclusions successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding folder to Windows Defender exclusions: {ex.Message}");
            }
        }
        public static void DisableTaskManager()
        {
            ExecuteCmd("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableTaskMgr /t REG_DWORD /d 1 /f");
        }
        public static void DisableLockSwitchUser()
        {
            ExecuteCmd("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableLockWorkstation /t REG_DWORD /d 1 /f");
            ExecuteCmd("reg add \"HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v HideFastUserSwitching /t REG_DWORD /d 1 /f");
        }
        public static void DisableChangePasswordAndSignOut()
        {
            // ExecuteGpupdate();
            ExecuteCmd(
                "reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableChangePassword /t REG_DWORD /d 1 /f");
            //  ExecuteCmd(
            //    "reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableSignOut /t REG_DWORD /d 1 /f");


            //  RestartPolicy();
        }
        public static void DisableExplorerOnStartupOnlyUser()
        {
            // Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon  // For All System
            // Shell = ZeroOne\ZeroOne Loader.exe
            try
            {
                string executablePath = Process.GetCurrentProcess().MainModule.FileName.Replace("PCClient.exe", "RunPCClient.exe");
                using (RegistryKey key =
                       Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    key.SetValue("Shell", executablePath, RegistryValueKind.String);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disabling explorer.exe: {ex.Message}");
            }
        }
        public static void DisableExplorerOnStartupAllUser()
        {
            // Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon  // For All System
            // Shell = ZeroOne\ZeroOne Loader.exe
            try
            {
                string executablePath = Process.GetCurrentProcess().MainModule.FileName.Replace("PCClient.exe", "RunPCClient.exe");
                using (RegistryKey key =
                       Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    key.SetValue("Shell", executablePath, RegistryValueKind.String);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disabling explorer.exe: {ex.Message}");
            }
        }
        public static void RemoveShellOnlyUser()
        {
            // حذف مقدار Shell از رجیستری برای همه کاربران (بازگشت به پیش‌فرض)
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    if (key != null)
                    {
                        key.DeleteValue("Shell", false);
                        Console.WriteLine("Shell value removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Registry key not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing Shell value: {ex.Message}");
            }
        }

        #region Restor Defualt

        public static void EnableTaskManager()
        {
            const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
            {
                key.DeleteValue("DisableTaskMgr", false);
            }
        }
        public static void EnableLockSwitchUser()
        {

            ExecuteCmd("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableLockWorkstation /t REG_DWORD /d 0 /f");
            ExecuteCmd("reg add \"HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v HideFastUserSwitching /t REG_DWORD /d 0 /f");
        }
        public static void EnableChangePasswordAndSignOut()
        {
            const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
            {
                key.DeleteValue("DisableChangePassword", false);
                key.DeleteValue("DisableSignOut", false);
            }




        }
        public static void EnabledChangePassword()
        {
            try
            {
                using (DirectoryEntry localPolicy = new DirectoryEntry("WinNT://./Local Policies/Security Options"))
                {
                    foreach (DirectoryEntry policy in localPolicy.Children)
                    {
                        if (policy.Name == "Accounts: Limit local account use of blank passwords to console logon only")
                        {
                            policy.Properties["OptionValue"].Value = 1; // 0 برای غیرفعال کردن، 1 برای فعال کردن
                            policy.CommitChanges();
                            Console.WriteLine("Group Policy تنظیمات با موفقیت تغییر کرد!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing Group Policy: {ex.Message}");
            }
        }
        public static void EnableExplorerOnStartupOnlyUser()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    key.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enabling explorer.exe: {ex.Message}");
            }
        }
        public static void EnableExplorerOnStartupAllUser()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    key.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enabling explorer.exe: {ex.Message}");
            }
        }
        #endregion

        static void ExecuteCmd(string command)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            process.Start();
            process.WaitForExit();
        }
        public static void RestartPolicy()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c gpupdate /force";
                process.StartInfo.Verb = "runas";
                process.StartInfo.CreateNoWindow = true; // مخفی کردن پنجره
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // مخفی کردن پنجره
                process.Start();
                process.WaitForExit();

                Console.WriteLine("Group Policy updated successfully!");

                ServiceController service = new ServiceController("LanmanWorkstation");
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

                Console.WriteLine("Service reloaded successfully!");
            }
            catch (Exception e)
            {

            }
        }

    }


    public class FirewallManager
    {
        public static void AddInboundPortRule(string ruleName = "RamPage 5520")
        {
            string arguments = $"advfirewall firewall add rule name=\"{ruleName}\" dir=in action=allow protocol=TCP localport={5520}";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = arguments,
                Verb = "runas", // اجرای با دسترسی ادمین
                UseShellExecute = true,
                CreateNoWindow = true
            };

            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطا در افزودن قانون فایروال: " + ex.Message);
            }
        }
        public static void CleanAndOpenPort(int port, string protocol = "TCP")
        {
            var ruleNames = FindFirewallRulesByPort(port);

            foreach (var ruleName in ruleNames)
            {
                DeleteFirewallRule(ruleName);
            }

            AddFirewallRule(port, protocol);
        }

        private static List<string> FindFirewallRulesByPort(int port)
        {
            var ruleNames = new List<string>();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "advfirewall firewall show rule name=all",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string currentRuleName = null;

            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                if (line.StartsWith("Rule Name:", StringComparison.OrdinalIgnoreCase))
                {
                    currentRuleName = line.Substring(10).Trim();
                }

                if (line.Contains($"{port}") && line.ToLower().Contains($"localport"))
                {
                    if (!string.IsNullOrEmpty(currentRuleName))
                    {
                        ruleNames.Add(currentRuleName);
                        currentRuleName = null; // Avoid duplicates
                    }
                }
            }

            return ruleNames;
        }

        private static void DeleteFirewallRule(string ruleName)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"advfirewall firewall delete rule name=\"{ruleName}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            Console.WriteLine($"Deleted rule: {ruleName}");
        }

        private static void AddFirewallRule(int port, string protocol)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"advfirewall firewall add rule name=\"Open Port {port} {protocol}\" dir=in action=allow protocol={protocol} localport={port}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            Console.WriteLine($"Added rule: Open Port {port} {protocol}");
        }
    }
}
