namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Defines the operations for managing user accounts in the e-commerce system.
/// </summary>
public interface IUserService
{
    Task<UserResponse> RegisterUser(UserRegistrationDTO dto);
    Task<UserResponse> LoginUser(UserLoginDTO dto);
    void LogoutUser();
    Task<UserResponse> GetUser(Guid userId);
    Task<AddressResponse> SaveUserAddress(RegisterAddressDTO dto);
    Task<AddressResponse?> GetUserAddress(Guid userId);
    Task<AddressResponse> UpdateUserAddress(RegisterAddressDTO dto);
    Task<bool> HasAddress(Guid userId);
    Task<List<UserResponse>> GetAllUsers(Guid adminUserId);
    Task<List<UserResponse>> SearchUsers(AdminUserSearchDTO dto, Guid adminUserId);
    Task UpdateUserRole(UpdateUserRoleDTO dto, Guid adminUserId);
    Task<User> ValidateAdminUser(Guid userId);
}
