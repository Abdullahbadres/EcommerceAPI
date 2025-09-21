using EcommerceAPI.Configurations;
// using EcommerceAPI.Repositories.User;
// using EcommerceAPI.Repositories.Customer;
// using EcommerceAPI.Repositories.Product;
// using EcommerceAPI.Repositories.Category;
// using EcommerceAPI.Repositories.Cart;
// using EcommerceAPI.Repositories.Order;
// using EcommerceAPI.Repositories.OrderItem;
// using EcommerceAPI.Repositories.Address;
// using EcommerceAPI.Repositories.Payment;
// using EcommerceAPI.Repositories.Shipment;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _db;
    public IUserRepository Users { get; set; }
    public ICustomerRepository Customers { get; set; }
    public IProductRepository Products { get; set; }
    public ICategoryRepository Categories { get; set; }
    public ICartRepository Carts { get; set; }
    public IOrderRepository Orders { get; set; }
    public IOrderItemRepository OrderItems { get; set; }
    public IAddressRepository Addresses { get; set; }
    public IPaymentRepository Payments { get; set; }
    public IShipmentRepository Shipments { get; set; }

    public UnitOfWork(
        ApplicationDBContext db,
        IUserRepository users,
        ICustomerRepository customers,
        IProductRepository products,
        ICategoryRepository categories,
        ICartRepository carts,
        IOrderRepository orders,
        IOrderItemRepository orderItems,
        IAddressRepository addresses,
        IPaymentRepository payments,
        IShipmentRepository shipments)
    {
        _db = db;
        Users = users;
        Customers = customers;
        Products = products;
        Categories = categories;
        Carts = carts;
        Orders = orders;
        OrderItems = orderItems;
        Addresses = addresses;
        Payments = payments;
        Shipments = shipments;
    }

    public async Task<int> SaveChangesAsync()
            => await _db.SaveChangesAsync();
}
