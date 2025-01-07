using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommand : BaseCommand
{
    private readonly IShoppingCartService _shoppingCartService;
    CartMenu cartMenu = new CartMenu();

    public CartCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IShoppingCartService shoppingCartService
    )
        : base(triggerKey, userService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public override async Task Execute(Guid? currentUserId)
    {
        int choice = 0;
        bool cartChoice = true;
        while (cartChoice)
        {
            cartMenu.Display();
            switch (choice)
            {
                case 1:
                    _shoppingCartService.AddToShoppingCart();
                    break;
                case 2:
                _shoppingCartService.RemoveItemShoppingCart();
                    break;
                case 3:
                _shoppingCartService.GetShoppingCart();
                    break;
                case 4:
                _shoppingCartService.
                    break;
            }
        }
    }
}
