using System;
using System.Globalization;
using System.Windows.Forms;
using MyCom.Class;

namespace MyCom.Object
{
    public partial class Date_SamimMiladi : UserControl
    {
        private readonly PersianCalendar Per_C = new PersianCalendar();
        private readonly Class_Text CT1 = new Class_Text();
        private readonly Class_Code CC1 = new Class_Code();

        public string _varDateUS;
        public bool Not_Run_Form_Load = false;
        //  public DateTime Date_US;
        public Date_SamimMiladi()
        {
            InitializeComponent();
        }

        public void Set_Bind(string Year, string Month, string Day)
        {
            Cmb_Year.Text = Year;
            Cmb_Month.Text = Month;
            Cmb_Day.Text = Day;
        }

        public void Set_Date(string GDate)
        {
            try
            {
                if (GDate.Length == 10)
                {
                    Cmb_Day.Text = GDate.Substring(8, 2);
                    Cmb_Month.Text = GDate.Substring(5, 2);
                    Cmb_Year.Text = GDate.Substring(0, 4);
                }
                else
                    Set_Today();
            }
            catch
            {
                //Cmb_Day.Text = @"01";
                //Cmb_Month.Text = @"01";
                //Cmb_Year.Text = @"1390";
                Set_Today();
            }
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
        }
        //public void Set_Date(DateTime GDate)
        //{
        //    try
        //    {
        //        Cmb_Day.Text = GDate.Substring(8, 2);
        //        Cmb_Month.Text = GDate.Substring(5, 2);
        //        Cmb_Year.Text = GDate.Substring(0, 4);
        //    }
        //    catch
        //    {
        //        Cmb_Day.Text = @"01";
        //        Cmb_Month.Text = @"01";
        //        Cmb_Year.Text = @"1390";
        //    }
        //    Date_IR = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
        //    Fun_DateUS();

        //}
        public void Set_Today()
        {
            Cmb_Day.Text = CC1.T_USDay();
            Cmb_Month.Text = CC1.T_USMonth();
            Cmb_Year.Text = CC1.T_USYear();
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
            //Fun_DateUS();
        }

        public void Date_Refresh()
        {
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
            //Fun_DateUS();
        }

        private void DateTavalod_Load(object sender, EventArgs e)
        {
            if (!Not_Run_Form_Load)
                _varDateUS = "1990/01/01";

            Height = Cmb_Year.Height;
            Width = Cmb_Year.Width + Cmb_Month.Width + Cmb_Day.Width;

            for (var i = 1990; i < 2080; i++)
                Cmb_Year.Properties.Items.Add(i.ToString());

            for (var i = 1; i < 13; i++)
                if (i < 10)
                    Cmb_Month.Properties.Items.Add("0" + i);
                else
                    Cmb_Month.Properties.Items.Add(i.ToString());
            if (!Not_Run_Form_Load)
            {
                Cmb_Month.Text = CC1.T_USMonth();
                Cmb_Year.Text = CC1.T_USYear();
                Load_Day();
                Cmb_Day.Text = CC1.T_USDay();
            }
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;

            //Fun_DateUS();

        }

        private void DateTavalod_Resize(object sender, EventArgs e)
        {
            Height = Cmb_Year.Height;
            Width = Cmb_Year.Width + Cmb_Month.Width + Cmb_Day.Width;
        }

        private int Count_Click_Day = 0;

        private void Cmb_Day_TextChanged(object sender, EventArgs e)
        {

            if (Cmb_Day.Text.Length == 2 && Count_Click_Day > 1)
            {
                Cmb_Month.Select();
                Count_Click_Day = 0;
            }

            SeT_USDay_Cmb_Day();

        }

        public void SeT_USDay_Cmb_Day()
        {
            try
            {
                Count_Click_Day++;
                var A1 = Convert.ToInt32(Cmb_Day.Text);
                var A2 = Convert.ToInt32(Cmb_Day.Properties.Items[Cmb_Day.Properties.Items.Count - 1]);
                if (Cmb_Day.Text != "" && A1 > A2)
                    Cmb_Day.SelectedIndex = Cmb_Day.Properties.Items.Count - 1;
            }
            catch
            {

            }
            // Date1 = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
        }

        private int Count_Click_Month = 0;

        private void Cmb_Month_TextChanged(object sender, EventArgs e)
        {

            if (Cmb_Month.Text.Length == 2 && Count_Click_Month > 1)
            {
                Cmb_Year.Select();
                Count_Click_Month = 0;
            }

            Count_Click_Month++;
            if (Cmb_Month.Text != "" && Convert.ToInt32(Cmb_Month.Text) > 12)
                Cmb_Month.Text = @"12";

            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
            //Fun_DateUS();
        }

        public void Load_Day()
        {
            Cmb_Day.Properties.Items.Clear();

            var Temp_Day = 32;

            var Temp_Month = Convert.ToInt32(Cmb_Month.Text);
            var Temp_Year = Per_C.IsLeapYear(Convert.ToInt32(Cmb_Year.Text));

            if (Temp_Month > 6 && Temp_Month < 12)
                Temp_Day = 31;

            if (Temp_Year && Temp_Month == 12)
                Temp_Day = 31;
            else if
                (Temp_Year == false && Temp_Month == 12)
                Temp_Day = 30;

            for (var i = 1; i < Temp_Day; i++)
                if (i < 10)
                    Cmb_Day.Properties.Items.Add("0" + i);
                else
                    Cmb_Day.Properties.Items.Add(i.ToString());
        }

