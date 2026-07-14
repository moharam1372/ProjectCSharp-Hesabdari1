namespace MyCom.Object
{
    partial class Date
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
            this.timer1 = new System.Windows.Forms.Timer();
            this.Cmb_Day = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Cmb_Month = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Cmb_Year = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.Cmb_Day.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cmb_Month.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cmb_Year.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Cmb_Day
            // 
            this.Cmb_Day.Location = new System.Drawing.Point(119, 0);
            this.Cmb_Day.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_Day.Name = "Cmb_Day";
            this.Cmb_Day.Properties.Appearance.Font = new System.Drawing.Font("B Yekan", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Cmb_Day.Properties.Appearance.Options.UseFont = true;
            this.Cmb_Day.Properties.Appearance.Options.UseTextOptions = true;
            this.Cmb_Day.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Day.Properties.AppearanceDisabled.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Day.Properties.AppearanceDisabled.Options.UseFont = true;
            this.Cmb_Day.Properties.AppearanceDropDown.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Day.Properties.AppearanceDropDown.Options.UseFont = true;
            this.Cmb_Day.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.Cmb_Day.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Day.Properties.AppearanceFocused.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Day.Properties.AppearanceFocused.Options.UseFont = true;
            this.Cmb_Day.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.Cmb_Day.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Day.Properties.AutoComplete = false;
            this.Cmb_Day.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Cmb_Day.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Cmb_Day.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Cmb_Day.Properties.LookAndFeel.SkinName = "Foggy";
            this.Cmb_Day.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Cmb_Day.Properties.MaxLength = 2;
            this.Cmb_Day.Size = new System.Drawing.Size(50, 36);
            this.Cmb_Day.TabIndex = 1;
            this.Cmb_Day.SelectedIndexChanged += new System.EventHandler(this.Cmb_Day_SelectedIndexChanged);
            this.Cmb_Day.TextChanged += new System.EventHandler(this.Cmb_Day_TextChanged);
            this.Cmb_Day.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cmb_Day_KeyDown);
            this.Cmb_Day.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cmb_Day_KeyPress);
            this.Cmb_Day.Leave += new System.EventHandler(this.Cmb_Day_Leave);
            // 
            // Cmb_Month
            // 
            this.Cmb_Month.Location = new System.Drawing.Point(69, 0);
            this.Cmb_Month.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_Month.Name = "Cmb_Month";
            this.Cmb_Month.Properties.Appearance.Font = new System.Drawing.Font("B Yekan", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Cmb_Month.Properties.Appearance.Options.UseFont = true;
            this.Cmb_Month.Properties.Appearance.Options.UseTextOptions = true;
            this.Cmb_Month.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Month.Properties.AppearanceDisabled.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Month.Properties.AppearanceDisabled.Options.UseFont = true;
            this.Cmb_Month.Properties.AppearanceDropDown.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Month.Properties.AppearanceDropDown.Options.UseFont = true;
            this.Cmb_Month.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.Cmb_Month.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Month.Properties.AppearanceFocused.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Month.Properties.AppearanceFocused.Options.UseFont = true;
            this.Cmb_Month.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.Cmb_Month.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Month.Properties.AutoComplete = false;
            this.Cmb_Month.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Cmb_Month.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Cmb_Month.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Cmb_Month.Properties.LookAndFeel.SkinName = "Foggy";
            this.Cmb_Month.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Cmb_Month.Properties.MaxLength = 2;
            this.Cmb_Month.Size = new System.Drawing.Size(50, 36);
            this.Cmb_Month.TabIndex = 2;
            this.Cmb_Month.SelectedIndexChanged += new System.EventHandler(this.Cmb_Month_SelectedIndexChanged);
            this.Cmb_Month.TextChanged += new System.EventHandler(this.Cmb_Month_TextChanged);
            this.Cmb_Month.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cmb_Month_KeyPress);
            this.Cmb_Month.Leave += new System.EventHandler(this.Cmb_Month_Leave);
            // 
            // Cmb_Year
            // 
            this.Cmb_Year.Location = new System.Drawing.Point(0, 0);
            this.Cmb_Year.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_Year.Name = "Cmb_Year";
            this.Cmb_Year.Properties.Appearance.Font = new System.Drawing.Font("B Yekan", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Cmb_Year.Properties.Appearance.Options.UseFont = true;
            this.Cmb_Year.Properties.Appearance.Options.UseTextOptions = true;
            this.Cmb_Year.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Year.Properties.AppearanceDisabled.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Year.Properties.AppearanceDisabled.Options.UseFont = true;
            this.Cmb_Year.Properties.AppearanceDropDown.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Year.Properties.AppearanceDropDown.Options.UseFont = true;
            this.Cmb_Year.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.Cmb_Year.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Year.Properties.AppearanceFocused.Font = new System.Drawing.Font("B Yekan", 14.25F);
            this.Cmb_Year.Properties.AppearanceFocused.Options.UseFont = true;
            this.Cmb_Year.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.Cmb_Year.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cmb_Year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Cmb_Year.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Cmb_Year.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Cmb_Year.Properties.LookAndFeel.SkinName = "Foggy";
            this.Cmb_Year.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Cmb_Year.Properties.MaxLength = 4;
            this.Cmb_Year.Size = new System.Drawing.Size(69, 36);
            this.Cmb_Year.TabIndex = 3;
            this.Cmb_Year.TextChanged += new System.EventHandler(this.Cmb_Year_TextChanged);
            this.Cmb_Year.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cmb_Year_KeyPress);
            this.Cmb_Year.Leave += new System.EventHandler(this.Cmb_Year_Leave);
            // 
            // Date
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.Cmb_Year);
            this.Controls.Add(this.Cmb_Month);
            this.Controls.Add(this.Cmb_Day);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "Date";
            this.Size = new System.Drawing.Size(380, 332);
            this.Load += new System.EventHandler(this.DateTavalod_Load);
            this.Leave += new System.EventHandler(this.Date_Leave);
            this.Resize += new System.EventHandler(this.DateTavalod_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Cmb_Day.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cmb_Month.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cmb_Year.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        public DevExpress.XtraEditors.ComboBoxEdit Cmb_Day;
        public DevExpress.XtraEditors.ComboBoxEdit Cmb_Month;
        public DevExpress.XtraEditors.ComboBoxEdit Cmb_Year;
    }
}
