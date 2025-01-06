using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_Databaser_i_ett_sammanhang.Models;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    //hämta produkter från en lista i products.
    //Lägg en index på items i listan.
    //AddToCart, removefromcart, savecart.
    public interface IShoppingCartService
    {
        Task <List<Shopping_Cart>> GetShoppingCart(Guid userId);
        Task AddToShoppingCart(Guid userId, int productId, int quantity, int price );

        Task<List<Shopping_Cart>> RemoveItemShoppingCart(int productid);

        Task<List<Shopping_Cart>> HandleProductQuantity(Guid userId, int productId, int quantity);
        
    }
}
