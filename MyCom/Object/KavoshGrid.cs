using DevExpress.Data;
using DevExpress.Export;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
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
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.API.Native;
using MyCom.Class;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyCom.Class.ClsCollect;
using SvgImage = DevExpress.Utils.Svg.SvgImage;

namespace MyCom.Object
{
    public partial class KavoshGrid : GridControl
    {

        private ClsFont _font = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        BandedGridView DGV_Viw;
        DevExpress.XtraGrid.Views.Tile.TileView DGV_ViwTile;
        public KavoshGrid()
        {

            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            // DevExpress.UserSkins.OfficeSkins.Register();
            // DGV_Viw = this.MainView as BandedGridView;
        }
        public void AddEventRowCellClick<T>(Action<T> action, string getValue, string columnNameClicked)
        {
            GetViewBase.RowCellClick += (s1, e1) =>
            {
                if (e1.Button.IsLeft())
                {
                    var getValue2 = GetValue<T>(getValue);
                    if (e1.Column.FieldName == columnNameClicked)
                    {
                        action(getValue2);
                    }
                }
            };
        }
        public void AddEventRowClick<T>(Action<T> action, string getValue)
        {
            GetViewBase.RowClick += (s1, e1) =>
            {
                if (e1.Button.IsLeft())
                {
                    var getValue2 = GetValue<T>(getValue);
                    action(getValue2);
                }
            };
        }

        #region Title View

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="cntTotal">تعدادی که میخایی بیشتر نمایش بده</param>
        public void AddEventGetViewTile<T>(Action<List<T>> action, int cntTotal) where T : class
        {
            var tileView = MainView as TileView;
            if (tileView == null || action == null)
                return;

            tileView.PositionChanged += (s, e) =>
            {
                var itemSize = tileView.OptionsTiles.ItemSize;
                var pageSize = tileView.GridControl.ClientSize;

                int horizontalCount = pageSize.Width / itemSize.Width;
                int verticalCount = pageSize.Height / itemSize.Height;
                int totalVisible = horizontalCount * verticalCount;

                int topIndex = (tileView as IDataControllerVisualClient)?.TopRowIndex ?? 0;

                var visibleItems = new List<T>();

                for (int i = topIndex; i <= topIndex + totalVisible + cntTotal && i < tileView.RowCount; i++)
                {
                    if (tileView.GetRow(i) is T item)
                        visibleItems.Add(item);
                }

                action(visibleItems);
            };
        }

        public List<T> GetViewTileNotEvent<T>(int cntTotal)
        {
            var tileView = MainView as TileView;
            if (tileView == null)
                return null;

            var itemSize = tileView.OptionsTiles.ItemSize;
            var pageSize = tileView.GridControl.ClientSize;

            int horizontalCount = pageSize.Width / itemSize.Width;
            int verticalCount = pageSize.Height / itemSize.Height;
            int totalVisible = horizontalCount * verticalCount;

            int topIndex = (tileView as IDataControllerVisualClient)?.TopRowIndex ?? 0;

            var visibleItems = new List<T>();

            for (int i = topIndex; i <= topIndex + totalVisible + cntTotal && i < tileView.RowCount; i++)
            {
                if (tileView.GetRow(i) is T item)
                    visibleItems.Add(item);
            }
            return visibleItems;
        }
        public void RefreshTileViewAfterSearch(int cntTotal)
        {
            var tileView = MainView as TileView;

            var itemSize = tileView.OptionsTiles.ItemSize;
            var pageSize = tileView.GridControl.ClientSize;

            int horizontalCount = pageSize.Width / itemSize.Width;
            int verticalCount = pageSize.Height / itemSize.Height;
            int totalVisible = horizontalCount * verticalCount;

            int topIndex = (tileView as IDataControllerVisualClient)?.TopRowIndex ?? 0;

            //  var visibleItems = new List<T>();

            for (int i = topIndex; i <= topIndex + totalVisible + cntTotal && i < tileView.RowCount; i++)
            {
                tileView.RefreshRow(i);
                // if (tileView.GetRow(i) is T item)
                //   visibleItems.Add(item);
            }
            tileView.RefreshData();
            //  return visibleItems;
        }
        public void AddEventRowCellClickTileView<T>(Action<T> onClick) where T : class
        {
            var tileView = MainView as TileView;
            if (tileView == null || onClick == null)
                return;

            tileView.ItemClick += (s1, e1) =>
            {
                if (e1.Item is { } tileItem)
                {
                    int rowHandle = tileItem.RowHandle;
                    if (tileView.GetRow(rowHandle) is T item)
                    {
                        onClick(item);
                    }
                }
            };
        }
        public void AddEventRowCellRightClickTileView<T>(Action<T> onRightClick) where T : class
        {
            var tileView = MainView as TileView;
            if (tileView == null || onRightClick == null)
                return;

            tileView.ItemRightClick += (s1, e1) =>
            {

                if (e1.Item is { } tileItem)
                {
                    int rowHandle = tileItem.RowHandle;
                    if (tileView.GetRow(rowHandle) is T item)
                    {
                        onRightClick(item);
                    }
                }
            };
        }
        #endregion
        public KavoshGrid(IContainer container)
        {
            _search = new SearchControl
            {
                Width = 300,

                Client = this,
                Properties =
                {
                    Appearance = {TextOptions = {VAlignment = VertAlignment.Center, HAlignment = HorzAlignment.Center,}}
                }
            };
            container.Add(this);
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            //DGV_Viw = this.MainView as BandedGridView;

            Load += KavoshGrid_Load;



        }

        private void KavoshGrid_Load(object sender, EventArgs e)
        {

        }


