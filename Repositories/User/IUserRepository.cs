using EcommerceAPI.Models;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUserNameAsync(string userName);
    // Task AddAsync(User user);
}