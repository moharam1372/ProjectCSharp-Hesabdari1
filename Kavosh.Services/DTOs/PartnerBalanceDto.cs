namespace Kavosh.Services.DTOs
{
    public class PartnerBalanceDto
    {
        public Guid PartnerId { get; set; }
        public string PartnerFullName { get; set; }

        /// <summary>
        /// مثبت = شرکت به شریک بدهکار است (شریک بستانکار است)
        /// منفی = شریک به شرکت بدهکار است
        /// </summary>
        public long Balance { get; set; }
    }
}