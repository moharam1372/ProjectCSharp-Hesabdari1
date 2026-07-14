using System.ComponentModel.DataAnnotations.Schema;

namespace Kavosh.Domain.Entities
{
    public class ProductUnit : BaseEntity
    {
        public string Title { get; set; }

        [ForeignKey("ProductUnitId")]

        public virtual ICollection<Product> Product { get; set; }


    }
}