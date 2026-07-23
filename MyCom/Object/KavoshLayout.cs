using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using MyCom.Class;
using MyCom.Properties;
using Padding = System.Windows.Forms.Padding;

namespace MyCom.Object
{
    public partial class KavoshLayout : LayoutControl
    {
        readonly ClsFont _font = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        //readonly ClsFont _font = new ClsFont();
        public bool _disableAfterSave = true;

        [Category("Action")]
        public event EventHandler BtnNewClick;
        [Category("Action")]
        public event EventHandler BtnCancelClick;
        [Category("Action")]
        public event EventHandler BtnSaveClick;

        [Category("Action")]
        public event EventHandler z_FocusedRowChangedEventArgs;

        private bool Chk;
        [DefaultValue(false)]
        [Category("عملیات"), Description("! سازنده مهندس مجتبی یادآور")]

        public KavoshLayout()
        {
            InitializeComponent();

        }

        public KavoshLayout(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

        }
        // 
        public List<string> NNAS = new List<string>();
        public void SetFieldColumnDataLayout(bool clear, int cntColumn, List<ClsCollect.modelControlDataLayout> field, float fontSize = 12f)
        {
            var Layout = this;
            //this.Root

            var getNow = Layout.Dock;
            int getHeight = 1080, getWidth = 1920;

            if (getNow == DockStyle.Top || getNow == DockStyle.Bottom)
                getHeight = Layout.Height;

            if (getNow == DockStyle.Right || getNow == DockStyle.Left)
                getWidth = Layout.Width;

            Layout.Dock = DockStyle.None;

            Layout.Width = getWidth;
            Layout.Height = getHeight;

            BeginUpdate();

            if (RightToLeft == RightToLeft.Yes)
            {
                Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
            }
            else
            {
                Root.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                Root.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
            }

            OptionsFocus.EnableAutoTabOrder = false;

            BackColor = Color.Transparent;
            var font = _font.ChangeFont(fontSize);
            if (clear)
                Clear();
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
                var item = Root.AddGroup(grp);

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

                            AppearanceItemCaption = { Font = ctrl != null ? font : null },

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
                    RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);

                SimpleSeparator separator = new SimpleSeparator
                {
                    Name = "ss" + i,
                    Spacing = new DevExpress.XtraLayout.Utils.Padding(3),
                    AppearanceItemCaption = { BackColor = Color.Red }
                };
                Root.AddItem(separator);
                separator.Move(lstList[i - 1],
                    RightToLeft == RightToLeft.Yes
                        ? InsertType.Left
                        : InsertType.Right);
            }

            EndUpdate();
            BestFit();

