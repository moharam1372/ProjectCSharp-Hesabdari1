using System;
using System.Drawing;
using System.Windows.Forms;
using MyCom.Object;

namespace MyCom.Class
{
    public static class ClassMessageBox
    {
        //public static string Msg_Name => "Lina Industrial Group";
        public static bool Frm_Msg(string Message, string Title, int Icon, bool OK_Yes = true, Color Color_Text = default, RightToLeft rightToLeft = RightToLeft.Yes, string btnOk = "Ok", string btnCancel = "Cancel")
        {
            Frm_MSG Frm_MSG = new Frm_MSG(Message, Title, Icon, OK_Yes, Color_Text, btnOk, btnCancel) { Lbl_Msg = { RightToLeft = rightToLeft } };
            Frm_MSG.ShowDialog();
            return Frm_MSG.Check_Msg;
        }

        public static void ShowAccessDenied(bool english)
        {
            Frm_Msg("خطای دسترسی", Class_Text.Msg_Name, 6, false, Color.Red);
        }
        public static void ShowMSG(string Message, string Title, enumIcon Icon, Color color = default)
        {
            var getIco = Convert.ToInt32(Icon);

            Frm_MSG Frm_MSG = new Frm_MSG(Message, Title, getIco,false, color);
            Frm_MSG.ShowDialog();

        }
        public static bool ShowMSGQues(string Message, string Title, enumIcon Icon, Color color = default)
        {
            var getIco = Convert.ToInt32(Icon);

            Frm_MSG Frm_MSG = new Frm_MSG(Message, Title, getIco, true, color);
            Frm_MSG.ShowDialog();
            return Frm_MSG.Check_Msg;
        }
        public static void ShowAlert(string Message, enumIcon Icon, string location = "DR", int Time = 5, bool Show_Btnhide = true)
        {
            Frm_Pnl_Alt _pnlAlt = new Frm_Pnl_Alt();
            var getIco = Convert.ToInt32(Icon);
            _pnlAlt.Option_Msg(Message, "خطا", getIco, Time, false, false, Show_Btnhide);
            _pnlAlt.Show();
            Set_Location(_pnlAlt, location);
        }
        //public static void ShowAlert(string Message, int Icon, int Time = 5, bool Show_Btnhide = true, string location = "DR", RightToLeft rightToLeft = RightToLeft.No)
        //{
        //    Frm_Pnl_Alt _pnlAlt = new Frm_Pnl_Alt();

        //    _pnlAlt.Option_Msg(Message, Msg_Name, Icon, Time, false, false, Show_Btnhide);
        //    _pnlAlt.Lbl_Msg.RightToLeft = rightToLeft;
        //    _pnlAlt.Show();
        //    Set_Location(_pnlAlt, location);
        //}
        //public static void FrmAlertAccessDeny(int Time = 4, bool Show_Btnhide = true, string location = "DC", RightToLeft rightToLeft = RightToLeft.No)
        //{
        //    Frm_Alert("Access Deny", 1, Time, Show_Btnhide, location, rightToLeft);
        //}

        public enum enumIcon
        {
            بلندگو, بستن_مربع, اطلاعات, سوال, لامپ, موفقیت, دسترسی, هشدار, ویرایش
        }

        //public class enumIcon
        //{
        //    /// <summary>
        //    /// هشدار
        //    /// </summary>      
        //    public static int بلندگو => 0;
        //    /// <summary>
        //    /// خطا
        //    /// </summary>
        //    public static int بستن_مربع => 1;
        //    /// <summary>
        //    /// اطلاعات
        //    /// </summary>
        //    public static int اطلاعات => 2;
        //    /// <summary>
        //    /// سوال
        //    /// </summary>
        //    public static int سوال => 3;
        //    /// <summary>
        //    /// هشدار
        //    /// </summary>
        //    public static int لامپ => 4;
        //    /// <summary>
        //    /// تایید
        //    /// </summary>
        //    public static int موفقیت => 5;
        //    public static int دسترسی => 6;
        //    public static int هشدار => 7;
        //    /// <summary>
        //    /// بدون تصویر
        //    /// </summary>
        //    public static int ویرایش => 8;
        //}

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
    }
}
