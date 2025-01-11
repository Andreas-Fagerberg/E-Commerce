namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Summarizes detailed order information for display or external use.
/// </summary>
public class OrderProductResponse
{
    public required string ProductName { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}