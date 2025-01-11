namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a data transfer object (DTO) for user login credentials.
/// </summary>
public class UserLoginDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}