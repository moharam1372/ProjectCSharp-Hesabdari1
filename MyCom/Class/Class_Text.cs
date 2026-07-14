using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Views.Grid;
using MyCom.Object;

namespace MyCom.Class
{

    public class Class_Text
    {
        public KeyPressEventArgs Check_Date(KeyPressEventArgs e, TextBox Temp)
        {
            var Temp_Length = Temp.SelectionLength;
            var Temp_Start = Temp.SelectionStart;
            var Number_Back = 0;
            for (var i = 0; i < Temp.TextLength; i++)
            {
                Temp.SelectionStart = i;
                Temp.SelectionLength = 1;

                if (Temp.SelectedText == "/")
                    Number_Back++;
            }

            Temp.SelectionStart = Temp_Start;
            Temp.SelectionLength = Temp_Length;

            if (Convert.ToInt16(e.KeyChar) == 47 && Number_Back < 2)
                e.KeyChar = Convert.ToChar(47);

            else if (Convert.ToInt16(e.KeyChar) == 8)
                e.KeyChar = Convert.ToChar(8);

            else if (Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57)
                e.KeyChar = Convert.ToChar(0);

            // ///
            return (e);
        }
        public KeyPressEventArgs Check_Number(KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 8)
                e.KeyChar = Convert.ToChar(8);

