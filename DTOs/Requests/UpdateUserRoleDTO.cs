namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents the data required to update a user's role.
/// </summary>
public class UpdateUserRoleDTO
{
    public required Guid UserId { get; set; }
    public required Role Role { get; set; }
}