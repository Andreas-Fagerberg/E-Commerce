namespace E_commerce_Databaser_i_ett_sammanhang;

public class HomeMenu : Menu
{
    public override void Display()
    {
        /* 
        Concept of swapping displayed products on screen
        
        int pageNumber = 1; 

            switch (input)
            {

                case ConsoleKey.LeftArrow or ConsoleKey.A:
                    if (pageNumber > 1)
                        Console.WriteLine("No pages left")
                        break;
                    pageNumber--;
                        break;
                    
                case ConsoleKey.RightArrow or ConsoleKey.D:
                    pageNumber--;
                        break;
                
                default:
                    Console.WriteLine("Please enter a valid command.")
                    continue;
            }
        */
        // REMOVENOTE: Not fully symmetrical (if that matters?)
        Console.WriteLine(
            $"""
                ┌─────────────────┬────────────────────┬┬───────────────────┬──────────────────┐
                │  Search - F1    │  Categories - F2   ││  Cart ({}) - F3   │  Checkout - F4   │             
                ├─────────────────┴────────────────────┼┼───────────────────┴──────────────────┤
                │ {}                                   ││ {}                                   │ 
                │                                      ││                                      │                            
                │ {}                                   ││ {}                                   │            
                │                                      ││                                      │            
                │ {}                                   ││ {}                                   │            
                │                                      ││                                      │            
                │ {}                                   ││ {}                                   │            
                │                                      ││                                      │            
                │ {}                                   ││ {}                                   │            
                │                                      ││                                      │            
                │ {}                                   ││ {}                                   │            
                │                                      ││                                      │                                        
                ├──────────────────────────────────────┼┼──────────────────────────────────────┤
                │   ← A/Left (Previous page)                           (Next page) D/Right →   │
                └──────────────────────────────────────────────────────────────────────────────┘
            """
        );
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
