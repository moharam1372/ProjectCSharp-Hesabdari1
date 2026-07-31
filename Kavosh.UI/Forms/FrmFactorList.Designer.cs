namespace Kavosh.UI.Forms
{
    partial class FrmFactorList
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlFunction = new Panel();
            srcGrid = new DevExpress.XtraEditors.SearchControl();
            dgvFactor = new MyCom.Object.KavoshGrid(components);
            viewFactor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnNew = new DevExpress.XtraEditors.SimpleButton();
            pnlFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewFactor).BeginInit();
            SuspendLayout();
            // 
            // pnlFunction
            // 
            pnlFunction.BackColor = Color.FromArgb(255, 255, 192);
            pnlFunction.Controls.Add(srcGrid);
            pnlFunction.Controls.Add(btnExportExcel);
            pnlFunction.Controls.Add(btnNew);
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(966, 36);
            pnlFunction.TabIndex = 0;
            // 
            // srcGrid
            // 
            srcGrid.Anchor = AnchorStyles.None;
            srcGrid.Client = dgvFactor;
            srcGrid.EditValue = "";
            srcGrid.Location = new Point(361, 2);
            srcGrid.Name = "srcGrid";
            srcGrid.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            srcGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            srcGrid.Properties.Client = dgvFactor;
            srcGrid.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            srcGrid.Size = new Size(245, 28);
            srcGrid.TabIndex = 7;
            // 
            // dgvFactor
            // 
            dgvFactor.Dock = DockStyle.Fill;
            dgvFactor.Location = new Point(0, 36);
            dgvFactor.MainView = viewFactor;
            dgvFactor.Name = "dgvFactor";
            dgvFactor.RightToLeft = RightToLeft.Yes;
            dgvFactor.Size = new Size(966, 533);
            dgvFactor.TabIndex = 1;
            dgvFactor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewFactor });
            // 
            // viewFactor
            // 
            viewFactor.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewFactor.GridControl = dgvFactor;
            viewFactor.Name = "viewFactor";
            viewFactor.OptionsBehavior.Editable = false;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
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
            // btnNew
            // 
            btnNew.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnNew.Appearance.Options.UseFont = true;
            btnNew.Dock = DockStyle.Right;
            btnNew.ImageOptions.SvgImage = Properties.Resources.newproduct;
            btnNew.Location = new Point(818, 0);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(148, 36);
            btnNew.TabIndex = 0;
            btnNew.Text = "فاکتور جدید";
            btnNew.Click += btnNew_Click;
            // 
            // FrmFactorList
            // 
            ClientSize = new Size(966, 569);
            Controls.Add(dgvFactor);
            Controls.Add(pnlFunction);
            Name = "FrmFactorList";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "لیست فاکتورها";
            Load += FrmFactorList_Load;
            pnlFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewFactor).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFunction;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private MyCom.Object.KavoshGrid dgvFactor;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewFactor;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SearchControl srcGrid;
    }
}