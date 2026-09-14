using DevExpress.XtraGrid.Views.Base;
using Kavosh.Domain.Enums;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace Kavosh.UI.Forms
{
    public partial class FrmChequeList : DevExpress.XtraEditors.XtraForm
    {
        private readonly ChequeService _chequeService;
        private readonly DefinitiveAccountService _definitiveAccountService;   

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtCheque;
        private bool _pendingOnly = false;

        //public FrmChequeList(ChequeService chequeService)
        //{
        //    InitializeComponent();
        //    _chequeService = chequeService;
        //    Shown += FrmChequeList_Shown;
        //    Activated += FrmChequeList_Activated;
        //}
        public FrmChequeList(ChequeService chequeService, DefinitiveAccountService definitiveAccountService)
        {
            InitializeComponent();
            _chequeService = chequeService;
            _definitiveAccountService = definitiveAccountService;
            Shown += FrmChequeList_Shown;
            Activated += FrmChequeList_Activated;
        }
        private async void FrmChequeList_Shown(object sender, EventArgs e)
        {
            Text = _pendingOnly ? "چک‌های در جریان" : "مدیریت چک‌ها";
            await SetStyle();
            SetFieldDgvCheque();
            await RefreshGridAsync();
        }

        private async void FrmChequeList_Activated(object sender, EventArgs e)
        {
            if (_dtCheque is not null)
                await RefreshGridAsync();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvCheque);
            _clsFontBold.ChangeFont(srcGrid,15);
            await dgvCheque.SetStyle();
        }

        private void SetFieldDgvCheque()
        {
            if (dgvCheque.ColumnCount() == 0)
            {
                _dtCheque = dgvCheque.GridStructure([
                    new() { Name = "تصویر", Object = KavoshGrid.enumObject.Link, },
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "شماره چک", Type = typeof(string) },
                    new() { Name = "طرف حساب", Type = typeof(string) },
                    new() { Name = "نوع", Type = typeof(string) },
                    new() { Name = "تاریخ سررسید", Type = typeof(string) },
                    new() { Name = "روز مانده", Type = typeof(int) },
                    new() { Name = "مبلغ", Type = typeof(long), PriceActive = true },
                    new() { Name = "وضعیت", Type = typeof(string) },
                    new() { Name = "توضیحات", Type = typeof(string) },
                    new() { Name = "پاس شد", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit },
                    new() { Name = "برگشت خورد", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                ], false, true, true);

                dgvCheque.ActiveScrollGrid();
                dgvCheque.HiddenColumn("Id");

                // رنگ‌بندی بر اساس روزهای مانده تا سررسید (آلارم بصری)
                viewCheque.RowCellStyle += ViewCheque_RowCellStyle;


                dgvCheque.AddEventRowCellClick<string>(numCheck =>
                {
                    new FrmManagePictureCheck(numCheck).ShowDialog();
                }, "شماره چک", "تصویر");


                //dgvCheque.AddEventRowCellClick<Guid>(async id =>
                //{
                //    await SetStatus(id, ChequeStatus.Cleared);
                //}, "Id", "پاس شد");
                dgvCheque.AddEventRowCellClick<Guid>(async id =>
                {
                    var confirm = ClassMessageBox.ShowMSGQues("این چک پاس/وصول شود؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.اطلاعات);
                    if (!confirm)
                        return;

                    await _definitiveAccountService.SettleCheckByChequeIdAsync(id);
                    await RefreshGridAsync();

                    Kavosh.Services.AppEvents.RaiseDataChanged();   // 👈 رفرش داشبورد FrmMain
                }, "Id", "پاس شد");
                dgvCheque.AddEventRowCellClick<Guid>(async id =>
                {
                    var confirm = ClassMessageBox.ShowMSGQues("این چک برگشت بخورد؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.اطلاعات);
                    if (!confirm) return;

                    await _definitiveAccountService.BounceChequeAsync(id);
                    await RefreshGridAsync();

                    Kavosh.Services.AppEvents.RaiseDataChanged();
                }, "Id", "برگشت خورد");
            }
        }

        private void ViewCheque_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var statusObj = viewCheque.GetRowCellValue(e.RowHandle, "وضعیت");
            if (statusObj is not string status || status != "در جریان")
                return;

            var daysObj = viewCheque.GetRowCellValue(e.RowHandle, "روز مانده");
            if (daysObj is not int days) return;

            if (days <= 0)
                e.Appearance.BackColor = Color.FromArgb(255, 190, 190);   // امروز یا گذشته - قرمز
            else if (days == 1)
                e.Appearance.BackColor = Color.FromArgb(255, 220, 180);   // ۱ روز مانده - نارنجی
            else if (days == 2)
                e.Appearance.BackColor = Color.FromArgb(255, 245, 200);   // ۲ روز مانده - زرد
        }

        private async Task RefreshGridAsync()
        {
            var items = await _chequeService.GetAllAsync();

            _dtCheque.Rows.Clear();
            foreach (var c in items)
            {
                var daysRemaining = (c.DueDate.Date - DateTime.Today).Days;

                var value = c.DueDate.DateTimePersian().Date;
                _dtCheque.Rows.Add(
                    "",
                    c.Id,
                    c.ChequeNumber,
                    c.PersonName,
                    c.IsReceived ? "دریافتی" : "پرداختی",
                    value,
                    daysRemaining,
                    c.Price,
                    StatusText(c.Status),
                    c.Description,
                    "پاس شد",
                    "برگشت خورد"
                );
            }
            dgvCheque.SetFieldSizeColumn();
        }

        private static string StatusText(ChequeStatus status) => status switch
        {
            ChequeStatus.Cleared => "پاس شده",
            ChequeStatus.Bounced => "برگشتی",
            _ => "در جریان"
        };

        private async Task SetStatus(Guid id, ChequeStatus status)
        {
            var confirm = ClassMessageBox.ShowMSGQues(
                status == ChequeStatus.Cleared ? "این چک پاس/وصول شود؟" : "این چک برگشت بخورد؟",
                Class_Text.Msg_Name, ClassMessageBox.enumIcon.اطلاعات);

            if (!confirm) return;

            await _chequeService.SetStatusAsync(id, status);
            await RefreshGridAsync();
        }

        private void FrmChequeList_Load(object sender, EventArgs e) { }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            dgvCheque.ExportToExcel("چک ها");
        }
    }
}