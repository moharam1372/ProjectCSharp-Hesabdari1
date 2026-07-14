using System.ComponentModel.DataAnnotations.Schema;

namespace Kavosh.Domain.Entities
{
    public class ProductGroup : BaseEntity
    {
        public string Title{ get; set; }

        [ForeignKey("ProductGroupId")]

        public virtual ICollection<Product> Product { get; set; }

    }
}