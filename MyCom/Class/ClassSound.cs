using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public static class ClassSound
    {
        static SoundPlayer player = new SoundPlayer();

        public static SoundPlayer Play1()
        {
            // تنظیم مسیر فایل WAV
            //player.SoundLocation = @"D:\sound.wav";
            player.Stream = Properties.Resources.Sale;

            // بارگذاری فایل صوتی
            player.Load();
            

            // پخش فایل به صورت غیرهمزمان
            Console.WriteLine("در حال پخش صدا...");
            player.PlayLooping();
            return player;
        }

   
    }
}
