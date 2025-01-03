
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class OrderService : IOrderService
{
    private readonly EcommerceContext ecommerceContext;
    public OrderService(EcommerceContext ecommerceContext)
    {
        this.ecommerceContext = ecommerceContext;
    }

    /// <summary>
    /// Creates a new order and returns the order details.
    /// </summary>
    public async Task<OrderResponse> CreateOrder(CreateOrderDTO dto)
    {
        UserValidation.CheckForValidUser(dto.UserId);
        OrderValidation.ValidateOrder(dto);

        // Extract all ProductIds from dto.Products and store them in a list.
        var productIds = dto.Products.Select(p => p.ProductId).ToList();

        // Retrieve product prices from the database.
        var productPrices = await ecommerceContext.Products
            .Where(p => productIds.Contains(p.ProductId)) // Filters by ProductId
            .ToDictionaryAsync(p => p.ProductId, p => p.Price); // Key: ProductId, Value: Price

        decimal totalCost = 0;

        OrderValidation.ValidateProducts(dto.Products, productPrices);

        foreach (var product in dto.Products)
        {
            totalCost += productPrices[product.ProductId] * product.Quantity;
        }

        // Create and save the Order object to the database.
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow,
            Status = Status.Pending,
            TotalCost = totalCost
        };

        try
        {
            await ecommerceContext.Orders.AddAsync(order);
            await ecommerceContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while saving the order: {ex.Message}");
        }

        // Return the order response for accessibility.
        return new OrderResponse
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalCost = totalCost
        };

        // Possibly add more data for the order. E.g. products and their details unit price etc.
    }

    /// <summary>
    /// Retrieve details of a specific order by its ID.
    /// </summary>  
    public Task<OrderResponse> GetOrderDetails(Guid orderId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieve all orders for a specific user.
    /// </summary>
    public Task<IEnumerable<OrderResponse>> GetUserOrders(Guid userId)
    {
        throw new NotImplementedException();
    }


}