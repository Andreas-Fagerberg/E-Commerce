namespace E_commerce_Databaser_i_ett_sammanhang;

public class HomeMenu : Menu
{
    private List<string> _menuContent;

    public HomeMenu(
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService,
        bool admin
    )
    {
        _menuContent = new List<string> { "Products", "Cart", "Checkout", "Orders" };
        if (admin)
        {
            _menuContent.Add("Admin Controls");
            AddCommand(
                new AdminCommands(
                    ConsoleKey.D5,
                    userService,
                    menuService,
                    productService,
                    cartService,
                    orderService,
                    paymentService
                )
            );
        }
        AddCommand(
            new ProductCommands(
                ConsoleKey.D1,
                userService,
                menuService,
                productService,
                cartService,
                orderService,
                paymentService
            )
        );
        AddCommand(
            new CartCommands(
                ConsoleKey.D2,
                userService,
                menuService,
                productService,
                cartService,
                orderService,
                paymentService
            )
        );
        AddCommand(
            new CheckoutCommands(
                ConsoleKey.D3,
                userService,
                menuService,
                productService,
                cartService,
                orderService,
                paymentService
            )
        );

        AddCommand(
            new ViewOrdersCommand(
                ConsoleKey.D4,
                userService,
                menuService,
                productService,
                cartService,
                orderService,
                paymentService
            )
        );

        AddCommand(
            new LogoutCommand(
                ConsoleKey.Escape,
                userService,
                menuService,
                productService,
                cartService,
                orderService,
                paymentService
            )
        );
    }

    public override void Display()
    {
        Console.Clear();
        int boxWidth = 79;
        string optionText1 = "Select an option below:";

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + optionText1 + new string(' ', boxWidth - (optionText1.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        for (int i = 0; i < 40; i++)
        {
            if (i > _menuContent.Count)
            {
                break;
            }
            if (_menuContent.Count > i && i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + _menuContent[i]
                        + new string(' ', boxWidth - (_menuContent[i].Length + 6))
                        + " │"
                );
                continue;
            }
            if (_menuContent.Count > i)
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + _menuContent[i]
                        + new string(' ', boxWidth - (_menuContent[i].Length + 6))
                        + " │"
                );
                continue;
            }
            Console.WriteLine(
                """
                │                                                                               │
                │ ESC. Log Out                                                                  │
                """
            );
        }

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }
}

