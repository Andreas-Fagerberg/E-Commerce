using Npgsql.Replication;

namespace E_commerce_Databaser_i_ett_sammanhang;

class Program
{
    static async Task Main(string[] args)
    {
        Guid? currentUserId = null; // Tracks the logged-in user's ID.
        EcommerceContext ecommerceContext = new EcommerceContext();
        IUserService userService = new UserService(ecommerceContext);
        IMenuService menuService = new MenuService(userService);
        IProductService productService = new ProductService(ecommerceContext);
        ICartService cartService = new CartService(ecommerceContext);
        IOrderService orderService = new OrderService(ecommerceContext);
        IPaymentService paymentService = new PaymentService(ecommerceContext);

        menuService.SetMenu(
            new LoginMenu(
                userService,
                menuService,
                productService,
                cartService,
                orderService,
                paymentService
            )
        );

        while (true)
        {
            Console.Clear();
            menuService.GetMenu().Display();
            ConsoleKey input = Console.ReadKey(true).Key;

            if (input.Equals(ConsoleKey.Escape))
            {
                Environment.Exit(0);
                break;
            }

            try
            {
                await menuService.GetMenu().ExecuteCommand(input, currentUserId);

                // After executing a command, check if we're in the LoginMenu
                // if (menuService.GetMenu() is LoginMenu loginMenu)
                // {
                //     // Get the logged-in user's ID through LoginMenu.
                //     var loggedInUserId = loginMenu.GetLoggedInUserId();
                //     if (loggedInUserId != null)
                //     {
                //         // Update the currentUserId
                //         currentUserId = loggedInUserId;
                //         // Switch to HomeMenu after successful login. (I assume this is what we prefer?)
                //         menuService.SetMenu(new HomeMenu());
                //     }
                // }
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
