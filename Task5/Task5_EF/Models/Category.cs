using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5_EF.Models
{
    [Table("Northwind.Categories")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
