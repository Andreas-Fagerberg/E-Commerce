using BCrypt.Net;

namespace E_commerce_Databaser_i_ett_sammanhang;


public class UserService
{


    // Will likely need to inject AppDbContext/_dbContext into constructor
    public UserService()
    {

    }


    // Method to register the user
    public UserResponse RegisterUser(UserRegistrationDTO dto)
    {
        // dto : FirstName, LastName, Email, Password
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

        // Check for duplicate email
        /* if (_dbContext.Users.Any(u => u.Email == dto.Email))
        {
            throw new InvalidOperationException("A user with that email already exists.");

        }*/

        // Hash Password with BCrypt
        string hashedPassword = HashPassword(dto.Password);

        // Create the user entity
        var user = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow
        };

        // Save to database
        //_dbContext.Users.Add(user);
        //_dbContext.SaveChanges();

        // Return the response (can be used in e.g. UI personalisation)
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
        // Should these be made into helper methods? Multiple references.
        if (IsValidEmail(dto.Email) == false)
        {
            throw new ArgumentException("The email address is invalid.");
        }

        if (string.IsNullOrEmpty(dto.Password))
        {
            throw new ArgumentException("Password must be at least 8 characters.");
        }

        // Query the database
        /* var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (u.Email == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        */

        if (!VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new ArgumentException("Invalid passsword.");
        }
    }




    #region Helper Methods

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


    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // To be used when logging in as an existing user. 
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }



    #endregion



}