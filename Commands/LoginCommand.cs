namespace E_commerce_Databaser_i_ett_sammanhang;

public class LoginCommand : MenuBaseCommand
{
    public UserResponse? LoggedInUser { get; private set; }

    public LoginCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService
    )
        : base(triggerKey, userService, menuService, productService, cartService, orderService) { }

    public override async Task Execute(Guid? currentUserId)
    {
        while (true)
        {
            try
            {
                var loginDetails = InputHandler.GetLoginInput();
                LoggedInUser = await userService.LoginUser(loginDetails);

                Console.WriteLine($"User logged in successfully!");
                Console.WriteLine(
                    $"Good to see you, {LoggedInUser.FirstName} {LoggedInUser.LastName}!"
                );
                if (await userService.CheckAdminPriviliges(LoggedInUser.UserId))
                {
                    menuService.SetMenu(
                        new HomeMenu(
                            userService,
                            menuService,
                            productService,
                            cartService,
                            orderService,
                            true
                        )
                    );
                    break;
                }
                menuService.SetMenu(
                    new HomeMenu(
                        userService,
                        menuService,
                        productService,
                        cartService,
                        orderService,
                        false
                    )
                );
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
