namespace Kavosh.UI.Forms
{
    partial class FrmBackup
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlFunction = new Panel();
            btnBackupNow = new DevExpress.XtraEditors.SimpleButton();
            btnRestore = new DevExpress.XtraEditors.SimpleButton();
            lstBackups = new DevExpress.XtraEditors.ListBoxControl();
            pnlFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lstBackups).BeginInit();
            SuspendLayout();
            // 
            // pnlFunction
            // 
            pnlFunction.Controls.Add(btnBackupNow);
            pnlFunction.Controls.Add(btnRestore);
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(371, 39);
            pnlFunction.TabIndex = 1;
            // 
            // btnBackupNow
            // 
            btnBackupNow.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnBackupNow.Appearance.Options.UseFont = true;
            btnBackupNow.Dock = DockStyle.Right;
            btnBackupNow.Location = new Point(212, 0);
            btnBackupNow.Name = "btnBackupNow";
            btnBackupNow.Size = new Size(159, 39);
            btnBackupNow.TabIndex = 0;
            btnBackupNow.Text = "تهیه پشتیبان جدید";
            btnBackupNow.Click += btnBackupNow_Click;
            // 
            // btnRestore
            // 
            btnRestore.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnRestore.Appearance.Options.UseFont = true;
            btnRestore.Dock = DockStyle.Left;
            btnRestore.Location = new Point(0, 0);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(152, 39);
            btnRestore.TabIndex = 1;
            btnRestore.Text = "بازگردانی نسخه‌ی انتخابی";
            btnRestore.Click += btnRestore_Click;
            // 
            // lstBackups
            // 
            lstBackups.Appearance.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            lstBackups.Appearance.Options.UseFont = true;
            lstBackups.Dock = DockStyle.Fill;
            lstBackups.Location = new Point(0, 39);
            lstBackups.Name = "lstBackups";
            lstBackups.RightToLeft = RightToLeft.No;
            lstBackups.Size = new Size(371, 396);
            lstBackups.TabIndex = 0;
            // 
            // FrmBackup
            // 
            //AutoScaleDimensions = new SizeF(6F, 13F);
            //AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(371, 435);
            Controls.Add(lstBackups);
            Controls.Add(pnlFunction);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmBackup";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "پشتیبان‌گیری از دیتابیس";
            Load += FrmBackup_Load;
            pnlFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lstBackups).EndInit();
            ResumeLayout(false);
        }

        private Panel pnlFunction;
        private DevExpress.XtraEditors.SimpleButton btnBackupNow;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.ListBoxControl lstBackups;
    }
}