namespace Kavosh.UI.Forms
{
    partial class FrmPardakhtDaryaft
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
            pnlTop = new Panel();
            layInput = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlFunction = new Panel();
            lblBalanceTitle = new DevExpress.XtraEditors.LabelControl();
            lblBalanceValue = new DevExpress.XtraEditors.LabelControl();
            dgvStatement = new MyCom.Object.KavoshGrid(components);
            viewStatement = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStatement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewStatement).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(255, 255, 192);
            pnlTop.Controls.Add(layInput);
            pnlTop.Controls.Add(pnlFunction);
            pnlTop.Dock = DockStyle.Right;
            pnlTop.Location = new Point(608, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(433, 471);
            pnlTop.TabIndex = 3;
            // 
            // layInput
            // 
            layInput.Dock = DockStyle.Fill;
            layInput.Location = new Point(0, 29);
            layInput.Name = "layInput";
            layInput.OptionsView.RightToLeftMirroringApplied = true;
            layInput.Root = Root;
            layInput.Size = new Size(433, 442);
            layInput.TabIndex = 0;
            layInput.Text = "kavoshLayoutPardakhtDaryaft";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(433, 442);
            Root.TextVisible = false;
            // 
            // pnlFunction
            // 
            pnlFunction.Dock = DockStyle.Top;
            pnlFunction.Location = new Point(0, 0);
            pnlFunction.Name = "pnlFunction";
            pnlFunction.Size = new Size(433, 29);
            pnlFunction.TabIndex = 1;
            // 
            // lblBalanceTitle
            // 
            lblBalanceTitle.Appearance.Font = new Font("Samim FD", 10F, FontStyle.Bold);
            lblBalanceTitle.Appearance.Options.UseFont = true;
            lblBalanceTitle.Dock = DockStyle.Top;
            lblBalanceTitle.Location = new Point(0, 0);
            lblBalanceTitle.Name = "lblBalanceTitle";
            lblBalanceTitle.Size = new Size(189, 22);
            lblBalanceTitle.TabIndex = 2;
            lblBalanceTitle.Text = "مانده‌ی شخص بعد از این تراکنش:";
            // 
            // lblBalanceValue
            // 
            lblBalanceValue.Appearance.Font = new Font("Samim FD", 11F, FontStyle.Bold);
            lblBalanceValue.Appearance.ForeColor = Color.Firebrick;
            lblBalanceValue.Appearance.Options.UseFont = true;
            lblBalanceValue.Appearance.Options.UseForeColor = true;
            lblBalanceValue.Dock = DockStyle.Top;
            lblBalanceValue.Location = new Point(0, 22);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.Size = new Size(6, 23);
            lblBalanceValue.TabIndex = 1;
            lblBalanceValue.Text = "0";
            // 
            // dgvStatement
            // 
            dgvStatement.Dock = DockStyle.Fill;
            dgvStatement.Location = new Point(0, 45);
            dgvStatement.MainView = viewStatement;
            dgvStatement.Name = "dgvStatement";
            dgvStatement.RightToLeft = RightToLeft.Yes;
            dgvStatement.Size = new Size(608, 426);
            dgvStatement.TabIndex = 0;
            dgvStatement.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewStatement });
            // 
            // viewStatement
            // 
            viewStatement.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewStatement.DetailHeight = 303;
            viewStatement.GridControl = dgvStatement;
            viewStatement.Name = "viewStatement";
            viewStatement.OptionsBehavior.Editable = false;
            viewStatement.OptionsEditForm.PopupEditFormWidth = 686;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            gridBand1.Width = 60;
            // 
            // FrmPardakhtDaryaft
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1041, 471);
            Controls.Add(dgvStatement);
            Controls.Add(lblBalanceValue);
            Controls.Add(lblBalanceTitle);
            Controls.Add(pnlTop);
            Name = "FrmPardakhtDaryaft";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "دریافت و پرداخت";
            Load += FrmPardakhtDaryaft_Load;
            pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStatement).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewStatement).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // ============= متغیرهای طراحی =============
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFunction;
        private MyCom.Object.KavoshLayout layInput;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        private DevExpress.XtraEditors.LabelControl lblBalanceTitle;
        private DevExpress.XtraEditors.LabelControl lblBalanceValue;

        private MyCom.Object.KavoshGrid dgvStatement;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewStatement;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}