        #region GridStructure New 2021
        public static Image ConvertToImage(object input, int width, int height)
        {
            if (input is Image img)
                return img;

            //if (input is Svg.SvgImage svgImage)
            //{
            //    return svgImage.Render(width, height);
            //}
            if (input is SvgImage devSvgImage)
            {
                var bitmap = new Bitmap(width, height);
                using var graphics = Graphics.FromImage(bitmap);
                using var cache = new GraphicsCache(graphics);

                // استفاده از DrawSvgImage به جای متد Draw یا Render
                cache.DrawSvgImage(devSvgImage, new Rectangle(0, 0, width, height), null);

                return bitmap;
            }

            if (input is byte[] svgBytes)
            {
                using var stream = new MemoryStream(svgBytes);
                var svgDoc = SvgDocument.Open<SvgDocument>(stream);
                svgDoc.Width = width;
                svgDoc.Height = height;
                return svgDoc.Draw();
            }

            throw new ArgumentException("Unsupported image format");
        }

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
            //public object ImageValue { get; set; }
            public bool RightToLeft { get; set; }
            public Action<object> Action { get; set; }

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
            VolumeFrom0, // 7
            VolumeFrom1, // 7
            Time, // 8
            CarPelak, //9
            Button, // 10    
            Link, // 11
            IP,
            PnlDate
        }

        private DataTable dt = new DataTable();
        public DataTable GridStructure(List<modelColumn> column, bool edit, bool sort, bool showFooter)
        {
            DGV_Viw = MainView as BandedGridView;
            #region لایسنس

            //  if (Lic1(column)) return;

            #endregion

            //  var DGV_Viw = this.MainView as GridView;

            DGV_Viw.BeginUpdate();
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
            DataSource = null;
            DGV_Viw.Columns.Clear();

            for (var i = 0; i < column.Count; i++)
            {
                if (column[i].Object == enumObject.Checked)
                    column[i].Type = typeof(bool);

                if (column[i].Type != null)
                    dt.Columns.Add(column[i].Name, column[i].Type);
                else
                    dt.Columns.Add(column[i].Name);
            }

            DataSource = dt;
            // this.DGV_Viw.EndUpdate();
            // this.DGV_Viw.BeginUpdate();
            // _data.DataSource = null;

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
                    DGV_Viw.Columns[nc].Caption = column[i].Caption;
                    // DGV_Viw.Columns[nc].AppearanceCell.Image = Properties.Resources.Accept_25;
                    // DGV_Viw.Columns[nc].AppearanceCell.TextOptions.

                    // ContextImageOptions = { Image = t.Image },

                    // clm = NC.Replace("btntext=", "");
                    if (column[i].ImageValue != null && column[i].Object == enumObject.Default)
                    {
                        var addButton = new RepositoryItemTextEdit();
                        if (column[i].ImageValue != null)
                        {
                            addButton.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                            addButton.Appearance.TextOptions.VAlignment = VertAlignment.Center;

                            addButton.Appearance.TextOptions.RightToLeft = true;
                            if (column[i].ImageValue is Image image)
                            {
                                addButton.ContextImageOptions.Image = image;
                            }
                            if (column[i].ImageValue is Byte[] svgImage)
                            {
                                addButton.ContextImageOptions.SvgImage = svgImage;
                                addButton.ContextImageOptions.SvgImageSize = new Size(19, 19);
                            }
                        }

                        RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;
                        // _font.ChangeFont(DGV_Viw.Columns[clm], sizeF);
                    }
                    else if (column[i].Type == typeof(Image))
                    {
                        DGV_Viw.Columns[nc].ColumnEdit =
                            new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                    }
                }

                catch
                {
                    // ignored
                }

