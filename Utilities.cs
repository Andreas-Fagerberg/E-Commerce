namespace E_commerce_Databaser_i_ett_sammanhang;

public static class Utilities
{
    public static void ClearAndWriteLine(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }

    public static void WriteLineWithPause(string message, int millisecondsDelay = 1000)
    {
        Console.WriteLine(message);
        Thread.Sleep(millisecondsDelay);
    }
}
