using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class EcommerceContext : DbContext
{
    public DbSet<User> Users { get; set; }
    // public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    // public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    // public DbSet<ProductOrders> ProductOrders { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        try
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json")
            .Build();

            string? connectionString = config.GetConnectionString("Default");
            builder.UseNpgsql(connectionString);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to connect to database, try again later.", ex);
        }
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

            product.Property(p => p.Available)
                .IsRequired()
                .HasDefaultValue(true);

            product.HasIndex(p => p.Name);

            product.HasIndex(p => p.Category);
        });


    }
}
