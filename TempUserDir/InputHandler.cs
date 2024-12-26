
namespace E_commerce_Databaser_i_ett_sammanhang;


/* 
Responsible for gathering raw user input and ensuring it meets minimal criteria.
More advanced validation is done in UserValidation.
Basic Responsibilities:
- Ensure Non-empty input
- Provide immediate feedback
- Validate basic format 
*/

public static class InputHandler
{


    // Method: Get Input for UserRegistration
    public static UserRegistrationDTO GetRegistrationInput()
    {
        Console.Write("Enter First Name: ");
        string firstname = ReadNonEmptyStrings("First name is required.");

        Console.Write("Enter Last Name: ");
        string lastname = ReadNonEmptyStrings("Last name is required.");

        Console.Write("Enter Email Address: ");
        string email = ReadAndValidateEmail();

        Console.Write("Enter Password: ");
        string password = ReadAndValidatePassword();

        return new UserRegistrationDTO
        {
            FirstName = firstname,
            LastName = lastname,
            Email = email,
            Password = password
        };
    }


    // Method: Get Input for UserLogin
    public static UserLoginDTO GetLoginInput()
    {
        Console.Write("Enter Email Address: ");
        string email = ReadAndValidateEmail();

        Console.Write("Enter Password: ");
        string password = ReadAndValidatePassword();

        return new UserLoginDTO
        {
            Email = email,
            Password = password
        };
    }





    #region Utility Methods

    private static string ReadNonEmptyStrings(string errorMessage)
    {
        while (true)
        {
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input) == false)
            {
                return input;
            }

            Console.WriteLine(errorMessage);
        }
    }


    private static string ReadAndValidateEmail()
    {
        while (true)
        {
            string email = ReadNonEmptyStrings("Email is required.");

            if (UserValidation.IsValidEmail(email) == true)
            {
                return email;
            }

            Console.WriteLine("Invalid email format. Please ensure it includes '@' and a domain, e.g., user@example.com.");
        }
    }

    private static string ReadAndValidatePassword()
    {
        while (true)
        {
            string password = ReadNonEmptyStrings("Password must be at least 8 characters long and include a number");

            try
            {
                UserValidation.ValidatePassword(password);
                return password;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Password Error {ex.Message}");
            }
        }
    }

    #endregion

}