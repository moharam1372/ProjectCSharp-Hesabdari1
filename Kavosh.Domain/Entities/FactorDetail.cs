namespace Kavosh.Domain.Entities
{
    public class FactorDetail : BaseEntity
    {
        public Guid FactorHeaderId { get; set; }
        public virtual FactorHeader FactorHeader { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }


        public float Count { get; set; }
        public long PriceUnit { get; set; } 
    }
}       