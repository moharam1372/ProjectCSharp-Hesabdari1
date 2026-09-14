using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Kavosh.Domain.Entities;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Form_Portable;
using MyCom.Object;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static MyCom.Form_Portable.FrmPortable;

namespace Kavosh.UI.Forms
{
    public partial class FrmCashRegister : DevExpress.XtraEditors.XtraForm
    {
        private readonly CashRegisterService _cashRegisterService;
        private readonly PartnerService _partnerService;
        private readonly ExpenseTypeService _expenseTypeService;
        private readonly PartnerExpenseService _partnerExpenseService;
   

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtPurchaseFactors, _dtPurchaseDocuments;
        private DataTable _dtSaleFactors, _dtSaleDocuments;
        private DataTable _dtPartnerBalance;

        // لیبل‌های گزارش - چون در Designer وجود ندارند، در کد ساخته می‌شوند (مشابه FrmMain.BuildDashboardPanel)
        private DevExpress.XtraEditors.LabelControl lblTotalSalesValue;
        private DevExpress.XtraEditors.LabelControl lblTotalPurchaseValue;
        private DevExpress.XtraEditors.LabelControl lblGrossProfitValue;
        private bool _reportPanelBuilt = false;


        private Guid _selectedExpenseId = Guid.Empty;
        private Guid _currentFilterPartnerId = Guid.Empty;
        public FrmCashRegister(CashRegisterService cashRegisterService,
            PartnerService partnerService,
            ExpenseTypeService expenseTypeService,
            PartnerExpenseService partnerExpenseService)
        {
            InitializeComponent();
            _cashRegisterService = cashRegisterService;
            _partnerService = partnerService;
            _expenseTypeService = expenseTypeService;
            _partnerExpenseService = partnerExpenseService;
          
            Shown += FrmCashRegister_Shown;
        }

        private async void FrmCashRegister_Shown(object sender, EventArgs e)
        {
            _clsFontBold.ChangeFont(tabPane1);
            _clsFontBold.ChangeFont(btnCreateSaleDocument);
            _clsFontBold.ChangeFont(btnCreatePurchaseDocument);
            _clsFontBold.ChangeFont(cmbSaleDocument, 14);
            _clsFontBold.ChangeFont(btnDocFinal);

            await SetFieldPurchaseTab();
            await SetFieldSaleTab();

            tabPane1.SelectedPageChanged += async (s1, e1) =>
            {
                if (e1.Page == tabExpense)
                {
                    await Task.Delay(200);
                    await SetFieldExpenseTab();
                }
                else if (e1.Page == tabReport)
                {
                    BuildReportPanel();
                    await RefreshReportAsync();

                }
                else if (e1.Page == tabNavigationPage1)

                {
                    await SetupDocCalcGridStructure();
                    await RefreshDocCalcGridAsync();
                }
            };
        }

        // =========================================================================
        // تب ۱ و ۲ — خرید و فروش
        // =========================================================================

        public async Task SetFieldPurchaseTab()
        {
            tabPurchase.WaitDownPage(async () =>
            {
                _clsFontBold.ChangeFont(dgvPurchaseFactors);
                _clsFontBold.ChangeFont(dgvPurchaseDocuments);
                await dgvPurchaseFactors.SetStyle();
                await dgvPurchaseDocuments.SetStyle();

                SetupFactorGrid(dgvPurchaseFactors, false);
                SetupDocumentGrid(dgvPurchaseDocuments, false);


                pnlPurchaseFunction.Controls.Add(btnCreatePurchaseDocument);
                btnCreatePurchaseDocument.Text = "ثبت سند خرید";
                btnCreatePurchaseDocument.Dock = DockStyle.Right;
                btnCreatePurchaseDocument.Click += async (s, e) => await CreateDocumentAsync(false);

                await RefreshFactorGridAsync(false);
                await RefreshDocumentGridAsync(false);
            });
        }

        public async Task SetFieldSaleTab()
        {
            tabSale.WaitDownPage(async () =>
            {
                _clsFontBold.ChangeFont(dgvSaleFactors);
                _clsFontBold.ChangeFont(dgvSaleDocuments);
                await dgvSaleFactors.SetStyle();
                await dgvSaleDocuments.SetStyle();

                SetupFactorGrid(dgvSaleFactors, true);
                SetupDocumentGrid(dgvSaleDocuments, true);

                panel2.Controls.Add(btnCreateSaleDocument);
                btnCreateSaleDocument.Text = "ثبت سند فروش";
                btnCreateSaleDocument.Dock = DockStyle.Right;
                btnCreateSaleDocument.Click += async (s, e) => await CreateDocumentAsync(true);

                await RefreshFactorGridAsync(true);
                await RefreshDocumentGridAsync(true);
            });
        }

