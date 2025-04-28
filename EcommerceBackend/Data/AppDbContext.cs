using Microsoft.EntityFrameworkCore;
using Models;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }
    public DbSet<Users> Users {get;set;}
    public DbSet<Category> Categories {get;set;}
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders {get;set;}
    public DbSet<OrderDetail> OrderDetails {get;set;}
    public DbSet<CartItem> CartItems {get;set;}
    public DbSet<Review> Reviews {get;set;}
    public DbSet<Payment> Payments {get;set;}
}