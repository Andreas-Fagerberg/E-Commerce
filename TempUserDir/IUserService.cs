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
    Task<AddressResponse> GetUserAddress(Guid userId);
    Task<AddressResponse> UpdateUserAddress(RegisterAddressDTO dto);
    Task<IEnumerable<UserResponse>> GetAllUsers(); // Admin function, gets all Users.
    Task UpdateUserRole(UpdateUserRole dto); // Admin function, update the User role.
    Task<IEnumerable<UserResponse>> SearchUsers(AdminUserSearchDTO dto); // Admin function, search for a specific User.
}
