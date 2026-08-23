namespace Kavosh.UI.Forms
{
    partial class FrmMarketer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvMarketer = new MyCom.Object.KavoshGrid(components);
            viewMarketer = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel1 = new Panel();
            srcGrid = new DevExpress.XtraEditors.SearchControl();
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMarketer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketer).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            SuspendLayout();
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
            splitContainerControl1.Panel2.Controls.Add(panel1);
            splitContainerControl1.Panel2.Controls.Add(dgvMarketer);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1198, 590);
            splitContainerControl1.SplitterPosition = 744;
            splitContainerControl1.TabIndex = 0;
            // 
            // dgvMarketer
            // 
            dgvMarketer.Dock = DockStyle.Fill;
            dgvMarketer.Location = new Point(0, 0);
            dgvMarketer.MainView = viewMarketer;
            dgvMarketer.Name = "dgvMarketer";
            dgvMarketer.RightToLeft = RightToLeft.Yes;
            dgvMarketer.Size = new Size(744, 590);
            dgvMarketer.TabIndex = 0;
            dgvMarketer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewMarketer });
            // 
            // viewMarketer
            // 
            viewMarketer.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewMarketer.GridControl = dgvMarketer;
            viewMarketer.Name = "viewMarketer";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 255, 192);
            panel1.Controls.Add(srcGrid);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(744, 36);
            panel1.TabIndex = 1;
            // 
            // srcGrid
            // 
            srcGrid.Anchor = AnchorStyles.None;
            srcGrid.Client = dgvMarketer;
            srcGrid.EditValue = "";
            srcGrid.Location = new Point(251, 3);
            srcGrid.Name = "srcGrid";
            srcGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            srcGrid.Properties.Client = dgvMarketer;
            srcGrid.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            srcGrid.RightToLeft = RightToLeft.Yes;
            srcGrid.Size = new Size(245, 20);
            srcGrid.TabIndex = 2;
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 33);
            layInput.Name = "layInput";
            layInput.OptionsView.RightToLeftMirroringApplied = true;
            layInput.Root = Root;
            layInput.Size = new Size(444, 557);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayoutMarketer";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(444, 557);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(444, 33);
            pnlFunction.TabIndex = 2;
            // 
            // FrmMarketer
            // 
            ClientSize = new Size(1198, 590);
            Controls.Add(splitContainerControl1);
            Name = "FrmMarketer";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مدیریت بازاریاب";
            Load += FrmMarketer_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMarketer).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketer).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlFunction;
        private MyCom.Object.KavoshGrid dgvMarketer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewMarketer;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private Panel panel1;
        private DevExpress.XtraEditors.SearchControl srcGrid;
    }
}