using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public class ClassFileManage
    {
        public class ModelFile
        {
            public Guid ChapterHeaderId { get; set; }    
            //public Guid DetailChapterId { get; set; }    
            public string Name { get; set; }    
            public string From { get; set; }    
            public string To { get; set; }
        }
        public async Task CopyFileAsync(string sourcePath, string destinationPath, IProgress<double> progress = null)
        {
            //const int bufferSize = 1024 * 1024; // 1MB buffer
            const int bufferSize = 1048576; // 1MB buffer
            byte[] buffer = new byte[bufferSize];
            long totalBytes = new FileInfo(sourcePath).Length;
            long totalRead = 0;

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                int bytesRead;
                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await destinationStream.WriteAsync(buffer, 0, bytesRead);
                    totalRead += bytesRead;

                    progress?.Report((double)totalRead / totalBytes * 100);
                }
            }
        }
        public async Task CopyFileAsyncUseCancel(string sourcePath, string destinationPath, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            const int bufferSize = 1048576; // 1MB buffer
            byte[] buffer = new byte[bufferSize];
            long totalBytes = new FileInfo(sourcePath).Length;
            long totalRead = 0;

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                int bytesRead;
                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested(); // بررسی کنسل شدن

                    await destinationStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                    totalRead += bytesRead;

                    progress?.Report((double)totalRead / totalBytes * 100);
                }
            }
        }

    }
}
