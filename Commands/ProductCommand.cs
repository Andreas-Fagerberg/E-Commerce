using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductCommand : MenuBaseCommand
{
    private List<Product> _products;
    private List<string> _menuContent = new List<string>
    {
        "Show all products",
        "Search for products",
        "Select category",
    };
    private readonly IProductService _productService;
    private BaseMenu baseMenu = new BaseMenu();
    private ProductMenu productMenu = new ProductMenu(); 

    public ProductCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService
    )
        : base(triggerKey, userService, menuService)
    {
        _productService = productService;
    }

    public override async Task Execute(Guid? currentUserId)
    {
        List<Product> _products = await _productService.GetAllProducts();
        while (true)
        {
            baseMenu.EditContent(_menuContent);
            baseMenu.Display();

            ConsoleKey input = Console.ReadKey().Key;

            switch (input)
            {
                case ConsoleKey.D1:
                    
                    break;

                case ConsoleKey.D2:

                    break;

                case ConsoleKey.D3:

                    break;
            }
            return;
        }
    }
}
