namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartMenu : Menu
{
    List<string> options = new List<string> { "Search", "Category", "Cart", "Checkout", "Log out" };

    public override void Display()
    {
        int boxWidth = 79;
        string optionText1 = "Select an option below:";

        // Viktigt

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + optionText1 + new string(' ', boxWidth - (optionText1.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        for (int i = 0; i < 40; i++)
        {
            if (i > options.Count)
            {
                break;
            }
            if (options.Count > i && i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + options[i]
                        + new string(' ', boxWidth - (options[i].Length + 6))
                        + " │"
                );
                continue;
            }
            if (options.Count > i)
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + options[i]
                        + new string(' ', boxWidth - (options[i].Length + 6))
                        + " │"
                );
                continue;
            }
            Console.WriteLine(
                """
                │                                                                               │
                │ ESC. Exit application                                                         │
                """
            );
            // Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        }

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }
}
