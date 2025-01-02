using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class EcommerceContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    // public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Host=localhost;Database=ECommerce;Username=postgres;Password=password");
    }

    // Om man vill konfigurera modellerna med constraints:
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(user =>
        {
            user.HasKey(u => u.UserId);

            user.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(75);

            user.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(75);

            user.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            user.HasIndex(u => u.Email).IsUnique();

            user.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);

            user.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(0) with time zone");

            // Relationship configuration
            user.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Address>(address =>
        {
            address.HasKey(a => a.AddressId);

            address.Property(a => a.UserId)
                .IsRequired(false);

            address.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);

            address.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.Region)
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(25);

            address.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.Entity<Order>(order =>
        {
            order.HasKey(o => o.OrderId);

            order.HasIndex(o => o.UserId);

            order.Property(o => o.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(0) with time zone");

            order.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValueSql("Pending");

            order.Property(o => o.TotalCost)
                .IsRequired()
                .HasColumnType("decimal(10, 2)");

            // Relationship configuration
            order.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        builder.Entity<OrderProduct>(orderProduct =>
        {
            orderProduct.HasKey(op => new { op.OrderId, op.ProductId });

            orderProduct.Property(op => op.Quantity)
                        .IsRequired();

            // Relationship with Order
            orderProduct.HasOne(op => op.Order)
                        .WithMany(o => o.OrderProducts)
                        .HasForeignKey(op => op.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Product
            orderProduct.HasOne(op => op.Product)
                        .WithMany()
                        .HasForeignKey(op => op.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);
        });

    }
}
