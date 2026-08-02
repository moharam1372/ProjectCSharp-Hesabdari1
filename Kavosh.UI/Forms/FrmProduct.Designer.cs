namespace Kavosh.UI.Forms
{
    partial class FrmProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvProduct = new MyCom.Object.KavoshGrid(components);
            viewProduct = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
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
            ((System.ComponentModel.ISupportInitialize)dgvProduct).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewProduct).BeginInit();
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
            splitContainerControl1.Panel1.Controls.Add(dgvProduct);
            splitContainerControl1.Panel1.Controls.Add(panel1);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(layInput);
            splitContainerControl1.Panel2.Controls.Add(pnlFunction);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1113, 608);
            splitContainerControl1.SplitterPosition = 384;
            splitContainerControl1.TabIndex = 0;
            // 
            // dgvProduct
            // 
            dgvProduct.Dock = DockStyle.Fill;
            dgvProduct.Location = new Point(0, 36);
            dgvProduct.MainView = viewProduct;
            dgvProduct.Name = "dgvProduct";
            dgvProduct.RightToLeft = RightToLeft.Yes;
            dgvProduct.Size = new Size(713, 572);
            dgvProduct.TabIndex = 0;
            dgvProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewProduct });
            // 
            // viewProduct
            // 
            viewProduct.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewProduct.GridControl = dgvProduct;
            viewProduct.Name = "viewProduct";
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
            panel1.Size = new Size(713, 36);
            panel1.TabIndex = 1;
            // 
            // srcGrid
            // 
            srcGrid.Anchor = AnchorStyles.None;
            srcGrid.Client = dgvProduct;
            srcGrid.EditValue = "";
            srcGrid.Location = new Point(234, 2);
            srcGrid.Name = "srcGrid";
            srcGrid.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            srcGrid.Properties.Appearance.Options.UseTextOptions = true;
            srcGrid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            srcGrid.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            srcGrid.Properties.AppearanceItemHighlight.BackColor = Color.FromArgb(255, 255, 128);
            srcGrid.Properties.AppearanceItemHighlight.Options.UseBackColor = true;
            srcGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            srcGrid.Properties.Client = dgvProduct;
            srcGrid.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            srcGrid.Properties.FindDelay = 200;
            srcGrid.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            srcGrid.RightToLeft = RightToLeft.Yes;
            srcGrid.Size = new Size(245, 28);
            srcGrid.TabIndex = 8;
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 33);
            layInput.Name = "layInput";
            layInput.Root = Root;
            layInput.Size = new Size(384, 575);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayout1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(384, 575);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(384, 33);
            pnlFunction.TabIndex = 2;
            // 
            // FrmProduct
            // 
            ClientSize = new Size(1113, 608);
            Controls.Add(splitContainerControl1);
            Name = "FrmProduct";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مدیریت محصولات";
            Load += FrmProduct_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProduct).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewProduct).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlFunction;
        private MyCom.Object.KavoshGrid dgvProduct;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewProduct;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private Panel panel1;
        private DevExpress.XtraEditors.SearchControl srcGrid;
    }
}