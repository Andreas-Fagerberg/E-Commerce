namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartMenu : Menu
{
    // Keeps track of current page number

    // List containing all pages/lists with all products.
    
    ICartService _CartService;
    private Guid _userId;
    private List<CartItem> _cartItems; 

    public CartMenu(ICartService CartService, Guid userId)
    {
        _CartService = CartService;
        _userId = userId;
        _cartItems = new List<CartItem>();
    }

    public override async void Display()
    {
        string displayRating = String.Empty;
        // List containing the current page/current products.
        // List<Product> currentProducts = _allProducts[0];
        var cart = await _CartService.GetShoppingCart(_userId);
         var cartItems = _CartService.ConvertCartToList(cart);

        // Used to decide the size of the menu.
        int boxWidth = 79;
        string headerText = "Your Shopping Cart:";

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine(
            "│ Name  │ Quantity       │ Price per Item     │ Total Price        │"
        );

        int i = 0;
        foreach (var item in cart)
        {
            if (i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + cartItem.
                        + new string(' ', 44 - cart.Name!.Length)
                        + "│ "
                        + cart.Price
                        + new string(' ', 16 - cart.Price.ToString().Length)
                        + "│ "
                        + "│"
                );
                i++;
                continue;
            }

            Console.WriteLine(
                "│ "
                    + (i + 1)
                    + ". "
                    + cartItem.Name
                    + new string(' ', 44 - cart.Name!.Length)
                    + "│ "
                    + cart.Price
                    + new string(' ', 16 - cart.Price.ToString().Length)
                    + "│ "
                    + new string(' ', 17)
                    + "│"
            );
            i++;
            continue;
        }
        Console.WriteLine(
            """
             
            │                                                                               │
            │  1. Add to cart.                                                              │
            │                                                                               │
            │ ESC. Go back.                                                                 │
            """
        );
    }

    // public void DisplayProduct(Product product)
    // {
    //     string displayRating =
    //         new string('★', product.Rating) + new string('☆', 5 - product.Rating);

    //     int boxWidth = 79;
    //     string headerText = "Select an option below:";

    //     Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
    //     Console.WriteLine(
    //         "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
    //     );
    //     Console.WriteLine("├" + new string('─', boxWidth) + "┤");

    //     Console.WriteLine(
    //         "│ NAME: " + product.Name + new string(' ', boxWidth - product.Name!.Length + 8) + "│"
    //     );

    //     Console.WriteLine(
    //         "│ DESCRIPTION: "
    //             + product.Description
    //             + new string(' ', boxWidth - product.Description!.Length + 15)
    //             + "│"
    //     );

    //     Console.WriteLine(
    //         "│ PRICE: "
    //             + product.Price
    //             + new string(' ', boxWidth - product.Price.ToString().Length + 9)
    //             + "│"
    //     );

    //     Console.WriteLine(
    //         "│ RATING: " + displayRating + new string(' ', 16 - displayRating.Length + 10) + "│"
    //     );

    //     // TODO: Should we set a custom message for available?
    //     Console.WriteLine(
    //         "│ IN STOCK: "
    //             + product.Available
    //             + new string(' ', boxWidth - product.Available!.ToString().Length + 12)
    //             + "│"
    //     );

    // Console.WriteLine(
    //     """

    //     │                                                                               │
    //     │  1. Add to cart.                                                              │
    //     │                                                                               │
    //     │ ESC. Go back.                                                                 │
    //     """
    // );

    //     Console.WriteLine("├" + new string('─', boxWidth) + "┤");
    //     Console.WriteLine("│" + new string(' ', boxWidth) + "│");
    //     Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    // }

    public List<List<CartItem>> EditContent(List<CartItem> allCartItems)
    {
        List<CartItem> tempList = new List<CartItem>();
        int i = 0;
        foreach (CartItem cartitem in allCartItems)
        {
            if (i >= 39)
            {
                _cartItems.Add(tempList);
                i = 0;
                tempList.Clear();
                tempList.Add(product);
            }
            tempList.Add(product);
            i++;
        }
        return new List<CartItem>(_cartItems.ToList());
    }
}
