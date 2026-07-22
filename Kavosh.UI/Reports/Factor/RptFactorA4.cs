using Kavosh.Services.DTOs;
using MyCom.Class;
using System.Globalization;

namespace Kavosh.UI.Reports.Factor
{
    public partial class RptFactorA4 : DevExpress.XtraReports.UI.XtraReport
    {
        public RptFactorA4()
        {
            InitializeComponent();
            // Format String Number Float =>            {0:#,#}
        }

        protected override void BeforeReportPrint()
        {
            //CultureInfo customCulture = new CultureInfo("fa-IR");

            //// تغییر کاراکتر اعشار به / (یا هر کاراکتر دلخواه)
            //customCulture.NumberFormat.NumberDecimalSeparator = "/";
            //customCulture.NumberFormat.PercentDecimalSeparator = "/";

            //// اعمال به ترد فعلی
            //Thread.CurrentThread.CurrentCulture = customCulture;
            //Thread.CurrentThread.CurrentUICulture = customCulture;

            CultureInfo customCulture = new CultureInfo("en-US");
            customCulture.NumberFormat.NumberDecimalSeparator = "/";

            //if (xrTableCell4 != null && xrTableCell4.DataBindings.Count > 0)




            if (Tag is not FactorReportDto data)
            {
                base.BeforeReportPrint();
                return;
            }
            DataSource = data.FactorDetails; // لیست محصولات در DetailReport نمایش داده میشه


            lblHeader2.Text = data.Header;
            lblNum.Text = data.Num;
            lblDate.Text = data.Date.DateTimePersian().Date;
            lblBuyerName.Text = data.Buyer;
            lblBuyerMobile.Text = data.Mobile;
            lblAddress.Text = data.Address;


            // فیلدهای GroupFooter که قبلاً متن ثابت بودن، حالا داینامیک میشن
            txtTaxes.Text = data.TaxAmount.ToString("N0");
            txtPreviousDebt.Text = data.PreviousDebt.ToString("N0");

            txtSumTotal.Text = data.PreviousDebt.ToString("N0");
            xrLabel25.Text = @"مبلغ قابل پرداخت: " + data.PayableAmount.ToString("N0");
            txt.Text = $"شماره کارت: {data.CardNumber}";
            xrLabel8.Text = $"شماره شبا: {data.ShabaNumber}";
            xrLabel9.Text = $"{data.BankName} - {data.AccountHolderName}";

            if (data.Logo is { Length: > 0 })
                picLogo.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(data.Logo));
            if (data.Mohr is { Length: > 0 })
                picMohr.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(data.Mohr));






            object value = this.GetCurrentColumnValue("Count");
    



            base.BeforeReportPrint();
        }
    }
}