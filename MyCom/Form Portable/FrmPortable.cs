using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DocumentFormat.OpenXml.Office2010.Excel;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCom.Form_Portable
{
    public partial class FrmPortable : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _titleForm;
        private readonly List<ModelPortableData> _modelData;
        private readonly ModelAction _action;
        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        public class ModelPortableData
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public class ModelAction
        {

            public Action<ModelPortableData> SaveData { get; set; }
            public Action<Guid> DeleteData { get; set; }
        }

        public FrmPortable(string titleForm, List<ModelPortableData> modelData, ModelAction action)
        {
            _titleForm = titleForm;
            _modelData = modelData;
            _action = action;
            InitializeComponent();
            this.Shown += FrmPortable_Shown;
        }

        private async void FrmPortable_Shown(object sender, EventArgs e)
        {
            await dgvPortable.SetStyle();
            _clsFontBold.ChangeFont(dgvPortable);
            await Task.WhenAll(

                 SetFieldDgvPortable()
            );
            this.Text = _titleForm;
            //await ();
        }


        private DataTable _dtPortable;

        public async Task SetFieldDgvPortable()
        {
            if (dgvPortable.ColumnCount() == 0)
            {
                _dtPortable = dgvPortable.GridStructure([
                    new() {Name = "Id",Type = typeof(Guid)},
                    new() {Name = "حذف",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.delete},
                    new() {Name = "نام",Type = typeof(string)},
                ], true, false, false);
                dgvPortable.ActiveScrollGrid();
                dgvPortable.HiddenColumn("Id");
                dgvPortable.MaxMinWidth("حذف", 45, 45);
                dgvPortable.MaxMinWidth("نام", 240, 240);
                dgvPortable.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);
                #region Event

                dgvPortable.GetViewBase.ValidatingEditor += (s1, e1) =>
                {
                    var getId = dgvPortable.GetValue<Guid?>("Id");
                    var getTitle = e1.Value;
                    if (!(getTitle is null))
                    {
                        var getNewId = Guid.NewGuid();
                        if (getId == null)
                        {
                            dgvPortable.SetValue("Id", getNewId);
                        }

                        _action.SaveData(new ModelPortableData
                        {
                            Id = getId ?? getNewId,
                            Title = getTitle.ToString()
                        });


                    }

                    //  Close();
                };

                dgvPortable.AddEventRowCellClick<Guid>(id =>
                {
                    dgvPortable.DeleteRow(true, async () =>
                    {
                        _action.DeleteData(id);
                        //await SetFieldDgvPortable();
                    });
                }, "Id", "حذف");

                #endregion
            }

            #region LoadData

            _dtPortable.Rows.Clear();
            foreach (var i in _modelData)
            {
                _dtPortable.Rows.Add(i.Id, "حذف", i.Title);
            }
            dgvPortable.SetFieldSizeColumn();

            #endregion
        }

        private void FrmPortable_Load(object sender, EventArgs e)
        {

        }
    }
}