using EcommerceAPI.dto;

namespace EcommerceAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> CompleteProfileAsyync(int id, CompleteProfileDto dto);
    }
}
