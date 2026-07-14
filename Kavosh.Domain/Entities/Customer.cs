using System.ComponentModel.DataAnnotations;

namespace Kavosh.Domain.Entities
{
    public class Customer: BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        // آماده‌سازی برای آینده چندکاربره
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }  // برای Optimistic Concurrency
    }
}
