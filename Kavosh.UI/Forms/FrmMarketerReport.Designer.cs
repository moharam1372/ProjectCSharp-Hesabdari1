namespace Kavosh.UI.Forms
{
    partial class FrmMarketerReport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlTop = new Panel();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            srcGrid = new DevExpress.XtraEditors.SearchControl();
            dgvMarketerReport = new MyCom.Object.KavoshGrid(components);
            viewMarketerReport = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarketerReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketerReport).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(255, 255, 192);
            pnlTop.Controls.Add(srcGrid);
            pnlTop.Controls.Add(btnExportExcel);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(900, 38);
            pnlTop.TabIndex = 0;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnExportExcel.Appearance.Options.UseFont = true;
            btnExportExcel.Dock = DockStyle.Left;
            btnExportExcel.ImageOptions.SvgImage = Properties.Resources.exporttoxlsx;
            btnExportExcel.Location = new Point(0, 0);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(149, 38);
            btnExportExcel.TabIndex = 1;
            btnExportExcel.Text = "خروجی اکسل";
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // srcGrid
            // 
            srcGrid.Anchor = AnchorStyles.None;
            srcGrid.Client = dgvMarketerReport;
            srcGrid.EditValue = "";
            srcGrid.Location = new Point(327, 3);
            srcGrid.Name = "srcGrid";
            srcGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            srcGrid.Properties.Client = dgvMarketerReport;
            srcGrid.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            srcGrid.RightToLeft = RightToLeft.Yes;
            srcGrid.Size = new Size(245, 28);
            srcGrid.TabIndex = 2;
            // 
            // dgvMarketerReport
            // 
            dgvMarketerReport.Dock = DockStyle.Fill;
            dgvMarketerReport.Location = new Point(0, 38);
            dgvMarketerReport.MainView = viewMarketerReport;
            dgvMarketerReport.Name = "dgvMarketerReport";
            dgvMarketerReport.RightToLeft = RightToLeft.Yes;
            dgvMarketerReport.Size = new Size(900, 462);
            dgvMarketerReport.TabIndex = 1;
            dgvMarketerReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewMarketerReport });
            // 
            // viewMarketerReport
            // 
            viewMarketerReport.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewMarketerReport.GridControl = dgvMarketerReport;
            viewMarketerReport.Name = "viewMarketerReport";
            viewMarketerReport.OptionsBehavior.Editable = false;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // FrmMarketerReport
            // 
            ClientSize = new Size(900, 500);
            Controls.Add(dgvMarketerReport);
            Controls.Add(pnlTop);
            Name = "FrmMarketerReport";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "گزارش بازاریاب";
            Load += FrmMarketerReport_Load;
            pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarketerReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketerReport).EndInit();
            ResumeLayout(false);
        }

        private Panel pnlTop;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SearchControl srcGrid;
        private MyCom.Object.KavoshGrid dgvMarketerReport;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewMarketerReport;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}