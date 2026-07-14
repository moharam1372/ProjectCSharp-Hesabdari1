using Meziantou.Framework.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace MyCom.Class
{
    public static class ClassSecurityNetwork
    {
        public static void ShowDialogSecurityShare(string networkPath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = networkPath,
                UseShellExecute = true
            });
        }

        public static void ShowDialogSecurityShare0(string networkPath)
        {
            var startInfoAuth = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c net use {networkPath} /user:ali2",
                UseShellExecute = true
            };

            var authProcess = Process.Start(startInfoAuth);
            authProcess.WaitForExit();

        }

        public static void ShowDialogSecurityShare2(string networkPath)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "net",
                    Arguments = $"use {networkPath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = true, // استفاده از UseShellExecute برای باز کردن پنجره احراز هویت
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        Console.WriteLine($"Error: {error}");
                    }

                    Console.WriteLine($"Output: {output}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //  throw;
            }

        }

        public static void ShowDialogSecurityShare22(string networkPath, string user, string pass)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "net",
                Arguments = $"use {networkPath} /user:{user} {pass}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                //UseShellExecute = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                //UserName = user, // نام کاربری را اینجا وارد کنید
                //Password = GetSecureString(pass), // رمز عبور را اینجا وارد کنید
                //  Domain = "YourDomain" // دامنه (در صورت نیاز)
            };

            // تابع برای تبدیل رمز عبور به SecureString
            //SecureString GetSecureString(string password)
            //{
            //    var secureString = new SecureString();
            //    foreach (char c in password)
            //    {
            //        secureString.AppendChar(c);
            //    }

            //    secureString.MakeReadOnly();
            //    return secureString;
            //}

            var process2 = new Process { StartInfo = processStartInfo };
            process2.Start();
            //try
            //{
            //  using (var process = new Process { StartInfo = processStartInfo })
            //    {
            //  var outputBuilder = new StringBuilder();
            //  var errorBuilder = new StringBuilder();

            process2.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    // return;
                }
            };

            process2.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    // errorBuilder.AppendLine(e.Data);
                }
            };

            //   process.Start();
            //  process.BeginOutputReadLine();
            //  process.BeginErrorReadLine();

            // var processExited = process.WaitForExit(5000); // انتظار ۵ ثانیه

            //if (!processExited)
            //{
            //    process.Kill(); // در صورت انسداد، فرآیند را خاتمه دهید
            //}

            //  process.WaitForExit(); // منتظر بمانید تا فرآیند کامل شود

            //  string output = outputBuilder.ToString();
            //  string error = errorBuilder.ToString();

            //if (!string.IsNullOrWhiteSpace(error))
            //{
            //    Console.WriteLine($"Error: {error}");
            //}

            //Console.WriteLine($"Output: {output}");
            //  }
            //}
            //catch (Exception e)
            //{

            //}

        }

        public static void OpenShare(string networkPath, string username, string password)
        {
            //string networkPath = @"\\servername\sharedfolder";
            //string username = "your_username";
            //string password = "your_password";

            NetworkCredential credentials = new NetworkCredential(username, password);

            using (new NetworkConnection(networkPath, credentials))
            {
                var files = Directory.GetFiles(networkPath);
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
        }
        



        //static void Main()
        //{
        //    string networkPath = @"\\servername\sharedfolder";
        //    OpenNetworkFolder(networkPath);
        //}

        public static void OpenNetworkFolder(string networkPath)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c explorer \"{networkPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        errorBuilder.AppendLine(e.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                //var processExited = process.WaitForExit(2000);

                //if (!processExited)
                //{
                //    process.Kill();
                //}

                process.WaitForExit();

                string output = outputBuilder.ToString();
                string error = errorBuilder.ToString();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    Console.WriteLine($"Error: {error}");
                }

                Console.WriteLine($"Output: {output}");
            }



        }



        #region تست نام کاربری در شبکه برای جلوگیری از لاک شدن


       public static void UnlockAccountForAlways()
        {
            ProcessStartInfo psi = new ProcessStartInfo("net", "accounts /lockoutthreshold:0")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };  

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine("✅ نتیجه اجرای دستور:");
                Console.WriteLine(output);
            }

            //Console.WriteLine("تنظیمات با موفقیت اعمال شد. از این به بعد اکانت با رمز اشتباه لاک نمی‌شه.");
        }



        #endregion

        public static void SaveWindowsCredential(string target, string username, string password)
        {
            //var getCheckPC = TestConnection(target, username, password);
            //if (getCheckPC == false)
            //    return false;
            UnlockAccountForAlways();
            CredentialManager.WriteCredential(
                // applicationName: "TERMSRV/MOJTABAPC",
                applicationName: target,
                userName: username,
                secret: password,
                //type: CredentialType.DomainCertificate, Error
                //type: CredentialType.GenericCertificate, error
                type: CredentialType.DomainPassword,
                //type: CredentialType.Generic,
                persistence: CredentialPersistence.LocalMachine
            );
           // return true;
        }

        [DllImport("Advapi32.dll", SetLastError = true)]
        private static extern bool CredWrite([In] ref NativeCredential userCredential, [In] uint flags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct NativeCredential
        {
            public uint Flags;
            public uint Type;
            public string TargetName;
            public string UserName;
            public IntPtr CredentialBlob;
            public uint CredentialBlobSize;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr Comment;
        }
    }

}


