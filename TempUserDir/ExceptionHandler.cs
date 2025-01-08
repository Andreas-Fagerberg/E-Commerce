namespace E_commerce_Databaser_i_ett_sammanhang;

public static class ExceptionHandler
{
    /// <summary>
    /// Handles exceptions and outputs appropriate messages to the console.
    /// </summary>
    public static void Handle(Exception ex)
    {
        switch (ex)
        {
            case ArgumentException argEx:
                Console.WriteLine($"Validation Error: {argEx.Message}");
                break;

            case InvalidOperationException opEx:
                Console.WriteLine($"Error: {opEx.Message}");
                break;

            default:
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                break;
        }
    }
}