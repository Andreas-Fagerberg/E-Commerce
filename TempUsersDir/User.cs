using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(75)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(75)]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string? PasswordHash { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public ICollection<Address>? Addresses { get; set; }

    // FK: One-to-Many (one user can have many orders)
    // public ICollection<Order> Orders { get; set; } // Can also be List<T>.

    // FK: One-to-One (one user can have one cart)
    // public Cart Cart { get; set; } -- waiting for Cart.cs

    // Move this to dbContext file: public DbSet<User> Users { get; set; }
}