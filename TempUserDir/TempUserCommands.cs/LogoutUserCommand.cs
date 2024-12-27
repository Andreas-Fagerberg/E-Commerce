using System.ComponentModel;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class LogoutUserCommand : BaseCommand
{
    public LogoutUserCommand(ConsoleKey triggerkey, UserService userService)
        : base(triggerkey, userService)
    {

    }

    public override Task Execute(Guid? currentUserId)
    {
        try
        {
            currentUserId = userService.LogoutUser(currentUserId);
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

