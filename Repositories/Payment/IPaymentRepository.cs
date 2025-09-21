using EcommerceAPI.Models;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int id);
    Task<IEnumerable<Payment>> GetByOrderIdAsync(int orderId);
    Task<Payment?> GetByTransactionIdAsync(string transactionId);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Payment payment);
}