        // ساختار گرید فاکتورهای سندنخورده - انتخاب با CheckBox
        private void SetupFactorGrid(KavoshGrid grid, bool type)
        {
            if (grid.ColumnCount() > 0) return;

            var dt = grid.GridStructure([
                new() { Name = "Id", Type = typeof(Guid) },
                new() { Name = "انتخاب", Type = typeof(bool) },
                new() { Name = "کد فاکتور", Type = typeof(long) },
                new() { Name = "طرف حساب", Type = typeof(string) },
                new() { Name = "تاریخ", Type = typeof(string) },
                new() { Name = "مبلغ کل", Type = typeof(long), PriceActive = true },
            ], true, false, true);

            if (type) _dtSaleFactors = dt;
            else _dtPurchaseFactors = dt;

            grid.ActiveScrollGrid();
            grid.HiddenColumn("Id");
        }

        // ساختار گرید اسناد ثبت‌شده - با دکمه‌ی حذف سند
        private void SetupDocumentGrid(KavoshGrid grid, bool type)
        {
            if (grid.ColumnCount() > 0) return;

            var dt = grid.GridStructure([
                new() { Name = "Id", Type = typeof(Guid) },
                new() { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                new() { Name = "شماره سند", Type = typeof(long) },
                new() { Name = "تعداد فاکتور", Type = typeof(int) },
                new() { Name = "مبلغ کل سند", Type = typeof(long), PriceActive = true },
                new() { Name = "تاریخ", Type = typeof(string) },
                new() { Name = "توضیحات", Type = typeof(string) },
            ], false, true, true);


            grid.AddSummaryItem("مبلغ کل سند", "مبلغ کل سند", "{0}", SummaryItemType.Custom);

            if (type) _dtSaleDocuments = dt;
            else _dtPurchaseDocuments = dt;

            grid.ActiveScrollGrid();
            grid.HiddenColumn("Id");
            grid.MaxMinWidth("حذف", 45, 45);
            grid.MaxMinWidth("مبلغ کل سند", 125, 125);


            #region Event    

            double sumPrice = 0;
            grid.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
            {
                grid.AutoSummaryCalculate(e1, "مبلغ کل سند", "جمع", ref sumPrice, "تومان");
            };

            grid.AddEventRowCellClick<Guid>(id =>
            {
                grid.DeleteRow(true, async () =>
                {
                    await _cashRegisterService.DeleteDocumentAsync(id);
                    await RefreshFactorGridAsync(type);
                    await RefreshDocumentGridAsync(type);
                    ClassMessageBox.ShowMSG("سند حذف شد و فاکتورهای آن قابل ویرایش شدند.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
                });
            }, "Id", "حذف");

            #endregion

        }

        private async Task RefreshFactorGridAsync(bool type)
        {
            var dt = type ? _dtSaleFactors : _dtPurchaseFactors;
            if (dt is null) return;

            //try
            //{
            var items = await _cashRegisterService.GetUndocumentedFactorsAsync(type);

            dt.Rows.Clear();
            foreach (var f in items)
            {
                dt.Rows.Add(f.Id, false, f.Code, f.PersonName, f.DateFactor.DateTimePersian().Date, f.PriceTotal);
            }

            (type ? dgvSaleFactors : dgvPurchaseFactors).SetFieldSizeColumn();
            //}
            //catch (Exception e)
            //{

            //}
        }

        private async Task RefreshDocumentGridAsync(bool type)
        {
            var dt = type ? _dtSaleDocuments : _dtPurchaseDocuments;
            if (dt is null) return;

            var items = await _cashRegisterService.GetDocumentsAsync(type);

            dt.Rows.Clear();
            foreach (var d in items)
            {
                dt.Rows.Add(d.Id, "حذف", d.Code, d.FactorCount, d.TotalAmount, d.DateDocument.DateTimePersian().Date, d.Description);
            }

            (type ? dgvSaleDocuments : dgvPurchaseDocuments).SetFieldSizeColumn();
        }

        private async Task CreateDocumentAsync(bool type)
        {
            var dt = type ? _dtSaleFactors : _dtPurchaseFactors;

            var selectedIds = dt.Rows
                .Cast<DataRow>()
                .Where(r => r.RowState != DataRowState.Deleted)
                .Where(r => r["انتخاب"] != DBNull.Value && Convert.ToBoolean(r["انتخاب"]))
                .Select(r => (Guid)r["Id"])
                .ToList();

            if (selectedIds.Count == 0)
            {
                ClassMessageBox.ShowMSG("حداقل یک فاکتور را انتخاب کنید.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
                return;
            }

            var confirm = ClassMessageBox.ShowMSGQues(
                $"سند برای {selectedIds.Count} فاکتور ثبت شود؟ پس از ثبت، این فاکتورها دیگر قابل ویرایش نخواهند بود.",
                Class_Text.Msg_Name, ClassMessageBox.enumIcon.اطلاعات);

            if (!confirm) return;

            try
            {
                await _cashRegisterService.CreateDocumentAsync(type, selectedIds, "");
                await RefreshFactorGridAsync(type);
                await RefreshDocumentGridAsync(type);
                ClassMessageBox.ShowMSG("سند با موفقیت ثبت شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
            }
            catch (Exception ex)
            {
                ClassMessageBox.ShowMSG(ex.Message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
            }
        }

        // =========================================================================
        // تب ۳ — مدیریت هزینه‌ها (شریک + نوع هزینه + دریافت/پرداخت + جمع کلی هر شریک)
        // =========================================================================

        private bool _expenseTabLoaded = false;

        public async Task SetFieldExpenseTab()
        {
            if (_expenseTabLoaded)
            {
                await RefreshTransactionsAsync();
                return;
            }
            _expenseTabLoaded = true;

            // 👇 ساخت ساختار گرید سینک و بیرون از WaitDownPage، تا هیچ callbackِ زودهنگامی با دیتاتیبل null مواجه نشه
            SetupPartnerBalanceGrid();

            tabExpense.WaitDownPage(async () =>
            {
                _clsFontBold.ChangeFont(dgvPartnerBalance);
                await dgvPartnerBalance.SetStyle();

                layExpenseInput.RightToLeft = RightToLeft.Yes;
                pnlExpenseFunction.Controls.Add(layExpenseInput.ShowPanelOperation());
                layExpenseInput.AddButtonOperation();
                layExpenseInput._btnCancel.Enabled = false;

                var txtId = ClsCollect.ModelTextEdit("Id", 50, "");

                #region کمبو شریک - با امکان افزودن سریع (نمایش لیست شرکا در دراپ‌داون)

                var getPartners = (await _partnerService.GetAllAsync()).Select(p => new { p.Id, p.FullName }).ToList();

                Panel pnlPartner = null;
                //pnlPartner = ClsCollect.ModelGridToDataLayoutBtn("شریک", getPartners, "Id", "FullName", "", async () =>
                pnlPartner = ClsCollect.ModelGridToDataLayoutFull("شریک", getPartners, "Id", "FullName", "", PartnerChangeValue, async () =>
                {
                    var getData = (await _partnerService.GetAllAsync()).Select(s => new ModelPortableData { Id = s.Id, Title = s.FullName }).ToList();

                    var frmPortable = new FrmPortable("شریک", getData, new ModelAction
                    {
                        SaveData = async void (data) =>
                        {
                            await _partnerService.SaveAsync(new PartnerDto { Id = data.Id, FullName = data.Title });
                            var cmb = pnlPartner.Controls.OfType<GridLookUpEdit>().First();
                            getPartners = (await _partnerService.GetAllAsync()).Select(p => new { p.Id, p.FullName }).ToList();
                            cmb.UpdateGridLookUpEdit(getPartners);
                            await RefreshTransactionsAsync();
                        },
                        DeleteData = async void (id) =>
                        {
                            try { await _partnerService.DeleteAsync(id); }
                            catch (Exception ex) { ClassMessageBox.ShowMSG(ex.Message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار); }
                            var cmb = pnlPartner.Controls.OfType<GridLookUpEdit>().First();
                            getPartners = (await _partnerService.GetAllAsync()).Select(p => new { p.Id, p.FullName }).ToList();
                            cmb.UpdateGridLookUpEdit(getPartners);
                            await RefreshTransactionsAsync();
                        },
                    });
                    frmPortable.FormClosing += (s1, e1) => SendKeys.SendWait("{Enter}");
                    await frmPortable.ShowDialogAsync();
                });
                pnlPartner.Controls.Add(new SimpleButton { Width = 50, Left = 0 });
                pnlPartner.ConvertGroupToGrid().HiddenColumn("Id");


                //var cmbPerson = ClsCollect.ModelGridToDataLayoutFull("طرف حساب", persons, "Id", "FullName", "", async id =>
                //{
                //    await RefreshStatementAsync(id.ToGuid());
                //});
                #endregion

                #region کمبو نوع هزینه - با امکان افزودن سریع

                var getExpenseTypes = (await _expenseTypeService.GetAllAsync())
                    .Select(t => new { t.Id, t.Title }).ToList();

                Panel pnlExpenseType = null;
                pnlExpenseType = ClsCollect.ModelGridToDataLayoutBtn("نوع هزینه", getExpenseTypes, "Id", "Title", "", async () =>
                {
                    var getData = (await _expenseTypeService.GetAllAsync())
                        .Select(s => new ModelPortableData { Id = s.Id, Title = s.Title }).ToList();

                    var frmPortable = new FrmPortable("نوع هزینه", getData, new ModelAction
                    {
                        SaveData = async void (data) =>
                        {
                            await _expenseTypeService.SaveAsync(new ExpenseTypeDto { Id = data.Id, Title = data.Title });
                            var cmb = pnlExpenseType.Controls.OfType<GridLookUpEdit>().First();
                            getExpenseTypes = (await _expenseTypeService.GetAllAsync()).Select(t => new { t.Id, t.Title }).ToList();
                            cmb.UpdateGridLookUpEdit(getExpenseTypes);
                        },
                        DeleteData = async void (id) =>
                        {
                            try { await _expenseTypeService.DeleteAsync(id); }
                            catch (Exception ex) { ClassMessageBox.ShowMSG(ex.Message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار); }
                            var cmb = pnlExpenseType.Controls.OfType<GridLookUpEdit>().First();
                            getExpenseTypes = (await _expenseTypeService.GetAllAsync()).Select(t => new { t.Id, t.Title }).ToList();
                            cmb.UpdateGridLookUpEdit(getExpenseTypes);
                        },
                    });
                    frmPortable.FormClosing += (s1, e1) => SendKeys.SendWait("{Enter}");
                    await frmPortable.ShowDialogAsync();
                });
                pnlExpenseType.Controls.Add(new SimpleButton { Width = 50, Left = 0 });
                pnlExpenseType.ConvertGroupToGrid().HiddenColumn("Id");

                #endregion

                var cmbType = ClsCollect.ModelRadioGroup("نوع تراکنش", new List<ClsCollect.modelRadioGroup>
                {
                    new() { Column = 1, Field = "پرداخت شریک (هزینه از جیب)" },
                    new() { Column = 1, Field = "دریافت از شریک (تسویه/برداشت)" },
                }, true, BorderStyles.NoBorder, 12.5F);

                var txtAmount = ClsCollect.ModelTextEditPrice("مبلغ", 50, "");
                var dtDate = ClsCollect.ModelDateTime("تاریخ", 10, DateTime.Now.DateTimePersian().Date);
                var txtDescription = ClsCollect.ModelLayoutMemoEdit("توضیحات", 200, "");

                layExpenseInput.SetFieldColumnDataLayout(true, 1, [
                    new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
                    new() { Grp = 1, Ctrl = pnlPartner, AllowNull = false, SizeType = SizeConstraintsType.Custom, AutoHeight = 38 },
                    new() { Grp = 1, Ctrl = pnlExpenseType, SizeType = SizeConstraintsType.Custom, AutoHeight = 38 },
                    new() { Grp = 1, Ctrl = cmbType, AllowNull = false, SizeType = SizeConstraintsType.Custom, AutoHeight = 65 },
                    new() { Grp = 1, Ctrl = txtAmount, AllowNull = false },
                    new() { Grp = 1, Ctrl = dtDate, AllowNull = false },
                    new() { Grp = 1, Ctrl = txtDescription, SizeType = SizeConstraintsType.Custom, AutoHeight = 55 },
                ]);

                layExpenseInput.BtnSaveClick += LayExpenseInput_BtnSaveClick;
                layExpenseInput.CallNew();
            });
        }

        private void PartnerChangeValue(object obj)
        {
            _ = RefreshPartnerStatementAsync(obj.ToGuid());
        }

        // ساختار گرید «جمع هر شریک به‌صورت کلی» - یک ردیف برای هر شریک
        // ساختار گرید لیست تراکنش‌ها (بستانکار/بدهکار) - مثل صورت‌حساب دریافت/پرداخت فاز قبل
        private void SetupPartnerBalanceGrid()
        {
            if (_dtPartnerBalance is not null) return;

            _dtPartnerBalance = dgvPartnerBalance.GridStructure([
                new() { Name = "Id", Type = typeof(Guid) },
                new() { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                new()
                {
                    Name = "ویرایش", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit
                },
                new() { Name = "تاریخ", Type = typeof(string) },
                new() { Name = "شریک", Type = typeof(string) },
                new() { Name = "نوع هزینه", Type = typeof(string) },
                new() { Name = "بستانکار", Type = typeof(long), PriceActive = true },
                new() { Name = "بدهکار", Type = typeof(long), PriceActive = true },
                new() { Name = "مانده", Type = typeof(long), PriceActive = true },
                new() { Name = "توضیحات", Type = typeof(string) },
            ], false, true, true);

            dgvPartnerBalance.ActiveScrollGrid();
            dgvPartnerBalance.HiddenColumn("Id");
            dgvPartnerBalance.MaxMinWidth("حذف", 45, 45);
            dgvPartnerBalance.MaxMinWidth("ویرایش", 45, 45);

            viewPartnerBalance.RowCellStyle += (s1, e1) =>
            {
                var col = e1.Column.FieldName;
                if (col == "بستانکار") e1.Appearance.ForeColor = Color.FromArgb(0, 150, 0);
                else if (col == "بدهکار") e1.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                else if (col == "مانده")
                {
                    var balanceObj = viewPartnerBalance.GetRowCellValue(e1.RowHandle, "مانده");
                    if (balanceObj != null && balanceObj != DBNull.Value)
                    {
                        var balance = Convert.ToInt64(balanceObj);
                        if (balance > 0) e1.Appearance.ForeColor = Color.FromArgb(0, 150, 0);
                        else if (balance < 0) e1.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                    }
                }
            };

            dgvPartnerBalance.AddEventRowCellClick<Guid>(id =>
            {
                dgvPartnerBalance.DeleteRow(true, async () =>
                {
                    await _partnerExpenseService.DeleteAsync(id);
                    await RefreshTransactionsAsync();
                });
            }, "Id", "حذف");

            dgvPartnerBalance.AddEventRowCellClick<Guid>(async id =>
            {
                var dto = await _partnerExpenseService.GetByIdAsync(id);
                if (dto is null) return;

                _selectedExpenseId = dto.Id;
                layExpenseInput.SetValueType("شریک", dto.PartnerId);
                if (dto.ExpenseTypeId.HasValue)
                    layExpenseInput.SetValueType("نوع هزینه", dto.ExpenseTypeId.Value);
                layExpenseInput.SetValueType("نوع تراکنش", dto.IsPayment ? "پرداخت شریک (هزینه از جیب)" : "دریافت از شریک (تسویه/برداشت)");
                layExpenseInput.SetValueType("مبلغ", dto.Amount);
                layExpenseInput.SetValueType("تاریخ", dto.DateCustom.DateTimePersian().Date);
                layExpenseInput.SetValueType("توضیحات", dto.Description);
            }, "Id", "ویرایش");
        }

        // همه‌ی تراکنش‌ها (وقتی هنوز شریکی از کمبو انتخاب نشده)
        private async Task RefreshTransactionsAsync()
        {
            if (_dtPartnerBalance is null) return;

            if (_currentFilterPartnerId != Guid.Empty)
            {
                await RefreshPartnerStatementAsync(_currentFilterPartnerId);
                return;
            }

            var items = await _partnerExpenseService.GetAllAsync();
            FillTransactionGrid(items, withRunningBalance: false);
        }

        // فقط تراکنش‌های شریک انتخاب‌شده - با مانده‌ی تجمعی
        private async Task RefreshPartnerStatementAsync(Guid partnerId)
        {
            if (_dtPartnerBalance is null) return;

            _currentFilterPartnerId = partnerId;
            var items = await _partnerExpenseService.GetStatementByPartnerAsync(partnerId);
            FillTransactionGrid(items, withRunningBalance: true);
        }

        private void FillTransactionGrid(List<PartnerExpenseDto> items, bool withRunningBalance)
        {
            _dtPartnerBalance.Rows.Clear();
            long runningBalance = 0;

            foreach (var e in items)
            {
                var credit = e.IsPayment ? e.Amount : 0;
                var debit = !e.IsPayment ? e.Amount : 0;

                if (withRunningBalance)
                    runningBalance += e.IsPayment ? e.Amount : -e.Amount;

                _dtPartnerBalance.Rows.Add(
                    e.Id, "حذف", "ویرایش",
                    e.DateCustom.DateTimePersian().Date,
                    e.PartnerFullName,
                    e.ExpenseTypeTitle,
                    credit, debit,
                    withRunningBalance ? runningBalance : (credit - debit),
                    e.Description
                );
            }

            dgvPartnerBalance.SetFieldSizeColumn();
        }

        // ============= ذخیره‌ی تراکنش هزینه =============
        private async void LayExpenseInput_BtnSaveClick(object sender, EventArgs e)
        {
            layExpenseInput._disableAfterSave = true;
            try
            {
                var partnerId = layExpenseInput.GetValue<Guid>("شریک");
                var expenseTypeIdRaw = layExpenseInput.GetValue<Guid>("نوع هزینه");
                var typeText = layExpenseInput.GetValue<string>("نوع تراکنش");
                var amount = layExpenseInput.GetValue<long>("مبلغ");
                var dateText = layExpenseInput.GetValue<string>("تاریخ");
                var description = layExpenseInput.GetValue<string>("توضیحات");

                var dto = new PartnerExpenseDto
                {
                    Id = _selectedExpenseId,   // 👈 اگه از دکمه‌ی «ویرایش» اومده باشه، همون رکورد آپدیت می‌شه؛ وگرنه جدید ثبت می‌شه
                    PartnerId = partnerId,
                    ExpenseTypeId = expenseTypeIdRaw == Guid.Empty ? (Guid?)null : expenseTypeIdRaw,
                    IsPayment = typeText == "پرداخت شریک (هزینه از جیب)",
                    Amount = amount,
                    DateCustom = string.IsNullOrWhiteSpace(dateText) ? DateTime.Now : dateText.ShamsiToMiladi() ?? DateTime.Now,
                    Description = description
                };

                await _partnerExpenseService.SaveAsync(dto);

                _selectedExpenseId = Guid.Empty;
                var keepPartnerId = partnerId;

                if (_currentFilterPartnerId != Guid.Empty)
                    await RefreshPartnerStatementAsync(_currentFilterPartnerId);
                else
                    await RefreshTransactionsAsync();

                ClassMessageBox.ShowMSG("هزینه ثبت شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);

                layExpenseInput.CallNew();
                layExpenseInput.SetValueType("تاریخ", DateTime.Now.DateTimePersian().Date);
                layExpenseInput.SetValueType("شریک", keepPartnerId);
            }
            catch (Exception ex)
            {
                ClassMessageBox.ShowMSG(ex.Message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
            }
            finally
            {
                layExpenseInput._disableAfterSave = false;
            }
        }

        // =========================================================================
        // تب ۴ — گزارش صندوق (چون Designer فاقد کنترل بود، مثل FrmMain.BuildDashboardPanel در کد ساخته می‌شود)
        // =========================================================================

        private void BuildReportPanel()
        {
            if (_reportPanelBuilt) return;
            _reportPanelBuilt = true;

            var btnRefresh = new SimpleButton { Text = "بروزرسانی", Dock = DockStyle.Right, Width = 120 };
            _clsFontBold.ChangeFont(btnRefresh);
            btnRefresh.Click += async (s, e) => await RefreshReportAsync();
            panel4.Controls.Add(btnRefresh);

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 130,
                ColumnCount = 2,
                RowCount = 3,
                RightToLeft = RightToLeft.Yes
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 3; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 3));

            AddReportRow(table, 0, "جمع کل فروش:", out lblTotalSalesValue, Color.FromArgb(0, 120, 0));
            AddReportRow(table, 1, "جمع کل خرید:", out lblTotalPurchaseValue, Color.FromArgb(180, 90, 0));
            AddReportRow(table, 2, "سود ناخالص:", out lblGrossProfitValue, Color.Black);

            tabReport.Controls.Add(table);
            table.BringToFront();
        }

        private void AddReportRow(TableLayoutPanel table, int row, string title, out LabelControl valueLabel, Color color)
        {
            var lblTitle = new LabelControl
            {
                Text = title,
                Dock = DockStyle.Fill,
                AutoSizeMode = LabelAutoSizeMode.None
            };
            lblTitle.Appearance.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            valueLabel = new LabelControl
            {
                Text = "...",
                Dock = DockStyle.Fill,
                AutoSizeMode = LabelAutoSizeMode.None
            };
            valueLabel.Appearance.Font = new Font("Samim FD", 13F, FontStyle.Bold);
            valueLabel.Appearance.ForeColor = color;
            valueLabel.Appearance.Options.UseForeColor = true;
            valueLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

            table.Controls.Add(lblTitle, 1, row);
            table.Controls.Add(valueLabel, 0, row);
        }

        private async Task RefreshReportAsync()
        {
            var report = await _cashRegisterService.GetProfitReportAsync();

            lblTotalSalesValue.Text = report.TotalSales.ToString("N0");
            lblTotalPurchaseValue.Text = report.TotalPurchases.ToString("N0");
            lblGrossProfitValue.Text = report.GrossProfit.ToString("N0");
            lblGrossProfitValue.Appearance.ForeColor = report.GrossProfit >= 0
                ? Color.FromArgb(0, 150, 0)
                : Color.FromArgb(255, 0, 0);
        }

        private void FrmCashRegister_Load(object sender, EventArgs e) { }


        #region محاسبه درآمد  // Tab 4

        DataTable _dtDoc;
        List<CashDocumentDto> _cashDocSell, _cashDocBuy;
        private async Task SetupDocGridStructure()
        {
            if (dgvDoc.ColumnCount() > 0)
                return;

            await dgvDoc.SetStyle();
            _clsFontBold.ChangeFont(dgvDoc);
            _clsFontBold.ChangeFont(label1);
            _clsFontBold.ChangeFont(lblDocStatus);
            _clsFontBold.ChangeFont(lblCalcDoc);

            _dtDoc = dgvDoc.GridStructure([
                new() { Name = "Id", Type = typeof(Guid) },
                new() { Name = "شماره سند", Type = typeof(long) },
                new() { Name = "تعداد فاکتور", Type = typeof(int) },
                new() { Name = "مبلغ کل سند", Type = typeof(long), PriceActive = true },
                new() { Name = "تاریخ", Type = typeof(string) },
                new() { Name = "نوع", Type = typeof(string) },
                new() { Name = "وضعیت تسویه", Type = typeof(string) },
            ], false, true, true);


            #region Event

            dgvDoc.GetViewBase.RowCellStyle += (s1, e1) =>
            {
                var getClm = e1.Column.FieldName;
                if (getClm == "وضعیت تسویه")
                {
                    var valueObj = dgvDoc.GetViewBase.GetRowCellValue(e1.RowHandle, getClm);
                    e1.Appearance.ForeColor = (string)valueObj == "تسویه شده" ? ClsUI.Ok : ClsUI.NotOk;

                }

            };

            #endregion

            dgvDoc.ActiveScrollGrid();
            dgvDoc.HiddenColumn("Id");
        }

        public async Task RefreshDocGridAsync()
        {

            // پر کردن DataTable dgvDoc
            splitContainer1.Panel1.WaitDownPage(() =>
            {
                if (_dtDoc == null)
                    return;

                _dtDoc.Rows.Clear();

                // افزودن اسناد فروش
                foreach (var doc in _cashDocSell)
                {
                    _dtDoc.Rows.Add(
                        doc.Id,
                        doc.Code,
                        doc.FactorCount,
                        doc.TotalAmount,
                        doc.DateDocument.DateTimePersian().Date,
                        "فروش",

                        doc.IsSettled ? "تسویه شده" : "تسویه نشده"
                    //cmbSaleDocument.Text
                    );
                }

                // افزودن اسناد خرید
                foreach (var doc in _cashDocBuy)
                {
                    _dtDoc.Rows.Add(
                        doc.Id,
                        doc.Code,
                        doc.FactorCount,
                        doc.TotalAmount,
                        doc.DateDocument.DateTimePersian().Date,
                        "خرید",
                        doc.IsSettled ? "تسویه شده" : "تسویه نشده"
                    );
                }

                dgvDoc.SetFieldSizeColumn();


                long _sumSales = 0, _sumBuy = 0;
                foreach (DataRow row in _dtDoc.Rows)
                {
                    var getType = row["نوع"];
                    if ((string)getType == "خرید")
                    {
                        _sumSales += row["مبلغ کل سند"].GetNum<long>();
                    }
                    if ((string)getType == "فروش")
                    {
                        _sumBuy += row["مبلغ کل سند"].GetNum<long>();
                    }
                }

                #endregion
                lblCalcDoc.Text = (_sumBuy - _sumSales).ToString("N0");
            });
        }
        private async void cmbSaleDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            await SetupDocGridStructure();
            if (cmbSaleDocument.Text == @"تسویه شده")
            {
                _cashDocSell = await _cashRegisterService.GetDocumentsAsync(true, true);
                _cashDocBuy = await _cashRegisterService.GetDocumentsAsync(false, true);
            }
            else if (cmbSaleDocument.Text == @"تسویه نشده")
            {
                _cashDocSell = await _cashRegisterService.GetDocumentsAsync(true, false);
                _cashDocBuy = await _cashRegisterService.GetDocumentsAsync(false, false);
            }
            else if (cmbSaleDocument.Text == @"همه")
            {
                _cashDocSell = await _cashRegisterService.GetDocumentsAsync(true);
                _cashDocBuy = await _cashRegisterService.GetDocumentsAsync(false);
            }
            await RefreshDocGridAsync();
            #region Calc



          
         


        }
        private async void btnDocFinal_Click(object sender, EventArgs e)
        {
            if (_dtDoc == null)
            {
                ClassMessageBox.ShowMSG("سندی یافت نشد !", Class_Text.Msg_Name, ClassMessageBox.enumIcon.بلندگو);
                return;
            }
        
            List<Guid> tempList = new List<Guid>();

            for (int i = 0; i < _dtDoc.Rows.Count; i++)
            {
                DataRow row = _dtDoc.Rows[i];

                bool isSettled = (string)row["وضعیت تسویه"] == "تسویه شده";

                Guid idValue = row["Id"].ToGuid();
                if (!isSettled)
                    tempList.Add(idValue);
            }
            if (tempList.Count == 0)
            {
                ClassMessageBox.ShowMSG("سندی یافت نشد !", Class_Text.Msg_Name, ClassMessageBox.enumIcon.بلندگو);
                return;
            }
            Guid[] ids = tempList.ToArray();


            var getAccept = ClassMessageBox.ShowMSGQues("بعد از ثبت سند محاسبه، دیگر مجاز به ویرایش فاکتور ها و اسناد نمی‌باشید.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
            if (!getAccept) return;
            getAccept = ClassMessageBox.ShowMSGQues("ادامه می دهید؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
            if (!getAccept) return;

            await _cashRegisterService.SettleDocumentAsync(ids);
            cmbSaleDocument.Text = @"همه";
            await RefreshDocGridAsync();

        }

        #region DGV Doc Calc

        DataTable _dtDocCalc;

        private async Task SetupDocCalcGridStructure()
     
        {
            if (dgvDocCalc.ColumnCount() > 0)
                return;
            await dgvDocCalc.SetStyle();
            _clsFontBold.ChangeFont(dgvDocCalc);
            //_clsFontBold.ChangeFont(label2);
            //_clsFontBold.ChangeFont(lblDocStatusCalc);
            _clsFontBold.ChangeFont(lblCalcDoc);
            _dtDocCalc = dgvDocCalc.GridStructure([
                new() { Name = "Id", Type = typeof(Guid) },
                new() { Name = "شماره سند", Type = typeof(long) },
                new() { Name = "تعداد سند", Type = typeof(int) },
                new() { Name = "مبلغ کل حرید", Type = typeof(long), PriceActive = true },
                new() { Name = "مبلغ کل فروش", Type = typeof(long), PriceActive = true },
                new() { Name = "محاسبه", Type = typeof(long), PriceActive = true },
                new() { Name = "تاریخ", Type = typeof(string) },
                //new() { Name = "نوع", Type = typeof(string) },
                //new() { Name = "وضعیت تسویه", Type = typeof(string) },
            ], false, true, true);
            dgvDocCalc.ActiveScrollGrid();
            dgvDocCalc.HiddenColumn(new List<string> { "Id" });

            #region Event



            #endregion


        }


        private async Task RefreshDocCalcGridAsync()
        {

            var items =await _cashRegisterService.GetSettlementRecordsAsync();

            _dtDocCalc.Rows.Clear();

        
            foreach (var f in items)
            {
                _dtDocCalc.Rows.Add(f.Id,
                    f.Code,
                    f.DocumentCount,
                    f.TotalSales,
                    f.TotalPurchases,
                    f.Profit,
                    f.DateSettlement.DateTimePersian().Date);
            }

          
        }

        #endregion


        #endregion


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDoc_Click(object sender, EventArgs e)
        {

        }
    }
}