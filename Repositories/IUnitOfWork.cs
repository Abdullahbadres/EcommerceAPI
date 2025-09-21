public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ICustomerRepository Customers { get; }
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    ICartRepository Carts { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IAddressRepository Addresses { get; }
    IPaymentRepository Payments { get; }
    IShipmentRepository Shipments { get; }

    Task<int> SaveChangesAsync();
}
