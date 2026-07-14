using DevExpress.Office.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{

    public static class ClassInstallFonts
    {
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        public static void InstallFonts()
        {
            if (Directory.Exists("FontSamim"))
            {
                var getFonts = Directory.GetFiles("FontSamim");
                foreach (var font in getFonts)
                {
                    int result = AddFontResource(font);
                }
            }
          
            //   string fontPath = @"C:\MY_FONT_LOCATION\MY_NEW_FONT.TTF";
            //  int result = AddFontResource(fontPath);
        }
    }
}
