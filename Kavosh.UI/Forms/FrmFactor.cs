using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;

namespace Kavosh.UI.Forms
{
    public partial class FrmFactor : DevExpress.XtraEditors.XtraForm
    {
        private readonly FactorHeaderService _factorHeaderService;
        private readonly PersonService _personService;
        private readonly ProductService _productService;
        private readonly PaymentTypeService _paymentTypeService;   // 👈 جدید

        public Guid? FactorIdToEdit;   // 👈 این خط رو اضافه کنید

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private Guid _selectedFactorId = Guid.Empty;
        private DataTable _dtFactorDetail;
        private DataTable _dtHowToPay;   // 👈 جدید
        public FrmFactor(FactorHeaderService factorHeaderService,
                          PersonService personService,
                          ProductService productService,
                          PaymentTypeService paymentTypeService)
        {
            InitializeComponent();
            _factorHeaderService = factorHeaderService;
            _personService = personService;
            _productService = productService;
            _paymentTypeService = paymentTypeService;
            Shown += FrmFactor_Shown;
        }

        private async void FrmFactor_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            await SetFieldLayInput();
            await SetFieldDgvFactorDetail();
            await SetFieldDgvHowToPay();
            if (FactorIdToEdit.HasValue)
                await LoadFactorToForm(FactorIdToEdit.Value);
            else
                await PrepareNewFactor();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvFactorDetail);
            _clsFontBold.ChangeFont(dgvHowToPay);
            await dgvFactorDetail.SetStyle();
            await dgvHowToPay.SetStyle();
        }

        // ============= هدر فاکتور (سمت راست) =============
        public async Task SetFieldLayInput()
        {
            layInput.RightToLeft = RightToLeft.Yes;
            pnlFunction.Controls.Add(layInput.ShowPanelOperation());
            layInput.AddButtonOperation();

            var txtId = ClsCollect.ModelTextEdit("Id", 50, "");
            var txtCode = ClsCollect.ModelTextEditNumber("کد فاکتور", 50, "");
            var txtMalyat1 = ClsCollect.ModelTextEditPrice("مالیات 1", 2, "", true, "درصد");
            var txtMalyat2 = ClsCollect.ModelTextEditPrice("مالیات 2", 2, "", true, "درصد");
            txtMalyat1.TextChanged += (s1, e1) => { dgvFactorDetail.GetViewBase.UpdateSummary(); };
            txtMalyat2.TextChanged += (s1, e1) => { dgvFactorDetail.GetViewBase.UpdateSummary(); };
            // طرف حساب (مشتری) — همون الگوی cmbGroup توی FrmProduct
            var getPersons = (await _personService.GetAllPersonsAsync())
                .Select(p => new { p.Id, p.FullName }).ToList();
            var cmbPerson = ClsCollect.ModelGridToDataLayoutBtn("طرف حساب", getPersons, "Id", "FullName", "");
            cmbPerson.ConvertGroupToGrid().HiddenColumn("Id");

            var cmbType = ClsCollect.ModelRadioGroup("نوع فاکتور", new List<ClsCollect.modelRadioGroup>{
                new() { Column = 1,Field = "فروش" },
                new() { Column = 2,Field = "خرید" },
            });

            var dtFactor = ClsCollect.ModelDateTime("تاریخ", 10, "");
            var txtDiscount = ClsCollect.ModelTextEditPrice("تخفیف", 10, "");

            layInput.SetFieldColumnDataLayout(true, 1, [
                new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
                new() { Grp = 1, Ctrl = txtCode, },
                new() { Grp = 1, Ctrl = cmbPerson, SizeType = SizeConstraintsType.Custom, AutoHeight = 38 },
                new() { Grp = 1, Ctrl = cmbType, SizeType = SizeConstraintsType.Custom, AutoHeight = 38  },
                new() { Grp = 1, Ctrl = dtFactor, },
                new() { Grp = 1, Ctrl = txtDiscount, },
                new() { Grp = 1, Ctrl = txtMalyat1, },
                new() { Grp = 1, Ctrl = txtMalyat2, },
            ], 13);

            layInput.BtnCancelClick += LayInput_BtnCancelClick;
            layInput.BtnSaveClick += LayInput_BtnSaveClick;
            layInput.BtnNewClick += LayInput_BtnNewClick; ;
        }




        // ============= خط‌های محصول (سمت چپ، قابل ویرایش) =============
        public async Task SetFieldDgvFactorDetail()
        {
            if (dgvFactorDetail.ColumnCount() == 0)
            {
                _dtFactorDetail = dgvFactorDetail.GridStructure([
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                    new() { Name = "محصول", Type = typeof(Guid) },
                    new() { Name = "تعداد", Type = typeof(float) },
                    new() { Name = "قیمت واحد", Type = typeof(long),PriceActive = true},   // مبلغ خرید - اطلاعاتی
                    new() { Name = "قیمت فروش", Type = typeof(long),PriceActive = true},   // 👈 جدید - قابل ویرایش، مبنای جمع و چاپ
                    new() { Name = "جمع", Type = typeof(long),PriceActive = true },
                ], true, false, true);

                dgvFactorDetail.ActiveScrollGrid();
                dgvFactorDetail.HiddenColumn("Id");
                dgvFactorDetail.MaxMinWidth("حذف", 45, 45);
                dgvFactorDetail.MaxMinWidth("جمع", 155, 155);
                dgvFactorDetail.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);
                dgvFactorDetail.AddSummaryItem("جمع", "جمع", "", SummaryItemType.Custom);
                #region Relation - انتخاب محصول از لیست محصولات

                // 👇 UnitPrice (مبلغ خرید) هم علاوه بر SellPrice (مبلغ فروش) گرفته می‌شود
                var getProducts = (await _productService.GetAllProductsAsync())
                    .Select(s => new { s.Id, s.Title, s.SellPrice, s.UnitPrice }).ToList();

                var cmbProduct = dgvFactorDetail.AddGridToGrid(getProducts, "محصول", "Id", "Title", select =>
                {
                    var getCount = dgvFactorDetail.GetValue<long>("تعداد");
                    var product = getProducts.First(f => f.Id == select.Id);

                    dgvFactorDetail.SetValue("قیمت واحد", product.UnitPrice);   // مبلغ خرید - فقط اطلاعاتی
                    dgvFactorDetail.SetValue("قیمت فروش", product.SellPrice);   // مبلغ فروش - پیش‌فرض، قابل تغییر توسط کاربر
                    dgvFactorDetail.SetValue("جمع", product.SellPrice * getCount);
                });
                cmbProduct.HiddenColumn("Id");
                cmbProduct.HiddenColumn("SellPrice");
                cmbProduct.HiddenColumn("UnitPrice");


                #endregion

                #region Event

                double sumTotal = 0, getMalyat1;
                dgvFactorDetail.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                {
                    var ConE = e1.ConvertItemSummary();
                    dgvFactorDetail.AutoSummaryCalculate(e1, "جمع", "جمع", ref sumTotal, "تومان");
                    var getMalyat1 = sumTotal * layInput.GetValue<long>("مالیات 1") / 100;
                    var getMalyat2 = sumTotal * layInput.GetValue<long>("مالیات 2") / 100;
                    if (ConE.FieldName == "جمع")
                    {
                        e1.TotalValue = "جمع کل: "+(getMalyat1 + getMalyat2 + sumTotal).ToString("N0");
                    }

                    // dgvFactorDetail.GetViewBase.UpdateSummary();
                };


                dgvFactorDetail.GetViewBase.CellValueChanged += (s1, e1) =>
                {
                    // 👇 جمع فقط بر اساس «تعداد» و «قیمت فروش» محاسبه می‌شود (نه قیمت واحد/خرید)
                    if (e1.Column.FieldName != "قیمت فروش" && e1.Column.FieldName != "تعداد")
                        return;

                    long getSellPrice = e1.Column.FieldName == "قیمت فروش"
                        ? e1.Value.GetNum<long>()
                        : dgvFactorDetail.GetValue<long>("قیمت فروش");

                    long getCount = e1.Column.FieldName == "تعداد"
                        ? e1.Value.GetNum<long>()
                        : dgvFactorDetail.GetValue<long>("تعداد");

                    dgvFactorDetail.SetValue("جمع", getSellPrice * getCount);

                };

                dgvFactorDetail.AddEventRowCellClick<Guid>(id =>
                {
                    dgvFactorDetail.DeleteRow(false);
                }, "Id", "حذف");

                #endregion
            }
        }

        // ============= آماده‌سازی رکورد جدید =============
        private async Task PrepareNewFactor()
        {
            layInput.WaitDownPage(async () =>
            {
                _selectedFactorId = Guid.Empty;
                layInput.CallNew();
                if (_dtFactorDetail is { Columns.Count: > 0 })
                    _dtFactorDetail.Rows.Clear();

                if (_dtHowToPay is { Columns.Count: > 0 })
                    _dtHowToPay.Rows.Clear(); // 👈 جدید

                var nextCode = await _factorHeaderService.GetNextCodeAsync();
                layInput.SetValueType("کد فاکتور", nextCode);
                layInput.SetValueType("تاریخ", DateTime.Now.DateTimePersian().Date);
            });

        }

        // ============= بارگذاری فاکتور موجود =============
        public async Task LoadFactorToForm(Guid id)
        {
            var dto = await _factorHeaderService.GetFactorByIdAsync(id);
            if (dto is null) return;

            _selectedFactorId = dto.Id;
            layInput.CallNew();
            await Task.Delay(200);

            layInput.SetValueType("Id", dto.Id);
            layInput.SetValueType("کد فاکتور", dto.Code);
            layInput.SetValueType("طرف حساب", dto.PersonId);
            layInput.SetValueType("نوع فاکتور", dto.Type ? "فروش" : "خرید");
            layInput.SetValueType("تاریخ", dto.DateFactor.DateTimePersian().Date);
            layInput.SetValueType("تخفیف", dto.Discount);
            layInput.SetValueType("مالیات 1", dto.Malyat1);
            layInput.SetValueType("مالیات 2", dto.Malyat2);

            _dtFactorDetail.Rows.Clear();
            foreach (var d in dto.Details)
            {
                _dtFactorDetail.Rows.Add(d.Id, "حذف", d.ProductId, d.Count, d.PriceUnit, d.SellPrice, d.LineTotal);
            }
            dgvFactorDetail.SetFieldSizeColumn();

            // 👇 جدید
            _dtHowToPay.Rows.Clear();
            foreach (var p in dto.HowToPays)
            {
                _dtHowToPay.Rows.Add(
                    p.Id, "حذف", p.PaymentTypeId, p.Price, p.CheckNumber,
                    p.CheckDate == null ? DBNull.Value : p.CheckDate.Value.DateTimePersian().Date,   // 👈 اگه null بود، DBNull میره تو ستون
                    p.Settlement, p.Description
                );
            }
            dgvHowToPay.SetFieldSizeColumn();
        }

        // ============= ذخیره (هدر + کالا + پرداخت با هم) =============
        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = true;
            var dto = new FactorHeaderDto
            {
                Id = _selectedFactorId,
                Code = layInput.GetValue<long>("کد فاکتور"),
                PersonId = layInput.GetValue<Guid>("طرف حساب"),
                Type = layInput.GetValue<string>("نوع فاکتور") == "فروش",
                DateFactor = layInput.GetValue<string>("تاریخ").ShamsiToMiladi().Value,
                Discount = layInput.GetValue<long>("تخفیف"),
                Malyat1 = layInput.GetValue<long>("مالیات 1"),
                Malyat2 = layInput.GetValue<long>("مالیات 2"),


                Details = _dtFactorDetail.Rows
                    .Cast<DataRow>()
                    .Where(r => r.RowState != DataRowState.Deleted)
                    .Where(r => r["محصول"] != DBNull.Value && r["محصول"] is Guid pid && pid != Guid.Empty)
                    .Select(r => new FactorDetailDto
                    {
                        Id = r["Id"] is Guid gid ? gid : Guid.Empty,
                        ProductId = (Guid)r["محصول"],
                        Count = Convert.ToSingle(r["تعداد"]),
                        PriceUnit = Convert.ToInt64(r["قیمت واحد"]),
                        SellPrice = Convert.ToInt64(r["قیمت فروش"])   // 👈 جدید
                    }).ToList(),

                // 👇 جدید: همون فیلتر ردیف خالی، این‌بار برای گرید پرداخت
                HowToPays = _dtHowToPay.Rows
                    .Cast<DataRow>()
                    .Where(r => r.RowState != DataRowState.Deleted)
                    .Where(r => r["نوع پرداخت"] != DBNull.Value && r["نوع پرداخت"] is Guid tid && tid != Guid.Empty)
                    .Select(r =>
                    {
                        var checkDate = r["تاریخ چک"].ToString().ShamsiToMiladi();
                        return new HowToPayDto
                        {
                            Id = r["Id"] is Guid gid ? gid : Guid.Empty,
                            PaymentTypeId = (Guid)r["نوع پرداخت"],
                            Price = Convert.ToInt64(r["مبلغ"]),
                            CheckNumber = r["شماره چک"] as string,
                            CheckDate = checkDate, // 👈 اصلاح شد
                            Settlement = r["تسویه"] != DBNull.Value && Convert.ToBoolean(r["تسویه"]),
                            Description = r["توضیحات"] as string
                        };
                    }).ToList()
            };

            var savedId = await _factorHeaderService.SaveFactorAsync(dto);
            _selectedFactorId = savedId;

            await PrepareNewFactor();

            Kavosh.Services.AppEvents.RaiseDataChanged(); // 👈 اضافه شد

            ClassMessageBox.ShowMSG("فاکتور ذخیره شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
            layInput._disableAfterSave = false;
        }

        private async void LayInput_BtnCancelClick(object sender, EventArgs e)
        {

        }
        private async void LayInput_BtnNewClick(object sender, EventArgs e)
        {
            await PrepareNewFactor();
        }
        private void FrmFactor_Load(object sender, EventArgs e) { }
        // ============= گرید نحوه‌ی پرداخت (پایین، کل عرض) =============
        public async Task SetFieldDgvHowToPay()
        {
            if (dgvHowToPay.ColumnCount() == 0)
            {
                _dtHowToPay = dgvHowToPay.GridStructure([
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "حذف", Action = DeleteRowHotToPay,Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                    new() { Name = "نوع پرداخت", Type = typeof(Guid) },
                    new() { Name = "مبلغ", Type = typeof(long), PriceActive = true },
                    new() { Name = "شماره چک", Type = typeof(string) },
                    new() { Name = "تاریخ چک",Action = EventDatePanel, Object = KavoshGrid.enumObject.PnlDate, ImageValue = MyCom.Properties.Resources.adateoccuring },
                    new() { Name = "تسویه", Type = typeof(bool),Object = KavoshGrid.enumObject.Checked},
                    new() { Name = "توضیحات", Type = typeof(string) },
                ], true, false, true);

                dgvHowToPay.ActiveScrollGrid();
                dgvHowToPay.HiddenColumn("Id");
                dgvHowToPay.MaxMinWidth("حذف", 45, 45);
                dgvHowToPay.MaxMinWidth("مبلغ", 155, 155);
                dgvHowToPay.MaxMinWidth("تاریخ چک", 100, 100);
                dgvHowToPay.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);
                dgvHowToPay.AddSummaryItem("مبلغ", "مبلغ", "", SummaryItemType.Custom);
                #region Relation - انتخاب نوع پرداخت

                var getPaymentTypes = await _paymentTypeService.GetAllAsync();
                var cmbPaymentType = dgvHowToPay.AddGridToGrid(getPaymentTypes, "نوع پرداخت", "Id", "Title");
                cmbPaymentType.HiddenColumn("Id");

                #endregion

                #region Event
                double sumTotal = 0;
                dgvHowToPay.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                {
                    dgvHowToPay.AutoSummaryCalculate(e1, "مبلغ", "جمع", ref sumTotal, "تومان");
                };


                dgvHowToPay.GetViewBase.InitNewRow += (s1, e1) =>
                {
                    dgvHowToPay.SetValue("Id", Guid.NewGuid());
                };

                #endregion
            }
        }



        private void DeleteRowHotToPay(object o)
        {
            dgvHowToPay.DeleteRow(false);
        }

        private void EventDatePanel(object obj)
        {
            var dt = obj as string;
            dgvHowToPay.SetValue("تاریخ چک", dt);

        }


    }
}