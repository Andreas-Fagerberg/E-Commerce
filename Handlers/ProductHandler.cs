namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductHandler
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ProductMenu _productMenu;
    private readonly BaseMenu _baseMenu;
    private List<List<Product>> _productLists = new List<List<Product>>();
    public List<Product> currentPage = new List<Product>();
    public bool searchMode = false;
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
        if (products is null && !searchMode)
        {
            _productLists = await _productService.GetProductLists();
        }
        else
        {
            _productLists = await _productService.GetProductLists(products);
        }

        _productMenu.EditContent(_productLists);
        await ProductSelection();
    }

    // This method handles product search functionality
    public async Task HandleSearchProducts()
    {
        while (true)
        {
            searchMode = true;
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
            searchMode = false;
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

            Utilities.WriteLineWithPause("Please select an option.", 500);
        }
    }

    public async Task HandleCategorySelection()
    {
        searchMode = true;
        var categories = Enum.GetValues<Category>();
        var menuContent = categories.Select(c => c.ToString()).ToList();
        _baseMenu.EditContent(menuContent, "Select a category below");

        while (true)
        {
            _baseMenu.Display();
            var input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.Escape)
            {
                return;
            }

            if (int.TryParse(input.KeyChar.ToString(), out int choice)) { }
            if (choice > menuContent.Count)
            {
                Utilities.WriteLineWithPause("Please select a category from the list.");
                continue;
            }
            if (Enum.TryParse<Category>(menuContent[choice - 1], out Category selectedCategory))
            {
                var products = await _productService.SearchProducts(
                    null,
                    selectedCategory.ToString()
                );
                if (products != null && products.Any())
                {
                    await HandleShowProducts(products);
                    searchMode = false;
                    return;
                }
                else
                {
                    Utilities.WriteLineWithPause("No products found in this category.");
                    continue;
                }
            }
        }
    }

    public async Task ProductSelection()
    {
        int selectionTracker = 0;
        if (!searchMode)
        {
            _productLists = await _productService.GetProductLists();
            _productMenu.EditContent(_productLists);
        }

        // Initial render
        currentPage = _productLists[_productMenu.GetPage()];
        _productMenu.SetLine(selectionTracker);
        _productMenu.Display();

        while (true)
        {
            ConsoleKey input = Console.ReadKey(intercept: true).Key;
            bool requiresRedraw = false;

            switch (input)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    _productMenu.SetPage(input);
                    selectionTracker = 0;
                    requiresRedraw = true;
                    break;
                case ConsoleKey.UpArrow:
                    selectionTracker--;
                    if (selectionTracker < 0)
                    {
                        selectionTracker = currentPage.Count - 1;
                    }
                    requiresRedraw = true;
                    break;

                case ConsoleKey.DownArrow:
                    selectionTracker++;
                    if (selectionTracker > 39 || selectionTracker > currentPage.Count - 1)
                    {
                        selectionTracker = 0;
                    }
                    requiresRedraw = true;
                    break;
                case ConsoleKey.Enter:
                    await HandleProductSelection(currentPage[selectionTracker]);
                    requiresRedraw = true;
                    break;
            }

            if (requiresRedraw)
            {
                currentPage = _productLists[_productMenu.GetPage()];
                _productMenu.SetLine(selectionTracker);
                _productMenu.Display();
            }
        }
    }
}
