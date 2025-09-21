using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("addresses")]
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; } = string.Empty;

        [StringLength(100)]
        public string Country { get; set; } = "USA";

        public bool IsDefault { get; set; } = false;

        // Navigation property
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }
    }
}
