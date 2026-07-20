using DevExpress.Pdf;
using DevExpress.XtraEditors;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Kavosh.UI.Forms
{
    public partial class FrmPerson : DevExpress.XtraEditors.XtraForm
    {
        private readonly PersonService _personService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtPerson;
        private Guid _selectedPersonId = Guid.Empty;

        public FrmPerson(PersonService personService)
        {
            InitializeComponent();
            _personService = personService;
            Shown += FrmPerson_Shown;
        }

        private async void FrmPerson_Shown(object sender, EventArgs e)
        {
            await Task.WhenAll(
                SetStyle(),
                SetFieldLayInput(),
                SetFieldDgvPerson());


            //await SetStyle();
            //await SetFieldLayInput();
            //await SetFieldDgvPerson();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvPerson);
            await dgvPerson.SetStyle();
        }

        // ============= گرید =============
        public async Task SetFieldDgvPerson()
        {
            splitContainerControl1.Panel1.WaitDownPage(async () =>
            {
                if (dgvPerson.ColumnCount() == 0)
                {
                    _dtPerson = dgvPerson.GridStructure([
                        new() { Name = "Id", Type = typeof(Guid) },
                        new() { Name = "ویرایش", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit },
                        new() { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete },
                        new() { Name = "نام و نام‌خانوادگی", Type = typeof(string) },
                        new() { Name = "موبایل", Type = typeof(string) },
                        new() { Name = "کد ملی", Type = typeof(string) },
                        new() { Name = "آدرس", Type = typeof(string) },
                        new() { Name = "صورت‌حساب", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.view },   // 👈 جدید

                    ], false, true, true);
                    dgvPerson.ActiveScrollGrid();

                    dgvPerson.HiddenColumn("Id");
                    dgvPerson.FixColumn("صورت‌حساب", FixedStyle.Left);
                    #region Event

                    dgvPerson.AddEventRowCellClick<Guid>(id =>
                    {
                        var frm = Program.CreateScopedForm<FrmDefinitiveAccount>();
                        frm.PersonIdToShow = id;
                        frm.OverShowWait<FrmDefinitiveAccount>(this.MdiParent ?? this);
                    }, "Id", "صورت‌حساب");

                    dgvPerson.AddEventRowCellClick<Guid>(async id =>
                    {
                        await LoadPersonToForm(id);
                    }, "Id", "ویرایش");

                    dgvPerson.AddEventRowCellClick<Guid>(id =>
                    {
                        dgvPerson.DeleteRow(true, async () =>
                        {
                            await _personService.DeletePersonAsync(id);
                            await RefreshGridAsync();
                        });
                    }, "Id", "حذف");

                    #endregion
                }

                await RefreshGridAsync();
            });


        }

        private async Task RefreshGridAsync()
        {
            var persons = await _personService.GetAllPersonsAsync();

            _dtPerson.Rows.Clear();
            foreach (var p in persons)
            {
                _dtPerson.Rows.Add(p.Id, "ویرایش", "حذف", p.FullName, p.Mobile, p.CodeMelli, p.Address);
            }
            dgvPerson.SetFieldSizeColumn();
        }

        // ============= فرم ورودی =============
        private TextEdit txtId, txtFullName, txtMobile, txtCodeMelli, txtAddress;

        public async Task SetFieldLayInput()
        {
            splitContainerControl1.Panel2.WaitDownPage(() =>
            {
                layInput.RightToLeft = RightToLeft.Yes;
                pnlFunction.Controls.Add(layInput.ShowPanelOperation());
                layInput.AddButtonOperation();

                txtId = ClsCollect.ModelTextEdit("Id", 50, "");
                txtFullName = ClsCollect.ModelTextEdit("نام و نام‌خانوادگی", 60, "");
                txtMobile = ClsCollect.ModelTextEditNumber("موبایل", 11, "", true, 13, false);
                txtCodeMelli = ClsCollect.ModelCodeMelli("کد ملی", "");
                txtAddress = ClsCollect.ModelLayoutMemoEdit("آدرس", 100, "");

                layInput.SetFieldColumnDataLayout(true, 1, [
                    new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never},
                    new() { Grp = 1, Ctrl = txtFullName, },
                    new() { Grp = 1, Ctrl = txtMobile, },
                    new() { Grp = 1, Ctrl = txtCodeMelli, },
                    new() { Grp = 1, Ctrl = txtAddress,SizeType = SizeConstraintsType.Custom,AutoHeight = 80},
                ]);

                layInput.BtnCancelClick += LayInput_BtnCancelClick;
                layInput.BtnSaveClick += LayInput_BtnSaveClick;
            });
        }

        private async Task LoadPersonToForm(Guid id)
        {
            var dto = await _personService.GetPersonByIdAsync(id);
            if (dto is null)
                return;

            layInput.CallNew();

            _selectedPersonId = dto.Id;
            txtId.Text = dto.Id.ToString();
            txtFullName.Text = dto.FullName;
            txtMobile.Text = dto.Mobile;
            txtCodeMelli.Text = dto.CodeMelli;
            txtAddress.Text = dto.Address;
        }

        private void ClearForm()
        {
            _selectedPersonId = Guid.Empty;
            txtId.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtCodeMelli.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void LayInput_BtnCancelClick(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = false;
            try
            {
                var dto = new PersonDto
                {
                    Id = _selectedPersonId,
                    FullName = txtFullName.Text,
                    Mobile = txtMobile.Text,
                    CodeMelli = txtCodeMelli.Text,
                    Address = txtAddress.Text
                };

                await _personService.SavePersonAsync(dto);

                await RefreshGridAsync();
                ClearForm();

                XtraMessageBox.Show("ذخیره شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                layInput._disableAfterSave = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmPerson_Load(object sender, EventArgs e)
        {

        }
    }
}