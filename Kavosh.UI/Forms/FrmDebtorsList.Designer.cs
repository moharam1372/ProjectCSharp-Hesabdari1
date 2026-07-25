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
            pnlSummary.BackColor = Color.FromArgb(255, 255, 192);
            pnlSummary.Controls.Add(lblCountValue);
            pnlSummary.Controls.Add(lblCountTitle);
            pnlSummary.Controls.Add(lblOtherDebtValue);
            pnlSummary.Controls.Add(lblOtherDebtTitle);
            pnlSummary.Controls.Add(lblCheckDebtValue);
            pnlSummary.Controls.Add(lblCheckDebtTitle);
            pnlSummary.Controls.Add(lblTotalValue);
            pnlSummary.Controls.Add(lblTotalTitle);
            pnlSummary.Dock = DockStyle.Top;
            pnlSummary.Location = new Point(0, 0);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(1012, 36);
            pnlSummary.TabIndex = 2;
            pnlSummary.Paint += pnlSummary_Paint;
            // 
            // lblCountValue
            // 
            lblCountValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblCountValue.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblCountValue.Appearance.Options.UseFont = true;
            lblCountValue.Location = new Point(888, 7);
            lblCountValue.Name = "lblCountValue";
            lblCountValue.Size = new Size(6, 23);
            lblCountValue.TabIndex = 0;
            lblCountValue.Text = "0";
            // 
            // lblCountTitle
            // 
            lblCountTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblCountTitle.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblCountTitle.Appearance.Options.UseFont = true;
            lblCountTitle.Location = new Point(910, 7);
            lblCountTitle.Name = "lblCountTitle";
            lblCountTitle.Size = new Size(92, 23);
            lblCountTitle.TabIndex = 1;
            lblCountTitle.Text = "تعداد بدهکاران:";
            // 
            // lblOtherDebtValue
            // 
            lblOtherDebtValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblOtherDebtValue.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblOtherDebtValue.Appearance.Options.UseFont = true;
            lblOtherDebtValue.Location = new Point(413, 7);
            lblOtherDebtValue.Name = "lblOtherDebtValue";
            lblOtherDebtValue.Size = new Size(6, 23);
            lblOtherDebtValue.TabIndex = 2;
            lblOtherDebtValue.Text = "0";
            // 
            // lblOtherDebtTitle
            // 
            lblOtherDebtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblOtherDebtTitle.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblOtherDebtTitle.Appearance.Options.UseFont = true;
            lblOtherDebtTitle.Location = new Point(478, 7);
            lblOtherDebtTitle.Name = "lblOtherDebtTitle";
            lblOtherDebtTitle.Size = new Size(120, 23);
            lblOtherDebtTitle.TabIndex = 3;
            lblOtherDebtTitle.Text = "جمع بدهی غیرچکی:";
            // 
            // lblCheckDebtValue
            // 
            lblCheckDebtValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblCheckDebtValue.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblCheckDebtValue.Appearance.ForeColor = Color.DarkOrange;
            lblCheckDebtValue.Appearance.Options.UseFont = true;
            lblCheckDebtValue.Appearance.Options.UseForeColor = true;
            lblCheckDebtValue.Location = new Point(189, 7);
            lblCheckDebtValue.Name = "lblCheckDebtValue";
            lblCheckDebtValue.Size = new Size(6, 23);
            lblCheckDebtValue.TabIndex = 4;
            lblCheckDebtValue.Text = "0";
            lblCheckDebtValue.Click += lblCheckDebtValue_Click;
            // 
            // lblCheckDebtTitle
            // 
            lblCheckDebtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblCheckDebtTitle.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblCheckDebtTitle.Appearance.ForeColor = Color.DarkOrange;
            lblCheckDebtTitle.Appearance.Options.UseFont = true;
            lblCheckDebtTitle.Appearance.Options.UseForeColor = true;
            lblCheckDebtTitle.Location = new Point(240, 7);
            lblCheckDebtTitle.Name = "lblCheckDebtTitle";
            lblCheckDebtTitle.Size = new Size(102, 23);
            lblCheckDebtTitle.TabIndex = 5;
            lblCheckDebtTitle.Text = "جمع بدهی چکی:";
            // 
            // lblTotalValue
            // 
            lblTotalValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalValue.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblTotalValue.Appearance.ForeColor = Color.Firebrick;
            lblTotalValue.Appearance.Options.UseFont = true;
            lblTotalValue.Appearance.Options.UseForeColor = true;
            lblTotalValue.Location = new Point(674, 7);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(6, 23);
            lblTotalValue.TabIndex = 6;
            lblTotalValue.Text = "0";
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalTitle.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            lblTotalTitle.Appearance.Options.UseFont = true;
            lblTotalTitle.Location = new Point(747, 7);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(91, 23);
            lblTotalTitle.TabIndex = 7;
            lblTotalTitle.Text = "جمع کل بدهی:";
            // 
            // dgvDebtors
            // 
            dgvDebtors.Dock = DockStyle.Fill;
            dgvDebtors.Location = new Point(0, 36);
            dgvDebtors.MainView = viewDebtors;
            dgvDebtors.Name = "dgvDebtors";
            dgvDebtors.RightToLeft = RightToLeft.Yes;
            dgvDebtors.Size = new Size(1012, 553);
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
            ClientSize = new Size(1012, 589);
            Controls.Add(dgvDebtors);
            Controls.Add(pnlSummary);
            Name = "FrmDebtorsList";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "لیست بدهکاران";
            Load += FrmDebtorsList_Load;
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
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