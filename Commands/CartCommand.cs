using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommand : BaseCommand
{
    private readonly IShoppingCartService _shoppingCartService;
    CartMenu cartMenu = new CartMenu();
    private IMenuService _menuService;

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
      
        bool cartChoice = true;
        while (cartChoice)
        {
            cartMenu.Display();

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
