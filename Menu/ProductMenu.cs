namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductsMenu : Menu
{
    private List<string> p = new List<string>();
    // csharpier-ignore-start
    public override void Display()
    {
        // Format menu later.
        Console.WriteLine(
            $"""
                ┌──────────────────────────────────────────────────────────────────────────────┐
                │ Select an item to add to your cart:                                   AAAL © │
                ├──────────────────────────────────────────────────────────────────────────────┤
                │                                                                              │
                │    1. {p[0]}              ★☆                                                │
                │    2. {p[1]}                                                                 │
                │    3. {p[2]}                                                                 │
                │    4. {p[3]}                                                                 │
                │    5. {p[4]}                                                                 │
                │    6. {p[5]}                                                                 │
                │    7. {p[6]}                                                                 │
                │    8. {p[7]}                                                                 │
                │    9. {p[8]}                                                                 │
                │                                                                              │
                ├──────────────────────────────────────────────────────────────────────────────┤
                │    ← Previous Page (Left Arrow)                 (Right Arrow) Next Page →    │
                └──────────────────────────────────────────────────────────────────────────────┘
            """
        );
    }
    // csharpier-ignore-end
    public void ChangeContent(List<string> products)
    {
        p = products;
        return;
    }
}
