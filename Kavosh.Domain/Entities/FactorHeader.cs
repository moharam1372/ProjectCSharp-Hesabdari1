namespace Kavosh.Domain.Entities
{
    public class FactorHeader : BaseEntity
    {
        /// <summary>
        /// به صورت خوکار از 1000 شروع بشه
        /// </summary>
        public long Code { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        /// <summary>
        /// فروش یا خرید
        /// فروش = true
        /// </summary>
        public bool Type { get; set; }
        public DateTime DateFactor { get; set; }
        public long Discount { get; set; }
        public long PriceTotal { get; set; }

        public ICollection<FactorDetail> FactorDetails { get; set; } = new List<FactorDetail>();
        public ICollection<HowToPay> HowToPays { get; set; } = new List<HowToPay>();
    }
}