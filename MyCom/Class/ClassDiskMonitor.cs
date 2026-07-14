using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public class ClassDiskMonitor
    {
        private readonly PerformanceCounter _diskActiveTime =
            new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

        public float GetCurrentDiskUsage()
        {
            // متد NextValue() مقدار فعلی رو برمی‌گردونه
            // برای مقداردهی اولیه، صدا زدنش قبل از حلقه لازمه
            return _diskActiveTime.NextValue();
        }
    }
}
