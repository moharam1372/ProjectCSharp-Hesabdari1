namespace MyCom.Object
{
    partial class Data_Grid
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
            this.components = new System.ComponentModel.Container();
            this.DGV = new DevExpress.XtraGrid.GridControl();
            this.DGV_Viw = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.banded = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.chkFind = new DevExpress.XtraEditors.CheckEdit();
            this.Tim_DoubleClick = new System.Windows.Forms.Timer(this.components);
            this.schControl = new DevExpress.XtraEditors.SearchControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.schLabel = new DevExpress.XtraEditors.LabelControl();
            this.pnlViewHeader = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Viw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schControl.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlViewHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV
            // 
            this.DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV.Location = new System.Drawing.Point(0, 89);
            this.DGV.LookAndFeel.SkinName = "Office 2010 Blue";
            this.DGV.LookAndFeel.UseDefaultLookAndFeel = false;
            this.DGV.MainView = this.DGV_Viw;
            this.DGV.Name = "DGV";
            this.DGV.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DGV.Size = new System.Drawing.Size(919, 484);
            this.DGV.TabIndex = 12;
            this.DGV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DGV_Viw});
            this.DGV.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.DGV_ProcessGridKey);
            this.DGV.Click += new System.EventHandler(this.DGV__Click);
            this.DGV.Enter += new System.EventHandler(this.DGV_Enter);
            // 
            // DGV_Viw
            // 
            this.DGV_Viw.Appearance.BandPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(226)))), ((int)(((byte)(214)))));
            this.DGV_Viw.Appearance.BandPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(197)))), ((int)(((byte)(182)))));
            this.DGV_Viw.Appearance.BandPanel.BorderColor = System.Drawing.Color.DodgerBlue;
            this.DGV_Viw.Appearance.BandPanel.Options.UseBackColor = true;
            this.DGV_Viw.Appearance.BandPanel.Options.UseBorderColor = true;
            this.DGV_Viw.Appearance.FixedLine.BackColor = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.FixedLine.BackColor2 = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.FixedLine.Options.UseBackColor = true;
            this.DGV_Viw.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.DGV_Viw.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.FooterPanel.Options.UseFont = true;
            this.DGV_Viw.Appearance.FooterPanel.Options.UseForeColor = true;
            this.DGV_Viw.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.DGV_Viw.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DGV_Viw.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DGV_Viw.Appearance.GroupButton.Options.UseTextOptions = true;
            this.DGV_Viw.Appearance.GroupButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DGV_Viw.Appearance.GroupButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DGV_Viw.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.DGV_Viw.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.GroupFooter.Options.UseFont = true;
            this.DGV_Viw.Appearance.GroupFooter.Options.UseForeColor = true;
            this.DGV_Viw.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.DGV_Viw.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DGV_Viw.Appearance.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DGV_Viw.Appearance.GroupRow.ForeColor = System.Drawing.Color.Lime;
            this.DGV_Viw.Appearance.GroupRow.Options.UseForeColor = true;
            this.DGV_Viw.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.DGV_Viw.Appearance.HeaderPanel.Options.UseFont = true;
            this.DGV_Viw.Appearance.HeaderPanelBackground.BackColor = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.HeaderPanelBackground.BackColor2 = System.Drawing.Color.Red;
            this.DGV_Viw.Appearance.HeaderPanelBackground.Options.UseBackColor = true;
            this.DGV_Viw.Appearance.HorzLine.BackColor = System.Drawing.Color.CornflowerBlue;
            this.DGV_Viw.Appearance.HorzLine.BackColor2 = System.Drawing.Color.SteelBlue;
            this.DGV_Viw.Appearance.HorzLine.Options.UseBackColor = true;
            this.DGV_Viw.Appearance.Row.Options.UseFont = true;
            this.DGV_Viw.Appearance.VertLine.BackColor = System.Drawing.Color.CornflowerBlue;
            this.DGV_Viw.Appearance.VertLine.BackColor2 = System.Drawing.Color.DodgerBlue;
            this.DGV_Viw.Appearance.VertLine.Options.UseBackColor = true;
            this.DGV_Viw.Appearance.ViewCaption.Options.UseFont = true;
            this.DGV_Viw.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.DGV_Viw.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.DGV_Viw.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.banded});
            this.DGV_Viw.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.DGV_Viw.GridControl = this.DGV;
            this.DGV_Viw.Name = "DGV_Viw";
            this.DGV_Viw.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.DGV_Viw.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.DGV_Viw.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.DGV_Viw.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.DGV_Viw.OptionsBehavior.AllowIncrementalSearch = true;
            this.DGV_Viw.OptionsBehavior.Editable = false;
            this.DGV_Viw.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.PlainText;
            this.DGV_Viw.OptionsCustomization.AllowBandMoving = false;
            this.DGV_Viw.OptionsCustomization.AllowChangeBandParent = true;
            this.DGV_Viw.OptionsFind.FindDelay = 120;
            this.DGV_Viw.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.DGV_Viw.OptionsFind.FindNullPrompt = "عبارت جهت جستجو را وارد نمایید";
            this.DGV_Viw.OptionsFind.ShowCloseButton = false;
            this.DGV_Viw.OptionsFind.ShowFindButton = false;
            this.DGV_Viw.OptionsLayout.Columns.AddNewColumns = false;
            this.DGV_Viw.OptionsLayout.Columns.StoreAllOptions = true;
            this.DGV_Viw.OptionsLayout.Columns.StoreAppearance = true;
            this.DGV_Viw.OptionsLayout.StoreDataSettings = false;
            this.DGV_Viw.OptionsLayout.StoreVisualOptions = false;
            this.DGV_Viw.OptionsMenu.DialogFormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.DGV_Viw.OptionsMenu.EnableColumnMenu = false;
            this.DGV_Viw.OptionsMenu.EnableFooterMenu = false;
            this.DGV_Viw.OptionsMenu.EnableGroupPanelMenu = false;
            this.DGV_Viw.OptionsMenu.ShowFooterItem = true;
            this.DGV_Viw.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.DGV_Viw.OptionsNavigation.EnterMoveNextColumn = true;
            this.DGV_Viw.OptionsPrint.AutoWidth = false;
            this.DGV_Viw.OptionsPrint.PrintFilterInfo = true;
            this.DGV_Viw.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.DGV_Viw.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.DGV_Viw.OptionsView.AllowHtmlDrawGroups = false;
            this.DGV_Viw.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateFocusedItem;
            this.DGV_Viw.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.DGV_Viw.OptionsView.BestFitUseErrorInfo = DevExpress.Utils.DefaultBoolean.True;
            this.DGV_Viw.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.DGV_Viw.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
            this.DGV_Viw.OptionsView.RowAutoHeight = true;
            this.DGV_Viw.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.DGV_Viw.OptionsView.ShowFooter = true;
            this.DGV_Viw.OptionsView.ShowGroupPanel = false;
            this.DGV_Viw.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator;
            this.DGV_Viw.RowHeight = 0;
            this.DGV_Viw.ViewCaptionHeight = 20;
            this.DGV_Viw.CustomDrawBandHeader += new DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventHandler(this.DGV_Viw_CustomDrawBandHeader);
            this.DGV_Viw.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.DGV_Viw_CustomDrawCell);
            this.DGV_Viw.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.DGV_Viw_ColumnWidthChanged);
            this.DGV_Viw.RowLoaded += new DevExpress.XtraGrid.Views.Base.RowEventHandler(this.DGV_Viw_RowLoaded);
            this.DGV_Viw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGV_Viw_KeyDown);
            this.DGV_Viw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DGV_Viw_KeyPress);
            this.DGV_Viw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_Viw_MouseDown);
            this.DGV_Viw.DoubleClick += new System.EventHandler(this.DGV_Viw_DoubleClick);
            this.DGV_Viw.DataSourceChanged += new System.EventHandler(this.DGV_Viw_DataSourceChanged);
            // 
            // banded
            // 
            this.banded.AppearanceHeader.ForeColor = System.Drawing.Color.Red;
            this.banded.AppearanceHeader.Options.UseForeColor = true;
            this.banded.Caption = "Other";
            this.banded.Name = "banded";
            this.banded.OptionsBand.AllowSize = false;
            this.banded.VisibleIndex = 0;
            this.banded.Width = 130;
            // 
            // chkFind
            // 
            this.chkFind.Location = new System.Drawing.Point(200, 2);
            this.chkFind.Name = "chkFind";
            this.chkFind.Properties.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
            this.chkFind.Properties.Appearance.ForeColor = System.Drawing.SystemColors.Menu;
            this.chkFind.Properties.Appearance.Options.UseBackColor = true;
            this.chkFind.Properties.Appearance.Options.UseForeColor = true;
            this.chkFind.Properties.Caption = "جستجوی هوشمند";
            this.chkFind.Properties.LookAndFeel.SkinName = "VS2010";
            this.chkFind.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chkFind.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkFind.Size = new System.Drawing.Size(133, 19);
            this.chkFind.TabIndex = 13;
            this.chkFind.Visible = false;
            // 
            // Tim_DoubleClick
            // 
            this.Tim_DoubleClick.Interval = 500;
            this.Tim_DoubleClick.Tick += new System.EventHandler(this.Tim_DoubleClick_Tick);
            // 
            // schControl
            // 
            this.schControl.Client = this.DGV;
            this.schControl.Location = new System.Drawing.Point(53, 2);
            this.schControl.Name = "schControl";
            this.schControl.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.schControl.Properties.Appearance.Options.UseBackColor = true;
            this.schControl.Properties.Appearance.Options.UseTextOptions = true;
            this.schControl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.schControl.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.schControl.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.schControl.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.schControl.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.schControl.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.schControl.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.schControl.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.schControl.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.schControl.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.schControl.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.schControl.Properties.AppearanceItemHighlight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.schControl.Properties.AppearanceItemHighlight.Options.UseBackColor = true;
            this.schControl.Properties.AppearanceItemSelected.BackColor = System.Drawing.Color.Red;
            this.schControl.Properties.AppearanceItemSelected.Options.UseBackColor = true;
            this.schControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.schControl.Properties.Client = this.DGV;
            this.schControl.Properties.FindDelay = 500;
            this.schControl.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.schControl.Properties.NullValuePrompt = "Enter the desired word to search";
            this.schControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.schControl.Size = new System.Drawing.Size(200, 20);
            this.schControl.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(135)))), ((int)(((byte)(198)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.schLabel);
            this.panel1.Controls.Add(this.schControl);
            this.panel1.Controls.Add(this.chkFind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(919, 30);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pnlButton
            // 
            this.pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.btnTest);
            this.pnlButton.Location = new System.Drawing.Point(254, 0);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(665, 28);
            this.pnlButton.TabIndex = 17;
            this.pnlButton.Visible = false;
            this.pnlButton.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlButton_Paint);
            // 
            // btnTest
            // 
            this.btnTest.Appearance.Options.UseTextOptions = true;
            this.btnTest.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnTest.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnTest.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTest.ImageOptions.Image = global::MyCom.Properties.Resources.Calendar_1_28;
            this.btnTest.Location = new System.Drawing.Point(525, 0);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(140, 28);
            this.btnTest.TabIndex = 16;
            this.btnTest.Text = "simpleButton1";
            this.btnTest.Visible = false;
            // 
            // schLabel
            // 
            this.schLabel.Appearance.ForeColor = System.Drawing.Color.White;
            this.schLabel.Appearance.Options.UseForeColor = true;
            this.schLabel.Appearance.Options.UseTextOptions = true;
            this.schLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.schLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.schLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.schLabel.Location = new System.Drawing.Point(5, 3);
            this.schLabel.Name = "schLabel";
            this.schLabel.Size = new System.Drawing.Size(46, 25);
            this.schLabel.TabIndex = 16;
            this.schLabel.Text = "Search";
            // 
            // pnlViewHeader
            // 
            this.pnlViewHeader.AutoScroll = true;
            this.pnlViewHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlViewHeader.Controls.Add(this.labelControl2);
            this.pnlViewHeader.Controls.Add(this.labelControl1);
            this.pnlViewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlViewHeader.Location = new System.Drawing.Point(0, 30);
            this.pnlViewHeader.Name = "pnlViewHeader";
            this.pnlViewHeader.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.pnlViewHeader.Size = new System.Drawing.Size(919, 59);
            this.pnlViewHeader.TabIndex = 16;
            this.pnlViewHeader.Visible = false;
            this.pnlViewHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlViewHeader_Paint);
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(80, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(268, 59);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "labelControl2";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(17, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 59);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "labelControl1";
            // 
            // Data_Grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.pnlViewHeader);
            this.Controls.Add(this.panel1);
            this.Name = "Data_Grid";
            this.Size = new System.Drawing.Size(919, 573);
            this.Load += new System.EventHandler(this.Data_Grid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Viw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schControl.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlViewHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl DGV;
        public DevExpress.XtraEditors.CheckEdit chkFind;
        private System.Windows.Forms.Timer Tim_DoubleClick;
        public DevExpress.XtraGrid.Views.BandedGrid.BandedGridView DGV_Viw;
        public DevExpress.XtraEditors.SearchControl schControl;
        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.LabelControl schLabel;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand banded;
        public System.Windows.Forms.Panel pnlButton;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        public System.Windows.Forms.Panel pnlViewHeader;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        //  private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
