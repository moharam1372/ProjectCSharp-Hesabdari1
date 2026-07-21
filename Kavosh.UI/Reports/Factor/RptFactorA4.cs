using Kavosh.Services.DTOs;
using MyCom.Class;

namespace Kavosh.UI.Reports.Factor
{
    public partial class RptFactorA4 : DevExpress.XtraReports.UI.XtraReport
    {
        public RptFactorA4()
        {
            InitializeComponent();
        }

        protected override void BeforeReportPrint()
        {
            if (Tag is not FactorReportDto data)
            {
                base.BeforeReportPrint();
                return;
            }

            lblHeader2.Text = data.Header;
            lblNum.Text = data.Num;
            lblDate.Text = data.Date.DateTimePersian().Date;
            lblBuyerName.Text = data.Buyer;
            lblBuyerMobile.Text = data.Mobile;
            lblAddress.Text = data.Address;
            DataSource = data.FactorDetails;

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

            base.BeforeReportPrint();
        }
    }
}