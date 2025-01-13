using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommands : MenuBaseCommand
{
    private CartHandler? _cartHandler;

    private List<string> _menuContent = new List<string> { "Show all items in cart" };

    private BaseMenu baseMenu = new BaseMenu();

    public CartCommands(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
    )
        : base(
            triggerKey,
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService
        ) { }

    public override async Task Execute()
    {
        _cartHandler = new CartHandler(cartService);
        while (true)
        {
            baseMenu.EditContent(_menuContent);
            baseMenu.Display();

            var input = Console.ReadKey(true).Key;

            try
            {
                switch (input)
                {
                    case ConsoleKey.D1:
                        await _cartHandler.HandleShowCart();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("Please select an option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                continue;
            }
        }
    }
}
