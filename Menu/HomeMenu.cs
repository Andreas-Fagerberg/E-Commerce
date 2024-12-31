namespace E_commerce_Databaser_i_ett_sammanhang;

public class HomeMenu : Menu
{
    private static int _currentPage = 1;

    public HomeMenu(IUserService userService)
    {
        AddCommand(new SelectCategoryCommand(ConsoleKey.F2, userService));
    }

    // List 0,1,2
    // -> = index 1  
    // OPTION 1
    public override void Display()
    {
        // REMOVENOTE: Not fully symmetrical (if that matters?).
        //Bygg scrollande meny,
        Console.WriteLine(
            $"""
                ┌──────────────────────────────────────────────────────────────────────────────┐
                │                                                                       AAAL © │
                ├──────────────────────────────────────────────────────────────────────────────┤
                │                                                                              │
                │    1. {}                                                                     │
                │    2. {}                                                                     │
                │    3. {}                                                                     │
                │    4. {}                                                                     │
                │    5. {}                                                                     │
                │    6. {}                                                                     │
                │    7. {}                                                                     │
                │    8. {}                                                                     │
                │    9. {}                                                                     │                                                                           
                │                                                                              │                                                                           
                ├──────────────────────────────────────────────────────────────────────────────┤
                │                                                                              │
                └──────────────────────────────────────────────────────────────────────────────┘
            """
        );
    }

    // OPTION 2
    // public override void Display2()
    // {
    //     Console.WriteLine(
    //         $"""
    //             ┌──────────────────────────────────────────────────────────────────────────────┐
    //             │  Select an option:                                                           │
    //             ├──────────────────────────────────────────────────────────────────────────────┤
    //             │ 1. Select categories                                                         │
    //             │ 2. HELP!!!                                                                   │
    //             │ 3. Return to home menu                                                       │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             │                                                                              │
    //             ├──────────────────────────────────────────────────────────────────────────────┤
    //             │   ← A/Left (Previous page)                           (Next page) D/Right →   │
    //             └──────────────────────────────────────────────────────────────────────────────┘
    //         """
    //     );
    // }
    public static int ChangePage()
    {
        throw new NotImplementedException();
    }
}
// // Box Drawing Characters (Unicode):

//     ─ (U+2500) – Horizontal line (top, middle, bottom).
//     │ (U+2502) – Vertical line (left, right).
//     ┌ (U+250C) – Top-left corner.
//     ┐ (U+2510) – Top-right corner.
//     └ (U+2514) – Bottom-left corner.
//     ┘ (U+2518) – Bottom-right corner.
//     ├ (U+251C) – Left intersection (vertical and horizontal).
//     ┤ (U+2524) – Right intersection (vertical and horizontal).
//     ┬ (U+252C) – Top intersection (horizontal and vertical).
//     ┴ (U+2534) – Bottom intersection (horizontal and vertical).
//     ┼ (U+253C) – Center intersection (both vertical and horizontal).
