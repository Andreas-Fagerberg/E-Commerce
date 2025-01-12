namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Handles the execution of the user logout process as part of the command pattern.
/// </summary>
public class LogoutCommand : MenuBaseCommand
{
    public LogoutCommand(
        ConsoleKey triggerkey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
    )
        : base(
            triggerkey,
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService
        )
    { }

    public override Task Execute()
    {
        try
        {
            userService.LogoutUser();
            Utilities.WriteLineWithPause($"Logout successful.");
        }
        catch (Exception ex)
        {
            ExceptionHandler.Handle(ex);
        }

        ResetToLoginMenu();
        return Task.CompletedTask;
    }




    // Suggestion: Move elsewhere.
    private void ResetToLoginMenu()
    {
        menuService.SetMenu(
            new LoginMenu(
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService));
    }
}