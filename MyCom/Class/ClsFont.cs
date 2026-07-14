using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using DevExpress.Utils;
using DevExpress.Utils.Animation;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraTab;
using DevExpress.XtraWaitForm;
using MyCom.Object;
using Application = System.Windows.Forms.Application;
using Color = System.Drawing.Color;
using FontStyle = System.Drawing.FontStyle;
using MessageBox = System.Windows.Forms.MessageBox;
using NavigationPane = DevExpress.XtraBars.Navigation.NavigationPane;
using RibbonControl = DevExpress.XtraBars.Ribbon.RibbonControl;
using Size = System.Drawing.Size;

namespace MyCom.Class
{
    public class ClsFont
    {
        PrivateFontCollection _dirFont = new PrivateFontCollection();

        private static readonly string samim = @"FontSamim\Samim.ttf";
        private static readonly string samimFD = @"FontSamim\Samim-FD.ttf";
        private static readonly string samimBold = @"FontSamim\Samim_Bold.ttf";
        private static readonly string samimBoldFD = @"FontSamim\Samim_Bold_FD.ttf";
        private static readonly string samimWOL = @"FontSamim\Samim_WOL.ttf";
        private static readonly string samimBoldFDWOL = @"FontSamim\Samim_Bold_FD_WOL.ttf";
        private static readonly string samimBoldWOL = @"FontSamim\Samim_Bold_WOL.ttf";
        private static readonly string iranNastaliq = @"FontSamim\IranNastaliq.ttf";

        private string nameFont = samimFD;

