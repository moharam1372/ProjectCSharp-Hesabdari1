using DevExpress.XtraEditors;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;

namespace Kavosh.UI.Forms
{
    public partial class FrmMarketer : DevExpress.XtraEditors.XtraForm
    {
        private readonly MarketerService _marketerService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtMarketer;
        private Guid _selectedMarketerId = Guid.Empty;

        public FrmMarketer(MarketerService marketerService)
        {
            InitializeComponent();
            _marketerService = marketerService;
            Shown += FrmMarketer_Shown;
        }

        private async void FrmMarketer_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            await SetFieldLayInput();
            await SetFieldDgvMarketer();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(srcGrid, 15);
            _clsFontBold.ChangeFont(dgvMarketer);
            await dgvMarketer.SetStyle();
        }

        public async Task SetFieldDgvMarketer()
        {
            splitContainerControl1.Panel1.WaitDownPage(async () =>
            {
                if (dgvMarketer.ColumnCount() == 0)
                {
                    _dtMarketer = dgvMarketer.GridStructure([
                        new() { Name = "Id", Type = typeof(Guid) },
                        new() { Name = "ویرایش", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit },
                        new() { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                        new() { Name = "نام بازاریاب", Type = typeof(string) },
                        new() { Name = "شماره تماس", Type = typeof(string) },
                    ], false, true, true);

                    dgvMarketer.ActiveScrollGrid();
                    dgvMarketer.HiddenColumn("Id");

                    dgvMarketer.AddEventRowCellClick<Guid>(async id =>
                    {
                        await LoadMarketerToForm(id);
                    }, "Id", "ویرایش");

                    dgvMarketer.AddEventRowCellClick<Guid>(id =>
                    {
                        dgvMarketer.DeleteRow(true, async () =>
                        {
                            try
                            {
                                await _marketerService.DeleteAsync(id);
                                await RefreshGridAsync();
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        });
                    }, "Id", "حذف");
                }

                await RefreshGridAsync();
            });
        }

        private async Task RefreshGridAsync()
        {
            var items = await _marketerService.GetAllAsync();

            _dtMarketer.Rows.Clear();
            foreach (var m in items)
                _dtMarketer.Rows.Add(m.Id, "ویرایش", "حذف", m.FullName, m.PhoneNumber);

            dgvMarketer.SetFieldSizeColumn();
        }

        private TextEdit txtId, txtTitle, txtPhone;

        public async Task SetFieldLayInput()
        {
            splitContainerControl1.Panel2.WaitDownPage(() =>
            {
                layInput.RightToLeft = RightToLeft.Yes;
                pnlFunction.Controls.Add(layInput.ShowPanelOperation());
                layInput.AddButtonOperation();

                txtId = ClsCollect.ModelTextEdit("Id", 50, "");
                txtTitle = ClsCollect.ModelTextEdit("نام بازاریاب", 100, "");
                txtPhone = ClsCollect.ModelTextEditNumber("شماره تماس", 11, "", true, 13, false);

                layInput.SetFieldColumnDataLayout(true, 1, [
                    new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never},
                    new() { Grp = 1, Ctrl = txtTitle, AllowNull = false },
                    new() { Grp = 1, Ctrl = txtPhone, },
                ]);

                layInput.BtnCancelClick += LayInput_BtnCancelClick;
                layInput.BtnSaveClick += LayInput_BtnSaveClick;
            });
            return;
        }

        private async Task LoadMarketerToForm(Guid id)
        {
            var dto = await _marketerService.GetByIdAsync(id);
            if (dto is null) return;

            layInput.CallNew();
            _selectedMarketerId = dto.Id;
            txtId.Text = dto.Id.ToString();
            txtTitle.Text = dto.FullName;
            txtPhone.Text = dto.PhoneNumber;
        }

        private void ClearForm()
        {
            _selectedMarketerId = Guid.Empty;
            txtId.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtPhone.Text = string.Empty;
        }

        private void LayInput_BtnCancelClick(object sender, EventArgs e) => ClearForm();

        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = true;
            try
            {
                var dto = new MarketerDto
                {
                    Id = _selectedMarketerId,
                    FullName = txtTitle.Text,
                    PhoneNumber = txtPhone.Text
                };

                await _marketerService.SaveAsync(dto);
                await RefreshGridAsync();
                ClearForm();
                //ClassMessageBox.ShowMSG();
                XtraMessageBox.Show("ذخیره شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                layInput._disableAfterSave = false;
            }
        }

        private void FrmMarketer_Load(object sender, EventArgs e) { }
    }
}