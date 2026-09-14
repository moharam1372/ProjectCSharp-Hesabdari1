namespace Kavosh.UI.Forms
{
    partial class FrmCashRegister
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
            tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            tabPurchase = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            dgvPurchaseDocuments = new MyCom.Object.KavoshGrid(components);
            viewPurchaseDocuments = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            dgvPurchaseFactors = new MyCom.Object.KavoshGrid(components);
            viewPurchaseFactors = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlPurchaseFunction = new Panel();
            btnCreatePurchaseDocument = new DevExpress.XtraEditors.SimpleButton();
            tabSale = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            dgvSaleDocuments = new MyCom.Object.KavoshGrid(components);
            viewSaleDocuments = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            dgvSaleFactors = new MyCom.Object.KavoshGrid(components);
            viewSaleFactors = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel2 = new Panel();
            btnCreateSaleDocument = new DevExpress.XtraEditors.SimpleButton();
            tabExpense = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            dgvPartnerBalance = new MyCom.Object.KavoshGrid(components);
            viewPartnerBalance = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            layExpenseInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlExpenseFunction = new Panel();
            tabReport = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            panel4 = new Panel();
            tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            splitContainer1 = new SplitContainer();
            dgvDoc = new MyCom.Object.KavoshGrid(components);
            viewDoc = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel1 = new Panel();
            btnDocFinal = new DevExpress.XtraEditors.SimpleButton();
            cmbSaleDocument = new DevExpress.XtraEditors.ComboBoxEdit();
            panel3 = new Panel();
            lblDocStatus = new Label();
            lblCalcDoc = new Label();
            label1 = new Label();
            dgvDocCalc = new MyCom.Object.KavoshGrid(components);
            viewDocCalc = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel5 = new Panel();
            ((System.ComponentModel.ISupportInitialize)tabPane1).BeginInit();
            tabPane1.SuspendLayout();
            tabPurchase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPurchaseDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseFactors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPurchaseFactors).BeginInit();
            pnlPurchaseFunction.SuspendLayout();
            tabSale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSaleDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewSaleDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSaleFactors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewSaleFactors).BeginInit();
            panel2.SuspendLayout();
            tabExpense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPartnerBalance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPartnerBalance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layExpenseInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            tabReport.SuspendLayout();
            tabNavigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDoc).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbSaleDocument.Properties).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocCalc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDocCalc).BeginInit();
            SuspendLayout();
            // 
            // tabPane1
            // 
            tabPane1.Controls.Add(tabPurchase);
            tabPane1.Controls.Add(tabSale);
            tabPane1.Controls.Add(tabExpense);
            tabPane1.Controls.Add(tabReport);
            tabPane1.Controls.Add(tabNavigationPage1);
            tabPane1.Dock = DockStyle.Fill;
            tabPane1.Location = new Point(0, 0);
            tabPane1.Name = "tabPane1";
            tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { tabPurchase, tabSale, tabExpense, tabNavigationPage1, tabReport });
            tabPane1.RegularSize = new Size(1247, 603);
            tabPane1.RightToLeft = RightToLeft.Yes;
            tabPane1.SelectedPage = tabPurchase;
            tabPane1.Size = new Size(1247, 603);
            tabPane1.TabIndex = 0;
            tabPane1.Text = "tabPane1";
            // 
            // tabPurchase
            // 
            tabPurchase.Caption = "خرید";
            tabPurchase.Controls.Add(dgvPurchaseDocuments);
            tabPurchase.Controls.Add(dgvPurchaseFactors);
            tabPurchase.Controls.Add(pnlPurchaseFunction);
            tabPurchase.Name = "tabPurchase";
            tabPurchase.Size = new Size(1247, 570);
            // 
            // dgvPurchaseDocuments
            // 
            dgvPurchaseDocuments.Dock = DockStyle.Fill;
            dgvPurchaseDocuments.Location = new Point(0, 34);
            dgvPurchaseDocuments.MainView = viewPurchaseDocuments;
            dgvPurchaseDocuments.Name = "dgvPurchaseDocuments";
            dgvPurchaseDocuments.Size = new Size(707, 536);
            dgvPurchaseDocuments.TabIndex = 2;
            dgvPurchaseDocuments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPurchaseDocuments });
            // 
            // viewPurchaseDocuments
            // 
            viewPurchaseDocuments.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewPurchaseDocuments.GridControl = dgvPurchaseDocuments;
            viewPurchaseDocuments.Name = "viewPurchaseDocuments";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand2";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // dgvPurchaseFactors
            // 
            dgvPurchaseFactors.Dock = DockStyle.Right;
            dgvPurchaseFactors.Location = new Point(707, 34);
            dgvPurchaseFactors.MainView = viewPurchaseFactors;
            dgvPurchaseFactors.Name = "dgvPurchaseFactors";
            dgvPurchaseFactors.Size = new Size(540, 536);
            dgvPurchaseFactors.TabIndex = 1;
            dgvPurchaseFactors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPurchaseFactors });
            // 
            // viewPurchaseFactors
            // 
            viewPurchaseFactors.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewPurchaseFactors.GridControl = dgvPurchaseFactors;
            viewPurchaseFactors.Name = "viewPurchaseFactors";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // pnlPurchaseFunction
            // 
            pnlPurchaseFunction.Controls.Add(btnCreatePurchaseDocument);
            pnlPurchaseFunction.Dock = DockStyle.Top;
            pnlPurchaseFunction.Location = new Point(0, 0);
            pnlPurchaseFunction.Name = "pnlPurchaseFunction";
            pnlPurchaseFunction.Size = new Size(1247, 34);
            pnlPurchaseFunction.TabIndex = 0;
            // 
            // btnCreatePurchaseDocument
            // 
            btnCreatePurchaseDocument.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreatePurchaseDocument.Location = new Point(1107, 3);
            btnCreatePurchaseDocument.Name = "btnCreatePurchaseDocument";
            btnCreatePurchaseDocument.Size = new Size(137, 28);
            btnCreatePurchaseDocument.TabIndex = 0;
            btnCreatePurchaseDocument.Text = "صدور سند خرید";
            // 
            // tabSale
            // 
            tabSale.Caption = "فروش";
            tabSale.Controls.Add(dgvSaleDocuments);
            tabSale.Controls.Add(dgvSaleFactors);
            tabSale.Controls.Add(panel2);
            tabSale.Name = "tabSale";
            tabSale.Size = new Size(1088, 538);
            // 
            // dgvSaleDocuments
            // 
            dgvSaleDocuments.Dock = DockStyle.Fill;
            dgvSaleDocuments.Location = new Point(0, 34);
            dgvSaleDocuments.MainView = viewSaleDocuments;
            dgvSaleDocuments.Name = "dgvSaleDocuments";
            dgvSaleDocuments.Size = new Size(548, 504);
            dgvSaleDocuments.TabIndex = 4;
            dgvSaleDocuments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewSaleDocuments });
            // 
            // viewSaleDocuments
            // 
            viewSaleDocuments.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand3 });
            viewSaleDocuments.GridControl = dgvSaleDocuments;
            viewSaleDocuments.Name = "viewSaleDocuments";
            // 
            // gridBand3
            // 
            gridBand3.Caption = "gridBand2";
            gridBand3.Name = "gridBand3";
            gridBand3.VisibleIndex = 0;
            // 
            // dgvSaleFactors
            // 
            dgvSaleFactors.Dock = DockStyle.Right;
            dgvSaleFactors.Location = new Point(548, 34);
            dgvSaleFactors.MainView = viewSaleFactors;
            dgvSaleFactors.Name = "dgvSaleFactors";
            dgvSaleFactors.Size = new Size(540, 504);
            dgvSaleFactors.TabIndex = 3;
            dgvSaleFactors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewSaleFactors });
            // 
            // viewSaleFactors
            // 
            viewSaleFactors.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand4 });
            viewSaleFactors.GridControl = dgvSaleFactors;
            viewSaleFactors.Name = "viewSaleFactors";
            // 
            // gridBand4
            // 
            gridBand4.Caption = "gridBand1";
            gridBand4.Name = "gridBand4";
            gridBand4.VisibleIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnCreateSaleDocument);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1088, 34);
            panel2.TabIndex = 1;
            // 
            // btnCreateSaleDocument
            // 
            btnCreateSaleDocument.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateSaleDocument.Location = new Point(949, 3);
            btnCreateSaleDocument.Name = "btnCreateSaleDocument";
            btnCreateSaleDocument.Size = new Size(137, 28);
            btnCreateSaleDocument.TabIndex = 1;
            btnCreateSaleDocument.Text = "صدور سند فروش";
            // 
            // tabExpense
            // 
            tabExpense.Caption = "هزینه ها";
            tabExpense.Controls.Add(dgvPartnerBalance);
            tabExpense.Controls.Add(layExpenseInput);
            tabExpense.Controls.Add(pnlExpenseFunction);
            tabExpense.Name = "tabExpense";
            tabExpense.Size = new Size(1088, 538);
            // 
            // dgvPartnerBalance
            // 
            dgvPartnerBalance.Dock = DockStyle.Fill;
            dgvPartnerBalance.Location = new Point(0, 34);
            dgvPartnerBalance.MainView = viewPartnerBalance;
            dgvPartnerBalance.Name = "dgvPartnerBalance";
            dgvPartnerBalance.Size = new Size(641, 504);
            dgvPartnerBalance.TabIndex = 3;
            dgvPartnerBalance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPartnerBalance });
            // 
            // viewPartnerBalance
            // 
            viewPartnerBalance.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand5 });
            viewPartnerBalance.GridControl = dgvPartnerBalance;
            viewPartnerBalance.Name = "viewPartnerBalance";
            // 
            // gridBand5
            // 
            gridBand5.Caption = "gridBand5";
            gridBand5.Name = "gridBand5";
            gridBand5.VisibleIndex = 0;
            // 
            // layExpenseInput
            // 
            layExpenseInput.Dock = DockStyle.Right;
            layExpenseInput.Location = new Point(641, 34);
            layExpenseInput.Name = "layExpenseInput";
            layExpenseInput.OptionsView.RightToLeftMirroringApplied = true;
            layExpenseInput.Root = Root;
            layExpenseInput.Size = new Size(447, 504);
            layExpenseInput.TabIndex = 2;
            layExpenseInput.Text = "kavoshLayout1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(447, 504);
            Root.TextVisible = false;
            // 
            // pnlExpenseFunction
            // 
            pnlExpenseFunction.Dock = DockStyle.Top;
            pnlExpenseFunction.Location = new Point(0, 0);
            pnlExpenseFunction.Name = "pnlExpenseFunction";
            pnlExpenseFunction.Size = new Size(1088, 34);
            pnlExpenseFunction.TabIndex = 1;
            // 
            // tabReport
            // 
            tabReport.Caption = "گزارش";
            tabReport.Controls.Add(panel4);
            tabReport.Name = "tabReport";
            tabReport.Size = new Size(1088, 579);
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1088, 34);
            panel4.TabIndex = 1;
            // 
            // tabNavigationPage1
            // 
            tabNavigationPage1.Caption = "محاسبه درآمد";
            tabNavigationPage1.Controls.Add(splitContainer1);
            tabNavigationPage1.Name = "tabNavigationPage1";
            tabNavigationPage1.Size = new Size(1247, 570);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvDoc);
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel1.Controls.Add(panel3);
            splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvDocCalc);
            splitContainer1.Panel2.Controls.Add(panel5);
            splitContainer1.Panel2.RightToLeft = RightToLeft.Yes;
            splitContainer1.Size = new Size(1247, 570);
            splitContainer1.SplitterDistance = 696;
            splitContainer1.TabIndex = 0;
            // 
            // dgvDoc
            // 
            dgvDoc.Dock = DockStyle.Fill;
            dgvDoc.Location = new Point(0, 35);
            dgvDoc.MainView = viewDoc;
            dgvDoc.Name = "dgvDoc";
            dgvDoc.Size = new Size(696, 505);
            dgvDoc.TabIndex = 0;
            dgvDoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewDoc });
            dgvDoc.Click += dgvDoc_Click;
            // 
            // viewDoc
            // 
            viewDoc.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand6 });
            viewDoc.GridControl = dgvDoc;
            viewDoc.Name = "viewDoc";
            // 
            // gridBand6
            // 
            gridBand6.Caption = "gridBand6";
            gridBand6.Name = "gridBand6";
            gridBand6.VisibleIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDocFinal);
            panel1.Controls.Add(cmbSaleDocument);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(696, 35);
            panel1.TabIndex = 1;
            // 
            // btnDocFinal
            // 
            btnDocFinal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDocFinal.Location = new Point(556, 3);
            btnDocFinal.Name = "btnDocFinal";
            btnDocFinal.Size = new Size(137, 28);
            btnDocFinal.TabIndex = 4;
            btnDocFinal.Text = "محاسبه";
            btnDocFinal.Click += btnDocFinal_Click;
            // 
            // cmbSaleDocument
            // 
            cmbSaleDocument.Location = new Point(3, 3);
            cmbSaleDocument.Name = "cmbSaleDocument";
            cmbSaleDocument.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbSaleDocument.Properties.Items.AddRange(new object[] { "تسویه نشده", "تسویه شده", "همه" });
            cmbSaleDocument.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbSaleDocument.Size = new Size(173, 20);
            cmbSaleDocument.TabIndex = 3;
            cmbSaleDocument.SelectedIndexChanged += cmbSaleDocument_SelectedIndexChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblDocStatus);
            panel3.Controls.Add(lblCalcDoc);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 540);
            panel3.Name = "panel3";
            panel3.Size = new Size(696, 30);
            panel3.TabIndex = 0;
            panel3.Paint += panel3_Paint;
            // 
            // lblDocStatus
            // 
            lblDocStatus.Dock = DockStyle.Right;
            lblDocStatus.Location = new Point(316, 0);
            lblDocStatus.Name = "lblDocStatus";
            lblDocStatus.Size = new Size(117, 30);
            lblDocStatus.TabIndex = 2;
            lblDocStatus.Text = "0";
            lblDocStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCalcDoc
            // 
            lblCalcDoc.Dock = DockStyle.Right;
            lblCalcDoc.Location = new Point(433, 0);
            lblCalcDoc.Name = "lblCalcDoc";
            lblCalcDoc.RightToLeft = RightToLeft.No;
            lblCalcDoc.Size = new Size(187, 30);
            lblCalcDoc.TabIndex = 1;
            lblCalcDoc.Text = "0";
            lblCalcDoc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Right;
            label1.Location = new Point(620, 0);
            label1.Name = "label1";
            label1.Size = new Size(76, 30);
            label1.TabIndex = 0;
            label1.Text = "محاسبه:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvDocCalc
            // 
            dgvDocCalc.Dock = DockStyle.Fill;
            dgvDocCalc.Location = new Point(0, 35);
            dgvDocCalc.MainView = viewDocCalc;
            dgvDocCalc.Name = "dgvDocCalc";
            dgvDocCalc.Size = new Size(547, 535);
            dgvDocCalc.TabIndex = 0;
            dgvDocCalc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewDocCalc });
            // 
            // viewDocCalc
            // 
            viewDocCalc.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand7 });
            viewDocCalc.GridControl = dgvDocCalc;
            viewDocCalc.Name = "viewDocCalc";
            // 
            // gridBand7
            // 
            gridBand7.Caption = "gridBand7";
            gridBand7.Name = "gridBand7";
            gridBand7.VisibleIndex = 0;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(547, 35);
            panel5.TabIndex = 1;
            // 
            // FrmCashRegister
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 603);
            Controls.Add(tabPane1);
            Name = "FrmCashRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "صندوق";
            Load += FrmCashRegister_Load;
            ((System.ComponentModel.ISupportInitialize)tabPane1).EndInit();
            tabPane1.ResumeLayout(false);
            tabPurchase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPurchaseDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseFactors).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPurchaseFactors).EndInit();
            pnlPurchaseFunction.ResumeLayout(false);
            tabSale.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSaleDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewSaleDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSaleFactors).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewSaleFactors).EndInit();
            panel2.ResumeLayout(false);
            tabExpense.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPartnerBalance).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPartnerBalance).EndInit();
            ((System.ComponentModel.ISupportInitialize)layExpenseInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            tabReport.ResumeLayout(false);
            tabNavigationPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDoc).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDoc).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cmbSaleDocument.Properties).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDocCalc).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDocCalc).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPurchase;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabSale;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabExpense;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabReport;
        private Panel pnlPurchaseFunction;
        private Panel panel2;
        private Panel pnlExpenseFunction;
        private Panel panel4;
        private MyCom.Object.KavoshGrid dgvPurchaseFactors;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPurchaseFactors;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private MyCom.Object.KavoshGrid dgvPurchaseDocuments;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPurchaseDocuments;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.SimpleButton btnCreatePurchaseDocument;
        private MyCom.Object.KavoshGrid dgvSaleDocuments;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewSaleDocuments;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private MyCom.Object.KavoshGrid dgvSaleFactors;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewSaleFactors;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraEditors.SimpleButton btnCreateSaleDocument;
        private MyCom.Object.KavoshLayout layExpenseInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private MyCom.Object.KavoshGrid dgvPartnerBalance;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPartnerBalance;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private MyCom.Object.KavoshGrid dgvDoc;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewDoc;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSaleDocument;
        private DevExpress.XtraEditors.SimpleButton btnDocFinal;
        private Panel panel3;
        private Label label1;
        private Label lblCalcDoc;
        private Label lblDocStatus;
        private MyCom.Object.KavoshGrid dgvDocCalc;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewDocCalc;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private Panel panel5;
    }
}