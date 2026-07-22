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
using DevExpress.XtraBars;
using DevExpress.XtraPrinting;

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

                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        PrintingSystemBase ps = printTool.PrintingSystem;

                        // غیرفعال کردن همه دکمه‌ها
                        foreach (PrintingSystemCommand cmd in Enum.GetValues(typeof(PrintingSystemCommand)))
                        {
                            ps.SetCommandVisibility(cmd, CommandVisibility.None);
                        }
                        // فعال کردن دکمه‌های مورد نظر
                        ps.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.All);
                        ps.SetCommandVisibility(PrintingSystemCommand.ExportPdf, CommandVisibility.All);
                        ps.SetCommandVisibility(PrintingSystemCommand.ExportFile, CommandVisibility.All);

                        // قفل کردن کامل نوار ابزار و جلوگیری از جابجایی
                        if (printTool.PreviewForm != null)
                        {
                            printTool.PreviewForm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                            printTool.PreviewForm.RightToLeftLayout = false;
                            var barManager = printTool.PreviewForm.PrintBarManager;
                            if (barManager != null)
                            {
                                // غیرفعال کردن شخصی‌سازی
                                barManager.AllowCustomization = false;
                                barManager.AllowQuickCustomization = false;
                                barManager.AllowShowToolbarsPopup = false;
                                barManager.AllowMoveBarOnToolbar = false;

              

                                // قفل کردن تمام نوارهای ابزار موجود
                                foreach (Bar bar in barManager.Bars)
                                {
                                    bar.OptionsBar.AllowQuickCustomization = false;
                                  
                                 
                                    bar.OptionsBar.AllowRename = false;
                                    bar.OptionsBar.AllowDelete = false;
                                }

                                #region Add Button

                                BarButtonItem customButton = new BarButtonItem(barManager, "نسخه چاپی");
                                customButton.Hint = "نسخه چاپی";
                                customButton.ItemAppearance.Normal.Font = new Font("Samim FD", 12);
                                customButton.ImageOptions.SvgImage = MyCom.Properties.Resources.Print2;
                                //customButton.Alignment = BarItemLinkAlignment.Left;
                                // می‌توانید یک آیکون نیز تنظیم کنید
                                // customButton.ImageOptions.Image = ...;

                                // 2. اضافه کردن دکمه به نوار ابزار اصلی
                                // نوار ابزار اصلی معمولاً اولین نوار در مجموعه Bars است
                                Bar toolbar = barManager.Bars[0];
                                if (toolbar != null)
                                {
                                    
                                    toolbar.ItemLinks.Add(customButton);
                                }

                                // 3. تعریف عملکرد دکمه
                                customButton.ItemClick += (s, e) => {
                                    // کاری که می‌خواهید دکمه انجام دهد را اینجا بنویسید
                                    // مثلاً نمایش یک پیام یا باز کردن یک فرم
                                    MessageBox.Show("دکمه سفارشی کلیک شد!");
                                    //rpt.ExportToImage("123.jpg");
                                    rpt.ExportToPdf("123.pdf");
                                };

                                #endregion
                            }
                        }
                  
                        // نمایش پیش‌نمایش
                        printTool.ShowPreviewDialog();
                    }


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