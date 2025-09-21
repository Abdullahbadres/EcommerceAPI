using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