        private void Cmb_Year_TextChanged(object sender, EventArgs e)
        {
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
            //Fun_DateUS();
        }

        public void Cmb_Day_Leave(object sender, EventArgs e)
        {
            if (Cmb_Day.Text == "")
                Cmb_Day.Text = CC1.T_USDay();

            if (Cmb_Day.Text == @"00")
                Cmb_Day.Text = CC1.T_USDay();

            if (Convert.ToInt32(Cmb_Day.Text) < 10 && Cmb_Day.Text.Length < 2)
                Cmb_Day.Text = @"0" + Cmb_Day.Text;

            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
            ////Fun_DateUS();
        }

        private void Cmb_Month_Leave(object sender, EventArgs e)
        {
            if (Cmb_Month.Text == "")
                Cmb_Month.Text = CC1.T_USMonth();

            if (Cmb_Month.Text == @"00")
                Cmb_Month.Text = CC1.T_USMonth();

            if (Convert.ToInt32(Cmb_Month.Text) < 10 && Cmb_Month.Text.Length < 2)
                Cmb_Month.Text = @"0" + Cmb_Month.Text;

            Load_Day();
            Count_Click_Day = 0;
            Cmb_Day_TextChanged(sender, e);

            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
        }

        private void Cmb_Day_KeyPress(object sender, KeyPressEventArgs e)
        {
            CT1.Check_Number(e);
        }

        private void Cmb_Year_KeyPress(object sender, KeyPressEventArgs e)
        {
            CT1.Check_Number(e);
        }

        private void Cmb_Year_Leave(object sender, EventArgs e)
        {
            if (Cmb_Year.Text == "")
                Cmb_Year.Text = CC1.T_USYear();

            if (Convert.ToInt32(Cmb_Year.Text) < 1990)
                Cmb_Year.Text = CC1.T_USYear();

            if (Convert.ToInt32(Cmb_Year.Text) > 2080)
                Cmb_Year.Text = CC1.T_USYear();

            Load_Day();
            Count_Click_Day = 0;
            Cmb_Day_TextChanged(sender, e);
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;

            //Fun_DateUS();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _varDateUS = Cmb_Year.Text + "/" + Cmb_Month.Text + "/" + Cmb_Day.Text;
            //Date_US = Shamsi_To_Miladi_Plus_Time(Date_IR );
            // Refresh();
            //  Height = 36;
        }

        private void Cmb_Day_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_Month_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private DateTime Shamsi_To_Miladi_Plus_Time(string sdate)
        {
            var p = new PersianCalendar();

            int year = Convert.ToInt32(sdate.Substring(0, 4));
            int month = Convert.ToInt32(sdate.Substring(5, 2));
            int day = Convert.ToInt32(sdate.Substring(8, 2));

            //  int Minute = p.GetMinute(DateTime.Now);
            // int Hour = p.GetHour(DateTime.Now);

            //if (Convert.ToInt32(month) < 10)
            //    month = "0" + month;

            //if (Convert.ToInt32(day) < 10)
            //    day = "0" + day;


            var D = DateTime.Now;
            DateTime mdate = p.ToDateTime(year, month, day, D.Hour, D.Minute, D.Second, 0);
            // MessageBox.Show(sdate.Substring(13, 2));
            // MessageBox.Show(sdate.Substring(16, 2));
            // sdfsdfsdf
            // DateTime mdate = Convert.ToDateTime(Convert.ToDateTime(new DateTime(year,month, day, p).ToString(CultureInfo.InvariantCulture)).ToString("yyyy/MM/dd"));
            return mdate;
        }
        private DateTime Shamsi_To_Miladi_No_Time(string sdate)
        {
            var p = new PersianCalendar();

            int year = Convert.ToInt32(sdate.Substring(0, 4));
            int month = Convert.ToInt32(sdate.Substring(5, 2));
            int day = Convert.ToInt32(sdate.Substring(8, 2));

            //  int Minute = p.GetMinute(DateTime.Now);
            // int Hour = p.GetHour(DateTime.Now);

            //if (Convert.ToInt32(month) < 10)
            //    month = "0" + month;

            //if (Convert.ToInt32(day) < 10)
            //    day = "0" + day;

            var D = DateTime.Now;
            DateTime mdate = p.ToDateTime(year, month, day, 0, 0, 0, 0);
            // MessageBox.Show(sdate.Substring(13, 2));
            // MessageBox.Show(sdate.Substring(16, 2));
            // sdfsdfsdf
            // DateTime mdate = Convert.ToDateTime(Convert.ToDateTime(new DateTime(year,month, day, p).ToString(CultureInfo.InvariantCulture)).ToString("yyyy/MM/dd"));
            return mdate;
        }



        public DateTime Date_US()
        {
            return Shamsi_To_Miladi_Plus_Time(_varDateUS);
        }
        public DateTime Date_US_NoTime()
        {
            return Shamsi_To_Miladi_No_Time(_varDateUS);
        }

        private void Cmb_Day_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Cmb_Day.Text.Length == 2)
            //{
            //    Cmb_Month.Select();
            //    Count_Click_Day = 0;
            //}

            //SeT_USDay_Cmb_Day();
        }

        private void Date_Leave(object sender, EventArgs e)
        {
            Cmb_Day_Leave(sender, e);
            Cmb_Month_Leave(sender, e);
        }

        private void Cmb_Month_KeyPress(object sender, KeyPressEventArgs e)
        {
            CT1.Check_Number(e);
        }
    }
}
