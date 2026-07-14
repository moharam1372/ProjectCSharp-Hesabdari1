namespace Kavosh.Domain.Entities
{
    public class Product : BaseEntity
    {
        public long ProductCode { get; set; }

        public Guid ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }

        public string Title { get; set; }

        public Guid ProductUnitId { get; set; }
        public ProductUnit ProductUnit { get; set; }

        /// <summary>
        /// موجودی اولیه
        /// </summary>
        public float InitialInventory { get; set; }
        public long UnitPrice { get; set; }
        public long SellPrice { get; set; }
        public string Description { get; set; }
    }
}