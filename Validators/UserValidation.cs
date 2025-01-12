namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Provides methods for validating user-related data and ensuring proper authorization.
/// </summary>
public static class UserValidation
{

    /// <summary>
    /// Validates that a user is currently logged in by checking their unique identifier.
    /// This method should be called in any operations or commands that require user authorization.
    /// </summary>
    public static void CheckForValidUser(Guid? currentUserId)
    {
        if (currentUserId == null || currentUserId == Guid.Empty)
        {
            throw new InvalidOperationException("No user is logged in. Please login to proceed.");
        }
    }

    /// <summary>
    ///  Validates that a user possesses the required privileges by checking their
    ///  role. Should be called in any operation or command that require
    ///  admin status.
    /// </summary>
    public static void ValidateUserRole(Role userRole, Role requiredRole)
    {
        if (userRole != requiredRole)
        {
            throw new UnauthorizedAccessException($"Action requires {userRole} privileges");
        }
    }

    /// <summary>
    /// Validates user registration data, including names, email, and password.
    /// </summary>
    public static void ValidateRegistration(UserRegistrationDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
        {
            throw new ArgumentException("First name and last name are required.");
        }

        if (!IsValidEmail(dto.Email))
        {
            throw new ArgumentException("Invalid email format.");
        }

        if (dto.Password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters.");
        }
    }


    /// <summary>
    /// Validates user login credentials, including email format and password requirements.
    /// </summary>
    public static void ValidateLogin(UserLoginDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new ArgumentException("Email is required.");
        }

        if (!IsValidEmail(dto.Email))
        {
            throw new ArgumentException("Invalid email format.");
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new ArgumentException("Password is required.");
        }

        ValidatePassword(dto.Password);
    }


    /// <summary>
    /// Validates a password, ensuring it meets length and non-empty requirements.
    /// </summary>
    public static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password is required.");
        }

        if (password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters.");
        }
    }


    /// <summary>
    /// Checks whether an email address is in a valid format.
    /// </summary>
    public static bool IsValidEmail(string email)
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


    /// <summary>
    /// Checks whether a name contains only letters, spaces, or hyphens.
    /// </summary>
    public static bool IsValidName(string inputName)
    {
        if (string.IsNullOrWhiteSpace(inputName))
        {
            return false;
        }
        // Regex pattern: only letters, spaces and hyphens are allowed.
        var validPattern = @"[a-zA-Z\s\-]+$";
        return System.Text.RegularExpressions.Regex.IsMatch(inputName, validPattern);
    }

    /// <summary>
    /// Checks if the user is an Admin or a regular user.
    /// </summary>
    public static bool CheckIfAdmin(UserResponse loggedInUser)
    {
        return loggedInUser.Role == Role.Admin;
    }


}
