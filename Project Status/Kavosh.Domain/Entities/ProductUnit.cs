namespace Kavosh.Domain.Entities
{
    public class ProductUnit : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}