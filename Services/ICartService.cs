
namespace E_commerce_Databaser_i_ett_sammanhang
{
    //hämta produkter från en lista i products.
    //Lägg en index på items i listan.
    //AddToCart, removefromcart, savecart.
    public interface ICartService
    {
        Task<Dictionary<int, (int Quantity, decimal Price, string Name)>> GetShoppingCart(
            Guid userId
        );
        Task AddToShoppingCart(Product product, int quantity);

        Task<Dictionary<int, (int Quantity, decimal Price, string Name)>> RemoveItemShoppingCart(
            int productId
        );

        Task<Dictionary<int, (int Quantity, decimal Price, string Name)>> UpdateProductQuantity(
            CartItem cartItem,
            int quantity
        );
        
        public void RemoveAllItems(Guid userId);
        List<CartItem> ConvertCartToList();

        Task SaveCartToDatabase(Guid userId);
    }
}
