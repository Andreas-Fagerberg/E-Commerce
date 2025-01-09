using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang
// If items in cart is changed to 0 remove said item or dont give the option to 0 and rather give the option to remove the item itself.
//använd dictionary för att hantera quantity, kolla listan, finns redan item i dictionary plussa på.
//Kolla cart från databasen efter inlogg.
//skapa command för att populera lista som sen kopplas till dictionary. Kan ha flera, tex en där alla items finns, en där dne försöker lägga in items som är slut, etc.
//Lägga till felhantering
//Kanske ta bort listan, lägga till direkt i dictionary.
{
    public class CartService : ICartService
    {
        private Dictionary<int, (int Quantity, decimal Price, string Name)> UserCart;

        private readonly EcommerceContext _context;

        public CartService(EcommerceContext context)
        {
            UserCart = new Dictionary<int, (int Quantity, decimal Price, string Name)>();
            _context = context;
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
        public async Task<
            Dictionary<int, (int Quantity, decimal Price, string Name)>
        > UpdateProductQuantity(Guid userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                await RemoveItemShoppingCart(productId);
            }
            else if (UserCart.ContainsKey(productId))
            {
                var currentItem = UserCart[productId];
                UserCart[productId] = (quantity, currentItem.Price, currentItem.Name);
            }
            return UserCart;
        }

        public async Task<
            Dictionary<int, (int Quantity, decimal Price, string Name)>
        > RemoveItemShoppingCart(int productId)
        {
            if (UserCart.ContainsKey(productId))
            {
                UserCart.Remove(productId);
            }

            return UserCart;
        }

        public async Task<
            Dictionary<int, (int Quantity, decimal Price, string Name)>
        > GetShoppingCart(Guid userId)
        {
            var dbCart = await _context.Carts.Where(sc => sc.UserId == userId).ToListAsync();
            UserCart.Clear();
            foreach (var item in dbCart)
            {
                UserCart[item.ProductId] = (item.Quantity, item.Price, item.Name);
            }

            return UserCart;
        }

        public async Task SaveCartToDatabase(Guid userId)
        {
            var cartItems = UserCart.Select(item => new Cart
            {
                UserId = userId,
                ProductId = item.Key,
                Quantity = item.Value.Quantity,
                Price = item.Value.Price,
                TotalPrice = item.Value.Quantity * item.Value.Price,
            });

            await _context.Carts.AddRangeAsync(cartItems);
            await _context.SaveChangesAsync();

            UserCart.Clear();
        }

        //Method that can be used to sum total cost in cart.
        public decimal TotalCost()
        {
            return UserCart.Sum(item => item.Value.Quantity * item.Value.Price);
        }
        //Make a method to turn the cart(dictionary) to a list of list holding the products.
        public List<>
    }
}
