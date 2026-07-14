using DevExpress.XtraEditors;
using MyCom.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyCom.Object;

namespace Kavosh.UI.Forms
{
    public partial class FrmProduct : DevExpress.XtraEditors.XtraForm
    {
        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        public FrmProduct()
        {
            InitializeComponent();
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
         await   dgvProduct.SetStyle();
        }


        private DataTable _dtProduct;

        public async Task SetFieldDgvProduct()
        {
            splitContainerControl1.Panel1.WaitDownPage(() =>
            {
                if (dgvProduct.ColumnCount() == 0)
                {
                    _dtProduct = dgvProduct.GridStructure([
                        new() { Name = "Id", Type = typeof(Guid) },
                        new() { Name = "کاردکس", Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.view},
                        new() { Name = "حذف", Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.delete },
                        new() { Name = "ویرایش", Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.edit},
                        new() { Name = "کد کالا", Type = typeof(long) },
                        new() { Name = "گروه کالا", Type = typeof(Guid) },
                        new() { Name = "نام کالا", Type = typeof(string) },
                        new() { Name = "اولیه", Type = typeof(float) },
                        new() { Name = "ورودی", Type = typeof(float) },
                        new() { Name = "خروجی", Type = typeof(float) },
                        new() { Name = "فعلی", Type = typeof(float) },
                    ], false, true, true);
                    dgvProduct.ActiveScrollGrid();


                    #region Relation



                    #endregion

                    #region Event



                    #endregion
                }
            });
        }

        public async Task SetFieldLayInput()
        {

            splitContainerControl1.Panel2.WaitDownPage(() =>
            {
                layInput.RightToLeft = RightToLeft.Yes;
                pnlFunction.Controls.Add(layInput.ShowPanelOperation());
                layInput.AddButtonOperation();

                var txtId = ClsCollect.ModelTextEdit("Id", 50, "");
                var txtCode = ClsCollect.ModelTextEditNumber("کد محصول", 50, "");
                var cmbGroup = ClsCollect.ModelTextEditNumber("گروه محصول", 50, "");
                var txtName = ClsCollect.ModelTextEdit("نام محصول", 50, "");
                var cmbUnit = ClsCollect.ModelTextEdit("سنجش", 50, "");
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

        private void LayInput_BtnSaveClick(object sender, EventArgs e)
        {

        }
    }
}