            Layout.Dock = getNow;
            Root.Enabled = false;
        }
        public class modelRelationLayoutAndDataTable
        {
            public string NameFieldLayout { get; set; }
            public string NameFieldDataTable { get; set; }
        }

        private List<modelRelationLayoutAndDataTable> _lstNameDataTable = new List<modelRelationLayoutAndDataTable>();
        private DataTable _dtRalation = new DataTable();

        public void AddRelationTagDataTable(string nameFieldLayout, string nameFieldDataTable)
        {
            _lstNameDataTable.Add(new modelRelationLayoutAndDataTable
            {
                NameFieldLayout = nameFieldLayout,
                NameFieldDataTable = nameFieldDataTable
            });
        }



        #region Creat Panel Function

        static Padding _padding = new Padding(0);
        GroupControl _pnlOperation = new GroupControl
        {
            Dock = DockStyle.Fill,
            Height = 30,
            Padding = _padding,
            ShowCaption = false,
            LookAndFeel = { UseDefaultLookAndFeel = false, SkinName = "Money Twins" }
        };

        public SimpleButton _btnNew = new SimpleButton { Width = 72, };
        public SimpleButton _btnCancel = new SimpleButton { Width = 72, Visible = false, };
        public SimpleButton _btnSave = new SimpleButton { Width = 72, Visible = false, };
        //   private Splitter _spl1 = new Splitter {Dock = DockStyle.Right,Width = 4};
        public void AddButtonOperation()
        {
            _btnNew.RightToLeft = RightToLeft;
            _btnCancel.RightToLeft = RightToLeft;
            _btnSave.RightToLeft = RightToLeft;

            if (RightToLeft == RightToLeft.Yes)
            {
                // _btnNew.ImageOptions.ImageToTextAlignment = ImageAlignToText.LeftCenter;

                _btnNew.Dock = DockStyle.Right;
                _btnCancel.Dock = DockStyle.Left;
                _btnSave.Dock = DockStyle.Right;

                _btnNew.Text = @"جدید";
                _btnCancel.Text = @"انصراف";
                _btnSave.Text = @"ذخیره";
            }
            else
            {
                //   _btnNew.ImageOptions.ImageToTextAlignment = ImageAlignToText.RightCenter;
                //  _btnNew.ImageOptions.ImageToTextIndent = 15;

                _btnNew.Dock = DockStyle.Left;
                _btnCancel.Dock = DockStyle.Right;
                _btnSave.Dock = DockStyle.Left;

                _btnNew.Text = @"New";
                _btnCancel.Text = @"Cancel";
                _btnSave.Text = @"Save";
            }

            _pnlOperation.Controls.AddRange(new Control[] { _btnNew, _btnCancel, _btnSave });
            SetIconBtn();
            _btnNew.Click += (s, e) =>
            {
                this.BeginUpdate();
                CallNew();

                BtnNewClick?.Invoke(this, e);

                this.EndUpdate();
            };
            _btnCancel.Click += (s, e) =>
            {
                BtnCancelClick?.Invoke(this, e);
                CallCancel();
            };
            _btnSave.Click += (s, e) =>
            {
                //bubble the event up to the parent
                if (!CheckNullField())
                    return;

                BtnSaveClick?.Invoke(this, e);
                CallSave();
            };
        }
        public bool CheckNullField(bool showErrorMsg = true)
        {
            //var g123 = Convert.ToInt32("aaaa");
            foreach (var grp in Root.Items)
            {
                if (grp is LayoutControlGroup _lcg)
                {
                    var controlGroup = _lcg.Items;
                    foreach (var item in controlGroup)
                    {
                        try
                        {
                            var layoutControlItem = ((LayoutControlItem)item);
                            var g = layoutControlItem.Name.Replace("LCI_", "");
                            //   var gForGroup = ((LayoutControlItem)item).Text.Replace("LCI_GRP_", "");
                            var gText = layoutControlItem.Text.Replace(":", "").Replace("*", "");

                            if (g == "GRP_GrpControl")
                            {
                                var getGrp = layoutControlItem.Control.Controls[0];

                                foreach (Control c in getGrp.Controls.OfType<Control>())
                                {
                                    if (c is LabelControl)
                                    {
                                        g = c.Text.Replace(":", "").Replace("*", "");
                                        var getValue2 = this.GetValue(g, true, "").ToString();
                                        if (c.Text.Contains("*") && string.IsNullOrEmpty(getValue2))
                                        {
                                            if (showErrorMsg)
                                            {
                                                Frm_MSG frmMsg = new Frm_MSG("فیلد: (" + g + ") " + "اجباری می باشد.", "ایران کاوش", 1, false, Color.Red);
                                                frmMsg.ShowDialog();
                                                c.Focus();
                                                c.Select();
                                            }

                                            return false;
                                        }
                                    }
                                }
                            }

                            var getValue = this.GetValue(g, true, "").ToString();
                            if (layoutControlItem.Text.Contains("*") && string.IsNullOrEmpty(getValue))
                            {
                                if (showErrorMsg)
                                {
                                    Frm_MSG frmMsg = new Frm_MSG("فیلد: (" + gText + ") " + "اجباری می باشد.", "ایران کاوش", 1, false, Color.Red);
                                    frmMsg.ShowDialog();
                                    layoutControlItem.Control.Focus();
                                    layoutControlItem.Control.Select();
                                }
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            // ignored
                        }
                    }


                    // Multi Column

                    //foreach (var item in controlGroup)
                    //{
                    //    try
                    //    {
                    //        var g = ((GroupControl)item).Name.Replace("LCI_", "");
                    //        var gText = ((GroupControl)item).Text.Replace(":", "").Replace("*", "");

                    //        var getValue = this.GetValue(g, true, "").ToString();
                    //        if (((GroupControl)item).Text.Contains("*") && string.IsNullOrEmpty(getValue))
                    //        {
                    //            if (showErrorMsg)
                    //            {
                    //                Frm_MSG frmMsg = new Frm_MSG("فیلد: (" + gText + ") " + "اجباری می باشد.", "ایران کاوش", 1, false, Color.Red);
                    //                frmMsg.ShowDialog();
                    //            }
                    //            return false;
                    //        }
                    //    }                    //    catch (Exception)
                    //    {
                    //        // ignored
                    //    }
                    //}
                }

            }

            return true;
        }
        public void CallSave()
        {
            try
            {
                _btnNew.Visible = _disableAfterSave;
                _btnCancel.Visible = _btnSave.Visible = !_disableAfterSave;
                //btnDelete.Visible = (_disableAfterSave);
                //pnlDown.Visible = _disableAfterSave && _usedDelete;
                //btnDelete.SendToBack();
                //btnPrint.Visible = _disableAfterSave && _usedPrint;
                //  if (_disableAfterSave)
                //    _btnCancel.BringToFront();
                Root.Enabled = !_disableAfterSave;
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void CallCancel()
        {
            _btnNew.Visible = true;
            _btnCancel.Visible = _btnSave.Visible = false;
            //btnDelete.Visible = false;
            //pnlDown.Visible = false;
            //btnPrint.Visible = false;
            Root.Enabled = false;
        }
        public void CallNew()
        {
            _btnNew.Visible = false;
            _btnCancel.Visible = _btnSave.Visible = true;
            // btnDelete.Visible = false;
            //  pnlDown.Visible = false;
            //  btnPrint.Visible = false;
            _btnCancel.BringToFront();
            Root.Enabled = true;
            // baseLayout.Enabled  = true;
            // group.Enabled  = true;

            this.SetNull(NNAS);
        }

        public GroupControl ShowPanelOperation()
        {
            _font.ChangeFont(_btnNew, 12);
            _font.ChangeFont(_btnCancel, 12);
            _font.ChangeFont(_btnSave, 12);
            return _pnlOperation;
        }

        public void SetIconBtn()
        {
            try
            {
                _btnCancel.ImageOptions.SvgImage = Resources.Cancel2;
                _btnCancel.ImageOptions.SvgImageSize = new Size(19, 19);

                //btnPrint.ImageOptions.SvgImage = Resources.Print2;
                //btnPrint.ImageOptions.SvgImageSize = new Size(22, 22);

                _btnSave.ImageOptions.SvgImage = Resources.Save3;
                _btnSave.ImageOptions.SvgImageSize = new Size(17, 17);

                _btnNew.ImageOptions.SvgImage = Resources.New2;
                _btnNew.ImageOptions.SvgImageSize = new Size(19, 19);
                //btnDelete.ImageOptions.SvgImage = Resources.delete2;
                //btnDelete.ImageOptions.SvgImageSize = new Size(22, 22);
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Work Value DataLayout
        public string FindControl(string name)
        {
            // var getCtrl2 = Controls.ToArray<Control>();
            var getCtrl = Controls.Find(name, true);
            return getCtrl[0].Name;
        }
        public Control FindControl2(string name)
        {
            // var getCtrl2 = Controls.ToArray<Control>();
            var getCtrl = Controls.Find(name, true);
            return getCtrl[0];
        }
        public IEnumerable<Control> GetAllControl(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(GetAllControl).Concat(controls);
        }
        public void SetValue(string name, object value)
        {
            var getCtrl = Controls.Find(name, true);
            getCtrl[0].Text = value.ToString();
        }

        public void SetValue(string name, object value, object defualtValue = null)
        {
            var getCtrl = Controls.Find(name, true);
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
        public void SetValueType(string name, object value)
        {
            // var getCtrl = (dynamic)Controls.Find(name, true)[0];

            var getCtrl1 = Controls.Find(name, true).ToList();

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

        //public Control GetGroup( string name)
        //{
        //  //  object getValue = null;
        //   // var getCtrl = (dynamic)Controls.Find(name, true)[0];
        //    var getCtrl = Controls.Find(name, true)[0];
        //    var getParent = getCtrl;
        //    return getParent;
        //    //            LayoutControlGroup
        //}
        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        public object GetValue(string name, bool value = true, object defualtValue = null)
        {
            object getValue = null;
            var getCtrl1 = Controls.Find(name, true).ToList();

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

            if (getValue == null || getValue == "")
                if (defualtValue != null)
                    return defualtValue;

            return getValue;
        }
        public T GetValue<T>(string name, bool value = true, object defualtValue = null)
        {

            object getValue = null;
            var getCtrl1 = Controls.Find(name, true).ToList();

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
                    if (control is PictureEdit pictureEdit1)
                    {
                        getValue = pictureEdit1.Image;

                        //getValue = pictureEdit1.Text == pictureEdit1.Properties.NullText
                        //    ? ""
                        //    : pictureEdit1.Text.Trim();
                    }
                }
            }
            if (getValue == null || ReferenceEquals(getValue, ""))
            {
                if (defualtValue != null)
                {
                    return (T)Convert.ChangeType(defualtValue, typeof(T));
                }

                else
                    return default;
            }

            #region Image

            var targetType = typeof(T);

            // برای انواع تصویر، رفتار متفاوت داشته باش
            if (targetType == typeof(Bitmap) || targetType == typeof(Image) || targetType.IsSubclassOf(typeof(Image)))
            {

                if (getValue is Image imageValue)
                {
                    // اگر نوع هدف دقیقاً Bitmap است و imageValue از نوع Bitmap نیست
                    if (targetType == typeof(Bitmap) && !(imageValue is Bitmap))
                    {
                        // تبدیل به Bitmap
                        return (T)(object)new Bitmap(imageValue);
                    }

                    // در غیر این صورت، مستقیماً برگردان
                    return (T)(object)imageValue;
                }

            }

            #endregion

            try
            {
                // Start
                var getType = typeof(T).FullName;
                if (getType == typeof(Guid).FullName)
                {
                    var getValue2 = Guid.Parse(getValue.ToString());
                    return (T)Convert.ChangeType(getValue2, typeof(T));
                }
                if (getType == typeof(long).FullName || getType == typeof(int).FullName || getType == typeof(short).FullName)
                {
                    var getValue2 = Convert.ToDecimal(getValue.ToString());
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
        public void SetNull(List<string> NNAS = null)
        {
            BeginUpdate();

            foreach (Control control in Controls)
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

            EndUpdate();
        }
        private void ctrl(Control c)
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

        public void AddNullTextToPersianName()
        {
            BeginUpdate();

            foreach (Control control in Controls)
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

            EndUpdate();
        }
        private void AddNullTextToPersianName3(Control getCtrl)
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
        public void AddPersianName(List<modelNamePersian> lstPersians)
        {
            foreach (modelNamePersian l in lstPersians)
            {
                ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = l.PersianName, Appearance = { Font = _font.ChangeFont(12), ForeColor = Color.CornflowerBlue } };
                SuperToolTip superToolTip = new SuperToolTip();
                superToolTip.Items.Add(toolTipTitleItem);

                var getCtrl1 = Controls.Find(l.ControlLayoutName, true).ToList();

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
        public void AddPersianName(string name, string namePersian)
        {
            ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem { Text = namePersian };
            SuperToolTip superToolTip = new SuperToolTip();
            superToolTip.Items.Add(toolTipTitleItem);

            var getCtrl1 = Controls.Find(name, true).ToList();

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

        public class modelCheckedListBoxControl : ClsCollect.modelDataTable
        {
            public CheckState CheckState { get; set; }
        }
        public List<modelCheckedListBoxControl> GetValue(CheckedListBoxControl item)
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
        public void HiddenColumn(GridLookUpEdit dgv, string nameColumn)
        {
            dgv.Properties.PopupView.Columns[nameColumn].Visible = false;
        }
        public void HiddenColumn(string nameField)
        {
            var get = Items.FindByName("LCI_" + nameField);
            if (get == null)
            {
                foreach (var item in Items)
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
        public void ShowColumn(string nameField)
        {
            var get = Items.FindByName("LCI_" + nameField);
            if (get == null)
            {
                foreach (var item in Items)
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
        public void ReadOnlyColumn(string nameField, bool readOnly = true)
        {
            BeginUpdate();

            foreach (Control obj in Controls)
            {
                if (obj.Name.ToLower().Contains(nameField.ToLower()))
                {
                    if (obj is ComboBoxEdit boxEdit)
                    {
                        boxEdit.Properties.ReadOnly = readOnly;
                        EndUpdate();
                        return;
                    }
                    if (obj is GridLookUpEdit lookUpEdit)
                    {
                        lookUpEdit.Properties.ReadOnly = readOnly;
                        EndUpdate();
                        return;
                    }
                    if (obj is pnlDateOpen pnlDateOpen)
                    {
                        pnlDateOpen.txtShowCalen.Properties.ReadOnly = readOnly;
                        EndUpdate();
                        return;
                    }
                    if (obj is MemoEdit memoEdit)
                    {
                        memoEdit.Properties.ReadOnly = readOnly;
                        EndUpdate();
                        return;
                    }
                    if (obj is TextEdit textEdit)
                    {
                        textEdit.Properties.ReadOnly = readOnly;
                        EndUpdate();
                        return;
                    }
                }
            }

            EndUpdate();
        }
        public GridLookUpEdit ConvertGroupToGrid(GroupControl ctrl) => ctrl.Controls[0] as GridLookUpEdit;

        //public GridLookUpEdit ConvertGroupToGrid(this GroupControl ctrl)
        //{
        //    return ctrl.Controls[0] as GridLookUpEdit;
        //}   

        #endregion



    }
}
