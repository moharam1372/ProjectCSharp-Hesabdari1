namespace Kavosh.UI.Forms
{
    partial class FrmBackupProgress
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblStatus = new DevExpress.XtraEditors.LabelControl();
            progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)progressBar.Properties).BeginInit();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(20, 25);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(77, 13);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "در حال پردازش...";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 50);
            progressBar.Name = "progressBar";
            progressBar.Properties.ShowTitle = true;
            progressBar.Size = new Size(360, 25);
            progressBar.TabIndex = 0;
            // 
            // FrmBackupProgress
            // 
            //AutoScaleDimensions = new SizeF(6F, 13F);
            //AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 100);
            ControlBox = false;
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FrmBackupProgress";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "لطفاً صبر کنید...";
            ((System.ComponentModel.ISupportInitialize)progressBar.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
    }
}