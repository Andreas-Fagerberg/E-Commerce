namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a Data Transfer Object (DTO) for specifying criteria to search for users.
/// This class is designed to be flexible and can be extended to include additional search properties
/// as needed, such as filtering by registration date or other user attributes.
/// </summary>
public class AdminUserSearchDTO
{
    public string? Email { get; set; }
    public Role? Role { get; set; }
}