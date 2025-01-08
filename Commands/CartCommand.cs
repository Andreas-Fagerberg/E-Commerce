using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommand : MenuBaseCommand
{
    private readonly IShoppingCartService _shoppingCartService;

    private List<string> _menuContent = new List<string>
    {
        "Show all items in cart",
        "Remove items from cart",
    };
    CartMenu cartMenu = new CartMenu();
    private BaseMenu baseMenu = new BaseMenu();
    private IMenuService _menuService;

    public CartCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IShoppingCartService shoppingCartService
    )
        : base(triggerKey, userService, menuService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public override async Task Execute(Guid? currentUserId)
    {
        var cart = await _shoppingCartService.GetShoppingCart(UserId);

      
        bool cartChoice = true;
        while (cartChoice)
        {
            baseMenu.EditContent(_menuContent);

              if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            try{

            switch (choice)
            {
                case 1:
                     _shoppingCartService.RemoveItemShoppingCart();
                    break;
                case 2:
                _shoppingCartService.RemoveItemShoppingCart();
                    break;
                case 3:
               
                    break;
                case 4:
                _shoppingCartService.
                    break;
            }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
