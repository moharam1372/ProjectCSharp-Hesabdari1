namespace Kavosh.Services.DTOs
{
    public class DefinitiveAccountDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public long DocNumber { get; set; }
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public DateTime DateCustom { get; set; }
        public long Price { get; set; }
        public bool Debtor { get; set; }
        public string Description { get; set; }
        public bool IsCheck { get; set; }
        public Guid? HowToPayId { get; set; }
    }
}