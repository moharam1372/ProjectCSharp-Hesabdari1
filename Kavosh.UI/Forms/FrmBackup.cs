using DevExpress.XtraEditors;
using Kavosh.DataAccess;
using Kavosh.DataAccess.Backup;
using MyCom.Class;
using System;
using System.Windows.Forms;

namespace Kavosh.UI.Forms
{
    public partial class FrmBackup : DevExpress.XtraEditors.XtraForm
    {
        private readonly DatabaseBackupService _backupService;

        public FrmBackup(DatabaseBackupService backupService)
        {
            InitializeComponent();
            _backupService = backupService;
            Shown += FrmBackup_Shown;
        }

        private void FrmBackup_Shown(object sender, EventArgs e) => RefreshList();

        private void RefreshList()
        {
            var backups = _backupService.GetBackupList();
            lstBackups.DataSource = backups;
            lstBackups.DisplayMember = nameof(BackupFileInfo.FileName);
            lstBackups.ValueMember = nameof(BackupFileInfo.FilePath);
        }

        private async void btnBackupNow_Click(object sender, EventArgs e)
        {
            using var progressForm = new FrmBackupProgress("در حال تهیه پشتیبان...");
            progressForm.Show(this);
            SetControlsEnabled(false);

            try
            {
                var progress = new Progress<int>(p => progressForm.SetProgress(p));
                var fileName = DateTime.Now.DateTimePersian().DateTimeForName + ".bak";
                await _backupService.BackupAsync(progress, fileName);

                RefreshList();
                XtraMessageBox.Show("پشتیبان‌گیری با موفقیت انجام شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"خطا در پشتیبان‌گیری:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressForm.Close();
                SetControlsEnabled(true);
            }
        }

        private async void btnRestore_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedItem is not BackupFileInfo selected)
            {
                XtraMessageBox.Show("یک نسخه‌ی پشتیبان از لیست انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = XtraMessageBox.Show(
                $"بازگردانی به نسخه‌ی «{selected.FileName}» انجام شود؟\n\nاطلاعات فعلی دیتابیس با این نسخه جایگزین می‌شود و این کار غیرقابل بازگشت است.",
                "تایید بازگردانی", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            using var progressForm = new FrmBackupProgress("در حال بازگردانی...");
            progressForm.Show(this);
            SetControlsEnabled(false);

            try
            {
                var progress = new Progress<int>(p => progressForm.SetProgress(p));
                await _backupService.RestoreAsync(selected.FilePath, progress);

                XtraMessageBox.Show("بازگردانی با موفقیت انجام شد. برنامه مجدداً راه‌اندازی می‌شود.",
                    "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"خطا در بازگردانی:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetControlsEnabled(true);
            }
            finally
            {
                progressForm.Close();
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnBackupNow.Enabled = enabled;
            btnRestore.Enabled = enabled;
            lstBackups.Enabled = enabled;
        }

        private void FrmBackup_Load(object sender, EventArgs e) { }
    }
}