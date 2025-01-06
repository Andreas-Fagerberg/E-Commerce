namespace E_commerce_Databaser_i_ett_sammanhang;
public interface IProductService
{
    Task<ListProductsCommand> GetAllProducts();
    Task<SearchProductCommand> SearchProducts();

}