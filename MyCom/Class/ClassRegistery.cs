using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MyCom.Class
{
    public static class ClassRegistery
    {
        public class ModelMenu
        {
            /// <summary>
            /// اسمی که قرار به فارسی در راست کلیلک اضافه بشه
            /// </summary>
            public string NameMenu { get; set; }
            public string IconAddress { get; set; }
            /// <summary>
            /// ali;mojtaba;reza 
            /// </summary>  
            public List<ModelSubMenu> SubCommand { get; set; }
        }

        public class ModelSubMenu
        {
            public string SubMenu { get; set; }
            public string SubMenuPersian { get; set; }
        }
        public static void AddMenuToRightClickFiles(string nameKey, string pathFileRun, ModelMenu menu)
        {
            //  var getAssress = Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");



            // ساخت منوی اصلی "ایران کاوش"
            using (RegistryKey iranKavoshKey = Registry.ClassesRoot.CreateSubKey($@"*\shell\{nameKey}"))
            {
                //iranKavoshKey.SetValue("MUIVerb", "ایران کاوش");
                //iranKavoshKey.SetValue("SubCommands", "AddVideo;CreatePoster");
                //iranKavoshKey.SetValue("Icon", "\"" + "E:\\Project Sadegh\\Icon\\atom-svgrepo-com.ico" + "\"");

                iranKavoshKey.SetValue("MUIVerb", menu.NameMenu);
                var getJoin = string.Join(";", menu.SubCommand.Select(s => s.SubMenu));
                iranKavoshKey.SetValue("SubCommands", getJoin);
                iranKavoshKey.SetValue("Icon", "\"" + "E:\\Project Sadegh\\Icon\\atom-svgrepo-com.ico" + "\"");
            }


            string commandStorePath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell";

            foreach (ModelSubMenu sm in menu.SubCommand)
            {
                // افزودن "افزودن فیلم"
                using (RegistryKey addVideoKey = Registry.LocalMachine.CreateSubKey($"{commandStorePath}\\{sm.SubMenu}"))
                {
                    addVideoKey.SetValue("", sm.SubMenuPersian);
                    using (RegistryKey commandKey = addVideoKey.CreateSubKey("command"))
                    {
                        //commandKey.SetValue("", "\"" + pathFileRun + "\"%1\""); // مشکل در ارسال آدرس با فاصله
                        //commandKey.SetValue("", $"\"{pathFileRun}\" \"%1\"");
                        //object value = $"\"{pathFileRun}\" \"%1\"";
                        object value = $"\"{pathFileRun}\" \"{sm.SubMenu}\" \"%1\"";

                        commandKey.SetValue("", value);


                        //  commandKey.SetValue("", "\"C:\\Program Files\\MyApp\\add_video.exe\" \"%1\"");
                    }
                }
            }

            //// افزودن "افزودن فیلم"
            //using (RegistryKey addVideoKey = Registry.LocalMachine.CreateSubKey($"{commandStorePath}\\AddVideo"))
            //{
            //    addVideoKey.SetValue("", "افزودن فیلم");
            //    using (RegistryKey commandKey = addVideoKey.CreateSubKey("command"))
            //    {
            //        commandKey.SetValue("", "\"" + pathFileRun + "\"%1\"");
            //        //  commandKey.SetValue("", "\"C:\\Program Files\\MyApp\\add_video.exe\" \"%1\"");
            //    }
            //}

            //// افزودن "ساخت پوستر"
            //using (RegistryKey createPosterKey = Registry.LocalMachine.CreateSubKey($"{commandStorePath}\\CreatePoster"))
            //{
            //    createPosterKey.SetValue("", "ساخت پوستر");
            //    using (RegistryKey commandKey = createPosterKey.CreateSubKey("command"))
            //    {
            //        commandKey.SetValue("", "\"" + pathFileRun + "\"%1\"");
            //    }
            //}


        }
    }
}
