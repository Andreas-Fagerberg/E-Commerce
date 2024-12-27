using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_Databaser_i_ett_sammanhang.Models;

namespace E_commerce_Databaser_i_ett_sammanhang
// If items in cart is changed to 0 remove said item or dont give the option to 0 and rather give the option to remove the item itself.
//använd dictionary för att hantera quantity, kolla listan, finns redan item i dictionary plussa på.
//Kolla cart från databasen efter inlogg.
//skapa command för att populera lista som sen kopplas till dictionary. Kan ha flera, tex en där alla items finns, en där dne försöker lägga in items som är slut, etc.
//Lägga till felhantering
{
    public class PostgresShoppingCartService : IShoppingCartService
    {
        private List<Shopping_Cart> cartProducts;
        Dictionary<Guid, int> Cart = new Dictionary<Guid, int>();

        //private List<Product> productList;

        public PostgresShoppingCartService()
        {
            cartProducts = new List<Shopping_Cart>();
        }

        public Shopping_Cart AddToShoppingCart(Guid userId, Guid productId, int quantity)
        {
            if (Cart.ContainsKey(productId))
            {
                Cart[productId] += quantity;
            }
            else
            {
                Cart[productId] = quantity;
            }

            var cartItem = new Shopping_Cart(userId, productId, quantity);
            cartProducts.Add(cartItem);
            return cartItem;
        }

        public List<Shopping_Cart> HandleProductQuantity(Guid userId, Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<Shopping_Cart> RemoveItemShoppingCart(Guid productid)
        {
            if (Cart.ContainsKey(productid))
            {
                Cart.Remove(productid);
            }
        }

        public void Checkout(int userId)
        {
            using (var context = new EcommerceContext())
            {
                foreach (var item in cartProducts)
                {
                    context.Shopping_Cart.Add(item);
                }
                context.SaveChanges();
            }
            // cartProducts.Clear();
        }
    }
}
