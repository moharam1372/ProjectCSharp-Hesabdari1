using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public static class ClassPrinter
    {
        public static PrintQueue GetInfo()
        {
            var allPrintersInfo = new Dictionary<string, int>();

            // روش 1: استفاده از PrinterSettings.InstalledPrinters برای دریافت نام همه پرینترها
            foreach (string printerName in PrinterSettings.InstalledPrinters.Where(w=>w.Contains("HP LaserJet ")))
            {
                try
                {
                    PrintQueue queue = null;

                    // بررسی می‌کنیم که آیا پرینتر روی سرور شبکه است یا محلی
                    if (printerName.StartsWith(@"\\"))
                    {
                        // پرینتر شبکه - استخراج نام سرور و پرینتر
                        string serverPath = printerName.Substring(0, printerName.LastIndexOf('\\'));
                        string printerShortName = printerName.Substring(printerName.LastIndexOf('\\') + 1);

                        using (PrintServer server = new PrintServer(serverPath))
                        {
                            queue = server.GetPrintQueue(printerShortName);
                        }
                    }
                    else
                    {
                        // پرینتر محلی
                        using (LocalPrintServer server = new LocalPrintServer())
                        {
                            queue = server.GetPrintQueue(printerName);
                        }
                    }

                    if (queue != null)
                    {
                        queue.Refresh();
                        return queue;
                        allPrintersInfo[printerName] = queue.NumberOfJobs;
                        queue.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@$"خطا در دریافت اطلاعات پرینتر {printerName}: {ex.Message}");
                }
            }

            // نمایش نتایج
            Console.WriteLine("خلاصه وضعیت صف پرینترها:\n");
            foreach (var item in allPrintersInfo)
            {
                Console.WriteLine(@$"{item.Key}: {item.Value} کار در صف");
            }


            return null;
        }
    }
}
