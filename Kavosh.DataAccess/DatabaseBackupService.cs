using Kavosh.DataAccess.Backup;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Kavosh.DataAccess
{
    public class DatabaseBackupService
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly string _backupFolder;

        public DatabaseBackupService(AppDbContext context)
        {
            _connectionString = context.Database.GetConnectionString();
            var builder = new SqlConnectionStringBuilder(_connectionString);
            _databaseName = builder.InitialCatalog;

            //_backupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            _backupFolder =  "d:\\Backups Khani";
            Directory.CreateDirectory(_backupFolder);
        }

        public List<BackupFileInfo> GetBackupList()
        {
            return Directory.GetFiles(_backupFolder, "*.bak")
                .Select(f => new FileInfo(f))
                .Select(fi => new BackupFileInfo
                {
                    FilePath = fi.FullName,
                    FileName = Path.GetFileNameWithoutExtension(fi.Name),
                    CreatedAt = fi.CreationTime,
                    SizeInBytes = fi.Length
                })
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }

        public async Task BackupAsync(IProgress<int> progress,string fileName, CancellationToken ct = default)
        {

            #region Edit
            if (!Directory.Exists("Backup"))
                Directory.CreateDirectory("Backup");
            var getDirectory = Environment.CurrentDirectory + $"\\Backup\\{"Test"}";

            string query = $@"BACKUP DATABASE [GamePort] TO  
DISK = N'{getDirectory}.bak' 
WITH NOFORMAT, INIT, 
NAME = N'{_databaseName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
//NAME = N'GamePort-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

            #endregion
           // var fileName = DateTime.Now.ToString("yyyy-MM-dd  -  HH-mm-ss") + ".bak";
            var filePath = Path.Combine(_backupFolder, fileName);

            using var connection = new SqlConnection(GetMasterConnectionString());
            connection.InfoMessage += (s, e) => ReportProgressFromMessage(e, progress);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandTimeout = 0;   // بدون محدودیت — بکاپ ممکنه طول بکشه
            command.CommandText = $"BACKUP DATABASE [{_databaseName}] TO DISK = @path WITH INIT, STATS = 5";
          //  command.CommandText = query;
            command.Parameters.AddWithValue("@path", filePath);

            await command.ExecuteNonQueryAsync(ct);
            progress?.Report(100);
        }

        public async Task RestoreAsync(string backupFilePath, IProgress<int> progress, CancellationToken ct = default)
        {
            // 👇 حیاتی: بدون این خط، احتمال بالای خطای "database in use" هست
            SqlConnection.ClearAllPools();

            using var connection = new SqlConnection(GetMasterConnectionString());
            connection.InfoMessage += (s, e) => ReportProgressFromMessage(e, progress);
            await connection.OpenAsync(ct);

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandTimeout = 60;
                cmd.CommandText = $"ALTER DATABASE [{_databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                await cmd.ExecuteNonQueryAsync(ct);
            }

            try
            {
                using var cmd = connection.CreateCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = $"RESTORE DATABASE [{_databaseName}] FROM DISK = @path WITH REPLACE, STATS = 5";
                cmd.Parameters.AddWithValue("@path", backupFilePath);
                await cmd.ExecuteNonQueryAsync(ct);
            }
            finally
            {
                // هر اتفاقی افتاد (موفق یا خطا)، باید دیتابیس رو از حالت SINGLE_USER دربیاریم
                using var cmd = connection.CreateCommand();
                cmd.CommandTimeout = 60;
                cmd.CommandText = $"ALTER DATABASE [{_databaseName}] SET MULTI_USER";
                await cmd.ExecuteNonQueryAsync(ct);
            }

            progress?.Report(100);
        }

        private string GetMasterConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString) { InitialCatalog = "master" };
            return builder.ConnectionString;
        }

        private static void ReportProgressFromMessage(SqlInfoMessageEventArgs e, IProgress<int> progress)
        {
            foreach (SqlError error in e.Errors)
            {
                // پیام‌های STATS معمولاً شبیه: "10 percent processed."
                var match = Regex.Match(error.Message, @"(\d+)\s*percent");
                if (match.Success && int.TryParse(match.Groups[1].Value, out var percent))
                    progress?.Report(percent);
            }
        }
    }
}