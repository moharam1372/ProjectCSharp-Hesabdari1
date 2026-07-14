namespace MyCom.Object
{
    partial class pnlDateOpen
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
            txtShowCalen = new DevExpress.XtraEditors.TextEdit();
            btnShowCalen = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)txtShowCalen.Properties).BeginInit();
            SuspendLayout();
            // 
            // txtShowCalen
            // 
            txtShowCalen.Dock = System.Windows.Forms.DockStyle.Fill;
            txtShowCalen.Location = new System.Drawing.Point(33, 0);
            txtShowCalen.Name = "txtShowCalen";
            txtShowCalen.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            txtShowCalen.Properties.Appearance.Options.UseFont = true;
            txtShowCalen.Properties.Appearance.Options.UseTextOptions = true;
            txtShowCalen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtShowCalen.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            txtShowCalen.Properties.LookAndFeel.SkinName = "Office 2010 Blue";
            txtShowCalen.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            txtShowCalen.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.SimpleMaskManager));
            txtShowCalen.Properties.MaskSettings.Set("mask", "0000/00/00");
            txtShowCalen.Properties.MaxLength = 10;
            txtShowCalen.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            txtShowCalen.Size = new System.Drawing.Size(597, 26);
            txtShowCalen.TabIndex = 2;
            txtShowCalen.EditValueChanged += txtShowCalen_EditValueChanged;
            // 
            // btnShowCalen
            // 
            btnShowCalen.Dock = System.Windows.Forms.DockStyle.Left;
            btnShowCalen.ImageOptions.Image = Properties.Resources.Calendar_1_28;
            btnShowCalen.Location = new System.Drawing.Point(0, 0);
            btnShowCalen.LookAndFeel.SkinName = "Office 2013";
            btnShowCalen.LookAndFeel.UseDefaultLookAndFeel = false;
            btnShowCalen.Name = "btnShowCalen";
            btnShowCalen.Size = new System.Drawing.Size(33, 29);
            btnShowCalen.TabIndex = 3;
            btnShowCalen.Click += btnShowCalen_Click;
            // 
            // pnlDateOpen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(txtShowCalen);
            Controls.Add(btnShowCalen);
            Name = "pnlDateOpen";
            Size = new System.Drawing.Size(630, 29);
            Load += pnlDateOpen_Load;
            Resize += pnlDateOpen_Resize;
            ((System.ComponentModel.ISupportInitialize)txtShowCalen.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtShowCalen;
        public DevExpress.XtraEditors.SimpleButton btnShowCalen;
    }
}
