using System.Security.Cryptography.X509Certificates;
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
            await _ecommerceContext.Products.AddAsync(product);

            await _ecommerceContext.SaveChangesAsync();

            return product;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create product", ex);
        }
    }
    public async Task<bool> RemoveProduct(int productId)
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
        if (products is null || products.Count.Equals(0))
        {
            products = await GetAllProducts();
        }
        List<List<Product>> splitProducts = new List<List<Product>>();
        List<Product> tempList = new List<Product>();
        int i = 0;
        foreach (Product product in products)
        {
            if (i >= 39)
            {
                tempList.Add(product);
                splitProducts.Add(tempList);
                i = 0;
                tempList.Clear();
            }
            tempList.Add(product);
            i++;
        }
        return new List<List<Product>>(splitProducts.ToList());
    }
}
