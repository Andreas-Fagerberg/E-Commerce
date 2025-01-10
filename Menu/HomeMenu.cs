namespace E_commerce_Databaser_i_ett_sammanhang;

public class HomeMenu : Menu
{
    private List<string> _menuContent = new List<string> { "Products", "Cart", "Checkout", "Log out" };

    public HomeMenu(IUserService userService, IMenuService menuService)
    {
        AddCommand(new ProductCommand(ConsoleKey.D1, userService, menuService));
        // AddCommand()
        // AddCommand()
        // AddCommand()
    }
//     public override void Display()
//     {
//         int boxWidth = 79;
//         string optionText1 = "Select an option below:";

//         // Viktigt

//         Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
//         Console.WriteLine(
//             "│ " + optionText1 + new string(' ', boxWidth - (optionText1.Length + 8)) + "AAAL © │"
//         );
//         Console.WriteLine("├" + new string('─', boxWidth) + "┤");

//         for (int i = 0; i < 40; i++)
//         {
//             if (i > options.Count)
//             {
//                 break;
//             }
//             if (options.Count > i && i < 9)
//             {
//                 Console.WriteLine(
//                     "│  "
//                         + (i + 1)
//                         + ". "
//                         + options[i]
//                         + new string(' ', boxWidth - (options[i].Length + 6))
//                         + " │"
//                 );
//                 continue;
//             }
//             if (options.Count > i)
//             {
//                 Console.WriteLine(
//                     "│ "
//                         + (i + 1)
//                         + ". "
//                         + options[i]
//                         + new string(' ', boxWidth - (options[i].Length + 6))
//                         + " │"
//                 );
//                 continue;
//             }
//             Console.WriteLine(
//                 """
//                 │                                                                               │
//                 │ ESC. Exit application                                                         │
//                 """
//             );
//             // Console.WriteLine("│" + new string(' ', boxWidth) + "│");
//         }

//         Console.WriteLine("├" + new string('─', boxWidth) + "┤");
//         Console.WriteLine("│" + new string(' ', boxWidth) + "│");
//         Console.WriteLine("└" + new string('─', boxWidth) + "┘");
//     }
// }
// //  Box Drawing Characters (Unicode):

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
