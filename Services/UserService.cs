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
            Role = Role.User,
            CreatedAt = DateTime.UtcNow,
        };

        await ecommerceContext.Users.AddAsync(user);
        await ecommerceContext.SaveChangesAsync();

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
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
            Email = user.Email,
            Role = user.Role,
        };
    }

    /// <summary>
    /// Logs out the current user. The caller is responsible for setting currentUserId = null.
    /// </summary>
    public void LogoutUser()
    {
        if (SessionHandler.CurrentUserId == null)
        {
            throw new InvalidOperationException("No user is currently logged in");
        }

        SessionHandler.ClearSession();
    }

    /// <summary>
    /// Retrieves user details by their unique identifier, typically for profile management,
    /// authorization, or UI personalization.
    /// </summary>
    public async Task<UserResponse> GetUser(Guid userId)
    {
        UserValidation.CheckForValidUser(userId);
        var user = await ecommerceContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return new UserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
        };
    }

    /// <summary>
    /// Saves a user's address to the database. Enforces a one-to-one relationship, ensuring
    /// the user can only have one address. Throws an exception if an address already exists.
    /// </summary>
    public async Task<AddressResponse> SaveUserAddress(RegisterAddressDTO dto)
    {
        UserValidation.CheckForValidUser(dto.UserId);

        var existingAddress = await ecommerceContext.Addresses.FirstOrDefaultAsync(a =>
            a.UserId == dto.UserId
        );

        if (existingAddress != null)
        {
            throw new InvalidOperationException(
                "User already has an address. Please update the existing address."
            );
        }

        var address = new Address
        {
            AddressId = Guid.NewGuid(),
            UserId = dto.UserId,
            Street = dto.Street,
            City = dto.City,
            Region = dto.Region,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
        };

        await ecommerceContext.Addresses.AddAsync(address);
        await ecommerceContext.SaveChangesAsync();

        return new AddressResponse
        {
            AddressId = address.AddressId,
            UserId =
                address.UserId
                ?? throw new InvalidOperationException("Address UserId cannot be null."),
            Street = address.Street,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            Country = address.Country,
        };
    }


    /// <summary>
    /// Retrieves a user's address by their unique identifier.
    /// Throws an exception if no address is found.
    /// </summary>
    public async Task<AddressResponse?> GetUserAddress(Guid userId)
    {
        UserValidation.CheckForValidUser(userId);

        var address = await ecommerceContext.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);

        if (address == null)
        {

            return null;
        }

        return new AddressResponse
        {
            UserId =
                address.UserId
                ?? throw new InvalidOperationException("Address UserId cannot be null."),
            Street = address.Street,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            Country = address.Country,
        };
    }


    /// <summary>
    ///  Updating an existing user's address in the database.
    /// </summary>
    public async Task<AddressResponse> UpdateUserAddress(RegisterAddressDTO dto)
    {
        UserValidation.CheckForValidUser(dto.UserId);
        AddressValidation.ValidateAddress(dto);

        var existingAddress = await ecommerceContext.Addresses.FirstOrDefaultAsync(a =>
            a.UserId == dto.UserId
        );

        if (existingAddress == null)
        {
            throw new InvalidOperationException(
                $"No existing address found for User: {dto.UserId}"
            );
        }

        existingAddress.Street = dto.Street;
        existingAddress.City = dto.City;
        existingAddress.Region = dto.Region;
        existingAddress.PostalCode = dto.PostalCode;
        existingAddress.Country = dto.Country;

        ecommerceContext.Addresses.Update(existingAddress);
        await ecommerceContext.SaveChangesAsync();

        return new AddressResponse
        {
            AddressId = existingAddress.AddressId,
            UserId = existingAddress.UserId
                ?? throw new InvalidOperationException("Address UserId cannot be null."),
            Street = existingAddress.Street,
            City = existingAddress.City,
            Region = existingAddress.Region,
            PostalCode = existingAddress.PostalCode,
            Country = existingAddress.Country,
        };
    }

    public async Task<bool> HasAddress(Guid userId)
    {
        return await ecommerceContext.Addresses.AnyAsync(a => a.UserId == userId);
    }

    #region Admin Methods


    /// <summary>
    /// Retrieves a list of all users in the system. This method is restricted to admin users only
    /// and validates the admin user's credentials and role before execution.
    /// </summary>
    public async Task<List<UserResponse>> GetAllUsers(Guid adminUserId)
    {
        await ValidateAdminUser(adminUserId);

        return await ecommerceContext
            .Users.Select(user => new UserResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
            })
            .ToListAsync();
    }


    /// <summary>
    /// Allows admin users to search for specific users based on criteria
    /// such as email or role. Further search-criteria can be added to this method
    /// but make sure to adjust the input method accordingly.
    /// </summary>
    public async Task<List<UserResponse>> SearchUsers(AdminUserSearchDTO dto, Guid adminUserId)
    {
        await ValidateAdminUser(adminUserId);

        var query = ecommerceContext.Users.AsQueryable();

        if (string.IsNullOrWhiteSpace(dto.Email) == false)
        {
            query = query.Where(u => u.Email.Contains(dto.Email));
        }

        if (dto.Role.HasValue)
        {
            query = query.Where(u => u.Role == dto.Role.Value);
        }

        var results = await query
            .Select(user => new UserResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
            })
            .ToListAsync();

        return results;
    }


    /// <summary>
    /// Updates the role of a specified user. Accessible only by admin users.
    /// </summary>
    public async Task UpdateUserRole(UpdateUserRoleDTO dto, Guid adminUserId)
    {
        await ValidateAdminUser(adminUserId);

        var targetUser = await ecommerceContext.Users.FindAsync(dto.UserId);

        if (targetUser == null)
        {
            throw new InvalidOperationException("Target user not found.");
        }

        if (targetUser.Role == dto.Role)
        {
            throw new InvalidOperationException(
                $"The target user already has the specified role '{dto.Role}'"
            );
        }

        targetUser.Role = dto.Role;

        await ecommerceContext.SaveChangesAsync();
    }

    #endregion



    #region Helper Methods

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }


    /// <summary>
    /// This ensures that the adminUserId belongs to an admin user in the system.
    /// If the user ID is invalid or doesn't belong to an admin, an exception is thrown.
    /// /// </summary>
    public async Task<User> ValidateAdminUser(Guid userId)
    {
        UserValidation.CheckForValidUser(userId);

        var user = await ecommerceContext.Users.FindAsync(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        UserValidation.ValidateUserRole(user.Role, Role.Admin);

        return user;
    }

    #endregion
}
