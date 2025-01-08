
namespace E_commerce_Databaser_i_ett_sammanhang;

public class LoginMenuCommands : MenuBaseCommand
{
    public UserResponse? LoggedInUser { get; private set; }

    public LoginMenuCommands(ConsoleKey triggerkey, IUserService userService, IMenuService menuService)
        : base(triggerkey, userService, menuService) { }
    public override async Task Execute(Guid? currentUserId)
    {
        while (true)
        {
            Utilities.ClearAndWriteLine(
            """
            [Login Commands]
            [1] Login
            [2] Register User
            [Esc] Exit            
            """);

            var input = Console.ReadKey(true).Key;

            try
            {
                switch (input)
                {
                    case ConsoleKey.D1: // Login
                        var loginDetails = InputHandler.GetLoginInput();
                        LoggedInUser = await userService.LoginUser(loginDetails);

                        Console.WriteLine($"User logged in successfully!");
                        Console.WriteLine($"Good to see you, {LoggedInUser.FirstName} {LoggedInUser.LastName}!");
                        break;

                    case ConsoleKey.D2: // Register
                        var registrationDetails = InputHandler.GetRegistrationInput();
                        var response = await userService.RegisterUser(registrationDetails);

                        Console.WriteLine($"User registered successfully!");
                        Console.WriteLine($"Welcome, {response.FirstName} {response.LastName}");
                        Console.ReadLine();
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}