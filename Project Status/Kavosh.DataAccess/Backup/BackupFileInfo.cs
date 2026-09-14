namespace Kavosh.DataAccess.Backup
{
    public class BackupFileInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public long SizeInBytes { get; set; }
    }
}