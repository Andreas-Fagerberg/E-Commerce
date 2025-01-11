namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Defines the operations for managing user accounts in the e-commerce system.
/// </summary>
public interface IUserService
{
    Task<UserResponse> RegisterUser(UserRegistrationDTO dto);
    Task<UserResponse> LoginUser(UserLoginDTO dto);
    void LogoutUser(Guid? currentUserId);
    Task<UserResponse> GetUser(Guid userId);
    Task<AddressResponse> SaveUserAddress(RegisterAddressDTO dto);
    Task<AddressResponse> GetUserAddress(Guid? userId);
    Task<AddressResponse> UpdateUserAddress(RegisterAddressDTO dto);
    Task<bool> HasAddress(Guid? userId);
    Task<List<UserResponse>> GetAllUsers(Guid? adminUserId); // Admin function, gets all Users.
    Task<List<UserResponse>> SearchUsers(AdminUserSearchDTO dto, Guid? adminUserId); // Admin function, search for a specific User.
    Task UpdateUserRole(UpdateUserRoleDTO dto, Guid? adminUserId); // Admin function, update the User role.
    Task<User> ValidateAdminUser(Guid? userId); // Validation method that includes a database operation.
    Task<bool> CheckAdminPriviliges(Guid? userId);
}