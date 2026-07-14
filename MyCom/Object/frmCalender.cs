using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using MyCom.Class;

namespace MyCom.Object
{
    public partial class frmCalender : Form
    {
        private readonly Class_Code CC1 = new Class_Code();
        readonly ClsFont _font = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private readonly DataTable _dtDayShamsi = new DataTable();
        private readonly DataTable _dtDayMiladi = new DataTable();
        private string Temp_Date;
        private string Temp_DateMiladi;
        public string SelDate;
        public DateTime SelDateEnglish;
        private int i, J_Column1;
        private Control _orginalParent;
        private Point _orginallocation;
        private readonly string _setDate = null;

        /// <summary>
        /// GetHoliday(Conection)
        /// </summary>
        public frmCalender(string setDate = null)
        {
            InitializeComponent();
            FunLoad();
            _setDate = setDate;
        }

        public void FunLoad()
        {
            Com_Date.Cmb_Day.SelectedIndexChanged += Com_Date_Day_SelectedIndexChanged;
            Com_Date.Cmb_Month.SelectedIndexChanged += Com_Date_Month_SelectedIndexChanged;
            Com_Date.Cmb_Year.SelectedIndexChanged += Com_Date_Year_SelectedIndexChanged;

            Com_DateMiladi.Cmb_Day.SelectedIndexChanged += Com_DateMiladi_Day_SelectedIndexChanged;
            Com_DateMiladi.Cmb_Month.SelectedIndexChanged += Com_DateMiladi_Month_SelectedIndexChanged;
            Com_DateMiladi.Cmb_Year.SelectedIndexChanged += Com_DateMiladi_Year_SelectedIndexChanged;

            dgvCalenShamsi.DGV_Viw.RowCellStyle += View_Calander2_RowCellStyle;
            dgvCalenShamsi.DGV_Viw.RowCellClick += View_Calander2_RowCellClick;

            dgvCalenMiladi.DGV_Viw.RowCellStyle += dgvCalenMiladi_RowCellStyle;
            dgvCalenMiladi.DGV_Viw.RowCellClick += dgvCalenMiladi_RowCellClick;
        }

        private void Com_Date_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Com_DateMiladi.Cmb_Day.Text = Temp_Date.ShamsiToMiladi().ToString("dd");
            //Com_DateMiladi.Cmb_Month.Text = Temp_Date.ShamsiToMiladi().ToString("MM");
            //Com_DateMiladi.Cmb_Year.Text = Temp_Date.ShamsiToMiladi().ToString("yyyy");
        }

