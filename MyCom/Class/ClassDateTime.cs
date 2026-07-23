using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using static MyCom.Class.ClsDateTime;

namespace MyCom.Class
{
    public static class ClsDateTime
    {
        static PersianCalendar _calenPersian = new PersianCalendar();

        public static class ModelDateTimePersianFunction
        {
            public static ModelDateTimePersian BetweenDateTime(DateTime date1, DateTime date2)
            {
                var getBetWeen = date2 - date1;

                ModelDateTimePersian persian = new ModelDateTimePersian
                {
                    Day = getBetWeen.TotalDays.ToString("N0"),
                    Hour = getBetWeen.TotalHours.ToString("N0"),
                    Minute = getBetWeen.TotalMinutes.ToString("N0"),
                    Second = getBetWeen.TotalSeconds.ToString("N0"),
                };
                return persian;
            }
            public static ModelDateTimePersian BetweenDateTime(ModelDateTimePersian date1, ModelDateTimePersian date2)
            {
                var getBetWeen = date2.DateTime - date1.DateTime;

                ModelDateTimePersian persian = new ModelDateTimePersian
                {
                    Day = getBetWeen.TotalDays.ToString("N0"),
                    Hour = getBetWeen.TotalHours.ToString("N0"),
                    Minute = getBetWeen.TotalMinutes.ToString("N0"),
                    Second = getBetWeen.TotalSeconds.ToString("N0"),
                };
                return persian;
            }
        }

        public class ModelDateTimePersian
        {
            #region Base

            public DateTime DateTime { get; set; }
            public string Year { get; set; }
            public string Month { get; set; }
            public string Day { get; set; }
            public string Hour { get; set; }
            public string Minute { get; set; }
            public string Second { get; set; }

            public string LongDateTime => Year + "/" + Month + "/" + Day + " - " + Hour + ":" + Minute + ":" + Second;
            public string LongTimeDate => Hour + ":" + Minute + ":" + Second + " - " + Year + "/" + Month + "/" + Day;
            public string DateTimeForName => Year + "-" + Month + "-" + Day + "  -  (" + Hour + "-" + Minute + "-" + Second+")";


            public string ShortDateTime => Year + "/" + Month + "/" + Day + " - " + Hour + ":" + Minute;
            public string ShortTimeDate => Hour + ":" + Minute + " - " + Year + "/" + Month + "/" + Day;

            public string YearMonth => Year + "/" + Month;
            public string MonthDay => Month + "/" + Day;
            public string Date => Year + "/" + Month + "/" + Day;

            public string Time => Hour + ":" + Minute + ":" + Second;
            public string HourMinute => Hour + ":" + Minute;
            public string MinuteSecond => Minute + ":" + Second;

            #endregion

            /// <summary>
            /// نام روز هفته
            /// </summary>
            /// <returns></returns>
            public string WeekDay => DayShamsi(DateTime.DayOfWeek.ToString());

            //public string WeekDayFA(ModelDate date)
            //{
            //    if (date == ModelDate.Gregorian)
            //        return DayShamsi(DateTime.DayOfWeek.ToString());
            //    var p = new PersianCalendar();
            //    return DayShamsi(p.GetDayOfWeek(DateTime).ToString());
            //}
            public int WeekDayNumber => DayNumber(DateTime.DayOfWeek.ToString());

            public ModelDateTimePersian AddYear(int year, ModelDate date)
            {
                if (date == ModelDate.Gregorian)
                    return DateTimePersian(DateTime.AddYears(year));
                var p = new PersianCalendar();
                return DateTimePersian(p.AddYears(DateTime, year));
            }
            public ModelDateTimePersian AddDay(int day, ModelDate date)
            {
                if (date == ModelDate.Gregorian)
                    return DateTimePersian(DateTime.AddDays(day));
                var p = new PersianCalendar();
                return DateTimePersian(p.AddDays(DateTime, day));
            }
            public ModelDateTimePersian AddMonth(int month, ModelDate date)
            {
                if (date == ModelDate.Gregorian)
                    return DateTimePersian(DateTime.AddMonths(month));
                var p = new PersianCalendar();
                return DateTimePersian(p.AddMonths(DateTime, month));
            }

