using System;
using System.Globalization;

namespace MyCom.Class
{
    public class Class_Code
    {
        private readonly PersianCalendar Per_C = new PersianCalendar();
     //123
        public string T_Year()
        {
            var Lbl_Date = Per_C.GetYear(DateTime.Today).ToString();
            return (Lbl_Date);
        }
        //1
        public string T_Month()
        {
            var Lbl_Date = "";

            Lbl_Date = Per_C.GetMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + Per_C.GetMonth(DateTime.Today)
                : Lbl_Date + Per_C.GetMonth(DateTime.Today);

            return (Lbl_Date);
        }
        public string T_Day()
        {
            var Lbl_Date = "";

            Lbl_Date = Per_C.GetDayOfMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + Per_C.GetDayOfMonth(DateTime.Today)
                : Lbl_Date + Per_C.GetDayOfMonth(DateTime.Today);

            return (Lbl_Date);
        }

        public string T_USYear()
        {
            var Lbl_Date = DateTime.Today.Year.ToString();
            return (Lbl_Date);
        }
        public string T_USMonth()
        {
            var Lbl_Date = "";

            Lbl_Date = DateTime.Today.Month < 10
                ? Lbl_Date + "0" + DateTime.Today.Month
                : Lbl_Date + DateTime.Today.Month;

            return (Lbl_Date);
        }
        public string T_USDay()
        {
            var Lbl_Date = "";

            Lbl_Date = DateTime.Today.Day < 10
                ? Lbl_Date + "0" + DateTime.Today.Day
                : Lbl_Date + DateTime.Today.Day;

            return (Lbl_Date);
        }

        public int Per_Code(int NCode, string Max)
        {
            if (NCode <= 0) return (0);
            if (decimal.Parse(Max) < 100)
                Max = "0" + Max;
            else if (decimal.Parse(Max) < 10)
                Max = "00" + Max;

            var Temp = NCode.ToString();
            var T1 = Today("Date").Substring(2, 2);
            T1 = T1 + Temp.Substring(Temp.Length - 2, 2) + Max;
            return (int.Parse(T1));
        }
        public string Today(string Date_Time)
        {
            var Lbl_Time = DateTime.Now.Hour < 10 ? "0" + (DateTime.Now.Hour) + ":" : (DateTime.Now.Hour) + ":";

            Lbl_Time = DateTime.Now.Minute < 10
                ? Lbl_Time + "0" + (DateTime.Now.Minute) + ":"
                : Lbl_Time + (DateTime.Now.Minute) + ":";

            Lbl_Time = DateTime.Now.Second < 10
                ? Lbl_Time + "0" + (DateTime.Now.Second)
                : Lbl_Time + (DateTime.Now.Second);

            var Lbl_Date = Per_C.GetYear(DateTime.Today) + "/";

            Lbl_Date = Per_C.GetMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + Per_C.GetMonth(DateTime.Today) + "/"
                : Lbl_Date + Per_C.GetMonth(DateTime.Today) + "/";

            Lbl_Date = Per_C.GetDayOfMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + Per_C.GetDayOfMonth(DateTime.Today)
                : Lbl_Date + Per_C.GetDayOfMonth(DateTime.Today);

            return Date_Time == "Time" ? Lbl_Time : (Date_Time == "Date" ? (Lbl_Date) : ("Error"));
        }

