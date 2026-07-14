using ImageMagick;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
//using Microsoft.AspNetCore.Http;

namespace MyCom.Class
{
    public static class ClassImage
    {
        public static Image ResizeByPercent(Image originalImage, int percent)
        {
            // اعتبارسنجی درصد
            if (percent < 0 || percent > 100)
                throw new ArgumentException("درصد باید بین 0 تا 100 باشد");

            // محاسبه ابعاد جدید
            double ratio = (100 - percent) / 100.0;
            int newWidth = (int)(originalImage.Width * ratio);
            int newHeight = (int)(originalImage.Height * ratio);

            // ایجاد تصویر جدید
            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                // تنظیمات برای بالاترین کیفیت
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
        public static Image ResizeAndCompressImage(Image originalImage, int maxWidth = 800, long quality = 60L)
        {
            // محاسبه نسبت ابعاد جدید
            double ratio = (double)maxWidth / originalImage.Width;
            int newHeight = (int)(originalImage.Height * ratio);
            int newWidth = maxWidth;

            if (originalImage.Width <= maxWidth)
            {
                newWidth = originalImage.Width;
                newHeight = originalImage.Height;
            }

            // ایجاد بیت‌مپ جدید
            var bitmap = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            // ذخیره در MemoryStream
            var codec = GetEncoderInfo("image/jpeg");
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, codec, encoderParams);

                // ایجاد Image جدید از آرایه بایت
                var resultImage = Image.FromStream(ms);

                // کپی کردن برای جلوگیری از بسته شدن Stream
                var finalImage = new Bitmap(resultImage);

                return finalImage;
            }
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            foreach (var codec in codecs)
            {
                if (codec.MimeType == mimeType)
                    return codec;
            }
            return null;
        }
        public static Image QRCode(this string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var getQRImage = qrCode.GetGraphic(20);
            return getQRImage;
            //lblInfo.Text = "لینک ساخته شد. گوشی را به وای‌فای وصل کن و QR Code را اسکن کن.";

            //using (var im = Image.FromFile(data))
            //{
            //    //Bitmap bm = new Bitmap(im, 170, 230);
            //    Bitmap bm = new Bitmap(im);
            //    im.Dispose();
            //    return bm;
            //}
        }
        public static Image LoadBitmap(this string path)
        {
            using (var im = Image.FromFile(path))
            {
                //Bitmap bm = new Bitmap(im, 170, 230);
                Bitmap bm = new Bitmap(im);
                im.Dispose();
                return bm;
            }
        }
        /// <summary>
        /// 170, 230
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image LoadBitmapSize(this string path, Size size = default)
        {
            using (var im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im, size == default ? new Size(170, 230) : size);
                im.Dispose();
                return bm;
            }
        }
        public static Image LoadBitmapSizeLow(this string path, Size size = default)
        {
            var targetSize = size == default ? new Size(170, 230) : size;

            using (var original = Image.FromFile(path))
            using (var resized = new Bitmap(original, targetSize))
            {
                var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L); // کیفیت پایین‌تر

                var ms = new MemoryStream();
                resized.Save(ms, jpgEncoder, encoderParams);
                ms.Position = 0;

                // مهم: از using برای Image استفاده نکن چون می‌خوای برگردونی
                return Image.FromStream(ms); // تصویر فشرده‌شده
            }


        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
        //public static Image ConvertWebpToImage(this string address)
        //{
        //    var getByte = File.ReadAllBytes(address);
        //    using (var stream = new MemoryStream(getByte))
        //    {
        //        using (var image = new MagickImage(stream))
        //        {
        //            // Convert the WebP image to a JPG image
        //            using (var outputStream = new MemoryStream())
        //            {
        //                image.Format = MagickFormat.Jpeg;
        //                image.Quality = 80;
        //                image.Write(outputStream);

        //                // return image.;

        //                var gg = Convert.ToBase64String(outputStream.ToArray());


        //                return Base64ToImage(gg);
        //            }
        //        }
        //    }
        //}
        public static Image ConvertWebpToImage(this string address)
        {
            var getByte = File.ReadAllBytes(address);
            using (var stream = new MemoryStream(getByte))
            using (var image = new MagickImage(stream))
            using (var outputStream = new MemoryStream())
            {
                image.Format = MagickFormat.Jpeg;
                image.Quality = 80;
                image.Write(outputStream);

                outputStream.Position = 0;
                return Image.FromStream(outputStream);
            }
        }

        public static Image Base64ToImage(this string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public static byte[] ImageToByteArray(this System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                var imageToByteArray = ms.ToArray();
                ms.Dispose();
                return imageToByteArray;
            }
        }

        public static string ImageToBase64(this Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            using (var ms = new MemoryStream())
            {
                // ذخیره تصویر در فرمت دلخواه (مثلاً PNG, JPEG)
                image.Save(ms, ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
        public static Image ByteArrayToImage(this byte[] imageIn)
        {
            using (var ms = new MemoryStream(imageIn))
            {
                return Image.FromStream(ms);
            }
        }
        //public static Image GetImagePost(this IFormFile file)
        //{
        //    var get = file.FileName;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        file.CopyTo(memoryStream);
        //        using (var img = Image.FromStream(memoryStream))
        //        {

        //            return img;
        //            //img.Save(getPath + "\\Image.jpg");
        //            //img.Dispose();
        //            // return Task.FromResult<IActionResult>(Ok(img));
        //            // TODO: ResizeImage(img, 100, 100);
        //        }
        //    }
        //}
        public static Image GetImageByLink(this string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            using var response2 = request.GetResponse();
            using var stream2 = response2.GetResponseStream();
            return Bitmap.FromStream(stream2);
        }
    }
}
