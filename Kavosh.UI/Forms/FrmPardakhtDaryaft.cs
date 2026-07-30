using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace Kavosh.UI.Forms
{
    public partial class FrmPardakhtDaryaft : XtraForm
    {
        private readonly DefinitiveAccountService _definitiveAccountService;
        private readonly PersonService _personService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtStatement;
        private Guid? _lastPersonId;
        public FrmPardakhtDaryaft(DefinitiveAccountService definitiveAccountService, PersonService personService)
        {
            _definitiveAccountService = definitiveAccountService;
            _personService = personService;

            components = new System.ComponentModel.Container();
            InitializeComponent();

            Shown += FrmPardakhtDaryaft_Shown;
        }

        private void FrmPardakhtDaryaft_Load(object sender, EventArgs e)
        {

        }
        private async void FrmPardakhtDaryaft_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            await SetFieldLayInput();
            SetFieldDgvStatement();
        }
        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvStatement);
            await dgvStatement.SetStyle();
        }
        // ============= فرم ورودی =============
        public async Task SetFieldLayInput()
        {
            layInput.RightToLeft = RightToLeft.Yes;
            pnlFunction.Controls.Add(layInput.ShowPanelOperation());
            layInput.AddButtonOperation();
            layInput._btnCancel.Enabled = false;

            var txtId = ClsCollect.ModelTextEdit("Id", 50, "");

            var persons = (await _personService.GetAllPersonsAsync())
                .Select(s => new { s.Id, s.FullName }).ToList();
            var cmbPerson = ClsCollect.ModelGridToDataLayoutFull("طرف حساب", persons, "Id", "FullName", "", async id =>
            {
                await RefreshStatementAsync(id.ToGuid());
            });
            cmbPerson.ConvertGroupToGrid().HiddenColumn("Id");

            var cmbType = ClsCollect.ModelRadioGroup("نوع تراکنش", new List<ClsCollect.modelRadioGroup>{
                new() { Column = 1, Field = "دریافت از مشتری" },
                new() { Column = 2, Field = "پرداخت به مشتری" },
            });
            cmbType.Properties.BorderStyle = BorderStyles.Simple;
            var cmbMethod = ClsCollect.ModelRadioGroup("نحوه پرداخت", new List<ClsCollect.modelRadioGroup>{
                new() { Column = 1, Field = "نقدی" },
                new() { Column = 2, Field = "کارت به کارت" },
                new() { Column = 1, Field = "چک" },
            });
            cmbMethod.Properties.BorderStyle = BorderStyles.Simple;
            var txtCheckNumber = ClsCollect.ModelTextEdit("شماره چک", 30, "");
            var dtCheckDate = ClsCollect.ModelDateTime("تاریخ چک", 10, "");
            var dtDate = ClsCollect.ModelDateTime("تاریخ", 10, DateTime.Now.DateTimePersian().Date);
            var txtPrice = ClsCollect.ModelTextEditPrice("مبلغ", 50, "");
            var txtDescription = ClsCollect.ModelLayoutMemoEdit("توضیحات", 200, "");

            layInput.SetFieldColumnDataLayout(true, 1, [
                new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
                new() { Grp = 1, Ctrl = cmbPerson, AllowNull = false, SizeType = SizeConstraintsType.Custom, AutoHeight = 38 },
                new() { Grp = 1, Ctrl = cmbType,AllowNull = false, SizeType = SizeConstraintsType.Custom, AutoHeight = 38 },
                new() { Grp = 1, Ctrl = cmbMethod, AllowNull = false,SizeType = SizeConstraintsType.Custom, AutoHeight = 62 },
                new() { Grp = 1, Ctrl = dtDate,AllowNull = false, },
                new() { Grp = 1, Ctrl = txtPrice, AllowNull = false },
                new() { Grp = 1, Ctrl = txtCheckNumber, },
                new() { Grp = 1, Ctrl = dtCheckDate, },
                new() { Grp = 1, Ctrl = txtDescription, SizeType = SizeConstraintsType.Custom, AutoHeight = 55 },
            ]);

            layInput.BtnSaveClick += LayInput_BtnSaveClick;
            layInput.CallNew();
        }

        // ============= گرید صورت‌حساب =============
        private void SetFieldDgvStatement()
        {
            if (dgvStatement.ColumnCount() == 0)
            {
                _dtStatement = dgvStatement.GridStructure([
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "تاریخ", Type = typeof(DateTime) },
                    new() { Name = "شماره سند", Type = typeof(long) },
                    new() { Name = "شرح", Type = typeof(string) },
                    new() { Name = "بدهکار", Type = typeof(long), PriceActive = true },
                    new() { Name = "بستانکار", Type = typeof(long), PriceActive = true },
                    new() { Name = "مانده", Type = typeof(long), PriceActive = true },
                ], false, true, true);

                dgvStatement.ActiveScrollGrid();
                dgvStatement.HiddenColumn("Id");

                viewStatement.RowCellStyle += (s1, e1) =>
                {
                    var getClm = e1.Column.FieldName;
                    if (getClm == "بدهکار")
                        e1.Appearance.ForeColor = Color.FromArgb(168, 255, 0, 0);
                    else if (getClm == "بستانکار")
                        e1.Appearance.ForeColor = Color.FromArgb(55, 152, 0);
                    else if (getClm == "مانده")
                    {
                        var balanceObj = viewStatement.GetRowCellValue(e1.RowHandle, "مانده");
                        if (balanceObj != null && balanceObj != DBNull.Value)
                        {
                            var balance = Convert.ToInt64(balanceObj);
                            if (balance > 0)
                                e1.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                            else if (balance < 0)
                                e1.Appearance.ForeColor = Color.FromArgb(55, 152, 0);
                        }
                    }
                };
            }
        }

        private async Task RefreshStatementAsync(Guid personId)
        {
            var items = await _definitiveAccountService.GetStatementAsync(personId);

            _dtStatement.Rows.Clear();
            long runningBalance = 0;
            foreach (var d in items)
            {
                runningBalance += d.Debtor ? d.Price : -d.Price;
                _dtStatement.Rows.Add(
                    d.Id, d.DateCustom, d.DocNumber, d.Description,
                    d.Debtor ? d.Price : 0, !d.Debtor ? d.Price : 0, runningBalance
                );
            }
            dgvStatement.SetFieldSizeColumn();
            lblBalanceValue.Text = runningBalance.ToString("N0");
        }
        // ============= ذخیره =============
        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = true;
            try
            {
                var personId = layInput.GetValue<Guid>("طرف حساب");
                var typeText = layInput.GetValue<string>("نوع تراکنش");
                var methodText = layInput.GetValue<string>("نحوه پرداخت");
                var dateText = layInput.GetValue<string>("تاریخ");
                var price = layInput.GetValue<long>("مبلغ");
                var checkNumber = layInput.GetValue<string>("شماره چک");
                var description = layInput.GetValue<string>("توضیحات");

                var parsedDate = string.IsNullOrWhiteSpace(dateText)
                    ? DateTime.Now
                    : dateText.ShamsiToMiladi() ?? DateTime.Now;

                var dto = new ReceiptPaymentDto
                {
                    PersonId = personId,
                    IsReceipt = typeText != "پرداخت به مشتری",
                    IsCheckPayment = methodText == "چک",
                    CheckNumber = checkNumber,
                    Price = price,
                    Description = description,
                    DateCustom = parsedDate
                };

                await _definitiveAccountService.CreateManualTransactionAsync(dto);

                _lastPersonId = personId;
                await RefreshStatementAsync(personId);

                Kavosh.Services.AppEvents.RaiseDataChanged();   // رفرش داشبورد FrmMain


                ClassMessageBox.ShowMSG("تراکنش با موفقیت ثبت شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
              

                layInput.CallNew();
                layInput.SetValueType("تاریخ", DateTime.Now.DateTimePersian().Date);
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
    }
}
