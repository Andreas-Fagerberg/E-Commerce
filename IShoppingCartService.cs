using E_commerce_Databaser_i_ett_sammanhang.Models;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    //hämta produkter från en lista i products.
    //Lägg en index på items i listan.
    //AddToCart, removefromcart, savecart.
    public interface ICartService
    {
        Task<List<ShoppingCart>> GetShoppingCart(Guid userId);
        Task AddToShoppingCart(Guid userId, int productId, int quantity, int price);

        Task<List<ShoppingCart>> RemoveItemShoppingCart(int productid);

        Task<List<ShoppingCart>> HandleProductQuantity(Guid userId, int productId, int quantity);

        Task Checkout();
    }
}
