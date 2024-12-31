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
        try
        {
            builder.UseNpgsql(
                "Host=localhost;Database=ECommerce;Username=postgres;Password=password"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the database: {ex.Message}");
            throw new InvalidOperationException(
                "Failed to connect to database, try again later.",
                ex
            );
        }
    }

    //Om man vill konfigurera modellerna lite extra: fler constraints exempelvis, då kan man använda 'OnModelCreating'.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // User configuration
        builder
            .Entity<User>()
            .Property(User => User.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Product configuration
        builder.Entity<Product>().Property(Product => Product.Id).UseIdentityColumn();

        builder.Entity<Product>().Property(Product => Product.Name).IsRequired().HasMaxLength(100);

        builder
            .Entity<Product>()
            .Property(Product => Product.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Product>().Property(Product => Product.Description).HasMaxLength(1000);

        builder
            .Entity<Product>()
            .Property(Product => Product.Price)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasDefaultValue(0.00m);

        builder
            .Entity<Product>()
            .Property(Product => Product.Rating)
            .IsRequired()
            .HasDefaultValue(0);

        builder
            .Entity<Product>()
            .Property(Product => Product.available)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationship and Index need their own Entity<Product>()
        builder
            .Entity<Product>()
            .HasOne(Product => Product.user)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Product>().HasIndex(Product => Product.Name);
        builder.Entity<Product>().HasIndex(Product => Product.Category);
    }
}
