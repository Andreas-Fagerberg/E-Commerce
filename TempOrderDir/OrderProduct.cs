namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents the physical products associated with an order.
/// </summary>
public class OrderProduct
{
    public Guid OrderId { get; set; } // CK
    public Guid ProductId { get; set; } // CK
    public int Quantity { get; set; } // Ensure that this is greater than 0 during object creation as Fluent API cannot handle this directly.

    // Navigation properties (WIP)
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}