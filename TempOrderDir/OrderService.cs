
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

        var productPrices = await GetProductPrices(productIds);

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
    /// Can be used to e.g. display order confirmation in the UI.
    /// </summary>  
    public async Task<OrderResponse> GetOrderDetails(Guid orderId)
    {
        // Fetch the order and related data
        var order = await ecommerceContext.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            throw new InvalidOperationException($"Order with ID {orderId} not found.");
        }

        // Map product details
        var productDetails = order.OrderProducts.Select(op => new OrderProductResponse
        {
            ProductName = op.Product?.Name ?? "Unknown Product", // Handle potential null Product
            Quantity = op.Quantity,
            UnitPrice = op.Product?.Price ?? 0.0M
        }).ToList();

        // Create and return the OrderResponse
        return new OrderResponse
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalCost = order.TotalCost,
            Products = productDetails
        };
    }

    /// <summary>
    /// Retrieve all orders associated with a specific user
    /// and provides a summary of each order.
    /// </summary>
    public async Task<List<OrderResponse>> GetUserOrders(Guid userId)
    {
        UserValidation.CheckForValidUser(userId);

        // Fetch all orders for the User.
        var orders = await ecommerceContext.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        if (orders.Any() == false)
        {
            return new List<OrderResponse>();
        }
        // Map orders to OrderResponse
        var orderResponses = orders.Select(order => new OrderResponse
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalCost = order.TotalCost
        }).ToList();

        return orderResponses;
    }

    #region Helper Methods

    private async Task<Dictionary<int, decimal>> GetProductPrices(IEnumerable<int> productIds)
    {
        return await ecommerceContext.Products
            .Where(p => productIds.Contains(p.ProductId))
            .ToDictionaryAsync(p => p.ProductId, p => p.Price);
    }
    #endregion
}