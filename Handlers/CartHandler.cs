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

    public async Task HandleShowCart()
    {
        Guid currentUserId = SessionHandler.GetCurrentUserId();
        index = 0;

        _cartItems = _cartService.ConvertCartToList();

        while (true)
        {
            _cartMenu.EditContent(_cartItems);
            _cartMenu.Display();

            var key = CustomKeyReader.GetKeyOrBuffered();

            if (key.Key.Equals(ConsoleKey.Escape))
            {
                break;
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
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }

            if (key.Key == ConsoleKey.D1)
            {
                Console.Write("Enter new quantity: ");
                string? choice = Console.ReadLine();
                if (int.TryParse(choice, out int newQuantity))
                {
                    await _cartService.UpdateProductQuantity(item, newQuantity);
                    _cartItems = _cartService.ConvertCartToList();
                    return;
                }
                Utilities.WriteLineWithPause("Please enter a number.");
            }
            else if (key.Key == ConsoleKey.D2)
            {
                await _cartService.RemoveItemShoppingCart(item.ProductId);
                _cartItems = _cartService.ConvertCartToList();
                return;
            }

            Utilities.WriteLineWithPause("Please select a valid option.");
        }
    }

    public async Task CartItemSelection()
    {
        int selectionTracker = 0;
        _cartItems = _cartService.ConvertCartToList();
        _cartMenu.EditContent(_cartItems);
        _cartMenu.SetLine(selectionTracker);
        _cartMenu.Display();

        while (true)
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            bool requiresRedraw = false;

            switch (input)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.UpArrow:
                    selectionTracker--;
                    if (selectionTracker < 0)
                    {
                        selectionTracker = _cartItems.Count - 1;
                    }
                    requiresRedraw = true;
                    break;

                case ConsoleKey.DownArrow:
                    selectionTracker++;
                    if (selectionTracker > 39 || selectionTracker > _cartItems.Count - 1)
                    {
                        selectionTracker = 0;
                    }
                    requiresRedraw = true;
                    break;
                case ConsoleKey.Enter:
                    await HandleCartItemSelection(_cartItems[selectionTracker]);
                    _cartItems = _cartService.ConvertCartToList();
                    requiresRedraw = true;
                    break;
            }

            if (requiresRedraw)
            {
                _cartMenu.EditContent(_cartItems);
                _cartMenu.SetLine(selectionTracker);
                _cartMenu.Display();
            }
        }
    }

    public static class CustomKeyReader
    {
        private static string buffer = "";

        public static ConsoleKeyInfo GetKeyOrBuffered()
        {
            // First read the new key
            var key = Console.ReadKey(true);

            // If it's not an arrow key, add to buffer and display
            if (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow)
            {
                buffer += key.KeyChar;
            }

            return key;
        }

        public static string GetBufferedLine()
        {
            string result = buffer;
            buffer = "";
            Console.WriteLine(result);
            result += Console.ReadLine();
            return result;
        }
    }
}
