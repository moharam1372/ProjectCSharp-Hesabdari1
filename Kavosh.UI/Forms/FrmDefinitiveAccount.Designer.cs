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
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            lblBalanceValue = new Label();
            lblBalanceTitle = new Label();
            lblPerson = new Label();
            dgvStatement = new MyCom.Object.KavoshGrid(components);
            viewStatement = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStatement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewStatement).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(255, 255, 192);
            pnlTop.Controls.Add(tablePanel1);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(950, 38);
            pnlTop.TabIndex = 2;
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 39.73F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 28.79F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 73.18F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 18.3F) });
            tablePanel1.Controls.Add(lblBalanceValue);
            tablePanel1.Controls.Add(lblBalanceTitle);
            tablePanel1.Controls.Add(lblPerson);
            tablePanel1.Dock = DockStyle.Right;
            tablePanel1.Location = new Point(438, 0);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Padding = new Padding(2);
            tablePanel1.RightToLeft = RightToLeft.No;
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel1.Size = new Size(512, 38);
            tablePanel1.TabIndex = 4;
            // 
            // lblBalanceValue
            // 
            tablePanel1.SetColumn(lblBalanceValue, 0);
            lblBalanceValue.Dock = DockStyle.Fill;
            lblBalanceValue.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblBalanceValue.Location = new Point(5, 2);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblBalanceValue, 0);
            lblBalanceValue.Size = new Size(120, 34);
            lblBalanceValue.TabIndex = 3;
            lblBalanceValue.Text = "0";
            lblBalanceValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBalanceTitle
            // 
            tablePanel1.SetColumn(lblBalanceTitle, 1);
            lblBalanceTitle.Dock = DockStyle.Fill;
            lblBalanceTitle.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblBalanceTitle.Location = new Point(131, 2);
            lblBalanceTitle.Name = "lblBalanceTitle";
            lblBalanceTitle.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblBalanceTitle, 0);
            lblBalanceTitle.Size = new Size(85, 34);
            lblBalanceTitle.TabIndex = 3;
            lblBalanceTitle.Text = "مانده مشتری:";
            lblBalanceTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPerson
            // 
            tablePanel1.SetColumn(lblPerson, 3);
            lblPerson.Dock = DockStyle.Fill;
            lblPerson.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblPerson.Location = new Point(455, 2);
            lblPerson.Name = "lblPerson";
            lblPerson.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblPerson, 0);
            lblPerson.Size = new Size(52, 34);
            lblPerson.TabIndex = 2;
            lblPerson.Text = "مشتری:";
            lblPerson.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvStatement
            // 
            dgvStatement.Dock = DockStyle.Fill;
            dgvStatement.Location = new Point(0, 38);
            dgvStatement.MainView = viewStatement;
            dgvStatement.Name = "dgvStatement";
            dgvStatement.RightToLeft = RightToLeft.Yes;
            dgvStatement.Size = new Size(950, 497);
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
            ClientSize = new Size(950, 535);
            Controls.Add(dgvStatement);
            Controls.Add(pnlTop);
            Name = "FrmDefinitiveAccount";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "صورت‌حساب مشتری";
            Load += FrmDefinitiveAccount_Load;
            pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStatement).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewStatement).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private MyCom.Object.KavoshGrid dgvStatement;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewStatement;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private Label lblBalanceValue;
        private Label lblBalanceTitle;
        private Label lblPerson;
    }
}