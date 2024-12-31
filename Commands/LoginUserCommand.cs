namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Handles the execution of the user login process as part of the command pattern.
/// </summary>
public class LoginUserCommand : BaseCommand
{
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
            LoggedInUser = await userService.LoginUser(dto);

            Console.WriteLine($"User logged in successfully!");
            Console.WriteLine($"Good to see you, {LoggedInUser.FirstName} {LoggedInUser.LastName}!");
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

    public Guid? GetCurrentUserId()
    {
        return LoggedInUser?.UserId;
    }
}