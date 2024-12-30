namespace E_commerce_Databaser_i_ett_sammanhang;

public class CategoryMenu : Menu
{
    private static Dictionary<string, string> c = new Dictionary<string, string>
    {
        { "1", " " },
        { "2", " " },
        { "3", " " },
        { "4", " " },
        { "5", " " },
        { "6", " " },
        { "7", " " },
        { "8", " " },
        { "9", " " },
    };

    public CategoryMenu(IUserService userService, IMenuService menuService)
    {
        AddCommand(new ExitCommand(ConsoleKey.Escape, userService, menuService));
        AddCommand(new SelectCategoryCommand(ConsoleKey.F17, userService));
        AddCommand(new HelpCommand(ConsoleKey.F6, userService));
    }

    public override void Display()
    {
        Console.WriteLine(
            $"""
                ┌──────────────────────────────────────────────────────────────────────────────┐
                │  Select categories:                                                          │
                ├──────────────────────────────────────┬┬──────────────────────────────────────┤
                │ 1. electronics [{c["1"]}]            ││                                      │
                │ 2. cleaning    [{c["2"]}]            ││                                      │
                │ 3.                                   ││                                      │
                │ 4.                                   ││                                      │
                │ 5.                                   ││                                      │
                │ 6.                                   ││                                      │
                │ 7.                                   ││                                      │
                │ 8.                                   ││                                      │
                │ 9.                                   ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                ├──────────────────────────────────────┼┼──────────────────────────────────────┤
                │                                                       Enter - Finish         │
                └──────────────────────────────────────────────────────────────────────────────┘
            """
        );
    }

    //  Select categories:

    // TODO: Method to change display printout to print x or "".
    public static void SelectCategory(string input)
    {
        if (string.IsNullOrWhiteSpace(c[input]))
        {
            c[input] = "x";
            return;
        }
        c[input] = " ";
        return;
    }
}
