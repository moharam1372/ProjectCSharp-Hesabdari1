namespace Kavosh.UI.Forms
{
    partial class FrmProductKardex
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
            pnlTop = new Panel();
            lblStockValue = new DevExpress.XtraEditors.LabelControl();
            lblStockTitle = new DevExpress.XtraEditors.LabelControl();
            lblProductTitle = new DevExpress.XtraEditors.LabelControl();
            dgvKardex = new MyCom.Object.KavoshGrid(components);
            viewKardex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();

            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKardex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewKardex).BeginInit();
            SuspendLayout();

            //
            // pnlTop
            //
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 45;
            pnlTop.Controls.Add(lblStockValue);
            pnlTop.Controls.Add(lblStockTitle);
            pnlTop.Controls.Add(lblProductTitle);
            pnlTop.Name = "pnlTop";

            //
            // lblProductTitle
            //
            lblProductTitle.Location = new Point(12, 15);
            lblProductTitle.Name = "lblProductTitle";
            lblProductTitle.Text = "کالا: ...";
            lblProductTitle.Appearance.Font = new Font(lblProductTitle.Appearance.Font, FontStyle.Bold);

            //
            // lblStockTitle
            //
            lblStockTitle.Location = new Point(340, 15);
            lblStockTitle.Name = "lblStockTitle";
            lblStockTitle.Text = "موجودی فعلی:";

            //
            // lblStockValue
            //
            lblStockValue.Location = new Point(420, 15);
            lblStockValue.Name = "lblStockValue";
            lblStockValue.Text = "0";
            lblStockValue.Appearance.Font = new Font(lblStockValue.Appearance.Font, FontStyle.Bold);

            //
            // dgvKardex
            //
            dgvKardex.Dock = DockStyle.Fill;
            dgvKardex.Location = new Point(0, 45);
            dgvKardex.MainView = viewKardex;
            dgvKardex.Name = "dgvKardex";
            dgvKardex.RightToLeft = RightToLeft.Yes;
            dgvKardex.TabIndex = 1;
            dgvKardex.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewKardex });

            //
            // viewKardex
            //
            viewKardex.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewKardex.GridControl = dgvKardex;
            viewKardex.Name = "viewKardex";
            viewKardex.OptionsBehavior.Editable = false;

            //
            // gridBand1
            //
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;

            //
            // FrmProductKardex
            //
            //AutoScaleDimensions = new SizeF(6F, 13F);
            //AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 550);
            Controls.Add(dgvKardex);
            Controls.Add(pnlTop);
            Name = "FrmProductKardex";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "کاردکس کالا";
            Load += FrmProductKardex_Load;

            pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKardex).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewKardex).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private DevExpress.XtraEditors.LabelControl lblProductTitle;
        private DevExpress.XtraEditors.LabelControl lblStockTitle;
        private DevExpress.XtraEditors.LabelControl lblStockValue;
        private MyCom.Object.KavoshGrid dgvKardex;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewKardex;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}