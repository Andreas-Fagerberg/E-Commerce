using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class EcommerceContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ProductOrders> ProductOrders { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Host=localhost;Database=ecommerce;Username=postgres;Password=password");
    }

    //Om man vill konfigurera modellerna lite extra: fler constraints exempelvis, då kan man använda 'OnModelCreating'.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<User>()
            .Property(User => User.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
