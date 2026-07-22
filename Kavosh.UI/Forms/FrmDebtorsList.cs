using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kavosh.UI.Forms
{
    public partial class FrmDebtorsList : DevExpress.XtraEditors.XtraForm
    {
        private readonly DefinitiveAccountService _definitiveAccountService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtDebtors;

        public FrmDebtorsList(DefinitiveAccountService definitiveAccountService)
        {
            InitializeComponent();
            _definitiveAccountService = definitiveAccountService;
            Shown += FrmDebtorsList_Shown;
            Activated += FrmDebtorsList_Activated;   // رفرش خودکار وقتی از FrmDefinitiveAccount برمی‌گرده
        }

        private async void FrmDebtorsList_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            SetFieldDgvDebtors();
            await RefreshGridAsync();
        }

        private async void FrmDebtorsList_Activated(object sender, EventArgs e)
        {
            if (_dtDebtors is not null)
                await RefreshGridAsync();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvDebtors);
            await dgvDebtors.SetStyle();
        }

        private void SetFieldDgvDebtors()
        {
            if (dgvDebtors.ColumnCount() == 0)
            {
                _dtDebtors = dgvDebtors.GridStructure([
                    new() { Name = "PersonId", Type = typeof(Guid) },
                    new() { Name = "نام مشتری", Type = typeof(string) },
                    new() { Name = "موبایل", Type = typeof(string) },
                    new() { Name = "آخرین تاریخ بدهی", Type = typeof(string) },
                    new() { Name = "بدهی چکی", Type = typeof(long), PriceActive = true },
                    new() { Name = "بدهی غیرچکی", Type = typeof(long), PriceActive = true },
                    new() { Name = "جمع بدهی", Type = typeof(long), PriceActive = true },
                    new() { Name = "مشاهده", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.view },
                ], false, true, true);

                dgvDebtors.ActiveScrollGrid();
                dgvDebtors.HiddenColumn("PersonId");

                // 👇 خلاقیت کوچیک: پس‌زمینه‌ی زرد کم‌رنگ روی ستون «بدهی چکی» وقتی مبلغش صفر نیست
                viewDebtors.RowCellStyle += ViewDebtors_RowCellStyle;

                dgvDebtors.AddEventRowCellClick<Guid>(personId =>
                {
                    var frm = Program.CreateScopedForm<FrmDefinitiveAccount>();
                    frm.PersonIdToShow = personId;
                    frm.OverShowWait<FrmDefinitiveAccount>(this.MdiParent ?? this);
                }, "PersonId", "مشاهده");
            }
        }

        private void ViewDebtors_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "بدهی چکی")
                return;

            var value = viewDebtors.GetRowCellValue(e.RowHandle, "بدهی چکی");
            if (value is long checkDebt && checkDebt > 0)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 245, 210);   // زرد کم‌رنگ
            }
        }

        private async Task RefreshGridAsync()
        {
            var debtors = await _definitiveAccountService.GetDebtorsListAsync();

            _dtDebtors.Rows.Clear();
            foreach (var d in debtors)
            {
                _dtDebtors.Rows.Add(
                    d.PersonId, d.PersonName, d.Mobile, d.LastDebtDate.DateTimePersian().Date,
                    d.CheckDebt, d.OtherDebt, d.TotalDebt, "مشاهده"
                );
            }
            dgvDebtors.SetFieldSizeColumn();

            // پنل خلاصه‌ی بالای فرم
            lblCountValue.Text = debtors.Count.ToString();
            lblCheckDebtValue.Text = debtors.Sum(d => d.CheckDebt).ToString("N0");
            lblOtherDebtValue.Text = debtors.Sum(d => d.OtherDebt).ToString("N0");
            lblTotalValue.Text = debtors.Sum(d => d.TotalDebt).ToString("N0");
        }

        private void FrmDebtorsList_Load(object sender, EventArgs e) { }
    }
}