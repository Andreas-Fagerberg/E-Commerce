using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductCommand : MenuBaseCommand
{
    private readonly IShoppingCartService _shoppingCartService;
    private List<Product> _products = new List<Product>();
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
        IProductService productService,
        IShoppingCartService shoppingCartService
    )
        : base(triggerKey, userService, menuService)
    {
        _productService = productService;
        _shoppingCartService = shoppingCartService;
    }

    public override async Task Execute(Guid? currentUserId)
    {
        List<Product> currentPage = new List<Product>();
        List<Product> products = await _productService.GetAllProducts();
        while (true)
        {
            int index = 0;
            baseMenu.EditContent(_menuContent);
            baseMenu.Display();

            ConsoleKey input1 = Console.ReadKey().Key;

            switch (input1)
            {
                case ConsoleKey.D1:
                    List<List<Product>> allProducts = productMenu.EditContent(products);


            }
            break;

                case ConsoleKey.D2:

                while (true)
                {
                    List<List<Product>> foundProducts = new List<List<Product>>();
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Utilities.WriteLineWithPause("Please enter atleast one character.");
                        continue;
                    }
                    while (true)
                    {
                        productMenu.Display();

                        var key = CustomKeyReader.GetKeyOrBuffered();

                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }

                        if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                        {
                            productMenu.SetPage(key.Key);
                            index = productMenu.GetPage();
                            continue;
                        }

                        currentPage = foundProducts[index];

                        string fullLine = CustomKeyReader.GetBufferedLine();
                        if (!int.TryParse(fullLine, out int choice))
                        {
                            Utilities.WriteLineWithPause("You have to enter a number.");
                            continue;
                        }

                        Product selectedProduct;
                        if (choice > 0 && choice <= currentPage.Count)
                        {
                            selectedProduct = currentPage[choice - 1];
                            productMenu.DisplayProduct(selectedProduct);
                        }
                        else
                        {
                            Utilities.WriteLineWithPause("Please select a product from the list.");
                            continue;
                        }

                        ConsoleKey input2 = Console.ReadKey().Key;

                        if (key.Equals(ConsoleKey.D1))
                        {
                            _shoppingCartService.AddToShoppingCart(selectedProduct.ProductId, selectedProduct.Price);
                            break;
                        }
                        else if (key.Equals(ConsoleKey.Escape))
                        {
                            break;
                        }
                        Utilities.WriteLineWithPause("Incorrect input.");
                        continue;
                    }
                    break;
                }
                break;

            case ConsoleKey.D3:

                break;
            }
            return;
        }
    }
}
