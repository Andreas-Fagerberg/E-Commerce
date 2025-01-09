
namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a data transfer object (DTO) containing user details for responses.
/// </summary>
public class UserResponse
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required Role Role { get; set; }
}