using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using EcommerceAPI.Configurations;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _db;
    public UserRepository(ApplicationDBContext db) => _db = db;

    public async Task AddAsync(User user)
            => await _db.Users.AddAsync(user);

    public Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        return Task.CompletedTask;

    }

    public async Task<User?> GetByIdAsync(int id)
    {
            return await _db.Users.Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.UserID == id);
    }


    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _db.Users.Include(u => u.Customer)
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }



}