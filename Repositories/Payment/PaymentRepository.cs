using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDBContext _db;

    public PaymentRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await _db.Payments.AddAsync(payment);
        return payment;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _db.Payments
            .Include(p => p.Order)
            .ThenInclude(o => o.Customer)
            .ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        return await _db.Payments
            .Include(p => p.Order)
            .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(p => p.PaymentID == id);
    }

    public async Task<IEnumerable<Payment>> GetByOrderIdAsync(int orderId)
    {
        return await _db.Payments
            .Where(p => p.OrderID == orderId)
            .ToListAsync();
    }

    public async Task<Payment?> GetByTransactionIdAsync(string transactionId)
    {
        return await _db.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.TransactionID == transactionId);
    }

    public async Task UpdateAsync(Payment payment)
    {
        _db.Payments.Update(payment);
    }

    public async Task DeleteAsync(Payment payment)
    {
        _db.Payments.Remove(payment);
    }
}
