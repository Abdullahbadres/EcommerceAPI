using EcommerceAPI.Models;

public interface IShipmentRepository
{
    Task<Shipment> AddAsync(Shipment shipment);
    Task<IEnumerable<Shipment>> GetAllAsync();
    Task<Shipment?> GetByIdAsync(int id);
    Task<IEnumerable<Shipment>> GetByOrderIdAsync(int orderId);
    Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber);
    Task UpdateAsync(Shipment shipment);
    Task DeleteAsync(Shipment shipment);
}
