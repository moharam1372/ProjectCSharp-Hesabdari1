using ImageMagick;
using System;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MyCom.Class
{
    public static class ClassGraphic
    {
        //public static Image ConvertWebpToImage(this string from)
        //{
        //    using (var magickImage = new MagickImage(from))
        //    {
        //        // ذخیره به JPG
        //        magickImage.Format = MagickFormat.Jpg;
        //      //  magickImage.Write(to);

        //        // تبدیل به System.Drawing.Image
        //        using (var ms = new MemoryStream())
        //        {
        //            magickImage.Format = MagickFormat.Bmp; // یا PNG برای کیفیت بهتر
        //            magickImage.Write(ms);
        //            ms.Position = 0;
        //            return Image.FromStream(ms); // خروجی از نوع Image
        //        }
        //    }
        //}

        public static Image OpenImageFrom(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // تصویر را از Stream می‌خوانیم
                Image img = Image.FromStream(fs);

                // اگر بخواهید تصویر را ذخیره کنید، می‌توانید آن را به‌صورت Bitmap ذخیره کنید
                Image clone = new Bitmap(img);

                return clone;
                // Stream به‌صورت خودکار بسته می‌شود؛ فایل دیگر قفل نیست
            }
        }

        public static Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static Image PrintScreenForm2()
        {
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(0, 0, 0, 0, bm.Size);

            return (Image)bm;
        }
        public static byte[] ImageToByte2(this Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        public static Image PrintScreenForm()
        {
            SendKeys.Send("%{PRTSC}");
            var get = Clipboard.GetImage();
            Clipboard.Clear();
            return get;

            // get?.Save(@"D:\screen1.jpg", ImageFormat.Jpeg);
        }
        public static byte[] ImageToByte(this System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static Image ByteToImage(this byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }



        public static Image GetScreen()
        {
            Screen scrn = Screen.PrimaryScreen;
            Bitmap bitmap = new Bitmap(scrn.Bounds.Width, scrn.Bounds.Height, PixelFormat.Format32bppArgb);
            var cr = Screen.AllScreens[0].Bounds;
            Graphics cg = Graphics.FromImage(bitmap);
            cg.CopyFromScreen(cr.Left, cr.Top, 0, 0, cr.Size);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                return Image.FromStream(ms);
            }
        }
        public static string GetScreenAsBase64()
        {
            Screen scrn = Screen.PrimaryScreen;
            Bitmap bitmap = new Bitmap(scrn.Bounds.Width, scrn.Bounds.Height, PixelFormat.Format32bppArgb);
            var cr = Screen.AllScreens[0].Bounds;
            Graphics cg = Graphics.FromImage(bitmap);
            cg.CopyFromScreen(cr.Left, cr.Top, 0, 0, cr.Size);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }


    }
}
