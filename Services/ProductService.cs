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
            throw new Exception("Failed to create product", ex);
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
        List<Product> tempProducts = new List<Product>();
        if (products is null)
        {
            tempProducts = await GetAllProducts();
        }
        else
        {
            tempProducts = products;
        }
        List<List<Product>> splitProducts = new List<List<Product>>();
        List<Product> tempList = new List<Product>();
        int i = 0;
        if (tempProducts.Count < 40)
        {
            foreach (Product product in tempProducts)
            {
                tempList.Add(product);
            }
            splitProducts.Add(tempList);
        }
        else
        {
            foreach (Product product in tempProducts)
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
        }

        return new List<List<Product>>(splitProducts.ToList());
    }
}
