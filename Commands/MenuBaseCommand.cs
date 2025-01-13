namespace E_commerce_Databaser_i_ett_sammanhang;

public abstract class MenuBaseCommand : ICommand
{
    public readonly ConsoleKey triggerKey;
    protected readonly IUserService userService;
    protected readonly IMenuService menuService;
    protected readonly IProductService productService;
    protected readonly ICartService cartService;
    protected readonly IOrderService orderService;
    protected readonly IPaymentService paymentService;

    protected MenuBaseCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
    )
    {
        this.triggerKey = triggerKey;
        this.userService = userService;
        this.menuService = menuService;
        this.productService = productService;
        this.cartService = cartService;
        this.orderService = orderService;
        this.paymentService = paymentService;
    }

    /* Using abstract method with a Task return type.
    (Task enables async operations to be performed within the method) */
    public abstract Task Execute();
}
