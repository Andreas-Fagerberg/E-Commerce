using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a user in the e-commerce system.
/// </summary>
public class User
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required Role Role { get; set; } = Role.User;
    public DateTime CreatedAt { get; set; }

    // Navigation properties (WIP)
    public Address? Address { get; set; }
    public ICollection<Order> Orders = [];
    // ShoppingCart
    // More..?

}

