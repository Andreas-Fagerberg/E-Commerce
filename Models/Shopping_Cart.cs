using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace E_commerce_Databaser_i_ett_sammanhang.Models
{
    public class ShoppingCart
    {
        //public int Cart_Id {get; private set;}
        public Guid UserId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }

        public decimal TotalPrice { get; private set; }

        public ShoppingCart(Guid userId, int productId, int quantity, decimal totalPrice)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }
    }
}

//var allProducts = Context.Products.ToList();
//var p = context.AllProducts.FirstOrDefault(p => p.Product == "TV");
//var price = context.AllProducts.Where(p => p.Price < 100).ToList();
