namespace Kavosh.UI.Forms
{
    public partial class FrmBackupProgress : DevExpress.XtraEditors.XtraForm
    {
        public FrmBackupProgress(string title)
        {
            InitializeComponent();
            lblStatus.Text = title;
        }

        public void SetProgress(int percent)
        {
            if (InvokeRequired)
            {
                Invoke(() => progressBar.EditValue = percent);
                return;
            }
            progressBar.EditValue = percent;
        }
    }
}