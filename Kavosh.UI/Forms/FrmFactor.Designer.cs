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
            dgvHowToPay = new MyCom.Object.KavoshGrid(components);
            viewHowToPay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            dgvFactorDetail = new MyCom.Object.KavoshGrid(components);
            viewFactorDetail = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlHowToPay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHowToPay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewHowToPay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFactorDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewFactorDetail).BeginInit();
            SuspendLayout();
            // 
            // pnlHowToPay
            // 
            pnlHowToPay.BackColor = Color.FromArgb(192, 192, 255);
            pnlHowToPay.Controls.Add(dgvHowToPay);
            pnlHowToPay.Dock = DockStyle.Bottom;
            pnlHowToPay.Location = new Point(0, 457);
            pnlHowToPay.Name = "pnlHowToPay";
            pnlHowToPay.Size = new Size(1147, 224);
            pnlHowToPay.TabIndex = 1;
            // 
            // dgvHowToPay
            // 
            dgvHowToPay.Dock = DockStyle.Fill;
            dgvHowToPay.Location = new Point(0, 0);
            dgvHowToPay.MainView = viewHowToPay;
            dgvHowToPay.Name = "dgvHowToPay";
            dgvHowToPay.RightToLeft = RightToLeft.Yes;
            dgvHowToPay.Size = new Size(1147, 224);
            dgvHowToPay.TabIndex = 1;
            dgvHowToPay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewHowToPay });
            // 
            // viewHowToPay
            // 
            viewHowToPay.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewHowToPay.GridControl = dgvHowToPay;
            viewHowToPay.Name = "viewHowToPay";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand1";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.IsSplitterFixed = true;
            splitContainerControl1.Location = new Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Appearance.BackColor = Color.LightCyan;
            splitContainerControl1.Panel1.Appearance.Options.UseBackColor = true;
            splitContainerControl1.Panel1.Controls.Add(layInput);
            splitContainerControl1.Panel1.Controls.Add(pnlFunction);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(dgvFactorDetail);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1147, 457);
            splitContainerControl1.SplitterPosition = 382;
            splitContainerControl1.TabIndex = 0;
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 33);
            layInput.Name = "layInput";
            layInput.OptionsView.RightToLeftMirroringApplied = true;
            layInput.Root = Root;
            layInput.Size = new Size(382, 424);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayoutFactor";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(382, 424);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(382, 33);
            pnlFunction.TabIndex = 2;
            // 
            // dgvFactorDetail
            // 
            dgvFactorDetail.Dock = DockStyle.Fill;
            dgvFactorDetail.Location = new Point(0, 0);
            dgvFactorDetail.MainView = viewFactorDetail;
            dgvFactorDetail.Name = "dgvFactorDetail";
            dgvFactorDetail.RightToLeft = RightToLeft.Yes;
            dgvFactorDetail.Size = new Size(749, 457);
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
            // FrmFactor
            // 
            //AutoScaleDimensions = new SizeF(6F, 13F);
            //AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 681);
            Controls.Add(splitContainerControl1);
            Controls.Add(pnlHowToPay);
            Name = "FrmFactor";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "فاکتور";
            Load += FrmFactor_Load;
            pnlHowToPay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHowToPay).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewHowToPay).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFactorDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewFactorDetail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHowToPay;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshGrid dgvFactorDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewFactorDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlFunction;
        private MyCom.Object.KavoshGrid dgvHowToPay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewHowToPay;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
    }
}