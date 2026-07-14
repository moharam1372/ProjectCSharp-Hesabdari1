using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraVerticalGrid.Events;
using MyCom.Class;
using MyCom.Properties;

namespace MyCom.Object
{
    public partial class dataLayout : XtraUserControl
    {
        readonly ClsFont _font = new ClsFont();
        public bool _disableAfterSave = true;
        public dataLayout()
        {
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            try
            {
                //baseLayout.BackgroundImage = Image.FromFile(@"C:\Users\mojtaba.yadavar\Desktop\New folder\aPJogV2.jpg");
                //baseLayout.BackgroundImageLayout = ImageLayout.Stretch;
                //group.ContentImageOptions.Image=Image.FromFile(@"C:\Users\mojtaba.yadavar\Desktop\New folder\aPJogV2.jpg");
                //group.BackgroundImageLayout = ImageLayout.Stretch;

                // baseLayout.Appearance.Control.Image=Image.FromFile(@"C:\Users\mojtaba.yadavar\Desktop\New folder\aPJogV2.jpg");
                _font.ChangeFont(btnNew);
                _font.ChangeFont(btnCancel);
                _font.ChangeFont(btnSave);
                _font.ChangeFont(btnPrint);
                _font.ChangeFont(btnDelete);
                _font.ChangeFont(lblStatus);
            }
            catch (Exception e)
            {
                // ignored
            }

            //   data.CellValueChanged += Data_CellValueChanged;
            //   data.FocusedRowChanged += Data_FocusedRowChanged;
        }
        [Category("Action")]
        public event EventHandler z_CellValueChangedEventArgs;

        [Category("Action")]
        public event EventHandler z_FocusedRowChangedEventArgs;

        private bool Chk;
        [DefaultValue(false)]
        [Category("عملیات"), Description("! سازنده مهندس مجتبی یادآور")]
        public bool Z_PanelShow
        {
            get { return Chk; }
            set { Chk = value; }
        }

        private bool _usedDelete = true;
        [DefaultValue(true)]
        [Category("عملیات"), Description("! سازنده مهندس مجتبی یادآور")]
        public bool Z_UsedDelete
        {
            get { return _usedDelete; }
            set { _usedDelete = value; }
        }

        private bool _usedPrint = true;
        [DefaultValue(true)]
        [Category("عملیات"), Description("! سازنده مهندس مجتبی یادآور")]
        public bool Z_UsedPrint
        {
            get { return _usedPrint; }
            set { _usedPrint = value; }
        }

