namespace E_commerce_Databaser_i_ett_sammanhang;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public bool Available;
    public User user { get; set; } // Build a relationsship
    // between User and Product (Many-to-One)
}
