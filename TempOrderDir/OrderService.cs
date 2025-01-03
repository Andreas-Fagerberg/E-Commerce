
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

        // Validate the input data (orderDTO).
        if (dto.Products.Any() == false)
        {
            throw new InvalidOperationException("The order list does not contain any products.");
        }

        // Materialize the product IDs.
        var productIds = new List<Guid>();
        foreach (var product in dto.Products)
        {
            productIds.Add(product.ProductId);
        }

        // Retrieve product prices from the database.
        var productPrices = await ecommerceContext.Products
            .Where(p => productIds.Contains(p.ProductId)) // Filters by ProductId
            .ToDictionaryAsync(p => p.ProductId, p => p.Price); // Key: ProductId, Value: Price

        decimal totalCost = 0;

        foreach (var product in dto.Products)
        {
            // Ensure ProductId exists in the dictionary.
            if (productPrices.ContainsKey(product.ProductId) == false)
            {
                throw new InvalidOperationException($"Product with ID {product.ProductId} not found in the database.");
            }

            totalCost += productPrices[product.ProductId] * product.Quantity;
        }

        // Create Order object.
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow,
            Status = Status.Pending,
            TotalCost = totalCost
        };

        // Save the order object to the database.
        await ecommerceContext.Orders.AddAsync(order);
        await ecommerceContext.SaveChangesAsync();

        return new OrderResponse
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalCost = totalCost
        };
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