        private void Com_DateMiladi_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Temp_DateMiladi = Com_DateMiladi._varDateUS;
                var getConToShamsi = Convert.ToDateTime(Temp_DateMiladi).MiladiToShamsi("Date").Substring(0, 8) + "01";
                // SetDayShamsi();
                int startDay = Convert.ToInt32(CC1.Shamsi2Miladi(getConToShamsi, "day", false));
                Com_Date.Cmb_Day.Text = Convert.ToDateTime(Temp_DateMiladi).MiladiToShamsi("Date").Substring(8, 2);
                Com_Date.Cmb_Year.Text = getConToShamsi.Substring(0, 4);
                Com_Date.Cmb_Month.Text = getConToShamsi.Substring(5, 2);
                SetDayShamsi(startDay);
            }
            catch (Exception exception)
            {

            }
        }

        private void Com_DateMiladi_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Temp_DateMiladi = Com_DateMiladi._varDateUS;//.Substring(0, 8) + "01";
                var getConToShamsi = Convert.ToDateTime(Temp_DateMiladi).MiladiToShamsi("Date").Substring(0, 8) + "01";
                // SetDayShamsi();
                int startDay = Convert.ToInt32(CC1.Shamsi2Miladi(getConToShamsi, "day", false));
                Com_Date.Cmb_Day.Text = Convert.ToDateTime(Temp_DateMiladi).MiladiToShamsi("Date").Substring(8, 2);
                Com_Date.Cmb_Year.Text = getConToShamsi.Substring(0, 4);
                Com_Date.Cmb_Month.Text = getConToShamsi.Substring(5, 2);
                SetDayShamsi(startDay);
            }
            catch (Exception exception)
            {

            }

        }

        private void Com_DateMiladi_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Temp_DateMiladi = Com_DateMiladi._varDateUS;//.Substring(0, 8) + "01";
                var getConToShamsi = Convert.ToDateTime(Temp_DateMiladi).MiladiToShamsi("Date").Substring(0, 8) + "01";
                // SetDayShamsi();
                int startDay = Convert.ToInt32(CC1.Shamsi2Miladi(getConToShamsi, "day", false));
                Com_Date.Cmb_Day.Text = Convert.ToDateTime(Temp_DateMiladi).MiladiToShamsi("Date").Substring(8, 2);
                Com_Date.Cmb_Year.Text = getConToShamsi.Substring(0, 4);
                Com_Date.Cmb_Month.Text = getConToShamsi.Substring(5, 2);
                SetDayShamsi(startDay);
            }
            catch (Exception exception)
            {

            }



        }

        private void Com_Date_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Temp_Date = Com_Date.Date_IR.Substring(0, 8) + "01";
                SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
                //Com_DateMiladi.Cmb_Day.Text = Temp_Date.ShamsiToMiladi().ToString("dd");
                //Com_DateMiladi.Cmb_Month.Text = Temp_Date.ShamsiToMiladi().ToString("MM");
                //Com_DateMiladi.Cmb_Year.Text = Temp_Date.ShamsiToMiladi().ToString("yyyy");
            }
            catch (Exception exception)
            {

            }
        }

        private void Com_Date_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Temp_Date = Com_Date.Date_IR.Substring(0, 8) + "01";
                int startDay = Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false));
                SetDayShamsi(startDay);
                //Com_DateMiladi.Cmb_Day.Text = Temp_Date.ShamsiToMiladi().ToString("dd");
                //Com_DateMiladi.Cmb_Month.Text = Temp_Date.ShamsiToMiladi().ToString("MM");
                //Com_DateMiladi.Cmb_Year.Text = Temp_Date.ShamsiToMiladi().ToString("yyyy");
            }
            catch (Exception exception)
            {

            }
        }

        private void Com_Date_Year_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Com_Date.Cmb_Year.Text.Length == 4)
                {
                    Temp_Date = Com_Date.Date_IR.Substring(0, 8) + "01";
                    SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
                }
            }
            catch (Exception exception)
            {
                // ignored
            }
        }



        private void dgvCalenMiladi_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string today = DateTime.Now.ToString("yyyy/MM/dd");
            var getDay = Convert.ToInt32(today.Substring(8, 2)).ToString();
            if ((string)e.CellValue != "")
            {
                var getValueDay = (Convert.ToInt16(e.CellValue)) < 10 ? "0" + e.CellValue : e.CellValue.ToString();
                //  var getHoliday = _lstHoliday.Contains(Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/" + getValueDay);

                if (e.CellValue.ToString().Equals(getDay) && Com_DateMiladi._varDateUS == today)
                    e.Appearance.BackColor = Color.FromArgb(181, 116, 255, 136);

                //  else if (getHoliday)
                //    e.Appearance.BackColor = Color.FromArgb(181, 255, 116, 116);

                else
                    e.Appearance.BackColor2 = Color.White;
            }
        }

        private void View_Calander2_RowCellClick(object sender, RowCellClickEventArgs e)
        {

            var o = dgvCalenShamsi.GetValue();
            if (!string.IsNullOrEmpty(o.ToString()))
            {
                var value = Convert.ToInt16(o);
                SelDate = Temp_Date.Substring(0, 8) + (value < 10 ? "0" + value : value.ToString());
                SelDateEnglish = SelDate.ShamsiToMiladi();
                Close();
            }
        }
        private void dgvCalenMiladi_RowCellClick(object sender, RowCellClickEventArgs e)
        {

            var o = dgvCalenMiladi.GetValue();
            if (!string.IsNullOrEmpty(o.ToString()))
            {
                var value = Convert.ToInt16(o);

                SelDateEnglish = Convert.ToDateTime(Com_DateMiladi.Cmb_Year.EditValue + "/" + Com_DateMiladi.Cmb_Month.EditValue + "/" + (value < 10 ? "0" + value : value.ToString()));

                SelDate = SelDateEnglish.MiladiToShamsi("Date");

                Close();
            }
        }
        private void View_Calander2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            // try
            // {
            string today = ClassDateTime.Today("Date");
            string getDay = Convert.ToInt32(ClsDateTime.TodayPersian().Day).ToString();
            int getMonth = Convert.ToInt32(ClsDateTime.TodayPersian().Month);
            int getSelDay = Convert.ToInt32(Com_Date.Date_IR.Substring(8, 2));
            var getSelMonth = Com_Date.Date_IR.Substring(5, 2);
            // string getDay = Convert.ToInt32(today.Substring(8, 2)).ToString();



            if (e.CellValue != null && !string.IsNullOrEmpty(_setDate))
            {
                if ((string)e.CellValue != "")
                {
                    string valueDay = e.CellValue.ToString();
                    var getValueDay = (Convert.ToInt16(e.CellValue)) < 10 ? "0" + e.CellValue : valueDay;
                    var getHoliday =
                        _lstHoliday.Contains(Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/" + getValueDay);

                    if (valueDay.Equals(getDay) && Com_Date.Date_IR == today)
                        e.Appearance.BackColor = Color.FromArgb(181, 116, 255, 136);

                    else if (getHoliday)
                        e.Appearance.BackColor = Color.FromArgb(181, 255, 116, 116);

                    else if (Convert.ToInt32(valueDay) == getSelDay && _setDate.Substring(5, 2) == getSelMonth)
                        e.Appearance.BackColor = Color.FromArgb(181, 191, 193, 40);

                    else
                        e.Appearance.BackColor2 = Color.White;
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(132, 123, 123);
                    e.Appearance.BackColor2 = Color.FromArgb(187, 132, 123, 123);
                }
            }

            // }
            //  catch (Exception exception)
            //  {

            // }
        }

        private void Com_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
                SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private readonly List<string> _lstHoliday = new List<string>();

        //public void ReadHoliday(string _conection)
        //{
        //    GetHoliday(_conection);
        //    // GetHoliday(@"Data Source=192.168.4.100\nlina;Initial Catalog=PPERS_3_1398;User ID=sa;Password=Pars@63");
        //}

        private async void frmCalender_Load(object sender, EventArgs e)
        {


            int i2;
            SetWeekDayTODgv();

            for (i2 = 0; i2 < 6; i2++)
            {
                _dtDayShamsi.Rows.Add();
                _dtDayMiladi.Rows.Add();
            }
            // Set_View_Day(0);

            _font.ChangeFont(btnToday);
            _font.ChangeFont(Com_Date);
            _font.ChangeFont(Com_DateMiladi);
            _font.ChangeFont(lblMonth);
            _font.ChangeFont(lblYear);
            _font.ChangeFont(Com_DateMiladi);
            _font.ChangeFont(lblBetween);

            //await dgvPriceList.InvokeThread(async () =>
           await dgvCalenShamsi.InvokeThread(async  () =>  
            {
                _font.ChangeFont(dgvCalenShamsi, 12);
                _font.ChangeFont(dgvCalenShamsi.DGV_Viw, 13);
            }, false);


            await dgvCalenMiladi.InvokeThread(async () =>
            {
                _font.ChangeFont(dgvCalenMiladi, 12);
                _font.ChangeFont(dgvCalenMiladi.DGV_Viw, 13);
            }, false);

            dgvCalenShamsi.LookAndFeel.SkinName = "WXI Compact";
            dgvCalenMiladi.LookAndFeel.SkinName = "WXI Compact";
            //dgvCalenShamsi.LookAndFeel.SkinName = "Office 2016 Colorful";
            //dgvCalenMiladi.LookAndFeel.SkinName = "Office 2016 Colorful";

            //dgvCalenShamsi.DGV_Viw.OptionsView.ColumnAutoWidth = true;
            //dgvCalenMiladi.DGV_Viw.OptionsView.ColumnAutoWidth = true;

            Com_Date.Height = 30;

            Set_Today();

            if (_setDate != null)
            {
                Com_Date.Set_Date(_setDate);
                Temp_Date = _setDate;
                // Temp_DateMiladi = _setDate.ShamsiToMiladi().ToString("yyyy/MM/dd");
            }

            Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
            Temp_DateMiladi = Temp_Date.ShamsiToMiladi().ToString("yyyy/MM/dd");
            int startDay = Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false));
            SetDayShamsi(startDay);
            var getBetWeen1 = ClsDateTime.ModelDateTimePersianFunction.BetweenDateTime(DateTime.Now, SelDateEnglish);
            var getBetWeen2 = Convert.ToDecimal(getBetWeen1.Day);
            lblBetween.Visible = true;
            if (getBetWeen2 == 0)
                lblBetween.Visible = false;
            else if (getBetWeen2 > 0)
                lblBetween.Text = getBetWeen2 + @" روز بعد";
            else if (getBetWeen2 < 0)
            {
                lblBetween.Text = Math.Abs(getBetWeen2) + @" روز قبل";
            }

        }

        public void SetLblMonthYear()
        {
            var getYear = Com_Date.Cmb_Year.EditValue;
            var getMonth = ClassDateTime.Mah(Convert.ToInt32(Com_Date.Cmb_Month.EditValue));

            lblYear.Text = getYear.ToString();
            lblMonth.Text = getMonth;
        }
        private void Btn_Next_Month_Click(object sender, EventArgs e)
        {
            //if (Com_DateMiladi.Cmb_Month.SelectedIndex < Com_DateMiladi.Cmb_Month.Properties.Items.Count - 1)
            //{
            //    Com_DateMiladi.Cmb_Month.SelectedIndex = Com_DateMiladi.Cmb_Month.SelectedIndex + 1;
            //}
            //else
            //{
            //    Com_DateMiladi.Cmb_Month.SelectedIndex = 0;
            //    if (Com_DateMiladi.Cmb_Year.SelectedIndex < Com_DateMiladi.Cmb_Year.Properties.Items.Count - 1)
            //    {
            //        Com_DateMiladi.Cmb_Year.SelectedIndex = Com_DateMiladi.Cmb_Year.SelectedIndex + 1;
            //    }
            //    else
            //        Com_DateMiladi.Cmb_Year.SelectedIndex = 0;
            //    // Btn_Next_Year_Click(sender, e);
            //}

            if (Com_Date.Cmb_Month.SelectedIndex < Com_Date.Cmb_Month.Properties.Items.Count - 1)
            {
                Com_Date.Cmb_Month.SelectedIndex = Com_Date.Cmb_Month.SelectedIndex + 1;
            }
            else
            {
                Com_Date.Cmb_Month.SelectedIndex = 0;
                if (Com_Date.Cmb_Year.SelectedIndex < Com_Date.Cmb_Year.Properties.Items.Count - 1)
                {
                    Com_Date.Cmb_Year.SelectedIndex = Com_Date.Cmb_Year.SelectedIndex + 1;
                }
                else
                    Com_Date.Cmb_Year.SelectedIndex = 0;
                // Btn_Next_Year_Click(sender, e);
            }
            Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
            SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));

        }

        private void Btn_Next_Year_Click(object sender, EventArgs e)
        {
            //if (Com_DateMiladi.Cmb_Year.SelectedIndex < Com_DateMiladi.Cmb_Year.Properties.Items.Count - 1)
            //{
            //    Com_DateMiladi.Cmb_Year.SelectedIndex = Com_DateMiladi.Cmb_Year.SelectedIndex + 1;
            //}
            //else
            //    Com_DateMiladi.Cmb_Year.SelectedIndex = 0;

            if (Com_Date.Cmb_Year.SelectedIndex < Com_Date.Cmb_Year.Properties.Items.Count - 1)
            {
                Com_Date.Cmb_Year.SelectedIndex = Com_Date.Cmb_Year.SelectedIndex + 1;
            }
            else
                Com_Date.Cmb_Year.SelectedIndex = 0;
            Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
            SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
        }

        private void Btn_Back_Year_Click(object sender, EventArgs e)
        {
            //if (Com_DateMiladi.Cmb_Year.SelectedIndex > 0)
            //{
            //    Com_DateMiladi.Cmb_Year.SelectedIndex = Com_DateMiladi.Cmb_Year.SelectedIndex - 1;
            //}
            //else
            //{
            //    Com_DateMiladi.Cmb_Year.SelectedIndex = Com_DateMiladi.Cmb_Year.Properties.Items.Count - 1;
            //}

            if (Com_Date.Cmb_Year.SelectedIndex > 0)
            {
                Com_Date.Cmb_Year.SelectedIndex = Com_Date.Cmb_Year.SelectedIndex - 1;
            }
            else
            {
                Com_Date.Cmb_Year.SelectedIndex = Com_Date.Cmb_Year.Properties.Items.Count - 1;
            }

            Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
            SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
        }

        private void Btn_Back_Month_Click(object sender, EventArgs e)
        {
            //if (Com_DateMiladi.Cmb_Month.SelectedIndex > 0)
            //    Com_DateMiladi.Cmb_Month.SelectedIndex = Com_DateMiladi.Cmb_Month.SelectedIndex - 1;
            //else
            //{
            //    Com_DateMiladi.Cmb_Month.SelectedIndex = Com_DateMiladi.Cmb_Month.Properties.Items.Count - 1;


            //    if (Com_DateMiladi.Cmb_Year.SelectedIndex > 0)
            //    {
            //        Com_DateMiladi.Cmb_Year.SelectedIndex = Com_DateMiladi.Cmb_Year.SelectedIndex - 1;
            //    }
            //    else
            //    {
            //        Com_DateMiladi.Cmb_Year.SelectedIndex = Com_DateMiladi.Cmb_Year.Properties.Items.Count - 1;
            //    }

            //    // Btn_Back_Year_Click(sender, e);
            //}

            if (Com_Date.Cmb_Month.SelectedIndex > 0)
                Com_Date.Cmb_Month.SelectedIndex = Com_Date.Cmb_Month.SelectedIndex - 1;
            else
            {
                Com_Date.Cmb_Month.SelectedIndex = Com_Date.Cmb_Month.Properties.Items.Count - 1;

                if (Com_Date.Cmb_Year.SelectedIndex > 0)
                {
                    Com_Date.Cmb_Year.SelectedIndex = Com_Date.Cmb_Year.SelectedIndex - 1;
                }
                else
                {
                    Com_Date.Cmb_Year.SelectedIndex = Com_Date.Cmb_Year.Properties.Items.Count - 1;
                }

                // Btn_Back_Year_Click(sender, e);
            }
            Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
            SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {

        }

        private void btnToday_Click(object sender, EventArgs e)
        {

            Set_Today();
            Temp_Date = Com_Date.Cmb_Year.Text + "/" + Com_Date.Cmb_Month.Text + "/01";
            SetDayShamsi(Convert.ToInt32(CC1.Shamsi2Miladi(Temp_Date, "day", false)));
            //Close();
        }
        public void Set_Today()
        {
            // Description convert datetime to miladi and close temp get english 
            // ToDo: Copy update to int 32
            Com_Date.Set_Today();
            SelDate = Com_Date.Date_IR;
            SelDateEnglish = Com_Date.Date_US();

            Com_DateMiladi.Set_Today();
            // SelDate = Com_Date.Date_IR;
            //  SelDateEnglish = Com_Date.Date_US();
        }

        private void Com_Date_Load(object sender, EventArgs e)
        {

        }

        private void SetDayShamsi(int _startDay)
        {
            Com_Date.Load_Day();
            Com_DateMiladi.Load_Day();
            //   Lbl_Mounth.Text = CC1.Get_Name_Mounth(Convert.ToInt32(Com_Date.Cmb_Month.Text));
            CC1.Get_Name_Mounth(Convert.ToInt32(Com_Date.Cmb_Month.Text));
            // Lbl_Year.Text = Com_Date.Cmb_Year.Text;

            for (int _row = 0; _row < 6; _row++)
                for (int _column = 0; _column < 7; _column++)
                {
                    _dtDayShamsi.Rows[_row][_column] = ""; // شمسی
                    _dtDayMiladi.Rows[_row][_column] = "";
                }

            int _cntDay = 1;

            for (int _column = _startDay; _column < 7; _column++)
            {
                _dtDayShamsi.Rows[0][_column] = _cntDay; // شمسی
                _dtDayMiladi.Rows[0][_column] = _cntDay * 2;

                _cntDay++;
            }
            for (int _row = 1; _row < 6; _row++)
            {
                int _column = 0;
                while (_column < 7 && Com_Date.Cmb_Day.Properties.Items.Count >= _cntDay)
                {
                    _dtDayShamsi.Rows[_row][_column] = _cntDay; // شمسی
                    _dtDayMiladi.Rows[_row][_column] = _cntDay * 2;

                    _column++;
                    _cntDay++;
                }
            }

            var _dateConToMilad = ClassDateTime.ShamsiToMiladi(
                Com_Date.Cmb_Year.Text + "/" +
                Com_Date.Cmb_Month.Text + "/" +
                Com_Date.Cmb_Day.Text);
            //  Com_Date.Cmb_Day.Text);


            _dateConToMilad = _dateConToMilad.AddDays(-(_dateConToMilad.Day - 1));
            var _dayOfw = 0;
            switch (_dateConToMilad.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    _dayOfw = 0;
                    break;

                case DayOfWeek.Sunday:
                    _dayOfw = 1;
                    break;

                case DayOfWeek.Monday:
                    _dayOfw = 2;
                    break;

                case DayOfWeek.Tuesday:
                    _dayOfw = 3;
                    break;

                case DayOfWeek.Wednesday:
                    _dayOfw = 4;
                    break;

                case DayOfWeek.Thursday:
                    _dayOfw = 5;
                    break;

                case DayOfWeek.Friday:
                    _dayOfw = 6;
                    break;

            }
            //  var _dayOfw = _dateConToMilad.DayOfWeek == DayOfWeek.Tuesday
            // var gaeDayUS= ClassDateTime.OneDayWeekUS(_dateConToMilad);
            SetDayMiladi(_dayOfw);

            SelDate = Com_Date.Date_IR;
            SelDateEnglish = Com_Date.Date_US();
            SetLblMonthYear();


        }
        private void SetDayMiladi(int _startDay)
        {
            //  Com_Date.Load_Day();
            //  Lbl_Mounth.Text = CC1.Get_Name_Mounth(Convert.ToInt32(Com_Date.Cmb_Month.Text));
            //  CC1.Get_Name_Mounth(Convert.ToInt32(Com_Date.Cmb_Month.Text));
            //  Lbl_Year.Text = Com_Date.Cmb_Year.Text;

            for (int _row = 0; _row < 6; _row++)
                for (int _column = 0; _column < 7; _column++)
                {
                    _dtDayMiladi.Rows[_row][_column] = ""; // میلادی
                }

            int _cntDay = 1;

            for (int _column = _startDay; _column < 7; _column++)
            {
                _dtDayMiladi.Rows[0][_column] = _cntDay; // میلادی

                _cntDay++;
            }

            for (int _row = 1; _row < 6; _row++)
            {
                int _column = 0;
                while (_column < 7 && Com_Date.Cmb_Day.Properties.Items.Count >= _cntDay)
                {
                    _dtDayMiladi.Rows[_row][_column] = _cntDay; // میلادی

                    _column++;
                    _cntDay++;
                }
            }

            // SelDate = Com_Date.Date_IR;
            //SelDateEnglish = Com_Date.Date_US();
        }
        public bool Today_Color(string C_Day, string C_Month, string C_Year)
        {
            string Temp_Date;
            if (Convert.ToInt32(C_Day) < 10)
                Temp_Date = C_Year + "/" + C_Month + "/0" + C_Day;
            else
                Temp_Date = C_Year + "/" + C_Month + "/" + C_Day;

            return Temp_Date == CC1.Today("Date");
        }


        private void SetWeekDayTODgv()
        {
            dgvCalenShamsi.GridStructure(dgvCalenShamsi, _dtDayShamsi, new List<string>
                {
                    "شنبه",
                    "1 شنبه",
                    "2 شنبه",
                    "3 شنبه",
                    "4 شنبه",
                    "5 شنبه",
                    "جمعه",
                }, null, false, false, true,
                new Color(), null, false);

            dgvCalenShamsi.DGV_Viw.OptionsCustomization.AllowFilter = false;

            dgvCalenShamsi.DGV_Viw.OptionsCustomization.AllowColumnResizing = false;


            dgvCalenMiladi.GridStructure(dgvCalenMiladi, _dtDayMiladi, new List<string>
                {
                    "Sa",//"شنبه",
                    "Su", // "1 شنبه",
                    "Mo",// "2 شنبه",
                    "Tu",//"3 شنبه",
                    "We",//"4 شنبه",
                    "Th",//"5 شنبه",
                    "Fr",//"جمعه",
                }, null, false, false, true,
                new Color(), null, false);

            dgvCalenMiladi.DGV_Viw.OptionsCustomization.AllowFilter = false;

            dgvCalenMiladi.DGV_Viw.OptionsCustomization.AllowColumnResizing = false;
            dgvCalenMiladi.DGV.RightToLeft = RightToLeft.No;

        }


        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
