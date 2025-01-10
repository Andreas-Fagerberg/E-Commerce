namespace E_commerce_Databaser_i_ett_sammanhang;

public static class CustomKeyReader
{
    private static string buffer = "";

    public static ConsoleKeyInfo GetKeyOrBuffered()
    {
        // First read the new key
        var key = Console.ReadKey(true);

        // If it's not an arrow key, add to buffer and display
        if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
        {
            buffer += key.KeyChar;
            Console.Write(key.KeyChar);
        }

        return key;
    }

    public static string GetBufferedLine()
    {
        Console.Write(buffer);
        string result = buffer;
        buffer = "";
        result += Console.ReadLine();
        return result;
    }
}
