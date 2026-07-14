namespace MyCom.Object
{
    partial class frmCalender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalender));
            btnToday = new DevExpress.XtraEditors.SimpleButton();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            lblBetween = new System.Windows.Forms.Label();
            lblMonth = new System.Windows.Forms.Label();
            lblYear = new System.Windows.Forms.Label();
            dgvCalenMiladi = new Data_Grid();
            dgvCalenShamsi = new Data_Grid();
            Com_DateMiladi = new Date_SamimMiladi();
            Com_Date = new Date_Samim();
            simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            Btn_Back_Year = new DevExpress.XtraEditors.SimpleButton();
            simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            Btn_Next_Year = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            Btn_Back_Month = new DevExpress.XtraEditors.SimpleButton();
            Btn_Next_Month = new DevExpress.XtraEditors.SimpleButton();
            btnTodayMiladi = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // btnToday
            // 
            btnToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnToday.Location = new System.Drawing.Point(404, 5);
            btnToday.LookAndFeel.SkinName = "Office 2013";
            btnToday.LookAndFeel.UseDefaultLookAndFeel = false;
            btnToday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnToday.Name = "btnToday";
            btnToday.Size = new System.Drawing.Size(69, 29);
            btnToday.TabIndex = 18;
            btnToday.Text = "امروز";
            btnToday.Click += btnToday_Click;
            // 
            // panelControl1
            // 
            panelControl1.ContentImageAlignment = System.Drawing.ContentAlignment.BottomCenter;
            panelControl1.Controls.Add(lblBetween);
            panelControl1.Controls.Add(lblMonth);
            panelControl1.Controls.Add(lblYear);
            panelControl1.Controls.Add(dgvCalenMiladi);
            panelControl1.Controls.Add(dgvCalenShamsi);
            panelControl1.Controls.Add(Com_DateMiladi);
            panelControl1.Controls.Add(Com_Date);
            panelControl1.Controls.Add(simpleButton4);
            panelControl1.Controls.Add(Btn_Back_Year);
            panelControl1.Controls.Add(simpleButton3);
            panelControl1.Controls.Add(Btn_Next_Year);
            panelControl1.Controls.Add(simpleButton2);
            panelControl1.Controls.Add(simpleButton1);
            panelControl1.Controls.Add(Btn_Back_Month);
            panelControl1.Controls.Add(Btn_Next_Month);
            panelControl1.Controls.Add(btnTodayMiladi);
            panelControl1.Controls.Add(btnToday);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            panelControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(476, 277);
            panelControl1.TabIndex = 3;
            panelControl1.Paint += panelControl1_Paint;
            // 
            // lblBetween
            // 
            lblBetween.Location = new System.Drawing.Point(173, 5);
            lblBetween.Name = "lblBetween";
            lblBetween.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lblBetween.Size = new System.Drawing.Size(224, 29);
            lblBetween.TabIndex = 22;
            lblBetween.Text = "0";
            lblBetween.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMonth
            // 
            lblMonth.Location = new System.Drawing.Point(254, 240);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new System.Drawing.Size(178, 32);
            lblMonth.TabIndex = 22;
            lblMonth.Text = "فروردین";
            lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYear
            // 
            lblYear.Location = new System.Drawing.Point(45, 240);
            lblYear.Name = "lblYear";
            lblYear.Size = new System.Drawing.Size(95, 32);
            lblYear.TabIndex = 22;
            lblYear.Text = "1404";
            lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCalenMiladi
            // 
            dgvCalenMiladi.Location = new System.Drawing.Point(479, 36);
            dgvCalenMiladi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dgvCalenMiladi.Name = "dgvCalenMiladi";
            dgvCalenMiladi.Size = new System.Drawing.Size(468, 209);
            dgvCalenMiladi.TabIndex = 21;
            // 
            // dgvCalenShamsi
            // 
            dgvCalenShamsi.Location = new System.Drawing.Point(5, 36);
            dgvCalenShamsi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dgvCalenShamsi.Name = "dgvCalenShamsi";
            dgvCalenShamsi.Size = new System.Drawing.Size(468, 195);
            dgvCalenShamsi.TabIndex = 21;
            // 
            // Com_DateMiladi
            // 
            Com_DateMiladi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Com_DateMiladi.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178);
            Com_DateMiladi.Location = new System.Drawing.Point(760, 3);
            Com_DateMiladi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Com_DateMiladi.Name = "Com_DateMiladi";
            Com_DateMiladi.RightToLeft = System.Windows.Forms.RightToLeft.No;
            Com_DateMiladi.Size = new System.Drawing.Size(161, 32);
            Com_DateMiladi.TabIndex = 20;
            Com_DateMiladi.Load += Com_Date_Load;
            // 
            // Com_Date
            // 
            Com_Date.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Com_Date.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178);
            Com_Date.Location = new System.Drawing.Point(5, 5);
            Com_Date.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Com_Date.Name = "Com_Date";
            Com_Date.RightToLeft = System.Windows.Forms.RightToLeft.No;
            Com_Date.Size = new System.Drawing.Size(161, 26);
            Com_Date.TabIndex = 20;
            Com_Date.Load += Com_Date_Load;
            // 
            // simpleButton4
            // 
            simpleButton4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            simpleButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            simpleButton4.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton4.ImageOptions.Image");
            simpleButton4.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton4.Location = new System.Drawing.Point(478, 232);
            simpleButton4.LookAndFeel.SkinName = "Blue";
            simpleButton4.LookAndFeel.UseDefaultLookAndFeel = false;
            simpleButton4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButton4.Name = "simpleButton4";
            simpleButton4.Size = new System.Drawing.Size(41, 40);
            simpleButton4.TabIndex = 0;
            simpleButton4.Visible = false;
            simpleButton4.Click += Btn_Back_Year_Click;
            // 
            // Btn_Back_Year
            // 
            Btn_Back_Year.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Btn_Back_Year.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            Btn_Back_Year.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("Btn_Back_Year.ImageOptions.Image");
            Btn_Back_Year.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            Btn_Back_Year.Location = new System.Drawing.Point(5, 240);
            Btn_Back_Year.LookAndFeel.SkinName = "Blue";
            Btn_Back_Year.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_Back_Year.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Btn_Back_Year.Name = "Btn_Back_Year";
            Btn_Back_Year.Size = new System.Drawing.Size(33, 32);
            Btn_Back_Year.TabIndex = 0;
            Btn_Back_Year.Click += Btn_Back_Year_Click;
            // 
            // simpleButton3
            // 
            simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            simpleButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            simpleButton3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton3.ImageOptions.Image");
            simpleButton3.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton3.Location = new System.Drawing.Point(621, 232);
            simpleButton3.LookAndFeel.SkinName = "Blue";
            simpleButton3.LookAndFeel.UseDefaultLookAndFeel = false;
            simpleButton3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new System.Drawing.Size(41, 40);
            simpleButton3.TabIndex = 0;
            simpleButton3.Visible = false;
            simpleButton3.Click += Btn_Next_Year_Click;
            // 
            // Btn_Next_Year
            // 
            Btn_Next_Year.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Btn_Next_Year.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            Btn_Next_Year.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("Btn_Next_Year.ImageOptions.Image");
            Btn_Next_Year.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            Btn_Next_Year.Location = new System.Drawing.Point(147, 240);
            Btn_Next_Year.LookAndFeel.SkinName = "Blue";
            Btn_Next_Year.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_Next_Year.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Btn_Next_Year.Name = "Btn_Next_Year";
            Btn_Next_Year.Size = new System.Drawing.Size(33, 32);
            Btn_Next_Year.TabIndex = 0;
            Btn_Next_Year.Click += Btn_Next_Year_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            simpleButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            simpleButton2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton2.ImageOptions.Image");
            simpleButton2.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton2.Location = new System.Drawing.Point(687, 232);
            simpleButton2.LookAndFeel.SkinName = "Blue";
            simpleButton2.LookAndFeel.UseDefaultLookAndFeel = false;
            simpleButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new System.Drawing.Size(41, 40);
            simpleButton2.TabIndex = 0;
            simpleButton2.Visible = false;
            simpleButton2.Click += Btn_Back_Month_Click;
            // 
            // simpleButton1
            // 
            simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            simpleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton1.Location = new System.Drawing.Point(905, 232);
            simpleButton1.LookAndFeel.SkinName = "Blue";
            simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
            simpleButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(41, 40);
            simpleButton1.TabIndex = 0;
            simpleButton1.Visible = false;
            simpleButton1.Click += Btn_Next_Month_Click;
            // 
            // Btn_Back_Month
            // 
            Btn_Back_Month.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Btn_Back_Month.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            Btn_Back_Month.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("Btn_Back_Month.ImageOptions.Image");
            Btn_Back_Month.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            Btn_Back_Month.Location = new System.Drawing.Point(214, 240);
            Btn_Back_Month.LookAndFeel.SkinName = "Blue";
            Btn_Back_Month.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_Back_Month.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Btn_Back_Month.Name = "Btn_Back_Month";
            Btn_Back_Month.Size = new System.Drawing.Size(33, 32);
            Btn_Back_Month.TabIndex = 0;
            Btn_Back_Month.Click += Btn_Back_Month_Click;
            // 
            // Btn_Next_Month
            // 
            Btn_Next_Month.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Btn_Next_Month.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            Btn_Next_Month.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("Btn_Next_Month.ImageOptions.Image");
            Btn_Next_Month.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            Btn_Next_Month.Location = new System.Drawing.Point(439, 240);
            Btn_Next_Month.LookAndFeel.SkinName = "Blue";
            Btn_Next_Month.LookAndFeel.UseDefaultLookAndFeel = false;
            Btn_Next_Month.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Btn_Next_Month.Name = "Btn_Next_Month";
            Btn_Next_Month.Size = new System.Drawing.Size(33, 32);
            Btn_Next_Month.TabIndex = 0;
            Btn_Next_Month.Click += Btn_Next_Month_Click;
            // 
            // btnTodayMiladi
            // 
            btnTodayMiladi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnTodayMiladi.Location = new System.Drawing.Point(479, 5);
            btnTodayMiladi.LookAndFeel.SkinName = "Office 2013";
            btnTodayMiladi.LookAndFeel.UseDefaultLookAndFeel = false;
            btnTodayMiladi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnTodayMiladi.Name = "btnTodayMiladi";
            btnTodayMiladi.Size = new System.Drawing.Size(69, 29);
            btnTodayMiladi.TabIndex = 18;
            btnTodayMiladi.Text = "Today";
            btnTodayMiladi.Click += btnToday_Click;
            // 
            // frmCalender
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(476, 277);
            Controls.Add(panelControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCalender";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Load += frmCalender_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton btnToday;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public Date_Samim Com_Date;
 
        private DevExpress.XtraEditors.SimpleButton Btn_Back_Year;
        private DevExpress.XtraEditors.SimpleButton Btn_Next_Year;
        private DevExpress.XtraEditors.SimpleButton Btn_Back_Month;
        private DevExpress.XtraEditors.SimpleButton Btn_Next_Month;
        private Data_Grid dgvCalenMiladi;
        public Date_SamimMiladi Com_DateMiladi;
        public DevExpress.XtraEditors.SimpleButton btnTodayMiladi;
    
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public Data_Grid dgvCalenShamsi;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblBetween;
    }
}