                if (column[i].PriceActive || column[i].FormatType == FormatType.Numeric)
                {
                    DGV_Viw.Columns[nc].DisplayFormat.FormatType = FormatType.Numeric;
                    FormatStringNegativeNumber(nc, column[i].CountFloat);
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
                        //_progressBar.LookAndFeel.SkinName = "Valentine";
                        _progressBar.LookAndFeel.SkinName = "Pumpkin";
                        //_progressBar.LookAndFeel.SkinMaskColor = Color.Lime;

                        RepositoryItems.Add(_progressBar);
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
                            ContextImageOptions = { },
                            MaxLength = 30,
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

                        RepositoryItems.Add(_memoEdit);
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


                        RepositoryItems.Add(_switch);

                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                        DGV_Viw.Columns[nc].ColumnEdit = _switch;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 195;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.PnlDate)
                    {
                        var addButton = new RepositoryItemButtonEdit
                        {
                            Name = nc,
                        };


                        #region Icon

                        if (column[i].ImageValue is not null)
                        {
                            var getImage = ConvertToImage(column[i].ImageValue, 19, 19);
                            addButton.Buttons[0].ImageOptions.Image = getImage;
                            addButton.Buttons[0].Kind = ButtonPredefines.Glyph;
                        }

                        var i1 = i;
                        if (column[i1].Action != null)
                        {
                            GetViewBase.RowCellClick += (s1, e1) =>
                            {
                                if (e1.Column.FieldName == column[i1].Name)
                                {
                                    frmCalender frmCalender = new frmCalender();
                                    frmCalender.ShowDialog();

                                    var selRowdate = frmCalender.SelDate;
                                    column[i1].Action?.Invoke(selRowdate);
                                }
                            };
                            addButton.Buttons[0].Click += (s1, e1) =>
                            {
                                frmCalender frmCalender = new frmCalender();
                                frmCalender.ShowDialog();

                                var selRowdate = frmCalender.SelDate;
                                column[i1].Action?.Invoke(selRowdate);
                            };
                        }

                        #endregion



                        RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;

                        DGV_Viw.Columns[nc].Caption = column[i].Caption;
                        DGV_Viw.Columns[nc].OptionsFilter.AllowFilter = false;
                        DGV_Viw.Columns[nc].OptionsFilter.AllowAutoFilter = false;
                        DGV_Viw.Columns[nc].OptionsColumn.FixedWidth = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSize = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSort = DefaultBoolean.False;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowEdit = true;
                        DGV_Viw.Columns[nc].OptionsColumn.ReadOnly = true;
                    }
                    else if (column[i].Object == enumObject.DateMask)
                    {
                        var maskData = new RepositoryItemTextEdit
                        {
                            Name = nc,
                            Mask = { EditMask = @"___/__/__", MaskType = MaskType.DateTime, UseMaskAsDisplayFormat = true },

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

                        RepositoryItems.Add(maskData);

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

                        RepositoryItems.Add(_rating);

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

                        RepositoryItems.Add(_bool);
                        // _bool.CheckStyle = CheckStyles.Standard;

                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = _bool;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 95;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.VolumeFrom0)
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

                        RepositoryItems.Add(_trackBar);
                        // _bool.CheckStyle = CheckStyles.Standard;

                        //DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = _trackBar;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                        DGV_Viw.Columns[nc].MaxWidth = 95;
                        DGV_Viw.Columns[nc].MinWidth = 95;
                    }
                    else if (column[i].Object == enumObject.VolumeFrom1)
                    {
                        var _trackBar = new RepositoryItemTrackBar { Name = nc, Minimum = 1, Maximum = 100, };

                        _trackBar.ShowLabels = true;
                        _trackBar.ShowLabelsForHiddenTicks = true;
                        _trackBar.ShowValueToolTip = true;

                        //  if (_trackBar.Labels.Count == 0)
                        // _trackBar.Labels.Add(new TrackBarLabel { Value = 5});
                        // _trackBar.LookAndFeel.SkinName = "Glass Oceans";
                        //_trackBar.LookAndFeel.SkinName = "Caramel";
                        //  _bool.LookAndFeel.SkinName = "Summer 2008";
                        // _trackBar.LookAndFeel.UseDefaultLookAndFeel = false;

                        RepositoryItems.Add(_trackBar);
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

                        RepositoryItems.Add(timeEdit);

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

                        RepositoryItems.Add(maskData);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = maskData;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                    }
                    else if (column[i].Object == enumObject.IP)
                    {
                        var maskData = new RepositoryItemTextEdit
                        {
                            Name = nc,
                            Mask =
                            {
                                EditMask = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)",
                                MaskType = MaskType.RegEx,
                                UseMaskAsDisplayFormat = true
                            },
                            Appearance = { Font = new Font("Samim", 12) },
                            AppearanceFocused = { Font = new Font("Samim", 12) },

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

                        maskData.LookAndFeel.SkinName = "WXICompact";
                        //maskData.LookAndFeel.SkinName = "WXI";
                        maskData.LookAndFeel.UseDefaultLookAndFeel = false;

                        RepositoryItems.Add(maskData);

                        DGV_Viw.Columns[nc].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        DGV_Viw.Columns[nc].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                        DGV_Viw.Columns[nc].ColumnEdit = maskData;
                        DGV_Viw.Columns[nc].AppearanceHeader.Name = nc;
                        DGV_Viw.Columns[nc].Caption = nc;
                    }
                    else if (column[i].Object == enumObject.Button)
                    {

                        var addButton = new RepositoryItemHyperLinkEdit
                        {
                            Name = nc,
                            ImageAlignment = HorzAlignment.Center,
                            //LookAndFeel =
                            //{
                            //    UseDefaultLookAndFeel = false,
                            //    SkinName = "Glass Oceans"
                            //}
                        };

                        if (column[i].ImageValue is not null)
                        {
                            var getImage = ConvertToImage(column[i].ImageValue, 19, 19);
                            addButton.Image = getImage;
                        }
                        var i1 = i;
                        if (column[i1].Action != null)
                        {
                            GetViewBase.RowCellClick += (s1, e1) =>
                            {
                                if (e1.Column.FieldName == column[i1].Name)
                                {
                                    var getId = GetValue<Guid>(e1.RowHandle,"Id");
                                    column[i1].Action(getId);
                                }
                            };
                      
                        }
                        addButton.ImageAlignment = HorzAlignment.Center;

                        RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;

                        DGV_Viw.Columns[nc].Caption = column[i].Caption;
                        DGV_Viw.Columns[nc].OptionsFilter.AllowFilter = false;
                        DGV_Viw.Columns[nc].OptionsFilter.AllowAutoFilter = false;
                        DGV_Viw.Columns[nc].OptionsColumn.FixedWidth = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSize = false;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowSort = DefaultBoolean.False;
                        DGV_Viw.Columns[nc].OptionsColumn.AllowEdit = false;
                    }
                    else if (column[i].Object == enumObject.Link)
                    {
                        var addButton = new RepositoryItemHyperLinkEdit
                        {
                            //column[i].ImageValue
                            Name = nc,
                            Caption = column[i].Name
                        };

                        RepositoryItems.Add(addButton);
                        DGV_Viw.Columns[nc].ColumnEdit = addButton;

                        // _font.ChangeFont(DGV_Viw.Columns[nc], sizeF);
                        DGV_Viw.Columns[nc].Caption = column[i].Caption;
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
                        DGV_Viw.Columns[nc].OptionsColumn.AllowEdit = false;
                    }

                }
            }

            for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            {
                DGV_Viw.Columns[i].VisibleIndex = i;
            }
            DGV_Viw.OptionsView.ShowFooter = showFooter;

            DGV_Viw.EndUpdate();
            SetFieldSizeColumn();

            return dt;
        }

        #region Tools
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

        #endregion

        private void ThreadSize()
        {
            //  var DGV_Viw = this.MainView as BandedGridView;
            Thread.Sleep(600);
            {
                try
                {
                    Invoke(new Action(() =>
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
        public void ThreadSizeFast()
        {
            //  var DGV_Viw = this.MainView as BandedGridView;
            //  Thread.Sleep(600);
            {
                try
                {
                    Invoke(new Action(() =>
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
                                //Information Columns Name 
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
        public class modelColumnFitWidth
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public string FieldName { get; set; }
        }
        private List<modelColumnFitWidth> _lstColumnFitWidth = new List<modelColumnFitWidth>();



        #endregion

        #region مدیریت فیلد گرید

        public class ModelGetValueTitle
        {
            public Guid Id { get; set; }
            public object Title { get; set; }
        }
        public RepositoryItemGridLookUpEdit AddGridToGrid<EF>(List<EF> lEF, string nameRelationColumn, string valueMember, string displayMember, Action<ModelGetValueTitle> action = null,
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

            this.DGV_Viw.Columns[nameRelationColumn].ColumnEdit = gridComboEdit;
            gridComboEdit.DataSource = lEF;

            _font.ChangeFont(gridComboEdit, 13);

            gridComboEdit.View.BestFitColumns(false);

            // gridComboEdit.View.OptionsFind.AlwaysVisible = true;

            gridComboEdit.View.OptionsView.ShowAutoFilterRow = true;
            //gridComboEdit.View.OptionsView.ColumnAutoWidth = false;
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

            if (action != null)
            {
                gridComboEdit.EditValueChanged += (s, e) =>
                {
                    var getValue = Guid.Parse(((GridLookUpEdit)s).EditValue.ToString());
                    var getTitle = ((GridLookUpEdit)s).Text.ToString();
                    action(new ModelGetValueTitle
                    {
                        Id = getValue,
                        Title = getTitle
                    });
                };
            }


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
        public static void EventRowStyle_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle % 2 == 0)
                e.Appearance.BackColor = Color.White;
            else
                e.Appearance.BackColor = Color.FromArgb(255, 192, 229, 221);
            // e.Appearance.BackColor = e.RowHandle % 2 == 0 ? Color.FromArgb(254, 225, 225) : Color.White;
        }

        public int RowCount()
        {
            return DGV_Viw.RowCount;
        }
        public int ColumnCount()
        {
            if (DGV_Viw != null)
                return DGV_Viw.Columns.Count;
            return 0;
        }

        public void AddAllowNewRowAndType(DefaultBoolean active, NewItemRowPosition position)
        {
            DGV_Viw.OptionsBehavior.AllowAddRows = active;
            DGV_Viw.OptionsView.NewItemRowPosition = position;
        }
        public void SetFieldSizeColumn()
        {
            //  ThreadSize();
            Thread _Thread = new Thread(ThreadSize);
            _Thread.Start();
            // ThreadSize();
        }
        public void MaxMinWidth(string nameColumn, int min, int max)
        {
            //  var DGV_Viw = this.MainView as BandedGridView;
            var getClm = DGV_Viw.Columns.Any(a => a.FieldName == nameColumn);
            if (getClm)
            {
                DGV_Viw.Columns[nameColumn].Width = max;
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
        }

        public BandedGridView GetViewBase => DGV_Viw;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns>نگهداشتن ستون پیش فرض نمایش</returns>
        public List<string> HiddenColumn()
        {
            var getOnlyVis = DGV_Viw.VisibleColumns.Select(s1 => s1.FieldName).ToList();
            for (int i = 0; i < ColumnCount(); i++)
            {
                DGV_Viw.Columns[i].Visible = false;
            }

            return getOnlyVis;
        }
        public void HiddenColumn(string nameColumn)
        {
            DGV_Viw.Columns[nameColumn].Visible = false;
        }
        public void HiddenColumn(List<string> nameColumns)
        {
            foreach (string s in nameColumns)
            {
                DGV_Viw.Columns[s].Visible = false;
            }
        }
        public void HiddenBandedGroup(string nameBanded = "banded")
        {
            DGV_Viw.Bands[nameBanded].Visible = false;
        }
        public void ShowBandedGroup(string nameBanded = "banded")
        {
            DGV_Viw.Bands[nameBanded].Visible = true;
        }
        public void ShowColumn(string nameColumn)
        {
            var getCount = DGV_Viw.VisibleColumns.Count;
            DGV_Viw.Columns[nameColumn].VisibleIndex = getCount + 1;
            DGV_Viw.Columns[nameColumn].Visible = true;
        }
        public void ShowColumn(List<string> nameColumns)
        {
            foreach (string s in nameColumns)
            {
                var getCount = DGV_Viw.VisibleColumns.Count;
                DGV_Viw.Columns[s].VisibleIndex = getCount + 1;
                DGV_Viw.Columns[s].Visible = true;
            }
        }
        public void ShowColumn(string nameColumn, int postion)
        {
            // var getCount = DGV_Viw.VisibleColumns.Count;
            DGV_Viw.Columns[nameColumn].VisibleIndex = postion;
            DGV_Viw.Columns[nameColumn].Visible = true;
        }
        //public void ShowColumnAutoPsotion( string nameColumn)
        //{
        //    DGV_Viw.Columns[nameColumn].Visible = true;
        //    var getCount = DGV_Viw.VisibleColumns.Count;
        //    DGV_Viw.Columns[nameColumn].VisibleIndex = getCount + 1;

        //}
        public void ShowColumns()
        {
            for (int i = 0; i < DGV_Viw.Columns.Count; i++)
            {
                var getName = DGV_Viw.Columns[i].FieldName;
                ShowColumn(getName);
            }
        }
        public void HiddenColumn(RepositoryItemGridLookUpEdit lookUpEdit, string nameColumn)
        {
            lookUpEdit.PopupView.Columns[nameColumn].Visible = false;
        }
        public void ReadOnlyColumn(string nameColumn)
        {
            DGV_Viw.Columns[nameColumn].OptionsColumn.ReadOnly = true;
        }
        public void AllowEditColumn(string nameColumn, bool enabled = false)
        {
            DGV_Viw.Columns[nameColumn].OptionsColumn.AllowEdit = enabled;
        }
        public void ColumnsAllow()
        {
            foreach (BandedGridColumn column in DGV_Viw.Columns)
            {
                DGV_Viw.Columns[column.FieldName].OptionsColumn.AllowEdit = true;
            }
        }
        public void ColumnsDeny()
        {
            foreach (BandedGridColumn column in DGV_Viw.Columns)
            {
                DGV_Viw.Columns[column.FieldName].OptionsColumn.AllowEdit = false;
            }
        }
        public void TagColumn(string nameColumn, string textTag)
        {
            DGV_Viw.Columns[nameColumn].Tag = textTag;
        }
        public void ShowTagColumn(string nameColumn)
        {
            DGV_Viw.Columns[nameColumn].ToolTip = DGV_Viw.Columns[nameColumn].Tag.ToString();
        }
        public void FormatStringNegativeNumber(string nameColumn)
        {
            if (RightToLeft == RightToLeft.Yes)
                DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;#,##0.###-";
            else
                DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;-#,##0.###";
        }
        public void FormatStringNegativeNumber(string nameColumn, int N0)
        {
            if (RightToLeft == RightToLeft.Yes)
            {
                if (N0 == 0)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0;#,##0-";
                else if (N0 == 1)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.#;#,##0.#-";
                else if (N0 == 2)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.##;#,##0.##-";
                else if (N0 == 3)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;#,##0.###-";
            }
            else
            {
                if (N0 == 0)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0;-#,##0";

                else if (N0 == 1)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.#;-#,##0.#";

                else if (N0 == 2)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.##;-#,##0.##";

                else if (N0 == 3)
                    DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "#,##0.###;-#,##0.###";
            }

            DGV_Viw.Columns[nameColumn].GroupFormat.FormatString = DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString;
        }
        public void FormatStringNegativeNumberNoPrice(string nameColumn)
        {
            if (RightToLeft == RightToLeft.Yes)
                DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "###;###-";
            else
                DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = "###;-###";
        }
        public void SettingGridView(GridView viewChild)
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
        public void AutoSummaryCalculate(CustomSummaryEventArgs e, string filedName, string titleDown, ref double valueCalcOutPut, string afterValue = "", int N = 3)
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
                        e.TotalValue = titleDown + ": " + valueCalcOutPut.ToString(FormatStringNegativeNumber(N)) + " " + afterValue;
                    break;

            }
        }
        public double AutoSummaryCalculateSelectedRows(CustomSummaryEventArgs e, object s, string filedName, string otherFiledName, string titleDown, ref double valueCalcOutPut, string afterValue = "", int N = 3)
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

        double cntSummary;

        public void AutoSummaryCalculateAvg(CustomSummaryEventArgs e, string filedName, string titleDown, ref double valueCalcOutPut, string afterValue = "")
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

        public void Sort(string nameColumn, ColumnSortOrder order = ColumnSortOrder.Ascending)
        {
            DGV_Viw.ClearSorting();
            DGV_Viw.Columns[nameColumn].SortOrder = order;
            DGV_Viw.MoveFirst();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="nameColumn"></param>
        /// <param name="style">پیش فرض فیکس از راست</param>
        public void ColumnFixedUseScroll(string nameColumn, FixedStyle style = FixedStyle.Left)
        {
            DGV_Viw.Columns[nameColumn].Fixed = style;
        }


        public string FormatStringMosbatNumber(int N0)
        {
            if (N0 == 1)
                return "#,##0.#";
            if (N0 == 2)
                return "#,##0.##";
            if (N0 == 3)
                return "#,##0.###";
            return "#,##0";
        }
        public void FormatStringNegativeNumberNotAshar(string nameColumn, string format = "#,##0.###")
        {
            DGV_Viw.Columns[nameColumn].DisplayFormat.FormatString = format;
        }
        public string FormatStringNegativeNumber(int N0)
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
        public string FormatStringNegativeNumber(RightToLeft rightToLeft)
        {
            if (rightToLeft == RightToLeft.Yes)
                return "#,##0.###;#,##0.###-";
            else
                return "#,##0.###;-#,##0.###";
        }
        public void ActiveScrollGrid(bool Active = true)
        {
            DGV_Viw.OptionsView.ColumnAutoWidth = !Active;
            DGV_Viw.BestFitColumns();
        }
        public void SingleClickCell(object sender, MouseEventArgs e, string clm)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell && e.Button == MouseButtons.Left && hitInfo.Column.FieldName == clm)
            {
                if (e.Clicks == 1)
                {
                    view.FocusedColumn = hitInfo.Column;
                    view.FocusedRowHandle = hitInfo.RowHandle;
                    //ClassKeyAndMouse.DoMouseClick();
                }
            }
        }

        #endregion

        #region کار با دیتا

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
        public T GetValueRowHandle<T>(string clm, int rowHandle, object defualtValue = null)
        {
            try
            {
                var get = DGV_Viw.GetRowCellValue(rowHandle, clm);


                var getType = typeof(T).FullName;

                if (getType == typeof(Guid).FullName)
                {
                    var readOnlySpan = get.ToString();
                    if (readOnlySpan != "")
                    {
                        var getValue2 = Guid.Parse(readOnlySpan);
                        return ChangeType<T>(getValue2);
                    }

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
        public T GetValue<T>(string clm, object defualtValue = null)
        {
            try
            {
                var get = DGV_Viw.GetRowCellValue(DGV_Viw.FocusedRowHandle, clm);


                var getType = typeof(T).FullName;

                if (getType == typeof(Guid).FullName)
                {
                    if (get != null)
                    {
                        var readOnlySpan = get.ToString();
                        if (readOnlySpan != "")
                        {
                            var getValue2 = Guid.Parse(readOnlySpan);
                            return ChangeType<T>(getValue2);
                        }
                    }
                    else
                    {
                        return default;
                    }
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
        public void DeleteRow(bool showQues)
        {
            DGV_Viw.BeginUpdate();
            if (showQues)
            {
                var getQues = ClassMessageBox.ShowMSGQues("حذف شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
                if (getQues)
                {
                    DGV_Viw.DeleteRow(DGV_Viw.FocusedRowHandle);
                }
            }
            else
            {
                DGV_Viw.DeleteRow(DGV_Viw.FocusedRowHandle);
            }

            DGV_Viw.EndUpdate();
        }
        public void DeleteRow(bool showQues, Action action, bool access = true)
        {
            if (access == false)
            {
                //ClassMessageBox.ShowMSGQues("حذف شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
                ClassMessageBox.ShowAccessDenied(false);
                return;
            }
            var action1 = new Action(action);
            DGV_Viw.BeginUpdate();
            if (showQues)
            {
                var getQues = ClassMessageBox.ShowMSGQues("حذف شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
                if (getQues)
                {
                    //  DGV_Viw.BeginUpdate();
                    DGV_Viw.DeleteRow(DGV_Viw.FocusedRowHandle);

                    action1.Invoke();
                    //    DGV_Viw.EndUpdate();
                }
            }
            else
            {
                DGV_Viw.DeleteRow(DGV_Viw.FocusedRowHandle);
                action1.Invoke();
            }

            DGV_Viw.EndUpdate();
        }
        public void DeleteRow(bool showQues, int row)
        {
            DGV_Viw.BeginUpdate();
            if (showQues)
            {
                var getQues = ClassMessageBox.ShowMSGQues("حذف شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال, Color.Red);
                if (getQues)
                {
                    //  DGV_Viw.BeginUpdate();
                    DGV_Viw.DeleteRow(row);
                    //    DGV_Viw.EndUpdate();
                }
            }
            else
            {
                DGV_Viw.DeleteRow(DGV_Viw.FocusedRowHandle);
            }

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

        public void ShowGroupByHeader()
        {
            DGV_Viw.OptionsView.ShowGroupPanel = true;
        }
        public void HideGroupByHeader()
        {
            DGV_Viw.OptionsView.ShowGroupPanel = false;
        }
        public void SetColumnAutoSize(bool _bool)
        {
            DGV_Viw.OptionsView.ColumnAutoWidth = _bool;
        }
        public void ShowFooter()
        {
            CheckForIllegalCrossThreadCalls = false;

            new Thread(() =>
            {
                try
                {
                    Thread.Sleep(2000);
                    DGV_Viw.OptionsView.ShowFooter = true;
                }
                catch (Exception e)
                {

                }

            }).Start();
        }
        public void HideFooter()
        {
            //   CheckForIllegalCrossThreadCalls = false;

            //  new Thread(() =>
            //  {
            //     Thread.Sleep(2000);
            DGV_Viw.OptionsView.ShowFooter = false;

            // }).Start();
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
        public void DisableWidthSize(string column, bool enable = false)
        {
            DGV_Viw.Columns[column].OptionsColumn.AllowSize = enable;
        }



        public void AddSummaryItem(string clmForCalc, string clmForView, string displayFormat, SummaryItemType type, bool clear = true)
        {
            if (clear)
                DGV_Viw.Columns[clmForView].Summary.Clear();

            GridColumnSummaryItem summaryItem = new GridColumnSummaryItem { Tag = clmForView, FieldName = clmForCalc, SummaryType = type, DisplayFormat = displayFormat };
            DGV_Viw.Columns[clmForView].Summary.Add(summaryItem);

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
            GridView gvChild = new GridView(this);

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

            if (LevelTree.Nodes.Count < 1)
            {
                LevelTree.Nodes.Add(relationName, gvChild);
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
        //public double GetAllSelectedRows(string fieldCheck)
        //{
        //    double sumSizesCheck = 0;   
        //    for (int i = 0; i < DGV_Viw.RowCount; i++)
        //    {
        //        if (GetValue<bool>(i, fieldCheck))
        //        {

        //        }
        //    }

        //    return sumSizesCheck;
        //}
        public double GetAllCheckedRow(string fieldCheck, string fieldCalcSum)
        {
            double sumSizesCheck = 0;
            for (int i = 0; i < DGV_Viw.RowCount; i++)
            {
                if (GetValue<bool>(i, fieldCheck))
                {
                    var getSize = GetValue<double>(i, fieldCalcSum);
                    sumSizesCheck += getSize;
                }
            }

            return sumSizesCheck;
        }

        public void CheckInverse(string fieldName)
        {
            var getValue = GetValue<bool>(fieldName);
            SetValue(fieldName, !getValue);
        }
        public void ChecKAllFalseOneTrue(string fieldName)
        {
            var getFoc = DGV_Viw.FocusedRowHandle;
            for (int i = 0; i < DGV_Viw.RowCount; i++)
            {
                SetValue(i, fieldName, false);
            }
            Thread.Sleep(150);
            SetValue(fieldName, true);
            Thread.Sleep(150);
            DGV_Viw.FocusedRowHandle = getFoc;

            // var getValue = GetValue<bool>(fieldName);

        }
        private Panel _pnlUP = new Panel
        {
            Height = 26,
            Dock = DockStyle.Fill
        };

        private SearchControl _search;

        SimpleButton btnRefresh = new SimpleButton { Name = "btnRefresh", Text = @"Refresh", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Refresh_25 };
        SimpleButton btnExcel = new SimpleButton { Name = "btnExcel", Text = @"Excel", Dock = DockStyle.Left, Width = 90, Image = Properties.Resources.excel_24 };
        SimpleButton btnSave = new SimpleButton { Name = "btnSave", Text = @"Save", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Save_3_23 };
        SimpleButton btnCancel = new SimpleButton { Name = "btnCancel", Text = @"Cancel", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Delete_25 };
        SimpleButton btnAdd = new SimpleButton { Name = "btnAdd", Text = @"Add", Dock = DockStyle.Right, Width = 90, Image = Properties.Resources.Add_25 };
        SimpleButton btnCustom = new SimpleButton { Name = "btnCustom", Text = @"Custom", Dock = DockStyle.Left, Width = 90 };

        public Panel GetPanelUp(bool search)
        {
            FunAddSearchToPanel();
            return _pnlUP;
        }
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
            //FunAddSearchToPanel();


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

        private void FunAddSearchToPanel()
        {
            _search = new SearchControl
            {
                Name = "_search",
                Width = 180,
                Client = this,
                Properties =
                {
                    NullValuePrompt = "جستجو",
                    FindDelay = 300,
                    Appearance =
                    {
                        BackColor = Color.FromArgb(185, 129, 190, 229),
                        TextOptions = {VAlignment = VertAlignment.Center, HAlignment = HorzAlignment.Center}
                    },
                    AppearanceFocused =
                    {
                        BackColor = Color.FromArgb(185, 174, 205, 224),
                        TextOptions = {VAlignment = VertAlignment.Center, HAlignment = HorzAlignment.Center}
                    }
                }
            };
            AddButtonOrControlUpGrid(_search);
            _font.ChangeFont(_search);
        }

        private void AddButtonOrControlUpGrid(Control control)
        {
            var getFindControl = _pnlUP.Controls.Find(control.Name, true).Any();
            //  var getFindControl = groupControl1.Controls.Find(control.Name, true).Any();
            if (!getFindControl)
            {
                _pnlUP.Controls.Add(control);
            }
        }

        #endregion

        //  #region Style
        public void FixColumn(string clm, FixedStyle fixedStyle)
        {
            var getClm = GetViewBase.Columns[clm];
            var clmFieldName = getClm.FieldName + "Bnd";
            GetViewBase.Bands.Add(new GridBand { Name = clmFieldName });
            GetViewBase.Bands[clmFieldName].Columns.Add(getClm);
            GetViewBase.Bands[clmFieldName].Fixed = fixedStyle;
        }
        public async Task SetStyle()
        {
            this.Invoke(() =>
            {
                DGV_Viw = MainView as BandedGridView;

                #region BandPanel

                DGV_Viw.Appearance.BandPanel.BackColor = Color.FromArgb(175, 226, 214);
                DGV_Viw.Appearance.BandPanel.BackColor2 = Color.FromArgb(136, 197, 182);
                DGV_Viw.Appearance.BandPanel.BorderColor = Color.DodgerBlue;
                DGV_Viw.Appearance.BandPanel.Options.UseBackColor = true;
                DGV_Viw.Appearance.BandPanel.Options.UseBorderColor = true;

                #endregion

                #region FixedLine

                DGV_Viw.Appearance.FixedLine.BackColor = Color.FromArgb(0, 220, 255);
                DGV_Viw.Appearance.FixedLine.BackColor2 = Color.FromArgb(0, 220, 255);
                DGV_Viw.Appearance.FixedLine.Options.UseBackColor = true;

                #endregion

                #region FocusedRow

                DGV_Viw.Appearance.FocusedRow.BorderColor = Color.Red;
                DGV_Viw.Appearance.FocusedRow.Options.UseBorderColor = true;

                #endregion

                #region FooterPanel

                DGV_Viw.Appearance.FooterPanel.ForeColor = Color.Red;
                DGV_Viw.Appearance.FooterPanel.Options.UseFont = true;
                DGV_Viw.Appearance.FooterPanel.Options.UseForeColor = true;
                DGV_Viw.Appearance.FooterPanel.Options.UseTextOptions = true;
                DGV_Viw.Appearance.FooterPanel.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Appearance.FooterPanel.TextOptions.VAlignment = VertAlignment.Center;

                #endregion

                #region GroupButton

                DGV_Viw.Appearance.GroupButton.Options.UseTextOptions = true;
                DGV_Viw.Appearance.GroupButton.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Appearance.GroupButton.TextOptions.VAlignment = VertAlignment.Center;

                #endregion

                #region GroupFooter

                DGV_Viw.Appearance.GroupFooter.ForeColor = Color.Red;
                DGV_Viw.Appearance.GroupFooter.Options.UseFont = true;
                DGV_Viw.Appearance.GroupFooter.Options.UseForeColor = true;
                DGV_Viw.Appearance.GroupFooter.Options.UseTextOptions = true;
                DGV_Viw.Appearance.GroupFooter.TextOptions.HAlignment = HorzAlignment.Center;
                DGV_Viw.Appearance.GroupFooter.TextOptions.VAlignment = VertAlignment.Center;

                #endregion

                #region GroupRow

                DGV_Viw.Appearance.GroupRow.ForeColor = Color.Red;
                DGV_Viw.Appearance.GroupRow.Options.UseForeColor = true;

                #endregion

                #region HeaderPanel

                DGV_Viw.Appearance.HeaderPanel.BackColor = Color.Red;
                DGV_Viw.Appearance.HeaderPanel.BackColor2 = Color.Red;
                DGV_Viw.Appearance.HeaderPanel.Options.UseBackColor = true;
                DGV_Viw.Appearance.HeaderPanel.Options.UseFont = true;

                #endregion

                #region HeaderPanelBackground

                DGV_Viw.Appearance.HeaderPanelBackground.BackColor = Color.FromArgb(0, 220, 255);
                DGV_Viw.Appearance.HeaderPanelBackground.BackColor2 = Color.FromArgb(0, 220, 255);
                //DGV_Viw.Appearance.HeaderPanelBackground.BackColor2 = Color.Red;
                DGV_Viw.Appearance.HeaderPanelBackground.Options.UseBackColor = true;

                #endregion

                #region HorzLine

                //DGV_Viw.Appearance.HorzLine.BackColor = Color.CornflowerBlue;
                DGV_Viw.Appearance.HorzLine.BackColor = Color.Black;
                DGV_Viw.Appearance.HorzLine.BackColor2 = Color.SteelBlue;
                DGV_Viw.Appearance.HorzLine.Options.UseBackColor = true;

                #endregion

                DGV_Viw.Appearance.Row.Options.UseFont = true;

                #region FocusedCell

                DGV_Viw.Appearance.FocusedCell.BackColor = Color.FromArgb(82, 229, 226);
                //  DGV_Viw.Appearance.FocusedCell.BackColor2 = Color.FromArgb(212, 221, 240);
                DGV_Viw.Appearance.FocusedCell.Options.UseBackColor = true;

                #endregion

                #region SelectedRow

                DGV_Viw.Appearance.SelectedRow.BackColor = Color.FromArgb(171, 195, 204);
                DGV_Viw.Appearance.SelectedRow.BackColor2 = Color.FromArgb(171, 195, 204);
                DGV_Viw.Appearance.SelectedRow.Options.UseBackColor = true;
                DGV_Viw.Appearance.SelectedRow.Options.UseFont = true;

                #endregion

                #region VertLine

                //DGV_Viw.Appearance.VertLine.BackColor = Color.CornflowerBlue;
                DGV_Viw.Appearance.HorzLine.BackColor = Color.Black;
                DGV_Viw.Appearance.VertLine.BackColor2 = Color.DodgerBlue;
                DGV_Viw.Appearance.VertLine.Options.UseBackColor = true;

                #endregion

                DGV_Viw.Appearance.ViewCaption.Options.UseFont = true;
                //this.DGV_Viw.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.25F);
                DGV_Viw.AppearancePrint.HeaderPanel.Options.UseFont = true;
                //this.DGV_Viw.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {this.banded});
                DGV_Viw.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                //this.DGV_Viw.GridControl = this.DGV;
                //this.DGV_Viw.Name = "DGV_Viw";
                // DGV_Viw.OptionsImageLoad.AsyncLoad = true;
                DGV_Viw.OptionsImageLoad.AnimationType = ImageContentAnimationType.SegmentedFade;

                #region OptionsBehavior

                DGV_Viw.OptionsBehavior.AllowSortAnimation = DefaultBoolean.True;
                DGV_Viw.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
                DGV_Viw.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
                DGV_Viw.OptionsBehavior.AllowFixedGroups = DefaultBoolean.True;
                DGV_Viw.OptionsBehavior.AllowGroupExpandAnimation = DefaultBoolean.True;
                DGV_Viw.OptionsBehavior.AllowIncrementalSearch = true;
                DGV_Viw.OptionsBehavior.Editable = false;

                DGV_Viw.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;

                #endregion

                DGV_Viw.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.PlainText;

                #region OptionsCustomization

                DGV_Viw.OptionsCustomization.AllowBandMoving = false;
                DGV_Viw.OptionsCustomization.AllowChangeBandParent = true;

                #endregion

                #region OptionsFind

                DGV_Viw.OptionsFind.FindDelay = 120;
                DGV_Viw.OptionsFind.FindMode = FindMode.Always;
                DGV_Viw.OptionsFind.FindNullPrompt = "عبارت جهت جستجو را وارد نمایید";
                DGV_Viw.OptionsFind.ShowCloseButton = false;
                DGV_Viw.OptionsFind.ShowFindButton = false;

                #endregion

                #region OptionsLayout

                DGV_Viw.OptionsLayout.Columns.AddNewColumns = false;
                DGV_Viw.OptionsLayout.Columns.StoreAllOptions = true;
                DGV_Viw.OptionsLayout.Columns.StoreAppearance = true;
                DGV_Viw.OptionsLayout.StoreDataSettings = false;
                DGV_Viw.OptionsLayout.StoreVisualOptions = false;

                #endregion

                #region OptionsMenu

                DGV_Viw.OptionsMenu.DialogFormBorderEffect = FormBorderEffect.Glow;
                DGV_Viw.OptionsMenu.EnableColumnMenu = false;
                DGV_Viw.OptionsMenu.EnableFooterMenu = false;
                DGV_Viw.OptionsMenu.EnableGroupPanelMenu = false;
                DGV_Viw.OptionsMenu.ShowFooterItem = true;
                DGV_Viw.OptionsMenu.ShowGroupSummaryEditorItem = true;

                #endregion

                #region OptionsNavigation

                DGV_Viw.OptionsNavigation.EnterMoveNextColumn = true;

                #endregion

                #region OptionsPrint

                DGV_Viw.OptionsPrint.AutoWidth = false;
                DGV_Viw.OptionsPrint.PrintFilterInfo = true;

                #endregion

                #region OptionsSelection

                DGV_Viw.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
                DGV_Viw.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DefaultBoolean.False;

                #endregion

                #region OptionsView

                DGV_Viw.OptionsView.AllowHtmlDrawGroups = false;
                DGV_Viw.OptionsView.AnimationType = GridAnimationType.AnimateAllContent;
                DGV_Viw.OptionsView.BestFitMode = GridBestFitMode.Full;
                DGV_Viw.OptionsView.BestFitUseErrorInfo = DefaultBoolean.True;
                DGV_Viw.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                DGV_Viw.OptionsView.HeaderFilterButtonShowMode = FilterButtonShowMode.Button;
                DGV_Viw.OptionsView.RowAutoHeight = false;
                DGV_Viw.RowHeight = 28;
                DGV_Viw.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
                DGV_Viw.OptionsView.ShowFooter = true;
                DGV_Viw.OptionsView.ShowGroupPanel = false;
                DGV_Viw.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Indicator;
                LookAndFeel.UseDefaultLookAndFeel = false;
                LookAndFeel.SkinName = "WXICompact";
                SkinContainerCollection skins = SkinManager.Default.Skins;
                #endregion


                DGV_Viw.ViewCaptionHeight = 20;
                DGV_Viw.NewItemRowText = @"افزودن اطلاعات جدید";

                #region Create Events

                DGV_Viw.CustomDrawBandHeader += CustomDrawBandHeader;
                DGV_Viw.CustomDrawCell += CustomDrawCell;
                DGV_Viw.ColumnWidthChanged += ColumnWidthChanged;
                DGV_Viw.KeyDown += KeyDown1;
                DGV_Viw.DoubleClick += DoubleClick1;
                DGV_Viw.RowCellClick += RowCellClick;
                DGV_Viw.RowStyle += RowStyle;
                DGV_Viw.KeyDown += (s, e) =>
                {
                    // var getClm = GetColumn();
                    if (e.Control && e.KeyCode == Keys.C)
                    {
                        Clipboard.SetText(DGV_Viw.GetFocusedDisplayText());
                        e.Handled = true;
                    }
                };
                DGV_Viw.MouseUp += MouseUp1;

                #endregion
            });
        }

        private void RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.Column == null)
                return;
        }
        private Color _color = Color.White;
        private void RowStyle(object sender, RowStyleEventArgs e)
        {
            //   if (rowEvenOld)
            //   if (DGV_Viw.RowCount > 0)
            //   e.Appearance.BackColor = e.RowHandle % 2 == 0 ? _color : Color.White;
            // e.Appearance.BackColor = e.RowHandle % 2 == 0 ? Color.FromArgb(209, 245, 233) : Color.White;
        }
        private void MouseUp1(object sender, MouseEventArgs e)
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
        private void CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            e.Info.AllowColoring = true;
            e.Info.AllowEffects = true;
        }
        private void CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
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
        private void ColumnWidthChanged(object sender, ColumnEventArgs e)
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
        private void KeyDown1(object sender, KeyEventArgs e)
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
        private void DoubleClick1(object sender, EventArgs e)
        {
            var getDbc = CheckDoubleClickRowCell(sender, e);
            if (getDbc)
                z_RowCellDoubleClick?.Invoke(this.DGV_Viw, e);
        }
        [Category("Double Click Row & Cell")]
        public event EventHandler z_RowCellDoubleClick;
        public bool CheckDoubleClickRowCell(object s, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            //if (s is GridView view && ea != null)
            if (s is BandedGridView view && ea != null)
            {
                GridHitInfo info = view.CalcHitInfo(ea.Location);
                if (info.InRow || info.InRowCell)
                    return true;
            }
            else if (s is GridView view1 && ea != null)
            {
                GridHitInfo info = view1.CalcHitInfo(ea.Location);
                if (info.InRow || info.InRowCell)
                    return true;
            }

            return false;

        }
        //#endregion
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



    }
}
