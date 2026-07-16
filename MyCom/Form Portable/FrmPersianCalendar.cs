using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace MyCom.Form_Portable
{
    /// <summary>
    /// تقویم شمسی (جلالی) با DevExpress WinForms
    /// راست‌چین، انتخاب ماه/سال، و انتخاب روز از طریق Grid (شنبه تا جمعه)
    /// </summary>
    public class FrmPersianCalendar : XtraForm
    {
        // ---------- تنظیمات فونت ----------
        private readonly Font AppFont = new Font("Samim FD", 10F, FontStyle.Regular);
        private readonly Font AppFontBold = new Font("Samim FD", 10F, FontStyle.Bold);

        // ---------- کنترل‌ها ----------
        private LabelControl lblMonth;
        private LookUpEdit lueMonth;
        private LabelControl lblYear;
        private SpinEdit speYear;
        private GridControl gridControl1;
        private GridView gridView1;
        private LabelControl lblSelected;

        // ---------- داده‌ها ----------
        private DataTable dtCalendar;
        private readonly PersianCalendar pc = new PersianCalendar();

        // نام ماه‌های شمسی، از فروردین تا اسفند
        private readonly string[] monthNames = new[]
        {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
        };

        // نام روزهای هفته، از شنبه تا جمعه (ترتیب ستون‌های گرید)
        private readonly string[] weekDayNames = new[]
        {
            "شنبه", "یک‌شنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه"
        };

        // موقعیت روز انتخاب‌شده در گرید (برای هایلایت)
        private int selectedRowHandle = -1;
        private int selectedColIndex = -1;
        private int? selectedDay = null;

        public int SelectedYear { get; private set; }
        public int SelectedMonth { get; private set; } // 1..12
        public int? SelectedDay => selectedDay;

        public event EventHandler SelectedDateChanged;

        public FrmPersianCalendar()
        {
            InitializeComponents();

            // تاریخ شمسی امروز به عنوان مقدار پیش‌فرض
            DateTime today = DateTime.Now;
            SelectedYear = pc.GetYear(today);
            SelectedMonth = pc.GetMonth(today);

            PopulateMonths();

            // مهم: باید قبل از مقداردهی به EditValue ها ساخته بشه،
            // چون مقداردهی به EditValue بلافاصله رویداد EditValueChanged
            // را صدا می‌زند و RefreshCalendar() به dtCalendar نیاز دارد.
            BuildGridSkeleton();

            speYear.EditValue = SelectedYear;
            lueMonth.EditValue = SelectedMonth;

            RefreshCalendar();
        }

        // ==========================================================
        //  ساخت و چیدمان کنترل‌ها (راست‌چین)
        // ==========================================================
        private void InitializeComponents()
        {
            this.Text = "تقویم شمسی";
            this.Size = new Size(480, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- راست‌چین کردن کل فرم ---
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Font = AppFont;

            lblMonth = new LabelControl
            {
                Text = "ماه:",
                Location = new Point(this.ClientSize.Width - 60, 20),
                Font = AppFont,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            lueMonth = new LookUpEdit
            {
                Location = new Point(this.ClientSize.Width - 220, 17),
                Size = new Size(150, 24),
                Font = AppFont,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            lueMonth.Properties.ShowHeader = false;
            lueMonth.Properties.ShowFooter = false;
            lueMonth.Properties.NullText = "";
            lueMonth.Properties.Appearance.Font = AppFont;
            lueMonth.Properties.AppearanceDropDown.Font = AppFont;
            lueMonth.EditValueChanged += (s, e) => RefreshCalendar();

            lblYear = new LabelControl
            {
                Text = "سال:",
                Location = new Point(this.ClientSize.Width - 260, 20),
                Font = AppFont,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            speYear = new SpinEdit
            {
                Location = new Point(this.ClientSize.Width - 360, 17),
                Size = new Size(90, 24),
                Font = AppFont,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            speYear.Properties.MinValue = 1300;
            speYear.Properties.MaxValue = 1500;
            speYear.Properties.Mask.EditMask = "0000";
            speYear.Properties.Mask.MaskType = MaskType.Numeric;
            speYear.Properties.Appearance.Font = AppFont;
            speYear.EditValueChanged += (s, e) => RefreshCalendar();

            gridControl1 = new GridControl
            {
                Location = new Point(20, 60),
                Size = new Size(this.ClientSize.Width - 40, 320),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                RightToLeft = RightToLeft.Yes
            };

            gridView1 = new GridView(gridControl1);
            gridControl1.MainView = gridView1;
            gridControl1.ViewCollection.Add(gridView1);

            lblSelected = new LabelControl
            {
                Text = "روز انتخاب شده: -",
                Location = new Point(20, 395),
                Font = AppFontBold,
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            this.Controls.Add(lueMonth);
            this.Controls.Add(lblMonth);
            this.Controls.Add(speYear);
            this.Controls.Add(lblYear);
            this.Controls.Add(gridControl1);
            this.Controls.Add(lblSelected);
        }

        // ==========================================================
        //  پر کردن لیست ماه‌ها در LookUpEdit
        // ==========================================================
        private void PopulateMonths()
        {
            DataTable dtMonths = new DataTable();
            dtMonths.Columns.Add("Id", typeof(int));
            dtMonths.Columns.Add("Name", typeof(string));

            for (int i = 0; i < monthNames.Length; i++)
                dtMonths.Rows.Add(i + 1, monthNames[i]);

            lueMonth.Properties.DataSource = dtMonths;
            lueMonth.Properties.DisplayMember = "Name";
            lueMonth.Properties.ValueMember = "Id";
            lueMonth.Properties.Columns.Clear();
            lueMonth.Properties.Columns.Add(new LookUpColumnInfo("Name", "نام ماه"));
        }

        // ==========================================================
        //  ساخت اسکلت جدول (۷ ستون × ۶ ردیف ثابت)
        // ==========================================================
        private void BuildGridSkeleton()
        {
            dtCalendar = new DataTable();
            for (int c = 0; c < 7; c++)
                dtCalendar.Columns.Add("col" + c, typeof(string));

            // حداکثر ۶ هفته برای پوشش هر حالتی کافی است
            for (int r = 0; r < 6; r++)
                dtCalendar.Rows.Add(dtCalendar.NewRow());

            gridControl1.DataSource = dtCalendar;

            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowColumnHeaders = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsNavigation.AutoFocusNewRow = false;
            gridView1.OptionsView.ColumnAutoWidth = true;
            gridView1.RowHeight = 40;
            gridView1.Appearance.Row.Font = AppFont;
            gridView1.Appearance.HeaderPanel.Font = AppFontBold;

            for (int i = 0; i < 7; i++)
            {
                GridColumn col = gridView1.Columns["col" + i];
                col.Caption = weekDayNames[i];
                col.VisibleIndex = i;
                col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                col.OptionsColumn.AllowEdit = false;
                col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            }

            gridView1.CustomDrawCell += GridView1_CustomDrawCell;
            gridView1.MouseDown += GridView1_MouseDown;
        }

        // ==========================================================
        //  محاسبه‌ی روزها بر اساس ماه/سال انتخاب‌شده و پر کردن گرید
        // ==========================================================
        private void RefreshCalendar()
        {
            try
            {
                if (lueMonth.EditValue == null || speYear.EditValue == null)
                    return;

                // محافظ: تا وقتی گرید ساخته نشده، کاری انجام نده
                if (dtCalendar == null)
                    return;

                SelectedMonth = Convert.ToInt32(lueMonth.EditValue);
                SelectedYear = Convert.ToInt32(speYear.EditValue);

                // تعداد روزهای ماه با استفاده از PersianCalendar
                // (۱ تا ۶: ۳۱ روز | ۷ تا ۱۱: ۳۰ روز | ۱۲: ۲۹ یا ۳۰ روز در کبیسه)
                int daysInMonth = pc.GetDaysInMonth(SelectedYear, SelectedMonth);

                // روز هفته‌ی اولین روز ماه
                DateTime firstDayGregorian = pc.ToDateTime(SelectedYear, SelectedMonth, 1, 0, 0, 0, 0);
                DayOfWeek dow = firstDayGregorian.DayOfWeek;

                // نگاشت به ترتیب هفته‌ی ایرانی: شنبه=0 ... جمعه=6
                int startCol = (dow == DayOfWeek.Saturday) ? 0 : (int)dow + 1;

                // خالی کردن جدول
                foreach (DataRow row in dtCalendar.Rows)
                    for (int c = 0; c < 7; c++)
                        row["col" + c] = "";

                int day = 1;
                int rowIndex = 0;
                int colIndex = startCol;

                while (day <= daysInMonth)
                {
                    dtCalendar.Rows[rowIndex]["col" + colIndex] = day.ToString();
                    day++;
                    colIndex++;
                    if (colIndex > 6)
                    {
                        colIndex = 0;
                        rowIndex++;
                    }
                }

                // ریست انتخاب قبلی چون ماه/سال عوض شده
                selectedRowHandle = -1;
                selectedColIndex = -1;
                selectedDay = null;
                lblSelected.Text = "روز انتخاب شده: -";

                gridView1.RefreshData();
                gridControl1.Invalidate();

                SelectedDateChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                
            }
        }

        // ==========================================================
        //  انتخاب روز با کلیک روی سلول گرید
        // ==========================================================
        private void GridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell && hitInfo.Column != null)
            {
                object cellValue = gridView1.GetRowCellValue(hitInfo.RowHandle, hitInfo.Column);
                string text = cellValue?.ToString();

                if (string.IsNullOrEmpty(text))
                    return; // سلول خالی (خارج از بازه‌ی روزهای ماه)

                selectedRowHandle = hitInfo.RowHandle;
                selectedColIndex = hitInfo.Column.VisibleIndex;
                selectedDay = int.Parse(text);

                lblSelected.Text = string.Format("روز انتخاب شده: {0} {1} {2}",
                    selectedDay, monthNames[SelectedMonth - 1], SelectedYear);

                gridControl1.Invalidate();
                SelectedDateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // ==========================================================
        //  ظاهر سلول‌ها: هایلایت روز انتخاب‌شده + رنگ جمعه
        // ==========================================================
        private void GridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            // جمعه (آخرین ستون) با رنگ متفاوت نمایش داده شود
            if (e.Column.VisibleIndex == 6)
                e.Appearance.ForeColor = Color.Firebrick;

            // هایلایت سلول انتخاب‌شده
            if (e.RowHandle == selectedRowHandle && e.Column.VisibleIndex == selectedColIndex)
            {
                e.Appearance.BackColor = Color.FromArgb(0, 120, 215);
                e.Appearance.ForeColor = Color.White;
                e.Appearance.Font = AppFontBold;
            }
        }

        //[STAThread]
        //public static void Main()
        //{
        //    DevExpress.UserSkins.BonusSkins.Register();
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new FrmPersianCalendar());
        //}
    }
}