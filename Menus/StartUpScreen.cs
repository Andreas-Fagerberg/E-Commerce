using System.Text;

public class StartUpScreen
{
    // csharpier-ignore-start
    public static void Display()
    {
        int y = 10;
        StringBuilder _buffer = new StringBuilder();
        _buffer.AppendLine(@"                          _____       _____       _____   _ ");
        _buffer.AppendLine(@"                         / __  |     / __  |     / __  | | |");
        _buffer.AppendLine(@"                        / /__| |    / /__| |    / /__| | | |");
        _buffer.AppendLine(@"                       / /___| |   / /___| |   / /___| | | |");
        _buffer.AppendLine(@"                      / /    | |  / /    | |  / /    | | | |_____");
        _buffer.AppendLine(@"                     /_/     |_/ /_/     |_/ /_/     |_/ \_______|");
        _buffer.AppendLine(@" _____   _   _   ____     _    _    _____    _______   _____    _____   ______    _____ ");
        _buffer.AppendLine(@"|_   _| | \ | | |  __ \  | |  | |  /  ____| |__   __| |  __ \  |_   _| |  ____|  / ____|");
        _buffer.AppendLine(@"  | |   |  \| | | |  | | | |  | | |  (___      | |    | |__) |   | |   | |__    | (___  ");
        _buffer.AppendLine(@"  | |   | . ` | | |  | | | |  | |  \___  \     | |    |  _  /    | |   |  __|    \___ \ ");
        _buffer.AppendLine(@" _| |_  | |\  | | |__| | | |__| |  ____)  |    | |    | | \ \   _| |_  | |____   ____) |");
        _buffer.AppendLine(@"|_____| |_| \_| |_____/   \____/  |_____ /     |_|    |_|  \_\ |_____| |______| |_____/ ");

        Console.CursorVisible = false;
        Console.Clear();
        string[] lines = _buffer.ToString().Split(Environment.NewLine);
        foreach (string line in lines)
        {
            Console.SetCursorPosition(20, y);
            Console.WriteLine(line);
            y++;
            Thread.Sleep(200);
        }
        Thread.Sleep(1000);
        Console.CursorVisible = true;
    }
}


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

//        _____
//      / __  |
//     / /__| |
//    / /___| |
//   / /    | |
//  /_/     |_/


//      _____   _
//     / __  | | |
//    / /__| | | |
//   / /___| | | |
//  / /    | | | |_______
// /_/     |_/ \_________/

//  _____   _   _   ____  _    _  _____ _______ _____  _____ ______  _____ 
// |_   _| | \ | | | __ \| |  | |/ ____|__   __|  __ \|_   _|  ____|/ ____|
//   | |   |  \| | |  | | |  | | (___    | |  | |__) | | | | |__  | (___  
//   | |   | . ` | |  | | |  | |\___ \   | |  |  _  /  | | |  __|  \___ \ 
//  _| |_  | |\  | |__| | |__| |____) |  | |  | | \ \ _| |_| |____ ____) |
// |_____| |_| \_|_____/ \____/|_____/   |_|  |_|  \_\_____|______|_____/ 