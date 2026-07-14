namespace MyCom.Object
{
    partial class Frm_Pnl_Alt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            Tim_Auto_Hide = new System.Windows.Forms.Timer(components);
            Tim_Auto_Show = new System.Windows.Forms.Timer(components);
            Pnl_Alarm = new System.Windows.Forms.Panel();
            Grp_Alert = new DevExpress.XtraEditors.GroupControl();
            Btn_OK = new DevExpress.XtraEditors.SimpleButton();
            Pnl_Alarm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Grp_Alert).BeginInit();
            Grp_Alert.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // Tim_Auto_Hide
            // 
            Tim_Auto_Hide.Interval = 1000;
            Tim_Auto_Hide.Tick += Tim_Auto_Hide_Tick;
            // 
            // Tim_Auto_Show
            // 
            Tim_Auto_Show.Enabled = true;
            Tim_Auto_Show.Interval = 1;
            Tim_Auto_Show.Tick += Tim_Auto_Show_Tick;
            // 
            // Pnl_Alarm
            // 
            Pnl_Alarm.BackColor = System.Drawing.Color.Transparent;
            Pnl_Alarm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Pnl_Alarm.Controls.Add(Grp_Alert);
            Pnl_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            Pnl_Alarm.Location = new System.Drawing.Point(0, 0);
            Pnl_Alarm.Name = "Pnl_Alarm";
            Pnl_Alarm.Size = new System.Drawing.Size(928, 532);
            Pnl_Alarm.TabIndex = 36;
            Pnl_Alarm.Paint += Pnl_Alarm_Paint;
            Pnl_Alarm.MouseDown += Pnl_Alarm_MouseDown;
            Pnl_Alarm.MouseHover += Pnl_Alarm_MouseHover;
            Pnl_Alarm.MouseMove += Pnl_Alarm_MouseMove;
            // 
            // Grp_Alert
            // 
            Grp_Alert.Appearance.BackColor = System.Drawing.Color.Transparent;
            Grp_Alert.Appearance.Options.UseBackColor = true;
            Grp_Alert.AppearanceCaption.BorderColor = System.Drawing.Color.FromArgb(192, 255, 255);
            Grp_Alert.AppearanceCaption.Options.UseBorderColor = true;
            Grp_Alert.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            Grp_Alert.Controls.Add(Btn_OK);
            Grp_Alert.Dock = System.Windows.Forms.DockStyle.Fill;
            Grp_Alert.Location = new System.Drawing.Point(0, 0);
            Grp_Alert.LookAndFeel.SkinName = "WXI";
            Grp_Alert.LookAndFeel.UseDefaultLookAndFeel = false;
            Grp_Alert.Name = "Grp_Alert";
            Grp_Alert.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Grp_Alert.Size = new System.Drawing.Size(928, 532);
            Grp_Alert.TabIndex = 36;
            Grp_Alert.Text = "سربرگ";
            Grp_Alert.Paint += Grp_Alert_Paint;
            Grp_Alert.MouseDown += Grp_Alert_MouseDown;
            Grp_Alert.MouseHover += Grp_Alert_MouseHover;
            Grp_Alert.MouseMove += Grp_Alert_MouseMove;
            // 
            // Btn_OK
            // 
            Btn_OK.Appearance.Font = new System.Drawing.Font("B Yekan", 9.75F);
            Btn_OK.Appearance.Options.UseFont = true;
            Btn_OK.ImageOptions.Image = Properties.Resources.close_16x16;
            Btn_OK.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            Btn_OK.Location = new System.Drawing.Point(3, 3);
            Btn_OK.LookAndFeel.SkinName = "Office 2007 Pink";
            Btn_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_OK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Btn_OK.Name = "Btn_OK";
            Btn_OK.Size = new System.Drawing.Size(22, 22);
            Btn_OK.TabIndex = 0;
            Btn_OK.TabStop = false;
            Btn_OK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Application;
            Btn_OK.Click += Btn_OK_Click;
            // 
            // Frm_Pnl_Alt
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(928, 532);
            Controls.Add(Pnl_Alarm);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            IconOptions.ShowIcon = false;
            Name = "Frm_Pnl_Alt";
            Opacity = 0.01D;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            TopMost = true;
            FormClosing += Frm_Pnl_Alt_FormClosing;
            Load += Frm_Pnl_Alt2_Load;
            MouseDown += Frm_Pnl_Alt_MouseDown;
            MouseMove += Frm_Pnl_Alt_MouseMove;
            Pnl_Alarm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Grp_Alert).EndInit();
            Grp_Alert.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer Tim_Auto_Hide;
        private System.Windows.Forms.Timer Tim_Auto_Show;
        private System.Windows.Forms.Panel Pnl_Alarm;
        private DevExpress.XtraEditors.GroupControl Grp_Alert;
        private DevExpress.XtraEditors.SimpleButton Btn_OK;
    }
}