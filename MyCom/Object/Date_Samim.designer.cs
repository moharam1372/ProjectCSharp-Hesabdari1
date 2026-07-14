namespace MyCom.Object
{
    partial class Date_Samim
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
            timer1 = new System.Windows.Forms.Timer(components);
            Cmb_Day = new DevExpress.XtraEditors.ComboBoxEdit();
            Cmb_Month = new DevExpress.XtraEditors.ComboBoxEdit();
            Cmb_Year = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)Cmb_Day.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Cmb_Month.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Cmb_Year.Properties).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // Cmb_Day
            // 
            Cmb_Day.Location = new System.Drawing.Point(111, 0);
            Cmb_Day.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Cmb_Day.Name = "Cmb_Day";
            Cmb_Day.Properties.Appearance.Font = new System.Drawing.Font("Samim FD", 12.75F, System.Drawing.FontStyle.Bold);
            Cmb_Day.Properties.Appearance.Options.UseFont = true;
            Cmb_Day.Properties.Appearance.Options.UseTextOptions = true;
            Cmb_Day.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Day.Properties.AppearanceDisabled.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Day.Properties.AppearanceDisabled.Options.UseFont = true;
            Cmb_Day.Properties.AppearanceDropDown.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Day.Properties.AppearanceDropDown.Options.UseFont = true;
            Cmb_Day.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            Cmb_Day.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Day.Properties.AppearanceFocused.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Day.Properties.AppearanceFocused.Options.UseFont = true;
            Cmb_Day.Properties.AppearanceFocused.Options.UseTextOptions = true;
            Cmb_Day.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Day.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Day.Properties.AppearanceReadOnly.Options.UseFont = true;
            Cmb_Day.Properties.AutoComplete = false;
            Cmb_Day.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            Cmb_Day.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            Cmb_Day.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            Cmb_Day.Properties.LookAndFeel.SkinName = "Foggy";
            Cmb_Day.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            Cmb_Day.Properties.MaxLength = 2;
            Cmb_Day.Size = new System.Drawing.Size(50, 32);
            Cmb_Day.TabIndex = 1;
            Cmb_Day.SelectedIndexChanged += Cmb_Day_SelectedIndexChanged;
            Cmb_Day.TextChanged += Cmb_Day_TextChanged;
            Cmb_Day.KeyDown += Cmb_Day_KeyDown;
            Cmb_Day.KeyPress += Cmb_Day_KeyPress;
            Cmb_Day.Leave += Cmb_Day_Leave;
            // 
            // Cmb_Month
            // 
            Cmb_Month.Location = new System.Drawing.Point(61, 0);
            Cmb_Month.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Cmb_Month.Name = "Cmb_Month";
            Cmb_Month.Properties.Appearance.Font = new System.Drawing.Font("Samim FD", 12.75F, System.Drawing.FontStyle.Bold);
            Cmb_Month.Properties.Appearance.Options.UseFont = true;
            Cmb_Month.Properties.Appearance.Options.UseTextOptions = true;
            Cmb_Month.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Month.Properties.AppearanceDisabled.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Month.Properties.AppearanceDisabled.Options.UseFont = true;
            Cmb_Month.Properties.AppearanceDropDown.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Month.Properties.AppearanceDropDown.Options.UseFont = true;
            Cmb_Month.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            Cmb_Month.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Month.Properties.AppearanceFocused.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Month.Properties.AppearanceFocused.Options.UseFont = true;
            Cmb_Month.Properties.AppearanceFocused.Options.UseTextOptions = true;
            Cmb_Month.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Month.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Month.Properties.AppearanceReadOnly.Options.UseFont = true;
            Cmb_Month.Properties.AutoComplete = false;
            Cmb_Month.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            Cmb_Month.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            Cmb_Month.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            Cmb_Month.Properties.LookAndFeel.SkinName = "Foggy";
            Cmb_Month.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            Cmb_Month.Properties.MaxLength = 2;
            Cmb_Month.Size = new System.Drawing.Size(50, 32);
            Cmb_Month.TabIndex = 2;
            Cmb_Month.SelectedIndexChanged += Cmb_Month_SelectedIndexChanged;
            Cmb_Month.TextChanged += Cmb_Month_TextChanged;
            Cmb_Month.KeyPress += Cmb_Month_KeyPress;
            Cmb_Month.Leave += Cmb_Month_Leave;
            // 
            // Cmb_Year
            // 
            Cmb_Year.Location = new System.Drawing.Point(0, 0);
            Cmb_Year.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Cmb_Year.Name = "Cmb_Year";
            Cmb_Year.Properties.Appearance.Font = new System.Drawing.Font("Samim FD", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178);
            Cmb_Year.Properties.Appearance.Options.UseFont = true;
            Cmb_Year.Properties.Appearance.Options.UseTextOptions = true;
            Cmb_Year.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Year.Properties.AppearanceDisabled.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Year.Properties.AppearanceDisabled.Options.UseFont = true;
            Cmb_Year.Properties.AppearanceDropDown.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Year.Properties.AppearanceDropDown.Options.UseFont = true;
            Cmb_Year.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            Cmb_Year.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Year.Properties.AppearanceFocused.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Year.Properties.AppearanceFocused.Options.UseFont = true;
            Cmb_Year.Properties.AppearanceFocused.Options.UseTextOptions = true;
            Cmb_Year.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Cmb_Year.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            Cmb_Year.Properties.AppearanceReadOnly.Options.UseFont = true;
            Cmb_Year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            Cmb_Year.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            Cmb_Year.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            Cmb_Year.Properties.LookAndFeel.SkinName = "Foggy";
            Cmb_Year.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            Cmb_Year.Properties.MaxLength = 4;
            Cmb_Year.Size = new System.Drawing.Size(61, 32);
            Cmb_Year.TabIndex = 3;
            Cmb_Year.SelectedIndexChanged += Cmb_Year_SelectedIndexChanged;
            Cmb_Year.TextChanged += Cmb_Year_TextChanged;
            Cmb_Year.KeyPress += Cmb_Year_KeyPress;
            Cmb_Year.Leave += Cmb_Year_Leave;
            // 
            // Date_Samim
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ActiveCaption;
            Controls.Add(Cmb_Year);
            Controls.Add(Cmb_Month);
            Controls.Add(Cmb_Day);
            Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178);
            Name = "Date_Samim";
            Size = new System.Drawing.Size(380, 332);
            Load += DateTavalod_Load;
            Leave += Date_Leave;
            Resize += DateTavalod_Resize;
            ((System.ComponentModel.ISupportInitialize)Cmb_Day.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Cmb_Month.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Cmb_Year.Properties).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        public DevExpress.XtraEditors.ComboBoxEdit Cmb_Day;
        public DevExpress.XtraEditors.ComboBoxEdit Cmb_Month;
        public DevExpress.XtraEditors.ComboBoxEdit Cmb_Year;
    }
}
