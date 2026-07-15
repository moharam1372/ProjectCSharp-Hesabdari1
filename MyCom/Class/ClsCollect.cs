using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Spreadsheet;
using DevExpress.UserSkins;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraWaitForm;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using MyCom.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FindMode = DevExpress.XtraEditors.FindMode;
using FixedStyle = DevExpress.XtraGrid.Columns.FixedStyle;
using ImageLocation = DevExpress.XtraEditors.ImageLocation;
using Label = System.Windows.Forms.Label;
using Padding = System.Windows.Forms.Padding;
using ShowFilterPanelMode = DevExpress.XtraVerticalGrid.ShowFilterPanelMode;

//using System.Windows;

namespace MyCom.Class
{
    public static class ClsCollect
    {

        static readonly ClsFont _font = new ClsFont(ClsFont.enumFont.samimFD);
        static readonly ClsFont _fontBold = new ClsFont(ClsFont.enumFont.samimFD, true);
        private static readonly Class_Text _classText = new Class_Text();

        private static void GridComboEdit_Enter(object sender, EventArgs e)
        {
            var edit = sender as GridLookUpEdit;
            edit.ShowPopup();
            //  BeginInvoke(new Action(() => ));
        }

        private static void GridComboEdit_Click(object sender, EventArgs e)
        {
            var getEdit = sender as GridLookUpEdit;
            getEdit.ShowPopup();
        }

        private static void View_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            GridCellInfo ci = e.Cell as GridCellInfo;
            if (!string.IsNullOrEmpty(ci.ViewInfo.MatchedString))
            {
                // ci.Appearance.BackColor = Color.Red;
                // ci.Appearance.ForeColor = Color.Red;
                // ci.ViewInfo.DefaultAppearance.ForeColor = Color.Red;
                //   ci.ViewInfo.MatchedString ="مینی";
                //   ci.ViewInfo.AllowHtmlString = true;
                //ci.ViewInfo.ErrorIconAlignment
                //ci.Appearance.Options.
            }
            //ci.Appearance.BackColor = gridLookUpEdit1View.Appearance.FocusedRow.BackColor;
            // if (e.DisplayText.ToLower().StartsWith(gridLookUpEdit1.AutoSearchText.ToLower()))

            // ci.ViewInfo.MatchedString = gridLookUpEdit1.AutoSearchText;
        }

