namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductMenu : Menu
{
    // Keeps track of current page number
    int index = 0;
    public ProductMenu() { }

    // List containing all pages/lists with all products.
    List<List<Product>> _allProducts = new List<List<Product>>();

    public override void Display()
    {
        string displayRating = String.Empty;
        // List containing the current page/current products.
        List<Product> currentProducts = _allProducts[index];

        // Used to decide the size of the menu.
        int boxWidth = 79;
        string headerText = "Select a product below:";
        string optionText2 =
            "←   Left (Previous page)                                (Next page) Right   →";


        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine(
            "│ Name:                                           │ Price:          │ Rating:   │"
        );


        int i = 0;
        foreach (Product product in currentProducts)
        {

            displayRating = new string('★', product.Rating) + new string('☆', 5 - product.Rating);

            if (i < 9)
            {
                Console.WriteLine(
                    "│  "
                    + (i + 1)
                    + ". "
                    + product.Name
                    + new string(' ', 44 - product.Name!.Length)
                    + "│ "
                    + product.Price
                    + new string(' ', 16 - product.Price.ToString().Length)
                    + "│ "
                    + displayRating
                    + new string(' ', 10 - displayRating.Length)
                    + "│"
                );
                i++;
                continue;
            }

            Console.WriteLine(
                "│ "
                + (i + 1)
                + ". "
                + product.Name
                + new string(' ', 44 - product.Name!.Length)
                + "│ "
                + product.Price
                + new string(' ', 16 - product.Price.ToString().Length)
                + "│ "
                + displayRating
                + new string(' ', 10 - displayRating.Length)
                + "│"
            );
            i++;
            continue;

        }

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine(
            "│ " + optionText2 + new string(' ', boxWidth - (optionText2.Length + 1)) + "│"
        );
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }
    public void DisplayProduct(Product product)
    {
        string displayRating = new string('★', product.Rating) + new string('☆', 5 - product.Rating);

        int boxWidth = 79;
        string headerText = "Select an option below:";

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        Console.WriteLine(
            "│ NAME: "
            + product.Name
            + new string(' ', boxWidth - product.Name!.Length + 8)
            + "│");

        Console.WriteLine(
            "│ DESCRIPTION: "
            + product.Description
            + new string(' ', boxWidth - product.Description!.Length + 15)
            + "│");

        Console.WriteLine(
            "│ PRICE: "
            + product.Price
            + new string(' ', boxWidth - product.Price.ToString().Length + 9)
            + "│");

        Console.WriteLine(
            "│ RATING: "
            + displayRating
            + new string(' ', 16 - displayRating.Length + 10)
            + "│");

        // TODO: Should we set a custom message for available?
        Console.WriteLine(
            "│ IN STOCK: "
            + product.Available
            + new string(' ', boxWidth - product.Available!.ToString().Length + 12)
            + "│");

        Console.WriteLine(
            """
            │                                                                               │
            │ ESC. Go back                                                                  │
            """
        );

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

    public List<List<Product>> EditContent(List<Product> allProducts)
    {
        List<Product> tempList = new List<Product>();
        int i = 0;
        foreach (Product product in allProducts)
        {
            if (i >= 39)
            {
                _allProducts.Add(tempList);
                i = 0;
                tempList.Clear();
                tempList.Add(product);
            }
            tempList.Add(product);
            i++;
        }
        return new List<List<Product>>(_allProducts.ToList());
    }

    public void SetPage(ConsoleKey key)
    {
        if (key.Equals(ConsoleKey.LeftArrow) && index > 0)
        {
            index--;
        }

        if (key.Equals(ConsoleKey.RightArrow) && index < _allProducts.Count - 1)
        {
            index++;
        }
    }

    public int GetPage()
    {
        return index;
    }
}
