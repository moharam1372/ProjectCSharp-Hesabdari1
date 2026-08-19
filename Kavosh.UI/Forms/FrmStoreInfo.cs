using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Kavosh.UI.Forms
{
    public partial class FrmStoreInfo : DevExpress.XtraEditors.XtraForm
    {
        private readonly StoreInfoService _storeInfoService;
        private readonly LoginUserService _loginUserService;
        private readonly AppSettingService _appSettingService;   // 👈 جدید


        private ClsFont _clsFont = new(false);
        private ClsFont _clsFontBold = new(true);

        private TextEdit txtStoreName, txtPhone, txtBankName, txtAccountHolderName, txtCardNumber, txtShabaNumber, txtTaxPercent;
        private MemoEdit txtAddress;

        public FrmStoreInfo(StoreInfoService storeInfoService, LoginUserService loginUserService, AppSettingService appSettingService)
        {
            InitializeComponent();
            _storeInfoService = storeInfoService;
            _loginUserService = loginUserService;
            _appSettingService = appSettingService;
            Shown += FrmStoreInfo_Shown;
            layInput.BtnSaveClick += LayInput_BtnSaveClick;
            kavoshLayout1.BtnSaveClick += KavoshLayout1_BtnSaveClick;

            pnlFunction.Controls.Add(layInput.ShowPanelOperation());
            layInput.AddButtonOperation();


            pnlPass.Controls.Add(kavoshLayout1.ShowPanelOperation());
            kavoshLayout1.AddButtonOperation();
        }

        private async void FrmStoreInfo_Shown(object sender, EventArgs e)
        {
            _clsFontBold.ChangeFont(tabPane1);
            await SetFieldLayInput();


            tabPane1.SelectedPageChanged += async (s1, e1) =>
            {
                if (e1.Page == tabNavigationPage2)
                {
                    await Task.Delay(500);
                    await SetFieldLayPass();
                }
                else if (e1.Page == tabNavigationPage1)
                {
                    await Task.Delay(500);
                    await SetFieldLayInput();
                }
                else if (e1.Page == tab3)
                {
                    await SetFieldLaySettingApp();
                }
            };


        }



        #region Setting App
        KavoshLayout _layInputSettings;
        public async Task SetFieldLaySettingApp()
        {
            tab3.WaitDownPage(async () =>
            {
                var chkBox = ClsCollect.ModelCheckEdit("موجودی منفی", CheckState.Unchecked, true);

          
                _layInputSettings = new KavoshLayout { RightToLeft = RightToLeft.Yes, Dock = DockStyle.Fill  };
                _layInputSettings._btnCancel.Enabled = false;
                _layInputSettings.SetFieldColumnDataLayout(true, 1,
                    [new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = chkBox, AllowNull = false }]);

                pnlSettingApp.Controls.Add(_layInputSettings.ShowPanelOperation());
                _layInputSettings.AddButtonOperation();

                pnlLayApp.Controls.Add(_layInputSettings);
                await Task.Delay(400);
                 _layInputSettings.CallNew();
                 await LoadDataSettingAsync();

                _layInputSettings.BtnSaveClick += async (s1, e1) =>
                {
                    _layInputSettings._disableAfterSave = true;
                    try
                    {
                        var dto = new AppSettingDto
                        {
                            PreventNegativeInventory = _layInputSettings.GetValue<bool>("موجودی منفی")
                        };

                        await _appSettingService.SaveAsync(dto);
                        ClassMessageBox.ShowMSG("تنظیمات ذخیره شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
                        _layInputSettings.CallNew();
                        await LoadDataSettingAsync();
                    }
                    catch (Exception ex)
                    {
                        ClassMessageBox.ShowMSG(ex.Message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
                    }
                    finally
                    {
                        _layInputSettings._disableAfterSave = false;
                    }
                };
            });
        }

        private async Task LoadDataSettingAsync()
        {
            var dto = await _appSettingService.GetAsync();
            if (dto is null) return;

           var getInver = dto.PreventNegativeInventory;

           _layInputSettings.SetValueType("موجودی منفی", getInver);

            //picLogo.Image = BytesToImage(dto.Logo);
            //picMohr.Image = BytesToImage(dto.Mohr);
        }
        #endregion
        private async void KavoshLayout1_BtnSaveClick(object sender, EventArgs e)
        {
            var getPassOld = kavoshLayout1.GetValue<string>("کلمه عبور فعلی");
            var getPass1 = kavoshLayout1.GetValue<string>("کلمه عبور جدید");
            var getPass2 = kavoshLayout1.GetValue<string>("تایید کلمه عبور");

            var enter = await _loginUserService.Enter(new LoginUserDto { Password = getPassOld, Username = "admin" });

            if (enter.TryGetValue(true, out var userInfo) && getPass1 == getPass2)
            {
                await _loginUserService.UpdateAsync(new LoginUserDto { Username = "admin", Password = getPass1 });
                ClassMessageBox.ShowMSG("کلمه عبور تغییر یافت", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
                Close();
            }
            else
                ClassMessageBox.ShowMSG("ورودی ها بررسی کنید", Class_Text.Msg_Name, ClassMessageBox.enumIcon.بستن_مربع);
        }

        public async Task SetFieldLayInput()
        {
            //kavoshLayout1.Dispose();

            tabNavigationPage1.WaitDownPage(async () =>
            {
                layInput.RightToLeft = RightToLeft.Yes;

                layInput._btnCancel.Enabled = false;

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

                    new() { Grp = 2, Ctrl = imgLogo, SizeType = SizeConstraintsType.Custom, AutoHeight = 200 },
                    new() { Grp = 2, Ctrl = imgMohr, SizeType = SizeConstraintsType.Custom, AutoHeight = 200 },
                ]);


                layInput.CallNew();
                await LoadDataAsync();
            });
            //layInput.SetNull();
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

            layInput.SetValueType("لوگو", dto.Logo);
            layInput.SetValueType("مهر|امضا", dto.Mohr);
            //picLogo.Image = BytesToImage(dto.Logo);
            //picMohr.Image = BytesToImage(dto.Mohr);
        }

        private async void LayInput_BtnSaveClick(object sender, EventArgs e)
        {
            layInput._disableAfterSave = true;
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
                ClassMessageBox.ShowMSG("اطلاعات فروشگاه ذخیره شد.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.موفقیت);
                layInput._disableAfterSave = false;
                layInput.CallNew();
                await LoadDataAsync();

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

        #region Password
        KavoshLayout kavoshLayout1 = new KavoshLayout { RightToLeft = RightToLeft.Yes, Dock = DockStyle.Fill };
        public async Task SetFieldLayPass()
        {
            tabNavigationPage2.WaitDownPage(async () =>
            {
                await Task.Delay(500);
                //layInput.Dispose();

                PanelPass.Controls.Add(kavoshLayout1);

                kavoshLayout1._btnCancel.Enabled = false;

                var getOldPass = ClsCollect.ModelTextEditPassword("کلمه عبور فعلی", 1, 12, "");
                var getPass1 = ClsCollect.ModelTextEditPassword("کلمه عبور جدید", 6, 12, "");
                var getPass2 = ClsCollect.ModelTextEditPassword("تایید کلمه عبور", 6, 12, "");

                kavoshLayout1.SetFieldColumnDataLayout(true, 1, [
                    new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = getOldPass, AllowNull = false },
                    new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = getPass1, AllowNull = false },
                    new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = getPass2, AllowNull = false }
                ], 13);
                kavoshLayout1.CallNew();
            });


            //await Task.Delay(500);
        }

        #endregion
        private void FrmStoreInfo_Load(object sender, EventArgs e) { }


    }
}