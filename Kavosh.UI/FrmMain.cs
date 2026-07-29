using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Kavosh.DataAccess;
using Kavosh.Services;
using Kavosh.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using MyCom.Class;
using MyCom.Form_Portable;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kavosh.UI
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ClsFont _clsFont = new ClsFont(false);
        private ClsFont _clsFontBold = new ClsFont(true);


        // 👇 جدید — فیلدهای داشبورد
        private System.Windows.Forms.Timer _dashboardTimer;
        private DevExpress.XtraEditors.LabelControl lblTotalDebtValue;
        private DevExpress.XtraEditors.LabelControl lblCheckDebtValue;
        private DevExpress.XtraEditors.LabelControl lblOtherDebtValue;
        private DevExpress.XtraEditors.LabelControl lblLastFactorValue;
        private Guid? _lastFactorId;
        private Panel _pnlDashboard;
        private Panel _pnlDashboardBox;   // جعبه‌ی وسط‌چین با اندازه‌ی ثابت

        public FrmMain()
        {
            InitializeComponent();

        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(ribbon);
            ribbon.CustomizeReborn();
        }


        private async void FrmMain_Load(object sender, EventArgs e)
        {
            //ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            //await SetStyle();

            await SetStyle();

            BuildDashboardPanel();
            await RefreshDashboardAsync();

            AppEvents.DataChanged += OnAppDataChanged;

            _dashboardTimer = new System.Windows.Forms.Timer { Interval = 15000 }; // هر ۱۵ ثانیه
            _dashboardTimer.Tick += async (s, ev) => await RefreshDashboardAsync();
            _dashboardTimer.Start();
        }

        #region On DAshboard

        private async void OnAppDataChanged()
        {
            // ممکنه از یه فرم دیگه صدا زده بشه؛ چون همه روی همون UI Thread هستن مشکلی نیست
            await RefreshDashboardAsync();
        }

        // ============= ساخت پنل داشبورد =============
        private void BuildDashboardPanel()
        {
            _pnlDashboard = new Panel
            {
                // BackColor = Color.FromArgb(240, 240, 240)
            };

            _pnlDashboardBox = new Panel
            {
                Size = new Size(280, 260),
                BackColor = Color.FromArgb(255, 255, 192),
                Padding = new Padding(12)
            };

            var lblCaption = new DevExpress.XtraEditors.LabelControl
            {
                Text = "خلاصه وضعیت",
                Dock = DockStyle.Top,
                Height = 26
            };
            lblCaption.Appearance.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            lblCaption.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                RightToLeft = RightToLeft.No,
                BackColor = Color.Transparent
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            for (int i = 0; i < table.RowCount; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / table.RowCount));

            AddDashboardRow(table, 0, "کل بدهی‌ها:", out lblTotalDebtValue, "مشاهده", (s, e) => OpenDebtorsList());

            AddDashboardRow(table, 1, "بدهی چک:", out lblCheckDebtValue, "مشاهده", (s, e) => OpenDebtorsList(FrmDebtorsList.DebtFilterType.CheckOnly));

            AddDashboardRow(table, 2, "بدهی غیرچک:", out lblOtherDebtValue, "مشاهده", (s, e) => OpenDebtorsList(FrmDebtorsList.DebtFilterType.OtherOnly));

            AddDashboardRow(table, 3, "آخرین فاکتور:", out lblLastFactorValue, "ویرایش", (s, e) => OpenLastFactor());

            _pnlDashboardBox.Controls.Add(table);
            _pnlDashboardBox.Controls.Add(lblCaption);

            _pnlDashboard.Controls.Add(_pnlDashboardBox);
            Controls.Add(_pnlDashboard);
            _pnlDashboard.BringToFront();

            PositionDashboardPanel();

            this.Resize += (s, e) => PositionDashboardPanel();
            this.MdiChildActivate += (s1, e1) =>
            {
                if (_pnlDashboard is null) return;
                _pnlDashboard.Visible = ActiveMdiChild is null;
            };
        }
        // چون Dock نمی‌کنیم، خودمون محل و اندازه رو با توجه به ریبون/استاتوس‌بار حساب می‌کنیم
        private void PositionDashboardPanel()
        {
            if (_pnlDashboard is null) return;

            const int margin = 10;

            int top = ribbon.Bottom;
            int bottom = ribbonStatusBar.Visible ? ribbonStatusBar.Top : ClientSize.Height;

            // پنل بیرونی همچنان کل عرض/ارتفاع زیر ریبون رو پوشش میده
            // (تا وقتی هیچ فرزندی باز نیست، پس‌زمینه‌ی خاکستری همه‌جا رو بگیره)
            _pnlDashboard.Location = new Point(0, top);
            _pnlDashboard.Size = new Size(ClientSize.Width, Math.Max(0, bottom - top));

            // ولی جعبه‌ی خلاصه، کوچیک و گوشه‌ی بالا-راست قرار می‌گیره
            _pnlDashboardBox.Location = new Point(_pnlDashboard.Width - _pnlDashboardBox.Width - margin, margin
            );
        }
        // یک ردیف از جدول: (عنوان + مقدار) در ستون راست، دکمه در ستون چپ
        private static void AddDashboardRow(
            TableLayoutPanel table,
            int rowIndex,
            string title,
            out DevExpress.XtraEditors.LabelControl valueLabel,
            string buttonText,
            EventHandler onClick)
        {
            var infoPanel = new Panel { Dock = DockStyle.Fill };

            var lblTitle = new DevExpress.XtraEditors.LabelControl
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 18,
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
            };
            lblTitle.Appearance.Font = new Font("Samim FD", 9F, FontStyle.Bold);
            lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; // چسبیده به راست (RTL)

            valueLabel = new DevExpress.XtraEditors.LabelControl
            {
                Text = "...",
                Dock = DockStyle.Top,
                Height = 22,
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
            };
            valueLabel.Appearance.Font = new Font("Samim FD", 11F, FontStyle.Bold);
            valueLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            infoPanel.Controls.Add(valueLabel);
            infoPanel.Controls.Add(lblTitle);

            var btn = new DevExpress.XtraEditors.SimpleButton
            {
                Text = buttonText,
                Dock = DockStyle.Fill,
                Margin = new Padding(4)
            };
            btn.Appearance.Font = new Font("Samim FD", 9F, FontStyle.Bold);
            btn.Click += onClick;

            table.Controls.Add(infoPanel, 0, rowIndex);
            table.Controls.Add(btn, 1, rowIndex);
        }
        private static Panel CreateDashboardCard(string title, out DevExpress.XtraEditors.LabelControl valueLabel)
        {
            var panel = new Panel { Size = new Size(210, 48), Cursor = Cursors.Hand };

            var lblTitle = new DevExpress.XtraEditors.LabelControl
            {
                Text = title,
                Location = new Point(0, 4),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Size = new Size(210, 18),
            };
            lblTitle.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            valueLabel = new DevExpress.XtraEditors.LabelControl
            {
                Text = "...",
                Location = new Point(0, 22),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Size = new Size(210, 22),
            };
            valueLabel.Appearance.Font = new Font("Samim FD", 11F, FontStyle.Bold);
            valueLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(valueLabel);
            return panel;
        }
        // چون کلیک روی خودِ Panel و روی لیبل‌های داخلش هر دو باید کار کنن
        private static void AttachClickToCard(Panel card, EventHandler handler)
        {
            card.Click += handler;
            foreach (Control c in card.Controls)
            {
                c.Cursor = Cursors.Hand;
                c.Click += handler;
            }
        }

        // ============= رفرش داده‌های داشبورد =============
        private async Task RefreshDashboardAsync()
        {
            using var scope = Program.ServiceProvider.CreateScope();
            var definitiveAccountService = scope.ServiceProvider.GetRequiredService<DefinitiveAccountService>();
            var factorHeaderService = scope.ServiceProvider.GetRequiredService<FactorHeaderService>();

            var (total, check, other) = await definitiveAccountService.GetDebtSummaryAsync();
            lblTotalDebtValue.Text = total.ToString("N0");
            lblCheckDebtValue.Text = check.ToString("N0");
            lblOtherDebtValue.Text = other.ToString("N0");

            var lastFactor = await factorHeaderService.GetLastFactorAsync();
            if (lastFactor is null)
            {
                lblLastFactorValue.Text = "—";
                _lastFactorId = null;
            }
            else
            {
                _lastFactorId = lastFactor.Id;
                lblLastFactorValue.Text = $"#{lastFactor.Code} - {lastFactor.PersonName}";
            }
        }

        // ============= ناوبری =============
        private void OpenDebtorsList(FrmDebtorsList.DebtFilterType filterType = FrmDebtorsList.DebtFilterType.All)
        {
            var frm = Program.CreateScopedForm<FrmDebtorsList>();
            frm.SetFilter(filterType);
            frm.OverShowWait<FrmDebtorsList>(this);
        }

        private void OpenLastFactor()
        {
            if (!_lastFactorId.HasValue) return;

            var frm = Program.CreateScopedForm<FrmFactor>();
            frm.FactorIdToEdit = _lastFactorId;
            frm.OverShowWait<FrmFactor>(this);
        }

        #endregion

        private void barBtnProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            //new FrmProduct().OverShowWait<FrmProduct>(this);

            var frm = Program.CreateScopedForm<FrmProduct>();
            frm.OverShowWait<FrmProduct>(this);
        }

        private void barPerson_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmPerson>();
            frm.OverShowWait<FrmPerson>(this);
        }

        private void barFactor_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var frm = Program.CreateScopedForm<FrmFactor>();
            //frm.OverShowWait<FrmFactor>(this);

            var frm = Program.CreateScopedForm<FrmFactorList>();   // 👈 قبلاً FrmFactor بود
            frm.OverShowWait<FrmFactorList>(this);
        }

        private void barBtnAccounting_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmDefinitiveAccount>();
            // PersonIdToShow رو ست نمی‌کنیم — کاربر خودش از LookUpEdit داخل فرم انتخاب می‌کنه
            frm.OverShowWait<FrmDefinitiveAccount>(this);
        }

        private void barBtnDebtorsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmDebtorsList>();
            frm.OverShowWait<FrmDebtorsList>(this);
        }

        private void barBtnSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmStoreInfo>();
            frm.OverShowWait<FrmStoreInfo>(this);
        }

        private bool _backupCompleted = false;

        private async void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 👇 اضافه‌شده به ابتدای متد موجود، قبل از منطق بکاپ
            AppEvents.DataChanged -= OnAppDataChanged;
            _dashboardTimer?.Stop();
            _dashboardTimer?.Dispose();


            if (_backupCompleted)
                return;   // بکاپ گرفته شده، اجازه بده واقعاً بسته بشه

            e.Cancel = true;   // موقتاً جلوی بسته‌شدن رو بگیر

            using var scope = Program.ServiceProvider.CreateScope();
            var backupService = scope.ServiceProvider.GetRequiredService<DatabaseBackupService>();

            using var progressForm = new FrmBackupProgress("در حال تهیه پشتیبان قبل از خروج...");
            progressForm.Show(this);
            Application.DoEvents();

            try
            {
                var progress = new Progress<int>(p => progressForm.SetProgress(p));
                var fileName = DateTime.Now.DateTimePersian().DateTimeForName + ".bak";
                await backupService.BackupAsync(progress, fileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"خطا در تهیه پشتیبان خودکار:\n{ex.Message}\n\nبرنامه بدون بکاپ بسته می‌شود.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                progressForm.Close();
            }

            _backupCompleted = true;
            Close();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmBackup>();
            frm.OverShowWait<FrmBackup>(this);
        }

        private void barCodeControl1_Click(object sender, EventArgs e)
        {

        }
    }
}