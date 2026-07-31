using Kavosh.Services.DTOs;
using MyCom.Class;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using RightToLeft = DevExpress.XtraReports.UI.RightToLeft;

namespace Kavosh.UI.Reports.Factor
{
    public partial class RptFactorA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public RptFactorA5()
        {
            InitializeComponent();
            // Format String Number Float =>            {0:#,#}
        }


        
 

        protected override void BeforeReportPrint()
        {
          

            CultureInfo customCulture = new CultureInfo("en-US");
            customCulture.NumberFormat.NumberDecimalSeparator = "/";

            if (Tag is not FactorReportDto data)
            {
                base.BeforeReportPrint();
                return;
            }


            #region HowToPay

            var subReport = new RptHowToPayListA5();
            subReport.Tag = data.HowToPays;
            subReport.RightToLeft = RightToLeft.Yes;
            
            xrSubreport1.ReportSource = subReport;

            #endregion

            DataSource = data.FactorDetails;

            lblHeader2.Text = data.Header;
            lblNum.Text = data.Num;
            lblDate.Text = data.Date.DateTimePersian().Date;
            lblBuyerName.Text = data.Buyer;
            lblBuyerMobile.Text = data.Mobile;
            lblAddress.Text = data.Address;

            long afterMalyat1 = (data.PriceTotal * data.Malyat1 / 100);
            txtTaxes.Text = afterMalyat1.ToString("N0");
            txtPreviousDebt.Text = data.PreviousDebt.ToString("N0");

            // 👇 اصلاح شد: جمع کل = مبلغ فاکتور + مالیات (بدون بدهی قبلی)
            //txtSumTotal.Text = (data.PriceTotal + data.TaxAmount).ToString("N0");
            txtSumTotal.Text = (afterMalyat1 + data.PayableAmount).ToString("N0");
           
            //xrLabel25.Text = @"مبلغ قابل پرداخت: " + data.PayableAmount.ToString("N0");
            txt.Text = $"شماره کارت: {data.CardNumber}";
            xrLabel8.Text = $"شماره شبا: {data.ShabaNumber}";
            xrLabel9.Text = $"{data.BankName} - {data.AccountHolderName}";

            if (data.Logo is { Length: > 0 })
                picLogo.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(data.Logo));
            if (data.Mohr is { Length: > 0 })
                picMohr.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(data.Mohr));

            base.BeforeReportPrint();
        }
   
        private void lblPage_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            var ePageIndex = e.PageIndex;
            lblPage.Text = @"صفحه: " + (ePageIndex + 1) + @" از " + this.Pages.Count;
        }

 
    }
}