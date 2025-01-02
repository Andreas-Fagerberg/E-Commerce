namespace E_commerce_Databaser_i_ett_sammanhang;

public interface IOrderService
{
    /// <summary>
    /// Places a new order and returns the created order details.
    /// </summary>
    Task<OrderResponse> PlaceOrder(CreateOrderDTO orderDTO);

    /// <summary>
    /// Retrieve details of a specific order by its ID.
    /// </summary>    
    Task<OrderResponse> GetOrderDetails(Guid orderId);

    /// <summary>
    /// Retrieve all orders for a specific user.
    /// </summary>
    Task<IEnumerable<OrderResponse>> GetUserOrders(Guid userId);
}