namespace E_commerce_Databaser_i_ett_sammanhang
{
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
