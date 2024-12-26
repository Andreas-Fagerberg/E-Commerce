using System.Security.Cryptography;
using BCrypt.Net;

namespace E_commerce_Databaser_i_ett_sammanhang;

// POTENTIAL TODO: 
// [X] - Move validation logic to a separate layer
// [ ] - UpdateUserDetails, DeleteAccount
// [ ] -ASync operations
// [ ] -Improve error messages

public class UserService
{
    private readonly EcommerceContext _ecommerceContext;


    public UserService(EcommerceContext ecommerceContext)
    {
        _ecommerceContext = ecommerceContext;
    }


    public UserResponse RegisterUser(UserRegistrationDTO dto)
    {
        UserValidation.ValidateRegistration(dto);

        if (_ecommerceContext.Users.Any(u => u.Email == dto.Email))
        {
            throw new InvalidOperationException("A user with that email already exists.");
        }

        string hashedPassword = HashPassword(dto.Password);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow
        };

        _ecommerceContext.Users.Add(user);
        _ecommerceContext.SaveChanges();

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }


    public UserResponse LoginUser(UserLoginDTO dto)
    {
        UserValidation.ValidateLogin(dto);

        var user = _ecommerceContext.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (user == null)
        {
            throw new InvalidOperationException("Invalid email or password");
        }

        if (!VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new ArgumentException("Invalid email or passsword.");
        }

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }


    public Guid? LogoutUser(Guid? currentUserId)
    {
        UserValidation.CheckForValidUser(currentUserId);
        Console.WriteLine($"Logging out user with ID: {currentUserId}.");
        return null;
    }

    #region Utility Methods

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    #endregion
}