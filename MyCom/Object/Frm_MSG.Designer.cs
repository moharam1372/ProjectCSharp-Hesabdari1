namespace MyCom.Object
{
    partial class Frm_MSG
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
            this.components = new System.ComponentModel.Container();
            this.Lbl_Msg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel_Network = new DevExpress.XtraEditors.GroupControl();
            this.Btn_Closed = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.Pic_Msg = new System.Windows.Forms.PictureBox();
            this.Btn_OK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Panel_Network)).BeginInit();
            this.Panel_Network.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Msg)).BeginInit();
            this.SuspendLayout();
            // 
            // Lbl_Msg
            // 
            this.Lbl_Msg.AutoSize = true;
            this.Lbl_Msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Msg.Location = new System.Drawing.Point(287, 71);
            this.Lbl_Msg.Name = "Lbl_Msg";
            this.Lbl_Msg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Lbl_Msg.Size = new System.Drawing.Size(55, 18);
            this.Lbl_Msg.TabIndex = 45;
            this.Lbl_Msg.Text = "متن پیام";
            this.Lbl_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Panel_Network
            // 
            this.Panel_Network.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Panel_Network.AppearanceCaption.Options.UseFont = true;
            this.Panel_Network.AppearanceCaption.Options.UseTextOptions = true;
            this.Panel_Network.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Panel_Network.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Panel_Network.Controls.Add(this.Btn_Closed);
            this.Panel_Network.Controls.Add(this.panel1);
            this.Panel_Network.Controls.Add(this.Lbl_Msg);
            this.Panel_Network.Controls.Add(this.Btn_Cancel);
            this.Panel_Network.Controls.Add(this.Pic_Msg);
            this.Panel_Network.Controls.Add(this.Btn_OK);
            this.Panel_Network.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Network.Location = new System.Drawing.Point(0, 0);
            this.Panel_Network.LookAndFeel.SkinMaskColor = System.Drawing.Color.DeepSkyBlue;
            this.Panel_Network.LookAndFeel.SkinName = "Money Twins";
            this.Panel_Network.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Panel_Network.Name = "Panel_Network";
            this.Panel_Network.Size = new System.Drawing.Size(376, 191);
            this.Panel_Network.TabIndex = 4;
            this.Panel_Network.Text = "متن پیام";
            this.Panel_Network.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Network_Paint);
            // 
            // Btn_Closed
            // 
            this.Btn_Closed.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Btn_Closed.Appearance.Options.UseFont = true;
            this.Btn_Closed.Appearance.Options.UseTextOptions = true;
            this.Btn_Closed.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Btn_Closed.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.Btn_Closed.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.Btn_Closed.ImageOptions.Image = global::MyCom.Properties.Resources.close_16x16;
            this.Btn_Closed.Location = new System.Drawing.Point(39, 181);
            this.Btn_Closed.LookAndFeel.SkinName = "Seven";
            this.Btn_Closed.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Btn_Closed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Closed.Name = "Btn_Closed";
            this.Btn_Closed.Size = new System.Drawing.Size(22, 21);
            this.Btn_Closed.TabIndex = 0;
            this.Btn_Closed.Visible = false;
            this.Btn_Closed.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 26);
            this.panel1.TabIndex = 46;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Btn_Cancel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Btn_Cancel.Appearance.Options.UseFont = true;
            this.Btn_Cancel.ImageOptions.SvgImage = global::MyCom.Properties.Resources.delete1;
            this.Btn_Cancel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_Cancel.Location = new System.Drawing.Point(63, 155);
            this.Btn_Cancel.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Btn_Cancel.LookAndFeel.SkinName = "Money Twins";
            this.Btn_Cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(124, 32);
            this.Btn_Cancel.TabIndex = 1;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Pic_Msg
            // 
            this.Pic_Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_Msg.Location = new System.Drawing.Point(332, 34);
            this.Pic_Msg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Pic_Msg.Name = "Pic_Msg";
            this.Pic_Msg.Size = new System.Drawing.Size(34, 32);
            this.Pic_Msg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_Msg.TabIndex = 36;
            this.Pic_Msg.TabStop = false;
            // 
            // Btn_OK
            // 
            this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Btn_OK.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Btn_OK.Appearance.Options.UseFont = true;
            this.Btn_OK.ImageOptions.SvgImage = global::MyCom.Properties.Resources.check1;
            this.Btn_OK.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_OK.Location = new System.Drawing.Point(191, 155);
            this.Btn_OK.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Btn_OK.LookAndFeel.SkinName = "Money Twins";
            this.Btn_OK.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Btn_OK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(124, 32);
            this.Btn_OK.TabIndex = 0;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Frm_MSG
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 191);
            this.Controls.Add(this.Panel_Network);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Frm_MSG";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_MSG2";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Frm_MSG_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_MSG_KeyDown);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Frm_MSG_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.Panel_Network)).EndInit();
            this.Panel_Network.ResumeLayout(false);
            this.Panel_Network.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Msg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton Btn_Cancel;
        public DevExpress.XtraEditors.SimpleButton Btn_OK;
        private System.Windows.Forms.PictureBox Pic_Msg;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.SimpleButton Btn_Closed;
        public System.Windows.Forms.Label Lbl_Msg;
        private DevExpress.XtraEditors.GroupControl Panel_Network;
        private System.Windows.Forms.Panel panel1;
    }
}