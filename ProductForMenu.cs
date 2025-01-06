// First, let's create a proper class to represent each product

public class ProductForMenu
{
    private const int MaxNameLength = 40; // Define reasonable constraints

    private string _name;
    public string? Name
    {
        get => _name;
        set =>
            _name =
                value?.Length > MaxNameLength ? value.Substring(0, MaxNameLength) + "..." : value;
    }

    public decimal Price { get; set; }

    public int Rating { get; set; } // 1-5 stars

    public string DisplayRating => new string('★', Rating) + new string('☆', 5 - Rating);
}

public class ProductList
{
    // Now we can create our list with proper typing
    static List<List<ProductForMenu>> productsList = new List<List<ProductForMenu>>();

    public static List<ProductForMenu> GetProducts(int i)
    {
        return productsList[i];
    }

    public static int GetPageAmount()
    {
        return productsList.Count;
    }

    internal static List<ProductForMenu> GetProducts(object index)
    {
        throw new NotImplementedException();
    }
}
