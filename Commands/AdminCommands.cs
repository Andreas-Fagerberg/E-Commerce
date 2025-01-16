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
        Guid currentUserId = SessionHandler.GetCurrentUserId();

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
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.D1: // View All Users
                        var users = await userService.GetAllUsers(currentUserId);
                        List<string> allUsers = new List<string>();
                        Console.WriteLine();
                        foreach (var user in users)
                        {
                            allUsers.Add($" NAME: {user.FirstName} {user.LastName}");
                            allUsers.Add($" EMAIL: {user.Email}");
                            allUsers.Add($" ROLE: {user.Role}");
                            allUsers.Add($" ID: {user.UserId}");
                        }
                        _adminMenu.EditContent(allUsers, "All users: ");
                        _adminMenu.Display();
                        Console.ReadKey(true);

                        break;

                    case ConsoleKey.D2: // Search Users
                        var searchCriteria = InputHandler.GetAdminSearchInput();
                        var searchResults = await userService.SearchUsers(
                            searchCriteria,
                            currentUserId
                        );

                        List<string> allResults = new List<string>();
                        foreach (var result in searchResults)
                        {
                            allResults.Add(
                                $" - {result.FirstName} {result.LastName} ({result.Email})"
                            );
                        }

                        _adminMenu.EditContent(allResults, "All matching users:");
                        _adminMenu.Display();
                        Console.ReadKey();

                        break;

                    case ConsoleKey.D3: // Update User Role
                        var emptyGuid = new Guid();
                        var newRole = InputHandler.GetRoleUpdateInput();
                        if (newRole.UserId.Equals(emptyGuid))
                        {
                            return;
                        }
                        await userService.UpdateUserRole(newRole, currentUserId);
                        Utilities.WriteLineWithPause($"User role for updated successfully.");
                        break;

                    case ConsoleKey.D4: // Create New Product
                        var newProduct = InputHandler.GetCreateProductInput();
                        if (newProduct is null)
                        {
                            return;
                        }
                        await productService.CreateProduct(newProduct);
                        Utilities.WriteLineWithPause("Product created successfully.", 3000);
                        break;

                    case ConsoleKey.D5: // Remove Product
                        var productId = await InputHandler.GetProductIdToRemove(productService);
                        if (productId is null)
                        {
                            return;
                        }
                        if (productId == 0)
                        {
                            break;
                        }

                        var isRemoved = await productService.RemoveProduct(productId);

                        if (isRemoved)
                        {
                            Utilities.WriteLineWithPause("Product removed successfully.", 3000);
                        }
                        else
                        {
                            Utilities.WriteLineWithPause("Failed to remove product.", 3000);
                        }
                        break;

                    default:
                        Utilities.WriteLineWithPause("Invalid option. Please try again.", 3000);
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
