namespace Kavosh.Domain.Entities
{
    public class ProductGroup : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}