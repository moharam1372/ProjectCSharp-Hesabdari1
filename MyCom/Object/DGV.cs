using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using MyCom.Class;


namespace MyCom.Object
{
    public partial class Data_Grid : XtraUserControl
    {
        private readonly ClsFont _font = new ClsFont(ClsFont.enumFont.samim);
        //  public event DGV MyNewEvent;

        //  private Class_Font _font = new Class_Font();
        //  public event RowCellClickEventHandler کلیک_انتخاب;

        private Class_Text _classText = new Class_Text();

        //  public bool rowEvenOld = false;
        private Color _color = Color.White;
        private bool Chk = false;
        private bool Sel = false;
        private bool Del = false;
        private bool Edi = false;
        private Color lineColor;
        private bool Show_Filter;
        private bool Hoshmand_Filter;
        private bool BestFit = true;

        private bool showTag = false;

        //#region Creted Event
        //[Category("Mojtaba Yadavar")]
        //public event DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler z_RowCellClick;
        //public void RowCellClickEvent(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        //{
        //    z_RowCellClick?.Invoke(this.DGV_Viw, e);
        //}


        //  public event DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler z_RowCellClick;
        [Category("Double Click Row & Cell")]
        public event System.EventHandler z_RowCellDoubleClick;

        //   DGV_Viw.DoubleClick += new System.EventHandler(this.DGV_Viw_DoubleClick);


        //DoubleClick
        private void DGV_Viw_DoubleClick(object sender, EventArgs e)
        {
            var getDbc = CheckDoubleClickRowCell(sender, e);
            if (getDbc)
                z_RowCellDoubleClick?.Invoke(this.DGV_Viw, e);
        }
        public bool CheckDoubleClickRowCell(object s, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            if (s is GridView view && ea != null)
            {
                GridHitInfo info = view.CalcHitInfo(ea.Location);
                if (info.InRow || info.InRowCell)
                    return true;
            }

            return false;
        }
        //[Category("Mojtaba Yadavar")]
        //public event FocusedColumnChangedEventHandler z_FocusedColumnChanged;
        //public void FocusedColumnChangedEvent(object sender, FocusedColumnChangedEventArgs e)
        //{
        //    z_FocusedColumnChanged?.Invoke(DGV_Viw, e);
        //}

        //[Category("Mojtaba Yadavar")]
        //public event CellValueChangedEventHandler z_CellValueChanged;
        //public void CellValueChangedEvent(object sender, CellValueChangedEventArgs e)
        //{
        //    z_CellValueChanged?.Invoke(DGV_Viw, e);
        //}

        //[Category("Mojtaba Yadavar")]
        //public event RowStyleEventHandler z_RowStyle;
        //public void RowStyleEvent(object sender, RowStyleEventArgs e)
        //{
        //    z_RowStyle?.Invoke(DGV_Viw, e);
        //}

        //[Category("Mojtaba Yadavar")]
        //public event RowCellStyleEventHandler z_RowCellStyle;
        //public void z_RowCellStyleEvent(object sender, RowCellStyleEventArgs e)
        //{
        //    z_RowCellStyle?.Invoke(DGV_Viw, e);
        //}

        //[Category("Mojtaba Yadavar")]
        //public event CellValueChangingEventHandler z_RowCellClick;
        //public void RowCellClickEvent(object sender, RowCellClickEventArgs e)
        //{
        //    z_RowCellClick?.Invoke(this, e);
        //}

        //[Category("Mojtaba Yadavar")]
        //public event RowCellClickEventHandler z_RowCellClick;
        //public void RowCellClickEvent(object sender, RowCellClickEventArgs e)
        //{
        //    z_RowCellClick?.Invoke(this, e);
        //}


        //dgvDetailsCreatedTmp.DGV_Viw.CellValueChanging += dgvDetailsCreatedTmp_CellValueChanging;

        ////  dgvDetailsCreatedTmp.DGV_Viw.BeforeLeaveRow += dgvDetailsCreatedTmp_BeforeLeaveRow;
        //dgvDetailsCreatedTmp.DGV_Viw.RowCountChanged += dgvDetailsCreatedTmp_RowCountChanged;
        //dgvDetailsCreatedTmp.DGV_Viw.InitNewRow += dgvDetailsCreatedTmp_InitNewRow;
        //dgvDetailsCreatedTmp.DGV_Viw.CustomColumnDisplayText += dgvDetailsCreatedTmp_CustomColumnDisplayText;

        // #endregion


        // private bool Show_Scroll ;

        // [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        // [Category("عملیات")]
        // [Description("The text displayed by the control.")]
        // [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        // [TypeConverter(typeof(ExpandableObjectConverter))]
        // [TypeConverter(typeof(Mojtaba_Yadavar_Convert)),Description("Sepehran")]

        //[DefaultValue(typeof (Color), "0, 0, 255")]
        //public Color مجتبی_یادآور
        //{
        //    get { return lineColor; }
        //    set
        //    {
        //        lineColor = value;
        //        Invalidate();
        //    }
        //}

        //string A;
        //[Category("Appearance")]
        //[Description("The text displayed by the control.")]
        //[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[EditorAttribute(typeof(System.Windows.Forms.Design.ComponentEditorForm), typeof(System.Drawing.Design.UITypeEditor))]

        private void Data_Grid_Load(object sender, EventArgs e)
        {
            //  Lic1();
            // banded.Visible = false;
            DevExpress.UserSkins.BonusSkins.Register();
            DGV_Viw.OptionsView.ShowBands = false;
            panel1.Visible = Show_Filter;
            DGV_Viw.OptionsFind.AlwaysVisible = false;
            chkFind.Visible = Hoshmand_Filter;
            // DGV_Viw.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
            DGV_Viw.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            // DGV.LookAndFeel.UseDefaultLookAndFeel = false;
            // DGV.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            // DGV.LookAndFeel.SkinName = "Office 2007 Blue";
            // DGV.LookAndFeel.SkinName = "Office 2013 Dark Gray";
            // banded.AppearanceHeader.Font = _font.ChangeFont(12);
        }

        #region (عملیات)

        [DefaultValue(false)]
        public bool Z_چکباکس
        {
            get { return Chk; }
            set { Chk = value; }
        }

        [DefaultValue(false)]
        public bool Z_انتخاب
        {
            get { return Sel; }
            set { Sel = value; }
        }

        [DefaultValue(false)]
        public bool Z_حذف
        {
            get { return Del; }
            set { Del = value; }
        }

        [DefaultValue(false)]
        public bool Z_ویرایش
        {
            get { return Edi; }
            set { Edi = value; }
        }

        #endregion(عملیات)

        #region (جستجو)

        [DefaultValue(false)]
        public bool Y_پنل_جستجو
        {
            get { return Show_Filter; }
            set { Show_Filter = value; }
        }

        #endregion (جستجو)

        #region (جستجوی هوشمند)

        [DefaultValue(false)]
        public bool Y_هوشمند
        {
            get { return Hoshmand_Filter; }
            set { Hoshmand_Filter = value; }
        }

        #endregion (جستجو)

        public Data_Grid()
        {
            InitializeComponent();

            DGV_Viw.RowCellClick += DGV_Viw_RowCellClick;
            DGV.GotFocus += DGV_GotFocus;
            DGV_Viw.CustomDrawCell += OnCustomDrawCell;
            DGV_Viw.RowStyle += DGV_Viw_RowStyle;
            DGV_Viw.TopRowChanged += DGV_Viw_TopRowChanged;
            DGV_Viw.GridMenuItemClick += DGV_Viw_GridMenuItemClick;
            DGV_Viw.KeyDown += (s, e) =>
            {
                // var getClm = GetColumn();
                if (e.Control && e.KeyCode == Keys.C)
                {
                    Clipboard.SetText(DGV_Viw.GetFocusedDisplayText());
                    e.Handled = true;
                }
            };
            // DGV_Viw.MouseMove += DGV_Viw_MouseMove;
            DGV_Viw.MouseUp += DGV_Viw_MouseUp;

            // DGV_Viw.DoubleClick += DGV_Viw_DoubleClick;
            // DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("Samim FD", 10);
            DGV_Viw.NewItemRowText = @"جهت افزودن اطلاعات جدید کلیک نمایید";
            // DGV_Viw.NewItemRowText
        }

        private void DGV_Viw_GridMenuItemClick(object sender, GridMenuItemClickEventArgs e)
        {
            // MessageBox.Show(e.SummaryItem.GetType().FullName);
        }
        private void DGV_Viw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var view = sender as GridView;
                GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
                if (hitInfo.Column == null)
                    return;

                bool riGLE = hitInfo.Column.RealColumnEdit is RepositoryItemGridLookUpEdit;
                bool riTE = hitInfo.Column.RealColumnEdit is RepositoryItemTextEdit;
                bool riHTL = hitInfo.Column.RealColumnEdit is RepositoryItemHypertextLabel;
                bool riTS = hitInfo.Column.RealColumnEdit is RepositoryItemToggleSwitch;
                //  bool riCB = hitInfo.Column.RealColumnEdit is RepositoryItemcheck;   

                bool riCE = hitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit;
                bool riBE = hitInfo.Column.RealColumnEdit is RepositoryItemButtonEdit;

                bool riTiE = hitInfo.Column.RealColumnEdit is RepositoryItemTimeEdit;

