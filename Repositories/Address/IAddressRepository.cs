using EcommerceAPI.Models;

public interface IAddressRepository
{
    Task<Address> AddAsync(Address address);
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(int id);
    Task<IEnumerable<Address>> GetByCustomerIdAsync(int customerId);
    Task UpdateAsync(Address address);
    Task DeleteAsync(Address address);
}
