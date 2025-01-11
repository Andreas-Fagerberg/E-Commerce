namespace E_commerce_Databaser_i_ett_sammanhang;

public class LoginCommand : MenuBaseCommand
{
    public LoginCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
    )
        : base(
            triggerKey,
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService
        )
    { }

    public override async Task<Guid> Execute()
    {
        while (true)
        {
            try
            {
                var loginDetails = InputHandler.GetLoginInput();
                UserResponse? loggedInUser = await userService.LoginUser(loginDetails);

                Console.WriteLine($"User logged in successfully!");
                Console.WriteLine(
                    $"Good to see you, {loggedInUser.FirstName} {loggedInUser.LastName}!"
                );

                // Sets Admin version of HomeMenu.
                if (await userService.CheckAdminPriviliges(loggedInUser.UserId))
                {
                    menuService.SetMenu(
                        new HomeMenu(
                            userService,
                            menuService,
                            productService,
                            cartService,
                            orderService,
                            paymentService,
                            true
                        )
                    );
                }

                // Sets regular version of HomeMenu.
                menuService.SetMenu(
                    new HomeMenu(
                        userService,
                        menuService,
                        productService,
                        cartService,
                        orderService,
                        paymentService,
                        false
                    )
                );
                return loggedInUser.UserId;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
