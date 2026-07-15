namespace MyCom.Form_Portable
{
    partial class FrmPortable
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
            dgvPortable = new MyCom.Object.KavoshGrid(components);
            viewPortable = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)dgvPortable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPortable).BeginInit();
            SuspendLayout();
            // 
            // dgvPortable
            // 
            dgvPortable.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvPortable.Location = new System.Drawing.Point(0, 0);
            dgvPortable.MainView = viewPortable;
            dgvPortable.Name = "dgvPortable";
            dgvPortable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dgvPortable.Size = new System.Drawing.Size(315, 387);
            dgvPortable.TabIndex = 0;
            dgvPortable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPortable });
            // 
            // viewPortable
            // 
            viewPortable.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewPortable.GridControl = dgvPortable;
            viewPortable.Name = "viewPortable";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // FrmPortable
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(315, 387);
            Controls.Add(dgvPortable);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPortable";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "___";
            Load += FrmPortable_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPortable).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPortable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Object.KavoshGrid dgvPortable;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPortable;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}