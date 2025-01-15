namespace E_commerce_Databaser_i_ett_sammanhang;

public class RegisterCommand : MenuBaseCommand
{
    public RegisterCommand(
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
                var registrationDetails = InputHandler.GetRegistrationInput();
                var response = await userService.RegisterUser(registrationDetails);

                SessionHandler.CurrentUserId = response.UserId;

                Utilities.WriteLineWithPause($"User registered successfully!");
                Utilities.WriteLineWithPause($"Welcome, {response.FirstName} {response.LastName}");

                menuService.SetMenu(
                    new HomeMenu(
                        userService,
                        menuService,
                        productService,
                        cartService,
                        orderService,
                        paymentService,
                        false));
                break;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
