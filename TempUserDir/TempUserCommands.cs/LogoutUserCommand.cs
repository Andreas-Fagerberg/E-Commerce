namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Handles the execution of the user logout process as part of the command pattern.
/// </summary>
public class LogoutUserCommand : BaseCommand
{
    public LogoutUserCommand(ConsoleKey triggerkey, IUserService userService)
        : base(triggerkey, userService)
    {

    }

    /// <summary>
    /// Executes the user logout process. The caller is responsible for nullifying the currentUserId after the method call.
    /// </summary>
    public override Task Execute(Guid? currentUserId)
    {
        try
        {
            userService.LogoutUser(currentUserId);
            Console.WriteLine($"Logout successful.");
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
            Console.WriteLine($"An Unexpected error occurred: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}

