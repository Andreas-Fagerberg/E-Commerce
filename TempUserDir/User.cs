using System.ComponentModel.DataAnnotations;
using E_commerce_Databaser_i_ett_sammanhang.Models;

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
    public DateTime CreatedAt { get; set; }

    // Navigation properties (WIP)
    public Address? Address { get; set; }
    public ICollection<Order> Orders = [];

    public ShoppingCart Cart {get; set;}
    // ShoppingCart
    // More..?

}

