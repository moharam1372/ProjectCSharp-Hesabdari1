using DevExpress.XtraEditors;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace Kavosh.UI.Forms
{
    public partial class FrmDefinitiveAccount : DevExpress.XtraEditors.XtraForm
    {
        private readonly DefinitiveAccountService _definitiveAccountService;
        private readonly PersonService _personService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtStatement;

        // 👇 اگه از FrmPerson باز بشه، این از قبل ست میشه؛ اگه مستقیم از منو باز بشه، خالیه و کاربر خودش انتخاب می‌کنه
        public Guid? PersonIdToShow;
        //public Guid? PersonIdToShow { get; set; }

        public FrmDefinitiveAccount(DefinitiveAccountService definitiveAccountService, PersonService personService)
        {
            InitializeComponent();
            _definitiveAccountService = definitiveAccountService;
            _personService = personService;
            Shown += FrmDefinitiveAccount_Shown;
        }

        private async void FrmDefinitiveAccount_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            await SetFieldPersonLookUp();
            SetFieldDgvStatement();

            if (PersonIdToShow.HasValue)
                cmbPerson.EditValue = PersonIdToShow.Value;   // این خودش رویداد EditValueChanged رو صدا می‌زنه و گرید رو پر می‌کنه
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvStatement);
            await dgvStatement.SetStyle();
        }
        GridLookUpEdit cmbPerson;
        private async Task SetFieldPersonLookUp()
        {
            var persons = (await _personService.GetAllPersonsAsync()).Select(s => new { s.Id, s.FullName }).ToList();


            cmbPerson = ClsCollect.ModelGridToDataLayout("مشتری", persons, "Id", "FullName", "");
            cmbPerson.HiddenColumn("Id");
            cmbPerson.Dock = DockStyle.Fill;
            tablePanel1.Controls.Add(cmbPerson);
            tablePanel1.SetRow(cmbPerson, 0);
            tablePanel1.SetColumn(cmbPerson, 2);
            cmbPerson.Margin = new Padding(-10);
            cmbPerson.EditValueChanged += async (s1, e1) =>
            {
                if (cmbPerson.EditValue is not Guid personId || personId == Guid.Empty)
                    return;

                await RefreshGridAsync(personId);
            };
        }

        private void SetFieldDgvStatement()
        {

            if (dgvStatement.ColumnCount() == 0)
            {
                _dtStatement = dgvStatement.GridStructure([
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "IsCheck", Type = typeof(bool) },
                    new() { Name = "IsSettled", Type = typeof(bool) },
                    new() { Name = "تاریخ", Type = typeof(DateTime) },
                    new() { Name = "شماره سند", Type = typeof(long) },
                    new() { Name = "شرح", Type = typeof(string) },
                    new() { Name = "بدهکار", Type = typeof(long), PriceActive = true },
                    new() { Name = "بستانکار", Type = typeof(long), PriceActive = true },
                    new() { Name = "مانده", Type = typeof(long), PriceActive = true },
                    new() { Name = "وضعیت", Type = typeof(string) }, // 👈 ستون جدید
                    new() { Name = "وصول چک", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit },
                ], false, true, true);

                dgvStatement.ActiveScrollGrid();
                dgvStatement.HiddenColumn(["Id", "IsCheck", "IsSettled"]);
                dgvStatement.MaxMinWidth("وصول چک", 90, 90);
                dgvStatement.MaxMinWidth("وضعیت", 80, 80);

                #region Event

                dgvStatement.GetViewBase.RowCellStyle += (s1, e1) =>
                {
                    var getClm = e1.Column.FieldName;

                    if (getClm == "بدهکار")
                    {
                        e1.Appearance.ForeColor = Color.FromArgb(168, 255, 0, 0);
                    }
                    else if (getClm == "بستانکار")
                    {
                        e1.Appearance.ForeColor = Color.FromArgb(255, 70, 243, 91);
                    }
                    else if (getClm == "مانده" || getClm == "وضعیت")
                    {
                        // مانده رو از خود ردیف می‌خونیم (نه فقط بدهکار/بستانکار همون خط)
                        var balanceObj = viewStatement.GetRowCellValue(e1.RowHandle, "مانده");
                        if (balanceObj != null && balanceObj != DBNull.Value)
                        {
                            var balance = Convert.ToInt64(balanceObj);
                            if (balance > 0)
                                e1.Appearance.ForeColor = Color.FromArgb(255, 0, 0); // بدهکار = قرمز
                            else if (balance < 0)
                                e1.Appearance.ForeColor = Color.FromArgb(0, 150, 0); // بستانکار = سبز
                        }
                    }
                };

                dgvStatement.AddEventRowCellClick<Guid>(async id =>
                {
                    await SettleCheckRow(id);
                }, "Id", "وصول چک");

                #endregion
            }
        }




        private async Task RefreshGridAsync(Guid personId)
        {
            var items = await _definitiveAccountService.GetStatementAsync(personId);

            _dtStatement.Rows.Clear();
            long runningBalance = 0;

            foreach (var d in items)
            {
                runningBalance += d.Debtor ? d.Price : -d.Price;

                // وضعیت بر اساس مانده‌ی تجمعیِ همین ردیف
                string status = runningBalance > 0 ? "بدهکار"
                    : runningBalance < 0 ? "بستانکار"
                    : "تسویه";

                _dtStatement.Rows.Add(
                    d.Id,
                    d.IsCheck,
                    d.IsSettled,
                    d.DateCustom,
                    d.DocNumber,
                    d.Description,
                    d.Debtor ? d.Price : 0,      // بدهکار
                    !d.Debtor ? d.Price : 0,     // بستانکار
                    runningBalance,              // مانده
                    status,                      // 👈 وضعیت
                    "وصول چک"
                );
            }

            dgvStatement.SetFieldSizeColumn();
            lblBalanceValue.Text = runningBalance.ToString("N0");
        }

        private async Task SettleCheckRow(Guid id)
        {
            var isCheck = dgvStatement.GetValue<bool>("IsCheck");
            var isSettled = dgvStatement.GetValue<bool>("IsSettled");
            var isDebtor = Convert.ToInt64(dgvStatement.GetValue<long>("بدهکار")) > 0;

            if (!isCheck || !isDebtor)
            {
                ClassMessageBox.ShowMSG("این ردیف مربوط به چک وصول‌نشده نیست.", Class_Text.Msg_Name,
                    ClassMessageBox.enumIcon.اطلاعات);
                return;
            }

            if (isSettled)
            {
                ClassMessageBox.ShowMSG("این چک قبلاً وصول شده است.", Class_Text.Msg_Name,
                    ClassMessageBox.enumIcon.اطلاعات);
                return;
            }

            var confirm = ClassMessageBox.ShowMSGQues("این چک وصول شود؟", Class_Text.Msg_Name,
                ClassMessageBox.enumIcon.اطلاعات);

        

            if (!confirm)
                return;

            try
            {
                await _definitiveAccountService.SettleCheckAsync(id);

                if (cmbPerson.EditValue is Guid personId)
                    await RefreshGridAsync(personId);
                ClassMessageBox.ShowMSG("چک با موفقیت وصول شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);



                #region Main

                Kavosh.Services.AppEvents.RaiseDataChanged();   // 👈 اضافه شد

                #endregion

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDefinitiveAccount_Load(object sender, EventArgs e) { }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            dgvStatement.ExportToExcel("صورت حساب");
        }
    }
}