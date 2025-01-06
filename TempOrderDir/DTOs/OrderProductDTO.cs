namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents the products included in an order during creation.
/// Allows the handling of product-specific details separately.
/// </summary>
public class OrderProductDTO
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}