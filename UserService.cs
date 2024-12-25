using System.Security.Cryptography;
using BCrypt.Net;

namespace E_commerce_Databaser_i_ett_sammanhang;

// POTENTIAL TODO: 
// Move validation logic to a separate layer
// ASync operations.

public class UserService
{
    private readonly EcommerceContext _ecommerceContext;


    public UserService(EcommerceContext ecommerceContext)
    {
        _ecommerceContext = ecommerceContext;
    }


    public UserResponse RegisterUser(UserRegistrationDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
        {
            throw new ArgumentException("First name and last name are required.");
        }
        if (IsValidEmail(dto.Email) == false)
        {
            throw new ArgumentException("Invalid email address.");
        }

        if (dto.Password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters.");
        }

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


    public bool LoginUser(UserLoginDTO dto) // Email & Password
    {
        if (IsValidEmail(dto.Email) == false)
        {
            throw new ArgumentException("The email address is invalid.");
        }

        if (string.IsNullOrEmpty(dto.Password) || dto.Password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters.");
        }

        var user = _ecommerceContext.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        if (!VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new ArgumentException("Invalid passsword.");
        }

        return true; // Successful login
    }




    #region Helper Methods

    private void ValidateUserRegistration(UserRegistrationDTO)
    {
        throw new NotImplementedException("TODO!");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mail = new System.Net.Mail.MailAddress(email);
            return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // To be used when logging in as an existing user. 
    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }



    #endregion



}