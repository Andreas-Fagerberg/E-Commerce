namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductCommands : MenuBaseCommand
{
    private readonly ProductHandler _productHandler;
    private BaseMenu baseMenu = new BaseMenu();
    private List<string> _menuContent = new List<string>
    {
        "Show all products",
        "Search for products",
        "Select category",
    };

    public ProductCommands(
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
    {
        _productHandler = new ProductHandler(productService, cartService);
    }

    public override async Task Execute(Guid? currentUserId)
    {
        while (true)
        {
            baseMenu.EditContent(_menuContent);
            baseMenu.Display();

            ConsoleKey input = Console.ReadKey().Key;

            switch (input)
            {
                case ConsoleKey.D1:
                    await _productHandler.HandleShowProducts();
                    break;
                case ConsoleKey.D2:
                    await _productHandler.HandleSearchProducts();
                    break;
                case ConsoleKey.D3:
                    await _productHandler.HandleCategorySelection();
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    continue;
            }
        }
    }
}
