using Npgsql.Replication;

namespace E_commerce_Databaser_i_ett_sammanhang;

class Program
{
    static async Task Main(string[] args)
    {
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
                await menuService.GetMenu().ExecuteCommand(input);
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
