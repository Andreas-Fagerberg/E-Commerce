namespace E_commerce_Databaser_i_ett_sammanhang;

public interface IOrderService
{
    Task<OrderResponse> CreateOrder(CreateOrderDTO orderDTO);
    Task<OrderResponse> GetOrderDetails(Guid orderId);
    Task<List<OrderResponse>> GetUserOrders(Guid userId);
}