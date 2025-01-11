namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductHandler
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ProductMenu _productMenu;
    private readonly BaseMenu _baseMenu;
    private List<List<Product>> _productLists = new List<List<Product>>();
    private int index = 0;

    public ProductHandler(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
        _productMenu = new ProductMenu();
        _baseMenu = new BaseMenu();
    }

    // This method handles showing and navigating through all products
    public async Task HandleShowProducts(List<Product>? products = null)
    {
        index = 0;
        if (products is null)
        {
            _productLists = await _productService.GetProductLists();
        }
        else
        {
            _productLists = await _productService.GetProductLists(products);
        }
        _productMenu.EditContent(_productLists);

        while (true)
        {
            // Display current page of products using your existing menu
            _productMenu.Display();

            var key = CustomKeyReader.GetKeyOrBuffered();

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }

            if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
            {
                _productMenu.SetPage(key.Key);
                index = _productMenu.GetPage();
                continue;
            }

            // Get current page for product selection
            var currentPage = _productLists[_productMenu.GetPage()];

            // Handle selecting a specific product
            string fullLine = CustomKeyReader.GetBufferedLine();

            if (!int.TryParse(fullLine, out int choice))
            {
                Utilities.WriteLineWithPause("You have to enter a number.");
                continue;
            }

            if (choice > 0 && choice <= currentPage.Count)
            {
                Product selectedProduct = currentPage[choice - 1];
                await HandleProductSelection(selectedProduct);
            }
            else
            {
                Utilities.WriteLineWithPause("Please select a product from the list.");
                continue;
            }
        }
    }

    // This method handles product search functionality
    public async Task HandleSearchProducts()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("Enter search term: ");
            var searchTerm = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Utilities.WriteLineWithPause("Please enter a search term.");
                return;
            }

            // Search for products and display them using the same flow as ShowAllProducts
            var products = await _productService.SearchProducts(searchTerm);
            await HandleShowProducts(products);
            return;
        }
    }

    // This method handles the display and actions for a single selected product
    private async Task HandleProductSelection(Product product)
    {
        while (true)
        {
            // Use your existing product detail display
            _productMenu.DisplayProduct(product);
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D1)
            {
                await _cartService.AddToShoppingCart(product, 1);
                return;
            }
            else if (key == ConsoleKey.Escape)
            {
                return;
            }

            Utilities.WriteLineWithPause("Please select an option.");
        }
    }

    public async Task HandleCategorySelection()
    {
        var categories = Enum.GetValues(typeof(Category));
        List<string> menuContent = new List<string>();
        foreach (Category category in categories)
        {
            menuContent.Add(category.ToString());
        }

        _baseMenu.EditContent(menuContent, "Select a category below");

        while (true)
        {
            _baseMenu.Display();
            var key = CustomKeyReader.GetKeyOrBuffered();

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }

            string fullLine = CustomKeyReader.GetBufferedLine();

            if (!int.TryParse(fullLine, out int choice))
            {
                Utilities.WriteLineWithPause("You have to enter a number.");
                continue;
            }

            if (choice > 0 && choice <= menuContent.Count)
            {
                var products = await _productService.SearchProducts(null, menuContent[choice]);
                await HandleShowProducts(products);
                return;
            }
            Utilities.WriteLineWithPause("Please select a category from the list.");
            continue;
        }
    }
}
