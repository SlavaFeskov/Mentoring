using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5_EF.Models
{
    [Table("Northwind.CreditCards")]
    public class CreditCard
    {
        [Key]
        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ExpireDate { get; set; }

        [Required]
        public string CardHolder { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}