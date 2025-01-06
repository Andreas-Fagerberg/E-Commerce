namespace E_commerce_Databaser_i_ett_sammanhang;
public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> SearchProducts(string? productName = null, string? category = null);

}