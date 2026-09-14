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
            pnlTop.BackColor = Color.FromArgb(255, 255, 192);
            pnlTop.Controls.Add(lblStockValue);
            pnlTop.Controls.Add(lblStockTitle);
            pnlTop.Controls.Add(lblProductTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(995, 33);
            pnlTop.TabIndex = 2;
            // 
            // lblStockValue
            // 
            lblStockValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStockValue.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblStockValue.Appearance.Options.UseFont = true;
            lblStockValue.Location = new Point(493, 6);
            lblStockValue.Name = "lblStockValue";
            lblStockValue.Size = new Size(6, 20);
            lblStockValue.TabIndex = 0;
            lblStockValue.Text = "0";
            // 
            // lblStockTitle
            // 
            lblStockTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStockTitle.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblStockTitle.Appearance.Options.UseFont = true;
            lblStockTitle.Location = new Point(550, 6);
            lblStockTitle.Name = "lblStockTitle";
            lblStockTitle.Size = new Size(76, 20);
            lblStockTitle.TabIndex = 1;
            lblStockTitle.Text = "موجودی فعلی:";
            // 
            // lblProductTitle
            // 
            lblProductTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblProductTitle.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblProductTitle.Appearance.Options.UseFont = true;
            lblProductTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblProductTitle.Location = new Point(632, 6);
            lblProductTitle.Name = "lblProductTitle";
            lblProductTitle.Size = new Size(355, 20);
            lblProductTitle.TabIndex = 2;
            lblProductTitle.Text = "کالا: ...";
            // 
            // dgvKardex
            // 
            dgvKardex.Dock = DockStyle.Fill;
            dgvKardex.Location = new Point(0, 33);
            dgvKardex.MainView = viewKardex;
            dgvKardex.Name = "dgvKardex";
            dgvKardex.RightToLeft = RightToLeft.Yes;
            dgvKardex.Size = new Size(995, 544);
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
            ClientSize = new Size(995, 577);
            Controls.Add(dgvKardex);
            Controls.Add(pnlTop);
            Name = "FrmProductKardex";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "کاردکس کالا";
            Load += FrmProductKardex_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
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