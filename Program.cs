namespace E_commerce_Databaser_i_ett_sammanhang;

using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        Guid? currentUserId = null; // Tracks the logged-in user's ID.
        IUserService userService = new UserService(new EcommerceContext());
        IMenuService menuService = new AppMenuService(userService);

        menuService.SetMenu(new LoginMenu(userService));

        while (true)
        {
            Console.Clear();
            menuService.GetMenu().Display();
            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.Escape
                or ConsoleKey.F7:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.LeftArrow
                or ConsoleKey.A:

                    continue;

                case ConsoleKey.RightArrow
                or ConsoleKey.D:

                    continue;
            }
            foreach (ConsoleKey consoleKey in menuService.GetTriggerKeys())
            {
                if (input.Equals(consoleKey))
                {
                    menuService.ChangeMenu(input);
                    continue;
                }
            }
            try
            {
                await menuService.GetMenu().ExecuteCommand(input, currentUserId);

                // After executing a command, check if we're in the LoginMenu
                if (menuService.GetMenu() is LoginMenu loginMenu)
                {
                    // Get the logged-in user's ID through LoginMenu.
                    var loggedInUserId = loginMenu.GetLoggedInUserId();
                    if (loggedInUserId != null)
                    {
                        // Update the currentUserId
                        currentUserId = loggedInUserId;
                        // Switch to HomeMenu after successful login. (I assume this is what we prefer?)
                        menuService.SetMenu(new HomeMenu());
                    }
                }
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.Message)
                    ? "Something went wrong, please try again."
                    : ex.Message;
            }
        }
    }
}
