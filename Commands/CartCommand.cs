using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommand : MenuBaseCommand
{
    private readonly ICartService _cartService;
    private CartHandler _cartHandler;

    private List<string> _menuContent = new List<string>
    {
        "Show all items in cart",
        "Remove items from cart",
    };
    private CartMenu _cartMenu ;
    private BaseMenu baseMenu = new BaseMenu();
    private IMenuService _menuService;

    public CartCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ICartService cartService
    )
        : base(triggerKey, userService, menuService, cartService)
    {
        _cartService = cartService;
    }

    public override async Task Execute(Guid? currentUserId)
    {
        var cart = await _cartService.GetShoppingCart(currentUserId.Value);

      
        bool cartChoice = true;
        while (cartChoice)
        {
            baseMenu.EditContent(_menuContent);
            baseMenu.Display();
            

              if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
             var input = Console.ReadKey(true).Key;

            try{
               
            switch (input)
            {
                case ConsoleKey.D1:
                     await _cartHandler.HandleShowCart();
                    break;
                case ConsoleKey.D2:
             
                    break;
                default: 
                break;    
            }
            }
            catch (Exception ex)
            {


            }
             private int GetNumberFromKey(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.D1 or ConsoleKey.NumPad1 => 1,
                ConsoleKey.D2 or ConsoleKey.NumPad2 => 2,
                ConsoleKey.D3 or ConsoleKey.NumPad3 => 3,
                ConsoleKey.D4 or ConsoleKey.NumPad4 => 4,
                ConsoleKey.D5 or ConsoleKey.NumPad5 => 5,
                ConsoleKey.D6 or ConsoleKey.NumPad6 => 6,
                ConsoleKey.D7 or ConsoleKey.NumPad7 => 7,
                ConsoleKey.D8 or ConsoleKey.NumPad8 => 8,
                ConsoleKey.D9 or ConsoleKey.NumPad9 => 9,
                ConsoleKey.D0 or ConsoleKey.NumPad0 => 0,
                _ => -1
            };
        }
    }
}

