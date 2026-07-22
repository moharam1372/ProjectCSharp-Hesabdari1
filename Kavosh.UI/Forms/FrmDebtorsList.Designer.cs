namespace Kavosh.UI.Forms
{
    partial class FrmDebtorsList
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
            pnlSummary = new Panel();
            lblCountValue = new DevExpress.XtraEditors.LabelControl();
            lblCountTitle = new DevExpress.XtraEditors.LabelControl();
            lblOtherDebtValue = new DevExpress.XtraEditors.LabelControl();
            lblOtherDebtTitle = new DevExpress.XtraEditors.LabelControl();
            lblCheckDebtValue = new DevExpress.XtraEditors.LabelControl();
            lblCheckDebtTitle = new DevExpress.XtraEditors.LabelControl();
            lblTotalValue = new DevExpress.XtraEditors.LabelControl();
            lblTotalTitle = new DevExpress.XtraEditors.LabelControl();
            dgvDebtors = new MyCom.Object.KavoshGrid(components);
            viewDebtors = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();

            pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDebtors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDebtors).BeginInit();
            SuspendLayout();

            //
            // pnlSummary
            //
            pnlSummary.Dock = DockStyle.Top;
            pnlSummary.Height = 55;
            pnlSummary.Controls.Add(lblCountValue);
            pnlSummary.Controls.Add(lblCountTitle);
            pnlSummary.Controls.Add(lblOtherDebtValue);
            pnlSummary.Controls.Add(lblOtherDebtTitle);
            pnlSummary.Controls.Add(lblCheckDebtValue);
            pnlSummary.Controls.Add(lblCheckDebtTitle);
            pnlSummary.Controls.Add(lblTotalValue);
            pnlSummary.Controls.Add(lblTotalTitle);
            pnlSummary.Name = "pnlSummary";

            //
            // lblCountTitle / lblCountValue
            //
            lblCountTitle.Location = new Point(12, 18);
            lblCountTitle.Text = "تعداد بدهکاران:";
            lblCountValue.Location = new Point(110, 18);
            lblCountValue.Text = "0";
            lblCountValue.Appearance.Font = new Font(lblCountValue.Appearance.Font, FontStyle.Bold);

            //
            // lblCheckDebtTitle / lblCheckDebtValue
            //
            lblCheckDebtTitle.Location = new Point(220, 18);
            lblCheckDebtTitle.Text = "جمع بدهی چکی:";
            lblCheckDebtTitle.Appearance.ForeColor = Color.DarkOrange;
            lblCheckDebtValue.Location = new Point(330, 18);
            lblCheckDebtValue.Text = "0";
            lblCheckDebtValue.Appearance.Font = new Font(lblCheckDebtValue.Appearance.Font, FontStyle.Bold);
            lblCheckDebtValue.Appearance.ForeColor = Color.DarkOrange;

            //
            // lblOtherDebtTitle / lblOtherDebtValue
            //
            lblOtherDebtTitle.Location = new Point(460, 18);
            lblOtherDebtTitle.Text = "جمع بدهی غیرچکی:";
            lblOtherDebtValue.Location = new Point(580, 18);
            lblOtherDebtValue.Text = "0";
            lblOtherDebtValue.Appearance.Font = new Font(lblOtherDebtValue.Appearance.Font, FontStyle.Bold);

            //
            // lblTotalTitle / lblTotalValue
            //
            lblTotalTitle.Location = new Point(710, 18);
            lblTotalTitle.Text = "جمع کل بدهی:";
            lblTotalTitle.Appearance.Font = new Font(lblTotalTitle.Appearance.Font, FontStyle.Bold);
            lblTotalValue.Location = new Point(810, 18);
            lblTotalValue.Text = "0";
            lblTotalValue.Appearance.Font = new Font(lblTotalValue.Appearance.Font, FontStyle.Bold);
            lblTotalValue.Appearance.ForeColor = Color.Firebrick;

            //
            // dgvDebtors
            //
            dgvDebtors.Dock = DockStyle.Fill;
            dgvDebtors.Location = new Point(0, 55);
            dgvDebtors.MainView = viewDebtors;
            dgvDebtors.Name = "dgvDebtors";
            dgvDebtors.RightToLeft = RightToLeft.Yes;
            dgvDebtors.TabIndex = 1;
            dgvDebtors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewDebtors });

            //
            // viewDebtors
            //
            viewDebtors.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewDebtors.GridControl = dgvDebtors;
            viewDebtors.Name = "viewDebtors";
            viewDebtors.OptionsBehavior.Editable = false;

            //
            // gridBand1
            //
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;

            //
            // FrmDebtorsList
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvDebtors);
            Controls.Add(pnlSummary);
            Name = "FrmDebtorsList";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "لیست بدهکاران";
            Load += FrmDebtorsList_Load;

            pnlSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDebtors).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDebtors).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSummary;
        private DevExpress.XtraEditors.LabelControl lblCountTitle;
        private DevExpress.XtraEditors.LabelControl lblCountValue;
        private DevExpress.XtraEditors.LabelControl lblCheckDebtTitle;
        private DevExpress.XtraEditors.LabelControl lblCheckDebtValue;
        private DevExpress.XtraEditors.LabelControl lblOtherDebtTitle;
        private DevExpress.XtraEditors.LabelControl lblOtherDebtValue;
        private DevExpress.XtraEditors.LabelControl lblTotalTitle;
        private DevExpress.XtraEditors.LabelControl lblTotalValue;
        private MyCom.Object.KavoshGrid dgvDebtors;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewDebtors;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}