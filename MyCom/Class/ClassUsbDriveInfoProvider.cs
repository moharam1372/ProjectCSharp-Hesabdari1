using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCom.Class
{

    public static class ClassUsbDriveInfoProvider
    {
        public class UsbDriveInfo
        {
            public string VolumeLabel { get; set; }
            public string DriveLetter { get; set; }
            public string TotalSize { get; set; }
            public ulong TotalSizeUse { get; set; }
            public string FreeSpace { get; set; }
            public ulong FreeSpaceUse { get; set; }
            public string StableSerial { get; set; }
        }
        public static List<ManagementObject> SafeWmiQuery(string query, int timeoutMs = 5000)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var searcher = new ManagementObjectSearcher(query);
                    return searcher.Get().Cast<ManagementObject>().ToList();
                }
                catch
                {
                    return new List<ManagementObject>();
                }
            });

            if (task.Wait(timeoutMs))
                return task.Result;
            return new List<ManagementObject>(); // رد کردن در صورت تاخیر
        }

        public static List<UsbDriveInfo> GetUsbDrives()
        {
            var result = new List<UsbDriveInfo>();

            try
            {
                // var diskDrives = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'").Get();
                var diskDrives = SafeWmiQuery("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'", 5000);

                foreach (ManagementObject disk in diskDrives)
                {
                    try
                    {
                        string deviceId = disk["DeviceID"]?.ToString() ?? "";
                        string pnpId = disk["PNPDeviceID"]?.ToString() ?? "";
                        string serial = ExtractSerialFromPnP(pnpId);

                        var partitions = SafeWmiQuery($"ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{deviceId}'}} " +
                                                      $"WHERE AssocClass = Win32_DiskDriveToDiskPartition", 3000);


                        foreach (ManagementObject partition in partitions)
                        {
                            try
                            {
                                var logicalDisks = SafeWmiQuery(
                                    $"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}}" +
                                    $" WHERE AssocClass = Win32_LogicalDiskToPartition", 3000);


                                foreach (ManagementObject logicalDisk in logicalDisks)
                                {

                                    try
                                    {
                                        var info = new UsbDriveInfo
                                        {
                                            VolumeLabel = logicalDisk["VolumeName"]?.ToString() ?? "Unknown",
                                            DriveLetter = logicalDisk["DeviceID"]?.ToString() ?? "Unknown",
                                            TotalSize = FormatSize(Convert.ToUInt64(logicalDisk["Size"] ?? 0)),
                                            TotalSizeUse = Convert.ToUInt64(logicalDisk["Size"] ?? 0),
                                            FreeSpace = FormatSize(Convert.ToUInt64(logicalDisk["FreeSpace"] ?? 0)),
                                            FreeSpaceUse = Convert.ToUInt64(logicalDisk["FreeSpace"] ?? 0),

                                            StableSerial = serial
                                        };

                                        result.Add(info);
                                    }
                                    catch { /* skip logicalDisk errors */ }
                                }
                            }
                            catch { /* skip partition errors */ }
                        }
                    }
                    catch { /* skip disk errors */ }
                }
            }
            catch { /* global WMI error */ }

            return result;
        }
        public static string FormatSize(this ulong bytes)
        {
            const ulong OneGB = 1024UL * 1024UL * 1024UL;
            const ulong OneMB = 1024UL * 1024UL;

            if (bytes >= OneGB)
            {
                double gb = bytes / (double)OneGB;
                return $"{gb:0.00}  (GB)";
            }
            else
            {
                double mb = bytes / (double)OneMB;
                return $"{Math.Round(mb)}  (MB)";
            }
        }
        private static string ExtractSerialFromPnP(string pnpId)
        {
            try
            {
                var parts = pnpId.Split('\\');
                if (parts.Length >= 3)
                    return parts[2].Split('&')[0];
            }
            catch { }
            return "Unknown";
        }


        public static void ConnectedDisconnected(Control uiContext, Action connect, Action disconnect)
        {
            HashSet<string> processedVolumes = new HashSet<string>();
            ManagementEventWatcher watcher;
            // 1. پرس‌وجوی رابطه بین دیسک و پارتیشن
            string assocQuery = @"SELECT * FROM __InstanceOperationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_DiskPartition'";

            watcher = new ManagementEventWatcher(assocQuery);
            watcher.EventArrived += (sender, args) =>
            {
                // درایو پارتیشن
                var partition = (ManagementBaseObject)args.NewEvent["TargetInstance"];
                var partitionName = partition["DeviceID"]?.ToString(); // مثلاً \\.\PHYSICALDRIVE1

                // 2. پیدا کردن دیسک فیزیکی مرتبط
                string driveQuery = $@"Associators of {{Win32_DiskPartition.DeviceID='{partitionName}'}} Where AssocClass=Win32_DiskDriveToDiskPartition";
                try
                {
                    var searcher = new ManagementObjectSearcher(driveQuery);

                    var managementObjectCollection = searcher.Get();
                    if (managementObjectCollection != null)
                    {
                        foreach (var o in managementObjectCollection)
                        {
                            var disk = (ManagementObject)o;
                            var deviceId = disk["DeviceID"]?.ToString(); // مثلاً \\.\PHYSICALDRIVE1
                            // برای USB فقط فیلتر کنید
                            var interfaceType = disk["InterfaceType"]?.ToString(); // "USB"

                            if (interfaceType != "USB")
                                continue;

                            // 3. فقط اولین پارتیشن را پردازش کنید
                            if (processedVolumes.Add(deviceId))
                            {
                                // حالا می‌توانید از `Win32_Volume` برای گرفتن حرف درایو استفاده کنید
                                string volQuery =
                                    $@"Associators of {{Win32_DiskPartition.DeviceID='{partitionName}'}} Where AssocClass=Win32_LogicalDiskToPartition";

                                foreach (ManagementObject vol in new ManagementObjectSearcher(volQuery).Get())
                                {
                                    var driveLetter = vol["DeviceID"]?.ToString(); // مثلاً "E:"
                                    if (!string.IsNullOrEmpty(driveLetter))
                                    {
                                        Console.WriteLine($"✅ USB Disk {deviceId} mounted as {driveLetter}");
                                        uiContext.Invoke(connect);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        uiContext.Invoke(disconnect);
                    }
                }
                catch (Exception e)
                {
                    uiContext.Invoke(disconnect);
                }
                processedVolumes.Clear();
            };

            watcher.Start();
            Console.WriteLine("در حال گوش دادن به رویدادهای USB...");
        }
        //public static void ConnectedDisconnected(Control uiContext, Action connect, Action disconnect)
        //{
        //    //var query = new WqlEventQuery(
        //    //    "SELECT * FROM __InstanceOperationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_Volume'"
        //    //    );

        //    var query = new WqlEventQuery(
        //        "SELECT * FROM __InstanceOperationEvent WITHIN 2 " +
        //        "WHERE TargetInstance ISA 'Win32_Volume' AND TargetInstance.DriveType = 2"
        //    );
        //    //var query1 = new WqlEventQuery(
        //    //    "SELECT * FROM __InstanceOperationEvent  "
        //    //);

        //    var watcher = new ManagementEventWatcher(query);
        //    var watcher2 = new ManagementEventWatcher(query);

        //    watcher.EventArrived += (sender, args) =>
        //    {
        //        var eventType = args.NewEvent.ClassPath.ClassName;
        //        var volume = (ManagementBaseObject)args.NewEvent["TargetInstance"];
        //        var driveLetter = volume["DriveLetter"];

        //        if (eventType == "__InstanceCreationEvent")
        //        {
        //            Console.WriteLine($"✅ Volume Mounted: {driveLetter}");
        //            uiContext.Invoke(connect);
        //            //connect?.Invoke();
        //        }
        //        else if (eventType == "__InstanceDeletionEvent")
        //        {
        //            Console.WriteLine($"❌ Volume Removed: {driveLetter}");
        //            uiContext.Invoke(disconnect);
        //            //disconnect?.Invoke();
        //        }
        //    };

        //    watcher.Start();
        //    Console.WriteLine("در حال گوش دادن به رویدادهای Volume...");
        //}
    }
}

