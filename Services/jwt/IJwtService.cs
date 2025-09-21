using EcommerceAPI.Models; // Added missing using statement for User model

namespace EcommerceAPI.Services.JWT // Added missing namespace declaration
{
    public interface IJwtService
    {
        string GenerateToken(User userDt);
    }
}
