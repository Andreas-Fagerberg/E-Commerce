namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductMenu : Menu
{
    public ProductMenu() { }

    public override void Display()
    {
        int index = 0;
        List<ProductForMenu> products = ProductList.GetProducts(index);

        int boxWidth = 79;
        string optionText1 = "Select a product below:";
        string optionText2 =
            "← A/Left (Previous page)                                (Next page) D/Right →";

        // Viktigt

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + optionText1 + new string(' ', boxWidth - (optionText1.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine(
            "│ Name:                                           │ Price:          │ Rating:   │"
        );

        for (int i = 0; i < 40; i++)
        {
            if (i > products.Count)
            {
                break;
            }
            if (products.Count > i && i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + products[i].Name
                        + new string(' ', 44 - products[i].Name.Length)
                        + "│ "
                        + products[i].Price
                        + new string(' ', 16 - products[i].Price.ToString().Length)
                        + "│ "
                        + products[i].DisplayRating
                        + new string(' ', 10 - products[i].DisplayRating.Length)
                        + "│"
                );
                continue;

                { }
            }
            if (products.Count > i)
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + products[i].Name
                        + new string(' ', 44 - products[i].Name.Length)
                        + "│ "
                        + products[i].Price
                        + new string(' ', 16 - products[i].Price.ToString().Length)
                        + "│ "
                        + products[i].DisplayRating
                        + new string(' ', 10 - products[i].DisplayRating.Length)
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
}
