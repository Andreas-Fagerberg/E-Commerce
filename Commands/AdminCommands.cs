
namespace E_commerce_Databaser_i_ett_sammanhang;

public class AdminCommands : MenuBaseCommand
{
    private readonly IProductService _productService;
    public AdminCommands(ConsoleKey triggerkey, IUserService userService, IMenuService menuService, ProductService productService)
        : base(triggerkey, userService, menuService)
    {
        _productService = productService;
    }
    public override async Task Execute(Guid? currentUserId)
    {
        await userService.ValidateAdminUser(currentUserId);

        while (true)
        {
            Utilities.ClearAndWriteLine(
            """
            [Admin Commands]
            [1] View All Users
            [2] Search Users
            [3] Update User Role
            [4] Create New Product
            [Esc] Exit Admin Menu
            """);

            var input = Console.ReadKey(true).Key;

            try
            {
                switch (input)
                {
                    case ConsoleKey.D1: // View All Users
                        var users = await userService.GetAllUsers(currentUserId);
                        Console.WriteLine("[Users]");
                        foreach (var user in users)
                        {
                            Console.WriteLine($"- {user.FirstName} {user.LastName} ({user.Email})");
                        }
                        break;

                    case ConsoleKey.D2: // Search Users
                        var searchCriteria = InputHandler.GetAdminSearchInput();
                        var searchResults = await userService.SearchUsers(searchCriteria, currentUserId);
                        Console.WriteLine("Search Results:");
                        foreach (var result in searchResults)
                        {
                            Console.WriteLine($"- {result.FirstName} {result.LastName} ({result.Email})");
                        }
                        break;

                    case ConsoleKey.D3: // Update User Role
                        var newRole = InputHandler.GetRoleUpdateInput();
                        await userService.UpdateUserRole(newRole, currentUserId);
                        Console.WriteLine("User role updated successfully.");
                        break;

                    case ConsoleKey.D4: // Create New Product
                        var newProduct = InputHandler.GetCreateProductInput(_productService);
                        await _productService.CreateProduct(newProduct);
                        Console.WriteLine("Product created successfully.");
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