using System.Windows.Input;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class LoginUserCommand : BaseCommand
{
    public LoginUserCommand(ConsoleKey triggerkey, UserService userService)
        : base(triggerkey, userService)
    {
    }

    public override async Task Execute(Guid? currentUserId)
    {
        try
        {

            var dto = InputHandler.GetLoginInput();
            var response = await userService.LoginUser(dto);

            Console.WriteLine($"User logged in successfully!");
            Console.WriteLine($"Good to see you, {response.FirstName} {response.LastName}!");
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
    }
}