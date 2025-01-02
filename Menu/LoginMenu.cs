namespace E_commerce_Databaser_i_ett_sammanhang;

public class LoginMenu : Menu
{
    private readonly LoginUserCommand loginCommand;
    public LoginMenu(IUserService userService)
    {
        loginCommand = new LoginUserCommand(ConsoleKey.D1, userService);
        AddCommand(loginCommand);
        AddCommand(new RegisterUserCommand(ConsoleKey.D2, userService));
    }

    public Guid? GetLoggedInUserId()
    {
        return loginCommand.GetCurrentUserId();
    }
    public override void Display()
    {
        Utilities.ClearAndWriteLine(
            """
            [Login Menu]
            [1] Login
            [2] Register
            [Esc] Exit
            """);
    }
}
