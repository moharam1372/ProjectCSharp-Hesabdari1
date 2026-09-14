
using System.IO;
using DevExpress.Utils.Menu; // Required for DXPopupMenu and DXMenuItem
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Events;
using MyCom.Class;
using static DevExpress.Utils.Localization.XtraLocalizer;

namespace Kavosh.UI.Forms
{
    public partial class FrmManagePictureCheck : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _numCheck = null;
        private ClsFont _clsFont = new();
        private ClsFont _clsFontBold = new(true);

        public FrmManagePictureCheck(string numCheck)
        {
            _numCheck = numCheck;
            InitializeComponent();
        }

        private void FrmManagePictureCheck_Load(object sender, EventArgs e)
        {
            _clsFontBold.ChangeFont(btnSave);
            picCheck.PopupMenuShowing += picCheck_PopupMenuShowing;
            if (_numCheck != null)
            {
                var getAddress2 = Path.Combine(Application.StartupPath, "ImgCheck", _numCheck+".jpg");
                if (File.Exists(getAddress2))
                {
                    picCheck.Image = getAddress2.LoadBitmap();
                }
            }
        }
   
        private void picCheck_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            e.PopupMenu.Appearance.Font = _clsFont.ChangeFont(16);
            if (e.PopupMenu is DXPopupMenu popupMenu1)
            {
                popupMenu1.IsRightToLeft = true;
                foreach (DXMenuItem item in popupMenu1.Items)
                {
                    //item.Appearance.Font = _clsFont.ChangeFont(16);
                    if (item.Caption == "Copy")
                    {
                        item.Caption = "کپی تصویر";
                    }
                    else if (item.Caption == "Paste")
                    {
                        item.Caption = "چسباندن تصویر";
                    }
                    else if (item.Caption == "Load")
                    {
                        item.Caption = "بارگذاری تصویر";
                        item.Appearance.ForeColor = Color.FromArgb(2, 101, 127);
                    }
                    else if (item.Caption == "Delete")
                    {
                        item.Caption = "حذف تصویر";
                    }
                    else if (item.Caption == "Save")
                    {

                        item.Visible = false;
                    }
                }
            }
        }

        // رویداد کلیک آیتم جدید

        void XtraLocalizer_QueryLocalizedString(object sender, QueryLocalizedStringEventArgs e)
        {
            // شناسایی رشته‌های مربوط به منوی PictureEdit
            if (e.StringIDType == typeof(StringId))
            {
                switch ((StringId)e.StringID)
                {
                    // دستور Load (بارگذاری تصویر)
                    case StringId.PictureEditMenuLoad:
                        e.Value = "بارگذاری تصویر";
                        break;
                    // دستور Save (ذخیره تصویر)
                    case StringId.PictureEditMenuSave:
                        e.Value = "ذخیره تصویر";
                        break;
                    // دستور Cut (برش)
                    case StringId.PictureEditMenuCut:
                        e.Value = "برش";
                        break;
                    // دستور Copy (کپی)
                    case StringId.PictureEditMenuCopy:
                        e.Value = "کپی";
                        break;
                    // دستور Paste (چسباندن)
                    case StringId.PictureEditMenuPaste:
                        e.Value = "چسباندن";
                        break;
                    // دستور Delete (حذف)
                    case StringId.PictureEditMenuDelete:
                        e.Value = "حذف";
                        break;
                    // دستور Edit (ویرایش) - در صورت وجود
                    case StringId.PictureEditMenuEdit:
                        e.Value = "ویرایش";
                        break;
                    // منوی Zoom (بزرگ‌نمایی)
                    case StringId.PictureEditMenuZoom:
                        e.Value = "بزرگ‌نمایی";
                        break;
                    // زیرمنوی Zoom In (بزرگ‌تر)
                    case StringId.PictureEditMenuZoomIn:
                        e.Value = "بزرگ‌تر";
                        break;
                    // زیرمنوی Zoom Out (کوچک‌تر)
                    case StringId.PictureEditMenuZoomOut:
                        e.Value = "کوچک‌تر";
                        break;
                        //// زیرمنوی Zoom to Fit (متناسب با صفحه)
                        //case StringId.PictureEditMenuZoomToFit:
                        //    e.Value = "متناسب با صفحه";
                        //    break;
                        //// زیرمنوی Reset Zoom (بازنشانی بزرگ‌نمایی)
                        //case StringId.PictureEditMenuResetZoom:
                        //    e.Value = "بازنشانی";
                        //    break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
            var getAddress = Path.Combine(Application.StartupPath, "ImgCheck");
            Directory.CreateDirectory(getAddress);
            var getAddress2 = Path.Combine(Application.StartupPath, "ImgCheck", _numCheck+".jpg");
            if (picCheck.Image == null)
            {
                File.Delete(getAddress2);
                return;
            }
            picCheck.Image.SaveAsJpg(getAddress2);

        }
    }
}