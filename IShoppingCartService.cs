namespace E_commerce_Databaser_i_ett_sammanhang.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//hämta produkter från en lista i products.
//Lägg en index på items i listan.
//AddToCart, removefromcart, savecart.
public interface ICartService
{
    Task<Dictionary<int, (int Quantity, decimal Price)>> GetShoppingCart(Guid userId);
    Task AddToShoppingCart( int productId, int quantity, decimal price);

    Task<Dictionary<int, (int Quantity, decimal Price)>> RemoveItemShoppingCart(int productId);

    Task<Dictionary<int, (int Quantity, decimal Price)>> HandleProductQuantity(
        Guid userId,
        int productId,
        int quantity
    );

    Task Checkout(Guid userId);
}