            /// <summary>
            /// تاریخ روز آخر ماه
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            ///   
            public ModelDateTimePersian EndDayMonth(ModelDateTimePersian date)
            {
                var tmpDay = 32;

                int tmpMonth = Convert.ToInt32(date.Month);
                var tmpYear = _calenPersian.IsLeapYear(Convert.ToInt32(date.Year));

                if (tmpMonth > 6 && tmpMonth < 12)
                    tmpDay = 30;

                else if (tmpMonth >= 1 && tmpMonth <= 6)
                    tmpDay = 31;

                if (tmpYear && tmpMonth == 12)
                    tmpDay = 30;

                else if (tmpYear == false && tmpMonth == 12)
                    tmpDay = 29;

                var date1 = DateTimePersian(new DateTime(Convert.ToInt32(DateTime.Year), Convert.ToInt32(DateTime.Month), tmpDay));

                return date1;
            }
            public ModelDateTimePersian EndDayMonth()
            {
                var tmpDay = 32;

                int tmpMonth = Convert.ToInt32(Month);
                var tmpYear = _calenPersian.IsLeapYear(Convert.ToInt32(Year));

                if (tmpMonth > 6 && tmpMonth < 12)
                    tmpDay = 30;

                else if (tmpMonth >= 1 && tmpMonth <= 6)
                    tmpDay = 31;

                if (tmpYear && tmpMonth == 12)
                    tmpDay = 30;

                else if (tmpYear == false && tmpMonth == 12)
                    tmpDay = 29;

                //   var getToMilady = new DateTime();
                var getToMilady = (Year + "/" + Month + "/" + tmpDay).ShamsiToMiladi();

                var date1 = DateTimePersian(getToMilady);

                return date1;
            }

            public ModelDateTimePersian EndDayMonthFA()
            {
                var tmpDay = 32;

                int tmpMonth = Convert.ToInt32(Month);
                var tmpYear = _calenPersian.IsLeapYear(Convert.ToInt32(Year));


                if (tmpMonth > 6 && tmpMonth < 12)
                    tmpDay = 30;

                else if (tmpMonth >= 1 && tmpMonth <= 6)
                    tmpDay = 31;

                if (tmpYear && tmpMonth == 12)
                    tmpDay = 30;

                else if (tmpYear == false && tmpMonth == 12)
                    tmpDay = 29;

                var getToMilady = new DateTime();
                //   var getToMilady = "".ShamsiToMiladi();

                var date1 = DateTimePersian(getToMilady);

                return date1;
            }

            public enum ModelDate
            {
                Persian,
                Gregorian
            }
        }



        private static string DayShamsi(string dayMiladi)
        {
            switch (dayMiladi.ToLower())
            {
                case "saturday":
                    return "شنبه";
                case "sunday":
                    return "یک شنبه";
                case "monday":
                    return "دو شنبه";
                case "tuesday":
                    return "سه شنبه";
                case "wednesday":
                    return "چهار شنبه";
                case "thursday":
                    return "پنج شنبه";
                case "friday":
                    return "جمعه";
                default:
                    return "Error";
            }
        }
        private static int DayNumber(string dayMiladi)
        {
            switch (dayMiladi.ToLower())
            {
                case "saturday":
                    return 0;
                case "sunday":
                    return 1;
                case "monday":
                    return 2;
                case "tuesday":
                    return 3;
                case "wednesday":
                    return 4;
                case "thursday":
                    return 5;
                case "friday":
                    return 6;
                default:
                    return -1;
            }
        }
        public static ModelDateTimePersian DateTimePersian(this DateTime dateTime)
        {
            var convert = Convert.ToDateTime(dateTime);

            var p = new PersianCalendar();
            var year = p.GetYear(convert);

            string month;
            if (p.GetMonth(convert) < 10)
                month = "0" + p.GetMonth(convert);
            else
                month = p.GetMonth(convert).ToString();

            string day;
            if (p.GetDayOfMonth(convert) < 10)
                day = "0" + p.GetDayOfMonth(convert);
            else
                day = p.GetDayOfMonth(convert).ToString();

            ModelDateTimePersian persian = new ModelDateTimePersian
            {
                DateTime = dateTime,
                Year = year.ToString(),
                Month = month,
                Day = day,
                Hour = dateTime.Hour < 10 ? "0" + dateTime.Hour : dateTime.Hour.ToString(),
                Minute = dateTime.Minute < 10 ? "0" + dateTime.Minute : dateTime.Minute.ToString(),
                Second = dateTime.Second < 10 ? "0" + dateTime.Second : dateTime.Second.ToString(),
            };
            return persian;
        }

        public static ModelDateTimePersian TodayPersian()
        {
            var modelDateTimePersian = DateTimePersian(DateTime.Now);
            return modelDateTimePersian;
        }

        public static string CheckNullDate(this DateTime? date)
        {
            var date2 = date != null ? date.Value.Date.DateTimePersian().Date : "";
            return date2;
        }

        //public static string CheckNullDate(this ModelDateTimePersian date)
        //{
        //    var date2 = date != null ? date.DateTime.Date.DateTimePersian().Date : "";
        //    return date2;
        //}
    }

    public static class ClassDateTime
    {
        private static readonly PersianCalendar Per_C = new PersianCalendar();

        public static string تاریخ => "Date";
        public static string ساعت => "Time";

