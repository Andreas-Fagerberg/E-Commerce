namespace E_commerce_Databaser_i_ett_sammanhang;

public class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int Rating { get; set; }
    public required bool Available = false;
    public OrderProduct OrderProduct { get; set; }
    public ICollection<Cart> Carts { get; set; } = new List<Cart>(); // Build a relationsship
    // between OrderProduct and Product (Many-to-One)
}
