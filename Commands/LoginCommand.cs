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

    public override async Task Execute()
    {
        while (true)
        {
            try
            {
                var loginDetails = InputHandler.GetLoginInput();
                UserResponse? loggedInUser = await userService.LoginUser(loginDetails);

                if (loggedInUser == null)
                {
                    Console.WriteLine("Login failed. Please try again.");
                    continue;
                }

                SessionHandler.CurrentUserId = loggedInUser.UserId;
                await cartService.GetShoppingCart(SessionHandler.GetCurrentUserId());
                Utilities.WriteLineWithPause($"Good to see you, {loggedInUser.FirstName} {loggedInUser.LastName}!", 2000);


                // Determine if the user has admin privileges and set the appropriate HomeMenu
                bool isAdmin = UserValidation.CheckIfAdmin(loggedInUser);

                menuService.SetMenu(
                    new HomeMenu(
                        userService,
                        menuService,
                        productService,
                        cartService,
                        orderService,
                        paymentService,
                        isAdmin));

                // Exit the loop after a successful login and menu setup.
                break;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
