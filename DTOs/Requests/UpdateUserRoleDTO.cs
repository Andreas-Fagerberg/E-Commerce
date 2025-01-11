namespace E_commerce_Databaser_i_ett_sammanhang;

public class UpdateUserRoleDTO
{
    public required Guid UserId { get; set; }
    public required Role Role { get; set; }
}