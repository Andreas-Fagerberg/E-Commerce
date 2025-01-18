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


