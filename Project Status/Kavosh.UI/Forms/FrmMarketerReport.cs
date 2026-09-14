using Kavosh.Services;
using MyCom.Class;
using System;
using System.Data;

namespace Kavosh.UI.Forms
{
    public partial class FrmMarketerReport : DevExpress.XtraEditors.XtraForm
    {
        private readonly MarketerService _marketerService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtReport;

        public FrmMarketerReport(MarketerService marketerService)
        {
            InitializeComponent();
            _marketerService = marketerService;
            Shown += FrmMarketerReport_Shown;
        }

        private async void FrmMarketerReport_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            SetFieldDgvReport();
            await RefreshGridAsync();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvMarketerReport);
            _clsFontBold.ChangeFont(srcGrid, 15);
            await dgvMarketerReport.SetStyle();
        }

        private void SetFieldDgvReport()
        {
            if (dgvMarketerReport.ColumnCount() == 0)
            {
                _dtReport = dgvMarketerReport.GridStructure([
                    new() { Name = "نام بازاریاب", Type = typeof(string) },
                    new() { Name = "شماره تماس", Type = typeof(string) },
                    new() { Name = "تعداد فاکتور", Type = typeof(int) },
                    new() { Name = "تعداد مشتری جذب‌شده", Type = typeof(int) },
                    new() { Name = "جمع فروش", Type = typeof(long), PriceActive = true },
                ], false, true, true);

                dgvMarketerReport.ActiveScrollGrid();
            }
        }

        private async Task RefreshGridAsync()
        {
            var items = await _marketerService.GetMarketerReportAsync();

            _dtReport.Rows.Clear();
            foreach (var m in items)
            {
                _dtReport.Rows.Add(m.MarketerFullName, m.PhoneNumber, m.FactorCount, m.CustomerCount, m.TotalSales);
            }
            dgvMarketerReport.SetFieldSizeColumn();
        }

        private void FrmMarketerReport_Load(object sender, EventArgs e) { }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            dgvMarketerReport.ExportToExcel("گزارش بازاریاب");
        }
    }
}