            else if (Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57)
                e.KeyChar = Convert.ToChar(0);
            return (e);
        }
        public KeyPressEventArgs Check_Horoof(KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 1610) //Convert To 1740  // بررسی حرف 'ی' ، جهت ذخیره در بانک  
                e.KeyChar = Convert.ToChar(1740); // تبدیل حرف 'ی' جهت شناسایی در برنامه
            return (e);
        }
        public void FindGrid(GridView _dataGrid)
        {
            FindControl find = _dataGrid.GridControl.Controls.Find("FindControl", true)[0] as FindControl;

            // find.RightToLeft=RightToLeft.No;

            find.ClearButton.Text = @"پاک کردن";
            find.ClearButton.Font = new Font("B Nazanin", 13.75f);
            find.FindEdit.Font = new Font("B Nazanin", 13.75f);
            find.Height = find.ClearButton.Height + 30;
            // find.FindEdit.Left = 600;
            // find.Controls.Add(new SimpleButton { Name = "Mojtaba", Text = "تست", Left = 600 });
            _dataGrid.BeginUpdate();
            _dataGrid.RefreshData();
            _dataGrid.TopRowIndex = 0;
            _dataGrid.EndUpdate();

            // find.FindEdit.Focus();
        }
        public void Save_Setting_Width(ListBox Temp)
        {
            var Address1 = Application.StartupPath + @"\Setting.txt";
            try
            {

                var SW = new StreamWriter(Address1, false);
                foreach (var t in Temp.Items)
                {
                    SW.WriteLine(t);
                }
                SW.Close();
            }
            catch (Exception)
            {

                MessageBox.Show(@"خطا");
            }

        }
        public void Save_Setting_Visible(ListBox Temp)
        {
            var Address2 = Application.StartupPath + @"\Setting_Show.txt";
            try
            {
                var SW = new StreamWriter(Address2, false);
                foreach (var t in Temp.Items)
                {
                    SW.WriteLine(t);
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show(@"خطا");
            }
        }
        public static void Set_Location(Control Frm, string Location)
        {
            switch (Location)
            {
                case "DR": // پایین - راست
                    Frm.Left = SystemInformation.PrimaryMonitorSize.Width - Frm.Width - 2;
                    Frm.Top = SystemInformation.PrimaryMonitorSize.Height - Frm.Height - 42;
                    break;
                case "CC": // Center Screen
                    Frm.Left = (SystemInformation.PrimaryMonitorSize.Width - Frm.Width) / 2;
                    Frm.Top = (SystemInformation.PrimaryMonitorSize.Height - Frm.Height - 42) / 2;
                    break;
                case "CR": // وسط - راست
                    Frm.Left = SystemInformation.PrimaryMonitorSize.Width - Frm.Width - 2;
                    Frm.Top = (SystemInformation.PrimaryMonitorSize.Height - Frm.Height - 42) / 2;
                    break;
                case "DC": // پایین - وسط
                    Frm.Left = (SystemInformation.PrimaryMonitorSize.Width - Frm.Width) / 2;
                    Frm.Top = SystemInformation.PrimaryMonitorSize.Height - Frm.Height - 42;
                    break;
                case "DL": // پایین - چپ
                    Frm.Left = 2;
                    Frm.Top = SystemInformation.PrimaryMonitorSize.Height - Frm.Height - 42;
                    break;
                case "UR": // بالا - راست
                    Frm.Top = 3;
                    Frm.Left = SystemInformation.PrimaryMonitorSize.Width - Frm.Width;
                    break;
                case "UC": // بالا - وسط
                    Frm.Top = 3;
                    Frm.Left = (SystemInformation.PrimaryMonitorSize.Width - Frm.Width) / 2;
                    break;
                case "CL": // وسط - چپ
                    Frm.Left = 2;
                    Frm.Top = (SystemInformation.PrimaryMonitorSize.Height - Frm.Height - 42) / 2;
                    break;
                case "UL": // بالا - راست
                    Frm.Top = 3;
                    Frm.Left = 2;
                    break;
            }
        }
        public bool Frm_Msg(string Message, string Title, int Icon, bool OK_Yes = true, Color Color_Text = default(Color), RightToLeft left = RightToLeft.Yes,string btnOk="Ok",string btnCancel="Cancel")
        {
            Frm_MSG Frm_MSG = new Frm_MSG(Message, Title, Icon, OK_Yes, Color_Text, btnOk, btnCancel);
            Frm_MSG.Lbl_Msg.RightToLeft = left;
            Frm_MSG.ShowDialog();
            return Frm_MSG.Check_Msg;
        }
        public void Frm_Msg(string Message, string Title, int Icon)
        {
            Frm_MSG Frm_MSG = new Frm_MSG(Message, Title, Icon);
            Frm_MSG.ShowDialog();
        }
        public static string Msg_Name => "کافه کلوپ کاوش";
        #region ترکیب اعداد ، حروف انگلیسی و فارسی

        /// <summary>
        /// ترکیب اعداد ، حروف انگلیسی و فارسی
        /// </summary>
        /// <param name="e"></param>
        public char ConvertNumberToEnglishAndPersian(char e)
        {
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            Decoder utf8Decoder = utf8Encoder.GetDecoder();

            char[] convertedChar = new char[1];
            byte[] bytes = { 217, 160 };

            if (char.IsDigit(e))
            {
                bytes[1] = Convert.ToByte(160 + char.GetNumericValue(e));
                utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);
                e = convertedChar[0];
            }
            return e;
        }

        /// <summary>
        /// ترکیب اعداد ، حروف انگلیسی و فارسی
        /// </summary>
        /// <param name="e"></param>
        public void ConvertNumberToEnglishAndPersian(KeyPressEventArgs e)
        {
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            Decoder utf8Decoder = utf8Encoder.GetDecoder();

            char[] convertedChar = new char[1];
            byte[] bytes = { 217, 160 };

            if (char.IsDigit(e.KeyChar))
            {
                bytes[1] = Convert.ToByte(160 + char.GetNumericValue(e.KeyChar));
                utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);
                e.KeyChar = convertedChar[0];
            }
        }

        /// <summary>
        /// ترکیب اعداد، حروف انگلیسی و حروف فارسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ConvertNumberToEnglishAndPersian(string input)
        {
            UTF8Encoding utf8Encoder = new UTF8Encoding();
            Decoder utf8Decoder = utf8Encoder.GetDecoder();
            StringBuilder convertedChars = new StringBuilder();
            char[] convertedChar = new char[1];
            byte[] bytes = { 217, 160 };
            char[] inputCharArray = input.ToCharArray();
            foreach (char c in inputCharArray)
            {
                if (char.IsDigit(c))
                {
                    bytes[1] = Convert.ToByte(160 + char.GetNumericValue(c));
                    utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);
                    convertedChars.Append(convertedChar[0]);
                }
                else
                {
                    convertedChars.Append(c);
                }
            }

            return convertedChars.ToString();
        }

        #endregion
    }
}
