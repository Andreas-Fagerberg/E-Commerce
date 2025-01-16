namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents the physical products associated with an order.
/// </summary>
public class OrderProduct
{
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
