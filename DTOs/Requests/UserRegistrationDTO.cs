namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a data transfer object (DTO) for user registration details.
/// </summary>
public class UserRegistrationDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}