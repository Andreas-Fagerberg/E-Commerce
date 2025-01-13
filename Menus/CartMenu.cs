namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartMenu : Menu
{
    // Keeps track of current page number

    // List containing all pages/lists with all products.
    ICartService _cartService;
    private Guid _userId;
    private List<CartItem> _cartItems = new List<CartItem>();
    private string _headerText;

    public CartMenu()
    {
        // _CartService = CartService;
        // _userId = userId;
        // _cartItems = new List<CartItem>();
    }

    public override void Display()
    {
        Console.Clear();
        // Used to decide the size of the menu.
        int boxWidth = 79;

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + _headerText + new string(' ', boxWidth - (_headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        Console.WriteLine(
            "│ Name                                          │ Qty.   │ Price                │"
        );

        int i = 0;
        if (_cartItems.Count().Equals(0)) {}
        foreach (var item in _cartItems)
        {
            if (i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + item.Name
                        + new string(' ', 44 - item.Name.Length)
                        + "│ "
                        + item.Quantity.ToString()
                        + new string(' ', 16 - item.Quantity.ToString().Length)
                        + "│ "
                        + item.Price
                        + new string(' ', 16 - item.Price.ToString().Length)
                        + "│"
                );
                i++;
                continue;
            }
            else
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + item.Name
                        + new string(' ', 44 - item.Name!.Length)
                        + "│ "
                        + item.Quantity.ToString()
                        + new string(' ', 16 - item.Price.ToString().Length)
                        + "│ "
                        + item.Price
                        + new string(' ', 16 - item.Price.ToString().Length)
                        + "│"
                );
                i++;
            }

            continue;
        }
        Console.WriteLine(
            """                                               
            │                                                                               │
            │ ESC. Go back.                                                                 │
            """
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

    public void DisplayCartItems(CartItem cartItems)
    {
        int boxWidth = 79;
        string headerText = "Select an option below:";

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        Console.WriteLine(
            "│ NAME: "
                + cartItems.Name
                + new string(' ', boxWidth - cartItems.Name!.Length + 8)
                + "│"
        );

        Console.WriteLine(
            "│ QUANTITY: "
                + cartItems.Quantity
                + new string(' ', boxWidth - cartItems.Quantity.ToString().Length + 12)
                + "│"
        );

        Console.WriteLine(
            "│ PRICE: "
                + cartItems.Price
                + new string(' ', boxWidth - cartItems.Price.ToString().Length + 9)
                + "│"
        );

        Console.WriteLine(
            """

            │                                                                               │
            │  1. Change quantity.                                                          │
            │  2. Remove item from cart.                                                    │
            │                                                                               │
            │ ESC. Go back.                                                                 │
            """
        );

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

    // csharpier-ignore-start
    public void EditContent(List<CartItem> allCartItems, string headerText = "Your Shopping Cart:")
    {
        _headerText = headerText;
        _cartItems = allCartItems;
    }
    // csharpier-ignore-end
}
