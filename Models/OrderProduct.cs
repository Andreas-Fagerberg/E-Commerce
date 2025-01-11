namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents the physical products associated with an order.
/// </summary>
public class OrderProduct
{
    public Guid OrderId { get; set; } // CK
    public int ProductId { get; set; } // CK
    public int Quantity { get; set; }

    // Navigation properties (WIP)
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}