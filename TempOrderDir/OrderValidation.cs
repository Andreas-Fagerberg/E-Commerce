
namespace E_commerce_Databaser_i_ett_sammanhang;

public static class OrderValidation
{
    /// <summary>
    ///  Validates the entire DTO for basic rules.
    /// </summary>
    public static void ValidateOrder(CreateOrderDTO dto)
    {
        if (dto.Products.Any() == false || dto.Products == null)
        {
            throw new InvalidOperationException("The order does not contain any products.");
        }
    }

    /// <summary>
    /// Validates that the products in the DTO exists in the database.
    /// </summary>
    public static void ValidateProducts(IEnumerable<OrderProductDTO> products, Dictionary<Guid, decimal> productPrices)
    {
        foreach (var product in products)
        {
            if (productPrices.ContainsKey(product.ProductId) == false)
            {
                throw new InvalidOperationException($"Product with ID {product.ProductId} was not found in the database.");
            }

            if (product.Quantity <= 0)
            {
                throw new InvalidOperationException($"Product with ID: {product.ProductId} has an invalid quantity.");
            }
        }
    }
}