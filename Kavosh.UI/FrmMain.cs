using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kavosh.UI.Forms;
using MyCom.Class;

namespace Kavosh.UI
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ClsFont _clsFont = new ClsFont(false);
        private ClsFont _clsFontBold = new ClsFont(true);
        public FrmMain()
        {
            InitializeComponent();
        
        }

        public async Task SetStyle()
        {
            _clsFontBold.ChangeFont(ribbon);
            ribbon.CustomizeReborn();
        }


        private async void FrmMain_Load(object sender, EventArgs e)
        {

            await SetStyle();
        }

      
        private void barBtnProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmProduct().OverShowWait<FrmProduct>(this);
        }

     
    }
}