        FontStyle _fontStyle = FontStyle.Regular;
        public enum enumFont
        {
            samim,
            samimFD,
            samimBold,
            samimBoldFD,
            samimWOL,
            samimBoldFDWOL,
            samimBoldWOL,
        }
        public ClsFont(bool bold = false)
        {
            if (bold)
                _fontStyle = FontStyle.Bold;

            DevExpress.UserSkins.BonusSkins.Register();
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\FontSamim"))
                    Directory.CreateDirectory(Application.StartupPath + @"\FontSamim");

                File.WriteAllBytes(Application.StartupPath + @"\" + samim, Properties.Resources.Samim);
                File.WriteAllBytes(Application.StartupPath + @"\" + nameFont, Properties.Resources.Samim_FD);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBold, Properties.Resources.Samim_Bold);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBoldFD, Properties.Resources.Samim_Bold_FD);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimWOL, Properties.Resources.Samim_WOL);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBoldFDWOL, Properties.Resources.Samim_Bold_FD_WOL);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBoldWOL, Properties.Resources.Samim_Bold_WOL);
            }
            catch
            {
                // ignored
            }

            try
            {
                if (_dirFont.Families.Length < 1)
                {
                    //var getV = Lic1();

                    //if (getV)
                    //    _dirFont.AddFontFile(iranNastaliq);
                    //else
                    _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
                }
            }
            catch
            {
                // ignored  
            }
        }
        public ClsFont(enumFont enumFont, bool bold = false)
        {
            if (bold)
                _fontStyle = FontStyle.Bold;

            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\FontSamim"))
                    Directory.CreateDirectory(Application.StartupPath + @"\FontSamim");

                File.WriteAllBytes(Application.StartupPath + @"\" + samim, Properties.Resources.Samim);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimFD, Properties.Resources.Samim_FD);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBold, Properties.Resources.Samim_Bold);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBoldFD, Properties.Resources.Samim_Bold_FD);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimWOL, Properties.Resources.Samim_WOL);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBoldFDWOL, Properties.Resources.Samim_Bold_FD_WOL);
                File.WriteAllBytes(Application.StartupPath + @"\" + samimBoldWOL, Properties.Resources.Samim_Bold_WOL);
            }
            catch
            {
                // ignored
            }

            try
            {
                //  MessageBox.Show(enumFont.ToString());
                if (_dirFont.Families.Length < 1)
                {
                    //  var getV = Lic1();
                    var getV = false;

                    if (enumFont == enumFont.samim)
                        _dirFont.AddFontFile(getV ? iranNastaliq : samim);

                    else if (enumFont == enumFont.samimFD)
                        _dirFont.AddFontFile(getV ? iranNastaliq : samimFD);

                    else if (enumFont == enumFont.samimBold)
                        _dirFont.AddFontFile(getV ? iranNastaliq : samimBold);

                    else if (enumFont == enumFont.samimBoldFDWOL)
                        _dirFont.AddFontFile(samimBoldFDWOL);


                    #region Change Fonts
                    // امیر خان فرمودند انگلیسی
                    //TODO: Run
                    else if (enumFont == enumFont.samimBoldFD)
                        //  _dirFont.AddFontFile(samim);
                        _dirFont.AddFontFile(samimBoldFD);
                    #endregion



                    else if (enumFont == enumFont.samimWOL)
                        _dirFont.AddFontFile(samimWOL);
                }
            }
            catch
            {
                // ignored
            }
        }
        public Font ChangeFont(float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            FontStyle fontStyle = _fontStyle;
            var getFont = new Font(_dirFont.Families[0], size, fontStyle, GraphicsUnit.Pixel);
            return getFont;
        }

        //
        public void ChangeFont(Control control, float size = 13f)
        {
            // var getType = control.GetType();
            //    if (grp is LayoutControlGroup _lcg)
            if (control is TextEdit textEdit)
            {
                ChangeFont(textEdit, size);
            }
            if (control is MemoEdit memoEdit)
            {
                ChangeFont(memoEdit, size);
            }
            if (control is ProgressBarControl progressBarControl)
            {
                ChangeFont(progressBarControl, size);
            }
            if (control is MaskedTextBox maskedTextBox)
            {
                ChangeFont(maskedTextBox, size);
            }
            //if (control is Persian_Calendar_Samim calendarSamim)
            //{
            //    ChangeFont(calendarSamim, size);
            //}
            if (control is GridLookUpEdit gridLookUpEdit)
            {
                ChangeFont(gridLookUpEdit, size);
            }
            //if (control is TreeList2 treeList2)
            //{
            //    ChangeFont(treeList2, size);
            //}
            if (control is LabelControl labelControl)
            {
                ChangeFont(labelControl, size);
            }
            if (control is ToggleSwitch toggleSwitch)
            {
                ChangeFont(toggleSwitch, size);
            }

        }
        public void ChangeFont(BarStaticItem _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Appearance.Font = getFont;

            _obj.ItemAppearance.Normal.Font = getFont;
            _obj.ItemAppearance.Disabled.Font = getFont;
            _obj.ItemAppearance.Hovered.Font = getFont;
            _obj.ItemAppearance.Pressed.Font = getFont;
        }
        public void ChangeFont(ProgressBarControl _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            //  _obj.Font = getFont;
            _obj.Properties.Appearance.Font = getFont;

        }
        public void ChangeFont(CheckEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.LookAndFeel.UseDefaultLookAndFeel = false;
            _obj.LookAndFeel.SkinName = "WXI";
            //_obj.LookAndFeel.SkinName = "Office 2013 Dark Gray";

            _obj.Font = getFont;
        }
        public void ChangeFont(MaskedTextBox _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Font = getFont;
        }
        public void ChangeFont(GroupControl _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Font = getFont;
            _obj.Appearance.Font = getFont;
            _obj.AppearanceCaption.Font = getFont;
        }
        //public void ChangeFont(TreeList _obj, float size = 13f)
        //{
        //    if (_dirFont.Families.Length < 1)
        //        _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

        //    var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

        //    _obj.treeMap.Appearance.HeaderPanel.Font = getFont;
        //    _obj.treeMap.Appearance.Preview.Font = getFont;
        //    _obj.treeMap.Appearance.Row.Font = getFont;
        //    _obj.treeMap.Appearance.SelectedRow.Font = getFont;
        //    _obj.treeMap.AppearancePrint.Caption.Font = getFont;
        //    _obj.treeMap.Font = getFont;
        //}
        //public void ChangeFont(TreeList2 _obj, float size = 13f)
        //{
        //    if (_dirFont.Families.Length < 1)
        //        _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

        //    var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
        //    var getFont2 = new Font(_dirFont.Families[0], size - 1, _fontStyle, GraphicsUnit.Pixel);

        //    _obj.treeList.Appearance.HeaderPanel.Font = getFont;
        //    _obj.treeList.Appearance.Preview.Font = getFont;
        //    _obj.treeList.Appearance.Row.Font = getFont;
        //    _obj.treeList.Appearance.SelectedRow.Font = getFont;
        //    _obj.treeList.AppearancePrint.Caption.Font = getFont;
        //    // _obj.txtAddressFull.Font = getFont2;
        //    _obj.txtAddressFull.Properties.Appearance.Font = getFont2;
        //    _obj.txtAddressFull.Properties.AppearanceDisabled.Font = getFont2;
        //    _obj.txtAddressFull.Properties.AppearanceFocused.Font = getFont2;
        //    _obj.txtAddressFull.Properties.AppearanceReadOnly.Font = getFont2;
        //    _obj.schControl.Font = getFont;
        //    _obj.treeList.Font = getFont;
        //}
        public void ChangeFont(AppearanceObject _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Font = getFont;
        }
        //public void ChangeFont(Persian_Calendar_Samim _obj)
        //{
        //    ChangeFont(_obj.View_Calander);
        //    ChangeFont(_obj.Lbl_Mounth);
        //    ChangeFont(_obj.Lbl_Year);
        //    ChangeFont(_obj.Btn_Today);
        //    ChangeFont(_obj.Com_Date);
        //}
        //public void ChangeFont(Date_Samim _obj)
        //{
        //    ChangeFont(_obj.Cmb_Day);
        //    ChangeFont(_obj.Cmb_Month);
        //    ChangeFont(_obj.Cmb_Year);
        //}
        //public void ChangeFont(Date_SamimMiladi _obj)
        //{
        //    ChangeFontMiladi(_obj.Cmb_Day);
        //    ChangeFontMiladi(_obj.Cmb_Month);
        //    ChangeFontMiladi(_obj.Cmb_Year);
        //}
        public void ChangeFont(TextEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Font = getFont;
            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceReadOnly.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;

        }
        public void ChangeFont(DevExpress.XtraTreeList.TreeList _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Appearance.HeaderPanel.Font = getFont;
            _obj.Appearance.Preview.Font = getFont;
            _obj.Appearance.Row.Font = getFont;
            _obj.Appearance.SelectedRow.Font = getFont;
            _obj.AppearancePrint.Caption.Font = getFont;
            _obj.Font = getFont;
        }

        public void ChangeFont(GridView _obj, float size = 13, float sizePrint = 9f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Appearance.Row.Font = getFont;
            _obj.Appearance.SelectedRow.Font = getFont;
            _obj.Appearance.ViewCaption.Font = getFont;
            _obj.Appearance.FilterPanel.Font = getFont;
            _obj.Appearance.GroupRow.Font = getFont; // Group By

            var getFont1 = new Font(_dirFont.Families[0], (float)(size), _fontStyle, GraphicsUnit.Pixel);
            var getFont3 = new Font(_dirFont.Families[0], (float)(size + 1), _fontStyle, GraphicsUnit.Pixel);
            _obj.Appearance.GroupPanel.Font = getFont1;
            _obj.Appearance.HeaderPanel.Font = getFont3;
            _obj.Appearance.GroupFooter.Font = getFont1;
            _obj.Appearance.FooterPanel.Font = getFont1;

            _obj.Appearance.GroupRow.ForeColor = Color.FromArgb(35, 53, 179);

            var getFont2 = new Font("B Nazanin", sizePrint, _fontStyle, GraphicsUnit.Point);
            _obj.AppearancePrint.HeaderPanel.Font = getFont2;
            _obj.AppearancePrint.Row.Font = getFont2;
            _obj.AppearancePrint.FooterPanel.Font = getFont2;
            _obj.AppearancePrint.GroupFooter.Font = getFont2;
            _obj.AppearancePrint.Lines.Font = getFont2;
            _obj.AppearancePrint.Preview.Font = getFont2;
        }
        /// <summary>
        /// اندازه ستون های 
        /// *************
        /// انتخاب ، ویرایش و حذف
        /// </summary>
        /// <param name="_obj"></param>
        /// <param name="size"></param>
        public void ChangeFont(Data_Grid _obj, float size = 11f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.schLabel.Font = getFont;
            _obj.schControl.Font = getFont;

            //_obj.schControl.Properties.Appearance.Font = getFont;
            //_obj.schControl.Properties.AppearanceDropDown.Font = getFont;
            //_obj.schControl.Properties.AppearanceFocused.Font = getFont;
            //_obj.schControl.Properties.AppearanceDisabled.Font = getFont;

            _obj.LookAndFeel.UseDefaultLookAndFeel = false;
            _obj.LookAndFeel.SkinName = "Office 2016 Colorful";

            // _obj.LookAndFeel.SkinName = "Black";
            // _obj.LookAndFeel.SkinName = "Office 2019 White";
            // _obj.LookAndFeel.SkinName = "Office 2019 Colorful";
            // _obj.DGV.LookAndFeel.UseDefaultLookAndFeel = false;
            // _obj.DGV.LookAndFeel.SkinName = "Office 2019 Colorful";

            //if (_obj.DGV_Viw.Columns.Count > 0)
            //{
            //    try
            //    {
            //        _obj.DGV_Viw.Columns["انتخاب"].AppearanceHeader.Font = getFont;
            //        _obj.DGV_Viw.Columns["حذف"].AppearanceHeader.Font = getFont;
            //        _obj.DGV_Viw.Columns["ویرایش"].AppearanceHeader.Font = getFont;
            //    }
            //    catch
            //    {
            //        // ignored
            //    }
            //}
        }
        public void ChangeFont(KavoshGrid _obj1, float size = 13, float sizePrint = 9f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            //_obj1.LookAndFeel.UseDefaultLookAndFeel = false;
            // _obj1.LookAndFeel.SkinName = "Office 2016 Black";
            // _obj1.LookAndFeel.SkinName = "Office 2016 Colorful";

            var _obj = _obj1.MainView as BandedGridView;

            _obj.Appearance.Row.Font = getFont;
            _obj.Appearance.SelectedRow.Font = getFont;
            _obj.Appearance.ViewCaption.Font = getFont;
            _obj.Appearance.FilterPanel.Font = getFont;
            _obj.Appearance.GroupRow.Font = getFont; // Group By

            var getFont1 = new Font(_dirFont.Families[0], (float)(size), _fontStyle, GraphicsUnit.Pixel);
            var getFont3 = new Font(_dirFont.Families[0], (float)(size + 1), _fontStyle, GraphicsUnit.Pixel);
            _obj.Appearance.GroupPanel.Font = getFont1;
            _obj.Appearance.HeaderPanel.Font = getFont3;
            _obj.Appearance.GroupFooter.Font = getFont1;
            _obj.Appearance.FooterPanel.Font = getFont1;

            _obj.Appearance.GroupRow.ForeColor = Color.FromArgb(35, 53, 179);

            var getFont2 = new Font("B Nazanin", sizePrint, _fontStyle, GraphicsUnit.Point);
            _obj.AppearancePrint.HeaderPanel.Font = getFont2;
            _obj.AppearancePrint.Row.Font = getFont2;
            _obj.AppearancePrint.FooterPanel.Font = getFont2;
            _obj.AppearancePrint.GroupFooter.Font = getFont2;
            _obj.AppearancePrint.Lines.Font = getFont2;
            _obj.AppearancePrint.Preview.Font = getFont2;
        }
        public void ChangeFont(TileView _obj, float size = 13, float sizePrint = 9f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Appearance.ItemNormal.Font = getFont;
            _obj.Appearance.ItemFocused.Font = getFont;
            _obj.Appearance.ItemSelected.Font = getFont;
            _obj.Appearance.ItemPressed.Font = getFont;
            // _obj.Appearance.SelectedRow.Font = getFont;
            _obj.Appearance.ViewCaption.Font = getFont;
            //_obj.Appearance.FilterPanel.Font = getFont;
            //_obj.Appearance.GroupRow.Font = getFont; // Group By

            var getFont1 = new Font(_dirFont.Families[0], (float)(size), _fontStyle, GraphicsUnit.Pixel);
            var getFont3 = new Font(_dirFont.Families[0], (float)(size + 1), _fontStyle, GraphicsUnit.Pixel);
            //_obj.Appearance.GroupPanel.Font = getFont1;
            //_obj.Appearance.HeaderPanel.Font = getFont3;
            //_obj.Appearance.GroupFooter.Font = getFont1;
            //_obj.Appearance.FooterPanel.Font = getFont1;

            //_obj.Appearance.GroupRow.ForeColor = Color.FromArgb(35, 53, 179);


        }
        public void ChangeFont(GridColumn _obj, float size = 11f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            if (_obj != null) _obj.AppearanceHeader.Font = getFont;
        }
        public void ChangeFont(RepositoryItemGridLookUpEdit _obj, float size = 11f)
        {
            GridView repositoryItemGridLookUpEdit1View = new GridView();
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.View.Appearance.Row.Options.UseFont = true;
            _obj.View.Appearance.GroupPanel.Font = getFont;
            _obj.View.Appearance.HeaderPanel.Font = getFont;
            _obj.View.Appearance.Row.Font = getFont;
            _obj.View.Appearance.SelectedRow.Font = getFont;
            _obj.View.Appearance.SelectedRow.Options.UseFont = true;
            _obj.View.Appearance.ViewCaption.Font = getFont;
            _obj.View.AppearancePrint.HeaderPanel.Font = getFont;
            _obj.View.Appearance.FooterPanel.Font = getFont;

            repositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            repositoryItemGridLookUpEdit1View.Appearance.GroupPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.Row.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.SelectedRow.Font = getFont;

            repositoryItemGridLookUpEdit1View.Appearance.SelectedRow.Options.UseFont = true;
            repositoryItemGridLookUpEdit1View.Appearance.ViewCaption.Font = getFont;
            repositoryItemGridLookUpEdit1View.AppearancePrint.HeaderPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.FooterPanel.Font = getFont;
            // repositoryItemGridLookUpEdit1View.Appearance.FooterPanel.Font = getFont;

            ////_obj.Appearance.Font = getFont;
            ////_obj.AppearanceDropDown.Font = getFont;
            ////_obj.AppearanceReadOnly.Font = getFont;
            ////_obj.AppearanceFocused.Font = getFont;

            _obj.PopupView = repositoryItemGridLookUpEdit1View;

            // _obj.PopupView
        }
        public void ChangeFont(GridLookUpEdit _obj, float size = 11f)
        {
            GridView repositoryItemGridLookUpEdit1View = new GridView();
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Font = getFont;

            // Font Monitor get font new Apprecne Row Ppostion 

            _obj.Properties.View.Appearance.Row.Options.UseFont = true;
            _obj.Properties.View.Appearance.GroupPanel.Font = getFont;
            _obj.Properties.View.Appearance.HeaderPanel.Font = getFont;
            _obj.Properties.View.Appearance.Row.Font = getFont;
            _obj.Properties.View.Appearance.SelectedRow.Font = getFont;
            _obj.Properties.View.Appearance.SelectedRow.Options.UseFont = true;
            _obj.Properties.View.Appearance.ViewCaption.Font = getFont;
            _obj.Properties.View.AppearancePrint.HeaderPanel.Font = getFont;
            _obj.Properties.View.Appearance.FooterPanel.Font = getFont;

            repositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            repositoryItemGridLookUpEdit1View.Appearance.GroupPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.Row.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.SelectedRow.Font = getFont;

            repositoryItemGridLookUpEdit1View.Appearance.SelectedRow.Options.UseFont = true;
            repositoryItemGridLookUpEdit1View.Appearance.ViewCaption.Font = getFont;
            repositoryItemGridLookUpEdit1View.AppearancePrint.HeaderPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.FooterPanel.Font = getFont;
            repositoryItemGridLookUpEdit1View.Appearance.FooterPanel.Font = getFont;


            //_obj.Appearance.Font = getFont;
            //_obj.AppearanceDropDown.Font = getFont;
            //_obj.AppearanceReadOnly.Font = getFont;
            //_obj.AppearanceFocused.Font = getFont;


            _obj.Properties.PopupView = repositoryItemGridLookUpEdit1View;

            // _obj.PopupView


        }
        public void ChangeFont(LookUpEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Font = getFont;

            // Font Monitor get font new Apprecne Row Ppostion 

            _obj.Properties.Appearance.Options.UseFont = true;
            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDropDown.Options.UseFont = true;
            _obj.Properties.AppearanceDropDown.Font = getFont;




            //_obj.Appearance.Font = getFont;
            //_obj.AppearanceDropDown.Font = getFont;
            //_obj.AppearanceReadOnly.Font = getFont;
            //_obj.AppearanceFocused.Font = getFont;



            // _obj.PopupView


        }
        public void ChangeFont(SearchControl _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            _obj.Properties.AppearanceDropDown.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;
            _obj.Properties.AppearanceReadOnly.Font = getFont;
            _obj.Properties.NullValuePrompt = "جستجو ...";

        }

        public void ChangeFont(ComboBoxEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            _obj.Properties.AppearanceDropDown.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;
            _obj.Properties.AppearanceReadOnly.Font = getFont;

        }
        public void ChangeFontMiladi(ComboBoxEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(samim);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            _obj.Properties.AppearanceDropDown.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;
            _obj.Properties.AppearanceReadOnly.Font = getFont;

        }
        public void ChangeFont(LabelControl _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

        }
        public void ChangeFont(ToolStripMenuItem _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);


        }
        public void ChangeFont(Label _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

        }
        public void ChangeFont(BarItemLink _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Item.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.AppearanceDisabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Item.ItemAppearance.Normal.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemAppearance.Disabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemAppearance.Hovered.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemAppearance.Pressed.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Item.ItemInMenuAppearance.Normal.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemInMenuAppearance.Disabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemInMenuAppearance.Hovered.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemInMenuAppearance.Pressed.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
        }
        public void ChangeFont(BarSubItemLink _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Item.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.AppearanceDisabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Item.ItemAppearance.Normal.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemAppearance.Disabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemAppearance.Hovered.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemAppearance.Pressed.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Item.ItemInMenuAppearance.Normal.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemInMenuAppearance.Disabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemInMenuAppearance.Hovered.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Item.ItemInMenuAppearance.Pressed.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
        }
        public void ChangeFont(BarSubItem _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.ItemAppearance.SetFont(new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel));
            _obj.ItemInMenuAppearance.SetFont(new Font(_dirFont.Families[0], size, _fontStyle,
                GraphicsUnit.Pixel));
        }
        public void ChangeFont(RibbonPageGroup _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Page.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            // .Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);


        }
        public void ChangeFont(SimpleButton _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.LookAndFeel.UseDefaultLookAndFeel = false;
            _obj.LookAndFeel.SkinName = "WXI";
            //_obj.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            // _obj.LookAndFeel.SkinName = "Black";

        }
        public void ChangeFont(BarButtonItem _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.ItemAppearance.SetFont(new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel));
            _obj.ItemInMenuAppearance.SetFont(new Font(_dirFont.Families[0], size, _fontStyle,
                GraphicsUnit.Pixel));
        }
        public void ChangeFont(RibbonControl _control, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _control.Manager.ShowScreenTipsInMenus = true;
            foreach (RibbonPage page in _control.Pages)
            {
                page.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
                foreach (RibbonPageGroup group in page.Groups)
                {
                    ChangeFont(group, size);

                    foreach (var link in group.ItemLinks)
                    {
                        if (link is BarItemLink link2)
                        {
                            ChangeFont(link2, size);
                            if (link2.GetSuperTip() != null)
                            {
                                foreach (ToolTipItem gi in link2.GetSuperTip().Items)
                                {
                                    gi.Appearance.Font = new Font(_dirFont.Families[0], size - 1, _fontStyle, GraphicsUnit.Pixel);
                                    gi.Appearance.ForeColor = Color.FromArgb(69, 130, 7);
                                    gi.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                                    gi.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                                    gi.ImageOptions.SvgImage = Properties.Resources.bo_price;
                                    gi.ImageOptions.SvgImageSize = new Size(20, 20);
                                    gi.ImageOptions.Alignment = ToolTipImageAlignment.Right;
                                }
                            }
                        }
                        if (link is BarSubItemLink linkS)
                        {
                            foreach (BarItemLink d in linkS.Item.ItemLinks)
                            {
                                ChangeFont(d, size);

                                if (d.GetSuperTip() != null)
                                {
                                    foreach (ToolTipItem gi in d.GetSuperTip().Items)
                                    {
                                        //gi.ImageOptions.ImageToTextDistance
                                        gi.Appearance.Font = new Font(_dirFont.Families[0], size - 1, _fontStyle, GraphicsUnit.Pixel);
                                        gi.Appearance.ForeColor = Color.FromArgb(69, 130, 7);
                                        gi.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                                        gi.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                                        gi.ImageOptions.SvgImage = Properties.Resources.bo_price;
                                        gi.ImageOptions.SvgImageSize = new Size(20, 20);
                                        gi.ImageOptions.Alignment = ToolTipImageAlignment.Right;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ChangeFont(XtraTabControl _obj, float size = 12.5f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Appearance.Font = getFont;
            _obj.AppearancePage.Header.Font = getFont;


            foreach (XtraTabPage page in _obj.TabPages)
            {
                page.Appearance.Header.Font = getFont;
                //  page.AppearancePage.Header.Font = getFont;
            }
        }
        public void ChangeFont(TabPane _obj, float size = 12.5f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var _normal = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            var _pressed = new Font(_dirFont.Families[0], size + 1f, _fontStyle, GraphicsUnit.Pixel);
            var _hovered = new Font(_dirFont.Families[0], size + 1.5f, _fontStyle, GraphicsUnit.Pixel);

            _obj.Appearance.Font = _normal;
            _obj.AppearanceButton.Normal.Font = _normal;
            _obj.AppearanceButton.Pressed.Font = _pressed;
            _obj.AppearanceButton.Pressed.ForeColor = Color.FromArgb(192, 0, 0);
            _obj.AppearanceButton.Hovered.Font = _hovered;

            _obj.AllowTransitionAnimation = DefaultBoolean.True;
            _obj.TransitionAnimationProperties.FrameCount = 700;
            _obj.TransitionAnimationProperties.FrameInterval = 5000;
            _obj.TransitionType = Transitions.Fade;

            //_obj.LookAndFeel.UseDefaultLookAndFeel = false;
            //_obj.LookAndFeel.SkinName = "The Bezier";
        }
        public void ChangeFont(CheckedComboBoxEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            // _obj.Font = getFont;

            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            _obj.Properties.AppearanceDropDown.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;
            _obj.Properties.AppearanceReadOnly.Font = getFont;

            _obj.Properties.Appearance.Options.UseFont = true;
            _obj.Properties.AppearanceDisabled.Options.UseFont = true;
            _obj.Properties.AppearanceDropDown.Options.UseFont = true;
            _obj.Properties.AppearanceFocused.Options.UseFont = true;
            _obj.Properties.AppearanceReadOnly.Options.UseFont = true;
        }
        public void ChangeFont(RibbonPage _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
        }
        public void ChangeFont(ProgressPanel _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont2 = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            // _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.AppearanceCaption.Font = getFont2;
            _obj.AppearanceDescription.Font = getFont2;
        }
        public void ChangeFont(RepositoryItemProgressBar _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            // _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
        }
        public void ChangeFont(RepositoryItemMemoEdit _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            // _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            _obj.Appearance.ForeColor = Color.Black;
            _obj.AppearanceFocused.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            _obj.AppearanceReadOnly.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            _obj.AppearanceDisabled.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            _obj.AppearanceFocused.ForeColor = Color.FromArgb(255, 94, 0);
        }
        public void ChangeFont(RepositoryItemTextEdit _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            // _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
        }
        public void ChangeFont(RibbonControl _control, int _numPage, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            // foreach (RibbonPageGroup group in _control.SelectedPage.Groups)
            //foreach (RibbonPageGroup group in _control.Pages[_numPage].Groups)
            //{
            //    foreach (BarItemLink link in group.ItemLinks)
            //    {
            //        ChangeFont(link, size);
            //    }
            //}

            foreach (RibbonPage page in _control.Pages)
            {
                page.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
                foreach (RibbonPageGroup group in page.Groups)
                {
                    ChangeFont(group, size);
                    foreach (BarItemLink link in group.ItemLinks)
                    {
                        ChangeFont(link, size);

                        // link.Alignment .AllowHtmlText = DefaultBoolean.True;
                    }
                }
            }
        }
        public void ChangeFont(ListBoxControl _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            _obj.Appearance.Font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);


        }
        public void ChangeFont(BarManager _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            var f = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            foreach (BarItem bar in _obj.Items)
            {
                bar.ItemInMenuAppearance.Normal.Font = f;
                bar.ItemInMenuAppearance.Disabled.Font = f;
                bar.ItemInMenuAppearance.Pressed.Font = f;
                bar.ItemInMenuAppearance.Hovered.Font = f;
            }
        }
        public void ChangeFont(AccordionControl obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
            var f = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);
            var f2 = new Font(_dirFont.Families[0], size - 1, _fontStyle, GraphicsUnit.Pixel);

            obj.Appearance.AccordionControl.Font = f;
            obj.Appearance.Group.Disabled.Font = f;
            obj.Appearance.Group.Hovered.Font = f;
            obj.Appearance.Group.Normal.Font = f;
            obj.Appearance.Group.Pressed.Font = f;
            obj.Appearance.Hint.Font = f;
            obj.Appearance.Item.Disabled.Font = f;
            obj.Appearance.Item.Hovered.Font = f2;
            obj.Appearance.Item.Normal.Font = f;
            obj.Appearance.Item.Pressed.Font = f;
        }
        public void ChangeFont(TimeSpanEdit _obj, float size = 13f)
        {

            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.Properties.AllowEditDays = false;
            _obj.Properties.AllowEditSeconds = false;

            _obj.Properties.Mask.EditMask = @"HH:mm";
            _obj.Properties.MaxDays = 0;
            _obj.Properties.MaxMilliseconds = 0;
            _obj.Properties.MaxSeconds = 0;

            _obj.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            _obj.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            _obj.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            //  _obj.Properties.AppearanceDropDown.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;
            // _obj.Properties.AppearanceReadOnly.Font = getFont;

            //  _obj.Properties.DisplayFormat.FormatString = @"(0?\d|1\d|2[0-3])\:[0-5]\d";
            _obj.Properties.Mask.MaskType = MaskType.RegEx;
            _obj.Properties.Mask.EditMask = @"(0?\d|1\d|2[0-3])\:[0-5]\d";

            //_obj.Properties.Appearance.Options.UseFont = true;
            //_obj.Properties.AppearanceDisabled.Options.UseFont = true;
            //_obj.Properties.AppearanceDropDown.Options.UseFont = true;
            //_obj.Properties.AppearanceFocused.Options.UseFont = true;
            //_obj.Properties.AppearanceReadOnly.Options.UseFont = true;
        }
        public void ChangeFont(TimeEdit _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            _obj.EditValue = TimeSpan.Parse("00:00");
            _obj.Properties.BeepOnError = false;
            _obj.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            _obj.Properties.MaskSettings.Set("mask", "HH:mm");
            _obj.Properties.TimeEditStyle = TimeEditStyle.TouchUI;
            _obj.Properties.UseMaskAsDisplayFormat = true;
            //_obj.EnterMoveNextControl = true;

            _obj.Properties.TextEditStyle = TextEditStyles.Standard;

            _obj.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            _obj.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Center;

            _obj.Properties.Appearance.Font = getFont;
            _obj.Properties.AppearanceDisabled.Font = getFont;
            _obj.Properties.AppearanceFocused.Font = getFont;
            _obj.Properties.AdvancedModeOptions.LabelAppearance.Font = getFont;
            _obj.Properties.AdvancedModeOptions.ShiftedLabelAppearance.Font = getFont;
        }
        public void ChangeFont(CheckedListBoxControl _obj, float size = 13f)
        {
            if (_dirFont.Families.Length < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var getFont = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Pixel);

            // _obj.LookAndFeel.UseDefaultLookAndFeel = false;
            // _obj.LookAndFeel.SkinName = "Office 2013 Dark Gray";

            _obj.Font = getFont;
            _obj.Appearance.Font = getFont;
            _obj.AppearanceDisabled.Font = getFont;
            _obj.AppearanceSelected.Font = getFont;
            _obj.AppearanceHighlight.Font = getFont;
        }

        ///// <summary>
        ///// vGridControl
        ///// </summary>
        ///// <param name="_obj"></param>
        ///// <param name="size"></param>
        //public void ChangeFont(vGrid _obj, float size = 11f)
        //{
        //    var getFont = _dirFont.Families.Length;
        //    if (getFont < 1)
        //        _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);
        //    //  var f = new Font("Samim FD", size, _fontStyle, GraphicsUnit.Point, 0);
        //    var f = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);

        //    _obj.data.Appearance.RowHeaderPanel.Font = f;
        //    _obj.data.Appearance.DisabledRow.Font = f;
        //    _obj.data.Appearance.FocusedRow.Font = f;
        //    _obj.data.Appearance.HideSelectionRow.Font = f;
        //    _obj.data.Appearance.ModifiedRow.Font = f;
        //    _obj.data.Appearance.SelectedRow.Font = f;
        //    _obj.data.Appearance.ReadOnlyRow.Font = f;
        //    _obj.data.Appearance.SelectedCell.Font = f;
        //    _obj.data.Appearance.FocusedCell.Font = f;
        //    _obj.data.Appearance.RecordValue.Font = f;
        //    _obj.data.Appearance.Category.Font = f;
        //    _obj.data.Appearance.HorzLine.Font = f;
        //    _obj.data.Appearance.VertLine.Font = f;
        //    _obj.data.Appearance.PressedRow.Font = f;
        //    _obj.data.Appearance.FocusedRecord.Font = f;
        //    _obj.data.Appearance.SelectedRecord.Font = f;
        //    _obj.data.Appearance.Empty.Font = f;
        //    _obj.data.Appearance.SelectedCategory.Font = f;

        //    _obj.data.Appearance.ModifiedRecordValue.BackColor = Color.FromArgb(191, 239, 42);

        //    _obj.data.Appearance.DisabledRecordValue.ForeColor = Color.FromArgb(93, 0, 19);
        //    _obj.data.Appearance.DisabledRecordValue.BackColor2 = Color.FromArgb(196, 208, 210);
        //    _obj.data.Appearance.DisabledRecordValue.BackColor = Color.FromArgb(213, 238, 229);

        //    //  _obj.data.Appearance.ModifiedRow.BackColor = Color.FromArgb(191, 239, 42);
        //    _obj.data.Appearance.ModifiedRow.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.ModifiedRow.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.RecordValue.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.RecordValue.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.RowHeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.RowHeaderPanel.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.DisabledRow.ForeColor = Color.FromArgb(131, 237, 92, 92);
        //    //  _obj.data.Appearance.DisabledRow.BackColor = Color.FromArgb(255, 159, 47, 47);
        //    _obj.data.Appearance.DisabledRow.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.DisabledRow.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.FocusedRow.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.FocusedRow.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.HideSelectionRow.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.HideSelectionRow.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.FocusedCell.BackColor = Color.FromArgb(212, 246, 237);
        //    // _obj.data.Appearance.SelectedCell.BackColor = Color.FromArgb(172, 244, 226);
        //    // _obj.data.Appearance.HorzLine.BackColor = Color.FromArgb(172, 244, 226);

        //    // _obj.data.Appearance.ModifiedRecordValue.BackColor = Color.CadetBlue;
        //    // _obj.data.Appearance.ModifiedRow.BackColor = Color.CadetBlue;


        //    _obj.data.Appearance.SelectedRow.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.SelectedRow.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.ReadOnlyRow.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.ReadOnlyRow.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.SelectedCell.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.SelectedCell.TextOptions.VAlignment = VertAlignment.Center;

        //    _obj.data.Appearance.FocusedCell.TextOptions.HAlignment = HorzAlignment.Center;
        //    _obj.data.Appearance.FocusedCell.TextOptions.VAlignment = VertAlignment.Center;
        //}
        public void ChangeFont(PopupMenu _obj, float size = 10f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            var font2 = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            // var font3 = new Font(_dirFont.Families[0], size +2, _fontStyle, GraphicsUnit.Point, 0);

            _obj.MenuAppearance.Menu.Font = font;
            _obj.MenuAppearance.AppearanceMenu.Normal.Font = font;
            _obj.MenuAppearance.AppearanceMenu.Disabled.Font = font;
            _obj.MenuAppearance.AppearanceMenu.Hovered.Font = font2;
            _obj.MenuAppearance.AppearanceMenu.Pressed.Font = font;

          
        }
        public void ChangeFont(NavigationPane _obj, float size = 12f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var font = new Font(_dirFont.Families[0], size - 1, _fontStyle, GraphicsUnit.Point, 0);
            var font2 = new Font(_dirFont.Families[0], size + 1, _fontStyle, GraphicsUnit.Point, 0);
            var font3 = new Font(_dirFont.Families[0], size + 2, _fontStyle, GraphicsUnit.Point, 0);

            _obj.Font = font;
            _obj.Appearance.Font = font;
            _obj.PageProperties.AppearanceCaption.Font = font;
            _obj.AppearanceButton.Hovered.Font = font;
            _obj.AppearanceButton.Normal.Font = font;
            _obj.AppearanceButton.Pressed.Font = font2;
            _obj.AppearanceButton.Hovered.Font = font3;
            _obj.AppearanceButton.Pressed.ForeColor = Color.FromArgb(234, 31, 31);
            _obj.AppearanceButton.Hovered.ForeColor = Color.FromArgb(235, 106, 106);

            _obj.PageProperties.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
            _obj.PageProperties.AppearanceCaption.TextOptions.VAlignment = VertAlignment.Center;
        }
        public void ChangeFont(DataLayoutControl _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);

            _obj.Root.AppearanceItemCaption.Font = font;
            _obj.Root.AppearanceGroup.Font = font;

            _obj.Root.AppearanceGroup.TextOptions.HAlignment = HorzAlignment.Center;
            _obj.Root.AppearanceGroup.TextOptions.VAlignment = VertAlignment.Top;

            _obj.Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
            _obj.Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
            //  _obj.Root.Padding=new Padding(6);
            //  _obj.Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;

            _obj.Appearance.ControlDropDown.Font = font;
            _obj.Appearance.Control.Font = font;

            _obj.Appearance.Control.TextOptions.HAlignment = HorzAlignment.Center;
            //  _obj.Appearance.Control.TextOptions.VAlignment = VertAlignment.Top;
            _obj.Appearance.Control.TextOptions.VAlignment = VertAlignment.Center;

        }
        public void ChangeFont(SplitContainerControl _obj, float size = 13f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);
            //_obj.Panel1.Font = font;
            _obj.Panel1.AppearanceCaption.Font = font;
            _obj.Panel2.AppearanceCaption.Font = font;

            _obj.Panel1.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
            _obj.Panel1.AppearanceCaption.TextOptions.VAlignment = VertAlignment.Center;

            _obj.Panel2.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
            _obj.Panel2.AppearanceCaption.TextOptions.VAlignment = VertAlignment.Center;
            //_obj.Font = font;
            //  _obj.AppearanceCaption.Font = font;
            //  _obj.Appearance.Font = font;
            _obj.LookAndFeel.UseDefaultLookAndFeel = false;
            _obj.LookAndFeel.SkinName = "Glass Oceans";
        }
        public void ChangeFont(ToggleSwitch _obj, float size = 10f)
        {
            var getFont = _dirFont.Families.Length;
            if (getFont < 1)
                _dirFont.AddFontFile(Application.StartupPath + @"\" + nameFont);

            var font = new Font(_dirFont.Families[0], size, _fontStyle, GraphicsUnit.Point, 0);

            _obj.Font = font;
            _obj.Properties.Appearance.Font = font;

            _obj.LookAndFeel.UseDefaultLookAndFeel = false;

            _obj.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            //   _obj.LookAndFeel.SkinMaskColor = Color.FromArgb(179, 237, 171);
            //  _obj.LookAndFeel.SkinMaskColor = Color.FromArgb(197, 255, 190);
            //_obj.LookAndFeel.SkinName = "Office 2016 Black";

        }


        //private bool Lic1()
        //{
        //    var getNow = DateTime.Now;
        //    var dateLic = new DateTime(2022, 05, 30);
        //    return getNow >= dateLic;
        //}


        public System.Windows.Media.FontFamily ChangeFontWPF()
        {
            var ggg = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameFont);
           
            var fontFamily = new System.Windows.Media.FontFamily(new Uri(ggg), "./#Samim");
            var typeface = new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
          
            return fontFamily;
        }

      
    }
}
