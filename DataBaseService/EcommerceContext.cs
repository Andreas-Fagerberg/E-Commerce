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
            builder.UseNpgsql("Host=localhost;Database=ECommerce;Username=postgres;Password=password");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException
            (
                "Failed to connect to database, try again later.",ex
            );
        }
    }

    //Om man vill konfigurera modellerna lite extra: fler constraints exempelvis, då kan man använda 'OnModelCreating'.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>(product =>
        {
            product.Property(p => p.Id)
                .UseIdentityColumn();

            product.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            product.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(30);
            
            product.Property(p => p.Description)
                .HasMaxLength(50);
            
            product.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasDefaultValue(0.00m);
            
            product.Property(p => p.Rating)
                .IsRequired()
                .HasDefaultValue(0);
            
            product.Property(p => p.available)
                .IsRequired()
                .HasDefaultValue(true);
            
            product.HasOne(p => p.user)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            product.HasIndex(p => p.Name);
            
            product.HasIndex(p => p.Category);
        });
    }
}