        public static RepositoryItemGridLookUpEdit AddGridToGrid2<EF>(this Data_Grid dgv, List<EF> lEF,
            string nameRelationColumn, string valueMember, string displayMember,
            TextEditStyles _styles = TextEditStyles.Standard, string nullText = "", Image image = null)
        {
            //  dtTest.Clear();

            var gridComboEdit = new RepositoryItemGridLookUpEdit
            {
                ValueMember = valueMember,
                DisplayMember = displayMember,
                NullText = nullText,
                ContextImageOptions = { Image = image },

            };

            // var dtTest = litEFst;

            dgv.DGV_Viw.Columns[nameRelationColumn].ColumnEdit = gridComboEdit;
            gridComboEdit.DataSource = lEF;

            _font.ChangeFont(gridComboEdit, 13);

            gridComboEdit.View.BestFitColumns(false);

            // gridComboEdit.View.OptionsFind.AlwaysVisible = true;

            gridComboEdit.View.OptionsView.ShowAutoFilterRow = true;
            gridComboEdit.View.OptionsFind.SearchInPreview = true;
            gridComboEdit.View.OptionsFind.AllowFindPanel = true;
            gridComboEdit.View.OptionsFind.FindMode = FindMode.Always;
            gridComboEdit.AllowNullInput = DefaultBoolean.True;

            //

            gridComboEdit.View.OptionsFilter.AllowAutoFilterConditionChange = DefaultBoolean.True;
            gridComboEdit.View.OptionsFilter.AllowFilterEditor = true;
            gridComboEdit.View.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            gridComboEdit.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.ImmediatePopup = true;
            gridComboEdit.TextEditStyle = _styles;
            // gridComboEdit.TextEditStyle = TextEditStyles.Standard;
            gridComboEdit.PopulateViewColumns();
            gridComboEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            gridComboEdit.View.RowStyle += EventRowStyle_RowStyle;
            //  gridComboEdit.View.CellValueChanged += View_CellValueChanged; 

            gridComboEdit.ProcessNewValue += (s, e) =>
            {
                try
                {
                    if (e.DisplayValue == null)
                    {
                        ((GridLookUpEdit)s).EditValue = null;
                        e.Handled = true;
                    }
                }
                catch (Exception exception)
                {
                    // ignored
                }
            };
            gridComboEdit.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    SendKeys.Send("{BACKSPACE}");
                }
            };
            //gridComboEdit.CustomDisplayText += (s, e) =>
            //{
            //    // System.Console.WriteLine(e.Value);
            //    var get = Convert.ToString(e.Value);
            //    if (get == nullText && nullText != "")
            //        gridComboEdit.Appearance.ForeColor = Color.FromArgb(183, 183, 183);
            //    else
            //        gridComboEdit.Appearance.ForeColor = Color.FromArgb(16, 1, 1);

            //};
            return gridComboEdit;
        }

        private static void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {

        }

        #region Title Grid Control

        public static void CreatDataTable(this GridControl grid, TileView view, DataTable dataTable)
        {
            dataTable.Clear();
            dataTable.Columns.Clear();
            foreach (GridColumn column in view.Columns)
            {
                Type convertType = typeof(object);
                switch (column.UnboundType)
                {
                    case UnboundColumnType.Integer:
                        convertType = typeof(int);
                        break;
                    case UnboundColumnType.Decimal:
                        convertType = typeof(decimal);
                        break;
                    case UnboundColumnType.DateTime:
                        convertType = typeof(DateTime);
                        break;
                    case UnboundColumnType.String:
                        convertType = typeof(string);
                        break;
                    case UnboundColumnType.Boolean:
                        convertType = typeof(bool);
                        break;
                }

                dataTable.Columns.Add(column.Name, convertType);
            }

            grid.DataSource = dataTable;
        }

        public class ModelTitleView
        {
            public string Name { get; set; }
            public Type Type { get; set; }
            public string Format { get; set; }
        }
        public static void CreatDataTable(this GridControl grid, TileView view, DataTable dataTable, List<ModelTitleView> lstColumn)
        {
            grid.BeginUpdate();
            // 
            dataTable.Clear();
            dataTable.Columns.Clear();
            //view.Columns.Clear();

            for (var i = 0; i < lstColumn.Count; i++)
            {
                if (lstColumn[i].Name.Contains("="))
                {
                    var indexOff = lstColumn[i].Name.IndexOf("=", StringComparison.Ordinal);
                    var ggg = lstColumn[i].Name.Substring(0, indexOff).ToLower();

                    // Type _type = null;

                    //dataTable.Columns.Add(lstColumn[i].Name, lstColumn[i].Type);

                    switch (ggg.ToLower())
                    {
                        case "progress":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("progress=", ""));
                            break;

                        case "maskdate":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("maskdate=", ""));
                            break;

                        case "onoff":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("onoff=", ""));
                            break;

                        case "rating":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("rating=", ""));
                            break;

                        case "memo":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("memo=", ""));
                            break;

                        case "checked":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("checked=", ""), typeof(bool));
                            break;

                        case "volume":
                            dataTable.Columns.Add(lstColumn[i].Name.Replace("volume=", ""));
                            break;
                    }
                }
                else
                    dataTable.Columns.Add(lstColumn[i].Name, lstColumn[i].Type);
            }

            //  _data.DGV.DataSource = null;
            grid.DataSource = dataTable;

            for (var i = 0; i < lstColumn.Count; i++)
            {
                if (lstColumn[i].Format != null)
                    view.Columns[lstColumn[i].Name].DisplayFormat.FormatType = FormatType.Numeric;
            }

            for (var i = 0; i < lstColumn.Count; i++)
            {
                var nc = lstColumn[i].Name;

                if (nc.ToLower().Contains("progress")) // جدید پیشرفت کار
                {
                    lstColumn[i].Name = nc.Replace("progress=", "");
                    var clm = lstColumn[i].Name;
                    var progressBar = new RepositoryItemProgressBar
                    {
                        Name = clm
                    };

                    _font.ChangeFont(progressBar, 14);

                    grid.RepositoryItems.Add(progressBar);

                    progressBar.LookAndFeel.UseDefaultLookAndFeel = false;
                    progressBar.ShowTitle = true;
                    progressBar.LookAndFeel.SkinName = "Valentine";
                    progressBar.LookAndFeel.SkinMaskColor = Color.Lime;

                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    view.Columns[clm].ColumnEdit = progressBar;

                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    view.Columns[clm].MaxWidth = 100;
                    view.Columns[clm].MinWidth = 60;
                }
                else if (nc.ToLower().Contains("memo")) // جدید چند خطی
                {
                    lstColumn[i].Name = nc.Replace("memo=", "");
                    var clm = lstColumn[i].Name;
                    var memoEdit = new RepositoryItemMemoEdit
                    {
                        Name = clm,
                    };
                    grid.RepositoryItems.Add(memoEdit);
                    memoEdit.Appearance.TextOptions.RightToLeft = true;


                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment
                         = HorzAlignment.Center;

                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment =
                         HorzAlignment.Near;
                    view.Columns[clm].AppearanceCell.TextOptions.VAlignment =
                         VertAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions
                         .RightToLeft = true;

                    view.Columns[clm].ColumnEdit = memoEdit;

                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    view.Columns[clm].MaxWidth = 550;
                    view.Columns[clm].MinWidth = 150;

                    memoEdit.Appearance.Font = _font.ChangeFont();
                    memoEdit.Appearance.ForeColor = Color.Black;
                    memoEdit.AppearanceFocused.Font = _font.ChangeFont();
                    memoEdit.AppearanceReadOnly.Font = _font.ChangeFont();
                    memoEdit.AppearanceDisabled.Font = _font.ChangeFont();
                    memoEdit.AppearanceFocused.ForeColor = Color.FromArgb(255, 94, 0);

                    //_font.ChangeFont(_memoEdit, 10);
                }

                else if (nc.ToLower().Contains("onoff"))
                {
                    lstColumn[i].Name = nc.Replace("onoff=", "");
                    var clm = lstColumn[i].Name;
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


                    grid.RepositoryItems.Add(_switch);

                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment
                        = HorzAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment =
                        HorzAlignment.Near;

                    view.Columns[clm].ColumnEdit = _switch;
                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    view.Columns[clm].MaxWidth = 195;
                    view.Columns[clm].MinWidth = 95;
                }
                else if (nc.ToLower().Contains("maskdate"))
                {
                    lstColumn[i].Name = nc.ToLower().Replace("maskdate=", "");
                    var clm = lstColumn[i].Name;
                    var maskData = new RepositoryItemTextEdit
                    {
                        Name = clm,
                        Mask =
                        {
                            EditMask = @"___/__/__",
                            MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime,
                            UseMaskAsDisplayFormat = true

                        },

                        //  riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the 

                    };

                    maskData.LookAndFeel.SkinName = "Office 2013";
                    maskData.LookAndFeel.UseDefaultLookAndFeel = false;

                    grid.RepositoryItems.Add(maskData);

                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment
                        = HorzAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment =
                        HorzAlignment.Center;

                    view.Columns[clm].ColumnEdit = maskData;
                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    //   view.Columns[clm].MaxWidth = 100;
                    //  view.Columns[clm].MinWidth = 100;
                }
                else if (nc.ToLower().Contains("rating"))
                {
                    lstColumn[i].Name = nc.Replace("rating=", "");
                    var clm = lstColumn[i].Name;
                    var _rating = new RepositoryItemRatingControl
                    {
                        Name = clm,
                        ShowText = true,
                        ValueInterval = 0
                    };

                    // _rating.ItemClick += _rating_ItemClick;
                    // _rating.Click += _rating_Click;
                    // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                    // RepositoryItemRatingControl rating = new RepositoryItemRatingControl();
                    _rating.LookAndFeel.SkinName = "Caramel";
                    // _rating.LookAndFeel.SkinName = "Summer 2008";
                    _rating.LookAndFeel.UseDefaultLookAndFeel = false;

                    grid.RepositoryItems.Add(_rating);

                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment
                        = HorzAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment =
                        HorzAlignment.Near;

                    view.Columns[clm].ColumnEdit = _rating;
                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    view.Columns[clm].MaxWidth = 95;
                    view.Columns[clm].MinWidth = 95;
                }
                else if (nc.ToLower().Contains("checked"))
                {
                    lstColumn[i].Name = nc.Replace("checked=", "");
                    var clm = lstColumn[i].Name;
                    var _bool = new RepositoryItemCheckEdit
                    {
                        Name = clm,
                        CheckBoxOptions = { Style = CheckBoxStyle.Custom },
                        ImageOptions =
                        {
                            SvgImageChecked = Properties.Resources.Check_New_2, SvgImageUnchecked = Properties.Resources.UnCheck_New_2, SvgImageSize = new Size(20, 20)
                        }

                        //this.checkEdit1.Properties.AutoHeight = false;
                        //this.checkEdit1.Properties.Caption = "checkEdit1";
                        //this.checkEdit1.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom;
                        //this.checkEdit1.Properties.ImageOptions.SvgImageChecked = global::PackComponent.Properties.Resources.Check_New_2;
                        //this.checkEdit1.Properties.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
                        //this.checkEdit1.Properties.ImageOptions.SvgImageUnchecked = global::PackComponent.Properties.Resources.stop__1_;



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

                    grid.RepositoryItems.Add(_bool);
                    // _bool.CheckStyle = CheckStyles.Standard;

                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    view.Columns[clm].ColumnEdit = _bool;
                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    view.Columns[clm].MaxWidth = 95;
                    view.Columns[clm].MinWidth = 95;
                }
                else if (nc.ToLower().Contains("volume"))
                {
                    lstColumn[i].Name = nc.Replace("volume=", "");
                    var clm = lstColumn[i].Name;
                    var _trackBar = new RepositoryItemTrackBar
                    {
                        Name = clm,
                        Minimum = 0,
                        Maximum = 100,

                    };

                    _trackBar.ShowLabels = true;
                    _trackBar.ShowLabelsForHiddenTicks = true;
                    _trackBar.ShowValueToolTip = true;

                    //  if (_trackBar.Labels.Count == 0)
                    // _trackBar.Labels.Add(new TrackBarLabel { Value = 5});
                    // _trackBar.LookAndFeel.SkinName = "Glass Oceans";
                    _trackBar.LookAndFeel.SkinName = "Caramel";
                    //  _bool.LookAndFeel.SkinName = "Summer 2008";
                    _trackBar.LookAndFeel.UseDefaultLookAndFeel = false;

                    grid.RepositoryItems.Add(_trackBar);
                    // _bool.CheckStyle = CheckStyles.Standard;

                    view.Columns[clm].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    view.Columns[clm].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    view.Columns[clm].ColumnEdit = _trackBar;
                    view.Columns[clm].AppearanceHeader.Name = clm;
                    view.Columns[clm].Caption = clm;
                    view.Columns[clm].MaxWidth = 95;
                    view.Columns[clm].MinWidth = 95;
                }

                else
                {
                    try
                    {
                        view.Columns[nc].AppearanceCell.TextOptions.HAlignment =
                            HorzAlignment.Center;
                        view.Columns[nc].AppearanceHeader.TextOptions.HAlignment
                            = HorzAlignment.Center;
                        view.Columns[nc].OptionsColumn.AllowSize = true;
                        //  view.Columns[nc].OptionsColumn.AllowEdit = Allow_Edit;
                        // view.OptionsBehavior.Editable = Allow_Edit;
                        //  view.Columns[nc].OptionsColumn.AllowSort = Allow_Sort ? DefaultBoolean.True : DefaultBoolean.False;
                    }
                    catch
                    {
                        // ignored
                    }
                }
                // _font.ChangeFont(chkFind);
            }

            //for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            //{
            //    DGV_Viw.Columns[i].VisibleIndex = i;
            //}



            #region New





            #endregion

            // SetFieldSizeColumn();
            grid.EndUpdate();
        }

        #endregion

        #region Setting vGrid

        //public static void SettingDefaultvGrid(this vGrid vGrid, int maxAndMinHeight, int rowHeaderWidth)
        //{
        //    vGrid.data.OptionsBehavior.ResizeRowValues = false;
        //    vGrid.data.OptionsBehavior.ResizeHeaderPanel = false;
        //    vGrid.data.OptionsBehavior.ResizeRowHeaders = false;
        //    vGrid.data.OptionsBehavior.UseEnterAsTab = true;
        //    vGrid.data.OptionsFind.FindDelay = 100;
        //    vGrid.data.OptionsFind.FindMode = DevExpress.XtraVerticalGrid.FindMode.Always;
        //    vGrid.data.OptionsFind.ShowCloseButton = false;
        //    vGrid.data.OptionsFind.ShowFindButton = false;
        //    vGrid.data.OptionsFind.Visibility = FindPanelVisibility.Never;
        //    vGrid.data.OptionsFind.FindNullPrompt = "نام فیلد مورد نظر را وارد نمایید";
        //    vGrid.data.OptionsView.AllowHtmlText = true;
        //    vGrid.data.OptionsView.MaxRowAutoHeight = vGrid.data.OptionsView.MinRowAutoHeight = maxAndMinHeight;
        //    vGrid.data.OptionsView.FixRowHeaderPanelWidth = true;
        //    vGrid.data.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        //    vGrid.data.RowHeaderWidth = rowHeaderWidth;
        //}

        public static string GetNameCheckGrid()
        {
            return "DX$CheckboxSelectorColumn";
        }
        //

        #endregion

        #region مدیریت فیلد گرید

        public static int RowCount(this Data_Grid dgv)
        {
            return dgv.DGV_Viw.RowCount;
        }
        public static int ColumnCount(this Data_Grid dgv)
        {
            return dgv.DGV_Viw.Columns.Count;
        }

        public static void AddAllowNewRowAndType(this Data_Grid dgv, DefaultBoolean active, NewItemRowPosition position)
        {
            dgv.DGV_Viw.OptionsBehavior.AllowAddRows = active;
            dgv.DGV_Viw.OptionsView.NewItemRowPosition = position;
        }
        //public static void MaxMinWidth(this Data_Grid dgv, string nameColumn, int min, int Max)
        //{
        //    dgv.DGV_Viw.Columns[nameColumn].MinWidth = min;
        //    dgv.DGV_Viw.Columns[nameColumn].MaxWidth = Max;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns>نگهداشتن ستون پیش فرض نمایش</returns>
        public static List<string> HiddenColumn(this Data_Grid dgv)
        {
            var getOnlyVis = dgv.DGV_Viw.VisibleColumns.Select(s1 => s1.FieldName).ToList();
            for (int i = 0; i < dgv.ColumnCount(); i++)
            {
                dgv.DGV_Viw.Columns[i].Visible = false;
            }

            return getOnlyVis;
        }
        public static void HiddenColumn(this Data_Grid dgv, string nameColumn)
        {
            dgv.DGV_Viw.Columns[nameColumn].Visible = false;
        }
        public static void HiddenColumn(this Data_Grid dgv, List<string> nameColumns)
        {
            foreach (string s in nameColumns)
            {
                dgv.DGV_Viw.Columns[s].Visible = false;
            }
        }
        public static void HiddenBandedGroup(this Data_Grid dgv, string nameBanded = "banded")
        {
            dgv.DGV_Viw.Bands[nameBanded].Visible = false;
        }
        public static void ShowBandedGroup(this Data_Grid dgv, string nameBanded = "banded")
        {
            dgv.DGV_Viw.Bands[nameBanded].Visible = true;
        }
        public static void ShowColumn(this Data_Grid dgv, string nameColumn)
        {
            var getCount = dgv.DGV_Viw.VisibleColumns.Count;
            dgv.DGV_Viw.Columns[nameColumn].VisibleIndex = getCount + 1;
            dgv.DGV_Viw.Columns[nameColumn].Visible = true;
        }
        public static void ShowColumn(this Data_Grid dgv, List<string> nameColumns)
        {
            foreach (string s in nameColumns)
            {
                var getCount = dgv.DGV_Viw.VisibleColumns.Count;
                dgv.DGV_Viw.Columns[s].VisibleIndex = getCount + 1;
                dgv.DGV_Viw.Columns[s].Visible = true;
            }
        }
        public static void ShowColumn(this Data_Grid dgv, string nameColumn, int postion)
        {
            // var getCount = dgv.DGV_Viw.VisibleColumns.Count;
            dgv.DGV_Viw.Columns[nameColumn].VisibleIndex = postion;
            dgv.DGV_Viw.Columns[nameColumn].Visible = true;
        }
        //public static void ShowColumnAutoPsotion(this Data_Grid dgv, string nameColumn)
        //{
        //    dgv.DGV_Viw.Columns[nameColumn].Visible = true;
        //    var getCount = dgv.DGV_Viw.VisibleColumns.Count;
        //    dgv.DGV_Viw.Columns[nameColumn].VisibleIndex = getCount + 1;

        //}
        public static void ShowColumns(this Data_Grid dgv)
        {
            for (int i = 0; i < dgv.DGV_Viw.Columns.Count; i++)
            {
                var getName = dgv.DGV_Viw.Columns[i].FieldName;
                ShowColumn(dgv, getName);
            }
        }
        public static void HiddenColumn(this RepositoryItemGridLookUpEdit dgv, string nameColumn)
        {
            dgv.PopupView.Columns[nameColumn].Visible = false;
        }
        public static void ReadOnlyColumn(this Data_Grid dgv, string nameColumn)
        {
            dgv.DGV_Viw.Columns[nameColumn].OptionsColumn.ReadOnly = true;
        }
        public static void AllowEditColumn(this Data_Grid dgv, string nameColumn, bool enabled = false)
        {
            dgv.DGV_Viw.Columns[nameColumn].OptionsColumn.AllowEdit = enabled;
        }
        public static void ColumnsAllow(this Data_Grid dgv)
        {
            foreach (BandedGridColumn column in dgv.DGV_Viw.Columns)
            {
                dgv.DGV_Viw.Columns[column.FieldName].OptionsColumn.AllowEdit = true;
            }
        }
        public static void ColumnsDeny(this Data_Grid dgv)
        {
            foreach (BandedGridColumn column in dgv.DGV_Viw.Columns)
            {
                dgv.DGV_Viw.Columns[column.FieldName].OptionsColumn.AllowEdit = false;
            }
        }
        public static void TagColumn(this Data_Grid dgv, string nameColumn, string textTag)
        {
            dgv.DGV_Viw.Columns[nameColumn].Tag = textTag;
        }
        public static void ShowTagColumn(this Data_Grid dgv, string nameColumn)
        {
            dgv.DGV_Viw.Columns[nameColumn].ToolTip = dgv.DGV_Viw.Columns[nameColumn].Tag.ToString();
        }
        public static void FormatStringNegativeNumber(this Data_Grid dgv, string nameColumn)
        {
            if (dgv.DGV.RightToLeft == RightToLeft.Yes)
                dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;#,##0.###-";
            else
                dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;-#,##0.###";
        }
        public static void FormatStringNegativeNumberNoPrice(this Data_Grid dgv, string nameColumn)
        {
            if (dgv.DGV.RightToLeft == RightToLeft.Yes)
                dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "###;###-";
            else
                dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "###;-###";
        }
        public static void SettingGridView(this GridView viewChild)
        {
            // viewChild.ViewCaption = @"Final Test";

            viewChild.OptionsView.AllowCellMerge = false;
            viewChild.OptionsView.AllowHtmlDrawGroups = false;
            viewChild.OptionsView.AnimationType = GridAnimationType.AnimateFocusedItem;
            viewChild.OptionsView.BestFitMode = GridBestFitMode.Full;
            viewChild.OptionsView.BestFitUseErrorInfo = DefaultBoolean.True;
            viewChild.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            viewChild.OptionsView.HeaderFilterButtonShowMode = FilterButtonShowMode.Button;
            viewChild.OptionsView.RowAutoHeight = true;
            viewChild.OptionsView.ShowGroupPanel = false;
            viewChild.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Indicator;

            viewChild.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            viewChild.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            viewChild.OptionsBehavior.AllowFixedGroups = DefaultBoolean.True;
            viewChild.OptionsBehavior.AllowIncrementalSearch = true;
            viewChild.OptionsBehavior.Editable = false;
            viewChild.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;

            viewChild.OptionsView.ColumnAutoWidth = false;

            viewChild.Appearance.GroupFooter.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.GroupFooter.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.GroupButton.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.GroupButton.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.FooterPanel.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.FooterPanel.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.SelectedRow.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.SelectedRow.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.FocusedCell.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.FocusedCell.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.OddRow.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.OddRow.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.FocusedRow.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.FocusedRow.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.VertLine.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.VertLine.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.Preview.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.Preview.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.DetailTip.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.DetailTip.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.ColumnFilterButton.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.ColumnFilterButton.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.EvenRow.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.EvenRow.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.GroupRow.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.GroupRow.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.Row.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            viewChild.Appearance.HeaderPanel.TextOptions.VAlignment = VertAlignment.Center;

            viewChild.Appearance.Row.ForeColor = Color.FromArgb(206, 42, 42);
            //if (viewChild.Columns.Count>0)
            //viewChild.BestFitColumns(false);
        }
        public static void AutoSummaryCalculate(this CustomSummaryEventArgs e, string filedName, string titleDown, ref double valueCalcOutPut, string afterValue = "", int N = 3)
        {
            var ConE = e.ConvertItemSummary();

            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    valueCalcOutPut = 0.0;
                    break;
                case CustomSummaryProcess.Calculate:
                    if (e.FieldValue != null)
                    {
                        if (ConE.FieldName.Equals(filedName))
                            valueCalcOutPut += Convert.ToDouble(e.FieldValue.ToString());
                    }
                    break;
                case CustomSummaryProcess.Finalize:
                    if (ConE.FieldName.Equals(filedName))
                        e.TotalValue = titleDown + ": " +
                                       valueCalcOutPut.ToString(FormatStringNegativeNumber(N))
                                       + " " + afterValue;
                    break;

            }
        }
        public static double AutoSummaryCalculateSelectedRows(this CustomSummaryEventArgs e, object s, string filedName, string otherFiledName, string titleDown, ref double valueCalcOutPut, string afterValue = "", int N = 3)
        {
            var conE = e.ConvertItemSummary();
            BandedGridView gridView = (BandedGridView)s;

            var allSel = gridView.GetSelectedRows();
            var gR = e.RowHandle;
            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    valueCalcOutPut = 0.0;
                    break;
                case CustomSummaryProcess.Calculate:
                    if (e.FieldValue != null)
                    {
                        if (conE.FieldName.Equals(filedName))
                            if (allSel.Contains(gR))
                                valueCalcOutPut += Convert.ToDouble(e.FieldValue.ToString());
                    }
                    break;
                case CustomSummaryProcess.Finalize:
                    if (conE.FieldName.Equals(otherFiledName))
                    {
                        e.TotalValue = titleDown + ": " + valueCalcOutPut.ToString(FormatStringNegativeNumber(N)) + " " + afterValue;
                        return valueCalcOutPut;
                    }
                    break;

            }

            return 0;
        }

        static double cntSummary;

        public static void AutoSummaryCalculateAvg(this CustomSummaryEventArgs e, string filedName, string titleDown, ref double valueCalcOutPut, string afterValue = "")
        {
            //  var ConE = e.Item as GridGroupSummaryItem;

            var ConE = e.ConvertItemGroupSummary();
            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    {
                        valueCalcOutPut = 0.0;
                        cntSummary = 0;
                    }
                    break;
                case CustomSummaryProcess.Calculate:
                    if (e.FieldValue != null)
                    {
                        if (ConE.FieldName.Equals(filedName))
                        {
                            cntSummary++;
                            valueCalcOutPut = valueCalcOutPut + Convert.ToDouble(e.FieldValue.ToString());
                        }
                    }

                    break;
                case CustomSummaryProcess.Finalize:
                    if (ConE.FieldName.Equals(filedName))
                    {
                        e.TotalValue = "(" + titleDown + "= " + (valueCalcOutPut / cntSummary).ToString(FormatStringNegativeNumber(RightToLeft.No)) + ") " +
                                       afterValue;
                    }

                    break;
            }
        }

        public static void Sort(this Data_Grid dgv, string nameColumn, ColumnSortOrder order = ColumnSortOrder.Ascending)
        {
            dgv.DGV_Viw.ClearSorting();
            dgv.DGV_Viw.Columns[nameColumn].SortOrder = order;
            dgv.DGV_Viw.MoveFirst();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="nameColumn"></param>
        /// <param name="style">پیش فرض فیکس از راست</param>
        public static void ColumnFixedUseScroll(this Data_Grid dgv, string nameColumn, FixedStyle style = FixedStyle.Left)
        {
            dgv.DGV_Viw.Columns[nameColumn].Fixed = style;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="nameColumn">نام ستون</param>
        /// <param name="N0">تعداد اعشار</param>
        public static void FormatStringNegativeNumber(this Data_Grid dgv, string nameColumn, int N0)
        {
            if (dgv.RightToLeft == RightToLeft.Yes)
            {
                if (N0 == 0)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0;#,##0-";
                else if (N0 == 1)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.#;#,##0.#-";
                else if (N0 == 2)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.##;#,##0.##-";
                else if (N0 == 3)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;#,##0.###-";
            }
            else
            {
                if (N0 == 0)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0;-#,##0";

                else if (N0 == 1)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.#;-#,##0.#";

                else if (N0 == 2)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.##;-#,##0.##";

                else if (N0 == 3)
                    dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;-#,##0.###";
            }

            dgv.DGV_Viw.Columns[nameColumn].GroupFormat.FormatString = dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString;
        }
        public static string FormatStringMosbatNumber(int N0)
        {
            if (N0 == 1)
                return "#,##0.#";
            if (N0 == 2)
                return "#,##0.##";
            if (N0 == 3)
                return "#,##0.###";
            return "#,##0";
        }
        public static void FormatStringNegativeNumberNotAshar(this Data_Grid dgv, string nameColumn, string format = "#,##0.###")
        {
            dgv.DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = format;
        }
        public static string FormatStringNegativeNumber(int N0)
        {
            if (N0 == 0)
                return "#,##0;#,##0-";
            if (N0 == 1)
                return "#,##0.#;#,##0.#-";
            if (N0 == 2)
                return "#,##0.##;#,##0.##-";
            if (N0 == 3)
                return "#,##0.###;#,##0.###-";

            return null;
        }
        public static string FormatStringNegativeNumber(RightToLeft rightToLeft)
        {
            if (rightToLeft == RightToLeft.Yes)
                return "#,##0.###;#,##0.###-";
            else
                return "#,##0.###;-#,##0.###";
        }
        public static void ActiveScrollGrid(this Data_Grid dgv, bool Active = true)
        {
            dgv.DGV_Viw.OptionsView.ColumnAutoWidth = !Active;
            dgv.DGV_Viw.BestFitColumns();
        }
        //public static void SingleClickCell(object sender, MouseEventArgs e, string clm)
        //{
        //    GridView view = sender as GridView;
        //    GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
        //    if (hitInfo.InRowCell && e.Button == MouseButtons.Left && hitInfo.Column.FieldName == clm)
        //    {
        //        if (e.Clicks == 1)
        //        {
        //            view.FocusedColumn = hitInfo.Column;
        //            view.FocusedRowHandle = hitInfo.RowHandle;
        //            ClassKeyAndMouse.DoMouseClick();
        //        }
        //    }
        //}

        #endregion

        #region vGrid

        //public static RepositoryItemLookUpEdit AddGridTovGrid(this vGrid vGrid1,
        //    string connection, string query,
        //    DataTable dtTest, EditorRow Type1,
        //    string vMember, string dMember, RepositoryItemLookUpEdit lookUpEdit)
        //{
        //    dtTest.Clear();
        //    //  vGrid1.data.RepositoryItems.Clear();
        //    var dataQuery2 = _bank.LIGSelect_SQL(connection, query);

        //    // var lookUpEdit = new RepositoryItemLookUpEdit();

        //    lookUpEdit.DataSource = dataQuery2;
        //    lookUpEdit.ValueMember = vMember;
        //    lookUpEdit.DisplayMember = dMember;
        //    lookUpEdit.NullText = @"انتخاب نمایید";

        //    vGrid1.data.RepositoryItems.Add(lookUpEdit);

        //    Type1.Properties.RowEdit = lookUpEdit;
        //    //ItemGridNew.PopulateColumns();

        //    return lookUpEdit;
        //}

        //public static void AddFieldTovGrid(vGrid vGrid, List<ModelFieldvGrid> nameField)
        //{
        //    foreach (ModelFieldvGrid filed in nameField)
        //    {
        //        if (filed.LstMultiEditorRowProperties == null)
        //        {
        //            filed.TypeField.Properties.RowEdit = filed.RepositoryItem;
        //            filed.TypeField.Name = filed.Caption;
        //            filed.TypeField.Properties.Caption = filed.Caption;
        //            if (filed.RepositoryItem != null)
        //                filed.RepositoryItem.NullText = filed.NullText;
        //            vGrid.data.Rows.Add(filed.TypeField);
        //        }
        //        else // (filed.LstMultiEditorRowProperties.Count > 1)
        //        {
        //            // filed.TypeField.Properties.RowEdit = filed.RepositoryItem;
        //            var ggg = (MultiEditorRow)filed.TypeField;

        //            foreach (var item in filed.LstMultiEditorRowProperties)
        //            {
        //                ggg.PropertiesCollection.Add(item);
        //            }

        //            filed.TypeField.Name = filed.Caption;
        //            //filed.TypeField.Properties.Caption = filed.Caption;
        //            if (filed.RepositoryItem != null)
        //                filed.RepositoryItem.NullText = filed.NullText;
        //            vGrid.data.Rows.Add(filed.TypeField);
        //        }
        //    }
        //}
        public static RepositoryItemGridLookUpEdit AddvGridTovGrid(DataTable dtData, string valueMember,
            string displayMember, TextEditStyles styles = TextEditStyles.Standard)
        {
            var gridComboEdit = new RepositoryItemGridLookUpEdit
            {
                ValueMember = valueMember,
                DisplayMember = displayMember,
                DataSource = dtData,
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2019 Colorful" }
                //  LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray" }
            };

            _font.ChangeFont(gridComboEdit, 14);

            gridComboEdit.View.BestFitColumns(false);

            gridComboEdit.View.OptionsView.ShowAutoFilterRow = true;

            gridComboEdit.View.OptionsFind.SearchInPreview = true;
            gridComboEdit.View.OptionsFind.AllowFindPanel = true;
            gridComboEdit.View.OptionsFind.FindMode = FindMode.Always;
            gridComboEdit.View.OptionsFind.Condition = FilterCondition.Contains;

            gridComboEdit.View.OptionsFilter.AllowAutoFilterConditionChange = DefaultBoolean.True;
            gridComboEdit.View.OptionsFilter.AllowFilterEditor = true;
            gridComboEdit.View.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;
            // TODO: 
            // gridComboEdit.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.ImmediatePopup = true;
            gridComboEdit.TextEditStyle = styles;

            gridComboEdit.PopulateViewColumns();
            gridComboEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            gridComboEdit.View.RowStyle += EventRowStyle_RowStyle;
            return gridComboEdit;
        }

        public class modelFieldvGrid
        {
            public string Caption { get; set; }
            public BaseRow TypeField { get; set; }
            public RepositoryItem RepositoryItem { get; set; }
            public string NullText { get; set; }
        }

        //public static void AddFieldTovGrid(this vGrid _vGrid, List<modelFieldvGrid> nameField)
        //{
        //    _vGrid.data.Rows.Clear();
        //    foreach (modelFieldvGrid filed in nameField)
        //    {
        //        filed.TypeField.Properties.RowEdit = filed.RepositoryItem;
        //        filed.TypeField.Name = filed.Caption;
        //        filed.TypeField.Properties.Caption = filed.Caption;
        //        if (filed.RepositoryItem != null)
        //            filed.RepositoryItem.NullText = filed.NullText;
        //        _vGrid.data.Rows.Add(filed.TypeField);
        //    }
        //}

        //public static void AddFieldTovGridNotClear(this vGrid _vGrid, List<modelFieldvGrid> nameField)
        //{
        //    foreach (modelFieldvGrid filed in nameField)
        //    {
        //        filed.TypeField.Properties.RowEdit = filed.RepositoryItem;
        //        filed.TypeField.Name = filed.Caption;
        //        filed.TypeField.Properties.Caption = filed.Caption;
        //        if (filed.RepositoryItem != null)
        //            filed.RepositoryItem.NullText = filed.NullText;

        //        _vGrid.data.RecordsInterval = 1;
        //        _vGrid.data.Rows.Add(filed.TypeField);
        //    }
        //}

        //public static void AddButtonToUpPanelDGV(this Data_Grid dgv, modelButtonDGV buttonDgvs)
        //{
        //    SimpleButton button = new SimpleButton
        //    {
        //        Image = buttonDgvs.Image,
        //        Width = buttonDgvs.Width,
        //        Name = buttonDgvs.Name,
        //        Dock = buttonDgvs.DockStyle,
        //        Text = buttonDgvs.Text
        //    };

        //    dgv.AddButtonOrControlUpGrid(button);
        //}



        //public class modelButtonDGV
        //{
        //    public string Name { get; set; }
        //    public string Text { get; set; }
        //    public Image Image { get; set; }
        //    public DockStyle DockStyle { get; set; }
        //    public int Width { get; set; }

        //}
        #endregion

        #region Work Value And Cell

        //public static object GetValue(this vGrid vGrid1, string nameCell, bool displayValue = false, string defaultValue = null)
        //{
        //    var name = vGrid1.data.GetRowByName(nameCell);
        //    var getValue = displayValue
        //        ? vGrid1.data.GetCellDisplayText(name, 1)
        //        : vGrid1.data.GetCellValue(name, 1);

        //    if (getValue == null)
        //        if (defaultValue != null)
        //            return defaultValue;
        //    return getValue;
        //}

        //public static void SetValue(this vGrid vGrid1, string nameCell, object value)
        //{
        //    var name = vGrid1.data.GetRowByName(nameCell);
        //    vGrid1.data.SetCellValue(name, 1, value);
        //}

        //public static void SetNull(this vGrid vGrid1)
        //{
        //    vGrid1.data.BeginUpdate();
        //    for (int i = 0; i < vGrid1.data.Rows.Count; i++)
        //    {
        //        vGrid1.data.Rows[i].Properties.Value = null;
        //    }

        //    vGrid1.data.EndUpdate();
        //}

        //public static void CellDisable(this vGrid vGrid1, string nameCell, bool enabled = false)
        //{
        //    vGrid1.data.BeginUpdate();
        //    var cell = (EditorRow)vGrid1.data.Rows[nameCell];
        //    cell.Enabled = enabled;
        //    vGrid1.data.EndUpdate();

        //    //vGrid1.data.BeginUpdate();
        //    //var name = vGrid1.data.GetRowByName(nameCell);
        //    //name.Properties.ReadOnly = true;
        //    //vGrid1.data.EndUpdate();
        //}

        //public static void SetValueIfNull(this vGrid vGrid1, string nameCell, object value)
        //{
        //    var getValueDate = vGrid1.GetValue(nameCell);
        //    if (getValueDate == null)
        //        vGrid1.SetValue(nameCell, value);

        //}

        //public static void CellDisableIfNull(this vGrid vGrid1, string nameCell)
        //{
        //    var getValueDate = vGrid1.GetValue(nameCell);
        //    if (getValueDate != null)
        //    {
        //        var cell = (EditorRow)vGrid1.data.Rows[nameCell];
        //        cell.Enabled = false;
        //        //var name = vGrid1.data.GetRowByName(nameCell);
        //        //name.Properties.ReadOnly = true;
        //    }
        //}

        //public static bool GetEnabledCell(this vGrid vGrid1, string nameCell)
        //{
        //    var cell = ((EditorRow)vGrid1.data.Rows[nameCell]).Enabled;
        //    return cell;
        //}

        #endregion

        #region Model RepositoryItem

        public static RepositoryItemComboBox ModelComboBoxItem(List<string> list, float size = 13)
        {
            var data = new RepositoryItemComboBox
            {
                TextEditStyle = TextEditStyles.DisableTextEditor,
                AppearanceDropDown = { Font = _font.ChangeFont(size) }
            };
            data.Items.AddRange(list);
            return data;
        }



        public static RepositoryItemTextEdit ModelTextEditNumber(int maxLenth = 32000)
        {
            return new RepositoryItemTextEdit
            {
                Mask =
                {
                    MaskType = MaskType.RegEx,
                    EditMask = @"([0-9])+",
                    UseMaskAsDisplayFormat = false
                },
                MaxLength = maxLenth,
                Appearance = { TextOptions = { RightToLeft = false } }
            };
        }

        public static RepositoryItemTextEdit ModelTextEditPrice(int maxLenth = 32000)
        {
            return new RepositoryItemTextEdit
            {
                Mask =
                {
                    MaskType = MaskType.Numeric,
                    EditMask = @"N0",
                    UseMaskAsDisplayFormat = true
                },
                MaxLength = maxLenth,
                Appearance = { TextOptions = { RightToLeft = false } }
            };
        }

        private static ModelFieldvGrid ModelCheckEdit(string caption)
        {
            var grid = new ModelFieldvGrid
            {
                Caption = caption,
                TypeField = new EditorRow(),
                RepositoryItem = new RepositoryItemCheckEdit
                {
                    LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray" }
                }
            };

            return grid;
        }

        public static RepositoryItemMemoEdit ModelMemoEdit(int maxLenth = 32000)
        {
            var edit = new RepositoryItemMemoEdit();

            edit.AutoHeight = false;
            edit.MaxLength = maxLenth;
            edit.Appearance.TextOptions.RightToLeft = true;
            edit.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            edit.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            edit.AppearanceFocused.TextOptions.RightToLeft = true;
            edit.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Near;
            edit.AppearanceFocused.TextOptions.VAlignment = VertAlignment.Center;
            edit.AppearanceDisabled.TextOptions.RightToLeft = true;
            edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Near;
            edit.AppearanceDisabled.TextOptions.VAlignment = VertAlignment.Center;
            edit.KeyPress -= Edit_KeyPress;
            edit.KeyPress += Edit_KeyPress;
            return edit;
        }

        private static void Edit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // _classText.ConvertNumberToEnglishAndPersian(e);
        }

        public static RepositoryItemCheckEdit ModelCheckBox(float size = 13)
        {
            var data = new RepositoryItemCheckEdit
            {
                Appearance = { Font = _font.ChangeFont(size) },
                AppearanceDisabled = { Font = _font.ChangeFont(size) },
                AppearanceFocused = { Font = _font.ChangeFont(size) },
                AppearanceReadOnly = { Font = _font.ChangeFont(size) },

                CheckBoxOptions = { Style = CheckBoxStyle.Custom },
                ImageOptions =
                {
                    SvgImageChecked = Properties.Resources.Check_New_2, SvgImageUnchecked = Properties.Resources.UnCheck_New_2, SvgImageSize = new Size(20, 20)
                }

            };

            return data;
        }

        public static RepositoryItemTextEdit ModelTextEditDate(int maxLenth = 10)
        {
            return new RepositoryItemTextEdit
            {
                MaxLength = maxLenth,
                Appearance = { TextOptions = { RightToLeft = true } }
            };
        }

        #endregion

        #region PleaseWaitCounter 
        //public static void PleaswaitStartEducation(XtraForm _form)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWaitEducation), true, true, false);
        //}
        //public static void PleaswaitStartNet(XtraForm _form)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWaitNet), true, true, false);
        //}
        //public static void PleaswaitStartDesign(XtraForm _form)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWaitDesign), true, true, false);
        //}
        //public static void PleaswaitStartPlanning(XtraForm _form)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWaitPlanning), true, true, false);
        //}
        //public static void PleaswaitStart(XtraForm _form)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWaitCounter), true, true, false);
        //}

        //public static void PleaswaitCount(double count)
        //{
        //    SplashScreenManager.Default.SendCommand(PleaseWaitCounter.SplashScreenCommand.max, "Count=" + count);
        //}

        //public static void PleaswaitCounter(double count)
        //{
        //    SplashScreenManager.Default.SendCommand(PleaseWaitCounter.SplashScreenCommand.count, count);
        //}

        //public static void PleaswaitEnd()
        //{
        //    SplashScreenManager.CloseForm(false);
        //}

        //public static void PleaswaitStartWithoutCount(XtraForm _form)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWait1), true, true, false);
        //}

        //public static void PleaswaitEndWithoutCount()
        //{
        //    SplashScreenManager.CloseForm(false);
        //}

        //public static void PleaswaitStartWithoutCount(XtraForm _form, string msg)
        //{
        //    SplashScreenManager.ShowForm(_form, typeof(PleaseWait1), true, true, false);
        //    //  SplashScreenManager splashScreenManager = PleaseWait1;
        //    // if (!string.IsNullOrEmpty(caption))
        //    //  splashScreenManager.SetWaitFormCaption(caption);
        //}

        #endregion

        #region Data Layout Base Control

        public class modelControlDataLayout
        {
            public Control Ctrl { get; set; }
            public int Grp { get; set; }
            public LayoutVisibility Visibility { get; set; }
            public SizeConstraintsType SizeType { get; set; }
            public int AutoHeight { get; set; }
            public bool AllowNull { get; set; }
            public bool HideLabel { get; set; }
            /// <summary>
            /// Null After Save
            /// </summary>
            public bool NAS { get; set; }

            /// <summary>
            /// Relation Field To DataTable.
            /// </summary>
            public string RFTDataTable { get; set; }
            public modelControlDataLayout()
            {
                SizeType = SizeConstraintsType.Default;
                AllowNull = true;
                RFTDataTable = null;
                NAS = true;
                HideLabel = false;
            }
        }

        public static void SetFieldColumnDataLayout2(this dataLayout Layout, bool clear, int cntColumn, List<modelControlDataLayout> items, float fontSize = 12f, bool bold = false)
        {
            Layout.baseLayout.OptionsFocus.EnableAutoTabOrder = false;
            Layout.baseLayout.BeginUpdate();
            Layout.baseLayout.BackColor = Color.Transparent;
            var font = bold == false
                ? _font.ChangeFont(fontSize)
                : _fontBold.ChangeFont(fontSize);

            if (clear)
                Layout.baseLayout.Clear();

            List<LayoutControlGroup> lstList = new List<LayoutControlGroup>();

            int cRowIndex = 0;
            int cGrpIndex = cntColumn;

            for (int i = 1; i <= cntColumn; i++)
            {
                LayoutControlGroup grp = new LayoutControlGroup
                {
                    Name = "grp" + i,
                    TextVisible = false,
                    GroupBordersVisible = false,
                    AppearanceItemCaption =
                    {
                        Font = font,
                    },
                    //Padding = new DevExpress.XtraLayout.Utils.Padding(20),
                    AppearanceGroup = { Font = font },
                    OptionsTableLayoutItem = { RowIndex = cGrpIndex },
                };

                cGrpIndex--;
                var item = Layout.baseLayout.Root.AddGroup(grp);

                lstList.Add(item);

                for (int j = 0; j < items.Count; j++)
                {
                    items[j].Ctrl.Font = font;
                    if (grp.Name.Contains(items[j].Grp.ToString()))
                    {
                        DevExpress.XtraLayout.Utils.Padding padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);

                        LayoutControlGroup grp1 = new LayoutControlGroup
                        {
                            Visibility = items[j].Visibility,
                            Name = "Mojtaba_" + i,
                            TextVisible = false,
                            GroupBordersVisible = false,
                            AppearanceItemCaption =
                            {
                                Font = font,
                            },
                            //Padding = new DevExpress.XtraLayout.Utils.Padding(20),
                            AppearanceGroup = { Font = font },
                            OptionsTableLayoutItem = { RowIndex = cGrpIndex },
                        };

                        var addGrpSingle = item.AddGroup(grp1);

                        LayoutControlItem gItem = addGrpSingle.AddItem(
                            new LayoutControlItem
                            {

                                Name = "LCI_" + items[j].Ctrl.Name,
                                Visibility = items[j].Visibility,
                                AppearanceItemCaption =
                                {
                                    Font = font,
                                },

                                Control = items[j].Ctrl,
                                Text = items[j].Ctrl.Name + @":",
                                Padding = padding,
                                TextVisible = items[j].HideLabel == false,

                                OptionsTableLayoutItem = { RowIndex = cRowIndex },

                                SizeConstraintsType = items[j].SizeType,
                            });

                        Size newSize = new Size(gItem.Width, items[j].AutoHeight);

                        gItem.MaxSize = newSize;

                        if (!items[j].AllowNull)
                        {
                            gItem.AppearanceItemCaption.ForeColor = Color.Red;
                            gItem.Text = @"*" + gItem.Text;
                        }


                        cRowIndex++;
                    }

                    //  items[j].Ctrl.KeyDown += Ctrl_KeyDown;
                }

                // item.AddItem(separator);
            }


            for (int i = 1; i < lstList.Count; i++)
            {
                lstList[i].Move(lstList[i - 1],
                    Layout.baseLayout.RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);

                SimpleSeparator separator = new SimpleSeparator
                {
                    Name = "ss" + i,
                    Spacing = new DevExpress.XtraLayout.Utils.Padding(10)
                };
                Layout.baseLayout.Root.AddItem(separator);




                separator.Move(lstList[i - 1],
                    Layout.baseLayout.RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);
            }

            Layout.baseLayout.EndUpdate();
            Layout.baseLayout.BestFit();
        }

        #region Resize Center


        public static void SetFieldColumnDataLayoutResizeCenter(this dataLayout Layout, bool clear, int cntColumn, List<modelControlDataLayout> field, float fontSize = 12f)
        {
            Layout.baseLayout.BeginUpdate();

            if (Layout.baseLayout.RightToLeft == RightToLeft.Yes)
            {
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
            }
            else
            {
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
            }

            Layout.baseLayout.OptionsFocus.EnableAutoTabOrder = false;

            Layout.baseLayout.BackColor = Color.Transparent;
            var font = _font.ChangeFont(fontSize);
            if (clear)
                Layout.baseLayout.Clear();
            List<LayoutControlGroup> lstList = new List<LayoutControlGroup>();

            int cRowIndex = 0;
            int cGrpIndex = cntColumn;

            for (int i = 1; i <= cntColumn; i++)
            {
                LayoutControlGroup grp = new LayoutControlGroup
                {
                    Name = "grp" + i,
                    TextVisible = false,
                    GroupBordersVisible = false,
                    AppearanceItemCaption =
                    {
                        Font = font,
                    },
                    //Padding = new DevExpress.XtraLayout.Utils.Padding(20),
                    AppearanceGroup = { Font = font },
                    OptionsTableLayoutItem = { RowIndex = cGrpIndex },
                };

                cGrpIndex--;
                var item = Layout.baseLayout.Root.AddGroup(grp);

                lstList.Add(item);

                for (int j = 0; j < field.Count; j++)
                {
                    field[j].Ctrl.Font = font;
                    if (grp.Name.Contains(field[j].Grp.ToString()))
                    {
                        DevExpress.XtraLayout.Utils.Padding padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                        var gItem = item.AddItem(new LayoutControlItem
                        {
                            Name = "LCI_" + field[j].Ctrl.Name,
                            Visibility = field[j].Visibility,

                            AppearanceItemCaption =
                            {
                                Font = font,
                            },

                            Control = field[j].Ctrl,
                            Text = field[j].Ctrl.Name + @":",
                            Padding = padding,
                            TextVisible = true,

                            OptionsTableLayoutItem = { RowIndex = cRowIndex },

                            SizeConstraintsType = field[j].SizeType,
                        });

                        if (!field[j].AllowNull)
                        {
                            gItem.AppearanceItemCaption.ForeColor = Color.Red;
                            gItem.Text = @"*" + gItem.Text;
                        }
                        // LayoutControlItem
                        Size newSize = new Size(gItem.Width, field[j].AutoHeight);

                        gItem.MaxSize = newSize;

                        Layout.AddRelationTagDataTable(field[j].Ctrl.Name.Replace("*", ""), field[j].RFTDataTable);

                        cRowIndex++;
                    }

                    //  items[j].Ctrl.KeyDown += Ctrl_KeyDown;
                }
            }

            for (int i = 1; i < lstList.Count; i++)
            {
                lstList[i].Move(lstList[i - 1],
                    Layout.baseLayout.RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);

                SimpleSeparator separator = new SimpleSeparator
                {
                    Name = "ss" + i,
                    Spacing = new DevExpress.XtraLayout.Utils.Padding(2),
                    AppearanceItemCaption = { BackColor = Color.Red }
                };
                Layout.baseLayout.Root.AddItem(separator);
                separator.Move(lstList[i - 1],
                    Layout.baseLayout.RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);
            }

            Layout.baseLayout.EndUpdate();
            Layout.baseLayout.BestFit();
        }


        #endregion
        public static void SetFieldColumnDataLayout(this dataLayout Layout, bool clear, int cntColumn, List<modelControlDataLayout> field, float fontSize = 12f, bool bold = false)
        {
            //var getNow = Layout.Dock;
            //int getHeight = 1080, getWidth = 1920;

            //if (getNow == DockStyle.Top || getNow == DockStyle.Bottom)
            //    getHeight = Layout.Height;

            //if (getNow == DockStyle.Right || getNow == DockStyle.Left)
            //    getWidth = Layout.Width;

            //Layout.Dock = DockStyle.None;

            //Layout.Width = getWidth;
            //Layout.Height = getHeight;

            Layout.baseLayout.BeginUpdate();

            if (Layout.baseLayout.RightToLeft == RightToLeft.Yes)
            {
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
            }
            else
            {
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                Layout.baseLayout.Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
            }

            Layout.baseLayout.OptionsFocus.EnableAutoTabOrder = false;

            Layout.baseLayout.BackColor = Color.Transparent;
            // var font = _font.ChangeFont(fontSize);

            var font = bold == false
                ? _font.ChangeFont(fontSize)
                : _fontBold.ChangeFont(fontSize);

            if (clear)
                Layout.baseLayout.Clear();
            List<LayoutControlGroup> lstList = new List<LayoutControlGroup>();

            int cRowIndex = 0;
            int cGrpIndex = cntColumn;

            for (int i = 1; i <= cntColumn; i++)
            {
                LayoutControlGroup grp = new LayoutControlGroup
                {
                    Name = "grp" + i,
                    TextVisible = false,
                    GroupBordersVisible = false,
                    AppearanceItemCaption =
                    {
                        Font = font,
                    },
                    //Padding = new DevExpress.XtraLayout.Utils.Padding(20),
                    AppearanceGroup = { Font = font },
                    OptionsTableLayoutItem = { RowIndex = cGrpIndex },
                };

                cGrpIndex--;
                var item = Layout.baseLayout.Root.AddGroup(grp);

                lstList.Add(item);
                Layout.NNAS.Clear();
                for (int j = 0; j < field.Count; j++)
                {
                    Control ctrl = field[j].Ctrl;

                    if (ctrl != null)
                        ctrl.Font = font;
                    if (grp.Name.Contains(field[j].Grp.ToString()))
                    {
                        DevExpress.XtraLayout.Utils.Padding padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                        var gItem = item.AddItem(new LayoutControlItem
                        {
                            Name = ctrl != null ? "LCI_" + ctrl.Name : "Null_" + i,
                            Visibility = field[j].Visibility,

                            AppearanceItemCaption =
                            {
                            Font = ctrl != null ? font : null,
                            },

                            Control = ctrl,
                            Text = ctrl != null && !string.IsNullOrEmpty(ctrl.Name) && !ctrl.Name.Contains("GRP_") ? ctrl.Name + @":" : " ",
                            Padding = padding,
                            TextVisible = true,

                            OptionsTableLayoutItem = { RowIndex = cRowIndex },

                            SizeConstraintsType = field[j].SizeType,
                        });

                        if (!field[j].NAS)
                            Layout.NNAS.Add(ctrl.Name);

                        if (!field[j].AllowNull)
                        {
                            gItem.AppearanceItemCaption.ForeColor = Color.Red;
                            gItem.Text = @"*" + gItem.Text;
                        }

                        Size newSize = new Size(gItem.Width, field[j].AutoHeight);
                        gItem.MaxSize = newSize;

                        if (ctrl != null)
                        {
                            Layout.AddRelationTagDataTable(ctrl.Name.Replace("*", ""), field[j].RFTDataTable);
                        }

                        cRowIndex++;
                    }

                    //  items[j].Ctrl.KeyDown += Ctrl_KeyDown;
                }
            }
            for (int i = 1; i < lstList.Count; i++)
            {
                lstList[i].Move(lstList[i - 1],
                    Layout.baseLayout.RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);

                SimpleSeparator separator = new SimpleSeparator
                {
                    Name = "ss" + i,
                    Spacing = new DevExpress.XtraLayout.Utils.Padding(3),
                    AppearanceItemCaption = { BackColor = Color.Red }
                };
                Layout.baseLayout.Root.AddItem(separator);
                separator.Move(lstList[i - 1],
                    Layout.baseLayout.RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);
            }

            Layout.baseLayout.EndUpdate();
            Layout.baseLayout.BestFit();

            //  Layout.Dock = getNow;
        }

        #region Model Multi Column For Data Layout

        public class ModelMultiColumn
        {
            public Control Control { get; set; }
            public Type Type { get; set; }
            public bool AllowNull { get; set; }
            public int PercentRelative { get; set; }
            public ModelMultiColumn()
            {
                AllowNull = true;
            }
        }
        public class ModelReturnTable
        {
            public Control GroupControl { get; set; }
            public Control TableLayoutPanel { get; set; }
        }
        public static ModelReturnTable MultiColumnTable(this List<ModelMultiColumn> controls, BorderStyles borderStyles = BorderStyles.Default, float fontSize = 12f)
        {
            ModelReturnTable table = new ModelReturnTable();

            GroupControl grpControl = new GroupControl
            {
                // Name = "LCI_GRP_GrpControl",
                Name = "GRP_GrpControl",
                Dock = DockStyle.Fill,
                ShowCaption = false,
                Margin = new Padding(0, 10, 0, 0),
                BorderStyle = borderStyles,
                //Height = 20
            };

            TableLayoutPanel panel = new TableLayoutPanel
            {
                ColumnCount = controls.Count * 2,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Margin = new System.Windows.Forms.Padding(1)
                //  Margin = new System.Windows.Forms.Padding(20)
            };

            float sumRelative = 0;
            var sumControl = 0;

            foreach (ModelMultiColumn c in controls)
            {
                if (c.PercentRelative > 0)
                {
                    sumControl++;
                    sumRelative += c.PercentRelative;
                }
            }

            float getRelative = (100 - sumRelative) / (controls.Count - sumControl);

            int tblCnt = 0;

            for (int i = 0; i < controls.Count; i++)
            {
                tblCnt++;
                float getRelative2;
                if (controls[i].PercentRelative > 0)
                    getRelative2 = controls[i].PercentRelative;
                else
                    getRelative2 = getRelative;

                // LabelControl lblCtrl = new LabelControl
                LabelControl lblCtrl = new LabelControl
                {
                    //var getGRName= controls[i].Control.Name.re
                    Text = !string.IsNullOrEmpty(controls[i].Control.Name) ? controls[i].Control.Name + @":" : "",
                    Dock = DockStyle.Fill,
                    Appearance =
                    {
                        TextOptions = {HAlignment = HorzAlignment.Far, VAlignment = VertAlignment.Center},
                     //    BackColor = Color.FromArgb(225, 158, 158)
                    },
                    Font = _font.ChangeFont(12.5f),
                    Margin = i + tblCnt - 1 > 0
                        ? new Padding(35, 5, 2, 6)
                        : new Padding(1, 0, 2, 0)
                };

                //   lblCtrl.AutoSizeMode = LabelAutoSizeMode.None;

                if (!controls[i].AllowNull)
                {
                    lblCtrl.Appearance.ForeColor = Color.Red;
                    lblCtrl.Text = @"*" + lblCtrl.Text;
                }

                panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 10));
                panel.Controls.Add(lblCtrl, i + tblCnt - 1, 0);

                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, getRelative2));

                var getType = controls[i].Type;

                // var castCtrl = Convert.ChangeType(controls[i].Control, getType);
                // panel.Controls.Add(controls[i].Control  as , i + tblCnt, 0);


                // grpControl.Controls.Add(panel);
                // table.GroupControl = grpControl;
                // table.TableLayoutPanel = panel;


                // MessageBox.Show(panel.Controls.Count.ToString());



                if (getType != null)
                {
                    if (getType.Name is "GridLookUpEdit")
                    {
                        //  controls[i].Control = controls[i].Control as GridLookUpEdit;
                        //   controls[i].Control = controls[i].Control as GridLookUpEdit;
                        //  (controls[i].Control as GridLookUpEdit).ForceInitialize();
                        panel.Controls.Add(controls[i].Control as GridLookUpEdit, i + tblCnt, 0);
                        //  (controls[i].Control as GridLookUpEdit).ForceInitialize();
                        //  (controls[i].Control as GridLookUpEdit).Properties.PopulateViewColumns();
                    }

                    //else if (getType.Name is "pnlDateOpen")
                    //    panel.Controls.Add(controls[i].Control as pnlDateOpen, i + tblCnt, 0);

                    else if (getType.Name is "RadioGroup")
                        panel.Controls.Add(controls[i].Control as RadioGroup, i + tblCnt, 0);

                    else if (getType.Name is "MemoEdit")
                        panel.Controls.Add(controls[i].Control as MemoEdit, i + tblCnt, 0);

                    else if (getType.Name is "CheckEdit")
                        panel.Controls.Add(controls[i].Control as CheckEdit, i + tblCnt, 0);

                    else if (getType.Name is "TextEdit")
                        panel.Controls.Add(controls[i].Control as TextEdit, i + tblCnt, 0);

                    else if (getType.Name is "ToggleSwitch")
                        panel.Controls.Add(controls[i].Control as ToggleSwitch, i + tblCnt, 0);

                    else if (getType.Name is "PictureEdit")
                        panel.Controls.Add(controls[i].Control as PictureEdit, i + tblCnt, 0);

                    else if (getType.Name is "LabelControl")
                        panel.Controls.Add(controls[i].Control as LabelControl, i + tblCnt, 0);

                    else if (getType.Name is "CheckedListBoxControl")
                        panel.Controls.Add(controls[i].Control as CheckedListBoxControl, i + tblCnt, 0);

                    //else if (getType.Name is "Panel")
                    //    panel.Controls.Add(controls[i].Control as Panel, i + tblCnt, 0);

                    else if (getType.Name is "GroupControl")
                        panel.Controls.Add(controls[i].Control as GroupControl, i + tblCnt, 0);

                }
                else
                    panel.Controls.Add(controls[i].Control, i + tblCnt, 0);

                //  MessageBox.Show(panel.Controls.Count.ToString());

                //  panel.Controls.Add(controls[i].Control, i + tblCnt, 0);
                //  panel.Controls.Add(controls[i].Control, i + tblCnt, 0);

                _font.ChangeFont(controls[i].Control, fontSize);
                controls[i].Control.Dock = DockStyle.Fill;
                controls[i].Control.Margin = new Padding(0);
            }
            grpControl.Controls.Add(panel);

            table.GroupControl = grpControl;
            table.GroupControl.Dock = DockStyle.Fill;
            // table.GroupControl.

            table.TableLayoutPanel = panel;
            return table;
        }

        #endregion

        #region Model Control DataLayout

        public class modelDataTable
        {
            public Int64 Code { get; set; }
            public string Title { get; set; }
        }
        public class modelDataTable2
        {
            public Guid Code { get; set; }
            public string Title { get; set; }
        }
        public static GridLookUpEdit ModelGridToDataLayout(string name, DataTable dtData, string valueMember, string displayMember, string nullText, TextEditStyles styles = TextEditStyles.Standard)
        {

            GridLookUpEdit gridComboEdit = new GridLookUpEdit
            {
                Name = name,

                Properties =
                {
                    NullText = nullText,
                    ValueMember = valueMember,
                    DisplayMember = displayMember,
                    DataSource = dtData,
                    LookAndFeel =
                    {
                        UseDefaultLookAndFeel = false, SkinName = "Office 2019 Colorful"
                    },
                    // LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center,
                            VAlignment = VertAlignment.Center
                        }
                    },
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceDropDown =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center,
                            VAlignment = VertAlignment.Center
                        }
                    },
                    AllowNullInput = DefaultBoolean.True
                },
            };
            // gridComboEdit.RightToLeft = RightToLeft.No;
            _font.ChangeFont(gridComboEdit, 13);
            // var gridView = (GridLookUpEdit)gridComboEdit.Properties.PopupView;
            // GridView gridView = (GridView)gridComboEdit.Properties.PopupView;

            gridComboEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;

            gridComboEdit.Properties.View.OptionsFind.AllowFindPanel = true;
            gridComboEdit.Properties.View.OptionsFind.FindMode = FindMode.Always;
            gridComboEdit.Properties.View.OptionsFind.AlwaysVisible = true;
            gridComboEdit.Properties.View.OptionsFind.Condition = FilterCondition.Contains;

            //gridComboEdit.Properties.View.OptionsFind.ParserKind=

            gridComboEdit.Properties.View.OptionsView.ShowAutoFilterRow = true;

            gridComboEdit.Properties.View.OptionsFilter.AllowFilterIncrementalSearch = true;
            gridComboEdit.Properties.View.OptionsFilter.AllowFilterEditor = true;
            gridComboEdit.Properties.View.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            //  gridComboEdit.Properties.View.OptionsFind.Condition = FilterCondition.Contains;
            gridComboEdit.Properties.ImmediatePopup = true;
            gridComboEdit.Properties.TextEditStyle = styles;
            gridComboEdit.Properties.PopulateViewColumns();
            gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            // gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            // gridComboEdit.Properties.PopupView.Appearance.UpdateRightToLeft(true);

            //var gg = ((GridView) gridComboEdit.Properties.PopupView);
            //gg.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Center;
            //gg.Appearance.Row.TextOptions.VAlignment = VertAlignment.Center;
            //for (int i = 0; i < gridComboEdit.Properties.View.Columns.Count; i++)
            //{
            //    gridComboEdit.Properties.PopupView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //    gridComboEdit.Properties.PopupView.Columns[i].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //}

            gridComboEdit.Properties.View.RowStyle += EventRowStyle_RowStyle;
            gridComboEdit.ProcessNewValue += GridComboEdit_ProcessNewValue;
            gridComboEdit.CustomDisplayText += (s, e) =>
            {
                if (e.Value == null)
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(183, 183, 183);
                else
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            gridComboEdit.TextChanged += (s, e) =>
            {
                gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            // gridView.View.RowStyle += test_View_RowStyle;
            // ColumnView getData = gridComboEdit.Properties.PopupView;

            return gridComboEdit;
        }
        public static GridLookUpEdit ModelGridToDataLayout(string name, List<modelDataTable2> dtData, string nullText, TextEditStyles styles = TextEditStyles.Standard)
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("Code"));
            _dt.Columns.Add(new DataColumn("Title"));

            foreach (modelDataTable2 i in dtData)
            {
                _dt.Rows.Add(i.Code, i.Title);
            }

            GridLookUpEdit gridComboEdit = new GridLookUpEdit
            {
                Name = name,
                Properties =
                {
                    NullText = nullText,
                    ValueMember = "Code",
                    DisplayMember = "Title",
                    DataSource = _dt,
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceDropDown =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    AllowNullInput=DefaultBoolean.True
                },
            };
            gridComboEdit.RightToLeft = RightToLeft.No;
            _font.ChangeFont(gridComboEdit, 13);
            GridView gridView = (GridView)gridComboEdit.Properties.PopupView;

            gridView.OptionsFind.AllowFindPanel = true;
            gridView.OptionsFind.FindMode = FindMode.Always;
            gridView.OptionsFind.AlwaysVisible = true;
            gridView.OptionsFind.Condition = FilterCondition.Contains;

            gridView.OptionsView.ShowAutoFilterRow = true;

            gridView.OptionsFilter.AllowFilterIncrementalSearch = true;
            gridView.OptionsFilter.AllowFilterEditor = true;
            gridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            // gridView.OptionsLayout.AllowNullInput = DefaultBoolean.True;

            gridComboEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.Properties.ImmediatePopup = true;
            gridComboEdit.Properties.TextEditStyle = styles;
            gridComboEdit.Properties.PopulateViewColumns();
            gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            //for (int i = 0; i < gridComboEdit.Properties.View.Columns.Count; i++)
            //{
            //    gridComboEdit.Properties.PopupView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //    gridComboEdit.Properties.PopupView.Columns[i].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //}

            gridComboEdit.Properties.View.RowStyle += EventRowStyle_RowStyle;
            gridComboEdit.ProcessNewValue += GridComboEdit_ProcessNewValue;
            gridComboEdit.CustomDisplayText += (s, e) =>
            {
                if (e.Value == null)
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(183, 183, 183);
                else
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            // gridView.View.RowStyle += test_View_RowStyle;
            // ColumnView getData = gridComboEdit.Properties.PopupView;

            return gridComboEdit;
        }

        public static GridLookUpEdit ModelGridToDataLayout(string name, List<modelDataTable> dtData, string nullText, TextEditStyles styles = TextEditStyles.Standard)
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("Code"));
            _dt.Columns.Add(new DataColumn("Title"));

            foreach (modelDataTable i in dtData)
            {
                _dt.Rows.Add(i.Code, i.Title);
            }

            GridLookUpEdit gridComboEdit = new GridLookUpEdit
            {
                Name = name,
                Properties =
                {
                    NullText = nullText,
                    ValueMember = "Code",
                    DisplayMember = "Title",
                    DataSource = _dt,
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceDropDown =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    AllowNullInput=DefaultBoolean.True
                },
            };
            gridComboEdit.RightToLeft = RightToLeft.No;
            _font.ChangeFont(gridComboEdit, 13);
            GridView gridView = (GridView)gridComboEdit.Properties.PopupView;

            gridView.OptionsFind.AllowFindPanel = true;
            gridView.OptionsFind.FindMode = FindMode.Always;
            gridView.OptionsFind.AlwaysVisible = true;
            gridView.OptionsFind.Condition = FilterCondition.Contains;

            gridView.OptionsView.ShowAutoFilterRow = true;

            gridView.OptionsFilter.AllowFilterIncrementalSearch = true;
            gridView.OptionsFilter.AllowFilterEditor = true;
            gridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            // gridView.OptionsLayout.AllowNullInput = DefaultBoolean.True;

            gridComboEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.Properties.ImmediatePopup = true;
            gridComboEdit.Properties.TextEditStyle = styles;
            gridComboEdit.Properties.PopulateViewColumns();
            gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            //for (int i = 0; i < gridComboEdit.Properties.View.Columns.Count; i++)
            //{
            //    gridComboEdit.Properties.PopupView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //    gridComboEdit.Properties.PopupView.Columns[i].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //}

            gridComboEdit.Properties.View.RowStyle += EventRowStyle_RowStyle;
            gridComboEdit.ProcessNewValue += GridComboEdit_ProcessNewValue;
            gridComboEdit.CustomDisplayText += (s, e) =>
            {
                if (e.Value == null)
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(183, 183, 183);
                else
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            // gridView.View.RowStyle += test_View_RowStyle;
            // ColumnView getData = gridComboEdit.Properties.PopupView;

            return gridComboEdit;
        }
        private static void GridComboEdit_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string && string.IsNullOrEmpty(e.DisplayValue as string))
            {
                e.DisplayValue = null;
                e.Handled = true;
            }
        }

        class modelMonth
        {
            public int Index { get; set; }
            public string Month { get; set; }
        }
        public static GridLookUpEdit ModelMonth(string name)
        {
            var cmbMonth = ModelGridToDataLayout(name, new List<modelMonth>
            {
                new modelMonth {Index = 1, Month = "فروردین"},
                new modelMonth {Index = 2, Month = "اردیبهشت"},
                new modelMonth {Index = 3, Month = "خرداد"},
                new modelMonth {Index = 4, Month = "تیر"},
                new modelMonth {Index = 5, Month = "مرداد"},
                new modelMonth {Index = 6, Month = "شهریور"},
                new modelMonth {Index = 7, Month = "مهر"},
                new modelMonth {Index = 8, Month = "آبان"},
                new modelMonth {Index = 9, Month = "آذر"},
                new modelMonth {Index = 10, Month = "دی"},
                new modelMonth {Index = 11, Month = "بهمن"},
                new modelMonth {Index = 12, Month = "اسفند"},
            }, "Index", "Month", "انتخاب ماه");
            return cmbMonth;
        }



        public static event EventHandler<Model_GLE_GetValue> GetValue_GLUE_EventArgs;
        public class Model_GLE_GetValue : EventArgs
        {
            public string NameCtrl { get; set; }
            public object Data { get; set; }
        }

        public static GridLookUpEdit ModelGridToDataLayout<EF>(string name, List<EF> dtData, string valueMember, string displayMember, string nullText, TextEditStyles styles = TextEditStyles.Standard, float sizeFont = 13f) where EF : class
        {
           
            GridLookUpEdit gridComboEdit = new GridLookUpEdit
            {
                Name = name,
                RightToLeft = RightToLeft.Yes,
                Properties =
                {
                    NullText = nullText,
                    ValueMember = valueMember,
                    DisplayMember = displayMember,
                    DataSource = dtData,
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                },
            };

       

            gridComboEdit.EditValueChanged += (s1, e1) =>
            {
                var dr = (gridComboEdit.Properties.GetRowByKeyValue(gridComboEdit.EditValue) as EF);
                GetValue_GLUE_EventArgs?.Invoke(s1, new Model_GLE_GetValue { NameCtrl = name, Data = dr });
            };

            //GetValue_EventArgs += (s, e) =>
            //{
            //    var receivedData = e.Data;
            //    // انجام عملیات با داده‌های دریافت شده
            //};


            gridComboEdit.Properties.PopupView.CellValueChanged += (s, e) =>
            {
                gridComboEdit.Properties.Appearance.ForeColor = string.IsNullOrEmpty(gridComboEdit.Text)
                    ? Color.FromArgb(165, 13, 13)
                    : Color.Black;
            };

            _fontBold.ChangeFont(gridComboEdit, sizeFont);
            GridView gridView = (GridView)gridComboEdit.Properties.PopupView;

            gridView.OptionsView.ShowColumnHeaders = false; // بررسی شود - نمایش نام ستون ها
            gridView.OptionsFind.AllowFindPanel = true;
            gridView.OptionsFind.FindMode = FindMode.Always;
            gridView.OptionsFind.AlwaysVisible = true;

            gridView.OptionsView.ShowAutoFilterRow = true;

            gridView.OptionsFilter.AllowFilterIncrementalSearch = true;
            gridView.OptionsFilter.AllowFilterEditor = true;
            gridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            gridComboEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.Properties.ImmediatePopup = true;
            gridComboEdit.Properties.TextEditStyle = styles;
            gridComboEdit.Properties.PopulateViewColumns();
            gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            gridComboEdit.Properties.AllowNullInput = DefaultBoolean.True;

            gridComboEdit.Properties.View.RowStyle += EventRowStyle_RowStyle;
            gridComboEdit.KeyDown += GridComboEdit_KeyDown;
            gridComboEdit.CustomDisplayText += (s, e) =>
            {
                gridComboEdit.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };

         
            // gridView.View.RowStyle += test_View_RowStyle;
            // ColumnView getData = gridComboEdit.Properties.PopupView;

       

            return gridComboEdit;
        }
        public static GroupControl ModelGridToDataLayoutBtn<EF>(string name, List<EF> dtData, string valueMember, string displayMember, string nullText,Action action=null, TextEditStyles styles = TextEditStyles.Standard, float sizeFont = 13f)
        {
            GridLookUpEdit gridComboEdit = new GridLookUpEdit
            {
                Name = name,
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                Properties =
                {
                    NullText = nullText,
                    ValueMember = valueMember,
                    DisplayMember = displayMember,
                    DataSource = dtData,
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                },
            };

            gridComboEdit.Properties.PopupView.CellValueChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(gridComboEdit.Text))
                {
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(165, 13, 13);
                }
                else
                {
                    gridComboEdit.Properties.Appearance.ForeColor = Color.Black;
                }
            };

            _font.ChangeFont(gridComboEdit, sizeFont);
            GridView gridView = (GridView)gridComboEdit.Properties.PopupView;

            gridView.OptionsFind.AllowFindPanel = true;
            gridView.OptionsFind.FindMode = FindMode.Always;
            gridView.OptionsFind.AlwaysVisible = true;

            gridView.OptionsView.ShowAutoFilterRow = true;

            gridView.OptionsFilter.AllowFilterIncrementalSearch = true;
            gridView.OptionsFilter.AllowFilterEditor = true;
            gridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            gridComboEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.Properties.ImmediatePopup = true;
            gridComboEdit.Properties.TextEditStyle = styles;
            gridComboEdit.Properties.PopulateViewColumns();
            gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            gridComboEdit.Properties.AllowNullInput = DefaultBoolean.True;

            gridComboEdit.Properties.View.RowStyle += EventRowStyle_RowStyle;
            gridComboEdit.KeyDown += GridComboEdit_KeyDown;
            gridComboEdit.CustomDisplayText += (s, e) =>
            {
                if (e.Value == null)
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(183, 183, 183);
                else
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };

            gridComboEdit.Properties.DoubleClick += (s, e) =>
            {
                gridComboEdit.SelectAll();
            };


            GroupControl groupControl = new GroupControl
            {
                Name = gridComboEdit.Name, 
                Dock = DockStyle.None, 
                ShowCaption = false,
                BorderStyle = BorderStyles.NoBorder
            };
            // {Name = "GridPnl_" + gridComboEdit.Name, Dock = DockStyle.Fill,ShowCaption = false,BorderStyle = BorderStyles.NoBorder};
     

            #region Button

            if (action != null)
            {
                var font = _fontBold.ChangeFont();

                SimpleButton btnFastToday = new SimpleButton
                {
                    Visible = true,
                    Text = @"+",
                    Dock = DockStyle.Left,
                    Height = groupControl.Height - 2,
                    Font = font,
                    Width = 50
                };
                btnFastToday.LookAndFeel.UseDefaultLookAndFeel = false;
                btnFastToday.LookAndFeel.SkinName = "Glass Oceans";
                btnFastToday.Click += (s1, e1) => { action(); };
                //groupControl.Controls.Add(gridComboEdit);
                groupControl.Controls.Add(btnFastToday);
                //gridComboEdit.BringToFront();
            }
            groupControl.Controls.Add(gridComboEdit);
            gridComboEdit.BringToFront();
            //gridComboEdit.Click -= dateCalen_Click;
            //gridComboEdit.Click += dateCalen_Click;

            #endregion


            //  groupControl.BringToFront();
            return groupControl;
        }
        public static GroupControl ModelGridToDataLayout2(string name, DataTable dtData, string valueMember, string displayMember, string nullText, TextEditStyles styles = TextEditStyles.Standard, float sizeFont = 13f)
        {
            GridLookUpEdit gridComboEdit = new GridLookUpEdit
            {
                Name = name,
                Dock = DockStyle.Fill,
                Properties =
                {
                    NullText = nullText,
                    ValueMember = valueMember,
                    DisplayMember = displayMember,
                    DataSource = dtData,
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}
                    },
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                },
            };

            gridComboEdit.Properties.PopupView.CellValueChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(gridComboEdit.Text))
                {
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(165, 13, 13);
                }
                else
                {
                    gridComboEdit.Properties.Appearance.ForeColor = Color.Black;
                }
            };

            _font.ChangeFont(gridComboEdit, sizeFont);
            GridView gridView = (GridView)gridComboEdit.Properties.PopupView;

            gridView.OptionsFind.AllowFindPanel = true;
            gridView.OptionsFind.FindMode = FindMode.Always;
            gridView.OptionsFind.AlwaysVisible = true;

            gridView.OptionsView.ShowAutoFilterRow = true;

            gridView.OptionsFilter.AllowFilterIncrementalSearch = true;
            gridView.OptionsFilter.AllowFilterEditor = true;
            gridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual;

            gridComboEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;
            gridComboEdit.Properties.ImmediatePopup = true;
            gridComboEdit.Properties.TextEditStyle = styles;
            gridComboEdit.Properties.PopulateViewColumns();
            gridComboEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            gridComboEdit.Properties.AllowNullInput = DefaultBoolean.True;

            gridComboEdit.Properties.View.RowStyle += EventRowStyle_RowStyle;
            gridComboEdit.KeyDown += GridComboEdit_KeyDown;
            gridComboEdit.CustomDisplayText += (s, e) =>
            {
                if (e.Value == null)
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(183, 183, 183);
                else
                    gridComboEdit.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            // gridView.View.RowStyle += test_View_RowStyle;
            // ColumnView getData = gridComboEdit.Properties.PopupView;
            // LayoutControlGroup

            //  LayoutControlGroup groupControl = new LayoutControlGroup
            GroupControl groupControl = new GroupControl
            { Name = gridComboEdit.Name, Dock = DockStyle.None, ShowCaption = false, BorderStyle = BorderStyles.NoBorder };
            // {Name = "GridPnl_" + gridComboEdit.Name, Dock = DockStyle.Fill,ShowCaption = false,BorderStyle = BorderStyles.NoBorder};
            groupControl.Controls.Add(gridComboEdit);
            groupControl.Height = 0;
            groupControl.Dock = DockStyle.Fill;
            //  groupControl.BringToFront();
            return groupControl;
        }

        private static void GridComboEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                SendKeys.SendWait("{BACKSPACE}");

            //GridLookUpEdit editor = sender as GridLookUpEdit;
            //if (editor.Text.Length == 0 && (e.KeyData == Keys.Back || e.KeyData == Keys.Delete))
            //{
            //    editor.EditValue = string.Empty;
            //    e.Handled = true;
            //}

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="_checked">مقدار پیش فرض</param>
        /// <param name="enabled">فعال/غیرفعال</param>
        /// <param name="superTip"></param>
        /// <returns></returns>
        public static CheckEdit ModelCheckEdit(string name, CheckState _checked, bool enabled = true, string superTip = "")
        {
            var font = _font.ChangeFont();
            var Ctrl = new CheckEdit
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                CheckState = _checked,
                Properties =
                {
                    NullText = superTip,
                    GlyphAlignment = HorzAlignment.Center,
                    GlyphVAlignment = VertAlignment.Center,
                    GlyphVerticalAlignment = VertAlignment.Center,
                    Appearance = {BackColor = Color.Transparent, TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}},
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled = {BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "VS2010"},

                    CheckBoxOptions = {Style = CheckBoxStyle.Custom},
                    ImageOptions =
                    {
                        SvgImageChecked = Properties.Resources.Check_New_2, SvgImageUnchecked = Properties.Resources.UnCheck_New_2, SvgImageSize = new Size(20, 20)
                    }
                },

                #endregion
            };




            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            return Ctrl;
        }
        public static TextEdit ModelTextEditIP(string name, string nullText, bool enabled = true)
        {
            var font = _font.ChangeFont();

            var Ctrl = new TextEdit
            {

                Name = name,
                Font = font,
                Enabled = enabled,

                Properties =
                {
                    NullText = nullText,
                    Mask =
                    {
                        MaskType = MaskType.RegEx,
                        EditMask = "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)"
                    },
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                   // MaxLength = maxLength
                }
            };
            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            return Ctrl;
        }
        public static TextEdit ModelTextEdit(string name, int maxLength, string nullText, bool enabled = true)
        {
            var font = _font.ChangeFont();
            var Ctrl = new TextEdit
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                RightToLeft = RightToLeft.Yes,
                Properties =
                {
                    NullText = nullText,

                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Foggy"},
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    MaxLength = maxLength
                }
            };

            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            return Ctrl;
        }
        public static TextEdit ModelTextEditPrice(string name, int maxLength, string nullText, bool enabled = true, string titleLeft = "تومان", float fontSize = 13f)
        {
            var font = _fontBold.ChangeFont(fontSize);
            var Ctrl = new TextEdit
            {
                RightToLeft = RightToLeft.No,
                Name = name,
                Font = font,
                Enabled = enabled,

                Properties =
                {

                    AllowMouseWheel = false,
                    NullText = nullText,
                    Mask =
                    {
                        MaskType = MaskType.Numeric,
                        EditMask = @"N0",
                        UseMaskAsDisplayFormat = true,
                        SaveLiteral = false,

                    },
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center,
                            VAlignment = VertAlignment.Center,
                            RightToLeft = false
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154)},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    MaxLength = maxLength
                }
            };


            LabelControl lblT = new LabelControl
            {
                Text = titleLeft,
                Appearance = { ForeColor = Color.FromArgb(92, 0, 255), TextOptions = { HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center } },
                AutoSizeMode = LabelAutoSizeMode.None,
                Font = _fontBold.ChangeFont(fontSize - 1),
                Left = Ctrl.Left,
                Height = Ctrl.Height - 2,
                Width = 38,
            };
            lblT.Click += (s1, e1) =>
            {
                Ctrl.Focus();
            };
            Ctrl.Controls.Add(lblT);

            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            //Ctrl.Spin += (s, e) =>
            //{
            //    e.Handled = fa;
            //};

            return Ctrl;
        }
        public static TextEdit ModelTextEditNumber(string name, int maxLength, string nullText, bool enabled = true, float fontSize = 13f, bool jodakonnade = false, string n = "N0")
        {
            var font = _font.ChangeFont(fontSize);
            var Ctrl = new TextEdit
            {
                RightToLeft = RightToLeft.No,
                Name = name,
                Font = font,
                Enabled = enabled,
                Properties =
                {
                    NullText = nullText,

                  //  Mask =
                  //  {

                        //// اگر وقدار ورودی false
                        //MaskType = MaskType.RegEx,        // ← تغییر به RegEx
                        //EditMask = @"\d*",                // ← فقط اعداد 0-9
                        //UseMaskAsDisplayFormat = false,
                        //SaveLiteral = false,
                        //ShowPlaceHolders = false

                        //// اگر وقدار ورودی true
                        //MaskType = MaskType.Numeric,
                        //EditMask = n,
                        //UseMaskAsDisplayFormat = useMaskAsDisplayFormat,
                        //SaveLiteral = true,
                        //ShowPlaceHolders = false
                   // },
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center, RightToLeft = false
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154)},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    MaxLength = maxLength
                }
            };
            if (jodakonnade)
            {
                // با جداکننده (مثلاً 1,000,000)
                Ctrl.Properties.Mask.MaskType = MaskType.Numeric;
                Ctrl.Properties.Mask.EditMask = n;
                Ctrl.Properties.Mask.UseMaskAsDisplayFormat = false;
                //Ctrl.Properties.Mask.SaveLiteral = true;
                //Ctrl.Properties.Mask.ShowPlaceHolders = false;
            }
            else
            {
                // حالت بدون جداکننده با تعداد اعشار دلخواه
                if (n.ToLower() == "n0")
                {
                    // فقط اعداد صحیح (بدون اعشار)
                    Ctrl.Properties.Mask.MaskType = MaskType.RegEx;
                    Ctrl.Properties.Mask.EditMask = @"\d*";
                }
                else
                {
                    int getN = Convert.ToInt32(n.Substring(1, 1));
                    // با اعشار (مثلاً 1234.56)
                    Ctrl.Properties.Mask.MaskType = MaskType.RegEx;
                    // الگوی Regex برای اعداد با اعشار
                    //Ctrl.Properties.Mask.EditMask = $@"\d*\.?\d{{0,{getN}}}";

                    Ctrl.Properties.Mask.EditMask = $@"\d*{"."}?\d{{0,{getN}}}";
                }

                Ctrl.Properties.Mask.UseMaskAsDisplayFormat = false;
                Ctrl.Properties.Mask.SaveLiteral = false;
                Ctrl.Properties.Mask.ShowPlaceHolders = false;
            }


            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            return Ctrl;
        }
        public static TextEdit ModelTextEditPassword(string name, int minLength, int maxLength, string nullText, bool enabled = true, bool dblShowPass = false, float fontSize = 13f)
        {

            var font = _font.ChangeFont(fontSize);
            var Ctrl = new TextEdit
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                Properties =
                {
                    PasswordChar = '*',
                    NullText = nullText,

                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center,
                            RightToLeft = true
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154)},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    MaxLength = maxLength
                }
            };
            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            Ctrl.Leave += (s, e) =>
            {
                if (Ctrl.Text.Length != 0)
                    if (Ctrl.Text.Length < minLength)
                    {
                        Frm_MSG frmMsg = new Frm_MSG("کلمه عبور نمی تواند از" + " " + minLength + " " + "کمتر باشد!", Class_Text.Msg_Name, 1, false, Color.Red);
                        frmMsg.ShowDialog();
                        Ctrl.Select();
                        Ctrl.Focus();
                    }

            };
            if (dblShowPass)
            {
                Ctrl.DoubleClick += (s1, e1) => Ctrl.Properties.PasswordChar = Ctrl.Properties.PasswordChar == default ? '*' : default;

            }

            return Ctrl;
        }
        public static MemoEdit ModelLayoutMemoEdit(string name, int maxLenth, string nullText, bool enabled = true)
        {
            var memoE = new MemoEdit
            {
                Name = name,
                Enabled = enabled,
                RightToLeft = RightToLeft.Yes,
                Properties =
                {

                    AutoHeight = false,
                    NullText = nullText,

                    MaxLength = maxLenth,
                    Appearance =
                    {
                        BackColor = Color.White,
                        // ParentAppearance = { BackColor = Color.Red},
                        TextOptions =
                        {
                            RightToLeft = true, HAlignment = HorzAlignment.Near, VAlignment = VertAlignment.Center
                        }
                    },
                    AppearanceFocused =
                    {
                        //BackColor = Color.FromArgb(255, 241, 242, 154),
                        BackColor = Color.FromArgb(255, 242, 242, 217),
                        //  ParentAppearance = { BackColor = Color.Green},
                        TextOptions =
                        {
                            RightToLeft = true, HAlignment = HorzAlignment.Near, VAlignment = VertAlignment.Center
                        }
                    },
                    AppearanceDisabled =
                    {
                         BackColor = Color.FromArgb(194, 180, 180, 180),
                        TextOptions =
                        {
                            RightToLeft = true, HAlignment = HorzAlignment.Near, VAlignment = VertAlignment.Center
                        }
                    },
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Xmas 2008 Blue"},
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},

                },


            };
            memoE.KeyPress -= Edit_KeyPress;
            memoE.KeyPress += Edit_KeyPress;
            memoE.CustomDisplayText += (s, e) =>
            {
                memoE.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            memoE.TextChanged += (s, e) =>
            {
                memoE.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            return memoE;
        }
        public static TextEdit ModelDateTime(string name, int maxLength, string nullText, bool enabled = true)
        {
            //var font = _font.ChangeFont();
            var font = _fontBold.ChangeFont();

            var Ctrl = new TextEdit
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                RightToLeft = RightToLeft.No,
                Properties =
                {
                    ReadOnly = true,
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center
                        }
                    },
                    // AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},

                    AppearanceFocused = { BackColor = Color.FromArgb(255, 242, 242, 217) },
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "WXI" },
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI Compact"},
                    MaxLength = maxLength
                }
            };
            SimpleButton btnRm = new SimpleButton { Left = Ctrl.Left, Height = Ctrl.Height - 2, Width = 30, ImageOptions = { SvgImage = MyCom.Properties.Resources.delete, SvgImageSize = new Size(22, 22) } };
            btnRm.LookAndFeel.UseDefaultLookAndFeel = false;
            btnRm.LookAndFeel.SkinName = "Glass Oceans";
            btnRm.Click += (s1, e1) =>
            {
                Ctrl.Text = "";
            };
            Ctrl.Controls.Add(btnRm);


            SimpleButton btnFastToday = new SimpleButton { Visible = false, Text = @"امروز", Left = Ctrl.Right, Height = Ctrl.Height - 2, Font = font, Width = 50 };
            btnFastToday.LookAndFeel.UseDefaultLookAndFeel = false;
            btnFastToday.LookAndFeel.SkinName = "Glass Oceans";
            btnFastToday.Click += (s1, e1) =>
            {
                Ctrl.Focus();
                Ctrl.Text = DateTime.Now.DateTimePersian().Date;
            };
            Ctrl.Controls.Add(btnFastToday);

            Ctrl.Click -= dateCalen_Click;
            Ctrl.Click += dateCalen_Click;

            Ctrl.SizeChanged += (s, e) =>
            {
                //btnFastToday.Left = Ctrl.Right - 57;
                btnFastToday.Left = Ctrl.Width - 50;
                btnFastToday.Visible = true;
            };
            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };

            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
                //MessageBox.Show(btnFastToday.Left.ToString());
                //MessageBox.Show("Right: "+Ctrl.Right.ToString());
                //MessageBox.Show("Width: " + Ctrl.Width.ToString());
            };
         
            return Ctrl;
        }
        //public static pnlDateOpen ModelDate(string name, bool enabled = true, string nullText = "")
        //{
        //    var font = _font.ChangeFont();
        //    var Ctrl = new pnlDateOpen
        //    {
        //        Name = name,
        //        Enabled = enabled,
        //        txtShowCalen =
        //        {
        //            Properties = { NullText = nullText},
        //            Font = font,
        //            LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Xmas 2008 Blue"},
        //        },
        //    };
        //    Ctrl.txtShowCalen.CustomDisplayText += (s, e) =>
        //    {
        //        Ctrl.txtShowCalen.Properties.Appearance.ForeColor = e.Value == null
        //            ? Color.FromArgb(183, 183, 183)
        //            : Color.FromArgb(16, 1, 1);
        //    };
        //    Ctrl.TextChanged += (s, e) =>
        //    {
        //        Ctrl.txtShowCalen.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
        //    };
        //    return Ctrl;
        //}
        public static SimpleButton ModelButton(string name, bool enabled = true, float fontSize = 13f)
        {
            var font = _font.ChangeFont(fontSize);
            var Ctrl = new SimpleButton
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                //LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray" },
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "WXI" },
            };
            return Ctrl;
        }
        public static RadioGroup ModelRadioGroup(string name, List<modelRadioGroup> items, bool enabled = true, BorderStyles borderStyle = BorderStyles.NoBorder, float fontSize = 11.5f, string nullText = "")
        {
            var getControl = new RadioGroup
            {
                Name = name,
                Dock = DockStyle.Fill,
                Properties =
                {
                    NullText = nullText,
                    Columns = items.Max(m => m.Column),
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI Compact"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "The Bezier"},
                     //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "VS2010"},
                    Appearance = {Font = _fontBold.ChangeFont(fontSize)},
                    AppearanceReadOnly = {Font = _fontBold.ChangeFont(fontSize)},
                    AppearanceFocused = {Font = _fontBold.ChangeFont(fontSize)},
                    AppearanceDisabled = {Font = _fontBold.ChangeFont(fontSize)}
                },
                BackColor = Color.Transparent,
                BorderStyle = borderStyle,
            };

            foreach (modelRadioGroup item in items)
            {
                getControl.Properties.Items.Add(new RadioGroupItem(item.Column - 1, item.Field));
            }

            getControl.Height = 0;

            return getControl;
        }
        public class modelRadioGroupControl : modelRadioGroup
        {
            public List<Control> Control { get; set; }
        }
        public static RadioGroup ModelRadioGroup(string name, List<modelRadioGroupControl> items, bool enabled = true, BorderStyles borderStyle = BorderStyles.NoBorder, float fontSize = 13f, string nullText = "")
        {
            var getControl = new RadioGroup
            {
                Name = name,
                Properties =
                {
                    NullText = nullText, Columns = items.Max(m => m.Column),
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "The Bezier"},
                    //   LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "VS2010"},
                },
                BackColor = Color.Transparent,
                BorderStyle = borderStyle
            };

            foreach (modelRadioGroupControl item in items)
            {
                getControl.Properties.Items.Add(new RadioGroupItem(item.Column - 1, item.Field));
            }

            getControl.SelectedIndexChanged += (s, e) =>
            {
                foreach (var t in items)
                {
                    foreach (Control c in t.Control)
                    {
                        Control parent1 = c.Parent;
                        foreach (Control p2 in parent1.Controls)
                        {
                            if (!p2.Text.ToLower().Contains(":"))
                            {
                                p2.Text = "";
                            }
                            p2.Visible = false;
                        }
                    }
                }

                if (getControl.SelectedIndex > -1)
                {
                    var gg = items.First(f =>
                        f.Field == getControl.Properties.Items[getControl.SelectedIndex].Description);
                    foreach (Control c in gg.Control)
                    {
                        Control parent1 = c.Parent;
                        foreach (Control p2 in parent1.Controls)
                        {
                            if (c.Name.ToLower() == p2.Name.ToLower() ||
                                c.Name.ToLower() == p2.Text.ToLower().Replace(":", ""))
                            {
                                p2.Visible = true;
                            }
                        }
                    }
                }

            };

            return getControl;
        }
        public static ToggleSwitch ModelToggleSwitch(string name, modelToggleSwitch toggleSwitch, bool enabled = true, float fontSize = 13f)
        {
            var font = _font.ChangeFont(fontSize);
            var Ctrl = new ToggleSwitch
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                LookAndFeel =
                    {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                //{UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                Properties = { OnText = toggleSwitch.On, OffText = toggleSwitch.Off },
                IsOn = toggleSwitch.IsOn
            };
            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.EditValueChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            return Ctrl;
        }
        public static CheckedListBoxControl ModelCheckedListBox(string name, List<object> items, bool enabled = true, float fontSize = 13f)
        {
            var font = _font.ChangeFont(fontSize);
            var Ctrl = new CheckedListBoxControl
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                RightToLeft = RightToLeft.No,

                CheckOnClick = true,
                // HotTrackSelectMode = HotTrackSelectMode.SelectItemOnClick,
                Appearance =
                    {
                        BackColor = Color.White,
                        //TextOptions =
                        //{
                        //    HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center
                        //}
                    },
                // AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                AppearanceDisabled =
                    {
                       // BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "WXI" },
                //LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Whiteprint" },
                //  MaxLength = maxLength

            };
            foreach (var item in items)
            {
                Ctrl.Items.Add(item);
            }
            //Ctrl.Click -= dateCalen_Click;
            //Ctrl.Click += dateCalen_Click;
            return Ctrl;
        }
        public static CheckedListBoxControl ModelCheckedListBox(string name, List<string> items, bool enabled = true, float fontSize = 13f)
        {

            var font = _font.ChangeFont(fontSize);
            var Ctrl = new CheckedListBoxControl
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                RightToLeft = RightToLeft.No,

                CheckOnClick = true,
                // HotTrackSelectMode = HotTrackSelectMode.SelectItemOnClick,
                Appearance =
                {
                    BackColor = Color.White,
                    //TextOptions =
                    //{
                    //    HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center
                    //}
                },
                // AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                AppearanceDisabled =
                {
                    // BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                },
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "WXI" },
                //LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Whiteprint" },
                //  MaxLength = maxLength

            };
            foreach (var item in items)
            {
                Ctrl.Items.Add(item);
            }
            //Ctrl.Click -= dateCalen_Click;
            //Ctrl.Click += dateCalen_Click;
            return Ctrl;
        }
        public static CheckedListBoxControl ModelCheckedListBox(string name, List<modelCheckedListBoxControl> items, bool enabled = true, float fontSize = 13f)
        {
            var font = _font.ChangeFont(fontSize);
            var Ctrl = new CheckedListBoxControl
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                RightToLeft = RightToLeft.No,

                CheckOnClick = true,
                // HotTrackSelectMode = HotTrackSelectMode.SelectItemOnClick,
                Appearance =
                {
                    BackColor = Color.White,
                    //TextOptions =
                    //{
                    //    HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center
                    //}
                },
                // AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                AppearanceDisabled =
                {
                    // BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                },
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "WXI" },
                //LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Whiteprint" },
                //  MaxLength = maxLength

            };
            foreach (var i in items)
            {
                Ctrl.Items.Add(i.Code, i.Title, i.CheckState, true);
            }
            //Ctrl.Click -= dateCalen_Click;
            //Ctrl.Click += dateCalen_Click;
            return Ctrl;
        }
        public static ProgressBarControl ModelProgressBar(string name, int position, float fontSize = 13f)
        {
            var barProgress = new ProgressBarControl
            {
                Name = name,
                Position = 50,
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Office 2007 Pink", SkinMaskColor = Color.DeepSkyBlue },
                Properties = { ShowTitle = true, Appearance = { Font = _font.ChangeFont(fontSize) } }
            };
            return barProgress;
        }
        public static TimeEdit ModelTimeEdit(string name, string defaultTime, string nullText, bool enabled = true)
        {
            var font = _font.ChangeFont();

            var Ctrl = new TimeEdit
            {
                Name = name,
                Font = font,
                Enabled = enabled,
                EditValue = null,
                Properties =
                {
                    NullText = nullText,
                    TimeEditStyle = TimeEditStyle.TouchUI,
                    MaskSettings = {MaskExpression = "HH:mm"},
                    Appearance = {BackColor = Color.White, TextOptions = {HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center}},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceDisabled = {BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)},
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                }
            };
            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            return Ctrl;
        }


        public class TagPriceControl
        {
            public decimal Min { get; set; }
            public decimal Max { get; set; }

            public TagPriceControl(decimal min, decimal max)
            {
                Max = max;
                Min = min;
            }
        }
        public static TextEdit ModelTextEditPriceControl(string name, int maxLength, TagPriceControl tag, string nullText, bool enabled = true, float fontSize = 13f)
        {
            var font = _font.ChangeFont(fontSize);
            var Ctrl = new TextEdit
            {
                Tag = tag,
                Name = name,
                Font = font,
                Enabled = enabled,
                RightToLeft = RightToLeft.No,
                Properties =
                {
                    NullText = nullText,
                    Mask =
                    {
                        MaskType = MaskType.Numeric,
                        EditMask = @"N0",
                        UseMaskAsDisplayFormat = true,
                        AutoComplete = AutoCompleteType.None
                    },
                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center,
                            VAlignment = VertAlignment.Center,
                            RightToLeft = false
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154)},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                    MaxLength = maxLength
                }
            };

            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);

                if (Ctrl.Tag != null && Ctrl.Text != "")
                {
                    TagPriceControl getControl = (TagPriceControl)Ctrl.Tag;
                    decimal getValue = Convert.ToDecimal(Ctrl.Text);
                    //if (getValue > 0)
                    {
                        if (getControl.Max == 0)
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(241, 37, 37);
                            try
                            {
                                Ctrl.Text = "0";
                            }
                            catch (Exception exception)
                            {

                            }
                        }
                        else if (getValue > getControl.Max)
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(241, 37, 37);
                            try
                            {
                                Ctrl.Text = getControl.Max.ToString();
                            }
                            catch (Exception exception)
                            {

                            }
                        }
                        else if (getValue < getControl.Min)
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(241, 37, 37);
                            try
                            {
                                Ctrl.Text = getControl.Min.ToString();
                            }
                            catch (Exception exception)
                            {

                            }
                        }
                        else if (getValue == 0)
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(241, 37, 37);
                            try
                            {
                                Ctrl.Text = getControl.Min.ToString();
                            }
                            catch (Exception exception)
                            {

                            }
                        }


                        else
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(24, 195, 120);
                        }
                    }
                }
            };

            Ctrl.Enter += (s, e) =>
            {
                if (Ctrl.Tag != null)
                {
                    TagPriceControl getControl = (TagPriceControl)Ctrl.Tag;
                    if (Ctrl.Text != "" && Ctrl.Text != Ctrl.Properties.NullText)
                    {
                        decimal getValue = Convert.ToDecimal(Ctrl.Text);

                        if (getValue > getControl.Max)
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(241, 37, 37);
                            Ctrl.Text = getControl.Max.ToString();
                        }

                        else if (getValue < getControl.Min)
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(241, 37, 37);
                            Ctrl.Text = getControl.Min.ToString();
                        }
                        else
                        {
                            Ctrl.Properties.AppearanceFocused.BackColor = Color.FromArgb(24, 195, 120);
                        }
                    }
                }
            };

            return Ctrl;
        }
        public static TrackBarControl ModelTrackBar(string name, int minValue, int maxValue, int value, int countPercentValue, bool enabled = true)
        {
            var font = _font.ChangeFont();
            TrackBarControl barControl = new TrackBarControl
            {
                Name = name,
                Font = font,
                Enabled = enabled,

                //   Height = 200,

                Properties =
                {
                    ShowValueToolTip = true,
                    AutoSize = false,
                    AutoHeight = true,
                    Minimum = minValue,
                    Maximum = maxValue,
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154),},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled = {BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)},
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2019 Colorful"},
                    LabelAppearance = { Font = font },

                },
                Value = value,
            };
            //SuperToolTip superToolTip1 = new SuperToolTip();
            //ToolTipTitleItem toolTipTitleItem1 = new ToolTipTitleItem();
            //superToolTip1.Items.Add(toolTipTitleItem1);
            //barControl.SuperTip = superToolTip1;

            if (countPercentValue > 0)
            {
                barControl.Properties.ShowLabels = true;
                var getBetWeen = maxValue - minValue + 1; // = 100%
                var calcPercent = (getBetWeen * countPercentValue) / 100;
                if (calcPercent > 0)
                {
                    var calcPercent2 = getBetWeen / calcPercent;

                    for (int i = minValue; i <= maxValue; i += calcPercent2)
                    {
                        barControl.Properties.Labels.Add(new TrackBarLabel(i.ToString(), i, true));
                    }
                }
            }

            barControl.CustomDisplayText += (s, e) =>
            {
                barControl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            barControl.ValueChanged += (s, e) =>
            {
                barControl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
                //  toolTipTitleItem1.Text = barControl.Value.ToString("N0");
            };
            //  superToolTip1.
            return barControl;
        }
        // checkedComboBoxEdit


        public static CheckedComboBoxEdit ModelCheckedComboBoxEdit<T>(string name, List<T> data, string valueMember, string displayMember, string nullText, bool enabled = true, float fontSize = 13f)
        {
            var Ctrl = new CheckedComboBoxEdit
            {
                Name = name,
                // Font = font,
                Enabled = enabled,
                Properties =
                {



                    DisplayMember = displayMember,
                    ValueMember=valueMember,
                    DataSource = data,
                    NullText = nullText,

                    Appearance =
                    {
                        BackColor = Color.White,
                        TextOptions =
                        {
                            HAlignment = HorzAlignment.Center, VAlignment = VertAlignment.Center, RightToLeft = false
                        }
                    },
                    //AppearanceFocused = {BackColor = Color.FromArgb(255, 241, 242, 154)},
                    AppearanceFocused = {BackColor = Color.FromArgb(255, 242, 242, 217)},
                    AppearanceDisabled =
                    {
                        BackColor = Color.FromArgb(201, 209, 210), ForeColor = Color.FromArgb(77, 66, 108)
                    },
                    LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "WXI"},
                    //LookAndFeel = {UseDefaultLookAndFeel = false, SkinName = "Office 2013 Dark Gray"},
                   
                }
            };
            _font.ChangeFont(Ctrl, fontSize);
            Ctrl.CustomDisplayText += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = e.Value == null
                    ? Color.FromArgb(183, 183, 183)
                    : Color.FromArgb(16, 1, 1);
            };
            Ctrl.TextChanged += (s, e) =>
            {
                Ctrl.Properties.Appearance.ForeColor = Color.FromArgb(16, 1, 1);
            };
            //Ctrl.Leave += (s, e) =>
            //{
            //    if (Ctrl.Text.Length != 0)
            //        if (Ctrl.Text.Length < minLength)
            //        {
            //            Frm_MSG frmMsg = new Frm_MSG("کلمه عبور نمی تواند از" + " " + minLength + " " + "کمتر باشد!", Class_Text.Msg_Name, 1, false, Color.Red);
            //            frmMsg.ShowDialog();
            //            Ctrl.Select();
            //            Ctrl.Focus();
            //        }

            //};
            return Ctrl;
        }

        public static void SetValueTrackBar(this TrackBarControl barControl, int minValue, int maxValue, int value, int countPercentValue)
        {
            barControl.Properties.Minimum = minValue;
            barControl.Properties.Maximum = maxValue;
            barControl.Value = value;

            if (countPercentValue > 0)
            {
                barControl.Properties.ShowLabels = true;
                var getBetWeen = maxValue - minValue + 1; // = 100%
                var calcPercent = (getBetWeen * countPercentValue) / 100;
                if (calcPercent > 0)
                {
                    var calcPercent2 = getBetWeen / calcPercent;

                    for (int i = minValue; i <= maxValue; i += calcPercent2)
                    {
                        barControl.Properties.Labels.Add(new TrackBarLabel(i.ToString(), i, true));
                    }
                }
            }
        }

        //CheckedListBoxControl lstSystem = new CheckedListBoxControl{Name = "سی    ستم"};
        public class modelPanelButtonText
        {
            public PanelControl Panel { get; set; }
            public SimpleButton SimpleButton { get; set; }
            public TextEdit TextEdit { get; set; }
        }

        /// <summary>
        /// Panel Merge (BUTTON & TEXTEDIT)
        /// </summary>
        /// <param name="name">جهت Get Or Set Value</param>
        /// <param name="maxLenth"></param>
        /// <param name="image">تصویر باتن</param>
        /// <param name="enabled"></param>
        /// <param name="nullText"></param>
        /// <param name="sizeFont"></param>
        /// <returns></returns>

        public static modelPanelButtonText ModelPanelButtonText(string name, int maxLenth, SvgImage image, bool enabled = true, string nullText = "", int sizeFont = 13)
        {
            modelPanelButtonText controls = new modelPanelButtonText();
            //Panel panel = new Panel { Name = name, Dock = DockStyle.Fill, };
            PanelControl panel = new PanelControl
            {
                Name = name,
                Dock = DockStyle.Fill,
                LookAndFeel =
                    {UseDefaultLookAndFeel = false, SkinName = "WXI"}
            };


            SimpleButton button = new SimpleButton
            {
                Dock = DockStyle.Left,
                Width = 30,
                ImageOptions =
                {
                    SvgImage =image,
                   // Image = image,
                    Location = ImageLocation.MiddleCenter
                },
                LookAndFeel =
                {UseDefaultLookAndFeel = false, SkinName = "WXI"},
            };

            var getTxt = ModelTextEdit(name, maxLenth, nullText, enabled);
            var getFo = getTxt.Font;
            getTxt.Font = new Font(getFo.FontFamily, sizeFont, getFo.Style, getFo.Unit);
            getTxt.Dock = DockStyle.Fill;
            getTxt.ReadOnly = true;

            button.SendToBack();

            panel.Controls.Add(getTxt);
            panel.Controls.Add(button);

            controls.Panel = panel;
            controls.SimpleButton = button;
            controls.TextEdit = getTxt;
            controls.TextEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            controls.TextEdit.Properties.LookAndFeel.SkinName = "WXI";
            return controls;
        }
        public static PictureEdit ModelPicture(string name, string address = null, bool enabled = true)
        {
            PictureEdit pictureEdit = new PictureEdit
            {
                Name = name,
                Enabled = enabled,
                Properties =
                {
                    ShowMenu = false,
                    SizeMode = PictureSizeMode.Zoom
                }
            };
            if (!string.IsNullOrEmpty(address))
                try
                {
                    pictureEdit.Image = Image.FromFile(address);
                }
                catch (Exception)
                {
                    // ignored
                }

            return pictureEdit;
        }
        public class modelRadioGroup
        {
            public string Field { get; set; }
            public int Column { get; set; }
        }
        public class modelToggleSwitch
        {
            public string On { get; set; }
            public string Off { get; set; }
            public bool IsOn { get; set; }
        }

        #region UpdateGridLookUpEdit

        public static void UpdateGridLookUpEdit<EF>(this GridLookUpEdit lookUpEdit, List<EF> _dt)
        {
            lookUpEdit.Properties.DataSource = null;
            lookUpEdit.Properties.DataSource = _dt;
        }
        public static void UpdateGridLookUpEdit(this GridLookUpEdit lookUpEdit, DataTable _dt)
        {
            lookUpEdit.Properties.DataSource = null;
            lookUpEdit.Properties.DataSource = _dt;
        }
        public static void UpdateGridLookUpEdit<EF>(this RepositoryItemGridLookUpEdit lookUpEdit, List<EF> _dt)
        {
            lookUpEdit.DataSource = null;
            lookUpEdit.DataSource = _dt;
        }
        public static void UpdateGridLookUpEdit<EF>(this RepositoryItemGridLookUpEdit lookUpEdit, DataTable _dt)
        {
            lookUpEdit.DataSource = null;
            lookUpEdit.DataSource = _dt;
        }

        #endregion
        private static void dateCalen_Click(object sender, EventArgs e)
        {
            // XtraForm xtraForm = new XtraForm();
            //  ClsCollect.PleaswaitStart(xtraForm);
            TextEdit control = (TextEdit)sender;
            if (!string.IsNullOrEmpty(control.Text))
                control.Text = ModelDateTime2(control.Text);
            else
                control.Text = ModelDateTime2(null);

            //  ClsCollect.PleaswaitEndWithoutCount();
        }
        private static string ModelDateTime2(string setDate)
        {
            frmCalender calender = new frmCalender(setDate);
            calender.ShowDialog();
            return calender.SelDate;
        }
        public static void FormatStringNegativeNumber(this GridLookUpEdit lookUpEdit, string clm)
        {
            lookUpEdit.Properties.View.Columns[clm].DisplayFormat.FormatType = FormatType.Numeric;
            lookUpEdit.Properties.View.Columns[clm].DisplayFormat.FormatString = lookUpEdit.RightToLeft == RightToLeft.Yes
                ? "#,##0.###;#,##0.###-"
                : "#,##0.###;-#,##0.###";
        }
        public static object GetValue(this GridLookUpEdit Layout, string name, bool gValue = true, object defualtValue = null)
        {
            DataTable getF3 = (DataTable)Layout.Properties.DataSource;

            var getValueSel = Layout.EditValue;
            if (getValueSel != null)
            {
                var getChecAny = getF3.AsEnumerable().Any(f => f[Layout.Properties.ValueMember].ToString() == getValueSel.ToString());
                if (getChecAny)
                {
                    var getValue2 = getF3.AsEnumerable().First(f => f[Layout.Properties.ValueMember].ToString() == getValueSel.ToString())[name];
                    // var getValue2 = getF3.AsEnumerable().First(f => f[Layout.Properties.ValueMember].ToString() == getValueSel.ToString())[name];
                    return getValue2;
                }
            }

            var getValue = Layout.Properties.View.GetRowCellValue(Layout.Properties.View.FocusedRowHandle, name);
            var getText = Layout.Properties.View.GetFocusedRowCellDisplayText(name);

            if (gValue)
            {
                if (getValue == null || getValue == "")
                {
                    if (defualtValue != null)
                        return defualtValue;
                }
                else
                    return getValue;

            }

            else
            {
                if (string.IsNullOrEmpty(getText))
                {
                    if (defualtValue != null)
                        return defualtValue;
                }
                return getText;
            }

            return null;
        }
        public static object GetValue<T>(this GridLookUpEdit layout, List<T> modelData, string name, bool gValue = true, object defualtValue = null)
        {
            DataTable getF3 = modelData.ToDataTable();

            var getValueSel = layout.EditValue;
            if (getValueSel != null)
            {
                var getValue2 = getF3.AsEnumerable().First(f => f[layout.Properties.ValueMember].ToString() == getValueSel.ToString())[name];
                // var getValue2 = getF3.AsEnumerable().First(f => f[Layout.Properties.ValueMember].ToString() == getValueSel.ToString())[name];
                return getValue2;
            }

            var getValue = layout.Properties.View.GetRowCellValue(layout.Properties.View.FocusedRowHandle, name);
            var getText = layout.Properties.View.GetFocusedRowCellDisplayText(name);

            if (gValue)
            {
                if (getValue == null || getValue == "")
                {
                    if (defualtValue != null)
                        return defualtValue;
                }
                else
                {
                    // Start
                    var getType = typeof(T).FullName;
                    if (getType == typeof(Guid).FullName)
                    {
                        var getValue2 = Guid.Parse(getValue.ToString());
                        return (T)Convert.ChangeType(getValue2, typeof(T));
                    }
                    // End
                    return getValue;
                }
            }

            else
            {
                if (string.IsNullOrEmpty(getText))
                {
                    if (defualtValue != null)
                        return defualtValue;
                }
                return getText;
            }

            return null;
        }
        #endregion

        #region Work Value DataLayout
        public static string FindControl(this dataLayout Layout, string name)
        {
            // var getCtrl2 = Layout.baseLayout.Controls.ToArray<Control>();
            var getCtrl = Layout.baseLayout.Controls.Find(name, true);
            return getCtrl[0].Name;
        }
        public static Control FindControl2(this dataLayout Layout, string name)
        {
            // var getCtrl2 = Layout.baseLayout.Controls.ToArray<Control>();
            var getCtrl = Layout.baseLayout.Controls.Find(name, true);
            return getCtrl[0];
        }
        public static IEnumerable<Control> GetAllControl(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(GetAllControl).Concat(controls);
        }
        public static void SetValue(this dataLayout Layout, string name, object value)
        {
            var getCtrl = Layout.baseLayout.Controls.Find(name, true);
            getCtrl[0].Text = value.ToString();
        }
        public static void SetValue(this dataLayout Layout, string name, object value, object defualtValue = null)
        {
            var getCtrl = Layout.baseLayout.Controls.Find(name, true);
            if (value != null)
                getCtrl[0].Text = value.ToString();
            else
            {
                if (defualtValue != null)
                    getCtrl[0].Text = defualtValue.ToString();
            }
        }

        /// <summary>
        /// 1-Radio Group: Select Index => ("index=i")
        /// </summary>
        /// <param name="Layout">Layout</param>
        /// <param name="name">فیلد</param>
        /// <param name="value">مقدار</param>
        public static void SetValueType(this dataLayout Layout, string name, object value)
        {
            // var getCtrl = (dynamic)Layout.baseLayout.Controls.Find(name, true)[0];

            var getCtrl1 = Layout.baseLayout.Controls.Find(name, true).ToList();

            if (getCtrl1.Count > 1)
                for (int i = 0; i < getCtrl1.Count; i++)
                {
                    if (getCtrl1[i] is GroupControl)
                    {
                        getCtrl1.RemoveAt(i);
                        break;
                    }
                }

            var getCtrl = (dynamic)getCtrl1[0];

            if (getCtrl is GridLookUpEdit edit)
            {
                if (value != null)
                {
                    edit.Text = value.ToString();
                    edit.EditValue = value.ToString();
                }
                else
                {
                    edit.Text = "";
                    edit.EditValue = "";
                }
            }
            else if (getCtrl is pnlDateOpen pnlDateOpen)
            {
                if (value == null)
                    value = ClsDateTime.TodayPersian().Date;
                pnlDateOpen.txtShowCalen.Text = value.ToString();
            }
            else if (getCtrl is RadioGroup radioGroup)
            {
                if (value.ToString().ToLower().Contains("index="))
                {
                    radioGroup.SelectedIndex = Convert.ToInt32(value.GetNum());
                }
                else
                    for (var i = 0; i < radioGroup.Properties.Items.Count; i++)
                    {
                        RadioGroupItem item = radioGroup.Properties.Items[i];
                        if (item.Description == value.ToString())
                        {
                            radioGroup.SelectedIndex = i;
                            return;
                        }
                    }
            }
            else if (getCtrl is CheckedComboBoxEdit checkedComboBoxEdit)
            {
                checkedComboBoxEdit.EditValue = value == null ? "" : value.ToString();
                checkedComboBoxEdit.RefreshEditValue();
                //  textEdit.Text = value.ToString();
            }
            else if (getCtrl is TextEdit textEdit)
            {
                textEdit.EditValue = value == null ? "" : value.ToString();
                //  textEdit.Text = value.ToString();
            }
            else if (getCtrl is CheckEdit checkEdit)
            {
                checkEdit.Checked = Convert.ToBoolean(value);
            }
            else if (getCtrl is ToggleSwitch toggleSwitch)
            {
                toggleSwitch.IsOn = Convert.ToBoolean(value);
            }
            else if (getCtrl is PictureEdit pictureEdit)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    if (File.Exists(value.ToString()))
                    {
                        pictureEdit.Image = GetCopyImage(value.ToString());
                    }
                }
                else
                    pictureEdit.Image = null;
            }
            else if (getCtrl is CheckedListBoxControl clbc)
            {
                var getConvert = value as List<modelCheckedListBoxControl>;

                //  var getConvert = value;

                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    foreach (CheckedListBoxItem i in clbc.Items)
                    {
                        if (getConvert.Any(a => a.Title == i.Description))
                        {
                            i.CheckState = getConvert.First(f => f.Title == i.Description).CheckState;
                        }
                    }
                }

                // else
                // checkedListBoxContro
            }
            else if (getCtrl is TimeEdit timeEdit)
            {
                timeEdit.EditValue = value == null ? "" : value.ToString();
                //  textEdit.Text = value.ToString();
            }



            //
            else if (getCtrl is PanelControl panelControl)
            {
                foreach (Control control in panelControl.Controls)
                {
                    if (control is TextEdit pnlTextEdit)
                    {
                        pnlTextEdit.Text = value.ToString();
                    }
                }
            }
            else if (getCtrl is Panel panel)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextEdit pnlTextEdit)
                    {
                        pnlTextEdit.Text = value.ToString();
                    }
                }
            }
        }

        //public static Control GetGroup(this dataLayout Layout, string name)
        //{
        //  //  object getValue = null;
        //   // var getCtrl = (dynamic)Layout.baseLayout.Controls.Find(name, true)[0];
        //    var getCtrl = Layout.baseLayout.Controls.Find(name, true)[0];
        //    var getParent = getCtrl;
        //    return getParent;
        //    //            LayoutControlGroup
        //}
        private static Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        public static object GetValue(this dataLayout Layout, string name, bool value = true, object defualtValue = null)
        {
            object getValue = null;
            var getCtrl1 = Layout.baseLayout.Controls.Find(name, true).ToList();

            if (getCtrl1.Count > 1)
                for (int i = 0; i < getCtrl1.Count; i++)
                {
                    if (getCtrl1[i] is GroupControl)
                    {
                        getCtrl1.RemoveAt(i);
                        break;
                    }
                }

            var getCtrl = (dynamic)getCtrl1[0];

            if (getCtrl is GridLookUpEdit edit)
            {
                var getBaseValue = edit.EditValue;
                if (getBaseValue != null)
                {
                    if (edit.Text == edit.Properties.NullText)
                        getValue = "";
                    else
                        getValue = value
                            ? edit.EditValue
                            : edit.Text;
                }
                else
                    getValue = "";
            }

            else if (getCtrl is pnlDateOpen)
                getValue = getCtrl.txtShowCalen.Text == getCtrl.txtShowCalen.Properties.NullText
                    ? ""
                    : getCtrl.txtShowCalen.Text;

            else if (getCtrl is RadioGroup ctrl)
                getValue = ctrl.SelectedIndex > -1
                    ? ctrl.Properties.Items[ctrl.SelectedIndex].Description
                    : null;

            else if (getCtrl is MemoEdit memoEdit)
                getValue = memoEdit.Text == memoEdit.Properties.NullText
                    ? ""
                    : memoEdit.Text.Trim();

            else if (getCtrl is CheckEdit checkEdit)
                getValue = checkEdit.Checked;

            else if (getCtrl is TextEdit textEdit)
                getValue = textEdit.Text == textEdit.Properties.NullText
                    ? ""
                    : textEdit.Text.Trim();

            else if (getCtrl is ToggleSwitch toggleSwitch)
                getValue = toggleSwitch.IsOn;

            else if (getCtrl is PictureEdit pictureEdit)
                getValue = pictureEdit.Image;

            else if (getCtrl is LabelControl labelControl)
                getValue = labelControl.Text;

            else if (getCtrl is CheckedListBoxControl checkedListBoxControl)
            {

                foreach (CheckedListBoxItem item in getCtrl.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        getValue = (CheckedListBoxItemCollection)getCtrl.Items;
                }
                // var getConvert = getCtrl as CheckedListBoxControl;
                // getValue = labelControl.Text;
            }

            else if (getCtrl is Panel panel)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextEdit pnlTextEdit)
                    {
                        getValue = pnlTextEdit.Text == pnlTextEdit.Properties.NullText
                            ? ""
                            : pnlTextEdit.Text.Trim();
                    }
                }
            }
            else if (getCtrl is PanelControl panelControl)
            {
                foreach (Control control in panelControl.Controls)
                {
                    if (control is TextEdit pnlTextEdit)
                    {
                        getValue = pnlTextEdit.Text == pnlTextEdit.Properties.NullText
                            ? ""
                            : pnlTextEdit.Text.Trim();
                    }
                }
            }

            if (getValue == null || getValue == "")
                if (defualtValue != null)
                    return defualtValue;

            return getValue;
        }
        public static T GetValue<T>(this dataLayout Layout, string name, bool value = true, object defualtValue = null)
        {

            object getValue = null;
            var getCtrl1 = Layout.baseLayout.Controls.Find(name, true).ToList();

            if (getCtrl1.Count > 1)
                for (int i = 0; i < getCtrl1.Count; i++)
                {
                    if (getCtrl1[i] is GroupControl)
                    {
                        getCtrl1.RemoveAt(i);
                        break;
                    }
                }

            var getCtrl = (dynamic)getCtrl1[0];

            if (getCtrl is GridLookUpEdit edit)
            {
                var getBaseValue = edit.EditValue;
                if (getBaseValue != null)
                {
                    if (edit.Text == edit.Properties.NullText)
                        getValue = "";
                    else if (value)
                        getValue = edit.EditValue;
                    else
                        getValue = edit.Text;
                }
                else
                    getValue = null;
            }

            else if (getCtrl is pnlDateOpen)
                getValue = getCtrl.txtShowCalen.Text == getCtrl.txtShowCalen.Properties.NullText
                    ? ""
                    : getCtrl.txtShowCalen.Text;

            else if (getCtrl is RadioGroup ctrl)
                getValue = ctrl.SelectedIndex > -1
                    ? ctrl.Properties.Items[ctrl.SelectedIndex].Description
                    : null;

            else if (getCtrl is MemoEdit memoEdit)
                getValue = memoEdit.Text == memoEdit.Properties.NullText
                    ? ""
                    : memoEdit.Text.Trim();

            else if (getCtrl is CheckEdit checkEdit)
                getValue = checkEdit.Checked;

            else if (getCtrl is CheckedComboBoxEdit checkedComboBoxEdit)
                getValue = checkedComboBoxEdit.EditValue;

            else if (getCtrl is TextEdit textEdit)
                if (textEdit.Text == textEdit.Properties.NullText)
                    getValue = "";
                else
                    getValue = textEdit.Text.Trim();

            else if (getCtrl is ToggleSwitch toggleSwitch)
                getValue = toggleSwitch.IsOn;

            else if (getCtrl is PictureEdit pictureEdit)
                getValue = pictureEdit.Image;

            else if (getCtrl is LabelControl labelControl)
                getValue = labelControl.Text;



            else if (getCtrl is CheckedListBoxControl checkedListBoxControl)
            {
                getValue = new List<modelCheckedListBoxControl>();
                foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
                {

                    if (item.CheckState == CheckState.Checked)
                        ((List<modelCheckedListBoxControl>)getValue).Add(new modelCheckedListBoxControl
                        {
                            CheckState = item.CheckState,
                            Title = item.Description,
                            Code = (long)item.Value
                        });

                    // getValue = (CheckedListBoxItemCollection)checkedListBoxControl.Items;
                }
            }
            else if (getCtrl is Panel panel)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextEdit pnlTextEdit)
                    {
                        getValue = pnlTextEdit.Text == pnlTextEdit.Properties.NullText
                            ? ""
                            : pnlTextEdit.Text.Trim();
                    }
                }
            }
            else if (getCtrl is PanelControl panelControl)
            {
                foreach (Control control in panelControl.Controls)
                {
                    if (control is TextEdit pnlTextEdit)
                    {
                        getValue = pnlTextEdit.Text == pnlTextEdit.Properties.NullText
                            ? ""
                            : pnlTextEdit.Text.Trim();
                    }
                }
            }

            if (getValue == null || ReferenceEquals(getValue, ""))
                if (defualtValue != null)
                {
                    return (T)Convert.ChangeType(defualtValue, typeof(T));
                }
                else
                    return default;


            if (getValue == null || ReferenceEquals(getValue, ""))
                if (defualtValue != null)
                {
                    return (T)Convert.ChangeType(defualtValue, typeof(T));
                }
                else
                    return default;

            try
            {
                // Start
                var getType = typeof(T).FullName;
                if (getType == typeof(Guid).FullName)
                {
                    var getValue2 = Guid.Parse(getValue.ToString());
                    return (T)Convert.ChangeType(getValue2, typeof(T));
                }
                // End
                return (T)Convert.ChangeType(getValue, typeof(T));
            }
            catch (Exception e)
            {
                return default;
            }
        }
        public class modelNamePersian
        {
            public string ControlLayoutName { get; set; }
            public string PersianName { get; set; }
        }
        /// <summary>
        /// Null After Save
        /// </summary>
        /// <param name="Layout"></param>   
        /// <param name="NNAS">Not Null After Save</param>
        public static void SetNull(this dataLayout Layout, List<string> NNAS = null)
        {
            Layout.baseLayout.BeginUpdate();

            foreach (Control control in Layout.baseLayout.Controls)
            {
                if (control is GroupControl grp)
                {
                    foreach (Control control2 in grp.Controls)
                        if (control2 is Panel pnl3)
                            foreach (Control control3 in pnl3.Controls)
                            {
                                if (control3 is GroupControl grp2)
                                    foreach (Control control4 in grp2.Controls)
                                    {
                                        if (NNAS != null && NNAS.Any(a => a != control4.Name))
                                            continue;
                                        ctrl(control4);
                                    }
                                else
                                {
                                    if (NNAS != null && NNAS.Any(a => a != control3.Name))
                                        continue;
                                    ctrl(control3);
                                }
                            }
                }
                else
                {
                    if (NNAS != null && NNAS.Any(a => a != control.Name))
                        continue;
                    ctrl(control);
                }
            }

            Layout.baseLayout.EndUpdate();
        }
        private static void ctrl(Control c)
        {
            if (!(c is GroupControl) && !(c is SimpleButton) && !(c is LabelControl))
            {
                c.Text = null;
            }
            if (c is TimeEdit timeEdit)
            {
                timeEdit.EditValue = null;
            }
            if (c is pnlDateOpen open)
            {
                open.txtShowCalen.Text = null;
            }
            if (c is RadioGroup rdo)
            {
                rdo.SelectedIndex = -1;
            }
            if (c is Panel pnl)
            {
                foreach (Control control2 in pnl.Controls)
                {
                    if (control2 is TextEdit pnlTextEdit)
                    {
                        pnlTextEdit.Text = null;
                    }
                }
            }
            if (c is CheckedListBoxControl listBoxControl)
            {
                foreach (CheckedListBoxItem item in listBoxControl.Items)
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }

        #region NullText To PersianName

        public static void AddNullTextToPersianName(this dataLayout Layout)
        {
            Layout.baseLayout.BeginUpdate();

            foreach (Control control in Layout.baseLayout.Controls)
            {
                if (control is GroupControl grp)
                {
                    foreach (Control control2 in grp.Controls)
                        if (control2 is Panel pnl3)
                            foreach (Control control3 in pnl3.Controls)
                            {
                                if (control3 is GroupControl grp2)
                                    foreach (Control control4 in grp2.Controls)
                                    {
                                        AddNullTextToPersianName3(control4);
                                    }
                                else
                                    AddNullTextToPersianName3(control3);
                            }
                }
                else
                    AddNullTextToPersianName3(control);
            }

            Layout.baseLayout.EndUpdate();
        }
        private static void AddNullTextToPersianName3(Control getCtrl)
        {
            var cornflowerBlue = Color.CornflowerBlue;
            if (getCtrl is GridLookUpEdit edit)
            {
                if (edit.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = edit.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    edit.SuperTip = superToolTip;
                }

            }
            else if (getCtrl is pnlDateOpen pnlDateOpen)
            {
                if (pnlDateOpen.txtShowCalen.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = pnlDateOpen.txtShowCalen.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    pnlDateOpen.txtShowCalen.SuperTip = superToolTip;
                }
            }
            else if (getCtrl is RadioGroup radioGroup)
            {
                if (radioGroup.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = radioGroup.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    radioGroup.SuperTip = superToolTip;
                }
            }
            else if (getCtrl is TextEdit textEdit)
            {
                if (textEdit.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = textEdit.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    textEdit.SuperTip = superToolTip;
                }
            }
            if (getCtrl is MemoEdit memoEdit1)
            {
                if (memoEdit1.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = memoEdit1.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    memoEdit1.SuperTip = superToolTip;
                }
            }
            else if (getCtrl is CheckEdit checkEdit)
            {
                if (checkEdit.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = checkEdit.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    checkEdit.SuperTip = superToolTip;
                }
            }
            else if (getCtrl is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = toggleSwitch.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    toggleSwitch.SuperTip = superToolTip;
                }
            }
            else if (getCtrl is PictureEdit pictureEdit)
            {
                if (pictureEdit.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = pictureEdit.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    pictureEdit.SuperTip = superToolTip;
                }
            }
            if (getCtrl is TimeEdit timeEdit)
            {
                if (timeEdit.Properties.NullText != null)
                {
                    ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = timeEdit.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(toolTipTitleItem);
                    timeEdit.SuperTip = superToolTip;
                }
            }
            else if (getCtrl is Panel panel)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextEdit textEdit)
                    {
                        if (textEdit.Properties.NullText != null)
                        {
                            ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = textEdit.Properties.NullText, Appearance = { Font = _font.ChangeFont(), ForeColor = cornflowerBlue } };
                            SuperToolTip superToolTip = new SuperToolTip();
                            superToolTip.Items.Add(toolTipTitleItem);
                            textEdit.SuperTip = superToolTip;
                        }
                    }
                }
            }
        }
        public static void AddPersianName(this dataLayout Layout, List<modelNamePersian> lstPersians)
        {
            foreach (modelNamePersian l in lstPersians)
            {
                ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = l.PersianName, Appearance = { Font = _font.ChangeFont(12), ForeColor = Color.CornflowerBlue } };
                SuperToolTip superToolTip = new SuperToolTip();
                superToolTip.Items.Add(toolTipTitleItem);

                var getCtrl1 = Layout.baseLayout.Controls.Find(l.ControlLayoutName, true).ToList();

                if (getCtrl1.Count > 1)
                    for (int i = 0; i < getCtrl1.Count; i++)
                    {
                        if (getCtrl1[i] is GroupControl)
                        {
                            getCtrl1.RemoveAt(i);
                            break;
                        }
                    }

                var getCtrl = (dynamic)getCtrl1[0];

                if (getCtrl is GridLookUpEdit)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is pnlDateOpen)
                    getCtrl.txtShowCalen.SuperTip = superToolTip;

                else if (getCtrl is RadioGroup)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is MemoEdit)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is CheckEdit)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is TextEdit)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is ToggleSwitch)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is PictureEdit)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is LabelControl)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is CheckedListBoxControl)
                    getCtrl.SuperTip = superToolTip;

                else if (getCtrl is Panel panel)
                {
                    foreach (Control control in panel.Controls)
                    {
                        if (control is TextEdit textEdit)
                        {
                            textEdit.SuperTip = superToolTip;
                        }
                    }
                }
            }
        }
        public static void AddPersianName(this dataLayout Layout, string name, string namePersian)
        {
            ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = namePersian };
            SuperToolTip superToolTip = new SuperToolTip();
            superToolTip.Items.Add(toolTipTitleItem);

            var getCtrl1 = Layout.baseLayout.Controls.Find(name, true).ToList();

            if (getCtrl1.Count > 1)
                for (int i = 0; i < getCtrl1.Count; i++)
                {
                    if (getCtrl1[i] is GroupControl)
                    {
                        getCtrl1.RemoveAt(i);
                        break;
                    }
                }

            var getCtrl = (dynamic)getCtrl1[0];

            if (getCtrl is GridLookUpEdit)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is pnlDateOpen)
                getCtrl.txtShowCalen.SuperTip = superToolTip;

            else if (getCtrl is RadioGroup)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is MemoEdit)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is CheckEdit)
                getCtrl.SuperTip = superToolTip;

            else if (getCtrl is TextEdit)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is ToggleSwitch)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is PictureEdit)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is LabelControl)
                getCtrl.SuperTip = superToolTip;


            else if (getCtrl is CheckedListBoxControl)
                getCtrl.SuperTip = superToolTip;

            else if (getCtrl is Panel panel)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextEdit textEdit)
                    {
                        textEdit.SuperTip = superToolTip;
                    }
                }
            }
        }

        #endregion

        public class modelCheckedListBoxControl : modelDataTable
        {
            public CheckState CheckState { get; set; }
        }
        public static List<modelCheckedListBoxControl> GetValue(this CheckedListBoxControl item)
        {
            var lstItem = new List<modelCheckedListBoxControl>();
            foreach (CheckedListBoxItem i in item.Items)
            {
                lstItem.Add(new modelCheckedListBoxControl
                {
                    Code = Convert.ToInt64(i.Value.ToString()),
                    Title = i.Description,
                    CheckState = i.CheckState
                });
            }

            return lstItem;
        }

        #endregion

        #region مدیریت فیلد دیتا لایوت
        public static void HiddenColumn(this GridLookUpEdit dgv, string nameColumn)
        {
            dgv.Properties.PopupView.Columns[nameColumn].Visible = false;
        }
        public static void HiddenColumn(this dataLayout layout, string nameField)
        {
            var get = layout.baseLayout.Items.FindByName("LCI_" + nameField);
            if (get == null)
            {
                foreach (var item in layout.baseLayout.Items)
                {
                    var i = item as LayoutControlItem;
                    if (i != null && i.Name.Contains("LCI_GRP_"))
                    {
                        var c = i.Control as GroupControl;
                        foreach (var i2 in c.Controls.OfType<TableLayoutPanel>())
                        {
                            foreach (var f in i2.Controls.OfType<Control>())
                            {
                                if (f.Name.Contains(nameField))
                                {
                                    //  i2.Visible = false;
                                    // get.Visibility = LayoutVisibility.Never;
                                    i.Visibility = LayoutVisibility.Never;
                                    return;
                                }
                            }

                        }
                    }
                }
            }
            get.Visibility = LayoutVisibility.Never;

        }
        public static void ShowColumn(this dataLayout layout, string nameField)
        {
            var get = layout.baseLayout.Items.FindByName("LCI_" + nameField);
            if (get == null)
            {
                foreach (var item in layout.baseLayout.Items)
                {
                    var i = item as LayoutControlItem;
                    if (i != null && i.Name.Contains("LCI_GRP_"))
                    {
                        var c = i.Control as GroupControl;
                        foreach (var i2 in c.Controls.OfType<TableLayoutPanel>())
                        {
                            foreach (var f in i2.Controls.OfType<Control>())
                            {
                                if (f.Name.Contains(nameField))
                                {
                                    //  i2.Visible = false;
                                    // get.Visibility = LayoutVisibility.Never;
                                    i.Visibility = LayoutVisibility.Always;
                                    return;
                                }
                            }

                        }
                    }
                }
            }
            get.Visibility = LayoutVisibility.Always;
        }
        public static void ReadOnlyColumn(this dataLayout layout, string nameField, bool readOnly = true)
        {
            layout.baseLayout.BeginUpdate();

            foreach (Control obj in layout.baseLayout.Controls)
            {
                if (obj.Name.ToLower().Contains(nameField.ToLower()))
                {
                    if (obj is ComboBoxEdit boxEdit)
                    {
                        boxEdit.Properties.ReadOnly = readOnly;
                        layout.baseLayout.EndUpdate();
                        return;
                    }
                    if (obj is GridLookUpEdit lookUpEdit)
                    {
                        lookUpEdit.Properties.ReadOnly = readOnly;
                        layout.baseLayout.EndUpdate();
                        return;
                    }
                    if (obj is pnlDateOpen pnlDateOpen)
                    {
                        pnlDateOpen.txtShowCalen.Properties.ReadOnly = readOnly;
                        layout.baseLayout.EndUpdate();
                        return;
                    }
                    if (obj is MemoEdit memoEdit)
                    {
                        memoEdit.Properties.ReadOnly = readOnly;
                        layout.baseLayout.EndUpdate();
                        return;
                    }
                    if (obj is TextEdit textEdit)
                    {
                        textEdit.Properties.ReadOnly = readOnly;
                        layout.baseLayout.EndUpdate();
                        return;
                    }
                }
            }

            layout.baseLayout.EndUpdate();
        }
        public static GridLookUpEdit ConvertGroupToGrid(this GroupControl ctrl)
        {
            try
            {
                return ctrl.Controls[0] as GridLookUpEdit;
            }
            catch (Exception e)
            {
                return ctrl.Controls[1] as GridLookUpEdit;
            }
        }

        //public static GridLookUpEdit ConvertGroupToGrid(this GroupControl ctrl)
        //{
        //    return ctrl.Controls[0] as GridLookUpEdit;
        //}   

        #endregion

        //#endregion

        #region Splitter

        public static void DefaultSettingSplitter(this SplitContainerControl control, bool showCaption)
        {
            control.Panel1.ShowCaption = showCaption;
            control.Panel1.BorderStyle = BorderStyles.Simple;
            control.Panel1.CaptionLocation = Locations.Top;
            control.Panel1.CaptionLocation = Locations.Top;
            control.Panel1.AppearanceCaption.ForeColor = Color.FromArgb(64, 100, 21);
            control.Panel1.Appearance.BackColor = Color.Transparent;

            control.Panel2.ShowCaption = showCaption;
            control.Panel2.BorderStyle = BorderStyles.Simple;
            control.Panel2.CaptionLocation = Locations.Top;
            control.Panel2.CaptionLocation = Locations.Top;
            control.Panel2.AppearanceCaption.ForeColor = Color.FromArgb(64, 100, 21);
            control.Panel2.Appearance.BackColor = Color.Transparent;
            // control.Panel2.AppearanceCaption.ForeColor = Color.SteelBlue;

            //  control.Appearance.

            control.LookAndFeel.UseDefaultLookAndFeel = false;
            control.ShowCaption = showCaption;
            //control.LookAndFeel.SkinName = "WXI";
            //control.LookAndFeel.SkinName = "Office 2019 Colorful";
            // control.LookAndFeel.SkinName = "basic";
            // control.LookAndFeel.SkinName = "Xmas 2008 Blue";
            control.LookAndFeel.SkinName = "London Liquid Sky";
            // control.LookAndFeel.SkinName = "Seven";
        }
        public static void DefaultSettingSplitterNoCaption(this SplitContainerControl control)
        {
            control.Panel1.ShowCaption = false;
            control.Panel1.BorderStyle = BorderStyles.Simple;
            control.Panel1.CaptionLocation = Locations.Top;
            control.Panel1.CaptionLocation = Locations.Top;
            control.Panel1.AppearanceCaption.ForeColor = Color.FromArgb(64, 100, 21);
            control.Panel1.Appearance.BackColor = Color.Transparent;

            control.Panel2.ShowCaption = false;
            control.Panel2.BorderStyle = BorderStyles.Simple;
            control.Panel2.CaptionLocation = Locations.Top;
            control.Panel2.CaptionLocation = Locations.Top;
            control.Panel2.AppearanceCaption.ForeColor = Color.FromArgb(64, 100, 21);
            control.Panel2.Appearance.BackColor = Color.Transparent;
            // control.Panel2.AppearanceCaption.ForeColor = Color.SteelBlue;

            //  control.Appearance.

            control.LookAndFeel.UseDefaultLookAndFeel = false;
            control.ShowCaption = false;
            control.LookAndFeel.SkinName = "Glass Oceans";
            // control.LookAndFeel.SkinName = "basic";
            // control.LookAndFeel.SkinName = "Xmas 2008 Blue";
            //  control.LookAndFeel.SkinName = "London Liquid Sky";
            // control.LookAndFeel.SkinName = "Seven";
        }
        #endregion

        public class ModelFieldvGrid
        {
            public string Caption { get; set; }

            /// <summary>
            /// تک فیلد و
            /// چند فیلدی
            /// </summary>
            public BaseRow TypeField { get; set; }

            /// <summary>
            /// نوع فیلد
            /// TextEdit,
            /// Label,
            /// ComboBox,
            /// Grid
            /// </summary>
            public RepositoryItem RepositoryItem { get; set; }

            /// <summary>
            /// نوع فیلد های چند فیلدی
            /// </summary>
            public List<MultiEditorRowProperties> LstMultiEditorRowProperties { get; set; }

            public string NullText { get; set; }
        }

        #region GridLookUPEdit
        public static object GetText(this GridLookUpEdit upEdit, string clm)
        {
            var getValue =
                upEdit.Properties.PopupView.GetRowCellDisplayText(
                    upEdit.Properties.PopupView.FocusedRowHandle, clm);
            return getValue;
        }
        public static void DisableHideFilterPanelGrid(this GridLookUpEdit gridLookUpEdit, string clm = null)
        {
            gridLookUpEdit.Properties.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridLookUpEdit.Properties.View.OptionsFilter.AllowFilterEditor = false;
            gridLookUpEdit.Properties.View.HideFindPanel();
            if (clm != null)
                gridLookUpEdit.Properties.View.Columns[clm].OptionsFilter.AllowFilter = false;
        }
        public static void DisableHideFilterPanelGrid(this Data_Grid dataGrid, string clm = null)
        {
            dataGrid.DGV_Viw.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            dataGrid.DGV_Viw.OptionsFilter.AllowFilterEditor = false;
            dataGrid.DGV_Viw.HideFindPanel();
            if (clm != null)
                dataGrid.DGV_Viw.Columns[clm].OptionsFilter.AllowFilter = false;
        }
        public static void EnableShowFilterPanelGrid(this Data_Grid dataGrid, string clm = null)
        {
            dataGrid.DGV_Viw.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            dataGrid.DGV_Viw.OptionsFilter.AllowFilterEditor = true;
            dataGrid.DGV_Viw.ShowFindPanel();
            if (clm != null)
                dataGrid.DGV_Viw.Columns[clm].OptionsFilter.AllowFilter = true;
        }

        #endregion

        #region SetComponent Other
        public static string ModelShowCalenDateToGrid(string setDate = null)
        {
            frmCalender calender = new frmCalender(setDate);
            calender.ShowDialog();
            return calender.SelDate;
        }

        #endregion

        #region Summary Custome
        public static GridColumnSummaryItem ConvertItemSummary(this CustomSummaryExistEventArgs e)
        {
            return e.Item as GridColumnSummaryItem;
        }
        public static GridColumnSummaryItem ConvertItemSummary(this CustomSummaryEventArgs e)
        {
            return e.Item as GridColumnSummaryItem;
        }

        public static GridGroupSummaryItem ConvertItemGroupSummary(this CustomSummaryEventArgs e)
        {
            return e.Item as GridGroupSummaryItem;
        }
        #endregion

        #region Ribbon DevExpres Manager
        public static void CustomizeReborn(this RibbonControl _form)
        {
            _form.RightToLeft = RightToLeft.Yes;
            _form.ShowToolbarCustomizeItem = false;
            _form.ShowApplicationButton = DefaultBoolean.False;
            foreach (RibbonPage page in _form.Pages)
            {
                foreach (RibbonPageGroup @group in page.Groups)
                {
                    @group.ShowCaptionButton = false;
                }
            }
        }

        #endregion

        #region Thread Invoke

        static Class_Text classText = new Class_Text();

        public static async Task InvokeThread(this Control control, Action function, bool showWait)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e1) =>
            {
                var getTop = control.Top;
                var getLeft = control.Left;
                var getWidth = control.Width;
                var getHeight = control.Height;

                //  var getParent = control.Parent;
                // 1

                ProgressPanel button = new ProgressPanel
                {
                    Left = getWidth / 2,
                    Top = getHeight / 2,
                    Width = 40,
                    Height = 40,
                    ShowCaption = false,
                    ShowDescription = false,
                    BackColor = Color.Transparent
                    // BackColor = Color.FromArgb(178, 244, 230)
                };
                //  PleaseWaitControl wait1 = new PleaseWaitControl();
                if (showWait)
                {
                    try
                    {
                        control.Invoke(new Action(() =>
                        {
                            //wait1.SetDesktopLocation(500,500);
                            //wait1.ShowDialog();
                            // wait1.load
                            control.Controls.Add(button);
                            // button.BringToFront();
                        }));
                    }
                    catch (Exception e)
                    {
                        var t = MultiLineMsg(e);
                        classText.Frm_Msg(t, "Error", 2, false, new Color(), RightToLeft.No);
                    }
                }

                new Thread(() =>
                {
                    control.Invoke(new Action(() =>
                    {
                        try
                        {
                            function();
                        }
                        catch (Exception e)
                        {
                            var t = MultiLineMsg(e);
                            classText.Frm_Msg(t, "Error", 2, false, new Color(), RightToLeft.No);
                        }
                    }));

                    if (showWait)
                        try
                        {
                            control.Invoke(new Action(() =>
                                {
                                    //  wait1.Hide();
                                    control.Invoke(new Action(() => { control.Controls.Remove(button); }));
                                }));
                        }
                        catch (Exception e)
                        {
                            var t = MultiLineMsg(e);
                            classText.Frm_Msg(t, "Error", 2, false, new Color(), RightToLeft.No);
                        }
                }).Start();
            };
            worker.RunWorkerAsync();
        }

        private static string MultiLineMsg(Exception e)
        {
            string t = "";
            var getMsgLng = e.Message.Length;
            int cntSub = 0;

            while (getMsgLng >= 100)
            {
                t += e.Message.Substring(cntSub, 100) + Environment.NewLine;
                getMsgLng -= 100;
                if (cntSub == 0)
                    cntSub++;
                cntSub += 100;
            }

            t += e.Message.Substring(cntSub, getMsgLng - 1);
            return t;
        }

        #endregion

        #region Public
        public static object GetNumber(this string string1)
        {
            //  string a = "str123";
            string b = "0";
            int val;

            if (string1 != null)
                foreach (var t in string1)
                {
                    if (Char.IsDigit(t))
                        b += t;
                }

            if (b.Length > 0)
                val = int.Parse(b);

            return b;
        }
        public static List<object> OpenExcelSingleColumn(string Range)
        {
            // DevExpress.Spreadsheet.Worksheet
            SpreadsheetControl excel = new SpreadsheetControl();
            List<object> list = new List<object>();
            // Load a workbook from the file.
            // excel.LoadDocument(@"C:\Users\mojtaba.yadavar\Desktop\packing.xlsx", DocumentFormat.Xlsx);


            XtraOpenFileDialog ofd = new XtraOpenFileDialog
            {
                Title = @"LIG",
                CheckFileExists = true,
                KeepPosition = true,
                CheckPathExists = true,
                Filter = @"Excel file|*.xlsx"
            };
            var getQues = ofd.ShowDialog();

            if (getQues == DialogResult.OK)
            {
                excel.LoadDocument(ofd.FileName, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                var column = excel.ActiveWorksheet.Columns[Range].CurrentRegion;
                var endRow = column.BottomRowIndex;

                for (int i = 1; i <= endRow; i++)
                {
                    list.Add(column[i].Value);
                }
            }

            return list;
        }

        public static Image ReadOpenImage(string address)
        {
            byte[] buff = System.IO.File.ReadAllBytes(address);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buff);
            //  layBaseNew.@group.ContentImageOptions.Image = Image.FromStream(ms);
            return Image.FromStream(ms);
        }

        #region Image

        //public static byte[] ConvertImageToByte(Image image)
        //{

        //}

        #endregion

        #endregion

        #region Panel Confirm

        public static WindowsUIButton ConvertPanelConfirmToWinUIButton(this IBaseButton ctrl) => ctrl as WindowsUIButton;

        //((WindowsUIButton) uiPlanning.uiConfirm.Buttons[0]).Caption

        #endregion

        #region Data Table

        public static T GetValue<T>(this DataRow row, string clm, object defaultValue = null)
        {
            if (typeof(T) == typeof(PersianCalendar))
            {
                var getT = row[clm].ToString().ShamsiToMiladi();
                return (T)Convert.ChangeType(getT, typeof(T));
            }
            //if (typeof(T) == typeof(ClsDateTime.ModelDateTimePersian))
            //{
            //    if (row[clm].ToString().Length == 5)
            //    {
            //        var function = DateTime.Now.DateTimePersian();

            //        function.Hour = row[clm].ToString().Substring(0, 2);
            //        function.Minute = row[clm].ToString().Substring(3, 2);

            //        return (T)Convert.ChangeType(function, typeof(T));
            //    }
            //}

            try
            {
                var getValue = (T)Convert.ChangeType(row[clm], typeof(T));
                return getValue;
            }
            catch
            {
                return (T)Convert.ChangeType(defaultValue, typeof(T));
            }
        }


        #endregion

        public static void WaitDownPage(this XtraForm frm, Action action)
        {
            try
            {
                Task.Run(() =>
                {
                    Panel panel = null;
                    Label label = null;
                    bool hasError = false;

                    frm.Invoke(() =>
                    {
                        panel = new Panel
                        {
                            Name = "pnlW",
                            RightToLeft = RightToLeft.Yes,
                            Dock = DockStyle.Bottom,
                            Height = 25,
                            BackColor = Color.FromArgb(72, 139, 145)
                        };

                        ProgressPanel progressPanel = new ProgressPanel
                        {
                            Dock = DockStyle.Right,
                            Width = 35,
                            ShowCaption = false,
                            ShowDescription = false,
                            LookAndFeel =
                            {
                                UseDefaultLookAndFeel = false,
                                SkinName = "WXI",
                                SkinMaskColor = Color.Red
                            }
                        };

                        label = new Label
                        {
                            AutoSize = false,
                            Width = 400,
                            Height = 25,
                            Text = @"لطفا کمی صبر نمایید - در حال پردازش اطلاعات",
                            Dock = DockStyle.Right,
                            TextAlign = ContentAlignment.MiddleLeft,
                            ForeColor = Color.FromArgb(137, 243, 248),
                            RightToLeft = RightToLeft.Yes
                        };

                        _fontBold.ChangeFont(label);
                        frm.AddControl(panel);
                        panel.SendToBack();
                        panel.AddControl(label);
                        panel.AddControl(progressPanel);
                    });

                    // تأخیر برای نمایش پنل
                    Thread.Sleep(300);

                    try
                    {
                        frm.Invoke(action);
                    }
                    catch (Exception ex)
                    {
                        hasError = true;

                        frm.Invoke(() =>
                        {
                            if (label != null)
                            {
                                label.Text = @$"خطا: {ex.Message}";
                                label.ForeColor = Color.Red;
                            }
                        });
                    }

                    // حذف پنل فقط اگر خطا رخ نداده باشد
                    if (!hasError)
                    {
                        frm.Invoke(() =>
                        {
                            if (panel != null && frm.Controls.Contains(panel))
                            {
                                frm.Controls.Remove(panel);
                                panel.Dispose();
                            }
                        });
                    }
                });
            }
            catch (Exception e)
            {

            }


            //Task.Run(() =>
            //{
            //    Panel panel = null;

            //    frm.Invoke(() =>
            //    {
            //        panel = new Panel
            //        {
            //            Name = "pnlW",
            //            RightToLeft = RightToLeft.Yes,
            //            Dock = DockStyle.Bottom,
            //            Height = 25,
            //            BackColor = Color.FromArgb(72, 139, 145)
            //        };
            //        ProgressPanel progressPanel = new ProgressPanel
            //        {
            //            Dock = DockStyle.Right,
            //            Width = 35,
            //            ShowCaption = false,
            //            ShowDescription = false,
            //            LookAndFeel =
            //                {
            //                    UseDefaultLookAndFeel = false,
            //                    SkinName = "WXI",
            //                    SkinMaskColor = Color.Red
            //                }
            //        };
            //        Label label = new Label
            //        {
            //            AutoSize = false,
            //            Width = 400,
            //            Height = 25,
            //            Text = "لطفا کمی صبر نمایید - در حال پردازش اطلاعات",
            //            Dock = DockStyle.Right,
            //            TextAlign = ContentAlignment.MiddleLeft,
            //            ForeColor = Color.FromArgb(137, 243, 248),
            //            RightToLeft = RightToLeft.Yes
            //        };

            //        _fontBold.ChangeFont(label);
            //        frm.AddControl(panel);
            //        panel.SendToBack();

            //        panel.AddControl(label);
            //        panel.AddControl(progressPanel);

            //    });

            //    // تأخیر برای نمایش پنل
            //    Thread.Sleep(300);

            //    // اجرای action در رشته‌ی UI
            //    frm.Invoke(() =>
            //    {
            //        action(); // مستقیم اجرا کن، نیازی به new Action نیست
            //    });

            //    // حذف پنل بعد از پایان کار
            //    frm.Invoke(() =>
            //    {
            //        if (panel != null && frm.Controls.Contains(panel))
            //        {
            //            frm.Controls.Remove(panel);
            //            panel.Dispose();
            //        }
            //    });
            //});
        }
        public static void WaitDownPage(this Control frm, Action action)
        {
            try
            {
                Task.Run(() =>
                {
                    Panel panel = null;
                    Label label = null;
                    bool hasError = false;

                    frm.Invoke(() =>
                    {
                        panel = new Panel
                        {
                            Name = "pnlW",
                            RightToLeft = RightToLeft.Yes,
                            Dock = DockStyle.Bottom,
                            Height = 25,
                            BackColor = Color.FromArgb(72, 139, 145)
                        };

                        ProgressPanel progressPanel = new ProgressPanel
                        {
                            Dock = DockStyle.Right,
                            Width = 35,
                            ShowCaption = false,
                            ShowDescription = false,
                            LookAndFeel =
                            {
                                UseDefaultLookAndFeel = false,
                                SkinName = "WXI",
                                SkinMaskColor = Color.Red
                            }
                        };

                        label = new Label
                        {
                            AutoSize = false,
                            Width = 400,
                            Height = 25,
                            Text = @"لطفا کمی صبر نمایید - در حال پردازش اطلاعات",
                            Dock = DockStyle.Right,
                            TextAlign = ContentAlignment.MiddleLeft,
                            ForeColor = Color.FromArgb(137, 243, 248),
                            RightToLeft = RightToLeft.Yes
                        };

                        _fontBold.ChangeFont(label);
                        frm.AddControl(panel);
                        panel.SendToBack();
                        panel.AddControl(label);
                        panel.AddControl(progressPanel);
                    });

                    // تأخیر برای نمایش پنل
                    Thread.Sleep(300);

                    try
                    {
                        frm.Invoke(action);
                    }
                    catch (Exception ex)
                    {
                        hasError = true;

                        frm.Invoke(() =>
                        {
                            if (label != null)
                            {
                                label.Text = @$"خطا: {ex.Message}";
                                label.ForeColor = Color.Red;
                            }
                        });
                    }

                    // حذف پنل فقط اگر خطا رخ نداده باشد
                    if (!hasError)
                    {
                        frm.Invoke(() =>
                        {
                            if (panel != null && frm.Controls.Contains(panel))
                            {
                                frm.Controls.Remove(panel);
                                panel.Dispose();
                            }
                        });
                    }
                });
            }
            catch (Exception e)
            {

            }


            //Task.Run(() =>
            //{
            //    Panel panel = null;

            //    frm.Invoke(() =>
            //    {
            //        panel = new Panel
            //        {
            //            Name = "pnlW",
            //            RightToLeft = RightToLeft.Yes,
            //            Dock = DockStyle.Bottom,
            //            Height = 25,
            //            BackColor = Color.FromArgb(72, 139, 145)
            //        };
            //        ProgressPanel progressPanel = new ProgressPanel
            //        {
            //            Dock = DockStyle.Right,
            //            Width = 35,
            //            ShowCaption = false,
            //            ShowDescription = false,
            //            LookAndFeel =
            //                {
            //                    UseDefaultLookAndFeel = false,
            //                    SkinName = "WXI",
            //                    SkinMaskColor = Color.Red
            //                }
            //        };
            //        Label label = new Label
            //        {
            //            AutoSize = false,
            //            Width = 400,
            //            Height = 25,
            //            Text = "لطفا کمی صبر نمایید - در حال پردازش اطلاعات",
            //            Dock = DockStyle.Right,
            //            TextAlign = ContentAlignment.MiddleLeft,
            //            ForeColor = Color.FromArgb(137, 243, 248),
            //            RightToLeft = RightToLeft.Yes
            //        };

            //        _fontBold.ChangeFont(label);
            //        frm.AddControl(panel);
            //        panel.SendToBack();

            //        panel.AddControl(label);
            //        panel.AddControl(progressPanel);

            //    });

            //    // تأخیر برای نمایش پنل
            //    Thread.Sleep(300);

            //    // اجرای action در رشته‌ی UI
            //    frm.Invoke(() =>
            //    {
            //        action(); // مستقیم اجرا کن، نیازی به new Action نیست
            //    });

            //    // حذف پنل بعد از پایان کار
            //    frm.Invoke(() =>
            //    {
            //        if (panel != null && frm.Controls.Contains(panel))
            //        {
            //            frm.Controls.Remove(panel);
            //            panel.Dispose();
            //        }
            //    });
            //});
        }
        public static void Wait(this XtraForm frm, Action action)
        {
            try
            {
                SplashScreenManager.ShowForm(frm, typeof(SplashScreen1), true, true, false);

                try
                {
                    var action1 = new Action(action);
                    action1.Invoke();
                }
                catch (Exception e)
                {
                    SplashScreenManager.CloseForm(false);
                }

            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        public static T OverShowWait<T>(this XtraForm frm, Form ribbonFrom)
        {
            try
            {
                SplashScreenManager.ShowForm(frm, typeof(SplashScreen1), true, true, false);
                // System.Threading.Thread.Sleep(2000);

                bool checkOpen = false;
                var getFormsOpen = Application.OpenForms;
                XtraForm tmpFrm = null;

                try
                {
                    foreach (XtraForm form in getFormsOpen)
                    {
                        if (form.Name == frm.Name)
                        {
                            tmpFrm = form;
                            checkOpen = true;
                            break;
                        }
                    }

                }
                catch (Exception e)
                {
                    ribbonFrom.Show();
                }

                if (!checkOpen)
                {
                    frm.Load += (s, e1) =>
                    {
                        BonusSkins.Register();
                        SkinManager.EnableFormSkins();
                        //frm.LookAndFeel.SkinName = (ribbonFrom)ribbonFrom;
                        //frm.LookAndFeel.SkinName = "Office 2019 White";
                        //frm.LookAndFeel.SkinName = "WXI";
                        frm.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
                        frm.LookAndFeel.UseDefaultLookAndFeel = false;
                        frm.LookAndFeel.UseWindowsXPTheme = false;
                        frm.FormBorderEffect = FormBorderEffect.None;
                    };

                    frm.MdiParent = ribbonFrom;
                    frm.Show();
                }
                else
                {
                    tmpFrm.WindowState = FormWindowState.Maximized;
                    tmpFrm.BringToFront();
                    tmpFrm.Activate();
                }
                ribbonFrom.Activate();
                ribbonFrom.Show();

                var getConvertFrm = (T)Convert.ChangeType(tmpFrm, typeof(T));
                return getConvertFrm;

            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        public static T OverShow<T>(this XtraForm frm, Form ribbonFrom)
        {
            // ClsCollect.PleaswaitStart(frm);
            bool checkOpen = false;
            var getFormsOpen = Application.OpenForms;
            XtraForm tmpFrm = null;

            try
            {
                foreach (XtraForm form in getFormsOpen)
                {
                    if (form.Name == frm.Name)
                    {
                        tmpFrm = form;
                        checkOpen = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                // ClsCollect.PleaswaitEnd();
            }

            if (!checkOpen)
            {
                frm.Load += (s, e1) =>
                {
                    BonusSkins.Register();
                    SkinManager.EnableFormSkins();
                    SkinManager.EnableMdiFormSkins();
                    frm.LookAndFeel.SkinName = "WXI";
                    frm.LookAndFeel.UseDefaultLookAndFeel = false;
                    frm.LookAndFeel.UseWindowsXPTheme = false;
                    frm.FormBorderEffect = FormBorderEffect.None;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                };

                frm.MdiParent = ribbonFrom;
                frm.Show();
                // tmpFrm = frm;
            }
            else
            {
                tmpFrm.WindowState = FormWindowState.Normal;
                //tmpFrm.WindowState = FormWindowState.Maximized;
                tmpFrm.StartPosition = FormStartPosition.CenterScreen;
                tmpFrm.BringToFront();
                tmpFrm.Activate();
                ribbonFrom.Show();
            }

            var getConvertFrm = (T)Convert.ChangeType(tmpFrm, typeof(T));

            // ClsCollect.PleaswaitEnd();
            return getConvertFrm;
        }
        public static T OverShow<T>(this Form frm, Form ribbonFrom)
        {
            bool checkOpen = false;
            var getFormsOpen = Application.OpenForms;
            XtraForm tmpFrm = null;

            foreach (XtraForm form in getFormsOpen)
            {
                if (form.Name == frm.Name)
                {
                    tmpFrm = form;
                    checkOpen = true;
                    break;
                }
            }

            if (!checkOpen)
            {
                frm.MdiParent = ribbonFrom;
                frm.Show();
            }
            else
            {
                tmpFrm.WindowState = FormWindowState.Maximized;
                tmpFrm.BringToFront();
                tmpFrm.Activate();
            }

            var getConvertFrm = (T)Convert.ChangeType(tmpFrm, typeof(T));
            return getConvertFrm;
        }
        //public static void AddPagesReport(ref StiReport sumPage, StiReport getReport)
        //{
        //    //StiReport stiReportAddPage = new StiReport
        //    //{
        //    //    NeedsCompiling = false,
        //    //    IsRendered = true,
        //    //    ReportUnit = StiReportUnitType.Inches // مهم
        //    //};

        //    sumPage.NeedsCompiling = false;
        //    sumPage.IsRendered = true;
        //    sumPage.ReportUnit = StiReportUnitType.Millimeters;
        //    // sumPage.ReportUnit = StiReportUnitType.Inches;
        //    //sumPage.RenderedPages.Clear();

        //    foreach (StiPage page in getReport.CompiledReport.RenderedPages)
        //    {
        //        page.Report = sumPage;
        //        page.NewGuid();
        //        sumPage.RenderedPages.Add(page);
        //    }

        //    //   return sumPage;
        //}

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
        public static void EventRowStyle_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle % 2 == 0)
                e.Appearance.BackColor = Color.White;
            else
                e.Appearance.BackColor = Color.FromArgb(255, 192, 229, 221);
            // e.Appearance.BackColor = e.RowHandle % 2 == 0 ? Color.FromArgb(254, 225, 225) : Color.White;
        }
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                var getType = prop.GetMethod.ReturnType;
                var typeFullName = getType.FullName;
                if (!typeFullName.ToLower().Contains("System.Nullable".ToLower()))
                    dataTable.Columns.Add(prop.Name, getType);
                else
                    dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static object GetNum(this object data, int defaultValue = 0)
        {
            string replace = Regex.Replace(data.ToString(), @"[^0-9]", "");
            if (replace == "")
                return defaultValue;
            return replace;
        }
        public static T GetValueDT<T>(this object data, T defaultValue = default)
        {
            string replace = data.ToString();
            if (replace == "")
            {
                return (T)Convert.ChangeType(defaultValue, typeof(T));
            }
            return (T)Convert.ChangeType(replace, typeof(T));

        }
        public static T GetNumMines<T>(this object data, int defaultValue = 0)
        {
            var replace = Convert.ToDecimal(data.ToString());

            return (T)Convert.ChangeType(replace, typeof(T));
        }
        public static T GetNum<T>(this object data, int defaultValue = 0)
        {
            string replace = Regex.Replace(data.ToString(), @"[^0-9]", "");
            if (replace == "")
            {
                return (T)Convert.ChangeType(defaultValue, typeof(T));
            }
            return (T)Convert.ChangeType(replace, typeof(T));
        }
        public static object CheckZeroNull(this object value)
        {
            if (value != null)
                if (value.ToString() == "0")
                    return null;

            return value;


            //  return (T)Convert.ChangeType(value, typeof(T));

        }
        public static Guid ToGuid(this object data, bool newId = true)
        {
            if (data != null)
            {
                var getS = data.ToString();
                return getS == "" ? newId ? Guid.NewGuid() : Guid.Empty : Guid.Parse(data.ToString());
            }
            return newId ? Guid.NewGuid() : Guid.Empty;
        }
        public static Guid? ToGuidN(this object data, bool newId = true, bool vNull = false)
        {
            object getS;
            if (data != null)
            {
                getS = data.ToString();
            }
            else
            {
                getS = Guid.Empty;
            }
            //else
            //{
            //    if (newId && vnull == false)
            //    {
            //        return Guid.NewGuid();
            //    }
            //    if (newId == false && vnull)
            //    {
            //        return default;
            //    }
            //}
            //123

            var b = Guid.Parse(getS.ToString()) == Guid.Empty;
            if ((getS.ToString() == "" || b) && vNull)
                return null;
            if (getS.ToString() == "" || getS.ToString() == Guid.Empty.ToString())
                if (newId)
                    return Guid.NewGuid();
                else
                    return default(Guid);
            return Guid.Parse(data.ToString());
        }
        public static bool ToBoolN(this object data)
        {
            var getS = data.ToString();
            var b = Int16.Parse(getS);

            if (b == 1)
            {
                return true;
            }

            return false;


        }
        public static void EnabledAction(this Control ctrl, bool _bool)
        {
            ctrl.Invoke(() => ctrl.Enabled = _bool);
        }
        public static void MessageQues(Action action, string msg, bool access = true)
        {
            if (access == false)
            {
                ClassMessageBox.ShowAccessDenied(false);
                return;
            }

            var getQues = ClassMessageBox.ShowMSGQues(msg, Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
            if (getQues)
            {
                var action1 = new Action(action);
                action1.Invoke();
            }
        }

        public static void Delete(bool showQues, Action action, bool access = true)
        {
            if (access == false)
            {
                //ClassMessageBox.ShowMSGQues("حذف شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
                ClassMessageBox.ShowAccessDenied(false);
                return;
            }
            var action1 = new Action(action);

            if (showQues)
            {
                var getQues = ClassMessageBox.ShowMSGQues("حذف شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
                if (getQues)
                {
                    //  DGV_Viw.BeginUpdate();


                    action1.Invoke();
                    //    DGV_Viw.EndUpdate();
                }
            }
            else
            {

                action1.Invoke();
            }


        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static Process GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p;
            //p.MainModule.FileName.Dump();
        }
    }
}