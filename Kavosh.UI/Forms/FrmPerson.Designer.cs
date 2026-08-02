namespace Kavosh.UI.Forms
{
    partial class FrmPerson
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
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvPerson = new MyCom.Object.KavoshGrid(components);
            viewPerson = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            panel1 = new Panel();
            srcGrid = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPerson).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPerson).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).BeginInit();
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
            splitContainerControl1.Panel1.Controls.Add(dgvPerson);
            splitContainerControl1.Panel1.Controls.Add(panel1);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(layInput);
            splitContainerControl1.Panel2.Controls.Add(pnlFunction);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1092, 629);
            splitContainerControl1.SplitterPosition = 384;
            splitContainerControl1.TabIndex = 0;
            // 
            // dgvPerson
            // 
            dgvPerson.Dock = DockStyle.Fill;
            dgvPerson.Location = new Point(0, 36);
            dgvPerson.MainView = viewPerson;
            dgvPerson.Name = "dgvPerson";
            dgvPerson.RightToLeft = RightToLeft.Yes;
            dgvPerson.Size = new Size(692, 593);
            dgvPerson.TabIndex = 0;
            dgvPerson.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPerson });
            // 
            // viewPerson
            // 
            viewPerson.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewPerson.GridControl = dgvPerson;
            viewPerson.Name = "viewPerson";
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
            layInput.Root = Root;
            layInput.Size = new Size(384, 596);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayout1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(384, 596);
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
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 255, 192);
            panel1.Controls.Add(srcGrid);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(692, 36);
            panel1.TabIndex = 2;
            // 
            // srcGrid
            // 
            srcGrid.Anchor = AnchorStyles.None;
            srcGrid.Client = dgvPerson;
            srcGrid.EditValue = "";
            srcGrid.Location = new Point(224, 2);
            srcGrid.Name = "srcGrid";
            srcGrid.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            srcGrid.Properties.Appearance.Options.UseTextOptions = true;
            srcGrid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            srcGrid.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            srcGrid.Properties.AppearanceItemHighlight.BackColor = Color.FromArgb(255, 255, 128);
            srcGrid.Properties.AppearanceItemHighlight.Options.UseBackColor = true;
            srcGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            srcGrid.Properties.Client = dgvPerson;
            srcGrid.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            srcGrid.Properties.FindDelay = 200;
            srcGrid.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            srcGrid.RightToLeft = RightToLeft.Yes;
            srcGrid.Size = new Size(245, 28);
            srcGrid.TabIndex = 8;
            // 
            // FrmPerson
            // 
            ClientSize = new Size(1092, 629);
            Controls.Add(splitContainerControl1);
            Name = "FrmPerson";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مدیریت مشتریان";
            Load += FrmPerson_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPerson).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPerson).EndInit();
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlFunction;
        private MyCom.Object.KavoshGrid dgvPerson;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPerson;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private Panel panel1;
        private DevExpress.XtraEditors.SearchControl srcGrid;
    }
}