        public string Shamsi2Miladi(string sdate, string Day_Date, bool Name_Month)
        {
            var year = int.Parse(sdate.Substring(0, 4));
            var month = int.Parse(sdate.Substring(5, 2));
            var day = int.Parse(sdate.Substring(8, 2));
            var p = new PersianCalendar();
            var mdate = p.ToDateTime(year, month, day, 0, 0, 0, 0);

            if (string.Compare(Day_Date, "date", true) == 0)
                return mdate.ToString().Substring(0, 10);
            if (string.Compare(Day_Date, "day", true) == 0)
                return Name_Month == false
                    ? Day_Code(mdate.DayOfWeek.ToString())
                    : Day_Shamsi(mdate.DayOfWeek.ToString());
            return "Error";
        }
        public string Day_Shamsi(string Day_Miladi)
        {
            switch (Day_Miladi)
            {
                case "Saturday":
                    return ("شنبه");
                case "Sunday": ;
                    return ("یک شنبه");
                case "Monday":
                    return ("دو شنبه");
                case "Tuesday":
                    return ("سه شنبه");
                case "Wednesday":
                    return ("چهار شنبه");
                case "Thursday":
                    return ("پنج شنبه");
                case "Friday":
                    return ("جمعه");
                default:
                    return ("");
            }
        }
        public string Day_Code(string Day_Miladi)
        {
            switch (Day_Miladi)
            {
                case "Saturday":
                    return ("0");
                case "Sunday": ;
                    return ("1");
                case "Monday":
                    return ("2");
                case "Tuesday":
                    return ("3");
                case "Wednesday":
                    return ("4");
                case "Thursday":
                    return ("5");
                case "Friday":
                    return ("6");
                default:
                    return ("");
            }
        }
        public string Get_Name_Mounth(int Code_Mounth)
        {
            var MounthName = string.Empty;
            switch (Code_Mounth)
            {
                case 1:
                    MounthName = "فروردین";
                    break;
                case 2:
                    MounthName = "اردیبهشت";
                    break;
                case 3:
                    MounthName = "خرداد";
                    break;
                case 4:
                    MounthName = "تیر";
                    break;
                case 5:
                    MounthName = "مرداد";
                    break;
                case 6:
                    MounthName = "شهریور";
                    break;
                case 7:
                    MounthName = "مهر";
                    break;
                case 8:
                    MounthName = "آبان";
                    break;
                case 9:
                    MounthName = "آذر";
                    break;
                case 10:
                    MounthName = "دی";
                    break;
                case 11:
                    MounthName = "بهمن";
                    break;
                case 12:
                    MounthName = "اسفند";
                    break;
            }
            return MounthName;
        }
        public string Save_Date()
        {
            var Lbl_Time = DateTime.Now.Hour < 10 ? "0" + (DateTime.Now.Hour) + "-" : (DateTime.Now.Hour) + "-";

            Lbl_Time = DateTime.Now.Minute < 10
                ? Lbl_Time + "0" + (DateTime.Now.Minute) + "-"
                : Lbl_Time + (DateTime.Now.Minute) + "-";

            Lbl_Time = DateTime.Now.Second < 10
                ? Lbl_Time + "0" + (DateTime.Now.Second)
                : Lbl_Time + (DateTime.Now.Second);

            var Lbl_Date = Per_C.GetYear(DateTime.Today) + "-";

            Lbl_Date = Per_C.GetMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + Per_C.GetMonth(DateTime.Today) + "-"
                : Lbl_Date + Per_C.GetMonth(DateTime.Today) + "-";

            Lbl_Date = Per_C.GetDayOfMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + Per_C.GetDayOfMonth(DateTime.Today)
                : Lbl_Date + Per_C.GetDayOfMonth(DateTime.Today);

            return Lbl_Date + "   &   " + Lbl_Time;
        }
        public string Mon1()
        {
            return Per_C.GetMonth(DateTime.Today) < 10
                ? Per_C.GetYear(DateTime.Today) + @"/0" + Per_C.GetMonth(DateTime.Today) + @"/"
                : Per_C.GetYear(DateTime.Today) + @"/" + Per_C.GetMonth(DateTime.Today) + @"/";
        }
        public string Alg_Coding(string Str_Pass, string Str_User)
        {
            if (Str_Pass != "")
            {
                var Tmp_Len = Str_Pass.Length + Str_User.Length;
                var Tmp_Pass_Asci = 0.ToString();
                var Tmp_User_Asci = 0.ToString();
                for (var i = 0; i < Str_Pass.Length; i++)
                    Tmp_Pass_Asci = Tmp_Pass_Asci + (int)char.Parse(Str_Pass.Substring(i, 1));

                for (var i = 0; i < Str_User.Length; i++)
                    Tmp_User_Asci = Tmp_User_Asci + (int)char.Parse(Str_User.Substring(i, 1));

                double Tmp_Coding1;
                if (Convert.ToDouble(Tmp_Pass_Asci) > Convert.ToDouble(Tmp_User_Asci))
                    Tmp_Coding1 = Convert.ToDouble(Tmp_Pass_Asci) - Convert.ToDouble(Tmp_User_Asci);
                else if (Convert.ToDouble(Tmp_Pass_Asci) < Convert.ToDouble(Tmp_User_Asci))
                    Tmp_Coding1 = Convert.ToDouble(Tmp_User_Asci) - Convert.ToDouble(Tmp_Pass_Asci);
                else
                    Tmp_Coding1 = Convert.ToDouble(Tmp_Pass_Asci) + Convert.ToDouble(Tmp_User_Asci);

                Tmp_Coding1 = Tmp_Coding1 / Tmp_Len;
                return Tmp_Coding1.ToString();
            }
            return null;
        }

      
        //public void Ply_Sound(string Address)
        //{
        //    var WMP1 = new WindowsMediaPlayer();
        //    try
        //    {
        //        if (File.Exists(Address))
        //            WMP1.URL = Address;
        //    }
        //    catch (Exception)
        //    {   }
        //}
    }
}
