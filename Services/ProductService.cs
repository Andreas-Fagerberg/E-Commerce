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
            System.Console.WriteLine();
            Console.ReadLine();

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
                var searchTerms = productName.Trim().ToLower().Split(' ');
                searchProduct = searchProduct.Where(p =>
                    searchTerms.All(term => p.Name.ToLower().Contains(term))
                );
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                var searchTerms = category.Trim().ToLower().Split(' ');

                searchProduct = searchProduct.Where(p =>
                    searchTerms.All(term => p.Category.ToLower().Contains(term))
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
            await _ecommerceContext.Products.AddAsync(product);

            await _ecommerceContext.SaveChangesAsync();

            return product;
        }
        catch (Exception ex)
        {
            throw new Exception(" Failed to create product", ex);
        }
    }

    public async Task<bool> RemoveProduct(int? productId)
    {
        try
        {
            var product = await _ecommerceContext.Products.FindAsync(productId);

            if (product == null)
            {
                Console.WriteLine($"No product found with ID: {productId}");
                return false;
            }

            _ecommerceContext.Products.Remove(product);
            await _ecommerceContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to remove product", ex);
        }
    }

    public async Task<List<List<Product>>> GetProductLists(List<Product>? products = null)
    {
        var tempProducts = products ?? await GetAllProducts();

        const int batchSize = 40;

        if (tempProducts.Count <= batchSize)
        {
            return new List<List<Product>> { new List<Product>(tempProducts) };
        }

        return tempProducts
            .Select((product, index) => new { product, index })
            .GroupBy(x => x.index / batchSize)
            .Select(g => g.Select(x => x.product).ToList())
            .ToList();
    }
}