        public static string GetYear(string date = null)
        {
            if (date != null)
                return date.Substring(0, 4);
            return Today("Date").Substring(0, 4);
        }
        public static string GetMonth(string date = null)
        {
            if (date != null)
                return date.Substring(5, 2);
            return Today("Date").Substring(5, 2);
        }
        public static string GetDay(string date = null)
        {
            if (date != null)
                return date.Substring(8, 2);
            return Today("Date").Substring(8, 2);
        }
        public static string GetHour(string date = null)
        {
            if (date != null)
            {
                // if (date.Length == 5 || date.Length == 9)
                return date.Substring(0, 2);
            }
            return Today("Time").Substring(0, 2);
        }
        public static string GetMinute(string date = null)
        {
            if (date != null)
            {
                return date.Substring(3, 2);
            }
            return Today("Time").Substring(3, 2);
        }

        public static string Today(string _DateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            var Lbl_Time = DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour + ":" : DateTime.Now.Hour + ":";

            Lbl_Time = DateTime.Now.Minute < 10
                ? Lbl_Time + "0" + DateTime.Now.Minute + ":"
                : Lbl_Time + DateTime.Now.Minute + ":";

            Lbl_Time = DateTime.Now.Second < 10
                ? Lbl_Time + "0" + DateTime.Now.Second
                : Lbl_Time + DateTime.Now.Second;

            var Lbl_Date = calendar.GetYear(DateTime.Today) + "/";

            Lbl_Date = calendar.GetMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + calendar.GetMonth(DateTime.Today) + "/"
                : Lbl_Date + calendar.GetMonth(DateTime.Today) + "/";

            Lbl_Date = calendar.GetDayOfMonth(DateTime.Today) < 10
                ? Lbl_Date + "0" + calendar.GetDayOfMonth(DateTime.Today)
                : Lbl_Date + calendar.GetDayOfMonth(DateTime.Today);

            return _DateTime == "Time" ? Lbl_Time : (_DateTime == "Date" ? Lbl_Date : "Error");
        }

        public static string GetYearNow(string date = null)
        {
            var getNow = date ?? Today("Date");
            return getNow.Substring(0, 4);
        }
        public static string GetMonthNow(string date = null)
        {
            var getNow = date ?? Today("Date");
            return getNow.Substring(5, 2);
        }
        public static string GetDayNow(string date = null)
        {
            var getNow = date ?? Today("Date");
            return getNow.Substring(8, 2);
        }

        public static string GetYearNow(DateTime date)
        {
            // var getNow = date == default(DateTime) ? DateTime.Now : date;
            var getToShamsi = MiladiToShamsi(date, "Date");
            return getToShamsi.Substring(0, 4);
        }
        public static string GetMonthNow(DateTime date)
        {

            //   var getNow = date == default(DateTime) ? DateTime.Now : date;
            var getToShamsi = MiladiToShamsi(date, "Date");
            return getToShamsi.Substring(5, 2);
        }
        public static string GetDayNow(DateTime date)
        {

            //var getNow = date == default(DateTime) ? DateTime.Now : date;
            var getToShamsi = MiladiToShamsi(date, "Date");
            return getToShamsi.Substring(8, 2);
        }
        public static ModelDateTimePersian MergeDateAndTime(string persianDate, string end)
        {
            var getDate = persianDate.ShamsiToMiladi();
            var date1 = new DateTime(getDate.Year, getDate.Month, getDate.Day,
                Convert.ToInt32(end.Substring(0, 2)), Convert.ToInt32(end.Substring(3, 2)), 0);
            var gg = date1.DateTimePersian();
            return gg;
        }
        public static string ShamsiToMiladi(string sdate, string Day_Date)
        {

            int year = Convert.ToInt32(sdate.Substring(0, 4));
            int month = Convert.ToInt32(sdate.Substring(5, 2));
            int day = Convert.ToInt32(sdate.Substring(8, 2));
            var p = new PersianCalendar();

            //if (Convert.ToInt32(month) < 10)
            //    month = "0" + month;

            //if (Convert.ToInt32(day) < 10)
            //    day = "0" + day;

            var mdate = p.ToDateTime(year, month, day, 0, 0, 0, 0);


            if (String.Compare(Day_Date.ToLower(), "date", true) == 0)
                return mdate.ToShortDateString();

            return String.Compare(Day_Date.ToLower(), "day", true) == 0
                ? DayShamsi(mdate.DayOfWeek.ToString())
                : "Error";
        }

        public static DateTime ShamsiToMiladi(this string sdate)
        {

            int year = Convert.ToInt32(sdate.Substring(0, 4));
            int month = Convert.ToInt32(sdate.Substring(5, 2));
            int day = Convert.ToInt32(sdate.Substring(8, 2));
            var p = new PersianCalendar();

            //if (Convert.ToInt32(month) < 10)
            //    month = "0" + month;

            //if (Convert.ToInt32(day) < 10)
            //    day = "0" + day;

            var mdate = p.ToDateTime(year, month, day, 0, 0, 0, 0);

            // if (string.Compare(Day_Date.ToLower(), "date", true) == 0)
            return Convert.ToDateTime(mdate.ToShortDateString());
        }

