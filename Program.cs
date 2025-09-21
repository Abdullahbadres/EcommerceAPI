using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using EcommerceAPI.Services.Validation;
using EcommerceAPI.Services.Auth;
using EcommerceAPI.Services.Product;
using EcommerceAPI.Services.Cart;
using EcommerceAPI.Services.Order;
using EcommerceAPI.Services.Payment;
using EcommerceAPI.Services.JWT;
// using EcommerceAPI.Services.Redis;
// using EcommerceAPI.Repositories.Product;
// using EcommerceAPI.Repositories.User;
// using EcommerceAPI.Repositories.Customer;
// using EcommerceAPI.Repositories.Category;
// using EcommerceAPI.Repositories.Cart;
// using EcommerceAPI.Repositories.Order;
// using EcommerceAPI.Repositories.OrderItem;
// using EcommerceAPI.Repositories.Address;
// using EcommerceAPI.Repositories.Payment;
// using EcommerceAPI.Repositories.Shipment;
using EcommerceAPI.Models;
using EcommerceAPI.Configurations;
using EcommerceAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ambil config dari appsettings.json
var redisConnection = builder.Configuration.GetConnectionString("Redis")
?? throw new InvalidCastException("Redis connsction is missing from configuration");

//register redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(redisConnection)
);

//Add DBContext
builder.Services.AddDbContext<ApplicationDBContext>(option =>
option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"))
    .LogTo(Console.WriteLine, LogLevel.Information)
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "E - Commerce Service API",
        Version = "v1",
        Description = "API for E-Commerce platform"
    });

    //add auth header
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Masukan JWT token dengan format: Bearer {token}"

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers();

//Jwt Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)
            )
        };
    });

//Custom service Registration Depedency Injection (DI)
// builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRedisService, RedisService>();

//add repository Depedency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

//register unity of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Configure CORS 
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",
    builder =>
    {
        builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
    });
});

var app = builder.Build();

//exception handling middleware registration
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
