namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductsCommand : BaseCommand
{
    private IMenuService _menuService;
    private ProductsMenu _productMenu;

    // Store products in lists of 9 items per list.
    private List<string> products;
    public static bool searchMode = false;
    public static bool categoryMode = false;

    public ProductsCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService
    )
        : base(triggerKey, userService)
    {
        _menuService = menuService;
    }

    public override Task Execute(Guid? currentUserId)
    {
        _productMenu = new ProductsMenu();
        _menuService.SetMenu(_productMenu);

        while (true)
        {
            _productMenu.ChangeContent(products);
        }
    }
}
