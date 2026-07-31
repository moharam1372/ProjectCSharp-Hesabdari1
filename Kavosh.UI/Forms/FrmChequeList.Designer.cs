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
            ((System.ComponentModel.ISupportInitialize)dgvCheque).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewCheque).BeginInit();
            SuspendLayout();
            // 
            // dgvCheque
            // 
            dgvCheque.Dock = DockStyle.Fill;
            dgvCheque.Location = new Point(0, 0);
            dgvCheque.MainView = viewCheque;
            dgvCheque.Name = "dgvCheque";
            dgvCheque.RightToLeft = RightToLeft.Yes;
            dgvCheque.Size = new Size(1012, 589);
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
            // FrmChequeList
            // 
            ClientSize = new Size(1012, 589);
            Controls.Add(dgvCheque);
            Name = "FrmChequeList";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مدیریت چک‌ها";
            Load += FrmChequeList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCheque).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewCheque).EndInit();
            ResumeLayout(false);
        }

        private MyCom.Object.KavoshGrid dgvCheque;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewCheque;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}