namespace E_commerce_Databaser_i_ett_sammanhang;

public class LoginMenu : Menu
{
    private List<string> _menuContent;
    private string _headerContent;

    public LoginMenu(
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService
    )
    {
        _menuContent = new List<string> { "Login", "Register User" };
        _headerContent = "Select an option below:";
        AddCommand(
            new LoginCommand(
                ConsoleKey.D1,
                userService,
                menuService,
                productService,
                cartService,
                orderService
            )
        );

        AddCommand(
            new RegisterUserCommand(
                ConsoleKey.D2,
                userService,
                menuService,
                productService,
                cartService,
                orderService
            )
        );
    }

    public override void Display()
    {
        int lineCount = 1;
        int boxWidth = 79;

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ "
                + _headerContent
                + new string(' ', boxWidth - (_headerContent.Length + 8))
                + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        foreach (string item in _menuContent)
        {
            if (_menuContent is null || _menuContent.Count.Equals(0))
            {
                Console.WriteLine(
                    "│ No options found.                                                               │"
                );
                break;
            }
            Console.WriteLine(
                "│"
                    + new string(' ', 3 - lineCount.ToString().Length)
                    + lineCount
                    + ". "
                    + item
                    + new string(' ', boxWidth - (item.Length + lineCount.ToString().Length + 5))
                    + " │"
            );
            lineCount++;
            continue;
        }

        Console.WriteLine(
            """
            │                                                                               │
            │ ESC. Go Back.                                                                 │
            """
        );

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }
}