        public void Data_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            z_CellValueChangedEventArgs?.Invoke(this, e);
        }
        public void Data_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            z_FocusedRowChangedEventArgs?.Invoke(this, e);
        }

        [Category("Action")]

        public event EventHandler BtnNewClick;
        [Category("Action")]

        public event EventHandler BtnSaveClick;
        [Category("Action")]

        public event EventHandler BtnCancelClick;
        [Category("Action")]

        public event EventHandler BtnPrintClick;
        [Category("Action")]

        public event EventHandler BtnDeleteClick;
        [Category("Action")]

        public void btnNew_Click2(object sender, EventArgs e)
        {
            baseLayout.BeginUpdate();
            CallNew();

            BtnNewClick?.Invoke(this, e);

            baseLayout.EndUpdate();
        }

        public void CallNew()
        {
            btnNew.Visible = false;
            btnCancel.Visible = btnSave.Visible = true;
            btnDelete.Visible = false;
            pnlDown.Visible = false;
            btnPrint.Visible = false;
            btnCancel.BringToFront();
            baseLayout.Root.Enabled = true;
            // baseLayout.Enabled  = true;
            // group.Enabled  = true;

            this.SetNull(NNAS);
        }

        protected void btnSave_Click2(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (!CheckNullField())
                return;

            BtnSaveClick?.Invoke(this, e);
            CallSave();
        }

        public List<string> NNAS = new List<string>();
        public bool CheckNullField(bool showErrorMsg = true)
        {
            //var g123 = Convert.ToInt32("aaaa");
            foreach (var grp in group.Items)
            {
                if (grp is LayoutControlGroup _lcg)
                {
                    var controlGroup = _lcg.Items;
                    foreach (var item in controlGroup)
                    {
                        try
                        {
                            var g = ((LayoutControlItem)item).Name.Replace("LCI_", "");

                            var getTxt = ((LayoutControlItem)item).Control.Controls.OfType<TextEdit>().ToList();


                            //   var gForGroup = ((LayoutControlItem)item).Text.Replace("LCI_GRP_", "");
                            var gText = ((LayoutControlItem)item).Text.Replace(":", "").Replace("*", "");

                            //if (getTxt.Count > 0)
                            //{
                            //    var getlbl = getTxt[0].Text;
                            //    var getValue2 = this.GetValue(getTxt[0].Name, true, "").ToString();
                            //}

                            if (g == "GRP_GrpControl")
                            {
                                var getGrp = ((LayoutControlItem)item).Control.Controls[0];

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
                                                Frm_MSG frmMsg = new Frm_MSG("فیلد: (" + g + ") " + "اجباری می باشد.", "مدیریت گیم نت اکسیژن", 1, false, Color.Red);
                                                frmMsg.ShowDialog();
                                                var getObject = c.Controls;
                                            }

                                            return false;
                                        }
                                    }
                                }
                            }

                            var getValue = this.GetValue(g, true, "").ToString();
                            LayoutControlItem controlItem = ((LayoutControlItem)item);
                            if (controlItem.Text.Contains("*") && string.IsNullOrEmpty(getValue))
                            {
                                if (showErrorMsg)
                                {
                                    Frm_MSG frmMsg = new Frm_MSG("فیلد: (" + gText + ") " + "اجباری می باشد.", "مدیریت گیم نت اکسیژن", 1, false, Color.Red);
                                    frmMsg.ShowDialog();

                                    var getC2 = controlItem.Control;
                                    getC2.Select();
                                    getC2.Focus();
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
                    //                Frm_MSG frmMsg = new Frm_MSG("فیلد: (" + gText + ") " + "اجباری می باشد.", "گروه صنعتی لینا", 1, false, Color.Red);
                    //                frmMsg.ShowDialog();
                    //            }
                    //            return false;
                    //        }
                    //    }                        catch (Exception)
                    //    {
                    //        // ignored
                    //    }
                    //}
                }

            }

            return true;
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

        public List<modelRelationLayoutAndDataTable> GetRelationTagDataTable()
        {
            return _lstNameDataTable;
        }
        public void RelationTagDataTable(ref DataTable table)
        {
            _dtRalation = table;
        }
        public void ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            //int cntSalem = 0;
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                //var info = prop.PropertyType.BaseType;
                //if (info.Name!="Nullable")
                //try
                //{
                // row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);


                // table.Columns.Add(prop.Name, prop.PropertyType);
                //  cntSalem++;
                //}
                //catch (Exception e)
                //{
                //   // var getItem = data[i];
                //   // data.Remove(getItem);
                //}

            }
            // object[] values = new object[cntSalem];
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    // row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    //  if (values[i] != null)
                    {
                        values[i] = props[i].GetValue(item) ?? DBNull.Value;

                    }

                }
                table.Rows.Add(values);

            }

            RelationTagDataTable(ref table);
            //   return table;
        }

        public void RelationTagDataTable<EF>(ref List<EF> table)
        {
            ToDataTable(table);
        }

        public DataRow GetDataForEdit(string findField, object findValue)
        {
            // modelRelationLayoutAndDataTable table = new modelRelationLayoutAndDataTable();
            // this.Invoke(new Action(() => progEdit.Visible = true ));
            // ClsCollect.PleaswaitStartWithoutCount(null);
            CallNew();
            DataRow getRow = null;
            if (_dtRalation.Rows.Count > 0)
                foreach (var s in _lstNameDataTable)
                {
                    if (s.NameFieldDataTable != null && _dtRalation.Columns.Contains(s.NameFieldDataTable))
                    {
                        var getTypeColumn = _dtRalation.Columns[s.NameFieldDataTable].DataType;
                        var _value = nameof(Guid) == getTypeColumn.Name
                            ? Guid.Parse(findValue.ToString())
                            : Convert.ChangeType(findValue.ToString(), getTypeColumn);

                        getRow = _dtRalation.AsEnumerable().Where(f =>
                        {
                            var _find = nameof(Guid) == getTypeColumn.Name
                                ? Guid.Parse(f[findField].ToString())
                                : Convert.ChangeType(f[findField].ToString(), getTypeColumn);

                            if (_find.ToString() == _value.ToString())
                                return true;
                            return false;

                        }).First();

                        break;
                    }
                }

            foreach (modelRelationLayoutAndDataTable i in _lstNameDataTable)
            {
                if (getRow != null && i.NameFieldDataTable != null)
                {
                    var value = getRow[i.NameFieldDataTable];
                    this.SetValueType(i.NameFieldLayout, value);
                }
            }

            // ClsCollect.PleaswaitEnd();
            return getRow;
        }
        public void SetImageBackground(string address)
        {
            byte[] buff = System.IO.File.ReadAllBytes(address);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buff);
            group.ContentImageOptions.Image = Image.FromStream(ms);
            // return Image.FromStream(ms);
        }
        public void SetImageBackground(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            image.Dispose();
            group.ContentImageOptions.Image = bitmap;
            // return Image.FromStream(ms);
        }
        public void CallSave()
        {
            try
            {
                btnNew.Visible = _disableAfterSave;
                btnCancel.Visible = btnSave.Visible = !_disableAfterSave;
                btnDelete.Visible = (_disableAfterSave);
                pnlDown.Visible = _disableAfterSave && _usedDelete;
                btnDelete.SendToBack();
                btnPrint.Visible = _disableAfterSave && _usedPrint;
                if (_disableAfterSave)
                    btnCancel.BringToFront();
                baseLayout.Root.Enabled = !_disableAfterSave;
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void btnCancel_Click2(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            BtnCancelClick?.Invoke(this, e);
            CallCancel();
        }
        public void CallCancel()
        {
            btnNew.Visible = true;
            btnCancel.Visible = btnSave.Visible = false;
            btnDelete.Visible = false;
            pnlDown.Visible = false;
            btnPrint.Visible = false;
            baseLayout.Root.Enabled = false;
        }
        public void ThisEnable()
        {
            baseLayout.Root.Enabled = true;
        }
        protected void btnPrint_Click2(object sender, EventArgs e)
        {
            // bubble the event up to the parent
            BtnPrintClick?.Invoke(this, e);
            // btnNew.Visible = true;
            // btnCancel.Visible = btnSave.Visible = false;
            // btnDelete.Visible = false;
        }

        protected void btnDelete_Click2(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            Frm_MSG _frmMsg = new Frm_MSG("حذف شود ؟", "مدیریت گیم نت اکسیژن", 1, true);

            _frmMsg.ShowDialog();
            var getQues = _frmMsg.Check_Msg;
            if (getQues)
            {
                btnNew.Visible = true;
                btnCancel.Visible = btnSave.Visible = false;
                btnDelete.Visible = false;
                pnlDown.Visible = false;
                btnPrint.Visible = false;
                baseLayout.Root.Enabled = false;
                this.SetNull(NNAS);
                BtnDeleteClick?.Invoke(this, e);
            }
        }

        private void vGrid_Load(object sender, EventArgs e)
        {
            LookAndFeel.UseDefaultLookAndFeel = false;
            LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            //  LookAndFeel.SkinName = "WIX";
            //  LookAndFeel.SkinName = "Office 2010 Silver";
            baseLayout.Root.Enabled = false;
            //   MessageBox.Show(Chk.ToString());

            pnlOperation.Visible = Chk;

            //try
            //{
            //    btnCancel.ImageOptions.SvgImage = Resources.Cancel2;
            //    btnPrint.ImageOptions.SvgImage = Resources.Print2;
            //    btnSave.ImageOptions.SvgImage = Resources.Save2;
            //    btnNew.ImageOptions.SvgImage = Resources.New2;
            //    btnDelete.ImageOptions.SvgImage = Resources.delete2;
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}

            //  try
            {
                btnCancel.ImageOptions.SvgImage = Resources.Cancel2;
                btnCancel.ImageOptions.SvgImageSize = new Size(22, 22);
                btnPrint.ImageOptions.SvgImage = Resources.Print2;
                btnPrint.ImageOptions.SvgImageSize = new Size(22, 22);
                btnSave.ImageOptions.SvgImage = Resources.Save3;
                btnSave.ImageOptions.SvgImageSize = new Size(21, 21);
                btnNew.ImageOptions.SvgImage = Resources.New2;
                btnNew.ImageOptions.SvgImageSize = new Size(20, 20);
                btnDelete.ImageOptions.SvgImage = Resources.delete2;
                btnDelete.ImageOptions.SvgImageSize = new Size(22, 22);
            }
            //   catch (Exception)
            {
                // ignored
            }
        }

        private void pnlOperation_Paint(object sender, PaintEventArgs e)
        {

        }

        private void baseLayout_KeyDown(object sender, KeyEventArgs e)
        {
            //  MessageBox.Show(e.KeyCode.ToString());
        }

        private void baseLayout_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  MessageBox.Show(e.KeyChar.ToString());
        }

        private void pnlCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }


}
