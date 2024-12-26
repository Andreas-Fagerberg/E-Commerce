using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_Databaser_i_ett_sammanhang.Models;

namespace E_commerce_Databaser_i_ett_sammanhang
// If items in cart is changed to 0 remove said item or dont give the option to 0 and rather give the option to remove the item itself.
{
    public class PostgresShoppingCartService : IShoppingCartService
    {
        private List<Shopping_Cart> cartProducts;

        // private List<Product> productList;

        public PostgresShoppingCartService()
        {
            cartProducts = new List<Shopping_Cart>();
        }

        public Shopping_Cart AddToShoppingCart(int userId, int productId, int quantity)
        {
            var cartItem = new Shopping_Cart(userId, productId, quantity);
            cartProducts.Add(cartItem);
            return cartItem;
        }

        public List<Shopping_Cart> HandleProductQuantity(int userId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<Shopping_Cart> RemoveItemShoppingCart(int porductid)
        {
            throw new NotImplementedException();
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
