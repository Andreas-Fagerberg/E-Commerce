namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a data transfer object (DTO) for user address credentials.
/// </summary>
public class RegisterAddressDTO
{
    public required Guid UserId { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
}