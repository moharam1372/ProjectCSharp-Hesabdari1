using DevExpress.Utils;
using DevExpress.Utils.Html.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Kavosh.Domain.Entities;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Form_Portable;
using MyCom.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using static MyCom.Form_Portable.FrmPortable;

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

            //await Task.WhenAll(
            //    SetStyle(),
            //    SetFieldLayInput(),
            //    SetFieldDgvProduct()
            //);

            //try
            //{
            await SetStyle();
            await SetFieldLayInput();
            await SetFieldDgvProduct();
            //}
            //catch (Exception e1)
            //{

            //}
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvProduct);
            _clsFontBold.ChangeFont(srcGrid, 15);
            await dgvProduct.SetStyle();
        }


        private DataTable _dtProduct;


        public Task SetFieldDgvProduct()
        {
            splitContainerControl1.Panel1.WaitDownPage(async void () =>
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
                        new() { Name = "اولیه", Type = typeof(float) ,FormatType = FormatType.Custom,FormatString = ClsCollect.FormatStringNegativeNumber(1)},
                        new() { Name = "ورودی", Type = typeof(float) ,FormatType = FormatType.Custom,FormatString = ClsCollect.FormatStringNegativeNumber(1)},
                        new() { Name = "خروجی", Type = typeof(float),FormatType = FormatType.Custom,FormatString = ClsCollect.FormatStringNegativeNumber(1) },
                        new() { Name = "فعلی", Type = typeof(float),FormatType = FormatType.Custom,FormatString = ClsCollect.FormatStringNegativeNumber(1) },
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
                        var frm = Program.CreateScopedForm<FrmProductKardex>();
                        frm.ProductId = value;
                        frm.ShowDialog();
                        //frm.OverShowWait<FrmProductKardex>(this.MdiParent ?? this);
                    }, "Id", "کاردکس");

                    dgvProduct.AddEventRowCellClick<Guid>(async void (value) =>
                    {
                        await GetForEdit(value);

                    }, "Id", "ویرایش");

                    dgvProduct.AddEventRowCellClick<Guid>(value =>
                    {
                        dgvProduct.DeleteRow(true, async () =>
                        {
                            await _productService.DeleteProductAsync(value);
                        });
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
            return Task.CompletedTask;
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
                layInput.SetValueType("موجودی اولیه", getFirst.InitialInventory);
                layInput.SetValueType("سنجش", getFirst.ProductUnitId);
                layInput.SetValueType("قیمت واحد", getFirst.UnitPrice);
                layInput.SetValueType("قیمت فروش", getFirst.SellPrice);
                //layInput.SetValueType("", getFirst.InitialInventory);
                //layInput.SetValueType("", getFirst.InitialInventory);


            });
        }
        public Task SetFieldLayInput()
        {
            splitContainerControl1.Panel2.WaitDownPage(async () =>
            {
                layInput.RightToLeft = RightToLeft.Yes;
                pnlFunction.Controls.Add(layInput.ShowPanelOperation());
                layInput.AddButtonOperation();

                var txtId = ClsCollect.ModelTextEdit("Id", 50, "");
                var txtCode = ClsCollect.ModelTextEditNumber("کد محصول", 50, "", true, 13, false, "N0");
                //var txtCode = ClsCollect.ModelTextEditPrice("کد محصول", 50, "");


                var getProductGroup = await _productGroupService.GetAllAsync();
                GroupControl cmbGroup = null;
                cmbGroup = ClsCollect.ModelGridToDataLayoutBtn("گروه محصول", getProductGroup, "Id", "Title", "", async () =>
                {
                    var getData = (await _productGroupService.GetAllAsync()).Select(s => new ModelPortableData { Id = s.Id, Title = s.Title }).ToList();
                    var frmPortable = new FrmPortable("گروه محصول", getData, new ModelAction
                    {

                        SaveData = async void (getData) =>
                        {
                            var get = getData;
                            await _productGroupService.SaveAsync(new ProductGroupDto
                            {
                                Id = get.Id,
                                Title = get.Title
                            });

                            var getCmbGroup = cmbGroup.Controls.OfType<GridLookUpEdit>().First();
                            getProductGroup = await _productGroupService.GetAllAsync();
                            getCmbGroup.UpdateGridLookUpEdit(getProductGroup);
                        },

                        DeleteData = async void (id) =>
                        {
                            await _productGroupService.DeleteAsync(id);
                            var getCmbGroup = cmbGroup.Controls.OfType<GridLookUpEdit>().First();
                            getProductGroup = await _productGroupService.GetAllAsync();
                            getCmbGroup.UpdateGridLookUpEdit(getProductGroup);

                        },
                    });
                    frmPortable.FormClosing += async (s1, e1) =>
                    {
                        SendKeys.SendWait("{Enter}");
                        // await Task.Delay(1000);
                    };
                    await frmPortable.ShowDialogAsync();
                });

                cmbGroup.Controls.Add(new SimpleButton { Width = 50, Left = 0 });
                cmbGroup.ConvertGroupToGrid().HiddenColumn("Id");

                var txtName = ClsCollect.ModelTextEdit("نام محصول", 100, "");

                var getProductUnit = await _productUnitService.GetAllAsync();
       
                GroupControl cmbUnit = null;
                cmbUnit = ClsCollect.ModelGridToDataLayoutBtn("سنجش", getProductUnit, "Id", "Title", "", async () =>
                {
                    var getData = (await _productUnitService.GetAllAsync()).Select(s => new ModelPortableData { Id = s.Id, Title = s.Title }).ToList();
                    var frmPortable = new FrmPortable("سنجش", getData, new ModelAction
                    {
                        SaveData = async void (getData) =>
                        {
                            var get = getData;
                            var productUnitDto = new ProductUnitDto
                            {
                                Id = get.Id,
                                Title = get.Title
                            };
                            await _productUnitService.SaveAsync(productUnitDto);
                            var getCmbUnit = cmbUnit.Controls.OfType<GridLookUpEdit>().First();
                            getProductUnit = await _productUnitService.GetAllAsync();
                            getCmbUnit.UpdateGridLookUpEdit(getProductUnit);
                        },

                        DeleteData = async void (id) =>
                        {
                            await _productUnitService.DeleteAsync(id);
                            var getCmbUnit = cmbUnit.Controls.OfType<GridLookUpEdit>().First();
                            getProductUnit = await _productUnitService.GetAllAsync();
                            getCmbUnit.UpdateGridLookUpEdit(getProductUnit);
                            //await SetFieldDgvProduct();   // 👈 اضافه شد — هم گرید هم موجودی‌های محاسبه‌شده رفرش میشن
                        },

                    });
                    frmPortable.FormClosing += async (s1, e1) =>
                    {
                        SendKeys.SendWait("{Enter}");
                        // await Task.Delay(1000);
                    };
                    await frmPortable.ShowDialogAsync();

                 
                });
                cmbUnit.ConvertGroupToGrid().HiddenColumn("Id");

                var txtMojidi = ClsCollect.ModelTextEdit("موجودی اولیه", 50, "");
                var txtPriceUnit = ClsCollect.ModelTextEditPrice("قیمت واحد", 50, "");
                var txtPriceSell = ClsCollect.ModelTextEditPrice("قیمت فروش", 50, "");

                layInput.SetFieldColumnDataLayout(true, 1, [
                    new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never},
                    new() { Grp = 1, Ctrl = txtCode, },
                    new() { Grp = 1, Ctrl = cmbGroup, SizeType = SizeConstraintsType.Custom, AutoHeight = 38 },
                    new() { Grp = 1, Ctrl = txtName,AllowNull = false},
                    new() { Grp = 1, Ctrl = cmbUnit,AllowNull = false, SizeType = SizeConstraintsType.Custom, AutoHeight = 38  },
                    new() { Grp = 1, Ctrl = txtMojidi, },
                    new() { Grp = 1, Ctrl = txtPriceUnit,AllowNull = false, },
                    new() { Grp = 1, Ctrl = txtPriceSell,AllowNull = false, },

                ]);

                layInput.BtnCancelClick += LayInput_BtnCancelClick;
                layInput.BtnSaveClick += LayInput_BtnSaveClick;
            });
            return Task.CompletedTask;
        }

        private void LayInput_BtnCancelClick(object sender, EventArgs e)
        {
            _selectedProductId = Guid.Empty;   // 👈 حیاتی
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
                    ProductGroupId = layInput.GetValue<Guid>("گروه محصول"),
                    ProductUnitId = layInput.GetValue<Guid>("سنجش"),
                    InitialInventory = layInput.GetValue<float>("موجودی اولیه"),
                    SellPrice = layInput.GetValue<long>("قیمت فروش"),
                    UnitPrice = layInput.GetValue<long>("قیمت واحد")
                };

                var savedId = await _productService.SaveProductAsync(dto);
                _selectedProductId = Guid.Empty;   // 👈 مورد بعدی هم می‌بینی چرا لازمه

                await SetFieldDgvProduct();
                ClassMessageBox.ShowMSG("اطلاعات ذخیره شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                ClassMessageBox.ShowMSG(message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار); // آیکون درست
            }
            finally
            {
                layInput._disableAfterSave = false;
            }
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {

        }
    }
}