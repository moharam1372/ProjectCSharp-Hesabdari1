namespace MyCom.Object
{
    partial class optimChart
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
            components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel1 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel2 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel3 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel4 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel5 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel6 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel7 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel8 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel9 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel10 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel11 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel12 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel13 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            chart2 = new DevExpress.XtraCharts.ChartControl();
            panel1 = new System.Windows.Forms.Panel();
            colorPickEdit1 = new DevExpress.XtraEditors.ColorPickEdit();
            colorPickEdit2 = new DevExpress.XtraEditors.ColorPickEdit();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            btnExport = new DevExpress.XtraEditors.SimpleButton();
            trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            mnuChart = new DevExpress.XtraBars.PopupMenu(components);
            mnuExportImage = new DevExpress.XtraBars.BarButtonItem();
            mnuExportPDF = new DevExpress.XtraBars.BarButtonItem();
            mnuExportMHT = new DevExpress.XtraBars.BarButtonItem();
            mnuExportDocx = new DevExpress.XtraBars.BarButtonItem();
            mnuExportExcel = new DevExpress.XtraBars.BarButtonItem();
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)chart2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)colorPickEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorPickEdit2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mnuChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            SuspendLayout();
            // 
            // chart2
            // 
            chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            chart2.IndicatorsPaletteName = "Concourse";
            chart2.Legend.Name = "Default Legend";
            chart2.Location = new System.Drawing.Point(0, 44);
            chart2.LookAndFeel.SkinName = "Coffee";
            chart2.LookAndFeel.UseDefaultLookAndFeel = false;
            chart2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chart2.Name = "chart2";
            chart2.PaletteName = "Civic";
            chart2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            chart2.Size = new System.Drawing.Size(1196, 640);
            chart2.TabIndex = 2;
            chart2.ObjectSelected += chart2_ObjectSelected;
            chart2.Click += chart2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(140, 211, 232);
            panel1.Controls.Add(colorPickEdit1);
            panel1.Controls.Add(colorPickEdit2);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnExport);
            panel1.Controls.Add(trackBarControl1);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1196, 44);
            panel1.TabIndex = 3;
            // 
            // colorPickEdit1
            // 
            colorPickEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
            colorPickEdit1.EditValue = System.Drawing.Color.Empty;
            colorPickEdit1.Location = new System.Drawing.Point(478, 8);
            colorPickEdit1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            colorPickEdit1.Name = "colorPickEdit1";
            colorPickEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            colorPickEdit1.Properties.Appearance.Options.UseFont = true;
            colorPickEdit1.Properties.AutomaticColor = System.Drawing.Color.Black;
            colorPickEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            colorPickEdit1.Properties.LookAndFeel.SkinName = "Foggy";
            colorPickEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            colorPickEdit1.Properties.ShowSystemColors = false;
            colorPickEdit1.Properties.ShowWebColors = false;
            colorPickEdit1.Size = new System.Drawing.Size(211, 24);
            colorPickEdit1.TabIndex = 6;
            colorPickEdit1.EditValueChanged += colorPickEdit1_EditValueChanged;
            // 
            // colorPickEdit2
            // 
            colorPickEdit2.Anchor = System.Windows.Forms.AnchorStyles.None;
            colorPickEdit2.EditValue = System.Drawing.Color.Empty;
            colorPickEdit2.Location = new System.Drawing.Point(763, 8);
            colorPickEdit2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            colorPickEdit2.Name = "colorPickEdit2";
            colorPickEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            colorPickEdit2.Properties.Appearance.Options.UseFont = true;
            colorPickEdit2.Properties.AutomaticColor = System.Drawing.Color.Black;
            colorPickEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            colorPickEdit2.Properties.LookAndFeel.SkinName = "Foggy";
            colorPickEdit2.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            colorPickEdit2.Properties.ShowSystemColors = false;
            colorPickEdit2.Properties.ShowWebColors = false;
            colorPickEdit2.Size = new System.Drawing.Size(212, 24);
            colorPickEdit2.TabIndex = 5;
            colorPickEdit2.EditValueChanged += colorPickEdit2_EditValueChanged;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(416, 13);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 16);
            label2.TabIndex = 9;
            label2.Text = "Color 1:";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(701, 13);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 16);
            label1.TabIndex = 8;
            label1.Text = "Color 2:";
            // 
            // btnExport
            // 
            btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnExport.Location = new System.Drawing.Point(1068, 3);
            btnExport.LookAndFeel.SkinName = "VS2010";
            btnExport.LookAndFeel.UseDefaultLookAndFeel = false;
            btnExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(126, 38);
            btnExport.TabIndex = 7;
            btnExport.Text = "Export...";
            btnExport.Click += btnExport_Click;
            // 
            // trackBarControl1
            // 
            trackBarControl1.EditValue = null;
            trackBarControl1.Location = new System.Drawing.Point(4, 6);
            trackBarControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBarControl1.Name = "trackBarControl1";
            trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            trackBarLabel1.Label = "0";
            trackBarLabel2.Label = "30";
            trackBarLabel2.Value = 30;
            trackBarLabel3.Label = "60";
            trackBarLabel3.Value = 60;
            trackBarLabel4.Label = "90";
            trackBarLabel4.Value = 90;
            trackBarLabel5.Label = "120";
            trackBarLabel5.Value = 120;
            trackBarLabel6.Label = "150";
            trackBarLabel6.Value = 150;
            trackBarLabel7.Label = "180";
            trackBarLabel7.Value = 180;
            trackBarLabel8.Label = "210";
            trackBarLabel8.Value = 210;
            trackBarLabel9.Label = "240";
            trackBarLabel9.Value = 240;
            trackBarLabel10.Label = "270";
            trackBarLabel10.Value = 270;
            trackBarLabel11.Label = "300";
            trackBarLabel11.Value = 300;
            trackBarLabel12.Label = "330";
            trackBarLabel12.Value = 330;
            trackBarLabel13.Label = "360";
            trackBarLabel13.Value = 360;
            trackBarControl1.Properties.Labels.AddRange(new DevExpress.XtraEditors.Repository.TrackBarLabel[] { trackBarLabel1, trackBarLabel2, trackBarLabel3, trackBarLabel4, trackBarLabel5, trackBarLabel6, trackBarLabel7, trackBarLabel8, trackBarLabel9, trackBarLabel10, trackBarLabel11, trackBarLabel12, trackBarLabel13 });
            trackBarControl1.Properties.LookAndFeel.SkinName = "The Bezier";
            trackBarControl1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            trackBarControl1.Properties.Maximum = 360;
            trackBarControl1.Properties.SmallChange = 5;
            trackBarControl1.Size = new System.Drawing.Size(348, 45);
            trackBarControl1.TabIndex = 4;
            trackBarControl1.EditValueChanged += trackBarControl1_EditValueChanged;
            // 
            // mnuChart
            // 
            mnuChart.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(mnuExportImage), new DevExpress.XtraBars.LinkPersistInfo(mnuExportPDF), new DevExpress.XtraBars.LinkPersistInfo(mnuExportMHT), new DevExpress.XtraBars.LinkPersistInfo(mnuExportDocx), new DevExpress.XtraBars.LinkPersistInfo(mnuExportExcel) });
            mnuChart.Manager = barManager1;
            mnuChart.Name = "mnuChart";
            // 
            // mnuExportImage
            // 
            mnuExportImage.Caption = "Export to Image";
            mnuExportImage.Id = 0;
            mnuExportImage.Name = "mnuExportImage";
            mnuExportImage.ItemClick += mnuExportImage_ItemClick;
            // 
            // mnuExportPDF
            // 
            mnuExportPDF.Caption = "Export to PDF";
            mnuExportPDF.Id = 1;
            mnuExportPDF.Name = "mnuExportPDF";
            mnuExportPDF.ItemClick += mnuExportPDF_ItemClick;
            // 
            // mnuExportMHT
            // 
            mnuExportMHT.Caption = "Export to MHT";
            mnuExportMHT.Id = 2;
            mnuExportMHT.Name = "mnuExportMHT";
            mnuExportMHT.ItemClick += mnuExportMHT_ItemClick;
            // 
            // mnuExportDocx
            // 
            mnuExportDocx.Caption = "Export to Docx";
            mnuExportDocx.Id = 3;
            mnuExportDocx.Name = "mnuExportDocx";
            mnuExportDocx.ItemClick += mnuExportDocx_ItemClick;
            // 
            // mnuExportExcel
            // 
            mnuExportExcel.Caption = "Export to Excel";
            mnuExportExcel.Id = 4;
            mnuExportExcel.Name = "mnuExportExcel";
            mnuExportExcel.ItemClick += mnuExportExcel_ItemClick;
            // 
            // barManager1
            // 
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { mnuExportImage, mnuExportPDF, mnuExportMHT, mnuExportDocx, mnuExportExcel });
            barManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            barDockControlTop.Location = new System.Drawing.Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlTop.Size = new System.Drawing.Size(1196, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            barDockControlBottom.Location = new System.Drawing.Point(0, 684);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlBottom.Size = new System.Drawing.Size(1196, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlLeft.Size = new System.Drawing.Size(0, 684);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            barDockControlRight.Location = new System.Drawing.Point(1196, 0);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlRight.Size = new System.Drawing.Size(0, 684);
            // 
            // optimChart
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(chart2);
            Controls.Add(panel1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "optimChart";
            Size = new System.Drawing.Size(1196, 684);
            ((System.ComponentModel.ISupportInitialize)chart2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)colorPickEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorPickEdit2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mnuChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEdit2;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEdit1;
        private DevExpress.XtraBars.PopupMenu mnuChart;
        private DevExpress.XtraBars.BarButtonItem mnuExportImage;
        private DevExpress.XtraBars.BarButtonItem mnuExportPDF;
        private DevExpress.XtraBars.BarButtonItem mnuExportMHT;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraCharts.ChartControl chart2;
        private DevExpress.XtraBars.BarButtonItem mnuExportDocx;
        private DevExpress.XtraBars.BarButtonItem mnuExportExcel;
    }
}
