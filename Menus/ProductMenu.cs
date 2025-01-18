using System.Text;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductMenu : Menu
{
    private readonly StringBuilder _buffer = new StringBuilder();
    private int index = 0;
    private int selectionTracker = 0;
    private bool noProducts = false;
    private string headerText = string.Empty;
    private string errorMessage = string.Empty;
    private string bottomText = string.Empty;
    private List<List<Product>> _productLists = new List<List<Product>>();

    public override void Display()
    {
        // Clear the buffer instead of the console
        _buffer.Clear();

        string displayRating;
        List<Product> currentProducts = _productLists[index];
        int boxWidth = 79;

        // Build the entire display in memory first
        _buffer.AppendLine("┌" + new string('─', boxWidth) + "┐");
        _buffer.AppendLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        _buffer.AppendLine("├" + new string('─', boxWidth) + "┤");
        _buffer.AppendLine(
            "│ Name:                                        │ Price:             │ Rating:   │"
        );

        // Build product rows
        for (int i = 0; i < currentProducts.Count; i++)
        {
            Product product = currentProducts[i];
            displayRating = new string('★', product.Rating) + new string('☆', 5 - product.Rating);

            // Store the row content
            if (selectionTracker == i)
            {
                string row =
                    $" {(i < 9 ? " " : "")}{i + 1}. {product.Name}{new string(' ', 41 - product.Name!.Length)}│ {product.Price} {new string(' ', 14 - product.Price.ToString().Length)}SEK │ {displayRating}{new string(' ', 8 - displayRating.Length)}";
                _buffer.AppendLine($"<SELECTED>{row}<SELECTED>");
            }
            // If this is the selected row, we'll handle it specially during rendering
            else
            {
                string row =
                    $"│{(i < 9 ? "  " : " ")}{i + 1}. {product.Name}{new string(' ', 41 - product.Name!.Length)}│ {product.Price} {new string(' ', 14 - product.Price.ToString().Length)}SEK │ {displayRating}{new string(' ', 9 - displayRating.Length)} │";
                _buffer.AppendLine(row);
            }
        }

        _buffer.AppendLine(
            """
            │                                                                               │
            │ ESC. Go Back.                                          ENTER. Select product. │
            """
        );

        _buffer.AppendLine("├" + new string('─', boxWidth) + "┤");
        _buffer.AppendLine(
            "│ " + bottomText + new string(' ', boxWidth - (bottomText.Length + 1)) + "│"
        );
        _buffer.AppendLine("└" + new string('─', boxWidth) + "┘");

        // Now render everything at once
        RenderBuffer();
    }
    public void DisplayProduct(Product product)
    {
        Console.Clear();
        string displayRating =
            new string('★', product.Rating) + new string('☆', 5 - product.Rating);

        int boxWidth = 79;
        string headerText = "Select an option below:";

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        Console.WriteLine(
            "│ NAME: " + product.Name + new string(' ', boxWidth - (product.Name.Length + 7)) + "│"
        );

        Console.WriteLine(
            "│ DESCRIPTION: "
                + product.Description
                + new string(' ', boxWidth - (product.Description.Length + 14))
                + "│"
        );

        Console.WriteLine(
            "│ PRICE: "
                + product.Price
                + " SEK"
                + new string(' ', boxWidth - (product.Price.ToString().Length + 12))
                + "│"
        );

        Console.WriteLine(
            "│ RATING: " + displayRating + new string(' ', boxWidth - (displayRating.Length + 9)) + "│"
        );

        string stockStatus = product.Available ? "Yes" : "No";
        Console.WriteLine(
            "│ IN STOCK: "
                + stockStatus
                + new string(' ', boxWidth - (stockStatus.Length + 11))
                + "│"
        );

        Console.WriteLine(
            """
            │                                                                               │
            │  1. Add to cart.                                                              │
            │                                                                               │
            │ ESC. Go back.                                                                 │
            """
        );

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

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

    public void EditContent(List<List<Product>>? productLists = null)
    {
        if (productLists is null || productLists.Count.Equals(0))
        {
            noProducts = true;
            headerText = string.Empty;
            errorMessage = "No products found.";
            return;
        }
        noProducts = false;
        headerText = "Select a product below:";
        bottomText =
            "←   Left (Previous page)                                (Next page) Right   →";
        _productLists = productLists;
    }

    public void SetPage(ConsoleKey key)
    {
        if (key.Equals(ConsoleKey.LeftArrow) && index > 0)
        {
            index--;
        }

        if (key.Equals(ConsoleKey.RightArrow) && index < _productLists.Count - 1)
        {
            index++;
        }
    }

    public void SetLine(int selectionTracker)
    {
        this.selectionTracker = selectionTracker;
    }

    public int GetPage()
    {
        return index;
    }
}
