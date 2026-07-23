using Kavosh.Services;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Data;
using System.Linq;

namespace Kavosh.UI.Forms
{
    public partial class FrmProductKardex : DevExpress.XtraEditors.XtraForm
    {
        private readonly ProductService _productService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private DataTable _dtKardex;

        // 👇 قبل از OverShowWait ست میشه
        public Guid ProductId;

        public FrmProductKardex(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            Shown += FrmProductKardex_Shown;
        }

        private async void FrmProductKardex_Shown(object sender, EventArgs e)
        {
            await SetStyle();
            SetFieldDgvKardex();
            await LoadDataAsync();
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(dgvKardex);
            await dgvKardex.SetStyle();
        }

        private void SetFieldDgvKardex()
        {
            if (dgvKardex.ColumnCount() == 0)
            {
                _dtKardex = dgvKardex.GridStructure([
                    new() { Name = "تاریخ", Type = typeof(string) },
                    new() { Name = "شماره فاکتور", Type = typeof(long) },
                    new() { Name = "نوع", Type = typeof(string) },
                    new() { Name = "ورودی", Type = typeof(float) },
                    new() { Name = "خروجی", Type = typeof(float) },
                    new() { Name = "مانده", Type = typeof(float) },
                ], false, true, true);

                dgvKardex.ActiveScrollGrid();
            }
        }

        private async Task LoadDataAsync()
        {
            var product = await _productService.GetProductByIdAsync(ProductId);
            if (product is null) return;

            lblProductTitle.Text = $"کالا: {product.Title}";

            var kardex = await _productService.GetKardexAsync(ProductId);

            _dtKardex.Rows.Clear();
            float runningBalance = product.InitialInventory;   // 👈 شروع مانده از موجودی اولیه

            foreach (var row in kardex)
            {
                runningBalance += row.Input - row.Output;

                _dtKardex.Rows.Add(
                    row.Date.DateTimePersian().Date,
                    row.FactorCode,
                    row.Type ? "فروش" : "خرید",
                    row.Input,
                    row.Output,
                    runningBalance
                );
            }

            dgvKardex.SetFieldSizeColumn();
            lblStockValue.Text = runningBalance.ToString("N0");
        }

        private void FrmProductKardex_Load(object sender, EventArgs e) { }
    }
}