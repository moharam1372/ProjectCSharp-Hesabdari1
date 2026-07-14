using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCom.Class
{
    public static class GlobalExceptionHandler
    {
        public static void Initialize(string appName = "برنامه من", bool showMessageBox = true, bool openFile = false)
        {
            Application.ThreadException += (s, e) =>
                HandleException(e.Exception, $"{appName} - UI Thread", showMessageBox, openFile);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                HandleException(e.ExceptionObject as Exception, $"{appName} - AppDomain", showMessageBox, openFile);

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                HandleException(e.Exception, $"{appName} - TaskScheduler", showMessageBox, openFile);
                e.SetObserved(); // جلوگیری از کرش
            };
            //TaskCanceledException 
        }

        private static void HandleException(Exception? ex, string source, bool showMessageBox, bool openFile )
        {
            if (ex == null) return;

            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogWin");
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                string fileName = $"error_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
                string fullPath = Path.Combine(logPath, fileName);

                var sb = new StringBuilder();
                sb.AppendLine($"Exception Source: {source}");
                sb.AppendLine($"Message: {ex.Message}");
                sb.AppendLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    sb.AppendLine($"Inner Exception: {ex.InnerException.Message}");
                    sb.AppendLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }

                File.WriteAllText(fullPath, sb.ToString());

                if (showMessageBox)
                    MessageBox.Show(ex.Message, @"خطای غیرمنتظره", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (openFile)
                    Process.Start(fullPath);
            }
            catch
            {
                // اگر لاگ‌گیری خودش خطا داد، سکوت کن
            }
        }
    }
}