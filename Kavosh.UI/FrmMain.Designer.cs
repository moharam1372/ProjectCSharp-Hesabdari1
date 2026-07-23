namespace Kavosh.UI
{
    partial class FrmMain
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
            ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barBtnProduct = new DevExpress.XtraBars.BarButtonItem();
            barPerson = new DevExpress.XtraBars.BarButtonItem();
            barFactor = new DevExpress.XtraBars.BarButtonItem();
            barBtnAccounting = new DevExpress.XtraBars.BarButtonItem();
            barBtnSetting = new DevExpress.XtraBars.BarButtonItem();
            barBtnPardakhtDaryaft = new DevExpress.XtraBars.BarButtonItem();
            barBtnDebtorsList = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            ((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbon.ExpandCollapseItem, barBtnProduct, barPerson, barFactor, barBtnAccounting, barBtnSetting, barBtnPardakhtDaryaft, barBtnDebtorsList, barButtonItem1 });
            ribbon.Location = new Point(0, 0);
            ribbon.MaxItemId = 9;
            ribbon.Name = "ribbon";
            ribbon.PageHeaderItemLinks.Add(barButtonItem1);
            ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbon.Size = new Size(1175, 201);
            ribbon.StatusBar = ribbonStatusBar;
            // 
            // barBtnProduct
            // 
            barBtnProduct.Caption = "مدیریت محصولات";
            barBtnProduct.Id = 1;
            barBtnProduct.ImageOptions.SvgImage = Properties.Resources.bo_product_group;
            barBtnProduct.LargeWidth = 110;
            barBtnProduct.Name = "barBtnProduct";
            barBtnProduct.ItemClick += barBtnProduct_ItemClick;
            // 
            // barPerson
            // 
            barPerson.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barPerson.Caption = "مدیریت مشتریان";
            barPerson.Id = 2;
            barPerson.ImageOptions.SvgImage = Properties.Resources.bo_employee;
            barPerson.LargeWidth = 110;
            barPerson.Name = "barPerson";
            barPerson.ItemClick += barPerson_ItemClick;
            // 
            // barFactor
            // 
            barFactor.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barFactor.Caption = "فاکتورها";
            barFactor.Id = 3;
            barFactor.ImageOptions.SvgImage = Properties.Resources.list;
            barFactor.LargeWidth = 70;
            barFactor.Name = "barFactor";
            barFactor.ItemClick += barFactor_ItemClick;
            // 
            // barBtnAccounting
            // 
            barBtnAccounting.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barBtnAccounting.Caption = "صورت حساب مشتریان";
            barBtnAccounting.Id = 4;
            barBtnAccounting.ImageOptions.SvgImage = Properties.Resources.customerprofilereport;
            barBtnAccounting.LargeWidth = 140;
            barBtnAccounting.Name = "barBtnAccounting";
            barBtnAccounting.ItemClick += barBtnAccounting_ItemClick;
            // 
            // barBtnSetting
            // 
            barBtnSetting.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barBtnSetting.Caption = "تنظیمات";
            barBtnSetting.Id = 5;
            barBtnSetting.ImageOptions.SvgImage = Properties.Resources.viewsettings;
            barBtnSetting.LargeWidth = 70;
            barBtnSetting.Name = "barBtnSetting";
            barBtnSetting.ItemClick += barBtnSetting_ItemClick;
            // 
            // barBtnPardakhtDaryaft
            // 
            barBtnPardakhtDaryaft.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barBtnPardakhtDaryaft.Caption = "دریافت|پرداخت";
            barBtnPardakhtDaryaft.Id = 6;
            barBtnPardakhtDaryaft.ImageOptions.SvgImage = Properties.Resources.accounting;
            barBtnPardakhtDaryaft.LargeWidth = 100;
            barBtnPardakhtDaryaft.Name = "barBtnPardakhtDaryaft";
            // 
            // barBtnDebtorsList
            // 
            barBtnDebtorsList.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barBtnDebtorsList.Caption = "لیست بدهکاران";
            barBtnDebtorsList.Id = 7;
            barBtnDebtorsList.ImageOptions.SvgImage = Properties.Resources.listviewappointmentdeletedoccurrence;
            barBtnDebtorsList.LargeWidth = 100;
            barBtnDebtorsList.Name = "barBtnDebtorsList";
            barBtnDebtorsList.ItemClick += barBtnDebtorsList_ItemClick;
            // 
            // barButtonItem1
            // 
            barButtonItem1.Caption = "btnBackup";
            barButtonItem1.Id = 8;
            barButtonItem1.ImageOptions.SvgImage = Properties.Resources.actions_database;
            barButtonItem1.Name = "barButtonItem1";
            barButtonItem1.ItemClick += barButtonItem1_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "داشبورد";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(barBtnProduct);
            ribbonPageGroup1.ItemLinks.Add(barPerson);
            ribbonPageGroup1.ItemLinks.Add(barFactor);
            ribbonPageGroup1.ItemLinks.Add(barBtnPardakhtDaryaft);
            ribbonPageGroup1.ItemLinks.Add(barBtnAccounting);
            ribbonPageGroup1.ItemLinks.Add(barBtnDebtorsList);
            ribbonPageGroup1.ItemLinks.Add(barBtnSetting);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            ribbonStatusBar.Location = new Point(0, 585);
            ribbonStatusBar.Name = "ribbonStatusBar";
            ribbonStatusBar.Ribbon = ribbon;
            ribbonStatusBar.Size = new Size(1175, 37);
            // 
            // FrmMain
            // 
            //AutoScaleMode = AutoScaleMode.None;
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1175, 622);
            Controls.Add(ribbon);
            Controls.Add(ribbonStatusBar);
            IsMdiContainer = true;
            Name = "FrmMain";
            Ribbon = ribbon;
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            StatusBar = ribbonStatusBar;
            Text = "صفحه اصلی";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            ((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barBtnProduct;
        private DevExpress.XtraBars.BarButtonItem barPerson;
        private DevExpress.XtraBars.BarButtonItem barFactor;
        private DevExpress.XtraBars.BarButtonItem barBtnAccounting;
        private DevExpress.XtraBars.BarButtonItem barBtnSetting;
        private DevExpress.XtraBars.BarButtonItem barBtnPardakhtDaryaft;
        private DevExpress.XtraBars.BarButtonItem barBtnDebtorsList;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}