using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MyCom.Class;

namespace MyCom.Object
{
    public partial class Frm_MSG : XtraForm
    {
        public bool Check_Msg;
        private int k;
        private ClsFont _font = new ClsFont();
        public Frm_MSG(string Msg, string Title, int Icons, bool Bt = false, Color Color_Text = default(Color), string btnOk = "Ok", string btnCancel = "Cancel")
        {
            InitializeComponent();
            Btn_OK.Text = btnOk;
            Btn_Cancel.Text = btnCancel;

            if (Msg.Length < 40)
            {
                var _calc = 40 - Msg.Length;
                string _spcae = "";
                for (int i = 0; i < _calc; i++)
                {
                    _spcae = _spcae + " ";
                }

                Msg = Msg + _spcae;
            }
            Lbl_Msg.Text = Msg;
            Msg_Icon(Icons);
            Text = Title;
            Panel_Network.Text = Title;
            Btn_Cancel.Visible = Bt;

            Lbl_Msg.ForeColor = Color_Text;
            Set_Size();

            _font.ChangeFont(Lbl_Msg,17);
            _font.ChangeFont(Btn_OK, 11);
            _font.ChangeFont(Btn_Cancel, 11);

            Btn_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_OK.LookAndFeel.SkinName = "Money Twins";
            Btn_OK.LookAndFeel.SkinMaskColor = Color.FromArgb(192, 255, 255);

            Btn_Cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_Cancel.LookAndFeel.SkinName = "Money Twins";
            Btn_Cancel.LookAndFeel.SkinMaskColor = Color.FromArgb(192, 255, 255);

            Panel_Network.AppearanceCaption.Font = _font.ChangeFont(11.5F);

            // 
        }

        private void Frm_MSG_Load(object sender, EventArgs e)
        {
            Set_Size();
            timer1.Enabled = true;
            Thread.Sleep(250);
        }

        private void Set_Size()
        {
            Lbl_Msg.Left = 10;
            Width = Lbl_Msg.Width + 35;
            //+(Width / 5);
            Height = Lbl_Msg.Height + 129;

            Lbl_Msg.Left = Pic_Msg.Location.X - Lbl_Msg.Width + 25;

            if (Width < 230)
                Width = 230;
        }

        /// <summary>
        /// پنجره پیام
        /// </summary>
        /// <param name="Ic">کد خطا (آیکون مربوطه)</param>
        public void Msg_Icon(int Ic)
        {
            switch (Ic)
            {
                case 0:
                    Pic_Msg.Image = Properties.Resources.msgWarning_32;
                    break;

                case 7:
                    Pic_Msg.Image = Properties.Resources.msgWarning_32;
                    break;

                case 4:
                    Pic_Msg.Image = Properties.Resources.msgWarning_32;
                    break;

                case 1:
                    Pic_Msg.Image = Properties.Resources.msgError_Stop_32;
                    break;

                case 2:
                    Pic_Msg.Image = Properties.Resources.msgInfo_32;
                    break;

                case 3:
                    Pic_Msg.Image = Properties.Resources.msgQuestion_32;
                    break;

                case 5:
                    Pic_Msg.Image = Properties.Resources.msgOK;
                    break;

                case 6:
                    Pic_Msg.Image = Properties.Resources.msgSecurity_lock_32;
                    break;
            }
        }

        public bool Option_Msg(string Msg, string Tittle, int Icons, bool Bt)
        {
            Lbl_Msg.Text = Msg;
            Msg_Icon(Icons);
            Text = Tittle;
            Panel_Network.Text = Tittle;
            Btn_Cancel.Visible = Bt;
            Set_Size();

            return Check_Msg;
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            Check_Msg = true;
            Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Check_Msg = false;
            Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            k++;
            if (k == 2)
            {
                timer1.Enabled = false;
                k = 0;
            }
            Set_Size();
            _font.ChangeFont(Btn_Cancel, 13);
            _font.ChangeFont(Btn_OK, 13);

            Btn_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_OK.LookAndFeel.SkinName = "Money Twins";
            Btn_OK.LookAndFeel.SkinMaskColor = Color.FromArgb(192, 255, 255);


            Btn_Cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_Cancel.LookAndFeel.SkinName = "Money Twins";
            Btn_Cancel.LookAndFeel.SkinMaskColor = Color.FromArgb(192, 255, 255);

            _font.ChangeFont(Lbl_Msg,17);
            Panel_Network.AppearanceCaption.Font = _font.ChangeFont(11.5F);

            if (Lbl_Msg.RightToLeft == RightToLeft.No)
            {


            }
            else
            {

            }

            if (!Btn_Cancel.Visible)
                Btn_OK.Left = (Width / 2) - (Btn_OK.Width / 2);
        }



        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panelEx1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Lbl_Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Opacity = 0.45;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
            Opacity = 0.94;
        }

        private void Lbl_Title_Click(object sender, EventArgs e)
        {

        }

        private void Frm_MSG_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    Check_Msg = true;
                    Close();
                    break;
                case Keys.Escape:
                    Check_Msg = false;
                    Hide();
                    break;
            }
        }

        private void Frm_MSG_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Opacity = 0.45;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
            Opacity = 0.95;
        }

        private void Panel_Network_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    #region Icon MessageBox

    public struct Msg_Icon
    {
        /// <summary>
        /// Warning
        /// </summary>
        public int بلندگو => 0;
        /// <summary>
        /// Error
        /// </summary>
        public int بستن_مربع => 1;
        /// <summary>
        /// ویرایش
        /// </summary>
        public int اطلاعات => 2;
        public int سوال => 3;
        /// <summary>
        /// اطلاعات
        /// </summary>
        public int لامپ => 4;
        public int موفقیت => 5;
        public int دسترسی => 6;
        public int هشدار => 7;
        public int ویرایش => 8;
    }

    #endregion // کد آیکون جعبه پیام
}
