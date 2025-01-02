namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a data transfer object (DTO) containing address details for responses.
/// </summary>
public class AddressResponse
{
    public Guid AddressId { get; set; }
    public Guid UserId { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
}