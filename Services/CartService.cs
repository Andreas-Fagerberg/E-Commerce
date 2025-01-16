using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

// csharpier-ignore-start
    public class CartService : ICartService
    {
        private Dictionary<int, (int Quantity, decimal Price, string Name)> UserCart;

        private readonly EcommerceContext _context;

        public CartService(EcommerceContext context)
        {
            UserCart = new Dictionary<int, (int Quantity, decimal Price, string Name)>();
            _context = context;
        }


          public async void RemoveAllItems(Guid userId)
          {
            UserCart.Clear();
             var existingItems = await _context.Carts
            .Where(c => c.UserId == userId)
            .ToListAsync();

            _context.Carts.RemoveRange(existingItems);

             await _context.SaveChangesAsync();

          }


        //Called when adding a product to the cart, updates quantity if item already exist.

        public async Task AddToShoppingCart(Product product, int quantity)
        {

            if (UserCart.ContainsKey(product.ProductId))
            {
                var currentItem = UserCart[product.ProductId];
                UserCart[product.ProductId] = (
                    currentItem.Quantity + quantity,
                    product.Price,
                    product.Name
                );
            }
            else
            {
                UserCart[product.ProductId] = (quantity, product.Price, product.Name);
            }
            await Task.CompletedTask;
        }

        //For the user to manually change the quantity of a certain item in the shoppingcart, removing the item if the quantity changes to zero
        public async Task<Dictionary<int, (int Quantity, decimal Price, string Name)>> UpdateProductQuantity(CartItem item, int quantity)
        {
            if (item.Quantity <= 0)
            {
                await RemoveItemShoppingCart(item.ProductId);
            }
            else if (UserCart.ContainsKey(item.ProductId))
            {
                var currentItem = UserCart[item.ProductId];
                UserCart[item.ProductId] = (item.Quantity = quantity, currentItem.Price, currentItem.Name);
            }
            return UserCart;
        }

        public async Task<Dictionary<int, (int Quantity, decimal Price, string Name)>> RemoveItemShoppingCart(int productId)
        {
            if (UserCart.ContainsKey(productId))
            {
               UserCart.Remove(productId);
            }

            return UserCart;
        }

        public async Task<Dictionary<int, (int Quantity, decimal Price, string Name)>> GetShoppingCart(Guid userId)
        {
            var dbCart = await _context.Carts.Include (c => c.Product).Where(sc => sc.UserId == userId).ToListAsync();
            UserCart.Clear();
            foreach (var item in dbCart)
            {
                if(UserCart.ContainsKey(item.ProductId))
                {
                    var existingItem = UserCart[item.ProductId];
                    UserCart[item.ProductId] = (existingItem.Quantity, item.Price, item.Name);

                }
                else
                {
                    UserCart[item.ProductId] = (item.Quantity, item.Price, item.Name);
                }

            }

            return UserCart;
        }

        public async Task SaveCartToDatabase(Guid userId)
        {
            try {
                 var existingItems = await _context.Carts
            .Where(c => c.UserId == userId)
            .ToListAsync();

           var cartItemsToAdd = new List<Cart>();
           foreach (var item in UserCart)
        {
           var existingCartItem = existingItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == item.Key);
            if (existingCartItem != null)
            {

                 existingCartItem.Quantity = item.Value.Quantity;
                 existingCartItem.Price = item.Value.Price;
            }
            else
            {

           var cartItems = new Cart
                {
                    UserId = userId,
                    ProductId = item.Key,
                    Quantity = item.Value.Quantity,
                    Name = item.Value.Name,
                    Price = item.Value.Price,
                    TotalPrice = item.Value.Quantity * item.Value.Price
                };


            await _context.Carts.AddRangeAsync(cartItems);
            }
        }
         var itemsToRemove = existingItems.Where(c => !UserCart.ContainsKey(c.ProductId)).ToList();
        if (itemsToRemove.Any())
        {
            _context.Carts.RemoveRange(itemsToRemove);
        }
            await _context.SaveChangesAsync();

            UserCart.Clear();

        }
           catch (Exception ex)
        {
        Console.WriteLine($"Failed to save cart: {ex.Message}");
       if (ex.InnerException != null)
       {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
       }
         Console.WriteLine(ex.StackTrace);
         }
         }


        public List<CartItem> ConvertCartToList()
        {
             return UserCart
                .Select(item => new CartItem
                {
                    ProductId = item.Key,
                    Quantity = item.Value.Quantity,
                    Price = item.Value.Price,
                    Name = item.Value.Name,
                })
                .ToList();
        }



}
