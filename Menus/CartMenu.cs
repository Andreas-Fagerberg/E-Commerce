using System.Text;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartMenu : Menu
{
    // Keeps track of current page number

    // List containing all pages/lists with all products.
    private List<CartItem> _cartItems = new List<CartItem>();
    private string _headerText = string.Empty;
    private readonly StringBuilder _buffer = new StringBuilder();
    private int selectionTracker = 0;
    private string _bottomText = string.Empty;

    public CartMenu() { }

    public override void Display()
    {
        // Clear the buffer instead of the console
        _buffer.Clear();

        string displayRating;
        List<CartItem> currentProducts = _cartItems;
        int boxWidth = 79;

        // Build the entire display in memory first
        _buffer.AppendLine("┌" + new string('─', boxWidth) + "┐");
        _buffer.AppendLine(
            "│ " + _headerText + new string(' ', boxWidth - (_headerText.Length + 8)) + "AAAL © │"
        );
        _buffer.AppendLine("├" + new string('─', boxWidth) + "┤");
        _buffer.AppendLine(
            "│ Name                                          │ Qty.   │ Price                │"
        );

        // Build product rows
        for (int i = 0; i < currentProducts.Count; i++)
        {
            CartItem product = currentProducts[i];

            // Store the row content
            if (selectionTracker == i)
            {
                string row =
                    $" {(i < 9 ? " " : "")}{i + 1}. {product.Name}{new string(' ', 42 - product.Name!.Length)}│ {product.Quantity} {new string(' ', 5 - product.Quantity.ToString().Length)} │ {product.Price} {new string(' ', 15 - product.Price.ToString().Length)}SEK";
                _buffer.AppendLine($"<SELECTED>{row}<SELECTED>");
            }
            // If this is the selected row, we'll handle it specially during rendering
            else
            {
                string row =
                    $"│{(i < 9 ? "  " : " ")}{i + 1}. {product.Name}{new string(' ', 42 - product.Name!.Length)}│ {product.Quantity} {new string(' ', 5 - product.Quantity.ToString().Length)} │ {product.Price} {new string(' ', 15 - product.Price.ToString().Length)}SEK  │";
                _buffer.AppendLine(row);
            }
        }

        _buffer.AppendLine(
            """
            │                                                                               │
            │ ESC. Go Back.                                           ENTER. Select Product │
            """
        );

        _buffer.AppendLine("├" + new string('─', boxWidth) + "┤");
        _buffer.AppendLine(
            "│ " + _bottomText + new string(' ', boxWidth - (_bottomText.Length + 1)) + "│"
        );
        _buffer.AppendLine("└" + new string('─', boxWidth) + "┘");

        // Now render everything at once
        RenderBuffer();
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
                + new string(' ', boxWidth - (cartItems.Name.Length + 7))
                + "│"
        );

        Console.WriteLine(
            "│ QUANTITY: "
                + cartItems.Quantity
                + new string(' ', boxWidth - (cartItems.Quantity.ToString().Length + 11))
                + "│"
        );

        Console.WriteLine(
            "│ PRICE: "
                + cartItems.Price
                + " SEK"
                + new string(' ', boxWidth - (cartItems.Price.ToString().Length + 12))
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
    public void EditContent(List<CartItem> allCartItems, string headerText = "Select a cart item below:")
    {
        _headerText = headerText;
        _cartItems = allCartItems;
    }
    // csharpier-ignore-end
    private void RenderBuffer()
    {
        // Store cursor position and hide it during rendering
        Console.CursorVisible = false;

        // Clear console once
        Console.SetCursorPosition(0, 0);
        Console.Clear();

        // Split buffer into lines
        string[] lines = _buffer.ToString().Split(Environment.NewLine);

        foreach (string line in lines)
        {
            if (line.StartsWith("<SELECTED>"))
            {
                // Extract the actual content between the markers
                string content = line.Replace("<SELECTED>", "");
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(content);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(" │");
            }
            else
            {
                Console.WriteLine(line);
            }
        }

        // Restore cursor visibility
        Console.CursorVisible = true;
    }

    public void SetLine(int selectionTracker)
    {
        this.selectionTracker = selectionTracker;
    }
}
