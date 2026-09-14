namespace Kavosh.UI.Forms
{
    partial class FrmManagePictureCheck
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
            picCheck = new DevExpress.XtraEditors.PictureEdit();
            panel1 = new Panel();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)picCheck.Properties).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // picCheck
            // 
            picCheck.Dock = DockStyle.Fill;
            picCheck.Location = new Point(0, 40);
            picCheck.Name = "picCheck";
            picCheck.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picCheck.Size = new Size(924, 363);
            picCheck.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 255, 192);
            panel1.Controls.Add(btnSave);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(924, 40);
            panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.ImageOptions.SvgImage = Properties.Resources.save;
            btnSave.Location = new Point(791, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(128, 34);
            btnSave.TabIndex = 0;
            btnSave.Text = "ذخیره";
            btnSave.Click += btnSave_Click;
            // 
            // FrmManagePictureCheck
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 403);
            Controls.Add(picCheck);
            Controls.Add(panel1);
            Name = "FrmManagePictureCheck";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "تصویر چک";
            Load += FrmManagePictureCheck_Load;
            ((System.ComponentModel.ISupportInitialize)picCheck.Properties).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picCheck;
        private Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}