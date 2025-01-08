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
        List<Product> products = await _productService.GetAllProducts();
        while (true)
        {
            baseMenu.EditContent(_menuContent);
            baseMenu.Display();

            ConsoleKey input = Console.ReadKey().Key;

            switch (input)
            {
                case ConsoleKey.D1:
                    List<List<Product>> allProducts = productMenu.EditContent(products);

                    while (true)
                    {
                        productMenu.Display();

                        var key = CustomKeyReader.GetKeyOrBuffered();

                        if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                        {
                            productMenu.SetPage(key.Key);
                            continue;
                        }

                        else if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }

                        int index = productMenu.GetPage();
                        List<Product> currentList = allProducts[index];
                        string fullLine = CustomKeyReader.GetBufferedLine();
                        if (!int.TryParse(fullLine, out int choice))
                        {
                            Console.WriteLine("You have to enter a number.");
                        }
                        // allproducts[index][choice]
                        // 40 produkter i varje lista, index väljer vilken lista, choice = index i valda listan
                        // Choice index får inte vara out of range
                        // page index kan aldrig vara out of range eftersom den kontrolleras innan.
                        if (choice > 0 && choice <= currentList.Count)
                        {

                        }

                        currentList[choice - 1]




                    }
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
