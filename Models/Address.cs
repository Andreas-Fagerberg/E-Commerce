namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a physical address associated with a user.
/// </summary>
public class Address
{
    public Guid AddressId { get; set; }
    public Guid? UserId { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }

    public User? User { get; set; }
}

