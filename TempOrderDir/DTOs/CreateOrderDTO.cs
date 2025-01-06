namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Captures the data required to place an order.
/// </summary>
public class CreateOrderDTO
{
    public required Guid UserId { get; set; }
    public List<OrderProductDTO> Products { get; set; } = [];
}