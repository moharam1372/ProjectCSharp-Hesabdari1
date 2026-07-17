namespace Kavosh.UI.Forms
{
    partial class FrmFactor
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
            pnlHowToPay = new Panel();
            lblHowToPay = new DevExpress.XtraEditors.LabelControl();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvFactorDetail = new MyCom.Object.KavoshGrid(components);
            viewFactorDetail = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            pnlHowToPay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFactorDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewFactorDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            SuspendLayout();
            // 
            // pnlHowToPay
            // 
            pnlHowToPay.Controls.Add(lblHowToPay);
            pnlHowToPay.Dock = DockStyle.Bottom;
            pnlHowToPay.Location = new Point(0, 491);
            pnlHowToPay.Name = "pnlHowToPay";
            pnlHowToPay.Size = new Size(1147, 140);
            pnlHowToPay.TabIndex = 1;
            // 
            // lblHowToPay
            // 
            lblHowToPay.Location = new Point(12, 12);
            lblHowToPay.Name = "lblHowToPay";
            lblHowToPay.Size = new Size(216, 13);
            lblHowToPay.TabIndex = 0;
            lblHowToPay.Text = "نحوه‌ی پرداخت (HowToPay) — در دست ساخت";
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            splitContainerControl1.IsSplitterFixed = true;
            splitContainerControl1.Location = new Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(layInput);
            splitContainerControl1.Panel1.Controls.Add(pnlFunction);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(dgvFactorDetail);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1147, 491);
            splitContainerControl1.SplitterPosition = 725;
            splitContainerControl1.TabIndex = 0;
            // 
            // dgvFactorDetail
            // 
            dgvFactorDetail.Dock = DockStyle.Fill;
            dgvFactorDetail.Location = new Point(0, 0);
            dgvFactorDetail.MainView = viewFactorDetail;
            dgvFactorDetail.Name = "dgvFactorDetail";
            dgvFactorDetail.RightToLeft = RightToLeft.Yes;
            dgvFactorDetail.Size = new Size(725, 491);
            dgvFactorDetail.TabIndex = 0;
            dgvFactorDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewFactorDetail });
            // 
            // viewFactorDetail
            // 
            viewFactorDetail.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewFactorDetail.GridControl = dgvFactorDetail;
            viewFactorDetail.Name = "viewFactorDetail";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 33);
            layInput.Name = "layInput";
            layInput.OptionsView.RightToLeftMirroringApplied = true;
            layInput.Root = Root;
            layInput.Size = new Size(406, 458);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayoutFactor";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(406, 458);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(406, 33);
            pnlFunction.TabIndex = 2;
            // 
            // FrmFactor
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 631);
            Controls.Add(splitContainerControl1);
            Controls.Add(pnlHowToPay);
            Name = "FrmFactor";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "فاکتور";
            Load += FrmFactor_Load;
            pnlHowToPay.ResumeLayout(false);
            pnlHowToPay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFactorDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewFactorDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHowToPay;
        private DevExpress.XtraEditors.LabelControl lblHowToPay;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshGrid dgvFactorDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewFactorDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlFunction;
    }
}