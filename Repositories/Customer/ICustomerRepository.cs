using EcommerceAPI.Models;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}
