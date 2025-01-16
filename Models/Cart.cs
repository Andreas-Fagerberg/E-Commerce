namespace E_commerce_Databaser_i_ett_sammanhang;

public class Cart
{
    public int CartId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public User? User { get; set; }
    public Product Product { get; set; }
}
