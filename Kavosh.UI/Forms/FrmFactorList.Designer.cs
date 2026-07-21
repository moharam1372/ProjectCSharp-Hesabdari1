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
            btnNew = new DevExpress.XtraEditors.SimpleButton();
            dgvFactor = new MyCom.Object.KavoshGrid(components);
            viewFactor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewFactor).BeginInit();
            SuspendLayout();
            // 
            // pnlFunction
            // 
            pnlFunction.Controls.Add(btnNew);
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(1000, 36);
            pnlFunction.TabIndex = 0;
            // 
            // btnNew
            // 
            btnNew.Dock = DockStyle.Right;
            btnNew.Location = new Point(874, 0);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(126, 36);
            btnNew.TabIndex = 0;
            btnNew.Text = "فاکتور جدید";
            btnNew.Click += btnNew_Click;
            // 
            // dgvFactor
            // 
            dgvFactor.Dock = DockStyle.Fill;
            dgvFactor.Location = new Point(0, 36);
            dgvFactor.MainView = viewFactor;
            dgvFactor.Name = "dgvFactor";
            dgvFactor.RightToLeft = RightToLeft.Yes;
            dgvFactor.Size = new Size(1000, 562);
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
            // FrmFactorList
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 598);
            Controls.Add(dgvFactor);
            Controls.Add(pnlFunction);
            Name = "FrmFactorList";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "لیست فاکتورها";
            Load += FrmFactorList_Load;
            pnlFunction.ResumeLayout(false);
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
    }
}