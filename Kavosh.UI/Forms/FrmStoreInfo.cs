using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Kavosh.UI.Forms
{
    public partial class FrmStoreInfo : DevExpress.XtraEditors.XtraForm
    {
        private readonly StoreInfoService _storeInfoService;

        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private TextEdit txtStoreName, txtPhone, txtBankName, txtAccountHolderName, txtCardNumber, txtShabaNumber, txtTaxPercent;
        private MemoEdit txtAddress;

        public FrmStoreInfo(StoreInfoService storeInfoService)
        {
            InitializeComponent();
            _storeInfoService = storeInfoService;
            Shown += FrmStoreInfo_Shown;
        }

        private async void FrmStoreInfo_Shown(object sender, EventArgs e)
        {
            await SetFieldLayInput();
            await LoadDataAsync();
        }

        public async Task SetFieldLayInput()
        {
            layInput.RightToLeft = RightToLeft.Yes;
            pnlFunction.Controls.Add(layInput.ShowPanelOperation());
            layInput.AddButtonOperation();

            txtStoreName = ClsCollect.ModelTextEdit("نام فروشگاه", 150, "");
            txtPhone = ClsCollect.ModelTextEditNumber("تلفن", 15, "");

            txtBankName = ClsCollect.ModelTextEdit("نام بانک", 100, "");
            txtAccountHolderName = ClsCollect.ModelTextEdit("نام صاحب حساب", 100, "");
            txtCardNumber = ClsCollect.ModelTextEditNumber("شماره کارت", 16, "");
            txtShabaNumber = ClsCollect.ModelTextEditNumber("شماره شبا", 24, "");
            // توجه: اگه درصد مالیات نیاز به اعشار داره (مثلاً 9.5)، این کنترل رو با الگوی خودتون برای اعداد اعشاری هماهنگ کنید
            txtTaxPercent = ClsCollect.ModelTextEditNumber("درصد مالیات", 5, "");
            txtAddress = ClsCollect.ModelLayoutMemoEdit("آدرس", 300, "");

            var imgLogo = ClsCollect.ModelPicture2("لوگو");
            var imgMohr = ClsCollect.ModelPicture2("مهر|امضا");

            layInput.SetFieldColumnDataLayout(true, 2, [
                new() { Grp = 1, Ctrl = txtStoreName, },
                new() { Grp = 1, Ctrl = txtPhone, },
                new() { Grp = 1, Ctrl = txtBankName, },
                new() { Grp = 1, Ctrl = txtAccountHolderName, },
                new() { Grp = 1, Ctrl = txtCardNumber, },
                new() { Grp = 1, Ctrl = txtShabaNumber, },
                new() { Grp = 1, Ctrl = txtTaxPercent, },
                new() { Grp = 1, Ctrl = txtAddress, SizeType = SizeConstraintsType.Custom, AutoHeight = 80 },

                new() { Grp = 2, Ctrl = imgLogo, SizeType = SizeConstraintsType.Custom, AutoHeight = 150 },
                new() { Grp = 2, Ctrl = imgMohr, SizeType = SizeConstraintsType.Custom, AutoHeight = 150 },
            ]);

            layInput.BtnSaveClick += LayInput_BtnSaveClick;

        }

        private async Task LoadDataAsync()
        {
            var dto = await _storeInfoService.GetAsync();
            if (dto is null) return;

            txtStoreName.Text = dto.StoreName;
            txtPhone.Text = dto.Phone;
            txtAddress.Text = dto.Address;
            txtBankName.Text = dto.BankName;
            txtAccountHolderName.Text = dto.AccountHolderName;
            txtCardNumber.Text = dto.CardNumber;
            txtShabaNumber.Text = dto.ShabaNumber;
            txtTaxPercent.Text = dto.TaxPercent.ToString();

            //picLogo.Image = BytesToImage(dto.Logo);
            //picMohr.Image = BytesToImage(dto.Mohr);
        }

        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            try
            {
                var dto = new StoreInfoDto
                {
                    StoreName = txtStoreName.Text,
                    Phone = txtPhone.Text,
                    Address = txtAddress.Text,
                    BankName = txtBankName.Text,
                    AccountHolderName = txtAccountHolderName.Text,
                    CardNumber = txtCardNumber.Text,
                    ShabaNumber = txtShabaNumber.Text,
                    TaxPercent = float.TryParse(txtTaxPercent.Text, out var tax) ? tax : 0,
                    Logo = layInput.GetValue<Image>("لوگو").ImageToByte2(),
                    Mohr = layInput.GetValue<Image>("مهر|امضا").ImageToByte2(),
                    //Logo = ImageToBytes(picLogo.Image),
                    //Mohr = ImageToBytes(picMohr.Image)
                };

                await _storeInfoService.SaveAsync(dto);
                XtraMessageBox.Show("اطلاعات فروشگاه ذخیره شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private static void BrowseImage(PictureEdit target)
        {
            using var dlg = new OpenFileDialog { Filter = "تصویر|*.jpg;*.jpeg;*.png;*.bmp" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                target.Image = Image.FromFile(dlg.FileName);
            }
        }

        private static byte[]? ImageToBytes(Image? image)
        {
            if (image is null) return null;
            using var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        private static Image? BytesToImage(byte[]? bytes)
        {
            if (bytes is null || bytes.Length == 0) return null;
            using var ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        private void FrmStoreInfo_Load(object sender, EventArgs e) { }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }
    }
}