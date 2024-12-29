namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Defines the operations for managing user accounts in the e-commerce system.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user with the provided details.
    /// </summary>
    Task<UserResponse> RegisterUser(UserRegistrationDTO dto);

    /// <summary>
    /// Authenticates a user with the provided login credentials.
    /// </summary>
    Task<UserResponse> LoginUser(UserLoginDTO dto);

    /// <summary>
    /// Logs out the current user. The caller is responsible for setting currentUserId = null.
    /// </summary>
    void LogoutUser(Guid? currentUserId);

    /// <summary>
    /// Retrieves user details by their unique identifier, typically for profile management, 
    /// authorization, or UI personalization.
    /// </summary>
    Task<UserResponse> GetUserById(Guid userId);
}
