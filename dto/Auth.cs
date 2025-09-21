using System.ComponentModel.DataAnnotations;
using EcommerceAPI.Utils.Validation;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [PasswordStrength] // Added custom password strength validation
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression(@"^(Admin|Customer)$", ErrorMessage = "Role must be either 'Admin' or 'Customer'")]
    public string Role { get; set; } = string.Empty;
    public int? UserID { get; internal set; }
}

public class LoginDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}

public class CompleteProfileDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [PhoneNumber] // Added custom phone number validation
    public string Phone { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
    public string Address { get; set; } = string.Empty;
}

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "New password is required")]
    [PasswordStrength]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("NewPassword", ErrorMessage = "Password confirmation does not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public bool ProfileCompleted { get; set; }
    public CustomerProfileDto Customer { get; set; } = new CustomerProfileDto();
}

public class CustomerProfileDto
{
    public int CustomerID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
