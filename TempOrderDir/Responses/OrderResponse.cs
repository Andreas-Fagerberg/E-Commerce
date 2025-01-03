namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Summarizes the order details for display or external use.
/// </summary>
public class OrderResponse
{
    public required Guid OrderId { get; set; }
    public required DateTime CreatedAt { get; set; }

    // String instead of Enum for easier UI operations.
    public required string Status { get; set; }
    public required decimal TotalCost { get; set; }
    public List<OrderProductResponse> Products { get; set; } = [];
}
