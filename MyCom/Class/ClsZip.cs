using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
namespace MyCom.Class
{
    public static class ClsZip
    {
        public static void Zip(string address1, string address2)
        {
            ZipFile.CreateFromDirectory(address1, address2, CompressionLevel.Optimal, includeBaseDirectory: false);
        }
        public static void ExtractZip(string fromZip, string extractTo)
        {
            ZipFile.ExtractToDirectory(fromZip, extractTo);
        }
        public static void RemoveFolderFromZip(string zipPath, string folderName)
        {
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                var entriesToRemove = archive.Entries
                    .Where(entry => entry.FullName.StartsWith(folderName + "/", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var entry in entriesToRemove)
                {
                    entry.Delete();
                }
            }
        }
        public static void CompressSelectedFiles(string sourceFolder, string zipPath, List<string> selectedFiles)
        {
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            {
                foreach (string fileName in selectedFiles)
                {
                    string fullPath = Path.Combine(sourceFolder, fileName);

                    if (File.Exists(fullPath))
                    {
                        archive.CreateEntryFromFile(fullPath, fileName);
                    }
                }
            }
        }
    }
}
