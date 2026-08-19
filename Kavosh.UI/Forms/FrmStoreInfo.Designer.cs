namespace Kavosh.UI.Forms
{
    partial class FrmStoreInfo
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
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            PanelPass = new Panel();
            pnlPass = new Panel();
            tab3 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            pnlLayApp = new Panel();
            pnlSettingApp = new Panel();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabPane1).BeginInit();
            tabPane1.SuspendLayout();
            tabNavigationPage1.SuspendLayout();
            tabNavigationPage2.SuspendLayout();
            tab3.SuspendLayout();
            SuspendLayout();
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 33);
            layInput.Name = "layInput";
            layInput.OptionsView.RightToLeftMirroringApplied = true;
            layInput.Root = Root;
            layInput.Size = new Size(913, 480);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayoutStoreInfo";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(913, 480);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(913, 33);
            pnlFunction.TabIndex = 2;
            // 
            // tabPane1
            // 
            tabPane1.Controls.Add(tabNavigationPage1);
            tabPane1.Controls.Add(tabNavigationPage2);
            tabPane1.Controls.Add(tab3);
            tabPane1.Dock = DockStyle.Fill;
            tabPane1.Location = new Point(0, 0);
            tabPane1.Name = "tabPane1";
            tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { tabNavigationPage1, tabNavigationPage2, tab3 });
            tabPane1.RegularSize = new Size(913, 554);
            tabPane1.SelectedPage = tabNavigationPage1;
            tabPane1.Size = new Size(913, 554);
            tabPane1.TabIndex = 3;
            tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            tabNavigationPage1.Caption = "تنظیمات فاکتور";
            tabNavigationPage1.Controls.Add(layInput);
            tabNavigationPage1.Controls.Add(pnlFunction);
            tabNavigationPage1.Name = "tabNavigationPage1";
            tabNavigationPage1.Size = new Size(913, 513);
            // 
            // tabNavigationPage2
            // 
            tabNavigationPage2.Caption = "تغییر کلمه عبور";
            tabNavigationPage2.Controls.Add(PanelPass);
            tabNavigationPage2.Controls.Add(pnlPass);
            tabNavigationPage2.Name = "tabNavigationPage2";
            tabNavigationPage2.Size = new Size(913, 513);
            // 
            // PanelPass
            // 
            PanelPass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PanelPass.Location = new Point(560, 42);
            PanelPass.Name = "PanelPass";
            PanelPass.Size = new Size(350, 348);
            PanelPass.TabIndex = 4;
            // 
            // pnlPass
            // 
            pnlPass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlPass.Location = new Point(560, 3);
            pnlPass.Name = "pnlPass";
            pnlPass.Size = new Size(350, 33);
            pnlPass.TabIndex = 3;
            // 
            // tab3
            // 
            tab3.Caption = "تنظیمات برنامه";
            tab3.Controls.Add(pnlSettingApp);
            tab3.Controls.Add(pnlLayApp);
            tab3.Name = "tab3";
            tab3.Size = new Size(913, 513);
            // 
            // pnlLayApp
            // 
            pnlLayApp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlLayApp.Location = new Point(560, 42);
            pnlLayApp.Name = "pnlLayApp";
            pnlLayApp.Size = new Size(350, 348);
            pnlLayApp.TabIndex = 5;
            // 
            // pnlSettingApp
            // 
            pnlSettingApp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlSettingApp.Location = new Point(560, 3);
            pnlSettingApp.Name = "pnlSettingApp";
            pnlSettingApp.Size = new Size(350, 33);
            pnlSettingApp.TabIndex = 6;
            // 
            // FrmStoreInfo
            // 
            ClientSize = new Size(913, 554);
            Controls.Add(tabPane1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmStoreInfo";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "تنظیمات فروشگاه";
            Load += FrmStoreInfo_Load;
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabPane1).EndInit();
            tabPane1.ResumeLayout(false);
            tabNavigationPage1.ResumeLayout(false);
            tabNavigationPage2.ResumeLayout(false);
            tab3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlFunction;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private Panel pnlPass;
        private Panel PanelPass;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tab3;
        private Panel pnlSettingApp;
        private Panel pnlLayApp;
    }
}