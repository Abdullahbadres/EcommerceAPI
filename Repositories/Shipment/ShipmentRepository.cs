using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ShipmentRepository : IShipmentRepository
{
    private readonly ApplicationDBContext _db;

    public ShipmentRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Shipment> AddAsync(Shipment shipment)
    {
        await _db.Shipments.AddAsync(shipment);
        await _db.SaveChangesAsync();  // Ensure changes are persisted asynchronously
        return shipment;
    }

    public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await _db.Shipments
            .Include(s => s.Order)
            .ThenInclude(o => o.Customer)
            .Select(s => new Shipment
            {
                // Explicitly check for null for Order and Customer in the Select
                Order = s.Order != null ? s.Order : new Order(),
                Customer = s.Order != null && s.Order.Customer != null ? s.Order.Customer : new Customer(),
            })
            .ToListAsync();
    }

    public async Task<Shipment?> GetByIdAsync(int id)
    {
        var shipment = await _db.Shipments
            .Include(s => s.Order)
            .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(s => s.ShipmentID == id);

        return shipment;  // Return null if shipment is not found
    }

    public async Task<IEnumerable<Shipment>> GetByOrderIdAsync(int orderId)
    {
        return await _db.Shipments
            .Where(s => s.OrderID == orderId)
            .ToListAsync();
    }

    public async Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber)
    {
        return await _db.Shipments
            .Include(s => s.Order)
            .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
    }

    public async Task UpdateAsync(Shipment shipment)
    {
        _db.Shipments.Update(shipment);
        await _db.SaveChangesAsync();  // Ensure changes are persisted asynchronously
    }

    public async Task DeleteAsync(Shipment shipment)
    {
        _db.Shipments.Remove(shipment);
        await _db.SaveChangesAsync();  // Ensure changes are persisted asynchronously
    }
}
