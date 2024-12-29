using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a user in the e-commerce system.
/// </summary>
public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(75)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(75)]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public required string PasswordHash { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    // public ICollection<Address>? Addresses { get; set; } 
    // FK: One-to-Many (one user can have many orders)
    // public ICollection<Order>? Orders { get; set; } 

    // FK: One-to-One (one user can have one cart)
    // public ShoppingCart? Cart { get; set; } 
}
