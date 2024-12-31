namespace E_commerce_Databaser_i_ett_sammanhang;

public class LogoutMenu : Menu
{
    public LogoutMenu(IUserService userService, IMenuService menuService)
    {
        AddCommand(new LogoutUserCommand(ConsoleKey.D1, userService, menuService));
    }

    public override void Display()
    {
        // Do we want to display anything for this?
        // Or should F5 (Logout) Execute the Command immediately?


        Console.WriteLine(
            """
            Are you sure?
            [1] Yes
            [2] No (Not implemented)
            """);
    }
}
