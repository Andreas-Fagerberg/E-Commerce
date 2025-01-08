using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang
// If items in cart is changed to 0 remove said item or dont give the option to 0 and rather give the option to remove the item itself.
//använd dictionary för att hantera quantity, kolla listan, finns redan item i dictionary plussa på.
//Kolla cart från databasen efter inlogg.
//skapa command för att populera lista som sen kopplas till dictionary. Kan ha flera, tex en där alla items finns, en där dne försöker lägga in items som är slut, etc.
//Lägga till felhantering
//Kanske ta bort listan, lägga till direkt i dictionary.
{
    public class ShoppingCartService : IShoppingCartService
    {
        private Dictionary<int, (int Quantity, decimal Price)> Cart;

        private readonly EcommerceContext _context;

        public ShoppingCartService(EcommerceContext context)
        {
            Cart = new Dictionary<int, (int Quantity, decimal Price)>();
            _context = context;
        }

        //Called when adding a product to the cart, updates quantity if item already exist.

        public async Task AddToShoppingCart(Guid userId, int productId, int quantity, decimal price)
        {
            if (Cart.ContainsKey(productId))
            {
                var currentItem = Cart[productId];
                Cart[productId] = (currentItem.Quantity + quantity, price);
            }
            else
            {
                Cart[productId] = (quantity, price);
            }

            await Task.CompletedTask;
        }

        //For the user to manually change the quantity of a certain item in the shoppingcart, removing the item if the quantity changes to zero
        public async Task<Dictionary<int, (int Quantity, decimal Price)>> HandleProductQuantity(
            Guid userId,
            int productId,
            int quantity
        )
        {
            if (quantity <= 0)
            {
                await RemoveItemShoppingCart(productId);
            }
            else if (Cart.ContainsKey(productId))
            {
                var currentItem = Cart[productId];
                Cart[productId] = (quantity, currentItem.Price);
            }
            return Cart;
        }

        public async Task<Dictionary<int, (int Quantity, decimal Price)>> RemoveItemShoppingCart(
            int productId
        )
        {
            if (Cart.ContainsKey(productId))
            {
                Cart.Remove(productId);
            }

            return Cart;
        }

        public async Task<Dictionary<int, (int Quantity, decimal Price)>> GetShoppingCart(
            Guid userId
        )
        {
            var dbCart = await _context.Carts.Where(sc => sc.UserId == userId).ToListAsync();
            Cart.Clear();
            foreach (var item in dbCart)
            {
                Cart[item.ProductId] = (item.Quantity, item.Price);
            }

            return Cart;
        }

        public async Task Checkout(Guid userId)
        {
            var cartItems = Cart.Select(item => new ShoppingCart
            {
                UserId = userId,
                ProductId = item.Key,
                Quantity = item.Value.Quantity,
                Price = item.Value.Price,
                TotalPrice = item.Value.Quantity * item.Value.Price,
            });

            await _context.Carts.AddRangeAsync(cartItems);
            await _context.SaveChangesAsync();

            Cart.Clear();
        }

        //Method that can be used to sum total cost in cart.
        public decimal TotalCost()
        {
            return Cart.Sum(item => item.Value.Quantity * item.Value.Price);
        }
    }
}
