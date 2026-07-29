using DevExpress.XtraReports.UI;
using Kavosh.Services.DTOs;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using MyCom.Class;

namespace Kavosh.UI.Reports.Factor
{
    public partial class RptHowToPayListA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public RptHowToPayListA5()
        {
            InitializeComponent();
        }

        protected override void BeforeReportPrint()
        {
           
            if (Tag is not List<HowToPayReportDto> data)
            {
                base.BeforeReportPrint();
                return;
            }


            var source = data.Select(s => new 
            {
                Price = s.Price.ToString("N0"),
                CheckDate = s.CheckDate?.DateTimePersian().Date,
                Description = s.Description,
                PaymentTypeTitle = s.PaymentTypeTitle,
                Settlement = s.Settlement == false ? "پاس نشده" : "پاس شده",
                CheckNumber = "",
            }).ToList();
            DataSource = source;

            base.BeforeReportPrint();
        }
    }
}
