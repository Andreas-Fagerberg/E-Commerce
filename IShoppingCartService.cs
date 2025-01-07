using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    //hämta produkter från en lista i products.
    //Lägg en index på items i listan.
    //AddToCart, removefromcart, savecart.
    public interface IShoppingCartService
    {
        Task<List<ShoppingCart>> GetShoppingCart(
            Guid userId,
            int productId,
            int quantity,
            decimal price
        );
        Task AddToShoppingCart(Guid userId, int productId, int quantity, decimal price);

        Task<List<ShoppingCart>> RemoveItemShoppingCart(int productid);

        Task<List<ShoppingCart>> HandleProductQuantity(Guid userId, int productId, int quantity);

        Task Checkout(Guid userId);
    }
}