        /// <summary>
        ///  0000/00/00 - 00:00:00
        /// </summary>
        /// <param name="sdate"></param>
        /// <returns></returns>
        public static string ShamsiToMiladiPlusTime(this string sdate)
        {
            int year = Convert.ToInt32(sdate.Substring(6, 4));
            int month = Convert.ToInt32(sdate.Substring(3, 2));
            int day = Convert.ToInt32(sdate.Substring(0, 2));

            int Minute = Convert.ToInt32(sdate.Substring(14, 2));
            int Hour = Convert.ToInt32(sdate.Substring(11, 2));
            var p = new PersianCalendar();

            //if (Convert.ToInt32(month) < 10)
            //    month = "0" + month;

            //if (Convert.ToInt32(day) < 10)
            //    day = "0" + day;

            var mdate = p.ToDateTime(year, month, day, Hour, Minute, 0, 0);
            // MessageBox.Show(sdate.Substring(13, 2));
            //  MessageBox.Show(sdate.Substring(16, 2));
            // sdfsdfsdf

            return mdate.ToString("G");
        }

        /// <summary>
        ///  0000/00/00 - 00:00:00
        /// </summary>
        /// <param name="sdate"></param>
        /// <returns></returns>
        public static string MiladiToShamsiPlusTime(string sdate, bool In = false)
        {
            try
            {
                var Get_Convert = Convert.ToDateTime(sdate);

                var p = new PersianCalendar();
                var year = p.GetYear(Get_Convert);

                string month;
                if (p.GetMonth(Get_Convert) < 10)
                    month = "0" + p.GetMonth(Get_Convert);
                else
                    month = p.GetMonth(Get_Convert).ToString();

                string day;
                if (p.GetDayOfMonth(Get_Convert) < 10)
                    day = "0" + p.GetDayOfMonth(Get_Convert);
                else
                    day = p.GetDayOfMonth(Get_Convert).ToString();

                var Sum_Date = year + "/" + month + "/" + day;

                return Sum_Date + " - " + Get_Convert.ToString("HH:mm");

            }
            catch (Exception)
            {
                if (In)
                    return "";
                return "";
                //return "{عدم ثبت ورود}";
                //return "{عدم ثبت خروج}";
            }
        }

        public static string Miladi_To_Shamsi_Get_Month(DateTime sdate, string Day = "01")
        {

            {
                var Get_Convert = Convert.ToDateTime(sdate);

                var p = new PersianCalendar();
                var year = p.GetYear(Get_Convert);

                string month;
                if (p.GetMonth(Get_Convert) < 10)
                    month = "0" + p.GetMonth(Get_Convert);
                else
                    month = p.GetMonth(Get_Convert).ToString();

                var Sum_Date = year + "/" + month + "/" + Day;

                return Sum_Date;

            }

        }

