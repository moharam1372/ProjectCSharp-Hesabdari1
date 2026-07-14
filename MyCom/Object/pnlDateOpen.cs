using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using MyCom.Class;

namespace MyCom.Object
{
    public partial class pnlDateOpen : XtraUserControl
    {
        private ClsFont _clsFontBold = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, false);
        //  public string _selDate;
        public pnlDateOpen()
        {
            InitializeComponent();
            Height = 30;
            btnShowCalen.Height = 30;
            btnShowCalen.Width = 33;
        }
        [Category("Action")]
        public event EventHandler z_btnShowCalen;

        

        private void btnShowCalen_Click(object sender, EventArgs e)
        {
           
            frmCalender calender = new frmCalender(txtShowCalen.Text);
            calender.ShowDialog();
            txtShowCalen.Text = calender.SelDate;
            Height = 30;
            btnShowCalen.Height = 30;
            btnShowCalen.Width = 33;
            z_btnShowCalen?.Invoke(this, e);
            //  _selDate = calender.SelDate;
        }

        private void pnlDateOpen_Resize(object sender, EventArgs e)
        {
            Height = 30;
            btnShowCalen.Height = 30;
            btnShowCalen.Width = 33;
        }

        private void pnlDateOpen_Load(object sender, EventArgs e)
        {
            _clsFontBold.ChangeFont(txtShowCalen, 15);
            Height = 30;
            btnShowCalen.Height = 30;
            btnShowCalen.Width = 33;
        }

        private void txtShowCalen_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
