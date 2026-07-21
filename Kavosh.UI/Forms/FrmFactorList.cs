using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using Kavosh.Services;
using Kavosh.UI.Reports.Factor;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Kavosh.UI.Forms
{
    public partial class FrmFactorList : DevExpress.XtraEditors.XtraForm
    {
        private readonly FactorHeaderService _factorHeaderService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtFactor;

        public FrmFactorList(FactorHeaderService factorHeaderService)
        {
            InitializeComponent();
            _factorHeaderService = factorHeaderService;
            Shown += FrmFactorList_Shown;
            Activated += FrmFactorList_Activated;   // 👈 هر بار فرم فوکوس گرفت، رفرش میشه (بعد از برگشت از FrmFactor)
        }

        private async void FrmFactorList_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            await SetFieldDgvFactor();
        }

        private async void FrmFactorList_Activated(object sender, EventArgs e)
        {
            if (_dtFactor is not null)
                await RefreshGridAsync();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvFactor);
            await dgvFactor.SetStyle();
        }

        public async Task SetFieldDgvFactor()
        {
            if (dgvFactor.ColumnCount() == 0)
            {
                _dtFactor = dgvFactor.GridStructure([
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "ویرایش", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit },
                    new() { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                    new() { Name = "کد فاکتور", Type = typeof(long) },
                    new() { Name = "طرف حساب", Type = typeof(string) },
                    new() { Name = "نوع فاکتور", Type = typeof(string) },
                    new() { Name = "تاریخ", Type = typeof(DateTime) },
                    new() { Name = "مبلغ کل", Type = typeof(long), PriceActive = true },
                    new() { Name = "چاپ", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.Print2 },
                ], false, true, true);

                dgvFactor.ActiveScrollGrid();
                dgvFactor.HiddenColumn("Id");
                dgvFactor.MaxMinWidth("ویرایش", 40, 40);
                dgvFactor.MaxMinWidth("حذف", 40, 40);
                dgvFactor.MaxMinWidth("چاپ", 40, 40);
                //dgvFactor.FixColumn("چاپ", FixedStyle.MiddleLeft);

                #region Event
                dgvFactor.AddEventRowCellClick<Guid>(async id =>
                {

                    var rpt = new Kavosh.UI.Reports.Factor.RptFactorA4();
                    var modelFactor = await _factorHeaderService.GetFactorReportDataAsync(id);
                    rpt.Tag = modelFactor;
                    rpt.ShowPreview(); // or rpt.Print();

                }, "Id", "چاپ");

                dgvFactor.AddEventRowCellClick<Guid>(id =>
                {
                    OpenFactor(id);
                }, "Id", "ویرایش");

                dgvFactor.AddEventRowCellClick<Guid>(id =>
                {
                    dgvFactor.DeleteRow(true, async () =>
                    {
                        await _factorHeaderService.DeleteFactorAsync(id);
                        await RefreshGridAsync();
                    });
                }, "Id", "حذف");

                #endregion
            }

            await RefreshGridAsync();
        }

        private async Task RefreshGridAsync()
        {
            var factors = await _factorHeaderService.GetAllFactorsAsync();

            _dtFactor.Rows.Clear();
            foreach (var f in factors)
            {
                _dtFactor.Rows.Add(f.Id, "ویرایش", "حذف", f.Code, f.PersonName, f.Type ? "فروش" : "خرید", f.DateFactor, f.PriceTotal);
            }
            dgvFactor.SetFieldSizeColumn();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            OpenFactor(null);
        }

        private void OpenFactor(Guid? id)
        {
            var frm = Program.CreateScopedForm<FrmFactor>();
            frm.FactorIdToEdit = id;
            frm.OverShowWait<FrmFactor>(this.MdiParent);
        }

        private void FrmFactorList_Load(object sender, EventArgs e) { }
    }
}