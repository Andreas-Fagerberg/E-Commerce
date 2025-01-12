namespace E_commerce_Databaser_i_ett_sammanhang;

public class AdminCommands : MenuBaseCommand
{
    private readonly BaseMenu _baseMenu;
    private readonly AdminMenu _adminMenu;
    private readonly List<string> _menuContent;

    public AdminCommands(
        ConsoleKey triggerkey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
    )
        : base(
            triggerkey,
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService
        )
    {
        _baseMenu = new BaseMenu();
        _adminMenu = new AdminMenu();

        _menuContent = new List<string>
        {
            "View All Users",
            "Search Users",
            "Update User Role",
            "Create Product",
            "Remove Product",
        };
    }

    public override async Task Execute()
    {
        Guid currentUserId = SessionHandler.CurrentUserId
            ?? throw new InvalidOperationException("No user is currently logged in");

        await userService.ValidateAdminUser(currentUserId);

        _baseMenu.EditContent(_menuContent);
        while (true)
        {
            _baseMenu.Display();

            var input = Console.ReadKey(true).Key;

            try
            {
                switch (input)
                {
                    case ConsoleKey.D1: // View All Users
                        var users = await userService.GetAllUsers(currentUserId);
                        List<string> allUsers = new List<string>();
                        foreach (var user in users)
                        {
                            allUsers.Add($" - {user.FirstName} {user.LastName} ({user.Email})");
                        }
                        _adminMenu.EditContent(allUsers, "All users: ");
                        _adminMenu.Display();
                        break;

                    case ConsoleKey.D2: // Search Users
                        var searchCriteria = InputHandler.GetAdminSearchInput();
                        var searchResults = await userService.SearchUsers(searchCriteria, currentUserId);

                        List<string> allResults = new List<string>();
                        foreach (var result in searchResults)
                        {
                            allResults.Add($" - {result.FirstName} {result.LastName} ({result.Email})");
                        }

                        _adminMenu.EditContent(allResults, "All matching users:");
                        _adminMenu.Display();

                        break;

                    case ConsoleKey.D3: // Update User Role
                        var newRole = InputHandler.GetRoleUpdateInput();
                        await userService.UpdateUserRole(newRole, currentUserId);
                        Console.WriteLine("User role updated successfully.");
                        break;

                    case ConsoleKey.D4: // Create New Product
                        var newProduct = InputHandler.GetCreateProductInput();
                        await productService.CreateProduct(newProduct);
                        Console.WriteLine("Product created successfully.");
                        break;

                    case ConsoleKey.D5: // Remove Product
                        int productId = InputHandler.GetProductIdToRemove(productService);
                        var isRemoved = await productService.RemoveProduct(productId);
                        if (isRemoved)
                        {
                            Console.WriteLine("Product removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to remove product.");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
