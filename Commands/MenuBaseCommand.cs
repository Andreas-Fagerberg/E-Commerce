namespace E_commerce_Databaser_i_ett_sammanhang;

public abstract class MenuBaseCommand : ICommand
{
    public ConsoleKey TriggerKey { get; init; }
    protected readonly IUserService userService;
    protected readonly IMenuService menuService;
    protected readonly ICartService cartService;
    protected readonly IOrderService orderService;

    protected MenuBaseCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ICartService cartService,
        IOrderService orderService
    )
    {
        TriggerKey = triggerKey;
        this.userService = userService;
        this.menuService = menuService;
        this.cartService = cartService;
        this.orderService = orderService;
    }

    /* Using abstract method with a Task return type.
    (Task enables async operations to be performed within the method) */
    public abstract Task Execute(Guid? currentUserId);
}
