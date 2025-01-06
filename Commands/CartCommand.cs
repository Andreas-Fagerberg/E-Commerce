using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommand : BaseCommand
{
    private readonly IShoppingCartService _shoppingCartService;

    public CartCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IShoppingCartService shoppingCartService
    )
        : base(triggerKey, userService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public override async Task Execute(Guid? currentUserId) { }
}
