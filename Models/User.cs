using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("Users")]
    public class User
    {
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Role { get; set; } = string.Empty;
        public int? CustomerID { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual Customer? Customer { get; set; }
    }
}
