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
        try
        {
            StartUpScreen.Display();
        }
        catch
        {
            System.Console.WriteLine(
                "This would have been an awesome intro to our presentation <3"
            );
        }

        while (true)
        {
            Console.Clear();
            menuService.GetMenu().Display();
            ConsoleKey input = Console.ReadKey(true).Key;

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
