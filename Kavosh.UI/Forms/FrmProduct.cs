using DevExpress.Utils.Html.Internal;
using DevExpress.XtraEditors;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kavosh.UI.Forms
{
    public partial class FrmProduct : DevExpress.XtraEditors.XtraForm
    {
        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);
        private readonly ProductService _productService;
        private readonly ProductGroupService _productGroupService;
        private readonly ProductUnitService _productUnitService;
        private Guid _selectedProductId = Guid.Empty; // خالی یعنی رکورد جدید
        public FrmProduct(ProductService productService,
            ProductGroupService productGroupService,   // 👈 پارامتر
            ProductUnitService productUnitService)
        {
            InitializeComponent();
            _productService = productService;
            _productGroupService = productGroupService;   // 👈 مقداردهی
            _productUnitService = productUnitService;
            Shown += FrmMain_Shown;
        }

        private async void FrmMain_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            await SetFieldLayInput();
            await SetFieldDgvProduct();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvProduct);
            await dgvProduct.SetStyle();
        }


        private DataTable _dtProduct;


        public async Task SetFieldDgvProduct()
        {
            splitContainerControl1.Panel1.WaitDownPage(async () =>
            {
                if (dgvProduct.ColumnCount() == 0)
                {
                    _dtProduct = dgvProduct.GridStructure([
                        new() { Name = "Id", Type = typeof(Guid) },
                        new() { Name = "کاردکس", Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.view},
                        new() { Name = "حذف", Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.delete },
                        new() { Name = "ویرایش", Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.edit},
                        new() { Name = "کد محصول", Type = typeof(long) },
                        new() { Name = "گروه محصول", Type = typeof(Guid) },
                        new() { Name = "نام محصول", Type = typeof(string) },
                        new() { Name = "اولیه", Type = typeof(float) },
                        new() { Name = "ورودی", Type = typeof(float) },
                        new() { Name = "خروجی", Type = typeof(float) },
                        new() { Name = "فعلی", Type = typeof(float) },
                    ], false, true, true);
                    dgvProduct.ActiveScrollGrid();

                    dgvProduct.HiddenColumn(["Id"]);
                    dgvProduct.MaxMinWidth("کاردکس", 40, 40);
                    dgvProduct.MaxMinWidth("حذف", 40, 40);
                    dgvProduct.MaxMinWidth("ویرایش", 40, 40);

                    #region Relation

                    var getProductGroup = (await _productGroupService.GetAllAsync());
                    var cmbProductGroup = dgvProduct.AddGridToGrid(getProductGroup, "گروه محصول", "Id", "Title");
                    cmbProductGroup.HiddenColumn("Id");

                    #endregion

                    #region Event

                    dgvProduct.AddEventRowCellClick<Guid>(value =>
                    {

                    }, "Id", "کاردکس");

                    dgvProduct.AddEventRowCellClick<Guid>(async value =>
                    {

                        await GetForEdit(value);

                    }, "Id", "ویرایش");

                    dgvProduct.AddEventRowCellClick<Guid>(value =>
                    {

                    }, "Id", "حذف");

                    #endregion
                }

                #region Load Data
                _dtProduct.Rows.Clear();
                var getData = await _productService.GetAllProductsAsync();

                foreach (var i in getData)
                {
                    _dtProduct.Rows.Add(i.Id, "", "", "", i.ProductCode, i.ProductGroupId, i.Title, i.InitialInventory, i.InputStock, i.OutputStock, (i.InputStock + i.InitialInventory) - i.OutputStock);
                }
                dgvProduct.SetFieldSizeColumn();
                #endregion
            });

        }

        public async Task GetForEdit(Guid id)
        {
            splitContainerControl1.Panel2.WaitDownPage(async () =>
            {
                var getFirst = await _productService.GetProductByIdAsync(id);
                layInput.CallNew();
                await Task.Delay(200);
                _selectedProductId = getFirst.Id;
                layInput.SetValueType("Id", getFirst.Id);
                layInput.SetValueType("کد محصول", getFirst.ProductCode);
                layInput.SetValueType("گروه محصول", getFirst.ProductGroupId);
                layInput.SetValueType("نام محصول", getFirst.Title);
                layInput.SetValueType("نام محصول", getFirst.InitialInventory);


            });
        }
        public async Task SetFieldLayInput()
        {

            splitContainerControl1.Panel2.WaitDownPage(async () =>
            {
                layInput.RightToLeft = RightToLeft.Yes;
                pnlFunction.Controls.Add(layInput.ShowPanelOperation());
                layInput.AddButtonOperation();

                var txtId = ClsCollect.ModelTextEdit("Id", 50, "");
                var txtCode = ClsCollect.ModelTextEditNumber("کد محصول", 50, "");

                var getProductGroup = await _productGroupService.GetAllAsync();
                var cmbGroup = ClsCollect.ModelGridToDataLayout("گروه محصول", getProductGroup, "Id", "Title", "");
                cmbGroup.HiddenColumn("Id");

                var txtName = ClsCollect.ModelTextEdit("نام محصول", 100, "");

                var getProductUnit= await _productUnitService.GetAllAsync();
                var cmbUnit = ClsCollect.ModelGridToDataLayout("سنجش", getProductUnit, "Id", "Title", "");
                cmbUnit.HiddenColumn("Id");

                var txtMojidi = ClsCollect.ModelTextEdit("موجودی اولیه", 50, "");
                var txtPriceSell = ClsCollect.ModelTextEdit("قیمت فروش", 50, "");

                layInput.SetFieldColumnDataLayout(true, 1, [
                    new() { Grp = 1, Ctrl = txtId, },
                    new() { Grp = 1, Ctrl = txtCode, },
                    new() { Grp = 1, Ctrl = cmbGroup, },
                    new() { Grp = 1, Ctrl = txtName, },
                    new() { Grp = 1, Ctrl = cmbUnit, },
                    new() { Grp = 1, Ctrl = txtMojidi, },
                    new() { Grp = 1, Ctrl = txtPriceSell, },

                ]);

                layInput.BtnCancelClick += LayInput_BtnCancelClick;
                layInput.BtnSaveClick += LayInput_BtnSaveClick;
            });
        }

        private void LayInput_BtnCancelClick(object sender, EventArgs e)
        {

        }

        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = true;
            try
            {
                var dto = new ProductDto
                {
                    Id = _selectedProductId,
                    ProductCode = layInput.GetValue<long>("کد محصول"),
                    Title = layInput.GetValue<string>("نام محصول"),
                    //ProductGroupId = layInput.GetValue<Guid>("گروه محصول"),
                    ProductUnitId = Guid.Parse("e0fbeaa9-bbbe-44f5-af24-c69ce2d1136d"),
                    ProductGroupId = Guid.Parse("c560bc27-a69d-45c3-9fd2-4b3b17acb5ba"),
                    //ProductUnitId = layInput.GetValue<Guid>("سنجش"),
                    InitialInventory = layInput.GetValue<float>("موجودی اولیه"),
                    SellPrice = layInput.GetValue<long>("قیمت فروش"),
                };

                var savedId = await _productService.SaveProductAsync(dto);
                _selectedProductId = savedId;

                await SetFieldDgvProduct(); // رفرش گرید
                ClassMessageBox.ShowMSG("اطلاعات ذخیره شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
                layInput._disableAfterSave = false;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                ClassMessageBox.ShowMSG(message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
            }
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {

        }
    }
}