namespace MyCom.Object
{
    partial class dataLayout
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlOperation = new DevExpress.XtraEditors.PanelControl();
            pnlCenter = new System.Windows.Forms.Panel();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            btnNew = new DevExpress.XtraEditors.SimpleButton();
            pnlDown = new DevExpress.XtraEditors.PanelControl();
            lblStatus = new System.Windows.Forms.Label();
            btnDelete = new DevExpress.XtraEditors.SimpleButton();
            baseLayout = new DevExpress.XtraDataLayout.DataLayoutControl();
            group = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlBase = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)pnlOperation).BeginInit();
            pnlOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlDown).BeginInit();
            pnlDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)baseLayout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)group).BeginInit();
            pnlBase.SuspendLayout();
            SuspendLayout();
            // 
            // pnlOperation
            // 
            pnlOperation.Controls.Add(pnlCenter);
            pnlOperation.Controls.Add(btnCancel);
            pnlOperation.Controls.Add(btnPrint);
            pnlOperation.Controls.Add(btnSave);
            pnlOperation.Controls.Add(btnNew);
            pnlOperation.Dock = System.Windows.Forms.DockStyle.Top;
            pnlOperation.Location = new System.Drawing.Point(0, 0);
            pnlOperation.LookAndFeel.SkinName = "Office 2010 Blue";
            pnlOperation.LookAndFeel.UseDefaultLookAndFeel = false;
            pnlOperation.Name = "pnlOperation";
            pnlOperation.Size = new System.Drawing.Size(396, 31);
            pnlOperation.TabIndex = 3;
            pnlOperation.Paint += pnlOperation_Paint;
            // 
            // pnlCenter
            // 
            pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlCenter.Location = new System.Drawing.Point(170, 2);
            pnlCenter.Name = "pnlCenter";
            pnlCenter.Size = new System.Drawing.Size(72, 27);
            pnlCenter.TabIndex = 5;
            pnlCenter.Paint += pnlCenter_Paint;
            // 
            // btnCancel
            // 
            btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            btnCancel.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            btnCancel.Location = new System.Drawing.Point(86, 2);
            btnCancel.LookAndFeel.SkinName = "Office 2010 Blue";
            btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(84, 27);
            btnCancel.TabIndex = 1;
            btnCancel.Tag = "انصراف";
            btnCancel.Text = "انصراف";
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click2;
            // 
            // btnPrint
            // 
            btnPrint.Dock = System.Windows.Forms.DockStyle.Left;
            btnPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            btnPrint.Location = new System.Drawing.Point(2, 2);
            btnPrint.LookAndFeel.SkinName = "Office 2010 Blue";
            btnPrint.LookAndFeel.UseDefaultLookAndFeel = false;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(84, 27);
            btnPrint.TabIndex = 2;
            btnPrint.Tag = "چاپ";
            btnPrint.Text = "پرینت";
            btnPrint.Visible = false;
            btnPrint.Click += btnPrint_Click2;
            // 
            // btnSave
            // 
            btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            btnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            btnSave.Location = new System.Drawing.Point(242, 2);
            btnSave.LookAndFeel.SkinName = "Office 2010 Blue";
            btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(80, 27);
            btnSave.TabIndex = 1;
            btnSave.Tag = "ذخیره";
            btnSave.Text = "ذخیره";
            btnSave.Visible = false;
            btnSave.Click += btnSave_Click2;
            // 
            // btnNew
            // 
            btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            btnNew.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            btnNew.Location = new System.Drawing.Point(322, 2);
            btnNew.LookAndFeel.SkinName = "Office 2010 Blue";
            btnNew.LookAndFeel.UseDefaultLookAndFeel = false;
            btnNew.Name = "btnNew";
            btnNew.Size = new System.Drawing.Size(72, 27);
            btnNew.TabIndex = 1;
            btnNew.Tag = "جدید";
            btnNew.Text = "جدید";
            btnNew.Click += btnNew_Click2;
            // 
            // pnlDown
            // 
            pnlDown.Controls.Add(lblStatus);
            pnlDown.Controls.Add(btnDelete);
            pnlDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlDown.Location = new System.Drawing.Point(0, 348);
            pnlDown.LookAndFeel.SkinName = "Office 2010 Blue";
            pnlDown.LookAndFeel.UseDefaultLookAndFeel = false;
            pnlDown.Name = "pnlDown";
            pnlDown.Size = new System.Drawing.Size(396, 30);
            pnlDown.TabIndex = 4;
            pnlDown.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            lblStatus.Location = new System.Drawing.Point(31, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(279, 26);
            lblStatus.TabIndex = 22;
            lblStatus.Text = "..............";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblStatus.Visible = false;
            lblStatus.Click += lblStatus_Click;
            // 
            // btnDelete
            // 
            btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            btnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            btnDelete.Location = new System.Drawing.Point(310, 2);
            btnDelete.LookAndFeel.SkinName = "Office 2010 Blue";
            btnDelete.LookAndFeel.UseDefaultLookAndFeel = false;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(84, 26);
            btnDelete.TabIndex = 1;
            btnDelete.Tag = "حذف";
            btnDelete.Text = "حذف";
            btnDelete.Visible = false;
            btnDelete.Click += btnDelete_Click2;
            // 
            // baseLayout
            // 
            baseLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            baseLayout.Location = new System.Drawing.Point(0, 0);
            baseLayout.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            baseLayout.LookAndFeel.UseDefaultLookAndFeel = false;
            baseLayout.Name = "baseLayout";
            baseLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(443, 77, 650, 400);
            baseLayout.OptionsFocus.AllowFocusGroups = false;
            baseLayout.OptionsFocus.AllowFocusTabbedGroups = false;
            baseLayout.OptionsFocus.EnableAutoTabOrder = false;
            baseLayout.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups;
            baseLayout.OptionsView.AllowExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            baseLayout.Root = group;
            baseLayout.Size = new System.Drawing.Size(396, 317);
            baseLayout.TabIndex = 5;
            baseLayout.Text = "dataLayoutControl1";
            baseLayout.KeyDown += baseLayout_KeyDown;
            baseLayout.KeyPress += baseLayout_KeyPress;
            // 
            // group
            // 
            group.AppearanceGroup.Options.UseTextOptions = true;
            group.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            group.AppearanceGroup.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            group.AppearanceItemCaption.Options.UseTextOptions = true;
            group.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            group.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            group.AppearanceTabPage.Header.Options.UseTextOptions = true;
            group.AppearanceTabPage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            group.AppearanceTabPage.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            group.AppearanceTabPage.PageClient.Options.UseTextOptions = true;
            group.AppearanceTabPage.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            group.AppearanceTabPage.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            group.Name = "Root";
            group.Size = new System.Drawing.Size(396, 317);
            group.TextVisible = false;
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(baseLayout);
            pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBase.Location = new System.Drawing.Point(0, 31);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new System.Drawing.Size(396, 317);
            pnlBase.TabIndex = 4;
            // 
            // dataLayout
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlBase);
            Controls.Add(pnlOperation);
            Controls.Add(pnlDown);
            LookAndFeel.SkinName = "Office 2010 Blue";
            LookAndFeel.UseDefaultLookAndFeel = false;
            Name = "dataLayout";
            Size = new System.Drawing.Size(396, 378);
            Load += vGrid_Load;
            ((System.ComponentModel.ISupportInitialize)pnlOperation).EndInit();
            pnlOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlDown).EndInit();
            pnlDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)baseLayout).EndInit();
            ((System.ComponentModel.ISupportInitialize)group).EndInit();
            pnlBase.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        public DevExpress.XtraEditors.PanelControl pnlOperation;
        public DevExpress.XtraEditors.SimpleButton btnPrint;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.SimpleButton btnSave;
        public DevExpress.XtraEditors.SimpleButton btnNew;
        public DevExpress.XtraEditors.PanelControl pnlDown;
        public DevExpress.XtraEditors.SimpleButton btnDelete;
        public System.Windows.Forms.Label lblStatus;
        public DevExpress.XtraDataLayout.DataLayoutControl baseLayout;
        private System.Windows.Forms.Panel pnlBase;
        public System.Windows.Forms.Panel pnlCenter;
        public DevExpress.XtraLayout.LayoutControlGroup group;
    }
}
