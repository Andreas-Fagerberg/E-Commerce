using System.Security.Cryptography;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class UserService
{
    private readonly EcommerceContext _ecommerceContext;


    public UserService(EcommerceContext ecommerceContext)
    {
        _ecommerceContext = ecommerceContext;
    }


    public async Task<UserResponse> RegisterUser(UserRegistrationDTO dto)
    {
        UserValidation.ValidateRegistration(dto);

        if (await _ecommerceContext.Users.AnyAsync(u => u.Email == dto.Email))
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

        await _ecommerceContext.Users.AddAsync(user);
        await _ecommerceContext.SaveChangesAsync();

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }


    public async Task<UserResponse> LoginUser(UserLoginDTO dto)
    {
        UserValidation.ValidateLogin(dto);

        var user = await _ecommerceContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
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