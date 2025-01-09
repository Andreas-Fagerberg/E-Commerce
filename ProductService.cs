using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class ProductService : IProductService
{
    private readonly EcommerceContext _ecommerceContext;

    public ProductService(EcommerceContext ecommerceContext)
    {
        _ecommerceContext = ecommerceContext;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        try
        {
            var products = await _ecommerceContext.Products.ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to retrieve products from database", ex);
        }
    }

    public async Task<List<Product>> SearchProducts(
        string? productName = null,
        string? category = null
    )
    {
        try
        {
            var searchProduct = _ecommerceContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(productName))
            {
                searchProduct = searchProduct.Where(p =>
                    p.Name.ToLower().Contains(productName.ToLower())
                );
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                searchProduct = searchProduct.Where(p =>
                    p.Category.ToLower().Contains(category.ToLower())
                );
            }

            var products = await searchProduct.ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while searching products", ex);
        }
    }
    public async Task<Product> CreateProduct(Product product)
    {
        try
        {
            // Add product to our database
            await _ecommerceContext.Products.AddAsync(product);
            // Save changes to our database
            await _ecommerceContext.SaveChangesAsync();

            return product;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create product", ex);
        }
    }
}
