namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartHandler
{
    private readonly ICartService _cartService;
    private readonly CartMenu _cartMenu;
    private readonly BaseMenu _baseMenu;
    private List<CartItem> _cartItems;
    private int index = 0;

    public CartHandler(ICartService cartService)
    {
        _cartService = cartService;
        _cartMenu = new CartMenu();
        _baseMenu = new BaseMenu();
        _cartItems = new List<CartItem>();
    }

    public async Task HandleShowCart(List<CartItem>? cartItems = null)
    {
        Guid currentUserId = SessionHandler.GetCurrentUserId();
        index = 0;
        if (cartItems is null)
        {
            var userCart = await _cartService.GetShoppingCart(currentUserId);
            _cartItems = _cartService.ConvertCartToList(userCart);
        }
        else { }
        while (true)
        {
            _cartMenu.EditContent(_cartItems);
            _cartMenu.Display();

            var key = CustomKeyReader.GetKeyOrBuffered();

            if (key.Key.Equals(ConsoleKey.Escape))
            {
                break;
            }

            if (key.Key.Equals(ConsoleKey.Enter))
            {
                //Implement call to checkout functionality
            }

            // Handle cart item selection
            string fullLine = CustomKeyReader.GetBufferedLine();

            if (!int.TryParse(fullLine, out int choice))
            {
                Utilities.WriteLineWithPause("You have to enter a number.");
                continue;
            }

            if (choice > 0 && choice <= _cartItems.Count)
            {
                CartItem selectedItem = _cartItems[choice - 1];
                await HandleCartItemSelection(selectedItem);
            }
            else
            {
                Utilities.WriteLineWithPause("Please select an item from the cart.");
                continue;
            }
        }
    }

    private async Task HandleCartItemSelection(CartItem item)
    {
        while (true)
        {
            _cartMenu.DisplayCartItems(item);
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D1)
            {
                Console.Write("Enter new quantity: ");
                if (int.TryParse(Console.ReadLine(), out int newQuantity))
                {
                    await _cartService.UpdateProductQuantity(item);
                }
                return;
            }
            else if (key == ConsoleKey.D2)
            {
                await _cartService.RemoveItemShoppingCart(item.ProductId);

                return;
            }
            else if (key == ConsoleKey.Escape)
            {
                return;
            }

            Utilities.WriteLineWithPause("Please select a valid option.");
        }
    }

    // private async Task HandleCartItemRemoval(CartItem item) { }
    //   private int GetNumberFromKey(ConsoleKey key)
    //     {
    //         return key switch
    //         {
    //             ConsoleKey.D1 or ConsoleKey.NumPad1 => 1,
    //             ConsoleKey.D2 or ConsoleKey.NumPad2 => 2,
    //             ConsoleKey.D3 or ConsoleKey.NumPad3 => 3,
    //             ConsoleKey.D4 or ConsoleKey.NumPad4 => 4,
    //             ConsoleKey.D5 or ConsoleKey.NumPad5 => 5,
    //             ConsoleKey.D6 or ConsoleKey.NumPad6 => 6,
    //             ConsoleKey.D7 or ConsoleKey.NumPad7 => 7,
    //             ConsoleKey.D8 or ConsoleKey.NumPad8 => 8,
    //             ConsoleKey.D9 or ConsoleKey.NumPad9 => 9,
    //             ConsoleKey.D0 or ConsoleKey.NumPad0 => 0,
    //             _ => -1
    //         };
    //     }
}
