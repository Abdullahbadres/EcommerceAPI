using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Utils.Validation
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true; // Allow null values, use [Required] for mandatory fields

            var phoneNumber = value.ToString();
            if (string.IsNullOrWhiteSpace(phoneNumber)) return true;

            // Indonesian phone number pattern
            var pattern = @"^(\+62|62|0)8[1-9][0-9]{6,9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a valid Indonesian phone number (e.g., +628123456789, 08123456789)";
        }
    }

    public class PasswordStrengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            var password = value.ToString();
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Password must be at least 8 characters, contain uppercase, lowercase, digit, and special character
            var hasMinLength = password.Length >= 8;
            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);
            var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasMinLength && hasUpper && hasLower && hasDigit && hasSpecial;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be at least 8 characters long and contain uppercase, lowercase, digit, and special character";
        }
    }

    public class PriceRangeAttribute : ValidationAttribute
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;

        public PriceRangeAttribute(double minPrice = 0, double maxPrice = 1000000)
        {
            _minPrice = (decimal)minPrice;
            _maxPrice = (decimal)maxPrice;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (decimal.TryParse(value.ToString(), out decimal price))
            {
                return price >= _minPrice && price <= _maxPrice;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between {_minPrice:C} and {_maxPrice:C}";
        }
    }

    public class StockQuantityAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (int.TryParse(value.ToString(), out int quantity))
            {
                return quantity >= 0 && quantity <= 10000;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between 0 and 10,000";
        }
    }

    public class EmailDomainAttribute : ValidationAttribute
    {
        private readonly string[] _allowedDomains;

        public EmailDomainAttribute(params string[] allowedDomains)
        {
            _allowedDomains = allowedDomains;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            var email = value.ToString();
            if (string.IsNullOrWhiteSpace(email)) return true;

            if (_allowedDomains == null || _allowedDomains.Length == 0) return true;

            var domain = email.Split('@').LastOrDefault();
            return _allowedDomains.Contains(domain, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be from allowed domains: {string.Join(", ", _allowedDomains)}";
        }
    }
}
