namespace E_commerce_Databaser_i_ett_sammanhang;

public static class UserValidation
{
    // Validates that a user is currently logged in. 
    // Should be called in any method that requires a logged-in user to
    // ensure proper authorization.
    // Throws an exception if no user is logged in.
    public static void CheckForValidUser(Guid? currentUserId)
    {
        if (currentUserId == null)
        {
            throw new InvalidOperationException("No user is logged in.Please login to proceed.");
        }
    }

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
    }

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
}
