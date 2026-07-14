using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace MyCom.Class
{
    public static class ClassNetworkOther
    {
        public class StatusClient
        {
            public string Ip { get; set; }
            public string Mac { get; set; }
            public bool Status { get; set; }
            public string? GetRequestClient { get; set; }
        }
        //static void Main(string[] args)
        //{
        //    List<StatusClient> clients = new List<StatusClient>
        //    {
        //        new StatusClient { Ip = "192.168.1.1", Mac = "00-14-22-01-23-45", Status = false },
        //        new StatusClient { Ip = "192.168.1.2", Mac = "00-14-22-01-23-46", Status = false },
        //        new StatusClient { Ip = "192.168.1.3", Mac = "00-14-22-01-23-47", Status = false }
        //    };

        //    CheckClientsStatus(clients).Wait();

        //    //foreach (var client in clients)
        //    //{
        //    //    Console.WriteLine($"IP: {client.Ip}, MAC: {client.Mac}, Status: {client.Status}");
        //    //}
        //}

        public static async Task CheckClientsStatus(List<StatusClient> clients)
        {
            List<Task> pingTasks = new List<Task>();

            foreach (var client in clients)
            {
                pingTasks.Add(Task.Run(() => PingHost(client)));
            }

            await Task.WhenAll(pingTasks);
        }

        static void PingHost(StatusClient client)
        {
            Ping pinger = new Ping();

            try
            {
                PingReply reply = pinger.Send(client.Ip);
                client.Status = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                client.Status = false;
            }
        }



        public static void TurnOnPC(string macAddress)
        {
            using (UdpClient client = new UdpClient())
            {
                client.Connect(IPAddress.Broadcast, 9); // Port 9 is commonly used for Wake-on-LAN
                byte[] packet = new byte[102];
                for (int i = 0; i < 6; i++)
                {
                    packet[i] = 0xFF;
                }
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        packet[i * 6 + j + 6] = (byte)Convert.ToUInt32(macAddress.Substring(j * 2, 2), 16);
                    }
                }
                client.Send(packet, packet.Length);
            }
        }

        public static void TurnOnOtherComputerNetwork(string macAddress)
        {
            WOLClass client = new WOLClass();
            client.Connect(new IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
                0x2fff); // port=12287 let's use this one 
            client.SetClientToBrodcastMode();
            //set sending bites
            int counter = 0;
            //buffer to be send
            byte[] bytes = new byte[1024];   // more than enough :-)
            //first 6 bytes should be 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    string substring = macAddress.Substring(i, 2);
                    bytes[counter++] =
                        byte.Parse(substring, NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //now send wake up packet
            int reterned_value = client.Send(bytes, 1024);
        }
        public static string ExtractIpAddress(string path)
        {
            var match = Regex.Match(path, @"\\\\(\d+\.\d+\.\d+\.\d+)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
        public static string CleanNetworkPath(string rawPath)
        {
            if (string.IsNullOrWhiteSpace(rawPath))
                return string.Empty;

            // حذف فاصله‌های اضافی ابتدا و انتها
            string trimmed = rawPath.Trim();

            // تبدیل تمام بک‌اسلش‌های پشت‌سرهم به یک بک‌اسلش
            string cleaned = Regex.Replace(trimmed, @"\\{2,}", @"\");

            // اطمینان از شروع مسیر با دو بک‌اسلش (برای مسیر شبکه)
            if (!cleaned.StartsWith(@"\\"))
            {
                cleaned = @"\\" + cleaned.TrimStart('\\');
            }

            return cleaned;
        }

        public static void CreateAndShareFolder(string folderPath, string shareName)
        {

            // ساخت پوشه
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("📁 پوشه ساخته شد: " + folderPath);
            }

            // دستور PowerShell برای اشتراک‌گذاری پوشه با دسترسی کامل برای همه کاربران
            //  string shareName = Path.GetFileName(folderPath);
            string currentPath = Path.Combine(AppContext.BaseDirectory, folderPath);
            Console.WriteLine("📁 مسیر فعلی: " + currentPath);
      


            //string folderName = "MySharedFolder";
            //string folderPath = @"C:\مسیر\پوشه"; // مسیر واقعی پوشه‌ات رو جایگزین کن

            string command = $"net share {shareName}=\"{currentPath}\" /grant:Everyone,full";

            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    Console.WriteLine("✅ پوشه با موفقیت به اشتراک گذاشته شد.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ خطا در اشتراک‌گذاری پوشه:");
                Console.WriteLine(ex.Message);
            }
        }



    }



    public class WOLClass : UdpClient
    {
        //this is needed to send broadcast packet
        public void SetClientToBrodcastMode()
        {
            if (this.Active)
                this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 0);
        }


    }
}
