using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Implements the opeartions for managing user accounts in the e-commerce system.
/// </summary>
public class UserService : IUserService
{
    private readonly EcommerceContext ecommerceContext;

    public UserService(EcommerceContext ecommerceContext)
    {
        this.ecommerceContext = ecommerceContext;
    }


    /// <summary>
    /// Registers a new user to the database with the provided details.
    /// </summary>
    public async Task<UserResponse> RegisterUser(UserRegistrationDTO dto)
    {
        UserValidation.ValidateRegistration(dto);

        if (await ecommerceContext.Users.AnyAsync(u => u.Email == dto.Email))
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

        await ecommerceContext.Users.AddAsync(user);
        await ecommerceContext.SaveChangesAsync();

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }


    /// <summary>
    /// Authenticates a user with the provided login credentials.
    /// </summary>
    public async Task<UserResponse> LoginUser(UserLoginDTO dto)
    {
        UserValidation.ValidateLogin(dto);

        var user = await ecommerceContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
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


    /// <summary>
    /// Logs out the current user. The caller is responsible for setting currentUserId = null.
    /// </summary>
    public void LogoutUser(Guid? currentUserId)
    {
        UserValidation.CheckForValidUser(currentUserId);
        Console.WriteLine($"Logging out user with ID: {currentUserId}.");
    }


    /// <summary>
    /// Retrieves user details by their unique identifier, typically for profile management, 
    /// authorization, or UI personalization.
    /// </summary>
    public async Task<UserResponse> GetUser(Guid userId)
    {
        UserValidation.CheckForValidUser(userId);
        var user = await ecommerceContext.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }


    /// <summary>
    /// Saves a user's address to the database. Enforces a one-to-one relationship, ensuring 
    /// the user can only have one address. Throws an exception if an address already exists.
    /// </summary>
    public async Task<AddressResponse> SaveUserAddress(RegisterAddressDTO dto)
    {
        UserValidation.CheckForValidUser(dto.UserId);

        // Check if the user already has an address
        var existingAddress = await ecommerceContext.Addresses
            .FirstOrDefaultAsync(a => a.UserId == dto.UserId);

        if (existingAddress != null)
        {
            throw new InvalidOperationException("User already has an address. Please update the existing address.");
        }

        // Create the new address
        var address = new Address
        {
            AddressId = Guid.NewGuid(),
            UserId = dto.UserId,
            Street = dto.Street,
            City = dto.City,
            Region = dto.Region,
            PostalCode = dto.PostalCode,
            Country = dto.Country
        };

        // Save to the database
        await ecommerceContext.Addresses.AddAsync(address);
        await ecommerceContext.SaveChangesAsync();

        // Return the response
        return new AddressResponse
        {
            AddressId = address.AddressId,
            UserId = address.UserId ?? throw new InvalidOperationException("Address UserId cannot be null."),
            Street = address.Street,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }


    /// <summary>
    /// Retrieves a user's address by their unique identifier. 
    /// Throws an exception if no address is found.
    /// </summary>
    public async Task<AddressResponse> GetUserAddress(Guid userId)
    {
        UserValidation.CheckForValidUser(userId);

        var address = await ecommerceContext.Addresses
            .FirstOrDefaultAsync(a => a.UserId == userId);

        if (address == null)
        {
            throw new InvalidOperationException("Address for the specified user not found.");
        }

        return new AddressResponse
        {
            UserId = address.UserId ?? throw new InvalidOperationException("Address UserId cannot be null."),
            Street = address.Street,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            Country = address.Country
        };

    }







    #region Helper Methods 

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