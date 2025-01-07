namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductMenu : Menu
{
    int index = 0;
    public ProductMenu() { }

    List<List<Product>> _allProducts;

    public override void Display()
    {
        List<Product> currentProducts = _allProducts[index];

        int boxWidth = 79;
        string headerText = "Select a product below:";
        string optionText2 =
            "← A/Left (Previous page)                                (Next page) D/Right →";

        // Viktigt

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + headerText + new string(' ', boxWidth - (headerText.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine(
            "│ Name:                                           │ Price:          │ Rating:   │"
        );

        for (int i = 0; i < 40; i++)
        {
            if (i > currentProducts.Count)
            {
                break;
            }
            if (currentProducts.Count > i && i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + currentProducts[i].Name
                        + new string(' ', 44 - currentProducts[i].Name.Length)
                        + "│ "
                        + currentProducts[i].Price
                        + new string(' ', 16 - currentProducts[i].Price.ToString().Length)
                        + "│ "
                        + currentProducts[i].DisplayRating
                        + new string(' ', 10 - currentProducts[i].DisplayRating.Length)
                        + "│"
                );
                continue;

                { }
            }
            if (currentProducts.Count > i)
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + currentProducts[i].Name
                        + new string(' ', 44 - currentProducts[i].Name.Length)
                        + "│ "
                        + currentProducts[i].Price
                        + new string(' ', 16 - currentProducts[i].Price.ToString().Length)
                        + "│ "
                        + currentProducts[i].DisplayRating
                        + new string(' ', 10 - currentProducts[i].DisplayRating.Length)
                        + "│"
                );
                continue;
            }

            // Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        }

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine(
            "│ " + optionText2 + new string(' ', boxWidth - (optionText2.Length + 1)) + "│"
        );
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

    public void EditContent(List<Product> allProducts)
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
    }

    public void SetPage() { }
}
