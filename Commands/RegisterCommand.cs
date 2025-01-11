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
        ) { }

    public override async Task Execute(Guid? currentUserId)
    {
        while (true)
        {
            var input = Console.ReadKey(true).Key;

            try
            {
                var registrationDetails = InputHandler.GetRegistrationInput();
                var response = await userService.RegisterUser(registrationDetails);

                Console.WriteLine($"User registered successfully!");
                Console.WriteLine($"Welcome, {response.FirstName} {response.LastName}");
                Console.ReadLine();
                break;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
