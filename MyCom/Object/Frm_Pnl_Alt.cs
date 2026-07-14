using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MyCom.Class;

namespace MyCom.Object
{
    public partial class Frm_Pnl_Alt : XtraForm
    {
        private ClsFont _font = new ClsFont(ClsFont.enumFont.samimFD);
        //private readonly Class_Code CC1 = new Class_Code();
        public bool Check_Msg = false;
        private int k, Tmp_Opacity;
        public int Tim_Hozor = 10;
        private bool Chk_End_App;
        public bool Check_OK = false;
        private Thread s;
        public bool MoveFrom = false;
        public Frm_Pnl_Alt()
        {
            Tmp_Opacity = 0;
            InitializeComponent();
            Set_Size();
        }

        private void Frm_Pnl_Alt2_Load(object sender, EventArgs e)
        {
            /*     bool instanceCountOne = true;

      using (Mutex mtex = new Mutex(true,Application.ProductName, out instanceCountOne))
      {
          if (instanceCountOne)
 
          {
              MessageBox.Show("این برنامه قبلا اجرا شده است", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              Hide();
          }
      }*/

            //  CC1.Ply_Sound(Application.StartupPath + @"\Sound Music\Alarm10.wav");
            //s = new Thread(Beep);
            //s.Start();
            Set_Size();
            timer1.Enabled = true;
            // Thread.Sleep(250);
        }

        //private void Beep()
        //{
        //    Console.Beep(5070, 1150);
        //    s.Abort();
        //}
        public void Set_Size()
        {
            //Lbl_Msg.Left = 20;

            //Width = Lbl_Msg.Width + 45;
            ////+(Width / 5);
            //Height = Lbl_Msg.Height + 70;
            //Height = Lbl_Msg.Height + 100;



            if (Width < 230)
                Width = 230;

            // Btn_OK.Left = (int) (Width - ((Width*12)/100)+((Width*1.5)/100));
            //  Btn_OK.Top = Height - ((Height*20)/100);
        }


        private void Msg_Icon(int Ic)
        {
            switch (Ic)
            {
                //case 0:
                //    Grp_Alert.CaptionImageOptions.Image = Resources.announcement_32x32;
                //    break;
                case 0:
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.bo_attention;
                    // Grp_Alert.CaptionImageOptions.Image = Properties.Resources.announcement_32x32;
                    break;
                case 1:
                    //   Grp_Alert.CaptionImageOptions.Image = Properties.Resources.close_32x32;
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.security_warningcircled2;
                    break;
                case 2:
                    //  Grp_Alert.CaptionImageOptions.Image = Properties.Resources.paste_32x32;
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.edit;
                    break;
                case 3:
                    //  Grp_Alert.CaptionImageOptions.Image = Properties.Resources.question_32x32;
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.actions_question;
                    break;
                case 4:
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.bo_attention;
                    break;
                case 5:
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.actions_checkcircled;
                    break;
                default:
                    //  Grp_Alert.CaptionImageOptions.Image = Properties.Resources.question_32x32;
                    Grp_Alert.CaptionImageOptions.SvgImage = MyCom.Properties.Resources.actions_question;
                    break;

            }
        }
        public int بلندگو => 0;
        public int خطا => 1;
        public int Paste => 2;
        public int سوال => 3;
        public int لامپ => 4;
        public int موفقیت => 5;

        public void Option_Msg(string Msg, string Tittle, int Icon, int Get_Hozor = 5, bool End_App = false, bool MoveFrom = false, bool Show_BtnHide = false)
        {

            _font.ChangeFont(Grp_Alert);
            Grp_Alert.AppearanceCaption.Font = _font.ChangeFont(10);


            Btn_OK.Visible = Show_BtnHide;
            Msg_Icon(Icon);
            //Lbl_Msg.Text = Msg;
            if (Get_Hozor > 0)
                Tim_Hozor = Get_Hozor;
            Chk_End_App = End_App;
            // Msg_Icon(Icons);
            Grp_Alert.Text = Tittle;
            // Btn_Cancel.Visible = Bt;
            Set_Size();



        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if (Chk_End_App)
                Application.Exit();
            Check_OK = true;
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
        }

        private int x, y;

        private void Lbl_Msg_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Pnl_Alt_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MoveFrom)
            {
                Left += e.X - x;
                Top += e.Y - y;
            }
        }

        private void Frm_Pnl_Alt_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void Pnl_Alarm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MoveFrom)
            {
                Left += e.X - x;
                Top += e.Y - y;
            }
        }

        private void Pnl_Alarm_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void Lbl_Msg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MoveFrom)
            {
                Left += e.X - x;
                Top += e.Y - y;
            }
        }

        private void Lbl_Msg_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void Pnl_Alarm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Tim_Auto_Hide_Tick(object sender, EventArgs e)
        {
            if (Tmp_Opacity == Tim_Hozor)
            {
                Tim_Auto_Show.Enabled = false;
                Tim_Auto_Hide.Interval = 1;
                Opacity -= 0.0075;
            }
            else
                Tmp_Opacity++;
            if (Opacity == 0)
            {
                Close();
                if (Chk_End_App)
                    Application.Exit();
            }
        }

        private void Tim_Auto_Show_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1)
                Opacity += 0.0075;
            if (Opacity == 1)
                Tim_Auto_Hide.Enabled = true;
        }

        private void Frm_Pnl_Alt_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Pnl_Alarm_MouseHover(object sender, EventArgs e)
        {
            if (Tim_Auto_Hide.Interval == 1)
            {
                Tim_Auto_Hide.Interval = 1000;
                Tmp_Opacity = 0;
                Opacity = 1;
            }
        }

        private void Grp_Alert_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Grp_Alert_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void Grp_Alert_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MoveFrom)
            {
                Left += e.X - x;
                Top += e.Y - y;
            }
        }

        private void Grp_Alert_MouseHover(object sender, EventArgs e)
        {
            if (Tim_Auto_Hide.Interval == 1 && MoveFrom)
            {
                Tim_Auto_Hide.Interval = 1000;
                Tmp_Opacity = 0;
                Opacity = 1;
            }
        }

        private void Lbl_Msg_MouseHover(object sender, EventArgs e)
        {
            if (Tim_Auto_Hide.Interval == 1 && MoveFrom)
            {
                Tim_Auto_Hide.Interval = 1000;
                Tmp_Opacity = 0;
                Opacity = 1;
            }
        }
    }
}
