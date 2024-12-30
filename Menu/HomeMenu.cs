namespace E_commerce_Databaser_i_ett_sammanhang;

public class HomeMenu : Menu
{
    private static int _currentPage = 1;
    public HomeMenu
    (
        
    )
    {
        // AddCommand(new ICommand(ConsoleKey, IUserService));
        AddCommand(new FakeCommand(ConsoleKey.A, userService));
    }
    public override void Display()
    {
        // REMOVENOTE: Not fully symmetrical (if that matters?)
        Console.WriteLine(
            $"""                                                                    
                   LOG OUT - F5                   HELP - F6                    EXIT - F7/ESC 
                ┌─────────────────┬────────────────────┬┬───────────────────┬──────────────────┐
                │  Search - F1    │  Categories - F2   ││  Cart ({}) - F3   │  Checkout - F4   │
                ├─────────────────┴────────────────────┼┼───────────────────┴──────────────────┤
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                │                                      ││                                      │
                ├──────────────────────────────────────┼┼──────────────────────────────────────┤
                │   ← A/Left (Previous page)                           (Next page) D/Right →   │
                └──────────────────────────────────────────────────────────────────────────────┘
            """
        );
    }
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
