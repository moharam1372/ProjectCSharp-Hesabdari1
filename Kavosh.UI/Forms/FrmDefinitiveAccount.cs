using DevExpress.XtraEditors;
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

        private async Task SetFieldPersonLookUp()
        {
            var persons = await _personService.GetAllPersonsAsync();
            cmbPerson.Properties.DataSource = persons;
            cmbPerson.Properties.DisplayMember = nameof(PersonDto.FullName);
            cmbPerson.Properties.ValueMember = nameof(PersonDto.Id);
        }

        private void SetFieldDgvStatement()
        {
            if (dgvStatement.ColumnCount() == 0)
            {
                _dtStatement = dgvStatement.GridStructure([
                    new() { Name = "Id", Type = typeof(Guid) },
                    new() { Name = "IsCheck", Type = typeof(bool) },      // مخفی، فقط برای منطق دکمه
                    new() { Name = "IsSettled", Type = typeof(bool) },    // مخفی، فقط برای منطق دکمه
                    new() { Name = "تاریخ", Type = typeof(DateTime) },
                    new() { Name = "شماره سند", Type = typeof(long) },
                    new() { Name = "شرح", Type = typeof(string) },
                    new() { Name = "بدهکار", Type = typeof(long), PriceActive = true },
                    new() { Name = "بستانکار", Type = typeof(long), PriceActive = true },
                    new() { Name = "مانده", Type = typeof(long), PriceActive = true },
                    new() { Name = "وصول چک", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.edit },
                ], false, true, true);

                dgvStatement.ActiveScrollGrid();
                dgvStatement.HiddenColumn(["Id", "IsCheck", "IsSettled"]);
                dgvStatement.MaxMinWidth("وصول چک", 90, 90);

                dgvStatement.AddEventRowCellClick<Guid>(async id =>
                {
                    await SettleCheckRow(id);
                }, "Id", "وصول چک");
            }
        }

        private async void cmbPerson_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPerson.EditValue is not Guid personId || personId == Guid.Empty)
                return;

            await RefreshGridAsync(personId);
        }

        private async Task RefreshGridAsync(Guid personId)
        {
            var items = await _definitiveAccountService.GetStatementAsync(personId);

            _dtStatement.Rows.Clear();
            long runningBalance = 0;

            foreach (var d in items)
            {
                runningBalance += d.Debtor ? d.Price : -d.Price;

                _dtStatement.Rows.Add(
                    d.Id,
                    d.IsCheck,
                    d.IsSettled,
                    d.DateCustom,
                    d.DocNumber,
                    d.Description,
                    d.Debtor ? d.Price : 0,      // ستون بدهکار
                    !d.Debtor ? d.Price : 0,     // ستون بستانکار
                    runningBalance,
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
                XtraMessageBox.Show("این ردیف مربوط به چک وصول‌نشده نیست.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isSettled)
            {
                XtraMessageBox.Show("این چک قبلاً وصول شده است.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = XtraMessageBox.Show("این چک وصول شد؟", "تایید وصول چک",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await _definitiveAccountService.SettleCheckAsync(id);

                if (cmbPerson.EditValue is Guid personId)
                    await RefreshGridAsync(personId);

                XtraMessageBox.Show("چک با موفقیت وصول شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDefinitiveAccount_Load(object sender, EventArgs e) { }
    }
}