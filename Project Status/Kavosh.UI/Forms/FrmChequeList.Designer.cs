namespace Kavosh.UI.Forms
{
    partial class FrmChequeList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvCheque = new MyCom.Object.KavoshGrid(components);
            viewCheque = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlFunction = new Panel();
            srcGrid = new DevExpress.XtraEditors.SearchControl();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)dgvCheque).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewCheque).BeginInit();
            pnlFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).BeginInit();
            SuspendLayout();
            // 
            // dgvCheque
            // 
            dgvCheque.Dock = DockStyle.Fill;
            dgvCheque.Location = new Point(0, 36);
            dgvCheque.MainView = viewCheque;
            dgvCheque.Name = "dgvCheque";
            dgvCheque.RightToLeft = RightToLeft.Yes;
            dgvCheque.Size = new Size(1012, 553);
            dgvCheque.TabIndex = 0;
            dgvCheque.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewCheque });
            // 
            // viewCheque
            // 
            viewCheque.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewCheque.GridControl = dgvCheque;
            viewCheque.Name = "viewCheque";
            viewCheque.OptionsBehavior.Editable = false;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // pnlFunction
            // 
            pnlFunction.BackColor = Color.FromArgb(255, 255, 192);
            pnlFunction.Controls.Add(srcGrid);
            pnlFunction.Controls.Add(btnExportExcel);
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(1012, 36);
            pnlFunction.TabIndex = 1;
            // 
            // srcGrid
            // 
            srcGrid.Anchor = AnchorStyles.None;
            srcGrid.EditValue = "";
            srcGrid.Location = new Point(384, 2);
            srcGrid.Name = "srcGrid";
            srcGrid.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            srcGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            srcGrid.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            srcGrid.Size = new Size(245, 28);
            srcGrid.TabIndex = 7;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportExcel.Appearance.Options.UseFont = true;
            btnExportExcel.Dock = DockStyle.Left;
            btnExportExcel.ImageOptions.SvgImage = Properties.Resources.exporttoxlsx;
            btnExportExcel.Location = new Point(0, 0);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(149, 36);
            btnExportExcel.TabIndex = 6;
            btnExportExcel.Text = "خروجی اکسل";
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // FrmChequeList
            // 
            ClientSize = new Size(1012, 589);
            Controls.Add(dgvCheque);
            Controls.Add(pnlFunction);
            Name = "FrmChequeList";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مدیریت چک‌ها";
            Load += FrmChequeList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCheque).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewCheque).EndInit();
            pnlFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ResumeLayout(false);
        }

        private MyCom.Object.KavoshGrid dgvCheque;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewCheque;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private Panel pnlFunction;
        private DevExpress.XtraEditors.SearchControl srcGrid;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
    }
}