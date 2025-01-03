namespace E_commerce_Databaser_i_ett_sammanhang;

public class CategoryMenu : Menu
{
    public static void DoSomething(int index)
    {
        List<string> categories = GetCategories();
        string[] selections = new string[categories.Count];
        Array.Fill(selections, "[ ]");

        // Console.WriteLine(
        //     $"""
        //         ┌──────────────────────────────────────────────────────────────────────────────┐
        //         │  Select categories:                                                          │
        //         ├──────────────────────────────────────────────────────────────────────────────┤
        //         │                                                                              │
        //         │ 1. electronics [{c["1"]}]                                                    │
        //         │ 2. cleaning    [{c["2"]}]                                                    │
        //         │ 3.                                                                           │
        //         │ 4.                                                                           │
        //         │ 5.                                                                           │
        //         │ 6.                                                                           │
        //         │ 7.                                                                           │
        //         │ 8.                                                                           │
        //         │ 9.                                                                           │
        //         │                                                                              │
        //         ├──────────────────────────────────────────────────────────────────────────────┤
        //         │                                                                              │
        //         └──────────────────────────────────────────────────────────────────────────────┘
        //     """
        // );

        int boxWidth = 79;
        string optionText1 = "Select a category below:";
        string optionText2 =
            "← A/Left (Previous page)                                (Next page) D/Right →";

        // Viktigt

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ " + optionText1 + new string(' ', boxWidth - (optionText1.Length + 8)) + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        for (int i = 0; i < 40; i++)
        {
            if (i > categories.Count)
            {
                break;
            }
            if (categories.Count > i && i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + categories[i]
                        + new string(' ', 60 - categories[i].Length)
                        + selections[i]
                        + new string(' ', 11)
                        + "│"
                );
                continue;
            }
            if (categories.Count > i)
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + categories[i]
                        + new string(' ', 60 - categories[i].Length)
                        + selections[i]
                        + new string(' ', 11)
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

    public static List<string> GetCategories()
    {
        // Temporary hardcoded list for testing/development
        List<string> categories = new List<string>
        {
            "El ect ron ic s",
            "Gaming",
            "Cleaning",
            "Electronics",
            "Gaming",
            "Cleaning",
            "Electronics",
            "Gaming",
            "Cleaning",
            "Electronics",
            "Gaming",
            "Cleaning",
            "Electronics",
            "Gaming",
            "Cleaning",
        };

        return new List<string>(categories);
    }

    // You might want to add a method like this for when you implement the database
    public static async Task<List<string>> GetCategoriesFromDbAsync()
    {
        throw new NotImplementedException();
    }
    
    //  Select categories:

    // TODO: Method to change display printout to print x or "".
    // public static void SelectCategory(string input)
    // {
    //     if (string.IsNullOrWhiteSpace(c[input]))
    //     {
    //         c[input] = "x";
    //         return;
    //     }
    //     c[input] = " ";
    //     return;
    // }

    public override void Display()
    {
        throw new NotImplementedException();
    }
}

    
    
