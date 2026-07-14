using System.Drawing;
using System.Windows.Forms;
using MyCom.Properties;

namespace MyCom
{
    partial class SplashScreen1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen1));
            progressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            labelCopyright = new DevExpress.XtraEditors.LabelControl();
            labelStatus = new DevExpress.XtraEditors.LabelControl();
            peLogo = new DevExpress.XtraEditors.PictureEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            peImage = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)peLogo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).BeginInit();
            SuspendLayout();
            // 
            // progressBarControl
            // 
            progressBarControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarControl.EditValue = 0;
            progressBarControl.Location = new Point(11, 175);
            progressBarControl.Name = "progressBarControl";
            progressBarControl.Properties.LookAndFeel.SkinName = "DevExpress Style";
            progressBarControl.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            progressBarControl.RightToLeft = RightToLeft.Yes;
            progressBarControl.Size = new Size(325, 22);
            progressBarControl.TabIndex = 5;
            // 
            // labelCopyright
            // 
            labelCopyright.Appearance.Font = new Font("Samim", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCopyright.Appearance.Options.UseFont = true;
            labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelCopyright.Location = new Point(11, 208);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(117, 19);
            labelCopyright.TabIndex = 6;
            labelCopyright.Text = "Copyright 2023-2024";
            // 
            // labelStatus
            // 
            labelStatus.Appearance.Font = new Font("Samim", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelStatus.Appearance.Options.UseFont = true;
            labelStatus.Location = new Point(219, 133);
            labelStatus.Margin = new Padding(3, 3, 3, 1);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(114, 23);
            labelStatus.TabIndex = 7;
            labelStatus.Text = " ... در حال بارگذاری";
            // 
            // peLogo
            // 
            peLogo.Anchor = AnchorStyles.Left;
            peLogo.EditValue = resources.GetObject("peLogo.EditValue");
            peLogo.Location = new Point(48, 55);
            peLogo.Name = "peLogo";
            peLogo.Properties.AllowFocused = false;
            peLogo.Properties.Appearance.BackColor = Color.Transparent;
            peLogo.Properties.Appearance.Options.UseBackColor = true;
            peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peLogo.Properties.ShowMenu = false;
            peLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            peLogo.Size = new Size(97, 42);
            peLogo.TabIndex = 8;
            peLogo.Visible = false;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new Font("Samim", 11.25F);
            labelControl1.Appearance.ForeColor = Color.LimeGreen;
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Appearance.Options.UseForeColor = true;
            labelControl1.Location = new Point(201, 203);
            labelControl1.Margin = new Padding(3, 3, 3, 1);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(135, 23);
            labelControl1.TabIndex = 7;
            labelControl1.Text = "گروه برنامه نویسی آران";
            // 
            // peImage
            // 
            peImage.EditValue = Resources.rampage1300;
            peImage.Location = new Point(1, 1);
            peImage.Name = "peImage";
            peImage.Properties.AllowFocused = false;
            peImage.Properties.Appearance.BackColor = Color.Transparent;
            peImage.Properties.Appearance.Options.UseBackColor = true;
            peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peImage.Properties.ShowMenu = false;
            peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            peImage.Size = new Size(343, 126);
            peImage.TabIndex = 9;
            // 
            // SplashScreen1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 237);
            Controls.Add(peImage);
            Controls.Add(peLogo);
            Controls.Add(labelControl1);
            Controls.Add(labelStatus);
            Controls.Add(labelCopyright);
            Controls.Add(progressBarControl);
            Name = "SplashScreen1";
            Padding = new Padding(1);
            Text = "SplashScreen1";
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)peLogo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private DevExpress.XtraEditors.PictureEdit peLogo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit peImage;
    }
}
