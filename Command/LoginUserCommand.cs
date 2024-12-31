namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Handles the execution of the user login process as part of the command pattern.
/// </summary>
public class LoginUserCommand : BaseCommand
{
    /// <summary>
    /// Stores the logged-in user's details if the login process is successful. 
    /// </summary>
    public UserResponse? LoggedInUser { get; private set; }
    public LoginUserCommand(ConsoleKey triggerkey, IUserService userService)
        : base(triggerkey, userService)
    {
    }

    public override async Task Execute(Guid? currentUserId)
    {
        Utilities.ClearAndWriteLine("[Login]\n");

        try
        {
            var dto = InputHandler.GetLoginInput();
            Console.WriteLine("[DEBUG] LoginUserCommand: #1");
            LoggedInUser = await userService.LoginUser(dto);
            Console.WriteLine("[DEBUG] LoginUserCommand: #2");

            Console.WriteLine($"User logged in successfully!");
            Console.WriteLine($"Good to see you, {LoggedInUser.FirstName} {LoggedInUser.LastName}!");
            Console.WriteLine("[DEBUG] LoginUserCommand: #3");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");

        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        Console.ReadLine(); // TEMP: Breaker
    }
}