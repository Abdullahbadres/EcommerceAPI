using EcommerceAPI.Configurations;

using EcommerceAPI.Models; // Ensure this is the correct namespace for Address
using Microsoft.EntityFrameworkCore;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDBContext _db;

    public AddressRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Address> AddAsync(Address address)
    {
        await _db.Addresses.AddAsync(address);
        return address;
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _db.Addresses
            .Include(a => a.Customer)
            .ToListAsync();
    }

    public async Task<Address?> GetByIdAsync(int id)
    {
        return await _db.Addresses
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.AddressID == id);
    }

    public async Task<IEnumerable<Address>> GetByCustomerIdAsync(int customerId)
    {
        return await _db.Addresses
            .Where(a => a.CustomerID == customerId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Address address)
    {
        _db.Addresses.Update(address);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Address address)
    {
        _db.Addresses.Remove(address);
        await _db.SaveChangesAsync();
    }
}
