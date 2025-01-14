namespace E_commerce_Databaser_i_ett_sammanhang;

public class ExitCommand : MenuBaseCommand
{
    public ExitCommand(
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

    public override Task Execute()
    {
        Environment.Exit(0);
        return Task.CompletedTask;
    }
}
