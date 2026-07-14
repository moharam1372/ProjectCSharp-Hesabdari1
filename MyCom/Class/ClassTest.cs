using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyCom.Class
{
    public static class ClassTest
    {
        private static List<string> lic1 = new List<string>
        {
            "N9YD25A20A03551", // Salim

            #region Mojtaba Yadavar

            "WD-WCAW33164744", // Mojtaba
            "WD-WCC4J3YP0L7K", // Mojtaba
            "KJ202101001405833", // Mojtaba
            "WD-WCAYUCN24584", // Mojtaba
            "7F1820012180", // Mojtaba
            "CFB0F1DE50C0", // Mojtaba
            "N9YG07A20A00703", // Mojtaba
            //"VB57685860-f352b113" // Mojtaba

            #endregion

        };
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("g5"));
                }
                return builder.ToString();
            }
        }

        public class model
        {
            public bool B { get; set; }
            public DateTime? DateTime { get; set; }
            public string Eshterak { get; set; }
        }   
        public static model CheckLice()
        {
            model ff = new model();
            var getHdd = Get();

            var getS11 = Properties.Settings.Default.s1;
            var isNullOrEmpty = string.IsNullOrEmpty(getS11);
            bool exists = File.Exists("gameport.db");
            if (isNullOrEmpty && !exists)
            {
                var dateTime = GetDate().Value;
                Properties.Settings.Default.s1 = EncryptString(dateTime.ToString());
                Properties.Settings.Default.s2 = EncryptString(dateTime.AddDays(30).ToString());
                Properties.Settings.Default.Save();
                StreamWriter sw = new StreamWriter("gameport.db");
                sw.Close();
                ff.B = true;
                CheckLice();
            }
            else if (!isNullOrEmpty)
            {
                var getS1 = Properties.Settings.Default.s1;
                DateTime getS2 = Convert.ToDateTime(DecryptString(Properties.Settings.Default.s2));

                var getDatetime = DateTime.Now;

                if (getS2 >= getDatetime)
                {
                    ff.B = true;
                    ff.DateTime = getS2;
                }
                else
                {
                    ff.B = false;
                }
            }
            else
            {
                ff.B = false;
            }
            foreach (var h in getHdd)
            {
                if (lic1.Any(a => a == h.Replace(" ", "")))
                {
                    ff.B = true;
                    ff.DateTime = null;
                    ff.Eshterak = CreateSerial(lic1.First(a => a == h.Replace(" ", "")));
                }
            }
            return ff;
        }

        public class TimeData
        {
            public string datetime { get; set; }
        }

        public class Result1
        {
            public string Timezone { get; set; }
            public string Action { get; set; }
            public string Zone { get; set; }
            public string Date { get; set; }
            public string Year { get; set; }
            public string Month { get; set; }
            public string Day { get; set; }
            public string YearName { get; set; }
            public string MonthName { get; set; }
            public string DayName { get; set; }
            public string SeasonName { get; set; }
        }

        public class ApiResponse
        {
            public bool Ok { get; set; }
            public string Status { get; set; }
            public int StatusCode { get; set; }
            public Result1 Result { get; set; }
        }

        public static DateTime? GetDate()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync("https://api.ineo-team.ir/timezone.php?action=date&zone=fa").Result;
                    // HttpResponseMessage response = client.GetAsync("http://worldtimeapi.org/api/timezone/Etc/UTC").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    var timeData = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                    var timeData1 = timeData.Result as Result1;
                    //  TimeData timeData = JsonConvert.DeserializeObject<string >(responseBody);
                    DateTime dateTime = FormatDate(timeData1.Date).ShamsiToMiladi();
                    return dateTime;
                    // return dateTime;

                    // Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }

            return null;
        }


        static string FormatDate(string date)
        {
            // تاریخ را به اجزای مختلف تقسیم می‌کنیم
            string[] parts = date.Split('/');

            // بررسی تعداد اجزاء
            if (parts.Length != 3)
            {
                throw new FormatException("Invalid date format.");
            }

            // تبدیل ماه و روز به فرمت دو رقمی
            string year = parts[0];
            string month = parts[1].PadLeft(2, '0');
            string day = parts[2].PadLeft(2, '0');

            // ساخت تاریخ فرمتی جدید
            return $"{year}/{month}/{day}";
        }


        public static List<string> Get()
        {
            List<string> fff = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                fff.Add(wmi_HD["SerialNumber"].ToString());
            }
            return fff;
        }
        public static string EncryptString(this string plainText)
        {
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                var iv = Encoding.UTF8.GetBytes("gameprt206587424");
                aes.Key = Encoding.UTF8.GetBytes("2587453621gameprt2024gameprt2024"); // کلید باید 32 بایت باشد
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(this string cipherText)
        {
            byte[] iv = Encoding.UTF8.GetBytes("gameprt206587424");
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("2587453621gameprt2024gameprt2024"); // کلید باید 32 بایت باشد
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        private static string CreateSerial(string input)
        {
            // استفاده از SHA256 برای ایجاد هش ورودی
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // انتخاب حروف بزرگ (بدون "L" و "I")
                const string chars = "ABCDEFGHJKMNOPQRSTUVWXYZ";
                StringBuilder result = new StringBuilder(11); // 2 حرف + 6 عدد با خط تیره

                // افزودن دو حرف بزرگ
                for (int i = 0; i < 2; i++)
                {
                    result.Append(chars[bytes[i] % chars.Length]);
                }

                // افزودن اعداد با فرمت xx-xxx-xxx
                result.Append('-');
                for (int i = 2; i < 5; i++)
                {
                    result.Append(bytes[i] % 10);
                }
                result.Append('-');
                for (int i = 5; i < 8; i++)
                {
                    result.Append(bytes[i] % 10);
                }

                return result.ToString();
            }
        }


        //public static string CreateSerial()
        //{
        //    Random random = new Random();
        //    const string chars = "ABCDEFGHJKMNOPQRSTUVWXYZ"; // حذف "L" و "I"
        //    const string numbers = "0123456789"; // تولید دو حرف بزرگ انگلیسی
        //    string letters =
        //        new string(Enumerable.Repeat(chars, 2).Select(s => s[random.Next(s.Length)])
        //            .ToArray()); // تولید هشت عدد
        //    string digits =
        //        new string(Enumerable.Repeat(numbers, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        //    return letters + digits;
        //}
    }
}


