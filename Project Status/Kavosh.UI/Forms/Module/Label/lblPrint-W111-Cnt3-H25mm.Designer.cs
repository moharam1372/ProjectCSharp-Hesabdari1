namespace Kavosh.UI.Forms.Module.Label
{
    partial class lblPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.PharmacodeGenerator pharmacodeGenerator1 = new DevExpress.XtraPrinting.BarCode.PharmacodeGenerator();
            DevExpress.XtraPrinting.BarCode.PharmacodeGenerator pharmacodeGenerator2 = new DevExpress.XtraPrinting.BarCode.PharmacodeGenerator();
            DevExpress.XtraPrinting.BarCode.PharmacodeGenerator pharmacodeGenerator3 = new DevExpress.XtraPrinting.BarCode.PharmacodeGenerator();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.barCode3 = new DevExpress.XtraReports.UI.XRBarCode();
            this.barCode2 = new DevExpress.XtraReports.UI.XRBarCode();
            this.barCode1 = new DevExpress.XtraReports.UI.XRBarCode();
            this.lbl1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl3 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 25.4F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 25.4F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.barCode3,
            this.barCode2,
            this.barCode1,
            this.lbl1,
            this.lbl2,
            this.lbl3});
            this.Detail.Dpi = 25.4F;
            this.Detail.HeightF = 22.8F;
            this.Detail.HierarchyPrintOptions.Indent = 508F;
            this.Detail.Name = "Detail";
            // 
            // barCode3
            // 
            this.barCode3.AutoModule = true;
            this.barCode3.Dpi = 25.4F;
            this.barCode3.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.barCode3.LocationFloat = new DevExpress.Utils.PointFloat(0.9856126F, 6.854276F);
            this.barCode3.Module = 0.508F;
            this.barCode3.Name = "barCode3";
            this.barCode3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2.645833F, 2.645833F, 0F, 0F, 25.4F);
            this.barCode3.SizeF = new System.Drawing.SizeF(30.09757F, 12.17F);
            this.barCode3.StylePriority.UseFont = false;
            this.barCode3.StylePriority.UseTextAlignment = false;
            this.barCode3.Symbology = pharmacodeGenerator1;
            this.barCode3.Text = "123456789";
            this.barCode3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // barCode2
            // 
            this.barCode2.AutoModule = true;
            this.barCode2.Dpi = 25.4F;
            this.barCode2.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.barCode2.LocationFloat = new DevExpress.Utils.PointFloat(36.87563F, 6.854979F);
            this.barCode2.Module = 0.508F;
            this.barCode2.Name = "barCode2";
            this.barCode2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2.645833F, 2.645833F, 0F, 0F, 25.4F);
            this.barCode2.SizeF = new System.Drawing.SizeF(31.2F, 12.17F);
            this.barCode2.StylePriority.UseFont = false;
            this.barCode2.StylePriority.UseTextAlignment = false;
            this.barCode2.Symbology = pharmacodeGenerator2;
            this.barCode2.Text = "12345678";
            this.barCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // barCode1
            // 
            this.barCode1.AutoModule = true;
            this.barCode1.Dpi = 25.4F;
            this.barCode1.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.barCode1.LocationFloat = new DevExpress.Utils.PointFloat(74.36445F, 6.854276F);
            this.barCode1.Module = 0.508F;
            this.barCode1.Name = "barCode1";
            this.barCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2.645833F, 2.645833F, 0F, 0F, 25.4F);
            this.barCode1.SizeF = new System.Drawing.SizeF(31.19878F, 12.17141F);
            this.barCode1.StylePriority.UseFont = false;
            this.barCode1.StylePriority.UseTextAlignment = false;
            this.barCode1.Symbology = pharmacodeGenerator3;
            this.barCode1.Text = "123456789";
            this.barCode1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // lbl1
            // 
            this.lbl1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl1.Dpi = 25.4F;
            this.lbl1.Font = new DevExpress.Drawing.DXFont("Samim FD", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lbl1.LocationFloat = new DevExpress.Utils.PointFloat(75.46766F, 3.458388F);
            this.lbl1.Multiline = true;
            this.lbl1.Name = "lbl1";
            this.lbl1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291667F, 0.5291667F, 0F, 0F, 25.4F);
            this.lbl1.SizeF = new System.Drawing.SizeF(28.99235F, 18.96318F);
            this.lbl1.StylePriority.UseBorders = false;
            this.lbl1.StylePriority.UseFont = false;
            this.lbl1.StylePriority.UseTextAlignment = false;
            this.lbl1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl1.Visible = false;
            // 
            // lbl2
            // 
            this.lbl2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl2.Dpi = 25.4F;
            this.lbl2.Font = new DevExpress.Drawing.DXFont("Samim FD", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lbl2.LocationFloat = new DevExpress.Utils.PointFloat(37.99726F, 3.458388F);
            this.lbl2.Multiline = true;
            this.lbl2.Name = "lbl2";
            this.lbl2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291667F, 0.5291667F, 0F, 0F, 25.4F);
            this.lbl2.SizeF = new System.Drawing.SizeF(28.95674F, 18.96318F);
            this.lbl2.StylePriority.UseBorders = false;
            this.lbl2.StylePriority.UseFont = false;
            this.lbl2.StylePriority.UseTextAlignment = false;
            this.lbl2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl2.Visible = false;
            // 
            // lbl3
            // 
            this.lbl3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl3.Dpi = 25.4F;
            this.lbl3.Font = new DevExpress.Drawing.DXFont("Samim FD", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lbl3.LocationFloat = new DevExpress.Utils.PointFloat(2.408803F, 3.458388F);
            this.lbl3.Multiline = true;
            this.lbl3.Name = "lbl3";
            this.lbl3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291667F, 0.5291667F, 0F, 0F, 25.4F);
            this.lbl3.SizeF = new System.Drawing.SizeF(27.25119F, 18.96318F);
            this.lbl3.StylePriority.UseBorders = false;
            this.lbl3.StylePriority.UseFont = false;
            this.lbl3.StylePriority.UseTextAlignment = false;
            this.lbl3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl3.Visible = false;
            // 
            // lblPrint
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 25.4F;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(0F, 0F, 0F, 0F);
            this.PageHeightF = 22.8F;
            this.PageWidthF = 107F;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters;
            this.SnapGridSize = 1F;
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lbl3;
        private DevExpress.XtraReports.UI.XRLabel lbl1;
        private DevExpress.XtraReports.UI.XRLabel lbl2;
        private DevExpress.XtraReports.UI.XRBarCode barCode2;
        private DevExpress.XtraReports.UI.XRBarCode barCode1;
        private DevExpress.XtraReports.UI.XRBarCode barCode3;
    }
}
