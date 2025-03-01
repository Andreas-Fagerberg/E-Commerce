namespace E_commerce_Databaser_i_ett_sammanhang;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> SearchProducts(string? productName = null, string? category = null);
    Task<Product> CreateProduct(Product product);
    Task<bool> RemoveProduct(int? productId);
    Task<List<List<Product>>> GetProductLists(List<Product>? products = null);
}
