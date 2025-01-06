using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace E_commerce_Databaser_i_ett_sammanhang.Models
{
    public class Shopping_Cart
    {
        //public int Cart_Id {get; private set;}
        public Guid UserId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }

        public int SummedPrice {get; private set;}

        public Shopping_Cart(Guid userId, int productId, int quantity, int summedPrice)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            SummedPrice = summedPrice;
        }
    }
}

//var allProducts = Context.Products.ToList();
//var p = context.AllProducts.FirstOrDefault(p => p.Product == "TV");
//var price = context.AllProducts.Where(p => p.Price < 100).ToList();
