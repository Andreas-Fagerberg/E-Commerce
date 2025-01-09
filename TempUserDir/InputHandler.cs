
using System.Runtime.InteropServices;

namespace E_commerce_Databaser_i_ett_sammanhang;


/// <summary>
/// Handles user input by ensuring it meets basic criteria and providing immediate feedback.
/// Delegates advanced validation to the <see cref="UserValidation"/> class.
/// </summary>
public static class InputHandler
{

    /// <summary>
    /// Collects and performs basic validation on user input for registration details, including name, email, and password.
    /// </summary>
    public static UserRegistrationDTO GetRegistrationInput()
    {
        string firstname = ReadAndValidateName("First Name");

        string lastname = ReadAndValidateName("Last Name");

        string email = ReadAndValidateEmail();

        string password = ReadAndValidatePassword();

        return new UserRegistrationDTO
        {
            FirstName = firstname,
            LastName = lastname,
            Email = email,
            Password = password
        };
    }


    /// <summary>
    /// Collects and performs basic validation on user input for login details, including email and password.
    /// </summary>
    public static UserLoginDTO GetLoginInput()
    {
        string email = ReadAndValidateEmail();

        string password = ReadAndValidatePassword();

        return new UserLoginDTO
        {
            Email = email,
            Password = password
        };
    }

    /// <summary>
    /// Collects and performs basic validation on user input for address details.
    /// </summary>
    public static RegisterAddressDTO GetAddressInput(Guid currentUserId)
    {
        string street = ReadNonEmptyStrings("Street", "Street cannot be empty");
        string city = ReadNonEmptyStrings("City", "City cannot be empty");
        string region = ReadNonEmptyStrings("Region", "Region cannot be empty");
        string postalCode = ReadNonEmptyStrings("Postal Code", "Postal Code cannot be empty");
        string country = ReadNonEmptyStrings("Country", "Country cannot be empty");

        return new RegisterAddressDTO
        {
            UserId = currentUserId,
            Street = street,
            City = city,
            Region = region,
            PostalCode = postalCode,
            Country = country,
        };
    }

    /// <summary>
    /// Collects and validates input for searching users in the admin command.
    /// Allows filtering by email and/or role, with options to skip filters.
    /// </summary>
    public static AdminUserSearchDTO GetAdminSearchInput()
    {
        Console.WriteLine("Enter email to search (leave blank for no filter): ");
        var email = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(email) && !UserValidation.IsValidEmail(email))
        {
            Console.WriteLine("Invalid email format. Proceeding without email filter.");
            email = null;
        }

        Console.WriteLine(
            """
            Select role to filter by:
            [1] Admin
            [2] User
            [3] No Role Filter
            """);

        Role? role = null;

        var key = Console.ReadKey(true).Key;
        Console.WriteLine();

        switch (key)
        {
            case ConsoleKey.D1:
                role = Role.Admin;
                break;

            case ConsoleKey.D2:
                role = Role.User;
                break;

            case ConsoleKey.D3:
                role = null;
                break;

            default:
                Console.WriteLine("Invalid selection. Proceeding without a role filter.");
                break;
        }

        return new AdminUserSearchDTO { Email = email, Role = role };
    }


    /// <summary>
    /// Collects and validates input for updating a user's role.
    /// Ensures the provided User ID is in the correct format and allows
    /// the Admin to select a new role from predefined options.
    /// </summary>
    public static UpdateUserRoleDTO GetRoleUpdateInput()
    {
        Guid userId;

        while (true)
        {
            Console.WriteLine("Enter the User ID to update: ");
            var userIdInput = Console.ReadLine();
            if (Guid.TryParse(userIdInput, out userId)) break;
            Console.WriteLine("Invalid User ID format. Please try again.");
        }

        while (true)
        {
            Console.WriteLine(
                """
                Select the new role:
                [1] Admin
                [2] User
                """);

            Role role;
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    role = Role.Admin;
                    return new UpdateUserRoleDTO { UserId = userId, Role = role };


                case ConsoleKey.D2:
                    role = Role.User;
                    return new UpdateUserRoleDTO { UserId = userId, Role = role };

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }


    #region Helper Methods

    /// <summary>
    /// Reads user input and ensures it is non-empty, providing an error message if necessary.
    /// </summary>
    private static string ReadNonEmptyStrings(string fieldName, string errorMessage)
    {
        while (true)
        {
            Console.Write($"Enter {fieldName}: ");
            string input = Console.ReadLine()?.Trim()!;
            if (string.IsNullOrEmpty(input) == false)
            {
                return input;
            }

            Console.WriteLine(errorMessage); ;
        }
    }


    /// <summary>
    /// Reads and validates a name, ensuring it contains only valid characters.
    /// </summary>
    private static string ReadAndValidateName(string fieldName)
    {
        while (true)
        {

            string input = ReadNonEmptyStrings(fieldName, $"{fieldName} is required");

            if (UserValidation.IsValidName(input))
            {
                return input;
            }

            Console.WriteLine($"{fieldName} can only contain letters, spaces or hyphens.");
        }
    }


    /// <summary>
    /// Reads and validates an email address, providing feedback for invalid formats.
    /// </summary>
    private static string ReadAndValidateEmail()
    {
        while (true)
        {
            string email = ReadNonEmptyStrings("Email Address", "Email is required.").ToLower();

            if (UserValidation.IsValidEmail(email) == true)
            {
                return email;
            }

            Console.WriteLine("Invalid email format. Please ensure it includes '@' and a domain, e.g., user@example.com.");
        }
    }


    /// <summary>
    /// Reads and validates a password, ensuring it meets security requirements.
    /// </summary>
    private static string ReadAndValidatePassword()
    {
        Console.Write("Enter Password: ");

        while (true)
        {
            string password = ReadPassword();

            try
            {
                UserValidation.ValidatePassword(password);
                return password;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Password Error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Reads a password securely from the user, masking the input.
    /// </summary>
    public static string ReadPassword()
    {
        var password = new System.Text.StringBuilder();
        ConsoleKey key;

        do
        {
            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                // Erase the last character.
                Console.Write("\b \b");
                password.Length--;
            }
            // Checks that the key pressed is a valid printable char.
            else if (char.IsControl(keyInfo.KeyChar) == false)
            {
                // Display masking character.
                Console.Write("*");
                password.Append(keyInfo.KeyChar);
            }
        }
        while (key != ConsoleKey.Enter);

        Console.WriteLine();
        return password.ToString();
    }

    #endregion

}