        public static string MiladiToShamsi(this DateTime sdate, string Day_Date)
        {
            try
            {

                ChangeRegionTOEnglish();

                //key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                //key.SetValue("iCountry", "", RegistryValueKind.String);

                PersianCalendar calendar = new PersianCalendar();


                var year = calendar.GetYear(sdate);

                string month;
                if (calendar.GetMonth(sdate) < 10)
                    month = "0" + calendar.GetMonth(sdate);
                else
                    month = calendar.GetMonth(sdate).ToString();

                string day;
                if (calendar.GetDayOfMonth(sdate) < 10)
                    day = "0" + calendar.GetDayOfMonth(sdate);
                else
                    day = calendar.GetDayOfMonth(sdate).ToString();

                var Sum_Date = year + "/" + month + "/" + day;

                if (String.Compare(Day_Date.ToLower(), "date", true) == 0)
                    return Sum_Date;

                return String.Compare(Day_Date.ToLower(), "day", true) == 0
                    ? DayShamsi(sdate.DayOfWeek.ToString())
                    : "Error";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void ChangeRegionTOEnglish()
        {
            try
            {

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("LocaleName", "en-US", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("sCountry", "United States", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("sLanguage", "ENU", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("Locale", "00000409", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("iCalendarType", "1", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("iCountry", "1", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("iPaperSize", "1", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("iTLZero", "0", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("sCurrency", "$", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("sShortDate", "yyyy/MM/dd", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("s1159", "AM", RegistryValueKind.String);

                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                key.SetValue("s2359", "PM", RegistryValueKind.String);

                //key =
                //   Registry.CurrentUser.OpenSubKey(@"Control Panel\International\🌎🌏🌍", true);
                //key.SetValue("Calendar", "Gregorian", RegistryValueKind.String);
            }
            catch (Exception e)
            {

            }
        }

        public static string MiladiToShamsi(this DateTime sdate, string Day_Date, int Time)
        {
            try
            {
                var p = new PersianCalendar();
                var year = p.GetYear(sdate);

                string month;
                if (p.GetMonth(sdate) < 10)
                    month = "0" + p.GetMonth(sdate);
                else
                    month = p.GetMonth(sdate).ToString();

                string day;
                if (p.GetDayOfMonth(sdate) < 10)
                    day = "0" + p.GetDayOfMonth(sdate);
                else
                    day = p.GetDayOfMonth(sdate).ToString();

                string Hour;
                if (p.GetHour(sdate) < 10)
                    Hour = "0" + p.GetHour(sdate);
                else
                    Hour = p.GetHour(sdate).ToString();

                string Minute;
                if (p.GetMinute(sdate) < 10)
                    Minute = "0" + p.GetMinute(sdate);
                else
                    Minute = p.GetMinute(sdate).ToString();

                string Second;
                if (p.GetSecond(sdate) < 10)
                    Second = "0" + p.GetSecond(sdate);
                else
                    Second = p.GetSecond(sdate).ToString();

                var Sum_Date = year + "/" + month + "/" + day + " " + Hour + ":" + Minute + ":" + Second;

                if (String.Compare(Day_Date.ToLower(), "date", true) == 0)
                    return Sum_Date;

                return String.Compare(Day_Date.ToLower(), "day", true) == 0
                    ? DayShamsi(sdate.DayOfWeek.ToString())
                    : "Error";
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static string MiladiToShamsi_RTL(this DateTime sdate, string Day_Date, int Time)
        {
            try
            {
                var p = new PersianCalendar();
                var year = p.GetYear(sdate);

                string month;
                if (p.GetMonth(sdate) < 10)
                    month = "0" + p.GetMonth(sdate);
                else
                    month = p.GetMonth(sdate).ToString();

                string day;
                if (p.GetDayOfMonth(sdate) < 10)
                    day = "0" + p.GetDayOfMonth(sdate);
                else
                    day = p.GetDayOfMonth(sdate).ToString();

                string Hour;
                if (p.GetHour(sdate) < 10)
                    Hour = "0" + p.GetHour(sdate);
                else
                    Hour = p.GetHour(sdate).ToString();

                string Minute;
                if (p.GetMinute(sdate) < 10)
                    Minute = "0" + p.GetMinute(sdate);
                else
                    Minute = p.GetMinute(sdate).ToString();


                var Sum_Date = Hour + ":" + Minute + " # " + year + "/" + month + "/" + day;

                if (String.Compare(Day_Date.ToLower(), "date", true) == 0)
                    return Sum_Date;

                return String.Compare(Day_Date.ToLower(), "day", true) == 0
                    ? DayShamsi(sdate.DayOfWeek.ToString())
                    : "Error";
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static string Time()
        {
            return DateTime.Now.ToString("hh:mm");
        }

        public static string DayShamsi(string dayMiladi)
        {
            switch (dayMiladi.ToLower())
            {
                case "saturday":
                    return "شنبه";
                case "sunday":
                    return "یک شنبه";
                case "monday":
                    return "دو شنبه";
                case "tuesday":
                    return "سه شنبه";
                case "wednesday":
                    return "چهار شنبه";
                case "thursday":
                    return "پنج شنبه";
                case "friday":
                    return "جمعه";
                default:
                    return "";
            }
        }

        public static string DayShamsi(DayOfWeek dayMiladi)
        {
            switch (dayMiladi)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یک شنبه";
                case DayOfWeek.Monday:
                    return "دو شنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    return "خطا";
            }
        }

        public static string DayShamsiName(DateTime _date)
        {
            switch (_date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یک شنبه";
                case DayOfWeek.Monday:
                    return "دو شنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    return "خطا";
            }
        }

        public static string DayShamsiName(string _date)
        {
            var convertDate = _date.ShamsiToMiladi();
            switch (convertDate.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یک شنبه";
                case DayOfWeek.Monday:
                    return "دو شنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    return "خطا";
            }
        }

        public static string FormatDateTimeLina()
        {
            return "HH:mm - yyyy/MM/dd";
        }

        public static string FormatDateTimePlusTime()
        {
            return "yyyy/MM/dd HH:mm";
        }

        public static string FormatDateTimeMSSQL()
        {
            return "yyyy-MM-dd HH:mm:ss.fff";
        }

        public static string DayShamsiDate(this string Date_Shamsi)
        {
            var Value = DayShamsi(ShamsiToMiladi(Date_Shamsi).DayOfWeek);
            return Value;
        }

        public static List<string> Rtn_Week(DateTime DateTime, bool Check)
        {

            var DateOfWeek = new List<string>();
            var Get_Num_Day = (int)DateTime.DayOfWeek;
            var BetWeen = 7 - Get_Num_Day;

            if (DateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                Get_Num_Day -= 7;
                BetWeen += 7;
            }

            while (Get_Num_Day > -BetWeen)
            {
                DateOfWeek.Add(MiladiToShamsi(DateTime.AddDays(-Get_Num_Day - 1), "Date"));
                DateOfWeek.Add(MiladiToShamsi(DateTime.AddDays(-Get_Num_Day - 1), "Day"));
                Get_Num_Day--;
            }

            return DateOfWeek;
        }

        public static void Rtn_Week(DateTime DateTime, List<Label> Obj, bool Check, double Add_Day)
        {
            int Cnt_Lbl = 0;
            var Get_Num_Day = (int)DateTime.DayOfWeek;
            var BetWeen = 7 - Get_Num_Day;
            DateTime = DateTime.AddDays(Add_Day);
            if (DateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                Get_Num_Day -= 7;
                BetWeen += 7;
            }

            while (Get_Num_Day > -BetWeen)
            {
                Obj[Cnt_Lbl].Text = MiladiToShamsi(DateTime.AddDays(-Get_Num_Day - 1), "Date");
                Cnt_Lbl++;
                Get_Num_Day--;
            }
        }

        public static string Roz(int Roz)
        {
            switch (Roz)
            {
                case 1:
                    return "یکم";
                case 2:
                    return "دوم";
                case 3:
                    return "سوم";
                case 4:
                    return "چهارم";
                case 5:
                    return "پنجم";
                case 6:
                    return "ششم";
                case 7:
                    return "هفتم";
                case 8:
                    return "هشتم";
                case 9:
                    return "نهم";
                case 10:
                    return "دهم";
                case 11:
                    return "یازدهم";
                case 12:
                    return "دوازدهم";
                case 13:
                    return "سیزدهم";
                case 14:
                    return "چهاردهم";
                case 15:
                    return "پانزدهم";
                case 16:
                    return "شانزدهم";
                case 17:
                    return "هفتدهم";
                case 18:
                    return "هجدهم";
                case 19:
                    return "نوزدهم";
                case 20:
                    return "بیستم";
                case 21:
                    return "بیست و یکم";
                case 22:
                    return "بیست و دوم";
                case 23:
                    return "بیست و سوم";
                case 24:
                    return "بیست و چهارم";
                case 25:
                    return "بیست و پنجم";
                case 26:
                    return "بیست و ششم";
                case 27:
                    return "بیست و هفتم";
                case 28:
                    return "بیت و هشتم";
                case 29:
                    return "بیست و نهم";
                case 30:
                    return "سی ام";
                case 31:
                    return "سی و یکم";
            }

            return null;
        }

        public static string Mah(int Month)
        {
            switch (Month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
            }

            return null;
        }

        public static string Sal(int Sal)
        {
            switch (Sal)
            {
                case 96:
                    return "نود و شش";
                case 97:
                    return "نود و هفت";
                case 98:
                    return "نود و هشت";
                case 99:
                    return "نود و نه";


            }

            return null;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        static extern bool Win32SetSystemTime(ref SystemTime sysTime);

        //  [DllImport("kernel32.dll", SetLastError = true)]
        // private static extern bool SetSystemTime(ref SYSTEMTIME st);

        public static void Check_System_Date()
        {
            while (true)
            {

                try
                {
                    // Thread.Sleep(3000);


                    var client = new TcpClient("time.nist.gov", 13);
                    using (var streamReader = new StreamReader(client.GetStream()))
                    {
                        var response = streamReader.ReadToEnd();
                        var utcDateTimeString = response.Substring(7, 17);
                        var localDateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss",
                            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);


                        // جهت استفاده در ویندوز 10

                        /*  var localDateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss",
                           CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).AddMinutes(-271);*/


                        SystemTime st = new SystemTime
                        {
                            wYear = (short)localDateTime.Year,
                            wMonth = (short)localDateTime.Month,
                            wDay = (short)localDateTime.Day,
                            wHour = (short)localDateTime.Hour,
                            wMinute = (short)localDateTime.Minute,
                            wSecond = (short)localDateTime.Second
                        };

                        // Win32SetSystemTime(ref st);
                        // return false;
                    }
                }

                catch
                {
                    // ignored
                }
            }
        }

        public class ModelWeekOfDay
        {
            public int Code { get; set; }

            [Display(Name = "روز هفته")]
            public string Fa { get; set; }
            public string En { get; set; }
        }
        public static List<ModelWeekOfDay> FillDayOfWeek(ModelWeekOfDay addNull)
        {
            List<ModelWeekOfDay> lstWeekOfDays = new List<ModelWeekOfDay>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek))
                         .Cast<DayOfWeek>()
                         .OrderBy(d => (d - DayOfWeek.Saturday + 7) % 7))
            {
                lstWeekOfDays.Add(new ModelWeekOfDay
                {
                    Code = (int)day,
                    En = day.ToString(),
                    Fa = DayShamsi(day.ToString())
                });
            }
            lstWeekOfDays.Add(addNull);
            return lstWeekOfDays;
        }

        public static DateTime DateTimeUS()
        {
            try
            {
                DateTime DT = Convert.ToDateTime(DateTime.Now.ToString(new CultureInfo("en-US")));
                return DT;
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        ///  هفته قبل
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns></returns>
        public static DateTime LastWeekUS(DateTime dayInWeek)
        {
            DayOfWeek firstDay = DayOfWeek.Saturday;
            //  DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;
            DateTime firstDayInWeek = dayInWeek.AddDays(0).Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek.AddDays(-7);
        }

        /// <summary>
        /// تاریخ روز اول هفته
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns></returns>
        public static DateTime OneDayWeekUS(DateTime dayInWeek)
        {
            DayOfWeek firstDay = DayOfWeek.Saturday;
            //   DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;
            DateTime firstDayInWeek = dayInWeek.AddDays(0).Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        /// <summary>
        /// تاریخ روز اول هفته
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns></returns>
        public static string OneDayWeekFA(DateTime dayInWeek)
        {
            DayOfWeek firstDay = DayOfWeek.Saturday;
            //   DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return MiladiToShamsi(firstDayInWeek, "Date");
            // return Miladi_To_Shamsi(firstDayInWeek.AddDays(-1), "Date");
        }

        /// <summary>
        /// تاریخ روز اول ماه
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        ///        
        public static DateTime OneDayMonthUS(string Year, string Month)
        {
            if (Month.Length < 2)
                Month = "0" + Month;
            string _StartDateFA = Year + "/" + Month + "/" + "01";
            var Creat_Date_US = ShamsiToMiladi(_StartDateFA, "Date");

            return Convert.ToDateTime(Creat_Date_US);
        }

        /// <summary>
        /// تاریخ روز اول ماه
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        ///   
        public static DateTime OneDayMonthUS(string date)
        {
            var Year = date.Substring(0, 4);
            var Month = date.Substring(5, 2);

            if (Month.Length < 2)
                Month = "0" + Month;
            string _StartDateFA = Year + "/" + Month + "/" + "01";
            var Creat_Date_US = ShamsiToMiladi(_StartDateFA, "Date");

            return Convert.ToDateTime(Creat_Date_US);
        }

        /// <summary>
        /// تاریخ روز آخر ماه
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        ///   
        public static DateTime EndDayMonthUS(string Year, string Month)
        {
            if (Month.Length < 2)
                Month = "0" + Month;
            var Temp_Day = 32;

            int Temp_Month = Convert.ToInt32(Month);
            var Temp_Year = Per_C.IsLeapYear(Convert.ToInt32(Year));

            if (Temp_Month > 6 && Temp_Month < 12)
                Temp_Day = 30;

            else if (Temp_Month >= 1 && Temp_Month <= 6)
                Temp_Day = 31;

            if (Temp_Year && Temp_Month == 12)
                Temp_Day = 30;
            else if (Temp_Year == false && Temp_Month == 12)
                Temp_Day = 29;

            string _StartDateFA = Year + "/" + Month + "/" + Temp_Day;
            var Creat_Date_US = ShamsiToMiladi(_StartDateFA, "Date");

            return Convert.ToDateTime(Creat_Date_US);
        }

        /// <summary>
        /// تاریخ روز آخر ماه
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        ///   
        public static DateTime EndDayMonthUS(string date)
        {

            var Year = date.Substring(0, 4);
            var Month = date.Substring(5, 2);
            if (Month.Length < 2)
                Month = "0" + Month;
            var Temp_Day = 32;

            int Temp_Month = Convert.ToInt32(Month);
            var Temp_Year = Per_C.IsLeapYear(Convert.ToInt32(Year));

            if (Temp_Month > 6 && Temp_Month < 12)
                Temp_Day = 30;

            else if (Temp_Month >= 1 && Temp_Month <= 6)
                Temp_Day = 31;

            if (Temp_Year && Temp_Month == 12)
                Temp_Day = 30;
            else if (Temp_Year == false && Temp_Month == 12)
                Temp_Day = 29;

            string _StartDateFA = Year + "/" + Month + "/" + Temp_Day;
            var Creat_Date_US = ShamsiToMiladi(_StartDateFA, "Date");

            return Convert.ToDateTime(Creat_Date_US);
        }

        public static int EndDayMonthFA(string date)
        {
            var Year = date.Substring(0, 4);
            var Month = date.Substring(5, 2);
            if (Month.Length < 2)
                Month = "0" + Month;
            var Temp_Day = 32;

            int Temp_Month = Convert.ToInt32(Month);
            var Temp_Year = Per_C.IsLeapYear(Convert.ToInt32(Year));

            if (Temp_Month > 6 && Temp_Month < 12)
                Temp_Day = 30;

            else if (Temp_Month >= 1 && Temp_Month <= 6)
                Temp_Day = 31;

            if (Temp_Year && Temp_Month == 12)
                Temp_Day = 30;
            else if (Temp_Year == false && Temp_Month == 12)
                Temp_Day = 29;

            return Temp_Day;
        }

        public static int Load_Day(int Year, int Month)
        {
            // Cmb_Day.Properties.Items.Clear();

            var Temp_Day = 32;

            var Temp_Month = Month;
            var Temp_Year = Per_C.IsLeapYear(Year);

            if (Temp_Month > 6 && Temp_Month < 12)
                Temp_Day = 31;

            if (Temp_Year && Temp_Month == 12)
                Temp_Day = 31;
            else if
                (Temp_Year == false && Temp_Month == 12)
                Temp_Day = 30;

            return Temp_Day;
        }

        public static int GetAllFriday(string Year, string Month)
        {
            var Get_Start = OneDayMonthUS(Year, Month);
            var Get_End = EndDayMonthUS(Year, Month);
            int Count_Friday = 0;

            for (var Date = Get_Start.Date; Date <= Get_End; Date = Date.AddDays(1))
            {
                if (Date.DayOfWeek == DayOfWeek.Friday)
                {
                    Count_Friday++;
                }
            }

            // MessageBox.Show(Get_Start.DayOfWeek == DayOfWeek.Friday);

            return Count_Friday;
        }

        public static bool GetFriday(string _date) // جمعه
        {
            if (Convert.ToDateTime(ShamsiToMiladi(_date, "Date")).DayOfWeek == DayOfWeek.Friday)
            {
                return true;
            }

            return false;
        }

        public static bool GetThursday(string _date) // پنج شنبه
        {
            if (Convert.ToDateTime(ShamsiToMiladi(_date, "Date")).DayOfWeek == DayOfWeek.Thursday)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// تعداد روز درخواستی در ماه
        /// </summary>
        /// <param name="_dayOfWeek">روز درخواستی</param>
        /// <param name="_date">ماه</param>
        /// <returns></returns>
        public static int CountDayWeek(DayOfWeek _dayOfWeek, string _date)
        {
            int countDay = 0;
            var getYear = _date.Substring(0, 2);
            var getMonth = _date.Substring(5, 2);

            var oneDay = OneDayMonthUS(getYear, getMonth);
            var endDay = EndDayMonthUS(_date);

            for (DateTime date = oneDay; date <= endDay; date.AddDays(1))
            {
                if (date.DayOfWeek == _dayOfWeek)
                    countDay++;
            }

            return countDay;
        }

        /// <summary>
        /// تبدیل دقیقه به زمان
        /// </summary>
        /// <param name="_timeSpan"></param>
        /// <returns></returns>
        public static string SpanToTime(this string _timeSpan)
        {
            #region تبدیل دقیقه به زمان

            // var C = TimeSpan.FromMinutes(Math.Abs(Convert.ToDouble(_timeSpan)));
            var C = TimeSpan.FromMinutes(Math.Abs(Convert.ToDouble(_timeSpan)));
            var H = C.Hours < 10 ? "0" + C.Hours : C.Hours.ToString();
            var M = C.Minutes < 10 ? "0" + C.Minutes : C.Minutes.ToString();

            var result = H + ":" + M;
            return result;

            #endregion
        }
        /// <summary>
        /// تبدیل ثانیه به زمان
        /// </summary>
        /// <param name="_timeSpan"></param>
        /// <returns></returns>
        public static string SpanToTimeSecond(this string _timeSpan)
        {
            #region تبدیل ثانیه به زمان

            // var C = TimeSpan.FromMinutes(Math.Abs(Convert.ToDouble(_timeSpan)));
            string s = _timeSpan;
            double value = Math.Abs(Convert.ToDouble(s));
            var C = TimeSpan.FromSeconds(value);
            var M = C.Minutes < 10 ? "0" + C.Minutes : C.Minutes.ToString();
            var S = C.Seconds < 10 ? "0" + C.Seconds : C.Seconds.ToString();

            var result = M + ":" + S;
            return result;

            #endregion
        }
        /// <summary>
        /// تبدیل زمان به دقیقه
        /// </summary>
        /// <param name="_timeSpan"></param>
        /// <returns></returns>
        public static string TimeToSpan(this string _timeSpan)
        {
            #region تبدیل زمان به دقیقه

            if (Convert.ToInt32(_timeSpan.Replace(":", "")) <= 0)
                return "0";

            string time = _timeSpan;
            double Minutes = TimeSpan.Parse(time).TotalMinutes;

            return Minutes.ToString("####");

            #endregion
        }
    }
}

