using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

// If items in cart is changed to 0 remove said item or dont give the option to 0 and rather give the option to remove the item itself.
//använd dictionary för att hantera quantity, kolla listan, finns redan item i dictionary plussa på.
//Kolla cart från databasen efter inlogg.
//skapa command för att populera lista som sen kopplas till dictionary. Kan ha flera, tex en där alla items finns, en där dne försöker lägga in items som är slut, etc.
//Lägga till felhantering
//Kanske ta bort listan, lägga till direkt i dictionary.

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
            var dbCart = await _context.Carts.AsNoTracking().Include (c => c.Product).Where(sc => sc.UserId == userId).ToListAsync();
            UserCart.Clear();
            foreach (var item in dbCart)
            {
                Console.WriteLine($"Retrieved: ProductId={item.ProductId}, Quantity={item.Quantity}, Price={item.Price}, Name={item.Name}");
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

            // _context.Carts.RemoveRange(existingItems);

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
                    TotalPrice = item.Value.Quantity * item.Value.Price // Calculate the TotalPrice
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

        //Method that can be used to sum total cost in cart.


        //Make a method to turn the cart(dictionary) to a list of list holding the products.
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
