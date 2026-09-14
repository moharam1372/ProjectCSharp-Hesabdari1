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
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            dgvMarketer = new MyCom.Object.KavoshGrid(components);
            viewMarketer = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel1 = new Panel();
            srcGrid = new DevExpress.XtraEditors.SearchControl();
            tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            dgvMarketerReport = new MyCom.Object.KavoshGrid(components);
            viewMarketerReport = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlTop = new Panel();
            searchControl2 = new DevExpress.XtraEditors.SearchControl();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarketer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketer).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabPane1).BeginInit();
            tabPane1.SuspendLayout();
            tabNavigationPage1.SuspendLayout();
            tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMarketerReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketerReport).BeginInit();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl2.Properties).BeginInit();
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
            splitContainerControl1.Panel2.Controls.Add(dgvMarketer);
            splitContainerControl1.Panel2.Controls.Add(panel1);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1075, 549);
            splitContainerControl1.SplitterPosition = 744;
            splitContainerControl1.TabIndex = 0;
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 33);
            layInput.Name = "layInput";
            layInput.OptionsView.RightToLeftMirroringApplied = true;
            layInput.Root = Root;
            layInput.Size = new Size(315, 516);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayoutMarketer";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(315, 516);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(315, 33);
            pnlFunction.TabIndex = 2;
            // 
            // dgvMarketer
            // 
            dgvMarketer.Dock = DockStyle.Fill;
            dgvMarketer.Location = new Point(0, 36);
            dgvMarketer.MainView = viewMarketer;
            dgvMarketer.Name = "dgvMarketer";
            dgvMarketer.RightToLeft = RightToLeft.Yes;
            dgvMarketer.Size = new Size(744, 513);
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
            srcGrid.Size = new Size(245, 28);
            srcGrid.TabIndex = 2;
            // 
            // tabPane1
            // 
            tabPane1.Controls.Add(tabNavigationPage1);
            tabPane1.Controls.Add(tabNavigationPage2);
            tabPane1.Dock = DockStyle.Fill;
            tabPane1.Location = new Point(0, 0);
            tabPane1.Name = "tabPane1";
            tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { tabNavigationPage1, tabNavigationPage2 });
            tabPane1.RegularSize = new Size(1075, 590);
            tabPane1.SelectedPage = tabNavigationPage1;
            tabPane1.Size = new Size(1075, 590);
            tabPane1.TabIndex = 1;
            tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            tabNavigationPage1.Caption = "مدیریت بازاریاب";
            tabNavigationPage1.Controls.Add(splitContainerControl1);
            tabNavigationPage1.Name = "tabNavigationPage1";
            tabNavigationPage1.Size = new Size(1075, 549);
            // 
            // tabNavigationPage2
            // 
            tabNavigationPage2.Caption = "گزارش";
            tabNavigationPage2.Controls.Add(dgvMarketerReport);
            tabNavigationPage2.Controls.Add(pnlTop);
            tabNavigationPage2.Name = "tabNavigationPage2";
            tabNavigationPage2.Size = new Size(1075, 549);
            // 
            // dgvMarketerReport
            // 
            dgvMarketerReport.Dock = DockStyle.Fill;
            dgvMarketerReport.Location = new Point(0, 38);
            dgvMarketerReport.MainView = viewMarketerReport;
            dgvMarketerReport.Name = "dgvMarketerReport";
            dgvMarketerReport.RightToLeft = RightToLeft.Yes;
            dgvMarketerReport.Size = new Size(1075, 511);
            dgvMarketerReport.TabIndex = 3;
            dgvMarketerReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewMarketerReport });
            // 
            // viewMarketerReport
            // 
            viewMarketerReport.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewMarketerReport.GridControl = dgvMarketerReport;
            viewMarketerReport.Name = "viewMarketerReport";
            viewMarketerReport.OptionsBehavior.Editable = false;
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand1";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(255, 255, 192);
            pnlTop.Controls.Add(searchControl2);
            pnlTop.Controls.Add(btnExportExcel);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1075, 38);
            pnlTop.TabIndex = 2;
            // 
            // searchControl2
            // 
            searchControl2.Anchor = AnchorStyles.None;
            searchControl2.Client = dgvMarketerReport;
            searchControl2.EditValue = "";
            searchControl2.Location = new Point(415, 5);
            searchControl2.Name = "searchControl2";
            searchControl2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl2.Properties.Client = dgvMarketerReport;
            searchControl2.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            searchControl2.RightToLeft = RightToLeft.Yes;
            searchControl2.Size = new Size(245, 28);
            searchControl2.TabIndex = 3;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnExportExcel.Appearance.Options.UseFont = true;
            btnExportExcel.Dock = DockStyle.Left;
            btnExportExcel.ImageOptions.SvgImage = Properties.Resources.exporttoxlsx;
            btnExportExcel.Location = new Point(0, 0);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(149, 38);
            btnExportExcel.TabIndex = 1;
            btnExportExcel.Text = "خروجی اکسل";
            // 
            // FrmMarketer
            // 
            ClientSize = new Size(1075, 590);
            Controls.Add(tabPane1);
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
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarketer).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketer).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)srcGrid.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabPane1).EndInit();
            tabPane1.ResumeLayout(false);
            tabNavigationPage1.ResumeLayout(false);
            tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMarketerReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewMarketerReport).EndInit();
            pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl2.Properties).EndInit();
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
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private MyCom.Object.KavoshGrid dgvMarketerReport;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewMarketerReport;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private Panel pnlTop;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SearchControl searchControl2;
    }
}