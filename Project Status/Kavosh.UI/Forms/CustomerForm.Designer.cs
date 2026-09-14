namespace YourApp.UI.Forms
{
    partial class CustomerForm
    {
        private System.ComponentModel.IContainer components = null;

        // کنترل‌های DevExpress - در Visual Studio Designer با drag & drop
        // از Toolbox این کنترل‌ها را روی فرم قرار دهید و نام‌گذاری کنید:
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtFullName;
        private DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // -----------------------------------------------------------------
        // نکته مهم: این بخش به‌صورت دستی نوشته شده تا ساختار کلی مشخص باشد.
        // توصیه می‌شود این فرم را در Visual Studio با DevExpress Form Designer
        // دوباره بسازید (drag & drop کنترل‌ها روی فرم)، تا این InitializeComponent
        // به‌صورت خودکار و صحیح تولید شود و طراحی بصری هم قابل ویرایش بماند.
        // -----------------------------------------------------------------
        private void InitializeComponent()
        {
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            txtFullName = new DevExpress.XtraEditors.TextEdit();
            txtPhone = new DevExpress.XtraEditors.TextEdit();
            txtEmail = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            btnNew = new DevExpress.XtraEditors.SimpleButton();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtFullName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPhone.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEmail.Properties).BeginInit();
            SuspendLayout();
            // 
            // gridControl1
            // 
            gridControl1.Location = new Point(12, 12);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new Size(560, 300);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(90, 322);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(200, 28);
            txtFullName.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(340, 322);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(150, 28);
            txtPhone.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(90, 352);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 28);
            txtEmail.TabIndex = 6;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(12, 325);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(42, 13);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "نام کامل:";
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(300, 325);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(25, 13);
            labelControl2.TabIndex = 3;
            labelControl2.Text = "تلفن:";
            // 
            // labelControl3
            // 
            labelControl3.Location = new Point(12, 355);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(29, 13);
            labelControl3.TabIndex = 5;
            labelControl3.Text = "ایمیل:";
            // 
            // btnNew
            // 
            btnNew.Location = new Point(340, 352);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(75, 30);
            btnNew.TabIndex = 7;
            btnNew.Text = "جدید";
            btnNew.Click += btnNew_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(421, 352);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "ذخیره";
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(502, 352);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(70, 30);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "حذف";
            btnDelete.Click += btnDelete_Click;
            // 
            // CustomerForm
            // 
            ClientSize = new Size(584, 398);
            Controls.Add(gridControl1);
            Controls.Add(labelControl1);
            Controls.Add(txtFullName);
            Controls.Add(labelControl2);
            Controls.Add(txtPhone);
            Controls.Add(labelControl3);
            Controls.Add(txtEmail);
            Controls.Add(btnNew);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Name = "CustomerForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "مدیریت مشتریان";
            Load += CustomerForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtFullName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPhone.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEmail.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
