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

namespace Kavosh.UI.Forms
{
    public partial class FrmFactor : DevExpress.XtraEditors.XtraForm
    {
        private readonly FactorHeaderService _factorHeaderService;
        private readonly PersonService _personService;
        private readonly ProductService _productService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private Guid _selectedFactorId = Guid.Empty;
        private DataTable _dtFactorDetail;

        public FrmFactor(FactorHeaderService factorHeaderService,
                          PersonService personService,
                          ProductService productService)
        {
            InitializeComponent();
            _factorHeaderService = factorHeaderService;
            _personService = personService;
            _productService = productService;
            Shown += FrmFactor_Shown;
        }

        private async void FrmFactor_Shown(object sender, EventArgs e)
        {
            await Task.WhenAll(
                SetStyle(),
                SetFieldLayInput()
            );

            // این دو تا باید بعد از layInput اجرا بشن چون به فیلدهاش وابسته‌ان
            await SetFieldDgvFactorDetail();
            await PrepareNewFactor();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvFactorDetail);
            await dgvFactorDetail.SetStyle();
        }

        // ============= هدر فاکتور (سمت راست) =============
        public async Task SetFieldLayInput()
        {
            layInput.RightToLeft = RightToLeft.Yes;
            pnlFunction.Controls.Add(layInput.ShowPanelOperation());
            layInput.AddButtonOperation();

            var txtId = ClsCollect.ModelTextEdit("Id", 50, "");
            var txtCode = ClsCollect.ModelTextEditNumber("کد فاکتور", 50, "");

            // طرف حساب (مشتری) — همون الگوی cmbGroup توی FrmProduct
            var getPersons = (await _personService.GetAllPersonsAsync())
                .Select(p => new { p.Id, p.FullName }).ToList();
            var cmbPerson = ClsCollect.ModelGridToDataLayoutBtn("طرف حساب", getPersons, "Id", "FullName", "");
            cmbPerson.ConvertGroupToGrid().HiddenColumn("Id");

            //var cmbType = ClsCollect.ModelComboBox("نوع فاکتور", new[] { "خرید", "فروش" }, "");
            //var cmbType = ClsCollect.ModelGridToDataLayout("نوع فاکتور", new List<ClsCollect.modelDataTable>
            //{
            //    new() { Code = 1, Title = "فروش" },
            //    new() { Code = 2, Title = "خرید" },
            //}, "");

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
            ], 13);

            layInput.BtnCancelClick += LayInput_BtnCancelClick;
            layInput.BtnSaveClick += LayInput_BtnSaveClick;
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
                    new() { Name = "قیمت واحد", Type = typeof(long) },
                    new() { Name = "جمع", Type = typeof(long) },
                ], true, false, false);

                dgvFactorDetail.ActiveScrollGrid();
                dgvFactorDetail.HiddenColumn("Id");
                dgvFactorDetail.MaxMinWidth("حذف", 45, 45);
                dgvFactorDetail.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);

                #region Relation - انتخاب محصول از لیست محصولات

                var getProducts = (await _productService.GetAllProductsAsync()).Select(s => new { s.Id, s.Title, s.SellPrice }).ToList();
                var cmbProduct = dgvFactorDetail.AddGridToGrid(getProducts, "محصول", "Id", "Title", select =>
                {
                    var getSellPrice = getProducts.First(f => f.Id == select.Id).SellPrice;
                    dgvFactorDetail.SetValue("قیمت واحد", getSellPrice);



                });
                cmbProduct.HiddenColumn("Id");
                cmbProduct.HiddenColumn("SellPrice");

      
                #endregion

                #region Event

                dgvFactorDetail.AddEventRowCellClick<Guid>(id =>
                {
                    dgvFactorDetail.DeleteRow(true, () => { /* فقط از گرید حذف میشه، ذخیره نهایی موقع Save انجام میشه */ });
                }, "Id", "حذف");

                // TODO: پر کردن خودکار «قیمت واحد» بعد از انتخاب «محصول» + محاسبه‌ی زنده‌ی «جمع»
                // با الگوی اختصاصی خودمون (مثلاً روی GetViewBase.CellValueChanged یا ValidatingEditor)
                // اینجا بعداً تکمیل میشه — فعلاً «قیمت واحد» رو کاربر باید دستی وارد کنه.
                // SetupAutoFillPriceAndTotal();

                #endregion
            }
        }

        // ============= آماده‌سازی رکورد جدید =============
        private async Task PrepareNewFactor()
        {
            _selectedFactorId = Guid.Empty;
            layInput.CallNew();
            _dtFactorDetail.Rows.Clear();

            var nextCode = await _factorHeaderService.GetNextCodeAsync();
            layInput.SetValueType("کد فاکتور", nextCode);
            layInput.SetValueType("تاریخ", DateTime.Now);
        }

        // ============= بارگذاری فاکتور موجود برای ویرایش =============
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
            layInput.SetValueType("تاریخ", dto.DateFactor);
            layInput.SetValueType("تخفیف", dto.Discount);

            _dtFactorDetail.Rows.Clear();
            foreach (var d in dto.Details)
            {
                _dtFactorDetail.Rows.Add(d.Id, "حذف", d.ProductId, d.Count, d.PriceUnit, d.LineTotal);
            }
            dgvFactorDetail.SetFieldSizeColumn();
        }

        // ============= ذخیره (هدر + خط‌های محصول با هم) =============
        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = true;
            try
            {
                var dto = new FactorHeaderDto
                {
                    Id = _selectedFactorId,
                    Code = layInput.GetValue<long>("کد فاکتور"),
                    PersonId = layInput.GetValue<Guid>("طرف حساب"),
                    Type = layInput.GetValue<string>("نوع فاکتور") == "فروش",
                    DateFactor = layInput.GetValue<DateTime>("تاریخ"),
                    Discount = layInput.GetValue<long>("تخفیف"),
                    Details = _dtFactorDetail.Rows
                        .Cast<DataRow>()
                        .Where(r => r.RowState != DataRowState.Deleted)
                        .Select(r => new FactorDetailDto
                        {
                            Id = r["Id"] is Guid gid ? gid : Guid.Empty,
                            ProductId = (Guid)r["محصول"],
                            Count = Convert.ToSingle(r["تعداد"]),
                            PriceUnit = Convert.ToInt64(r["قیمت واحد"])
                        }).ToList()
                };

                var savedId = await _factorHeaderService.SaveFactorAsync(dto);
                _selectedFactorId = savedId;

                await PrepareNewFactor(); // آماده برای فاکتور بعدی
                ClassMessageBox.ShowMSG("فاکتور ذخیره شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                ClassMessageBox.ShowMSG(message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
            }
            finally
            {
                layInput._disableAfterSave = false;
            }
        }

        private async void LayInput_BtnCancelClick(object sender, EventArgs e)
        {
            await PrepareNewFactor();
        }

        private void FrmFactor_Load(object sender, EventArgs e) { }
    }
}