                if (hitInfo.InRowCell && riHTL)
                {
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    view.ShowEditor();

                    var edit = view.ActiveEditor as HypertextLabel;
                    if (edit == null)
                        return;
                    //   edit.IsOn = true;

                    view.CloseEditor();
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
                else if (hitInfo.InRowCell && riTE && riBE && !riGLE && !riTiE) // Location
                {
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    view.ShowEditor();

                    var edit = view.ActiveEditor as ButtonEdit;
                    if (edit == null)
                        return;
                    view.CloseEditor();
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
                else if (hitInfo.InRowCell && riGLE)
                {
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    view.ShowEditor();

                    var edit = view.ActiveEditor as GridLookUpEdit;
                    if (edit == null)
                        return;
                    //   edit.IsOn = true;
                    edit.ShowPopup();

                    // view.CloseEditor(); // call this method if you want to keep the view scrollable using the mouse wheel
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }

                else if (hitInfo.InRowCell && riTE && !riTiE)
                {
                    try
                    {
                        view.FocusedColumn = hitInfo.Column;
                        view.FocusedRowHandle = hitInfo.RowHandle;
                        view.ShowEditor();

                        var edit = view.ActiveEditor as TextEdit;
                        if (edit == null)
                            return;
                        //   edit.IsOn = true;
                        //  edit.Text = "1";

                        // view.CloseEditor(); // call this method if you want to keep the view scrollable using the mouse wheel
                        DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                    catch (Exception exception)
                    {

                    }
                }
                else if (hitInfo.InRowCell && riTS)
                {
                    // var view2 = sender as GridView;
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    view.ShowEditor();


                    // var aw1 = hitInfo.Column;
                    //var edit = view.EditingValue as ToggleSwitch;
                    //var edit = view.EditingValue as ToggleSwitch;
                    // var edit = view.ActiveEditor as ToggleSwitch;
                    var edit = view.ActiveEditor as ToggleSwitch;
                    if (edit == null)
                        return;
                    edit.Toggle();
                    // edit.IsOn = !edit.IsOn;
                    //  edit.Text = "1";

                    view.CloseEditor();
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
                else if (hitInfo.InRowCell && riTiE)
                {
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    view.ShowEditor();

                    var edit = view.ActiveEditor as TimeEdit;
                    if (edit == null)
                        return;
                    edit.ShowPopup();
                    //   view.CloseEditor();
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
                if (hitInfo.InRowCell && riCE)
                {
                    // var view2 = sender as GridView;
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    view.ShowEditor();

                    var edit = view.ActiveEditor as CheckEdit;
                    if (edit == null)
                        return;
                    edit.Checked = !edit.Checked;

                    view.CloseEditor();
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }
        private void DGV_Viw_RowStyle(object sender, RowStyleEventArgs e)
        {
            //   if (rowEvenOld)
            if (DGV_Viw.RowCount > 0)
                e.Appearance.BackColor = e.RowHandle % 2 == 0 ? _color : Color.White;
            // e.Appearance.BackColor = e.RowHandle % 2 == 0 ? Color.FromArgb(209, 245, 233) : Color.White;
        }

        /// <summary>
        /// آزمایشی
        /// </summary>
        int DC = 0;

        int clickOldRow;

        private void DGV_Viw_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.Column == null)
                return;
        }
        private void DGV_GotFocus(object sender, EventArgs e)
        {
            // _classText.FindGrid(DGV_Viw);
        }
        private void DGV__Click(object sender, EventArgs e)
        {

        }
        public void Grid_Structure(DataTable DT_Factor, GridControl DGV_Date, GridView DGV_Viw1, List<string> Name_Column, bool Allow_Edit = false,
            bool Allow_Sort = true, bool AutoFitColumn = true)
        {
            DGV_Viw.OptionsView.ShowBands = false;
            BestFit = AutoFitColumn;
            DT_Factor.Clear();
            DT_Factor.Columns.Clear();

            #region(افزودن افزونه های عملیاتی)

            if (Chk)
                DT_Factor.Columns.Add(" ", typeof(bool));

            if (Sel)
                DT_Factor.Columns.Add("انتخاب");

            if (Del)
                DT_Factor.Columns.Add("حذف");

            if (Edi)
                DT_Factor.Columns.Add("ویرایش");

            #endregion

            for (var i = 0; i < Name_Column.Count; i++)
            {
                if (Name_Column[i] == "تصویر")
                    DT_Factor.Columns.Add("تصویر", typeof(Image));
                else
                    DT_Factor.Columns.Add(Name_Column[i]);
            }

            DGV_Date.DataSource = DT_Factor;

            foreach (string t in Name_Column)
            {
                DGV_Viw1.Columns[t].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns[t].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns[t].OptionsColumn.AllowSize = true;
                DGV_Viw1.Columns[t].OptionsColumn.AllowEdit = Allow_Edit;
                DGV_Viw1.Columns[t].OptionsColumn.AllowSort = Allow_Sort ? DefaultBoolean.True : DefaultBoolean.False;
            }

            #region (افزودن چکباکس)

            var CheckBox = new RepositoryItemCheckEdit();
            CheckBox.ReadOnly = false;

            #endregion (افزودن چکباکس)

            #region (افزودن باتن انتخاب)

            var Select = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Select.Buttons[0].Kind = ButtonPredefines.Glyph;
            Select.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Select.Buttons[0].Image = Properties.Resources.apply_32x32;

            #endregion (افزودن باتن انتخاب)

            #region (افزودن باتن حذف)

            var Delete = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Delete.Buttons[0].Kind = ButtonPredefines.Glyph;
            Delete.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Delete.Buttons[0].Image = Properties.Resources.delete_32x32;


            #endregion (افزودن باتن حذف)

            #region (افزودن باتن ویرایش)

            var Edit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Edit.Buttons[0].Kind = ButtonPredefines.Glyph;
            Edit.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Edit.Buttons[0].Image = Properties.Resources.edit_32x32;


            #endregion (افزودن باتن ویرایش)


            #region (ویرایش چکباکس)

            if (Chk)
            {
                DGV_Date.RepositoryItems.Add(CheckBox);
                DGV_Viw1.Columns[" "].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns[" "].ColumnEdit = CheckBox;
                DGV_Viw1.Columns[" "].OptionsColumn.ReadOnly = false;
                DGV_Viw1.Columns[" "].AppearanceHeader.Name = "";
                DGV_Viw1.Columns[" "].Caption = "";
                DGV_Viw1.Columns[" "].MaxWidth = 30;
                DGV_Viw1.Columns[" "].MinWidth = 30;
                DGV_Viw1.Columns[" "].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns[" "].OptionsFilter.AllowAutoFilter = false;
            }

            #endregion (ویرایش چکباکس)

            #region (ویرایش انتخاب)

            if (Sel)
            {
                DGV_Date.RepositoryItems.Add(Select);
                DGV_Viw1.Columns["انتخاب"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["انتخاب"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["انتخاب"].ColumnEdit = Select;
                DGV_Viw1.Columns["انتخاب"].MaxWidth = 62;
                DGV_Viw1.Columns["انتخاب"].MinWidth = 62;
                DGV_Viw1.Columns["انتخاب"].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns["انتخاب"].OptionsFilter.AllowAutoFilter = false;

            }

            #endregion (ویرایش انتخاب)

            #region (ویرایش حذف)

            if (Del)
            {
                DGV_Date.RepositoryItems.Add(Delete);
                DGV_Viw1.Columns["حذف"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["حذف"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["حذف"].ColumnEdit = Delete;
                DGV_Viw1.Columns["حذف"].MaxWidth = 57;
                DGV_Viw1.Columns["حذف"].MinWidth = 57;
                DGV_Viw1.Columns["حذف"].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns["حذف"].OptionsFilter.AllowAutoFilter = false;
            }

            #endregion (ویرایش حذف)

            #region (ویرایش ، ویرایش)

            if (Edi)
            {
                DGV_Date.RepositoryItems.Add(Edit);
                DGV_Viw1.Columns["ویرایش"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["ویرایش"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["ویرایش"].ColumnEdit = Edit;
                DGV_Viw1.Columns["ویرایش"].MaxWidth = 67;
                DGV_Viw1.Columns["ویرایش"].MinWidth = 67;
                DGV_Viw1.Columns["ویرایش"].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns["ویرایش"].OptionsFilter.AllowAutoFilter = false;

            }

            #endregion (ویرایش ، ویرایش)

            //FindPanelGrid(DGV_Viw1, DGV_Date);
            try
            {
                // _font.ChangeFont(DGV_Viw);
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Update: 2019/04/13
        /// </summary>
        /// <param name="DT_Factor"></param>
        /// <param name="DGV_Date"></param>
        /// <param name="DGV_Viw1"></param>
        /// <param name="Name_Column"></param>
        /// <param name="Allow_Edit"></param>
        /// <param name="Allow_Sort"></param>
        /// <param name="AutoFitColumn"></param>
        /// <param name="colorEvenOld"></param>
        /// <param name="ItemCombo"></param>
        public void Grid_Structure_SetType(DataTable DT_Factor, GridControl DGV_Date, GridView DGV_Viw1, List<string> Name_Column, bool Allow_Edit = false,
            bool Allow_Sort = true, bool AutoFitColumn = true, Color colorEvenOld = default(Color), List<string> ItemCombo = null)
        {
            DGV_Viw.OptionsView.ShowBands = false;
            List<string> _NameColFormatN0 = new List<string>();
            List<string> _NameColFormatN1 = new List<string>();
            List<string> _NameColFormatN2 = new List<string>();
            List<string> _NameColFormatN3 = new List<string>();

            BestFit = AutoFitColumn;
            DT_Factor.Clear();
            DT_Factor.Columns.Clear();

            _color = colorEvenOld;
            //  rowEvenOld = RowEvenOld;

            #region(افزودن افزونه های عملیاتی)

            if (Chk)
                DT_Factor.Columns.Add(" ", typeof(bool));

            if (Sel)
                DT_Factor.Columns.Add("انتخاب");

            if (Del)
                DT_Factor.Columns.Add("حذف");

            if (Edi)
                DT_Factor.Columns.Add("ویرایش");

            #endregion

            for (var i = 0; i < Name_Column.Count; i++)
            {
                if (Name_Column[i] == "تصویر" || Name_Column[i] == "..." || Name_Column[i] == "ضمیمه")
                    DT_Factor.Columns.Add(Name_Column[i], typeof(Image));
                else
                {
                    if (Name_Column[i].Contains("="))
                    {
                        var IndexOFF = Name_Column[i].IndexOf("=", StringComparison.Ordinal);
                        var ggg = Name_Column[i].Substring(0, IndexOFF).ToLower();

                        Type _type = null;
                        if (ggg.Contains("image"))
                        {
                            Name_Column[i] = Name_Column[i].Replace("image=", "");
                            DT_Factor.Columns.Add(Name_Column[i], typeof(Image));
                        }
                        else if (ggg.Contains("int32") || ggg.Contains("int64") || ggg.Contains("double"))
                        {
                            if (ggg.Contains("int32"))
                            {
                                _type = Type.GetType("System.Int32");
                                Name_Column[i] = Name_Column[i].Replace("int32=", "");
                                if (ggg.ToLower().Contains("n0"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N0", "");
                                    _NameColFormatN0.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n1"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N1", "");
                                    _NameColFormatN1.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n2"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N2", "");
                                    _NameColFormatN2.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n3"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N3", "");
                                    _NameColFormatN3.Add(Name_Column[i]);
                                }

                                DT_Factor.Columns.Add(Name_Column[i], _type);
                            }
                            else if (ggg.Contains("int64"))
                            {
                                _type = Type.GetType("System.Int64");
                                Name_Column[i] = Name_Column[i].Replace("int64=", "");
                                if (ggg.ToLower().Contains("n0"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N0", "");
                                    _NameColFormatN0.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n1"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N1", "");
                                    _NameColFormatN1.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n2"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N2", "");
                                    _NameColFormatN2.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n3"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N3", "");
                                    _NameColFormatN3.Add(Name_Column[i]);
                                }

                                DT_Factor.Columns.Add(Name_Column[i], _type);
                            }
                            else if (ggg.Contains("double"))
                            {
                                _type = Type.GetType("System.Double");
                                Name_Column[i] = Name_Column[i].Replace("double=", "");
                                if (ggg.ToLower().Contains("n0"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N0", "");
                                    _NameColFormatN0.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n1"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N1", "");
                                    _NameColFormatN1.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n2"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N2", "");
                                    _NameColFormatN2.Add(Name_Column[i]);
                                }
                                else if (ggg.ToLower().Contains("n3"))
                                {
                                    Name_Column[i] = Name_Column[i].Replace("N3", "");
                                    _NameColFormatN3.Add(Name_Column[i]);
                                }

                                DT_Factor.Columns.Add(Name_Column[i], _type);
                            }
                        }

                        else
                            switch (ggg.ToLower())
                            {
                                case "float":
                                    _type = Type.GetType("System.Single");
                                    Name_Column[i] = Name_Column[i].Replace("float=", "");
                                    DT_Factor.Columns.Add(Name_Column[i], _type);
                                    break;

                                case "double":
                                    _type = Type.GetType("System.Double");
                                    Name_Column[i] = Name_Column[i].Replace("double=", "");
                                    DT_Factor.Columns.Add(Name_Column[i], _type);
                                    break;

                                case "string":
                                    _type = Type.GetType("System.String");
                                    Name_Column[i] = Name_Column[i].Replace("string=", "");
                                    DT_Factor.Columns.Add(Name_Column[i], _type);
                                    break;

                                case "combobox":
                                    DT_Factor.Columns.Add(Name_Column[i]);
                                    break;

                                case "bool":
                                    DT_Factor.Columns.Add(Name_Column[i]);
                                    Name_Column[i] = Name_Column[i].Replace("bool=", "");
                                    break;

                                case "progress":
                                    DT_Factor.Columns.Add(Name_Column[i].Replace("progress=", ""));
                                    break;

                                case "memo":
                                    DT_Factor.Columns.Add(Name_Column[i].Replace("memo=", ""));
                                    break;

                                case "negnum32": // نمایش اعداد منفی

                                    //  Name_Column[i] = Name_Column[i].ToLower().Replace("negnum32=", "");
                                    DT_Factor.Columns.Add(Name_Column[i].ToLower().Replace("negnum32=", ""));
                                    break;

                                case "image":
                                    break;

                                default:
                                    DT_Factor.Columns.Add(Name_Column[i], _type);
                                    break;
                            }

                        // var clm = Name_Column[i].Substring(IndexOFF + 1, Name_Column[i].Length - IndexOFF - 1);
                    }
                    else
                        DT_Factor.Columns.Add(Name_Column[i]);

                }

                // MessageBox.Show(Name_Column[i]);
            }

            DGV_Date.DataSource = DT_Factor;

            for (var i = 0; i < Name_Column.Count; i++)
            {
                var NC = Name_Column[i];
                if (_NameColFormatN0.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N0";
                }
                else if (_NameColFormatN1.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N1";
                }
                else if (_NameColFormatN2.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N2";
                }
                else if (_NameColFormatN3.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N3";
                }

                if (NC.ToLower().Contains("combobox"))
                {
                    Name_Column[i] = NC.Replace("combobox=", "");
                    var clm = Name_Column[i];
                    var ComboBox = new RepositoryItemComboBox
                    {
                        Name = "combobox=" + clm,
                        TextEditStyle = TextEditStyles.DisableTextEditor,
                        AppearanceDropDown = { Font = _font.ChangeFont() }
                    };
                    if (ItemCombo != null)
                        ComboBox.Items.AddRange(ItemCombo);

                    DGV_Date.RepositoryItems.Add(ComboBox);
                    DGV_Viw1.Columns["combobox=" + clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw1.Columns["combobox=" + clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw1.Columns["combobox=" + clm].ColumnEdit = ComboBox;
                    DGV_Viw1.Columns["combobox=" + clm].ColumnEdit = ComboBox;
                    DGV_Viw1.Columns["combobox=" + clm].AppearanceHeader.Name = clm;
                    DGV_Viw1.Columns["combobox=" + clm].Caption = clm;
                }
                else if (NC.ToLower().Contains("progress")) // جدید پیشرفت کار
                {
                    Name_Column[i] = NC.Replace("progress=", "");
                    var clm = Name_Column[i];
                    var ComboBox = new RepositoryItemProgressBar { Name = clm };

                    _font.ChangeFont(ComboBox, 14);

                    ComboBox.ShowTitle = true;
                    ComboBox.LookAndFeel.SkinName = "Metropolis Dark";
                    ComboBox.LookAndFeel.UseDefaultLookAndFeel = false;

                    DGV_Date.RepositoryItems.Add(ComboBox);
                    DGV_Viw1.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw1.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    DGV_Viw1.Columns[clm].ColumnEdit = ComboBox;
                    DGV_Viw1.Columns[clm].ColumnEdit = ComboBox;
                    DGV_Viw1.Columns[clm].AppearanceHeader.Name = clm;
                    DGV_Viw1.Columns[clm].Caption = clm;
                    DGV_Viw1.Columns[clm].MaxWidth = 100;
                    DGV_Viw1.Columns[clm].MinWidth = 60;
                }
                else if (NC.ToLower().Contains("memo")) // جدید چند خطی
                {
                    Name_Column[i] = NC.Replace("memo=", "");
                    var clm = Name_Column[i];
                    var MemoEdit = new RepositoryItemMemoEdit { Name = clm, };

                    _font.ChangeFont(MemoEdit, 14);

                    DGV_Date.RepositoryItems.Add(MemoEdit);
                    DGV_Viw1.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                    DGV_Viw1.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                    DGV_Viw1.Columns[clm].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

                    DGV_Viw1.Columns[clm].ColumnEdit = MemoEdit;
                    DGV_Viw1.Columns[clm].ColumnEdit = MemoEdit;
                    DGV_Viw1.Columns[clm].AppearanceHeader.Name = clm;
                    DGV_Viw1.Columns[clm].Caption = clm;
                    DGV_Viw1.Columns[clm].MaxWidth = 550;
                    DGV_Viw1.Columns[clm].MinWidth = 150;
                }
                else if (NC.ToLower().Contains("negnum32")) // اعداد منفی
                {
                    // MessageBox.Show(NC);
                    Name_Column[i] = NC.ToLower().Replace("negnum32=", "");
                    var clm = Name_Column[i];
                    var MemoEdit = new RepositoryItemTextEdit { Name = clm };

                    //var MemoEdit2 = new RepositoryItemLookUpEdit
                    //{

                    //};

                    _font.ChangeFont(MemoEdit, 14);

                    DGV_Date.RepositoryItems.Add(MemoEdit);

                    //DGV_Viw.Columns[clm].DisplayFormat.FormatType = FormatType.Numeric;
                    //DGV_Viw.Columns[clm].DisplayFormat.FormatString = "N0";

                    DGV_Viw1.Columns[clm].AppearanceCell.TextOptions.RightToLeft = true;
                    DGV_Viw1.Columns[clm].AppearanceCell.BackColor = Color.DarkOrange;
                    DGV_Viw1.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw1.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    DGV_Viw1.Columns[clm].ColumnEdit = MemoEdit;
                    DGV_Viw1.Columns[clm].AppearanceHeader.Name = clm;
                    DGV_Viw1.Columns[clm].Caption = clm;
                    //DGV_Viw1.Columns[clm].MaxWidth = 150;
                    //DGV_Viw1.Columns[clm].MinWidth = 120;
                }
                else
                {
                    try
                    {
                        DGV_Viw1.Columns[NC].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw1.Columns[NC].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw1.Columns[NC].OptionsColumn.AllowSize = true;
                        DGV_Viw1.Columns[NC].OptionsColumn.AllowEdit = Allow_Edit;
                        DGV_Viw1.OptionsBehavior.Editable = Allow_Edit;
                        DGV_Viw1.Columns[NC].OptionsColumn.AllowSort = Allow_Sort ? DefaultBoolean.True : DefaultBoolean.False;
                    }
                    catch
                    {
                        // ignored
                    }
                }

                _font.ChangeFont(chkFind);
            }

            #region (افزودن چکباکس)

            var CheckBox = new RepositoryItemCheckEdit { ReadOnly = false };

            #endregion (افزودن چکباکس)

            #region (افزودن باتن انتخاب)

            var Select = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Select.Buttons[0].Kind = ButtonPredefines.Glyph;
            Select.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Select.Buttons[0].Image = Properties.Resources.dgvSel2;
            //   Select.Buttons[0].Image = Properties.Resources.apply_32x32;

            #endregion (افزودن باتن انتخاب)

            #region (افزودن باتن حذف)

            var Delete = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Delete.Buttons[0].Kind = ButtonPredefines.Glyph;
            Delete.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            // Delete.Buttons[0].Image = Properties.Resources.delete_32x32;
            Delete.Buttons[0].Image = Properties.Resources.dgvDel2;


            #endregion (افزودن باتن حذف)

            #region (افزودن باتن ویرایش)

            var Edit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Edit.Buttons[0].Kind = ButtonPredefines.Glyph;
            Edit.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Edit.Buttons[0].Image = Properties.Resources.dgvEdit;

            #endregion (افزودن باتن ویرایش)



            #region (ویرایش چکباکس)

            if (Chk)
            {
                DGV_Date.RepositoryItems.Add(CheckBox);
                DGV_Viw1.Columns[" "].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns[" "].ColumnEdit = CheckBox;
                DGV_Viw1.Columns[" "].OptionsColumn.ReadOnly = false;
                DGV_Viw1.Columns[" "].AppearanceHeader.Name = "";
                DGV_Viw1.Columns[" "].Caption = "";
                DGV_Viw1.Columns[" "].MaxWidth = 30;
                DGV_Viw1.Columns[" "].MinWidth = 30;
                DGV_Viw1.Columns[" "].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns[" "].OptionsFilter.AllowAutoFilter = false;
            }

            #endregion (ویرایش چکباکس)

            #region (ویرایش انتخاب)

            if (Sel)
            {
                DGV_Date.RepositoryItems.Add(Select);
                DGV_Viw1.Columns["انتخاب"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["انتخاب"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["انتخاب"].ColumnEdit = Select;
                DGV_Viw1.Columns["انتخاب"].MaxWidth = 40;
                DGV_Viw1.Columns["انتخاب"].MinWidth = 40;
                DGV_Viw1.Columns["انتخاب"].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns["انتخاب"].OptionsFilter.AllowAutoFilter = false;

            }

            #endregion (ویرایش انتخاب)

            #region (ویرایش حذف)

            if (Del)
            {
                try
                {
                    DGV_Date.RepositoryItems.Add(Delete);
                    DGV_Viw1.Columns["حذف"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw1.Columns["حذف"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw1.Columns["حذف"].ColumnEdit = Delete;
                    DGV_Viw1.Columns["حذف"].MaxWidth = 40;
                    DGV_Viw1.Columns["حذف"].MinWidth = 40;
                    DGV_Viw1.Columns["حذف"].OptionsFilter.AllowFilter = false;
                    DGV_Viw1.Columns["حذف"].OptionsFilter.AllowAutoFilter = false;
                }
                catch
                {

                }
            }

            #endregion (ویرایش حذف)

            #region (ویرایش ، ویرایش)

            if (Edi)
            {
                DGV_Date.RepositoryItems.Add(Edit);
                DGV_Viw1.Columns["ویرایش"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["ویرایش"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw1.Columns["ویرایش"].ColumnEdit = Edit;
                DGV_Viw1.Columns["ویرایش"].MaxWidth = 47;
                DGV_Viw1.Columns["ویرایش"].MinWidth = 47;
                DGV_Viw1.Columns["ویرایش"].OptionsFilter.AllowFilter = false;
                DGV_Viw1.Columns["ویرایش"].OptionsFilter.AllowAutoFilter = false;

            }

            #endregion (ویرایش ، ویرایش)

        }
        public void Grid_Structure_Relation(Data_Grid _grid, DataTable _dt1, DataTable _dt2, List<string> Name_Columndt1, List<string> Name_Columndt2,
            bool Allow_Edit = false, bool Allow_Sort = true, bool AutoFitColumn = true, List<string> ItemCombo = null)
        {
            DGV_Viw.OptionsView.ShowBands = false;
            List<string> _NameCol = new List<string>();
            BestFit = AutoFitColumn;

            _dt2.Clear();
            _dt2.Columns.Clear();

            _dt1.Clear();
            _dt1.Columns.Clear();

            #region(افزودن افزونه های عملیاتی)

            #region جدول  زیر گروه

            _dt2.Columns.Add(" ", typeof(bool));
            _dt2.Columns.Add("انتخاب");
            _dt2.Columns.Add("حذف");
            _dt2.Columns.Add("ویرایش");

            #endregion


            if (Chk)
                _dt1.Columns.Add(" ", typeof(bool));

            if (Sel)
                _dt1.Columns.Add("انتخاب");

            if (Del)
                _dt1.Columns.Add("حذف");

            if (Edi)
                _dt1.Columns.Add("ویرایش");

            #endregion

            for (var i = 0; i < Name_Columndt1.Count; i++)
            {
                if (Name_Columndt1[i] == "تصویر")
                    _dt1.Columns.Add("تصویر", typeof(Image));
                else
                {
                    if (Name_Columndt1[i].Contains("="))
                    {
                        var IndexOFF = Name_Columndt1[i].IndexOf("=", StringComparison.Ordinal);
                        var clmColumn = Name_Columndt1[i].Substring(0, IndexOFF).ToLower();

                        Type _type = null;
                        if (clmColumn.Contains("int32") || clmColumn.Contains("double"))
                        {
                            if (clmColumn.Contains("int32"))
                            {
                                _type = Type.GetType("System.Int32");
                                Name_Columndt1[i] = Name_Columndt1[i].Replace("int32=", "");
                                if (clmColumn.ToLower().Contains("n0"))
                                {
                                    Name_Columndt1[i] = Name_Columndt1[i].Replace("N0", "");
                                    _NameCol.Add(Name_Columndt1[i]);
                                }

                                _dt1.Columns.Add(Name_Columndt1[i], _type);
                            }
                            else if (clmColumn.Contains("double"))
                            {
                                _type = Type.GetType("System.Double");
                                Name_Columndt1[i] = Name_Columndt1[i].Replace("double=", "");
                                if (clmColumn.ToLower().Contains("n0"))
                                {
                                    Name_Columndt1[i] = Name_Columndt1[i].Replace("N0", "");
                                    _NameCol.Add(Name_Columndt1[i]);
                                }

                                _dt1.Columns.Add(Name_Columndt1[i], _type);
                            }

                        }

                        else
                            switch (clmColumn)
                            {
                                case "float":
                                    _type = Type.GetType("System.Single");
                                    Name_Columndt1[i] = Name_Columndt1[i].Replace("float=", "");
                                    _dt1.Columns.Add(Name_Columndt1[i], _type);
                                    break;
                                case "double":
                                    _type = Type.GetType("System.Double");
                                    Name_Columndt1[i] = Name_Columndt1[i].Replace("double=", "");
                                    _dt1.Columns.Add(Name_Columndt1[i], _type);
                                    break;
                                case "string":
                                    _type = Type.GetType("System.string");
                                    Name_Columndt1[i] = Name_Columndt1[i].Replace("string=", "");
                                    _dt1.Columns.Add(Name_Columndt1[i], _type);
                                    break;
                                case "combobox":
                                    _dt1.Columns.Add(Name_Columndt1[i]);
                                    break;

                                default:
                                    _dt1.Columns.Add(Name_Columndt1[i], _type);
                                    break;
                            }

                        // var clm = Name_Column[i].Substring(IndexOFF + 1, Name_Column[i].Length - IndexOFF - 1);
                    }
                    else

                        _dt1.Columns.Add(Name_Columndt1[i]);
                }
            }

            _grid.DGV.DataSource = _dt1;

            #region جدول زیر گروه

            for (var i = 0; i < Name_Columndt2.Count; i++)
            {
                if (Name_Columndt2[i] == "تصویر")
                    _dt2.Columns.Add("تصویر", typeof(Image));
                else
                {
                    if (Name_Columndt2[i].Contains("="))
                    {
                        var IndexOFF = Name_Columndt2[i].IndexOf("=", StringComparison.Ordinal);
                        var clmColumn = Name_Columndt2[i].Substring(0, IndexOFF).ToLower();

                        Type _type = null;
                        if (clmColumn.Contains("int32") || clmColumn.Contains("double"))
                        {
                            if (clmColumn.Contains("int32"))
                            {
                                _type = Type.GetType("System.Int32");
                                Name_Columndt2[i] = Name_Columndt2[i].Replace("int32=", "");
                                if (clmColumn.ToLower().Contains("n0"))
                                {
                                    Name_Columndt2[i] = Name_Columndt2[i].Replace("N0", "");
                                    _NameCol.Add(Name_Columndt2[i]);
                                }

                                _dt2.Columns.Add(Name_Columndt2[i], _type);
                            }
                            else if (clmColumn.Contains("double"))
                            {
                                _type = Type.GetType("System.Double");
                                Name_Columndt2[i] = Name_Columndt2[i].Replace("double=", "");
                                if (clmColumn.ToLower().Contains("n0"))
                                {
                                    Name_Columndt2[i] = Name_Columndt2[i].Replace("N0", "");
                                    _NameCol.Add(Name_Columndt2[i]);
                                }

                                _dt2.Columns.Add(Name_Columndt2[i], _type);
                            }

                        }

                        else
                            switch (clmColumn)
                            {
                                case "float":
                                    _type = Type.GetType("System.Single");
                                    Name_Columndt2[i] = Name_Columndt2[i].Replace("float=", "");
                                    _dt2.Columns.Add(Name_Columndt2[i], _type);
                                    break;
                                case "double":
                                    _type = Type.GetType("System.Double");
                                    Name_Columndt2[i] = Name_Columndt2[i].Replace("double=", "");
                                    _dt2.Columns.Add(Name_Columndt2[i], _type);
                                    break;
                                case "string":
                                    _type = Type.GetType("System.string");
                                    Name_Columndt2[i] = Name_Columndt2[i].Replace("string=", "");
                                    _dt2.Columns.Add(Name_Columndt2[i], _type);
                                    break;
                                case "combobox":
                                    _dt2.Columns.Add(Name_Columndt2[i]);
                                    break;
                                default:
                                    _dt2.Columns.Add(Name_Columndt2[i], _type);
                                    break;
                            }

                        // var clm = Name_Column[i].Substring(IndexOFF + 1, Name_Column[i].Length - IndexOFF - 1);
                    }
                    else

                        _dt2.Columns.Add(Name_Columndt2[i]);
                }
            }

            #endregion

            for (var i = 0; i < Name_Columndt1.Count; i++)
            {
                var NC = Name_Columndt1[i];
                if (_NameCol.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Columndt1[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    DGV_Viw.Columns[Name_Columndt1[i]].DisplayFormat.FormatString = "N0";
                }

                if (NC.ToLower().Contains("combobox"))
                {
                    Name_Columndt1[i] = NC.Replace("combobox=", "");
                    var clm = Name_Columndt1[i];
                    var ComboBox = new RepositoryItemComboBox
                    {
                        Name = "combobox=" + clm,
                        TextEditStyle = TextEditStyles.DisableTextEditor,
                        AppearanceDropDown = { Font = new Font("B Yekan", 14) },
                    };
                    if (ItemCombo != null)
                        ComboBox.Items.AddRange(ItemCombo);
                    _grid.DGV.RepositoryItems.Add(ComboBox);
                    _grid.DGV_Viw.Columns["combobox=" + clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _grid.DGV_Viw.Columns["combobox=" + clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    _grid.DGV_Viw.Columns["combobox=" + clm].ColumnEdit = ComboBox;
                    _grid.DGV_Viw.Columns["combobox=" + clm].ColumnEdit = ComboBox;
                    _grid.DGV_Viw.Columns["combobox=" + clm].AppearanceHeader.Name = clm;
                    _grid.DGV_Viw.Columns["combobox=" + clm].Caption = clm;

                    // _grid.DGV_Viw.Columns["combobox"].MaxWidth = 30;
                    // _grid.DGV_Viw.Columns["combobox"].MinWidth = 30;
                    // _grid.DGV_Viw.Columns["combobox"].OptionsFilter.AllowFilter = false;
                    // _grid.DGV_Viw.Columns["combobox"].OptionsFilter.AllowAutoFilter = false;
                }

                else
                {
                    try
                    {
                        _grid.DGV_Viw.Columns[NC].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        _grid.DGV_Viw.Columns[NC].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        _grid.DGV_Viw.Columns[NC].OptionsColumn.AllowSize = true;
                        _grid.DGV_Viw.Columns[NC].OptionsColumn.AllowEdit = Allow_Edit;
                        _grid.DGV_Viw.Columns[NC].OptionsColumn.AllowSort = Allow_Sort ? DefaultBoolean.True : DefaultBoolean.False;
                    }
                    catch
                    {
                        // ignored
                    }
                }

            }

            #region (افزودن چکباکس)

            var CheckBox = new RepositoryItemCheckEdit { ReadOnly = false };

            #endregion (افزودن چکباکس)

            #region (افزودن باتن انتخاب)

            var Select = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Select.Buttons[0].Kind = ButtonPredefines.Glyph;
            Select.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Select.Buttons[0].Image = Properties.Resources.apply_32x32;

            #endregion (افزودن باتن انتخاب)

            #region (افزودن باتن حذف)

            var Delete = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Delete.Buttons[0].Kind = ButtonPredefines.Glyph;
            Delete.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Delete.Buttons[0].Image = Properties.Resources.delete_32x32;


            #endregion (افزودن باتن حذف)

            #region (افزودن باتن ویرایش)

            var Edit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            Edit.Buttons[0].Kind = ButtonPredefines.Glyph;
            Edit.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            Edit.Buttons[0].Image = Properties.Resources.edit_32x32;

            #endregion (افزودن باتن ویرایش)



            #region (ویرایش چکباکس)

            if (Chk)
            {
                _grid.DGV.RepositoryItems.Add(CheckBox);
                _grid.DGV_Viw.Columns[" "].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns[" "].ColumnEdit = CheckBox;
                _grid.DGV_Viw.Columns[" "].OptionsColumn.ReadOnly = false;
                _grid.DGV_Viw.Columns[" "].AppearanceHeader.Name = "";
                _grid.DGV_Viw.Columns[" "].Caption = "";
                _grid.DGV_Viw.Columns[" "].MaxWidth = 30;
                _grid.DGV_Viw.Columns[" "].MinWidth = 30;
                _grid.DGV_Viw.Columns[" "].OptionsFilter.AllowFilter = false;
                _grid.DGV_Viw.Columns[" "].OptionsFilter.AllowAutoFilter = false;
            }

            #endregion (ویرایش چکباکس)

            #region (ویرایش انتخاب)

            if (Sel)
            {
                _grid.DGV.RepositoryItems.Add(Select);
                _grid.DGV_Viw.Columns["انتخاب"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns["انتخاب"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns["انتخاب"].ColumnEdit = Select;
                _grid.DGV_Viw.Columns["انتخاب"].MaxWidth = 62;
                _grid.DGV_Viw.Columns["انتخاب"].MinWidth = 62;
                _grid.DGV_Viw.Columns["انتخاب"].OptionsFilter.AllowFilter = false;
                _grid.DGV_Viw.Columns["انتخاب"].OptionsFilter.AllowAutoFilter = false;

            }

            #endregion (ویرایش انتخاب)

            #region (ویرایش حذف)

            if (Del)
            {
                _grid.DGV.RepositoryItems.Add(Delete);
                _grid.DGV_Viw.Columns["حذف"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns["حذف"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns["حذف"].ColumnEdit = Delete;
                _grid.DGV_Viw.Columns["حذف"].MaxWidth = 57;
                _grid.DGV_Viw.Columns["حذف"].MinWidth = 57;
                _grid.DGV_Viw.Columns["حذف"].OptionsFilter.AllowFilter = false;
                _grid.DGV_Viw.Columns["حذف"].OptionsFilter.AllowAutoFilter = false;
            }

            #endregion (ویرایش حذف)

            #region (ویرایش ، ویرایش)

            if (Edi)
            {
                _grid.DGV.RepositoryItems.Add(Edit);
                _grid.DGV_Viw.Columns["ویرایش"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns["ویرایش"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                _grid.DGV_Viw.Columns["ویرایش"].ColumnEdit = Edit;
                _grid.DGV_Viw.Columns["ویرایش"].MaxWidth = 67;
                _grid.DGV_Viw.Columns["ویرایش"].MinWidth = 67;
                _grid.DGV_Viw.Columns["ویرایش"].OptionsFilter.AllowFilter = false;
                _grid.DGV_Viw.Columns["ویرایش"].OptionsFilter.AllowAutoFilter = false;

            }

            #endregion (ویرایش ، ویرایش)



            #region جدول زیر گروه

            //#region (افزودن چکباکس)

            //var CheckBox2 = new RepositoryItemCheckEdit { ReadOnly = false };

            //#endregion (افزودن چکباکس)

            //#region (افزودن باتن انتخاب)

            //var Select2 = new RepositoryItemButtonEdit
            //{
            //    TextEditStyle = TextEditStyles.HideTextEditor
            //};
            //Select2.Buttons[0].Kind = ButtonPredefines.Glyph;
            //Select2.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            //Select2.Buttons[0].Image = Properties.Resources.apply_32x32;

            //#endregion (افزودن باتن انتخاب)

            //#region (افزودن باتن حذف)

            //var Delete2 = new RepositoryItemButtonEdit
            //{
            //    TextEditStyle = TextEditStyles.HideTextEditor
            //};
            //Delete2.Buttons[0].Kind = ButtonPredefines.Glyph;
            //Delete2.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            //Delete2.Buttons[0].Image = Properties.Resources.delete_32x32;


            //#endregion (افزودن باتن حذف)

            //#region (افزودن باتن ویرایش)

            //var Edit2 = new RepositoryItemButtonEdit
            //{
            //    TextEditStyle = TextEditStyles.HideTextEditor
            //};
            //Edit2.Buttons[0].Kind = ButtonPredefines.Glyph;
            //Edit2.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
            //Edit2.Buttons[0].Image = Properties.Resources.edit_32x32;

            //#endregion (افزودن باتن ویرایش)



            //#region (ویرایش چکباکس)


            //_grid.DGV.RepositoryItems.Add(CheckBox2);
            ////_grid.DGV_Viw.Columns[" "].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns[" "].ColumnEdit = CheckBox;
            ////_grid.DGV_Viw.Columns[" "].OptionsColumn.ReadOnly = false;
            ////_grid.DGV_Viw.Columns[" "].AppearanceHeader.Name = "";
            ////_grid.DGV_Viw.Columns[" "].Caption = "";
            ////_grid.DGV_Viw.Columns[" "].MaxWidth = 30;
            ////_grid.DGV_Viw.Columns[" "].MinWidth = 30;
            ////_grid.DGV_Viw.Columns[" "].OptionsFilter.AllowFilter = false;
            ////_grid.DGV_Viw.Columns[" "].OptionsFilter.AllowAutoFilter = false;


            //#endregion (ویرایش چکباکس)

            //#region (ویرایش انتخاب)


            //_grid.DGV.RepositoryItems.Add(Select2);
            ////_grid.DGV_Viw.Columns["انتخاب"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns["انتخاب"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns["انتخاب"].ColumnEdit = Select;
            ////_grid.DGV_Viw.Columns["انتخاب"].MaxWidth = 62;
            ////_grid.DGV_Viw.Columns["انتخاب"].MinWidth = 62;
            ////_grid.DGV_Viw.Columns["انتخاب"].OptionsFilter.AllowFilter = false;
            ////_grid.DGV_Viw.Columns["انتخاب"].OptionsFilter.AllowAutoFilter = false;



            //#endregion (ویرایش انتخاب)

            //#region (ویرایش حذف)


            //_grid.DGV.RepositoryItems.Add(Delete2);
            ////_grid.DGV_Viw.Columns["حذف"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns["حذف"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns["حذف"].ColumnEdit = Delete;
            ////_grid.DGV_Viw.Columns["حذف"].MaxWidth = 57;
            ////_grid.DGV_Viw.Columns["حذف"].MinWidth = 57;
            ////_grid.DGV_Viw.Columns["حذف"].OptionsFilter.AllowFilter = false;
            ////_grid.DGV_Viw.Columns["حذف"].OptionsFilter.AllowAutoFilter = false;


            //#endregion (ویرایش حذف)

            //#region (ویرایش ، ویرایش)

            //_grid.DGV.RepositoryItems.Add(Edit2);
            ////_grid.DGV_Viw.Columns["ویرایش"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns["ویرایش"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ////_grid.DGV_Viw.Columns["ویرایش"].ColumnEdit = Edit;
            ////_grid.DGV_Viw.Columns["ویرایش"].MaxWidth = 67;
            ////_grid.DGV_Viw.Columns["ویرایش"].MinWidth = 67;
            ////_grid.DGV_Viw.Columns["ویرایش"].OptionsFilter.AllowFilter = false;
            ////_grid.DGV_Viw.Columns["ویرایش"].OptionsFilter.AllowAutoFilter = false;



            //#endregion (ویرایش ، ویرایش)

            #endregion

            // DGV_Viw.OptionsView.ColumnAutoWidth = Show_Scroll;
            // FindPanelGrid(_grid.DGV_Viw, _grid.DGV);
            try
            {
                //  _font.ChangeFont(DGV_Viw);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void ChangeNameColumnAction(string sel = "انتخاب", string del = "حذف", string edit = "ویرایش")
        {
            try
            {
                string name = DGV_Viw.Columns["انتخاب"].Name;
                if (name != null && name.Contains("انتخاب"))
                    DGV_Viw.Columns["انتخاب"].Caption = sel;

                name = DGV_Viw.Columns["حذف"].Name;
                if (name != null && name.Contains("حذف"))
                    DGV_Viw.Columns["حذف"].Caption = del;

                name = DGV_Viw.Columns["ویرایش"].Name;
                if (name != null && name.Contains("ویرایش"))
                    DGV_Viw.Columns["ویرایش"].Caption = edit;
            }
            catch (Exception e)
            {

            }
        }

        public void Grid_One_Column(GridView Grid, int Column, int Size, string Title = null)
        {
            if (Title != null)
            {
                Grid.Columns[Column].Caption = Title;
            }

            Grid.Columns[Column].Width = Size;
            Grid.Columns[Column].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            Grid.Columns[Column].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            Grid.Columns[Column].OptionsColumn.AllowSize = false;
        }

        private void DGV_Enter(object sender, EventArgs e)
        {

        }

        #region Find Hoshmand

        private string GetStringWithoutQuotes(string findText)
        {
            if (chkFind.Checked)
            {
                string stringWithoutQuotes = findText.ToLower().Replace("\"", string.Empty);
                return stringWithoutQuotes;
            }

            return null;
        }

        private int FindSubStringStartPosition(string dispalyText, string findText)
        {
            if (chkFind.Checked)
            {
                string stringWithoutQuotes = GetStringWithoutQuotes(findText);
                int index = dispalyText.ToLower().IndexOf(stringWithoutQuotes);
                return index;
            }

            return 0;
        }

        private bool HiglightSubString(RowCellCustomDrawEventArgs e, string findText)
        {
            if (chkFind.Checked)
            {
                int index = FindSubStringStartPosition(e.DisplayText, findText);
                if (index == -1)
                {
                    return false;
                }

                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Cache.Paint.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, GetStringWithoutQuotes(findText), e.Appearance, Color.Indigo, Color.LightSlateGray,
                    true, index);
                return true;
            }

            return false;
        }

        private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (chkFind.Checked)
            {
                GridView view = sender as GridView;
                if (view.OptionsFind.HighlightFindResults && !view.FindFilterText.Equals(string.Empty))
                {
                    CriteriaOperator op = ConvertFindPanelTextToCriteriaOperator(view.FindFilterText, view, false);
                    if (op is GroupOperator)
                    {
                        string findText = view.FindFilterText;
                        if (HiglightSubString(e, findText))
                            e.Handled = true;
                    }
                    else if (op is FunctionOperator)
                    {
                        FunctionOperator func = op as FunctionOperator;
                        CriteriaOperator colNameOperator = func.Operands[0];
                        string colName = colNameOperator.LegacyToString().Replace("[", string.Empty).Replace("]", string.Empty);
                        if (!e.Column.FieldName.StartsWith(colName)) return;

                        CriteriaOperator valueOperator = func.Operands[1];
                        string findText = valueOperator.LegacyToString().ToLower().Replace("'", "");
                        if (HiglightSubString(e, findText))
                            e.Handled = true;
                    }
                }
            }
        }

        public static CriteriaOperator ConvertFindPanelTextToCriteriaOperator(string findPanelText, GridView view, bool applyPrefixes)
        {
            if (!string.IsNullOrEmpty(findPanelText))
            {
                FindSearchParserResults parseResult = new FindSearchParser().Parse(findPanelText, (IEnumerable<IDataColumnInfo>)GetFindToColumnsCollection(view));
                //  if (applyPrefixes)
                //  parseResult.AppendColumnFieldPrefixes();

                return DxFtsContainsHelperAlt.Create(parseResult, FilterCondition.Contains, false);
            }

            return null;
        }

        private static ICollection GetFindToColumnsCollection(GridView view)
        {
            System.Reflection.MethodInfo mi = typeof(ColumnView).GetMethod("GetFindToColumnsCollection",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return mi.Invoke(view, null) as ICollection;
        }

        #endregion

        private void Tim_DoubleClick_Tick(object sender, EventArgs e)
        {
            //clickOldRow = DC = 0;
            //Tim_DoubleClick.Enabled = false;
        }

        public class modelButtonImage
        {
            public string Name { get; set; }
            public Image Image { get; set; }
        }

        public void ExportToExcel(string sheetName, bool rowColor = true)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "فایل اکسل|*.Xlsx";
            sfd.RestoreDirectory = true;
            // _print = true;
            XlsxExportOptionsEx setExcel = new XlsxExportOptionsEx
            {
                ExportType = ExportType.DataAware,
                //  ExportType = ExportType.WYSIWYG,
                ExportMode = XlsxExportMode.SingleFile,
                SheetName = sheetName,
                TextExportMode = TextExportMode.Value,
                ShowGridLines = false,
                ExportHyperlinks = true,
                RawDataMode = false,
                ShowBandHeaders = DefaultBoolean.False,
                AllowFixedColumns = DefaultBoolean.True,
                ApplyFormattingToEntireColumn = DefaultBoolean.True,
                LayoutMode = LayoutMode.Table,
                AllowConditionalFormatting = DefaultBoolean.True,
                //BandedLayoutMode = BandedLayoutMode.LinearColumns
            };
            DGV_Viw.OptionsPrint.AutoWidth = false;
            DGV_Viw.OptionsPrint.UsePrintStyles = !rowColor;
            //  setExcel.RightToLeftDocument = DefaultBoolean.True;
            sfd.FileName = sheetName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {


                //GV_Viw.ExportToXlsx(xxx, new  {ExportType = ExportType.DataAware});

                DGV_Viw.ExportToXlsx(sfd.FileName, setExcel);
                Process.Start(sfd.FileName);
                //dgvRequest.DGV_Viw.ShowPrintPreview();
            }

            // _print = false;
        }

        // Bank_SQL bankSql = new Bank_SQL();
        /// <summary>
        /// Update: 2020/06/01
        /// </summary>
        /// <param name="DT_Factor">دیتا گرید</param>
        /// <param name="_data">دیتا تیبل</param>
        /// <param name="Name_Column">ستون ها</param>
        /// <param name="_imgButton">لیست باتن ها</param>
        /// <param name="Allow_Edit">اجازه ویرایش</param>
        /// <param name="Allow_Sort">اجازه مرتب سازی</param>
        /// <param name="AutoFitColumn">ری سایز ستون ها</param>
        /// <param name="colorEvenOld">رنگ ردیفف های زوج</param>
        /// <param name="ItemCombo">اطلاعات کمبو</param>
        /// <param name="showFooter">فوتر</param>
        public void GridStructure(Data_Grid _data, DataTable DT_Factor, List<string> Name_Column, List<modelButtonImage> _imgButton, bool Allow_Edit = false,
            bool Allow_Sort = true, bool AutoFitColumn = true, Color colorEvenOld = default(Color), List<string> ItemCombo = null, bool showFooter = true)
        {
//            Lic1();

            this.DGV_Viw.BeginUpdate();

            DGV_Viw.OptionsView.ShowBands = false;
            List<string> _NameColFormatN0 = new List<string>();
            List<string> _NameColFormatN1 = new List<string>();
            List<string> _NameColFormatN2 = new List<string>();
            List<string> _NameColFormatN3 = new List<string>();

            BestFit = AutoFitColumn;
            DT_Factor.Clear();
            DT_Factor.Columns.Clear();
            DGV.DataSource = null;
            DGV_Viw.Columns.Clear();
            _color = colorEvenOld;
            //  rowEvenOld = RowEvenOld;

            #region(افزودن افزونه های عملیاتی)

            if (Chk)
                DT_Factor.Columns.Add(" ", typeof(bool));

            if (_imgButton != null)
                foreach (var t in _imgButton)
                {
                    DT_Factor.Columns.Add(t.Name);
                }

            #endregion

            for (var i = 0; i < Name_Column.Count; i++)
            {
                if (Name_Column[i].Contains("="))
                {
                    var IndexOFF = Name_Column[i].IndexOf("=", StringComparison.Ordinal);
                    var ggg = Name_Column[i].Substring(0, IndexOFF).ToLower();

                    Type _type = null;
                    if (ggg.Contains("image"))
                    {
                        Name_Column[i] = Name_Column[i].Replace("image=", "");
                        DT_Factor.Columns.Add(Name_Column[i], typeof(Image));
                    }
                    else if (ggg.Contains("int32") || ggg.Contains("int64") || ggg.Contains("double"))
                    {
                        if (ggg.Contains("int32"))
                        {
                            _type = Type.GetType("System.Int32");
                            Name_Column[i] = Name_Column[i].Replace("int32=", "");
                            if (ggg.ToLower().Contains("n0"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N0", "");
                                _NameColFormatN0.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n1"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N1", "");
                                _NameColFormatN1.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n2"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N2", "");
                                _NameColFormatN2.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n3"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N3", "");
                                _NameColFormatN3.Add(Name_Column[i]);
                            }

                            DT_Factor.Columns.Add(Name_Column[i], _type);
                        }
                        else if (ggg.Contains("int64"))
                        {
                            _type = Type.GetType("System.Int64");
                            Name_Column[i] = Name_Column[i].Replace("int64=", "");
                            if (ggg.ToLower().Contains("n0"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N0", "");
                                _NameColFormatN0.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n1"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N1", "");
                                _NameColFormatN1.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n2"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N2", "");
                                _NameColFormatN2.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n3"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N3", "");
                                _NameColFormatN3.Add(Name_Column[i]);
                            }

                            DT_Factor.Columns.Add(Name_Column[i], _type);
                        }
                        else if (ggg.Contains("double"))
                        {
                            _type = Type.GetType("System.Double");
                            Name_Column[i] = Name_Column[i].Replace("double=", "");
                            if (ggg.ToLower().Contains("n0"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N0", "");
                                _NameColFormatN0.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n1"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N1", "");
                                _NameColFormatN1.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n2"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N2", "");
                                _NameColFormatN2.Add(Name_Column[i]);
                            }
                            else if (ggg.ToLower().Contains("n3"))
                            {
                                Name_Column[i] = Name_Column[i].Replace("N3", "");
                                _NameColFormatN3.Add(Name_Column[i]);
                            }

                            DT_Factor.Columns.Add(Name_Column[i], _type);
                        }
                    }

                    else
                        switch (ggg.ToLower())
                        {
                            case "float":
                                _type = Type.GetType("System.Single");
                                Name_Column[i] = Name_Column[i].Replace("float=", "");
                                DT_Factor.Columns.Add(Name_Column[i], _type);
                                break;

                            case "double":
                                _type = Type.GetType("System.Double");
                                Name_Column[i] = Name_Column[i].Replace("double=", "");
                                DT_Factor.Columns.Add(Name_Column[i], _type);
                                break;

                            case "string":
                                _type = Type.GetType("System.String");
                                Name_Column[i] = Name_Column[i].Replace("string=", "");
                                DT_Factor.Columns.Add(Name_Column[i], _type);
                                break;

                            case "combobox":
                                DT_Factor.Columns.Add(Name_Column[i]);
                                break;

                            case "bool":
                                DT_Factor.Columns.Add(Name_Column[i]);
                                Name_Column[i] = Name_Column[i].Replace("bool=", "");

                                break;

                            case "progress":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("progress=", ""));
                                break;


                            case "maskdate":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("maskdate=", ""));
                                break;

                            case "onoff":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("onoff=", ""));
                                break;

                            case "rating":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("rating=", ""));
                                break;

                            case "memo":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("memo=", ""));
                                break;

                            case "checked":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("checked=", ""), typeof(bool));
                                break;

                            case "volume":
                                DT_Factor.Columns.Add(Name_Column[i].Replace("volume=", ""));
                                break;

                            case "image":
                                break;

                            default:
                                DT_Factor.Columns.Add(Name_Column[i], _type);
                                break;
                        }
                }
                else
                    DT_Factor.Columns.Add(Name_Column[i]);
            }

            //  _data.DGV.DataSource = null;
            _data.DGV.DataSource = DT_Factor;

            for (var i = 0; i < Name_Column.Count; i++)
            {
                var NC = Name_Column[i];
                if (_NameColFormatN0.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    this.FormatStringNegativeNumber(Name_Column[i]);
                    // DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N0";
                }
                else if (_NameColFormatN1.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    this.FormatStringNegativeNumber(Name_Column[i]);
                    //  DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N1";
                }
                else if (_NameColFormatN2.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    this.FormatStringNegativeNumber(Name_Column[i]);
                    //  DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N2";
                }
                else if (_NameColFormatN3.Contains(NC))
                {
                    DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatType = FormatType.Numeric;
                    this.FormatStringNegativeNumber(Name_Column[i]);
                    //  DGV_Viw.Columns[Name_Column[i]].DisplayFormat.FormatString = "N3";
                }

                if (NC.ToLower().Contains("combobox"))
                {
                    Name_Column[i] = NC.Replace("combobox=", "");
                    var clm = Name_Column[i];
                    var ComboBox = new RepositoryItemComboBox
                    {
                        Name = "combobox=" + clm,
                        TextEditStyle = TextEditStyles.DisableTextEditor,
                        AppearanceDropDown = { Font = _font.ChangeFont() }
                    };
                    if (ItemCombo != null)
                        ComboBox.Items.AddRange(ItemCombo);

                    _data.DGV.RepositoryItems.Add(ComboBox);
                    _data.DGV_Viw.Columns["combobox=" + clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns["combobox=" + clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns["combobox=" + clm].ColumnEdit = ComboBox;

                    _data.DGV_Viw.Columns["combobox=" + clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns["combobox=" + clm].Caption = clm;
                }
                else if (NC.ToLower().Contains("progress")) // جدید پیشرفت کار
                {
                    Name_Column[i] = NC.Replace("progress=", "");
                    var clm = Name_Column[i];
                    var _progressBar = new RepositoryItemProgressBar { Name = clm };

                    _font.ChangeFont(_progressBar, 14);

                    _progressBar.LookAndFeel.UseDefaultLookAndFeel = false;
                    _progressBar.ShowTitle = true;
                    _progressBar.LookAndFeel.SkinName = "Valentine";
                    _progressBar.LookAndFeel.SkinMaskColor = Color.Lime;

                    _data.DGV.RepositoryItems.Add(_progressBar);
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = _progressBar;

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    _data.DGV_Viw.Columns[clm].MaxWidth = 100;
                    _data.DGV_Viw.Columns[clm].MinWidth = 60;
                }
                else if (NC.ToLower().Contains("memo")) // جدید چند خطی
                {
                    Name_Column[i] = NC.Replace("memo=", "");
                    var clm = Name_Column[i];
                    var _memoEdit = new RepositoryItemMemoEdit { Name = clm, };
                    _memoEdit.Appearance.TextOptions.RightToLeft = true;


                    _data.DGV.RepositoryItems.Add(_memoEdit);
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.RightToLeft = true;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = _memoEdit;

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    // _data.DGV_Viw.Columns[clm].MaxWidth = 550;
                    // _data.DGV_Viw.Columns[clm].MinWidth = 150;

                    _memoEdit.Appearance.Font = _font.ChangeFont();
                    _memoEdit.Appearance.ForeColor = Color.Black;
                    _memoEdit.AppearanceFocused.Font = _font.ChangeFont();
                    _memoEdit.AppearanceReadOnly.Font = _font.ChangeFont();
                    _memoEdit.AppearanceDisabled.Font = _font.ChangeFont();
                    _memoEdit.AppearanceFocused.ForeColor = Color.FromArgb(255, 94, 0);

                    //_font.ChangeFont(_memoEdit, 10);
                }
                else if (NC.ToLower().Contains("negnum32")) // اعداد منفی
                {
                    // MessageBox.Show(NC);
                    Name_Column[i] = NC.ToLower().Replace("negnum32=", "");
                    var clm = Name_Column[i];
                    var MemoEdit = new RepositoryItemTextEdit { Name = clm };

                    //var MemoEdit2 = new RepositoryItemLookUpEdit
                    //{

                    //};

                    _font.ChangeFont(MemoEdit, 14);

                    _data.DGV.RepositoryItems.Add(MemoEdit);

                    //DGV_Viw.Columns[clm].DisplayFormat.FormatType = FormatType.Numeric;
                    //DGV_Viw.Columns[clm].DisplayFormat.FormatString = "N0";

                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.RightToLeft = true;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.BackColor = Color.DarkOrange;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = MemoEdit;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    //DGV_Viw1.Columns[clm].MaxWidth = 150;
                    //DGV_Viw1.Columns[clm].MinWidth = 120;
                }
                else if (NC.ToLower().Contains("onoff"))
                {
                    Name_Column[i] = NC.Replace("onoff=", "");
                    var clm = Name_Column[i];
                    var _switch = new RepositoryItemToggleSwitch
                    {
                        Name = clm,
                        ShowText = true,
                        OnText = @"تایید",
                        OffText = @"عدم تایید",
                        ValueOn = "1",
                        ValueOff = "0"
                    };
                    // _switch.LookAndFeel.SkinMaskColor = Color.Lime;
                    _switch.LookAndFeel.UseDefaultLookAndFeel = false;
                    //   RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                    _switch.LookAndFeel.SkinName = "Office 2019 Colorful";
                    // _switch.LookAndFeel.SkinName = "Office 2013 Light Gray";
                    //  ComboBox.LookAndFeel.SkinName = "Office 2013";


                    _data.DGV.RepositoryItems.Add(_switch);

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = _switch;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    _data.DGV_Viw.Columns[clm].MaxWidth = 195;
                    _data.DGV_Viw.Columns[clm].MinWidth = 95;
                }
                else if (NC.ToLower().Contains("maskdate"))
                {
                    Name_Column[i] = NC.ToLower().Replace("maskdate=", "");
                    var clm = Name_Column[i];
                    var maskData = new RepositoryItemTextEdit
                    {
                        Name = clm,
                        Mask = { EditMask = @"___/__/__", MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime, UseMaskAsDisplayFormat = true },

                        //  riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the 

                    };

                    maskData.LookAndFeel.SkinName = "Office 2013";
                    maskData.LookAndFeel.UseDefaultLookAndFeel = false;

                    _data.DGV.RepositoryItems.Add(maskData);

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = maskData;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    //   _data.DGV_Viw.Columns[clm].MaxWidth = 100;
                    //  _data.DGV_Viw.Columns[clm].MinWidth = 100;
                }
                else if (NC.ToLower().Contains("rating"))
                {
                    Name_Column[i] = NC.Replace("rating=", "");
                    var clm = Name_Column[i];
                    var _rating = new RepositoryItemRatingControl { Name = clm, ShowText = true, ValueInterval = 0 };

                    // _rating.ItemClick += _rating_ItemClick;
                    // _rating.Click += _rating_Click;
                    // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                    // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                    _rating.LookAndFeel.SkinName = "Caramel";
                    // _rating.LookAndFeel.SkinName = "Summer 2008";
                    _rating.LookAndFeel.UseDefaultLookAndFeel = false;

                    _data.DGV.RepositoryItems.Add(_rating);

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = _rating;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    _data.DGV_Viw.Columns[clm].MaxWidth = 95;
                    _data.DGV_Viw.Columns[clm].MinWidth = 95;
                }
                else if (NC.ToLower().Contains("checked"))
                {
                    Name_Column[i] = NC.Replace("checked=", "");
                    var clm = Name_Column[i];
                    var _bool = new RepositoryItemCheckEdit
                    {
                        Name = clm,
                        //// ValueChecked = CheckState.Checked
                        // ValueChecked = 0,
                        // ValueUnchecked = 1
                    };
                    //// _bool.AllowGrayed = true;
                    //// _bool.CheckStyle = CheckStyles.Standard;

                    _bool.LookAndFeel.SkinName = "Whiteprint";
                    //  _bool.LookAndFeel.SkinName = "Glass Oceans";
                    // _bool.LookAndFeel.SkinName = "Caramel";
                    // _bool.LookAndFeel.SkinName = "Summer 2008";
                    _bool.LookAndFeel.UseDefaultLookAndFeel = false;

                    _data.DGV.RepositoryItems.Add(_bool);
                    // _bool.CheckStyle = CheckStyles.Standard;

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = _bool;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    _data.DGV_Viw.Columns[clm].MaxWidth = 95;
                    _data.DGV_Viw.Columns[clm].MinWidth = 95;
                }
                else if (NC.ToLower().Contains("volume"))
                {
                    Name_Column[i] = NC.Replace("volume=", "");
                    var clm = Name_Column[i];
                    var _trackBar = new RepositoryItemTrackBar { Name = clm, Minimum = 0, Maximum = 100, };

                    _trackBar.ShowLabels = true;
                    _trackBar.ShowLabelsForHiddenTicks = true;
                    _trackBar.ShowValueToolTip = true;

                    //  if (_trackBar.Labels.Count == 0)
                    // _trackBar.Labels.Add(new TrackBarLabel { Value = 5});
                    // _trackBar.LookAndFeel.SkinName = "Glass Oceans";
                    _trackBar.LookAndFeel.SkinName = "Caramel";
                    //  _bool.LookAndFeel.SkinName = "Summer 2008";
                    _trackBar.LookAndFeel.UseDefaultLookAndFeel = false;

                    _data.DGV.RepositoryItems.Add(_trackBar);
                    // _bool.CheckStyle = CheckStyles.Standard;

                    _data.DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    _data.DGV_Viw.Columns[clm].ColumnEdit = _trackBar;
                    _data.DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                    _data.DGV_Viw.Columns[clm].Caption = clm;
                    _data.DGV_Viw.Columns[clm].MaxWidth = 95;
                    _data.DGV_Viw.Columns[clm].MinWidth = 95;
                }

                else
                {
                    try
                    {
                        _data.DGV_Viw.Columns[NC].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        _data.DGV_Viw.Columns[NC].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        _data.DGV_Viw.Columns[NC].OptionsColumn.AllowSize = true;
                        _data.DGV_Viw.Columns[NC].OptionsColumn.AllowEdit = Allow_Edit;
                        _data.DGV_Viw.OptionsBehavior.Editable = Allow_Edit;
                        _data.DGV_Viw.Columns[NC].OptionsColumn.AllowSort = Allow_Sort ? DefaultBoolean.True : DefaultBoolean.False;
                    }
                    catch
                    {
                        // ignored
                    }
                }

                _font.ChangeFont(chkFind);
            }

            for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            {
                DGV_Viw.Columns[i].VisibleIndex = i;
            }

            #region (افزودن چکباکس)

            var CheckBox = new RepositoryItemCheckEdit { ReadOnly = false };

            #endregion (افزودن چکباکس)

            #region New

            if (_imgButton != null)
                foreach (var t in _imgButton)
                {
                    //var addButton2 = new RepositoryItemButtonEdit();
                    //{

                    //};
                    var addButton = new RepositoryItemHypertextLabel
                    // var addButton = new RepositoryItemButtonEdit
                    {
                        Name = t.Name,
                        //TextEditStyle = TextEditStyles.HideTextEditor,
                        LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2019 Colorful" }
                        // Appearance = { BackColor = Color.FromArgb(0, 136, 255)}
                    };
                    // addButton.AllowInplaceBorderPainter = false;
                    // addButton.Buttons[0].Kind = ButtonPredefines.Glyph;
                    // addButton.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                    // addButton.Buttons[0].Image = t.Image;
                    // addButton.
                    // addButton.con
                    addButton.ContextImageOptions.Image = t.Image;
                    addButton.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    addButton.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                    // addButton.ImageOptions.ImageUri = @"C:\Users\Mojtaba.Yadavar\Desktop\New Icon\Clear_1_20.png";
                    // addButton.ImageOptions



                    //---------------
                    _data.DGV.RepositoryItems.Add(addButton);
                    _data.DGV_Viw.Columns[t.Name].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[t.Name].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    _data.DGV_Viw.Columns[t.Name].ColumnEdit = addButton;
                    _data.DGV_Viw.Columns[t.Name].OptionsColumn.Printable = DefaultBoolean.False;
                    _data.DGV_Viw.Columns[t.Name].MaxWidth = 44;
                    _data.DGV_Viw.Columns[t.Name].MinWidth = 44;
                    MaxMinWidth(t.Name, 44, 44);
                    _data.DGV_Viw.Columns[t.Name].OptionsFilter.AllowFilter = false;
                    _data.DGV_Viw.Columns[t.Name].OptionsFilter.AllowAutoFilter = false;
                    _data.DGV_Viw.Columns[t.Name].OptionsColumn.FixedWidth = false;
                    _data.DGV_Viw.Columns[t.Name].OptionsColumn.AllowSize = false;


                    _font.ChangeFont(_data.DGV_Viw.Columns[t.Name]);
                }

            _data.DGV_Viw.OptionsView.ShowFooter = showFooter;

            #endregion

            // SetFieldSizeColumn();
            this.DGV_Viw.EndUpdate();
        }
        public BandedGridColumn GridStructureSingleColumn(DataTable dt, string clm, int locIndex, object image = null, RepositoryItem repositoryItem = null,
            string bandName = "banded", int sizeF = 11)
        {
            this.DGV_Viw.BeginUpdate();

            DGV_Viw.OptionsView.ShowBands = false;

            #region(افزودن افزونه های عملیاتی)

            List<string> nameColFormatN0 = new List<string>();
            List<string> nameColFormatN1 = new List<string>();
            List<string> nameColFormatN2 = new List<string>();
            List<string> nameColFormatN3 = new List<string>();

            #endregion

            if (clm.Contains("="))
            {
                var IndexOFF = clm.IndexOf("=", StringComparison.Ordinal);
                var getType = clm.Substring(0, IndexOFF).ToLower();

                if (getType.Contains("image"))
                {
                    clm = clm.Replace("image=", "");
                    dt.Columns.Add(clm, typeof(Image));
                }
                else if (getType.Contains("int32") || getType.Contains("int64") || getType.Contains("double"))
                {
                    if (getType.Contains("int32"))
                    {
                        clm = clm.Replace("int32=", "");
                        if (getType.ToLower().Contains("n0"))
                        {
                            clm = clm.Replace("N0", "");
                            nameColFormatN0.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n1"))
                        {
                            clm = clm.Replace("N1", "");
                            nameColFormatN1.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n2"))
                        {
                            clm = clm.Replace("N2", "");
                            nameColFormatN2.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n3"))
                        {
                            clm = clm.Replace("N3", "");
                            nameColFormatN3.Add(clm);
                        }

                        dt.Columns.Add(clm, typeof(Int32));
                    }
                    else if (getType.Contains("int64"))
                    {
                        clm = clm.Replace("int64=", "");
                        if (getType.ToLower().Contains("n0"))
                        {
                            clm = clm.Replace("N0", "");
                            nameColFormatN0.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n1"))
                        {
                            clm = clm.Replace("N1", "");
                            nameColFormatN1.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n2"))
                        {
                            clm = clm.Replace("N2", "");
                            nameColFormatN2.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n3"))
                        {
                            clm = clm.Replace("N3", "");
                            nameColFormatN3.Add(clm);
                        }

                        dt.Columns.Add(clm, typeof(Int64));
                    }
                    else if (getType.Contains("double"))
                    {
                        clm = clm.Replace("double=", "");
                        if (getType.ToLower().Contains("n0"))
                        {
                            clm = clm.Replace("N0", "");
                            nameColFormatN0.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n1"))
                        {
                            clm = clm.Replace("N1", "");
                            nameColFormatN1.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n2"))
                        {
                            clm = clm.Replace("N2", "");
                            nameColFormatN2.Add(clm);
                        }
                        else if (getType.ToLower().Contains("n3"))
                        {
                            clm = clm.Replace("N3", "");
                            nameColFormatN3.Add(clm);
                        }

                        dt.Columns.Add(clm, typeof(double));
                    }
                }
                else
                    switch (getType.ToLower())
                    {
                        case "float":
                            clm = clm.Replace("float=", "");
                            dt.Columns.Add(clm, typeof(float));
                            break;

                        case "double":
                            clm = clm.Replace("double=", "");
                            dt.Columns.Add(clm, typeof(double));
                            break;

                        case "string":
                            clm = clm.Replace("string=", "");
                            dt.Columns.Add(clm, typeof(string));
                            break;

                        case "combobox":
                            dt.Columns.Add(clm);
                            break;

                        case "bool":
                            dt.Columns.Add(clm);
                            clm = clm.Replace("bool=", "");
                            break;

                        case "progress":
                            dt.Columns.Add(clm.Replace("progress=", ""));
                            break;

                        case "maskdate":
                            dt.Columns.Add(clm.Replace("maskdate=", ""));
                            break;

                        case "onoff":
                            dt.Columns.Add(clm.Replace("onoff=", ""));
                            break;

                        case "rating":
                            dt.Columns.Add(clm.Replace("rating=", ""));
                            break;

                        case "memo":
                            dt.Columns.Add(clm.Replace("memo=", ""));
                            break;

                        case "checked":
                            dt.Columns.Add(clm.Replace("checked=", ""), typeof(bool));
                            break;

                        case "volume":
                            dt.Columns.Add(clm.Replace("volume=", ""));
                            break;

                        case "image":
                            break;

                        case "button":
                            dt.Columns.Add(clm.Replace("button=", ""));
                            break;

                        case "btnsvg":
                            dt.Columns.Add(clm.Replace("btnsvg=", ""));
                            break;

                        case "btntext":
                            dt.Columns.Add(clm.Replace("btntext=", ""));
                            break;

                        case "rich":
                            dt.Columns.Add(clm.Replace("rich=", ""));
                            break;

                        default:
                            dt.Columns.Add(clm, typeof(string));
                            break;
                    }
            }
            else
            {
                dt.Columns.Add(clm);
            }

            string nameForDGV = clm;
            if (clm.Contains("="))
            {
                var IndexOFF = clm.IndexOf("=", StringComparison.Ordinal);
                nameForDGV = clm.Substring(IndexOFF + 1, clm.Length - IndexOFF - 1);
            }

            BandedGridColumn getClm = DGV_Viw.Columns.Add();

            getClm.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            getClm.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

            getClm.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            getClm.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            getClm.OwnerBand = DGV_Viw.Bands[bandName];
            getClm.Name = "col" + nameForDGV;
            getClm.FieldName = nameForDGV;
            getClm.Caption = nameForDGV;
            getClm.Visible = true;
            getClm.ColVIndex = locIndex;
            //   getClm.VisibleIndex = DGV_Viw.VisibleColumns.Count + 1;

            var NC = clm;

            if (NC.ToLower().Contains("progress")) // جدید پیشرفت کار
            {
                clm = NC.Replace("progress=", "");

                clm = clm;
                var _progressBar = new RepositoryItemProgressBar { Name = clm };

                _font.ChangeFont(_progressBar, 14);

                _progressBar.LookAndFeel.UseDefaultLookAndFeel = false;
                _progressBar.ShowTitle = true;
                _progressBar.LookAndFeel.SkinName = "Valentine";
                _progressBar.LookAndFeel.SkinMaskColor = Color.Lime;

                DGV.RepositoryItems.Add(_progressBar);
                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].ColumnEdit = _progressBar;

                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 100;
                DGV_Viw.Columns[clm].MinWidth = 60;
            }
            else if (NC.ToLower().Contains("memo")) // جدید چند خطی
            {
                clm = NC.Replace("memo=", "");
                clm = clm;
                var _memoEdit = new RepositoryItemMemoEdit { Name = clm, };
                _memoEdit.Appearance.TextOptions.RightToLeft = true;


                DGV.RepositoryItems.Add(_memoEdit);
                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.RightToLeft = true;

                DGV_Viw.Columns[clm].ColumnEdit = _memoEdit;

                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 550;
                DGV_Viw.Columns[clm].MinWidth = 150;

                _memoEdit.Appearance.Font = _font.ChangeFont();
                _memoEdit.Appearance.ForeColor = Color.Black;
                _memoEdit.AppearanceFocused.Font = _font.ChangeFont();
                _memoEdit.AppearanceReadOnly.Font = _font.ChangeFont();
                _memoEdit.AppearanceDisabled.Font = _font.ChangeFont();
                _memoEdit.AppearanceFocused.ForeColor = Color.FromArgb(255, 94, 0);

                //_font.ChangeFont(_memoEdit, 10);
            }
            else if (NC.ToLower().Contains("negnum32")) // اعداد منفی
            {
                // MessageBox.Show(NC);
                clm = NC.ToLower().Replace("negnum32=", "");
                clm = clm;
                var MemoEdit = new RepositoryItemTextEdit { Name = clm };

                //var MemoEdit2 = new RepositoryItemLookUpEdit
                //{

                //};

                _font.ChangeFont(MemoEdit, 14);

                DGV.RepositoryItems.Add(MemoEdit);

                //DGV_Viw.Columns[clm].DisplayFormat.FormatType = FormatType.Numeric;
                //DGV_Viw.Columns[clm].DisplayFormat.FormatString = "N0";

                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.RightToLeft = true;
                DGV_Viw.Columns[clm].AppearanceCell.BackColor = Color.DarkOrange;
                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].ColumnEdit = MemoEdit;
                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                //DGV_Viw1.Columns[clm].MaxWidth = 150;
                //DGV_Viw1.Columns[clm].MinWidth = 120;
            }
            else if (NC.ToLower().Contains("onoff"))
            {
                clm = NC.Replace("onoff=", "");
                clm = clm;
                var _switch = new RepositoryItemToggleSwitch
                {
                    Name = clm,
                    ShowText = true,
                    OnText = @"تایید",
                    OffText = @"عدم تایید",
                    ValueOn = "1",
                    ValueOff = "0"
                };
                _switch.LookAndFeel.SkinMaskColor = Color.Lime;
                _switch.LookAndFeel.UseDefaultLookAndFeel = false;
                //   RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                _switch.LookAndFeel.SkinName = "Office 2013 Light Gray";
                //  ComboBox.LookAndFeel.SkinName = "Office 2013";


                DGV.RepositoryItems.Add(_switch);

                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                DGV_Viw.Columns[clm].ColumnEdit = _switch;
                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 195;
                DGV_Viw.Columns[clm].MinWidth = 95;
            }
            else if (NC.ToLower().Contains("maskdate"))
            {
                clm = NC.ToLower().Replace("maskdate=", "");
                clm = clm;
                var maskData = new RepositoryItemTextEdit
                {
                    Name = clm,
                    Mask = { EditMask = @"___/__/__", MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime, UseMaskAsDisplayFormat = true },

                    //  riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the 

                };

                maskData.LookAndFeel.SkinName = "Office 2013";
                maskData.LookAndFeel.UseDefaultLookAndFeel = false;

                DGV.RepositoryItems.Add(maskData);

                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].ColumnEdit = maskData;
                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                //   _data.DGV_Viw.Columns[clm].MaxWidth = 100;
                //  _data.DGV_Viw.Columns[clm].MinWidth = 100;
            }
            else if (NC.ToLower().Contains("rating"))
            {
                clm = NC.Replace("rating=", "");

                var _rating = new RepositoryItemRatingControl { Name = clm, ShowText = true, ValueInterval = 0 };

                // _rating.ItemClick += _rating_ItemClick;
                // _rating.Click += _rating_Click;
                // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                _rating.LookAndFeel.SkinName = "Caramel";
                // _rating.LookAndFeel.SkinName = "Summer 2008";
                _rating.LookAndFeel.UseDefaultLookAndFeel = false;

                DGV.RepositoryItems.Add(_rating);

                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                DGV_Viw.Columns[clm].ColumnEdit = _rating;
                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 95;
                DGV_Viw.Columns[clm].MinWidth = 95;
            }
            else if (NC.ToLower().Contains("checked"))
            {
                clm = NC.Replace("checked=", "");
                var _bool = new RepositoryItemCheckEdit
                {
                    Name = clm,
                    //// ValueChecked = CheckState.Checked
                    // ValueChecked = 0,
                    // ValueUnchecked = 1
                    CheckBoxOptions = { Style = CheckBoxStyle.Custom },
                    ImageOptions =
                    {
                        SvgImageChecked = Properties.Resources.Check_New_2, SvgImageUnchecked = Properties.Resources.UnCheck_New_2, SvgImageSize = new Size(20, 20)
                    }
                };

                //// _bool.AllowGrayed = true;
                //// _bool.CheckStyle = CheckStyles.Standard;

                // _bool.LookAndFeel.SkinName = "Whiteprint";
                // _bool.LookAndFeel.SkinName = "Glass Oceans";
                // _bool.LookAndFeel.SkinName = "Caramel";
                // _bool.LookAndFeel.SkinName = "Summer 2008";
                //  _bool.LookAndFeel.UseDefaultLookAndFeel = false;

                DGV.RepositoryItems.Add(_bool);
                // _bool.CheckStyle = CheckStyles.Standard;

                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].ColumnEdit = _bool;
                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 95;
                DGV_Viw.Columns[clm].MinWidth = 95;
            }
            else if (NC.ToLower().Contains("volume"))
            {
                clm = NC.Replace("volume=", "");
                clm = clm;
                var _trackBar = new RepositoryItemTrackBar { Name = clm, Minimum = 0, Maximum = 100, };

                _trackBar.ShowLabels = true;
                _trackBar.ShowLabelsForHiddenTicks = true;
                _trackBar.ShowValueToolTip = true;

                //  if (_trackBar.Labels.Count == 0)
                // _trackBar.Labels.Add(new TrackBarLabel { Value = 5});
                // _trackBar.LookAndFeel.SkinName = "Glass Oceans";
                _trackBar.LookAndFeel.SkinName = "Caramel";
                //  _bool.LookAndFeel.SkinName = "Summer 2008";
                _trackBar.LookAndFeel.UseDefaultLookAndFeel = false;

                DGV.RepositoryItems.Add(_trackBar);
                // _bool.CheckStyle = CheckStyles.Standard;

                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].ColumnEdit = _trackBar;
                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 95;
                DGV_Viw.Columns[clm].MinWidth = 95;
            }
            else if (NC.ToLower().Contains("button"))
            {
                clm = NC.Replace("button=", "");


                var addButton = new RepositoryItemButtonEdit { Name = clm, TextEditStyle = TextEditStyles.HideTextEditor, };

                if (repositoryItem != null)
                {
                    // var getType = repositoryItem.GetType();
                    addButton = repositoryItem as RepositoryItemButtonEdit;
                    // addButton = (RepositoryItemButtonEdit)repositoryItem;
                    addButton.Name = clm;
                }

                addButton.Buttons[0].Kind = ButtonPredefines.Glyph;
                addButton.Buttons[0].Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                addButton.Buttons[0].Image = (Image)image;

                addButton.Buttons[0].Appearance.TextOptions.VAlignment = VertAlignment.Center;
                addButton.Buttons[0].Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                //---------------
                DGV.RepositoryItems.Add(addButton);
                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Columns[clm].ColumnEdit = addButton;
                DGV_Viw.Columns[clm].Width = 40;
                DGV_Viw.Columns[clm].MaxWidth = 40;
                DGV_Viw.Columns[clm].MinWidth = 40;
                MaxMinWidth(clm, 40, 40);

                DGV_Viw.Columns[clm].OptionsFilter.AllowFilter = false;
                DGV_Viw.Columns[clm].OptionsFilter.AllowAutoFilter = false;
                DGV_Viw.Columns[clm].OptionsColumn.FixedWidth = false;
                DGV_Viw.Columns[clm].OptionsColumn.AllowSize = false;
                //DGV_Viw.Columns[clm].Image = image;
                // if (repositoryItem != null)
                //   DGV_Viw.Columns[clm].OptionsColumn.AllowEdit = false;

                _font.ChangeFont(DGV_Viw.Columns[clm], sizeF);
            }
            else if (NC.ToLower().Contains("btntext"))
            {
                clm = NC.Replace("btntext=", "");

                var addButton = new RepositoryItemButtonEdit
                {
                    Name = clm,
                    TextEditStyle = TextEditStyles.DisableTextEditor,
                    ContextImageOptions = { Image = (Image)image }
                };

                DGV.RepositoryItems.Add(addButton);
                DGV_Viw.Columns[clm].ColumnEdit = addButton;

                _font.ChangeFont(DGV_Viw.Columns[clm], sizeF);
            }
            else if (NC.ToLower().Contains("btnsvg"))
            {
                clm = NC.Replace("btnsvg=", "");

                SvgImage getSvg;

                using (var memstr = new MemoryStream((byte[])image))
                {
                    getSvg = SvgImage.FromStream(memstr);
                }

                var addButton = new RepositoryItemHyperLinkEdit
                {
                    Name = clm,
                    ContextImageOptions = { SvgImage = getSvg, SvgImageSize = new Size(18, 18) },
                    ImageAlignment = HorzAlignment.Center,
                };

                DGV.RepositoryItems.Add(addButton);
                DGV_Viw.Columns[clm].ColumnEdit = addButton;

                _font.ChangeFont(DGV_Viw.Columns[clm], sizeF);

                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].Width = 26;
                DGV_Viw.Columns[clm].MaxWidth = 26;
                DGV_Viw.Columns[clm].MinWidth = 26;
                MaxMinWidth(clm, 26, 26);

                DGV_Viw.Columns[clm].OptionsFilter.AllowFilter = false;
                DGV_Viw.Columns[clm].OptionsFilter.AllowAutoFilter = false;
                DGV_Viw.Columns[clm].OptionsColumn.FixedWidth = false;
                DGV_Viw.Columns[clm].OptionsColumn.AllowSize = false;
                DGV_Viw.Columns[clm].OptionsColumn.AllowSort = DefaultBoolean.False;

            }

            else if (NC.ToLower().Contains("rich"))
            {
                clm = NC.Replace("rich=", "");

                var _richEdit = new RepositoryItemRichTextEdit { Name = clm };
                _richEdit.Appearance.TextOptions.RightToLeft = true;



                DGV.RepositoryItems.Add(_richEdit);
                DGV_Viw.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                DGV_Viw.Columns[clm].AppearanceCell.TextOptions.RightToLeft = true;

                DGV_Viw.Columns[clm].ColumnEdit = _richEdit;

                DGV_Viw.Columns[clm].AppearanceHeader.Name = clm;
                DGV_Viw.Columns[clm].Caption = clm;
                DGV_Viw.Columns[clm].MaxWidth = 550;
                DGV_Viw.Columns[clm].MinWidth = 150;

                _richEdit.Appearance.Font = _font.ChangeFont();
                _richEdit.Appearance.ForeColor = Color.Black;
                _richEdit.AppearanceFocused.Font = _font.ChangeFont();
                _richEdit.AppearanceReadOnly.Font = _font.ChangeFont();
                _richEdit.AppearanceDisabled.Font = _font.ChangeFont();
                _richEdit.AppearanceFocused.ForeColor = Color.FromArgb(255, 94, 0);
            }

            else
            {
                try
                {
                    DGV_Viw.Columns[NC].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw.Columns[NC].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw.Columns[NC].OptionsColumn.AllowSize = true;
                }
                catch
                {
                    // ignored
                }
            }

            _font.ChangeFont(chkFind);

            this.DGV_Viw.EndUpdate();
            return getClm;
        }
        public object GetValue(int row, string clm, object defualtValue = null)
        {
            string get = "";
            try
            {
                if (row > -1)
                    get = DGV_Viw.GetRowCellValue(row, clm).ToString();
            }
            catch (Exception e)
            {
                // ignored
            }

            if (defualtValue != null && !string.IsNullOrEmpty(defualtValue.ToString()))
                if (get == "")
                    return defualtValue;
            return get;
        }
        public object GetValue(string clm, object defualtValue = null)
        {
            //  if (DGV_Viw.FocusedRowHandle > -1)

            try
            {
                var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm);



                if (defualtValue != null && !string.IsNullOrEmpty(defualtValue.ToString()))
                {
                    var getNull = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm).ToString();
                    if (string.IsNullOrEmpty(getNull))
                        return defualtValue;
                }

                return get;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public T GetValue<T>(string clm, object defualtValue = null)
        {
            try
            {
                var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm);


                var getType = typeof(T).FullName;

                if (getType == typeof(Guid).FullName)
                {
                    var getValue2 = Guid.Parse(get.ToString());
                    return ChangeType<T>(getValue2);
                }

                if (defualtValue != null && !string.IsNullOrEmpty(defualtValue.ToString()))
                {
                    var getNull = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm).ToString();
                    if (string.IsNullOrEmpty(getNull))
                        return ChangeType<T>(defualtValue);
                    // return defualtValue;
                }

                return ChangeType<T>(get);
                //  return get;
            }
            catch (Exception e)
            {
                return default;
                //  return null;
            }
        }
        public T GetValue<T>(int rowHandle, string clm, object defualtValue = null)
        {
            try
            {
                object rowCellValue = DGV_Viw.GetRowCellValue(rowHandle, clm);

                var getType = typeof(T).FullName;

                if (getType == typeof(Guid).FullName)
                {
                    var getValue2 = Guid.Parse(rowCellValue.ToString());
                    return ChangeType<T>(getValue2);
                }


                var get = rowCellValue;
                if (defualtValue != null && !string.IsNullOrEmpty(defualtValue.ToString()))
                {
                    if (rowCellValue == null)
                        return default;

                    var getNull = rowCellValue.ToString();
                    if (string.IsNullOrEmpty(getNull))
                        return ChangeType<T>(defualtValue);

                    // return defualtValue;
                }

                return ChangeType<T>(get);
                //  return get;
            }
            catch (Exception e)
            {
                return default;
                //  return null;
            }
        }
        public static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return default;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }

        public dynamic GetValueDynamic(string clm)
        {
            var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm);
            //  var getNull = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm).ToString();
            return get;
        }
        public object GetValue(string clm)
        {
            var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm);
            //  var getNull = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm).ToString();
            return get;
        }
        public object GetValue(object defualtValue = null)
        {
            if (DGV_Viw.FocusedRowHandle > -1)
            {
                var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, GetColumn().FieldName);
                if (defualtValue != null && !string.IsNullOrEmpty(defualtValue.ToString()))
                {
                    var getNull = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, GetColumn().FieldName).ToString();
                    if (string.IsNullOrEmpty(getNull))
                        return defualtValue;
                }

                return get;
            }

            return null;
        }
        public T GetValue<T>(object defualtValue = null)
        {
            if (DGV_Viw.FocusedRowHandle > -1)
            {
                var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, GetColumn().FieldName);
                if (defualtValue != null && !string.IsNullOrEmpty(defualtValue.ToString()))
                {
                    var getNull = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, GetColumn().FieldName).ToString();
                    if (string.IsNullOrEmpty(getNull))
                        return ChangeType<T>(defualtValue);
                    // return defualtValue;
                }

                return ChangeType<T>(get);
                //return get;
            }

            // return null;
            return default;
        }
        public object GetText(int row, string clm)
        {
            var get = DGV_Viw.GetRowCellDisplayText(row, clm);
            return get;
        }
        //Test Trial Installer Type Attribute Aware Column Auto Filter Condition ERP
        public object GetText(int row, string clm, object defaultValue)
        {
            var get = DGV_Viw.GetRowCellDisplayText(row, clm);
            if (string.IsNullOrEmpty(get) && defaultValue != null)
                return defaultValue;
            return get;
        }
        public object GetText(string clm, object defaultValue = null)
        {
            var get = DGV_Viw.GetRowCellDisplayText(DGV_Viw.FocusedRowHandle, clm);
            if (string.IsNullOrEmpty(get) && defaultValue != null)
                return defaultValue;
            return get;
        }
        public void SetValue(int row, string clm, object value)
        {
            DGV_Viw.SetRowCellValue(row, clm, value);
        }
        public void SetValue(string clm, object value)
        {
            DGV_Viw.SetRowCellValue(DGV_Viw.FocusedRowHandle, clm, value);
        }
        public DataRow GetRow()
        {
            var getRow = DGV_Viw.GetFocusedDataRow();
            return getRow;
        }
        public DataRow GetRow(int row)
        {
            var getRow = (DataRow)DGV_Viw.GetRow(row);
            return getRow;
        }
        public void DeleteRow()
        {
            DGV_Viw.BeginUpdate();
            DGV_Viw.DeleteRow(DGV_Viw.FocusedRowHandle);
            DGV_Viw.EndUpdate();
        }
        public void DeleteRow(int row)
        {
            DGV_Viw.BeginUpdate();
            DGV_Viw.DeleteRow(row);
            DGV_Viw.EndUpdate();
        }
        public void DeleteRows()
        {
            DGV_Viw.BeginUpdate();
            while (DGV_Viw.RowCount > 0)
            {
                DGV_Viw.DeleteRow(0);
            }

            DGV_Viw.EndUpdate();
        }
        public GridColumn GetColumn()
        {
            GridColumn _getColumn = DGV_Viw.FocusedColumn;
            return _getColumn;
        }
        public GridColumn GetColumn(int column)
        {
            GridColumn _getColumn = DGV_Viw.Columns[column];
            return _getColumn;
        }
        public GridColumn GetColumn(string column)
        {
            GridColumn _getColumn = DGV_Viw.Columns[column];
            return _getColumn;
        }
        public void DisableMerge()
        {
            // DGV_Viw.Columns["عملکرد"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            // DGV_Viw.Columns["گزارش"].OptionsColumn.AllowMerge = DefaultBoolean.False;


            for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            {
                DGV_Viw.Columns[i].OptionsColumn.AllowMerge = DefaultBoolean.False;
            }

            DGV_Viw.OptionsView.AllowCellMerge = false;
        }
        public void DisableMerge(List<string> nameColumn)
        {
            foreach (var t in nameColumn)
            {
                DGV_Viw.Columns[t].OptionsColumn.AllowMerge = DefaultBoolean.False;
            }

            DGV_Viw.OptionsView.AllowCellMerge = true;
        }
        public void EnabledMerge()
        {
            for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            {
                DGV_Viw.Columns[i].OptionsColumn.AllowMerge = DefaultBoolean.True;
            }

            DGV_Viw.OptionsView.AllowCellMerge = true;
        }
        public void EnabledMerge(List<string> nameColumn)
        {

            foreach (var t in nameColumn)
            {
                DGV_Viw.Columns[t].OptionsColumn.AllowMerge = DefaultBoolean.True;
            }

            DGV_Viw.OptionsView.AllowCellMerge = true;
        }
        public void AddButtonOrControlUpGrid(Control control)
        {
            var getFindControl = pnlButton.Controls.Find(control.Name, true).Any();
            //  var getFindControl = groupControl1.Controls.Find(control.Name, true).Any();
            if (!getFindControl)
            {
                // control.Dock = dockStyle;
                pnlButton.Controls.Add(control);
                pnlButton.Visible = true;
                // control.Width = width;
            }
        }
        public void ShowGroupByHeader()
        {
            DGV_Viw.OptionsView.ShowGroupPanel = true;
        }
        public void HideGroupByHeader()
        {
            DGV_Viw.OptionsView.ShowGroupPanel = false;
        }
        public void ShowFooter()
        {
            CheckForIllegalCrossThreadCalls = false;

            new Thread(() =>
            {
                Thread.Sleep(2000);
                DGV_Viw.OptionsView.ShowFooter = true;

            }).Start();
        }
        public void HideFooter()
        {
            CheckForIllegalCrossThreadCalls = false;

            new Thread(() =>
            {
                Thread.Sleep(2000);
                DGV_Viw.OptionsView.ShowFooter = false;

            }).Start();
        }
        public void EnableMultiSelectColumn()
        {
            DGV_Viw.OptionsSelection.MultiSelect = true;
            DGV_Viw.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }
        public void DisableMultiSelectColumn()
        {
            DGV_Viw.OptionsSelection.MultiSelect = false;
            DGV_Viw.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        }
        public void ShowCheckBoxSelectorInColumnHeader(DefaultBoolean boolean)
        {
            DGV_Viw.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = boolean;
        }
        public class modelColumnFitWidth
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public string FieldName { get; set; }
        }
        private List<modelColumnFitWidth> _lstColumnFitWidth = new List<modelColumnFitWidth>();
        public void MaxMinWidth(string nameColumn, int min, int max)
        {
            var getClm = DGV_Viw.Columns.Any(a => a.FieldName == nameColumn);
            if (getClm)
            {
                DGV_Viw.Columns[nameColumn].Width = max;
                // DGV_Viw.Columns[nameColumn].MaxWidth = max;
            }

            foreach (modelColumnFitWidth item in _lstColumnFitWidth)
            {
                if (item.FieldName == nameColumn)
                {
                    item.Min = min;
                    item.Max = max;
                    return;
                }
            }

            _lstColumnFitWidth.Add(new modelColumnFitWidth { Min = min, Max = max, FieldName = nameColumn });

            DGV_Viw.Columns[nameColumn].MinWidth = min;
            DGV_Viw.Columns[nameColumn].MaxWidth = max;

            //new Task(() =>
            //{
            //    Thread.Sleep(1800);
            //    ThreadSize();
            //}).Start();

            //  SetFieldSizeColumn();
        }
        public void DisableWidthSize(string column, bool enable = false)
        {
            DGV_Viw.Columns[column].OptionsColumn.AllowSize = enable;
        }
        public enum modelBandedGroup
        {
            Start,
            Default,
            End
        }
        /// <summary>
        /// گروه بندی ستون ها
        /// </summary>
        /// <param name="bandedName">نام گروه</param>
        /// <param name="posBanded">موقعیت گروه</param>
        /// <param name="columnName">نام ستون</param>
        /// <param name="bandedGroup"></param>
        /// <param name="color"></param>
        public void ColumnBandedGroup(string bandedName, int posBanded, string columnName, modelBandedGroup bandedGroup = modelBandedGroup.Default, Color color = default(Color))
        {
            if (bandedGroup == modelBandedGroup.Start)
            {
                DGV_Viw.OptionsView.ShowBands = true;

                var getBandedPublic = DGV_Viw.Bands["banded"];
                for (int i = 0; i < DGV_Viw.Columns.Count; i++)
                {
                    var getColumn2 = DGV_Viw.Columns[i];
                    getColumn2.OwnerBand = getBandedPublic;
                }

                while (DGV_Viw.Bands.Count != 1)
                {
                    for (int i = 0; i < DGV_Viw.Bands.Count; i++)
                    {
                        if (DGV_Viw.Bands[i].Name != @"banded")
                        {
                            var getBandedForDel = DGV_Viw.Bands[i];
                            DGV_Viw.Bands.Remove(getBandedForDel);
                        }
                    }
                }
            }

            banded.AppearanceHeader.Font = _font.ChangeFont(12);

            var getBanded = DGV_Viw.Bands[bandedName];
            if (getBanded == null)
                DGV_Viw.Bands.Add(new GridBand
                {
                    Name = bandedName,
                    Caption = bandedName,
                    AppearanceHeader = { TextOptions = { HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center, }, Font = _font.ChangeFont(8), },
                    // AutoFillDown = true,
                    // Width=50,
                    OptionsBand =
                    {
                        FixedWidth = false,
                        AllowSize = false,
                        AllowHotTrack = true,
                        AllowMove = false,
                        AllowPress = true,
                        ShowInCustomizationForm = false
                    }
                });

            var getColumn = DGV_Viw.Columns[columnName];
            // getColumn.MinWidth = 100;
            // getColumn.MaxWidth = 100;
            var getBanded2 = DGV_Viw.Bands[bandedName];
            //getBanded2.
            //  getBanded2.AppearanceHeader.ForeColor = color;
            //  getBanded2.AppearanceHeader.BackColor2 = color;
            //   var getColor = Color.FromArgb(190, color.R, color.G, color.B);
            getBanded2.AppearanceHeader.BackColor = color;
            // if (getColumn.OwnerBand.Name == "banded")
            getColumn.OwnerBand = getBanded2;

            DGV_Viw.Bands.MoveTo(posBanded - 1, getBanded2);

            DGV_Viw.Bands.View.Appearance.BandPanel.Font = _font.ChangeFont(12);
            DGV_Viw.Bands.View.Appearance.BandPanel.TextOptions.HAlignment = HorzAlignment.Center;
            DGV_Viw.Bands.View.Appearance.BandPanel.TextOptions.VAlignment = VertAlignment.Center;

            if (bandedGroup == modelBandedGroup.End)
            {
                for (int i = 0; i < DGV_Viw.Bands.Count; i++)
                {
                    if (DGV_Viw.Bands[i].Name != @"banded")
                    {
                        var getBandedForDel = DGV_Viw.Bands[i];
                        if (getBandedForDel.Columns.Count == 1)
                            DGV_Viw.Bands.Remove(getBandedForDel);
                    }
                }

                DGV_Viw.OptionsView.ShowBands = true;
            }
        }
        public void AddSummaryItem(string column, string summaryName, string displayFormat, SummaryItemType type, bool clear = true)
        {
            if (clear)
                DGV_Viw.Columns[column].Summary.Clear();

            GridColumnSummaryItem summaryItem = new GridColumnSummaryItem { FieldName = summaryName, SummaryType = type, DisplayFormat = displayFormat };
            DGV_Viw.Columns[column].Summary.Add(summaryItem);
        }
        public void AddSummaryGroupItem(string column, string summaryName, string displayFormat, SummaryItemType type, bool clear = true)
        {
            if (clear)
                DGV_Viw.Columns[column].Summary.Clear();

            GridGroupSummaryItem groupSummaryItem = new GridGroupSummaryItem
            {
                FieldName = summaryName,
                SummaryType = type,
                DisplayFormat = displayFormat,
                ShowInGroupColumnFooter = DGV_Viw.Columns[column]
            };
            //   item1.ShowInGroupColumnFooter = gridView1.Columns["UnitPrice"];
            // DGV_Viw.Columns[column].Summary.Add(summaryItem);
            DGV_Viw.GroupSummary.Add(groupSummaryItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtMaster"></param>
        /// <param name="dtChild"></param>
        /// <param name="relationName">بدون Space</param>
        /// <param name="keyID"></param>
        /// <param name="childID"></param>
        /// <returns></returns>
        public GridView SetChildGridDetails(DataTable dtMaster, DataTable dtChild, string relationName, string keyID, string childID)
        {
            GridView gvChild = new GridView(DGV);

            try
            {
                DataSet dsFixDataSet = new DataSet();
                dsFixDataSet.Tables.Clear();

                dsFixDataSet.Tables.Add(dtMaster);
                dsFixDataSet.Tables.Add(dtChild);

                DataColumn keyColumn2 = dsFixDataSet.Tables[0].Columns[keyID];
                DataColumn foreignKeyColumn2 = dsFixDataSet.Tables[1].Columns[childID];

                var trim = relationName.Replace(" ", "");
                dsFixDataSet.Relations.Add(trim, keyColumn2, foreignKeyColumn2);
            }

            catch
            {
                // ignored
            }

            if (DGV.LevelTree.Nodes.Count < 1)
            {
                DGV.LevelTree.Nodes.Add(relationName, gvChild);
            }

            return gvChild;
        }
        public void OneClickSelectedRow(bool defaultClick)
        {
            if (!defaultClick)
                DGV_Viw.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
            else
                DGV_Viw.OptionsBehavior.EditorShowMode = EditorShowMode.Default;
        }
        private void DGV_Viw_TopRowChanged(object sender, EventArgs e)
        {
            // DGV_Viw.BestFitColumns(true);
        }
        private void DGV_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //GridControl grid = sender as GridControl;
            //GridView view = grid.FocusedView as GridView;
            //if (e.KeyData == Keys.Down)
            //{
            //    view.MoveNext();
            //    e.Handled = true;
            //}
            //if (e.KeyData == Keys.Up)
            //{
            //    view.MovePrev();
            //    e.Handled = true;
            //}
        }
        private void DGV_Viw_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            try
            {
                var gc = DGV_Viw.Columns.Any(f => f.FieldName == e.Column.FieldName);
                if (gc)
                {
                    var getMin = _lstColumnFitWidth.First(f => f.FieldName == e.Column.FieldName).Min;
                    var getMax = _lstColumnFitWidth.First(f => f.FieldName == e.Column.FieldName).Max;

                    // e.Column.Width = getMin;
                    e.Column.MaxWidth = getMax;

                    if (e.Column.Width < getMin)
                    {
                        e.Column.MinWidth = getMin;
                    }
                    else if (e.Column.Width > getMax)
                    {
                        e.Column.Width = getMax;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private void DGV_Viw_DataSourceChanged(object sender, EventArgs e)
        {
            //foreach (var i in _lstColumnFitWidth)
            //{
            //    GridColumn gridColumn = new GridColumn { FieldName = i.FieldName };
            //    var gc = DGV_Viw.Columns.Any(f => f.FieldName == i.FieldName);
            //    if (gc)
            //    {

            //        var getMin = _lstColumnFitWidth.First(f => f.FieldName == i.FieldName).Min;
            //        var getMax = _lstColumnFitWidth.First(f => f.FieldName == i.FieldName).Max;
            //        if (gridColumn.Width < getMin)
            //        {
            //            gridColumn.Width = getMin;
            //            // return;
            //        }
            //        else if (gridColumn.Width > getMax)
            //        {
            //            gridColumn.Width = getMax;
            //        }
            //    }
            //}
        }
        private void DGV_Viw_RowLoaded(object sender, RowEventArgs e)
        {
            //  SetFieldSizeColumn();
        }
        public void SetFieldSizeColumn()
        {
            //  ThreadSize();
            Thread _Thread = new Thread(ThreadSize);
            _Thread.Start();
            // ThreadSize();
        }
        private void ThreadSize()
        {
            Thread.Sleep(1200);
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        DGV_Viw.BestFitColumns(false);
                        foreach (var i in _lstColumnFitWidth)
                        {
                            //  DGV_Viw.Columns[i.FieldName].Width = 1;
                            // GridColumn gridColumn = new GridColumn { FieldName = i.FieldName };
                            var gc = DGV_Viw.Columns.Any(f => f.FieldName == i.FieldName);
                            if (gc)
                            {
                                var getMin = _lstColumnFitWidth.First(f => f.FieldName == i.FieldName).Min;
                                var getMax = _lstColumnFitWidth.First(f => f.FieldName == i.FieldName).Max;

                                DGV_Viw.Columns[i.FieldName].Width = getMin;
                                DGV_Viw.Columns[i.FieldName].MaxWidth = getMax;

                                if (DGV_Viw.Columns[i.FieldName].Width < getMin)
                                {
                                    DGV_Viw.Columns[i.FieldName].MinWidth = getMin;
                                }
                                else if (DGV_Viw.Columns[i.FieldName].Width > getMax)
                                {
                                    DGV_Viw.Columns[i.FieldName].Width = getMax;
                                }
                            }
                        }
                    }));
                }
                catch (Exception e)
                {
                    // ignored
                }
                //ShowCaptionHeader();
            }

            //));
        }
        public void OneSelectRow(RowCellClickEventArgs e)
        {
            //  DGV_Viw.OptionsSelection.MultiSelect = false;
            var getClm = e.Column.FieldName;
            // EnableMultiSelectColumn();
            if (getClm.ToLower() == "DX$CheckboxSelectorColumn".ToLower())
            {
                DGV_Viw.ClearSelection();
                new Task(() =>
                {
                    // Thread.Sleep(1);
                    Invoke(new Action(() => { DGV_Viw.SelectRow(DGV_Viw.FocusedRowHandle); }));

                }).Start();
            }
        }
        public void MultiSelectRow(RowCellClickEventArgs e)
        {
            // DGV_Viw.OptionsSelection.MultiSelect = true;
            var getClm = e.Column.FieldName;
            // EnableMultiSelectColumn();
            if (getClm.ToLower() == "DX$CheckboxSelectorColumn".ToLower())
            {
                //  DGV_Viw.ClearSelection();
                // new Task(() =>
                {
                    // Thread.Sleep(1000);
                    Invoke(new Action(() =>
                    {
                        var getSel = DGV_Viw.IsRowSelected(e.RowHandle);
                        if (getSel)
                        {
                            DGV_Viw.UnselectRow(e.RowHandle);
                        }
                        else
                        {
                            DGV_Viw.SelectRow(e.RowHandle);
                        }
                    }));

                }
                //).Start();
            }
        }
        public List<DataRow> GetSelectedRows()
        {
            List<DataRow> dataRows = new List<DataRow>();
            var getSelRow = DGV_Viw.GetSelectedRows();

            foreach (var i in getSelRow)
            {
                dataRows.Add(DGV_Viw.GetDataRow(i));
            }

            return dataRows;
        }
        public List<DataRow> GetSelectedRows(string clmName, bool value)
        {
            List<DataRow> dataRows = new List<DataRow>();

            for (int i = 0; i < DGV_Viw.RowCount; i++)
            {
                var getValue = DGV_Viw.GetRowCellValue(i, clmName).ToString().ToLower();
                if (getValue == value.ToString().ToLower())
                    dataRows.Add(DGV_Viw.GetDataRow(i));
            }
            return dataRows;
        }
        public DataRow OneCheckedRow(string clmCheck)
        {
            DGV_Viw.BeginDataUpdate();
            for (int i = 0; i < DGV_Viw.RowCount; i++)
            {
                SetValue(i, clmCheck, 0);
            }

            SetValue(DGV_Viw.FocusedRowHandle, clmCheck, 1);
            DGV_Viw.EndDataUpdate();
            var getRowSelect = DGV_Viw.GetDataRow(DGV_Viw.FocusedRowHandle);
            return getRowSelect;
        }
        public void MultiCheckedRow(string clmCheck)
        {
            DGV_Viw.BeginDataUpdate();

            //  List<DataRow> dataRows = new List<DataRow>();

            var getValue = GetValue<bool>(clmCheck);
            SetValue(DGV_Viw.FocusedRowHandle, clmCheck, !getValue);

            //for (int i = 0; i < DGV_Viw.RowCount; i++)
            //{
            //    getValue = GetValue<bool>(i, clmCheck);
            //    if (getValue)
            //        dataRows.Add(DGV_Viw.GetDataRow(i));
            //}
            DGV_Viw.EndDataUpdate();
            //  return dataRows;
        }
        public void SetCheckedRow(string clmCheck, bool value)
        {
            DGV_Viw.BeginDataUpdate();
            for (int i = 0; i < DGV_Viw.RowCount; i++)
            {
                SetValue(i, clmCheck, value ? 1 : 0);
            }
            DGV_Viw.EndDataUpdate();
        }
        public void ChangeColumnPosition(string clm, short position)
        {
            DGV_Viw.Columns[clm].ColVIndex = position;
        }
        public void ChangeColumnPosition(GridColumn clm, short position)
        {
            DGV_Viw.Columns[clm.FieldName].ColVIndex = position;
        }
        //private void DGV_Viw_DoubleClick(object sender, EventArgs e)
        //{
        //    //var getRowHandle = DGV_Viw.FocusedRowHandle;
        //    //var statusExpanded = DGV_Viw.GetMasterRowExpanded(getRowHandle);

        //    //if (statusExpanded)
        //    //    DGV_Viw.CollapseMasterRow(getRowHandle);
        //    //else
        //    //    DGV_Viw.ExpandMasterRow(getRowHandle);
        //    // SetFieldSizeColumn();
        //}
        private void v(object sender, EventArgs e)
        {
            // MessageBox.Show("ff");
        }
        private void DGV_Viw_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            e.Info.AllowColoring = true;
            e.Info.AllowEffects = true;
        }
        private void DGV_Viw_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void DGV_Viw_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift)
            {
                if (e.Control && e.KeyCode == Keys.Q)
                {
                    var focuseClm = DGV_Viw.FocusedColumn;
                    var gg = DGV_Viw.GetFocusedRowCellValue(focuseClm.FieldName);

                    DGV_Viw.ActiveFilterString = "([" + focuseClm.FieldName + "] = '" + gg + "')";

                    //  MessageBox.Show(gg.ToString());
                }
            }
            else
            {
                if (e.Control && e.KeyCode == Keys.Q)
                {
                    var focuseClm = DGV_Viw.FocusedColumn;
                    var gg = DGV_Viw.GetFocusedRowCellValue(focuseClm.FieldName);

                    // MessageBox.Show(DGV_Viw.ActiveFilterString.Replace(")", "").Replace("(", ""));

                    DGV_Viw.ActiveFilterString =
                        "(" + DGV_Viw.ActiveFilterString.Replace(")", "").Replace("(", "") + " and [" + focuseClm.FieldName + "] = '" + gg + "')";
                }

                //  MessageBox.Show(gg.ToString());
            }
        }
        private void DGV_Viw_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            e.Column.AppearanceHeader.BackColor = Color.FromArgb(255, 191, 207, 240);
            e.Column.AppearanceHeader.BackColor2 = Color.FromArgb(255, 202, 210, 226);

            if (DGV_Viw.RowCount > 0)
            {
                if (e.Column.FieldName == "ردیف" || e.Column.FieldName == "Row")
                {
                    e.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DGV_Viw_MouseDown(object sender, MouseEventArgs e)
        {


        }

        SimpleButton btnRefresh = new SimpleButton { Name = "btnRefresh", Text = @"Refresh", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Refresh_25 };
        SimpleButton btnExcel = new SimpleButton { Name = "btnExcel", Text = @"Excel", Dock = DockStyle.Left, Width = 90, Image = Properties.Resources.excel_24 };
        SimpleButton btnSave = new SimpleButton { Name = "btnSave", Text = @"Save", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Save_3_23 };
        SimpleButton btnCancel = new SimpleButton { Name = "btnCancel", Text = @"Cancel", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Delete_25 };
        SimpleButton btnAdd = new SimpleButton { Name = "btnAdd", Text = @"Add", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Add_25 };
        SimpleButton btnCustom = new SimpleButton { Name = "btnCustom", Text = @"Custom", Dock = DockStyle.Left, Width = 90 };

        public enum modelBtn
        {
            Refresh,
            Excel,
            Save,
            Cancel,
            Add,
            Custom,
        }
        public SimpleButton AddBtnPanelGrid(modelBtn modelBtn)
        {
            switch (modelBtn)
            {
                case modelBtn.Refresh:
                    _font.ChangeFont(btnRefresh);
                    AddButtonOrControlUpGrid(btnRefresh);
                    return btnRefresh;

                case modelBtn.Excel:
                    _font.ChangeFont(btnExcel);
                    AddButtonOrControlUpGrid(btnExcel);
                    return btnExcel;

                case modelBtn.Save:
                    _font.ChangeFont(btnSave);
                    AddButtonOrControlUpGrid(btnSave);
                    return btnSave;

                case modelBtn.Cancel:
                    _font.ChangeFont(btnCancel);
                    AddButtonOrControlUpGrid(btnCancel);
                    return btnCancel;

                case modelBtn.Add:
                    _font.ChangeFont(btnAdd);
                    AddButtonOrControlUpGrid(btnAdd);
                    return btnAdd;

                case modelBtn.Custom:
                    _font.ChangeFont(btnCustom);
                    AddButtonOrControlUpGrid(btnCustom);
                    return btnCustom;
            }
            return null;
        }

        //public void ShowCaptionHeader()
        //{
        //    DGV.Invoke(new Action(() =>
        //    {
        //        pnlViewHeader.Controls.Clear();

        //        // var getScroll = DGV_Viw.LeftCoord;
        //        pnlViewHeader.Padding = DGV_Viw.OptionsSelection.MultiSelect
        //            ? new System.Windows.Forms.Padding(17 + DGV_Viw.OptionsSelection.CheckBoxSelectorColumnWidth, 0, 0, 0)
        //            : new System.Windows.Forms.Padding(17, 0, 0, 0);

        //        foreach (BandedGridColumn c in DGV_Viw.Columns)
        //        {
        //            if (c.Visible)
        //            {
        //                //  SimpleButton btn = new SimpleButton { Dock = DockStyle.Left, Width = c.Width, Text = c.FieldName,BorderStyle = BorderStyles.Flat};
        //                var lbl = new LabelControl
        //                {
        //                    Dock = DockStyle.Left,
        //                    Text = c.ToolTip,
        //                    //Text = c.FieldName,
        //                    AutoSizeMode = LabelAutoSizeMode.None,
        //                    BorderStyle = BorderStyles.Simple,
        //                    Font = _font.ChangeFont(11.5F),
        //                    Appearance =
        //                    {
        //                        BackColor = Color.FromArgb(120, 190, 197, 247),
        //                        BackColor2 = Color.FromArgb(157, 190, 197, 247),
        //                        TextOptions = {VAlignment = VertAlignment.Center, HAlignment = HorzAlignment.Center}
        //                    }
        //                };
        //                lbl.Width = c.Width;
        //                pnlViewHeader.Controls.Add(lbl);
        //                lbl.BringToFront();
        //            }
        //        }
        //    }));
        //    pnlViewHeader.Invoke(new Action(() =>
        //    {
        //        pnlViewHeader.AutoScrollPosition = new Point(0, 0);
        //        pnlViewHeader.AutoScrollOffset = new Point(0, 0);
        //    }));

        //}

        public class modelFilterChangeCellStyle
        {
            public string Expression { get; set; }
            public Color Color2 { get; set; }
            public bool RowStyle { get; set; }

            public modelFilterChangeCellStyle()
            {
                RowStyle = false;
            }
        }
        public void SetFilterChangeCellStyle(string clm, List<modelFilterChangeCellStyle> value)
        {
            // dgvTable.DGV_Viw.FormatRules.Clear();
            for (var i = 0; i < value.Count; i++)
            {
                var s = value[i];
                var gClm = GetColumn(clm);
                GridFormatRule gfr = new GridFormatRule { Column = gClm, Name = gClm.Name + i, ApplyToRow = s.RowStyle };
                FormatConditionRuleExpression fcre = new FormatConditionRuleExpression();
                fcre.Appearance.BackColor = s.Color2;
                fcre.Appearance.Options.UseBackColor = true;
                fcre.Expression = s.Expression;
                gfr.Rule = fcre;
                DGV_Viw.FormatRules.Add(gfr);
            }
        }

        #region GridStructure New 2021

        public class modelColumn
        {
            public string Name { get; set; }
            public string Caption { get; set; }

            /// <summary>
            /// int, float, string, ... & Null
            /// </summary>
            public Type Type { get; set; }

            /// <summary>
            /// عددی ، غیر عددی
            /// </summary>
            public FormatType FormatType { get; set; }

            /// <summary>
            /// Type = Null
            /// </summary>
            public enumObject Object { get; set; }

            public bool PriceActive { get; set; }
            /// <summary>
            /// تعداد اعشار
            /// </summary>
            public short CountFloat { get; set; }

            public string FormatString { get; set; }
            /// <summary>
            /// Image OR SvgImage
            /// </summary>  
            public object ImageValue { get; set; }
            public bool RightToLeft { get; set; }

            public modelColumn()
            {
                Caption = "";
                FormatType = FormatType.None;
                Object = enumObject.Default;
                FormatString = null;
                PriceActive = false;
                ImageValue = null;
                CountFloat = 0;
                RightToLeft = false;
            }
        }

        public enum enumObject
        {
            Default, // 0
            Progress, // 1
            TextMemo, // 2
            Checked, // 3
            OnOff, // 4
            DateMask, // 5  
            Rating, // 6
            Volume, // 7
            Time, // 8
            CarPelak, //9
            Button, // 10    
            Link, // 11
            Picture // 9
        }

        public void GridStructure(DataTable dt, List<modelColumn> column, List<modelButtonImage> _imgButton, bool edit, bool sort, bool showFooter)
        {
            #region لایسنس

            //  if (Lic1(column)) return;

            #endregion

            this.DGV_Viw.BeginUpdate();
            DGV_Viw.CustomDrawCell += (s, e) =>
            {
                var getClm = e.Column.FieldName;
                foreach (var c in column)
                {
                    if (c.Name == getClm && c.RightToLeft)
                    {
                        var getForeColor = e.Appearance.ForeColor;
                        //   StringFormatInfo sfi = new StringFormatInfo(new StringFormat(StringFormatFlags.DirectionRightToLeft));
                        StringFormatInfo sfi = new StringFormatInfo(new StringFormat(StringFormatFlags.DirectionRightToLeft));
                        e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds, getForeColor, sfi);
                        e.Handled = true;
                    }
                }
            };

            DGV_Viw.OptionsView.ShowBands = false;

            dt.Clear();
            dt.Columns.Clear();
            DGV.DataSource = null;
            DGV_Viw.Columns.Clear();

            #region(افزودن افزونه های عملیاتی)

            if (_imgButton != null)
                foreach (var t in _imgButton)
                {
                    dt.Columns.Add(t.Name);
                }

            #endregion

            for (var i = 0; i < column.Count; i++)
            {
                if (column[i].Object == enumObject.Checked)
                    column[i].Type = typeof(bool);

                if (column[i].Type != null)
                    dt.Columns.Add(column[i].Name, column[i].Type);
                else
                    dt.Columns.Add(column[i].Name);
            }

            DGV.DataSource = dt;
            // this.DGV_Viw.EndUpdate();
            // this.DGV_Viw.BeginUpdate();
            // _data.DGV.DataSource = null;

            for (var i = 0; i < column.Count; i++)
            {
                var nc = column[i].Name;
                try
                {
                    DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw.Columns[nc].OptionsColumn.AllowSize = true;
                    DGV_Viw.Columns[nc].OptionsColumn.AllowEdit = edit;
                    DGV_Viw.OptionsBehavior.Editable = edit;
                    DGV_Viw.Columns[nc].OptionsColumn.AllowSort = sort ? DefaultBoolean.True : DefaultBoolean.False;
                    DGV_Viw.Columns[nc].ToolTip = column[i].Caption;
                    // DGV_Viw.Columns[nc].AppearanceCell.Image = Properties.Resources.Accept_25;
                    // DGV_Viw.Columns[nc].AppearanceCell.TextOptions.

                    // ContextImageOptions = { Image = t.Image },

                    // clm = NC.Replace("btntext=", "");
                    if (column[i].ImageValue != null && column[i].Object == enumObject.Default)
                    {
                        var addButton = new RepositoryItemTextEdit();
                        if (column[i].ImageValue != null)
                        {
                            addButton.Appearance.TextOptions.RightToLeft = true;
                            if (column[i].ImageValue is Image image)
                                addButton.ContextImageOptions.Image = image;
                            if (column[i].ImageValue is Byte[] svgImage)
                            {
                                addButton.ContextImageOptions.SvgImage = svgImage;
                                addButton.ContextImageOptions.SvgImageSize = new Size(19, 19);
                            }
                        }

                        DGV.RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;
                        // _font.ChangeFont(DGV_Viw.Columns[clm], sizeF);
                    }
                }

                catch
                {
                    // ignored
                }

                if (column[i].PriceActive || column[i].FormatType == FormatType.Numeric)
                {
                    DGV_Viw.Columns[nc].DisplayFormat.FormatType = FormatType.Numeric;
                    this.FormatStringNegativeNumber(nc, column[i].CountFloat);
                }
                else if (column[i].FormatType == FormatType.Custom)
                {
                    DGV_Viw.Columns[nc].DisplayFormat.FormatType = FormatType.Custom;

                    if (column[i].FormatString != null)
                    {
                        DGV_Viw.Columns[nc].DisplayFormat.FormatString = column[i].FormatString;

                    }

                }
                else if (column[i].FormatType == FormatType.None)
                {
                    if (column[i].Object == enumObject.Progress) // جدید پیشرفت کار
                    {
                        var _progressBar = new RepositoryItemProgressBar { Name = nc, };

                        _font.ChangeFont(_progressBar, 14);

                        _progressBar.LookAndFeel.UseDefaultLookAndFeel = false;
                        _progressBar.ShowTitle = true;
                        _progressBar.LookAndFeel.SkinName = "Valentine";
                        _progressBar.LookAndFeel.SkinMaskColor = Color.Lime;

                        DGV.RepositoryItems.Add(_progressBar);
                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = _progressBar;

                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 100;
                        DGV_Viw.Columns[nc].MinWidth = 60;
                    }
                    else if (column[i].Object == enumObject.TextMemo) // جدید چند خطی
                    {
                        var _memoEdit = new RepositoryItemMemoEdit
                        {
                            Name = nc,
                            ContextImageOptions = { }

                        };

                        if (column[i].ImageValue != null)
                        {
                            if (column[i].ImageValue is Image image)
                                _memoEdit.ContextImageOptions.Image = image;
                            if (column[i].ImageValue is byte[] svgImage)
                            {
                                _memoEdit.ContextImageOptions.SvgImage = svgImage;
                                _memoEdit.ContextImageOptions.SvgImageSize = new Size(20, 20);
                            }
                        }
                        _memoEdit.Appearance.TextOptions.RightToLeft = true;

                        DGV.RepositoryItems.Add(_memoEdit);
                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.RightToLeft = true;
                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                        //DGV_Viw.Columns[nc].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.RightToLeft = true;


                        DGV_Viw.Columns[nc].ColumnEdit = _memoEdit;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 550;
                        DGV_Viw.Columns[nc].MinWidth = 150;

                        _memoEdit.Appearance.Font = _font.ChangeFont();
                        _memoEdit.Appearance.ForeColor = Color.Black;
                        _memoEdit.AppearanceFocused.Font = _font.ChangeFont();
                        _memoEdit.AppearanceReadOnly.Font = _font.ChangeFont();
                        _memoEdit.AppearanceDisabled.Font = _font.ChangeFont();
                        _memoEdit.AppearanceFocused.ForeColor = Color.FromArgb(255, 94, 0);

                        //_font.ChangeFont(_memoEdit, 10);
                    }
                    else if (column[i].Object == enumObject.OnOff)
                    {
                        var _switch = new RepositoryItemToggleSwitch
                        {
                            Name = nc,
                            ShowText = true,
                            OnText = @"تایید",
                            OffText = @"عدم تایید",
                            ValueOn = "1",
                            ValueOff = "0",

                        };
                        // _switch.LookAndFeel.SkinMaskColor = Color.Lime;
                        _switch.LookAndFeel.UseDefaultLookAndFeel = false;
                        //   RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                        _switch.LookAndFeel.SkinName = "Office 2019 Colorful";
                        // _switch.LookAndFeel.SkinName = "Office 2013 Light Gray";
                        //  ComboBox.LookAndFeel.SkinName = "Office 2013";


                        DGV.RepositoryItems.Add(_switch);

                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                        DGV_Viw.Columns[nc].ColumnEdit = _switch;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 195;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.DateMask)
                    {
                        var maskData = new RepositoryItemTextEdit
                        {
                            Name = nc,
                            Mask = { EditMask = @"___/__/__", MaskType = MaskType.DateTime, UseMaskAsDisplayFormat = true },
                            ContextImageOptions =
                            {
                                //Image = column[i].ImageValue
                            }
                            //  riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the 

                        };


                        if (column[i].ImageValue != null)
                        {
                            if (column[i].ImageValue is Image image)
                                maskData.ContextImageOptions.Image = image;
                            if (column[i].ImageValue is byte[] svgImage)
                            {
                                maskData.ContextImageOptions.SvgImage = svgImage;
                                maskData.ContextImageOptions.SvgImageSize = new Size(20, 20);
                            }
                        }

                        maskData.LookAndFeel.SkinName = "Office 2013";
                        maskData.LookAndFeel.UseDefaultLookAndFeel = false;

                        DGV.RepositoryItems.Add(maskData);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = maskData;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;

                    }
                    else if (column[i].Object == enumObject.Rating)
                    {
                        var _rating = new RepositoryItemRatingControl { Name = nc, ShowText = true, ValueInterval = 0, };

                        // _rating.ItemClick += _rating_ItemClick;
                        // _rating.Click += _rating_Click;
                        // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                        // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                        _rating.LookAndFeel.SkinName = "Caramel";
                        // _rating.LookAndFeel.SkinName = "Summer 2008";
                        _rating.LookAndFeel.UseDefaultLookAndFeel = false;

                        DGV.RepositoryItems.Add(_rating);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                        DGV_Viw.Columns[nc].ColumnEdit = _rating;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 95;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.Checked)
                    {
                        var _bool = new RepositoryItemCheckEdit
                        {
                            Name = nc,

                            CheckBoxOptions = { Style = CheckBoxStyle.Custom },
                            ImageOptions =
                            {
                                SvgImageChecked = Properties.Resources.Check_New_2, SvgImageUnchecked = Properties.Resources.UnCheck_New_2, SvgImageSize = new Size(20, 20)
                            }

                            //// ValueChecked = CheckState.Checked
                            // ValueChecked = 0,
                            // ValueUnchecked = 1
                        };
                        //// _bool.AllowGrayed = true;
                        //// _bool.CheckStyle = CheckStyles.Standard;

                        //   _bool.LookAndFeel.SkinName = "Whiteprint";
                        //  _bool.LookAndFeel.SkinName = "Glass Oceans";
                        // _bool.LookAndFeel.SkinName = "Caramel";
                        // _bool.LookAndFeel.SkinName = "Summer 2008";
                        //  _bool.LookAndFeel.UseDefaultLookAndFeel = false;

                        DGV.RepositoryItems.Add(_bool);
                        // _bool.CheckStyle = CheckStyles.Standard;

                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = _bool;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 95;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.Volume)
                    {
                        var _trackBar = new RepositoryItemTrackBar { Name = nc, Minimum = 0, Maximum = 100, };

                        _trackBar.ShowLabels = true;
                        _trackBar.ShowLabelsForHiddenTicks = true;
                        _trackBar.ShowValueToolTip = true;

                        //  if (_trackBar.Labels.Count == 0)
                        // _trackBar.Labels.Add(new TrackBarLabel { Value = 5});
                        // _trackBar.LookAndFeel.SkinName = "Glass Oceans";
                        _trackBar.LookAndFeel.SkinName = "Caramel";
                        //  _bool.LookAndFeel.SkinName = "Summer 2008";
                        _trackBar.LookAndFeel.UseDefaultLookAndFeel = false;

                        DGV.RepositoryItems.Add(_trackBar);
                        // _bool.CheckStyle = CheckStyles.Standard;

                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = _trackBar;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 95;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.Time)
                    {
                        var timeEdit = new RepositoryItemTimeEdit
                        {
                            Name = nc,
                            TimeEditStyle = TimeEditStyle.TouchUI,
                            BeepOnError = true,
                            ShowDropDown = ShowDropDown.DoubleClick,
                            AllowFocused = true,
                            //  ContextImageOptions = { Image = column[i].ImageValue },
                        };

                        if (column[i].ImageValue != null)
                        {
                            if (column[i].ImageValue is Image image)
                                timeEdit.ContextImageOptions.Image = image;
                            if (column[i].ImageValue is byte[] svgImage)
                            {
                                timeEdit.ContextImageOptions.SvgImage = svgImage;
                                timeEdit.ContextImageOptions.SvgImageSize = new Size(20, 20);
                            }
                        }

                        timeEdit.AdvancedModeOptions.AllowCaretAnimation = DefaultBoolean.True;
                        timeEdit.AdvancedModeOptions.AllowSelectionAnimation = DefaultBoolean.True;
                        timeEdit.MaskSettings.Set("mask", "HH:mm");

                        timeEdit.UseMaskAsDisplayFormat = true;

                        // timeEdit.Buttons.AddRange(new[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
                        // timeEdit.LookAndFeel.SkinName = "Caramel";
                        // timeEdit.LookAndFeel.UseDefaultLookAndFeel = false;

                        DGV.RepositoryItems.Add(timeEdit);

                        DGV_Viw.Columns[nc].DisplayFormat.FormatString = "HH:mm";

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = timeEdit;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 65;
                        DGV_Viw.Columns[nc].MinWidth = 65;
                    }
                    else if (column[i].Object == enumObject.CarPelak)
                    {
                        var maskData = new RepositoryItemTextEdit
                        {
                            Name = nc,
                            Mask =
                            {
                                EditMask = @"([1-9][0-9][ا-ی][0-9][0-9][0-9]( ایران )[1-9][0-9])",
                                MaskType = MaskType.RegEx,
                                UseMaskAsDisplayFormat = true
                            },
                        };
                        if (column[i].ImageValue != null)
                        {
                            if (column[i].ImageValue is Image image)
                                maskData.ContextImageOptions.Image = image;
                            if (column[i].ImageValue is byte[] svgImage)
                            {
                                maskData.ContextImageOptions.SvgImage = svgImage;
                                maskData.ContextImageOptions.SvgImageSize = new Size(20, 20);
                            }
                        }

                        maskData.LookAndFeel.SkinName = "Office 2013";
                        maskData.LookAndFeel.UseDefaultLookAndFeel = false;

                        DGV.RepositoryItems.Add(maskData);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = maskData;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                    }
                    else if (column[i].Object == enumObject.Button)
                    {
                        SvgImage getSvg;

                        using (var memstr = new MemoryStream((byte[])column[i].ImageValue))
                        {
                            getSvg = SvgImage.FromStream(memstr);
                        }

                        var addButton = new RepositoryItemHyperLinkEdit
                        {
                            Name = nc,
                            ContextImageOptions = { SvgImage = getSvg, SvgImageSize = new Size(18, 18) },
                            ImageAlignment = HorzAlignment.Center,
                        };

                        DGV.RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;

                        // _font.ChangeFont(DGV_Viw.Columns[nc], sizeF);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].Width = 26;
                        DGV_Viw.Columns[nc].MaxWidth = 26;
                        DGV_Viw.Columns[nc].MinWidth = 26;
                        MaxMinWidth(nc, 26, 26);

                        DGV_Viw.Columns[nc].OptionsFilter.AllowFilter = false;
                        DGV_Viw.Columns[nc].OptionsFilter.AllowAutoFilter = false;
                        DGV_Viw.Columns[nc].OptionsColumn.FixedWidth = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSize = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSort = DefaultBoolean.False;
                    }
                    else if (column[i].Object == enumObject.Link)
                    {
                        var addButton = new RepositoryItemHyperLinkEdit
                        {
                            //column[i].ImageValue
                            Name = nc,
                            Caption = column[i].Name
                        };

                        DGV.RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;

                        // _font.ChangeFont(DGV_Viw.Columns[nc], sizeF);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].Width = 60;
                        DGV_Viw.Columns[nc].MaxWidth = 60;
                        DGV_Viw.Columns[nc].MinWidth = 60;
                        //  MaxMinWidth(nc, 60, 60);

                        DGV_Viw.Columns[nc].OptionsFilter.AllowFilter = false;
                        DGV_Viw.Columns[nc].OptionsFilter.AllowAutoFilter = false;
                        DGV_Viw.Columns[nc].OptionsColumn.FixedWidth = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSize = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSort = DefaultBoolean.False;
                    }
                    else if (column[i].Object == enumObject.Picture)
                    {
                        var addPicture = new RepositoryItemPictureEdit()
                        {
                            Name = nc,

                        };
                    }

                }
                _font.ChangeFont(chkFind);
            }

            for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            {
                DGV_Viw.Columns[i].VisibleIndex = i;
            }

            #region New

            if (_imgButton != null)
                foreach (var t in _imgButton)
                {
                    //var addButton2 = new RepositoryItemButtonEdit();
                    //{

                    //};
                    //RepositoryItemHypertextLabel
                    var addButton = new RepositoryItemButtonEdit
                    {
                        Name = t.Name,
                        LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2019 Colorful" },
                        Appearance = { TextOptions = { HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center, } },
                        // ContextImageOptions = { Image = t.Image },
                    };
                    addButton.ContextImage = t.Image;
                    //  addButton.ContextImageOptions.Image = t.Image;
                    //  addButton.Appearance.TextOptions./*HAlignment = HorzAlignment.Center;
                    //  addButton.Appearance.TextOptions.VAlignment = VertAlignment.Center;


                    //   addButton.Buttons[0].Image = t.Image;

                    //---------------
                    DGV.RepositoryItems.Add(addButton);
                    DGV_Viw.Columns[t.Name].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw.Columns[t.Name].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    DGV_Viw.Columns[t.Name].ColumnEdit = addButton;
                    DGV_Viw.Columns[t.Name].OptionsColumn.Printable = DefaultBoolean.False;
                    DGV_Viw.Columns[t.Name].MaxWidth = 50;
                    DGV_Viw.Columns[t.Name].MinWidth = 50;
                    MaxMinWidth(t.Name, 50, 50);
                    DGV_Viw.Columns[t.Name].OptionsFilter.AllowFilter = false;
                    DGV_Viw.Columns[t.Name].OptionsFilter.AllowAutoFilter = false;
                    DGV_Viw.Columns[t.Name].OptionsColumn.FixedWidth = false;
                    DGV_Viw.Columns[t.Name].OptionsColumn.AllowSize = false;

                    _font.ChangeFont(DGV_Viw.Columns[t.Name]);
                }

            DGV_Viw.OptionsView.ShowFooter = showFooter;

            #endregion

            this.DGV_Viw.EndUpdate();
            SetFieldSizeColumn();

            //foreach (modelColumn c in column)
            //{
            //    if (c.FormatType == FormatType.Numeric)
            //    {
            //        DGV_Viw.Columns[c.Name].DisplayFormat.FormatType = FormatType.Numeric;

            //        if (c.FormatString != null)
            //            if (c.PriceActive)
            //            {
            //                this.FormatStringNegativeNumber(c.Name, 3);
            //            }
            //            else
            //            {

            //                this.FormatStringNegativeNumber(c.Name, 3);
            //            }

            //    }
            //    else if (c.FormatType == FormatType.Custom)
            //    {
            //        DGV_Viw.Columns[c.Name].DisplayFormat.FormatType = FormatType.Custom;

            //        if (c.FormatString != null)
            //            if (c.PriceActive)
            //            {
            //                this.FormatStringNegativeNumber(c.Name, 3);
            //            }
            //            else
            //            {
            //                this.FormatStringNegativeNumber(c.Name, 3);
            //            }

            //    }
            //}
        }



        //controls[0].Click += null;
        #endregion

        private void pnlButton_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlViewHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}