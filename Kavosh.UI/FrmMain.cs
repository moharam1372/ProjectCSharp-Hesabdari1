using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Kavosh.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using MyCom.Class;
using MyCom.Form_Portable;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            //ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            await SetStyle();
        }


        private void barBtnProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            //new FrmProduct().OverShowWait<FrmProduct>(this);

            var frm = Program.CreateScopedForm<FrmProduct>();
            frm.OverShowWait<FrmProduct>(this);
        }

        private void barPerson_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmPerson>();
            frm.OverShowWait<FrmPerson>(this);
        }

        private void barFactor_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var frm = Program.CreateScopedForm<FrmFactor>();
            //frm.OverShowWait<FrmFactor>(this);

            var frm = Program.CreateScopedForm<FrmFactorList>();   // 👈 قبلاً FrmFactor بود
            frm.OverShowWait<FrmFactorList>(this);
        }

        private void barBtnAccounting_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmDefinitiveAccount>();
            // PersonIdToShow رو ست نمی‌کنیم — کاربر خودش از LookUpEdit داخل فرم انتخاب می‌کنه
            frm.OverShowWait<FrmDefinitiveAccount>(this);
        }

        private void barBtnDebtorsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = Program.CreateScopedForm<FrmDebtorsList>();
            frm.OverShowWait<FrmDebtorsList>(this);
        }

  
    }
}