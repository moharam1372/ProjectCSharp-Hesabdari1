using Kavosh.Domain.Enums;

namespace Kavosh.Services.DTOs
{
    public class ChequeDto
    {
        public Guid Id { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime DueDate { get; set; }
        public long Price { get; set; }
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public bool IsReceived { get; set; }
        public ChequeStatus Status { get; set; }
        public string Description { get; set; }
    }
}