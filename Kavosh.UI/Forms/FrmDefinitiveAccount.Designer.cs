namespace Kavosh.UI.Forms
{
    partial class FrmDefinitiveAccount
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
            pnlTop = new Panel();
            lblBalanceValue = new DevExpress.XtraEditors.LabelControl();
            lblBalanceTitle = new DevExpress.XtraEditors.LabelControl();
            cmbPerson = new DevExpress.XtraEditors.LookUpEdit();
            lblPerson = new DevExpress.XtraEditors.LabelControl();
            dgvStatement = new MyCom.Object.KavoshGrid(components);
            viewStatement = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();

            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbPerson.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStatement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewStatement).BeginInit();
            SuspendLayout();

            //
            // pnlTop
            //
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 45;
            pnlTop.Controls.Add(lblBalanceValue);
            pnlTop.Controls.Add(lblBalanceTitle);
            pnlTop.Controls.Add(cmbPerson);
            pnlTop.Controls.Add(lblPerson);
            pnlTop.Name = "pnlTop";

            //
            // lblPerson
            //
            lblPerson.Location = new Point(12, 15);
            lblPerson.Name = "lblPerson";
            lblPerson.Text = "مشتری:";

            //
            // cmbPerson
            //
            cmbPerson.Location = new Point(70, 12);
            cmbPerson.Name = "cmbPerson";
            cmbPerson.Size = new Size(250, 22);
            cmbPerson.Properties.NullText = "انتخاب مشتری...";
            cmbPerson.EditValueChanged += new EventHandler(cmbPerson_EditValueChanged);

            //
            // lblBalanceTitle
            //
            lblBalanceTitle.Location = new Point(340, 15);
            lblBalanceTitle.Name = "lblBalanceTitle";
            lblBalanceTitle.Text = "مانده مشتری:";

            //
            // lblBalanceValue
            //
            lblBalanceValue.Location = new Point(420, 15);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.Text = "0";
            lblBalanceValue.Appearance.Font = new Font(lblBalanceValue.Appearance.Font, FontStyle.Bold);

            //
            // dgvStatement
            //
            dgvStatement.Dock = DockStyle.Fill;
            dgvStatement.Location = new Point(0, 45);
            dgvStatement.MainView = viewStatement;
            dgvStatement.Name = "dgvStatement";
            dgvStatement.RightToLeft = RightToLeft.Yes;
            dgvStatement.TabIndex = 1;
            dgvStatement.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewStatement });

            //
            // viewStatement
            //
            viewStatement.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewStatement.GridControl = dgvStatement;
            viewStatement.Name = "viewStatement";
            viewStatement.OptionsBehavior.Editable = false;

            //
            // gridBand1
            //
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;

            //
            // FrmDefinitiveAccount
            //
            //AutoScaleDimensions = new SizeF(6F, 13F);
            //AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 550);
            Controls.Add(dgvStatement);
            Controls.Add(pnlTop);
            Name = "FrmDefinitiveAccount";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "صورت‌حساب مشتری";
            Load += FrmDefinitiveAccount_Load;

            pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cmbPerson.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStatement).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewStatement).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private DevExpress.XtraEditors.LabelControl lblPerson;
        private DevExpress.XtraEditors.LookUpEdit cmbPerson;
        private DevExpress.XtraEditors.LabelControl lblBalanceTitle;
        private DevExpress.XtraEditors.LabelControl lblBalanceValue;
        private MyCom.Object.KavoshGrid dgvStatement;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewStatement;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}