namespace E_commerce_Databaser_i_ett_sammanhang;

public class CartCommand : BaseCommand
{
    public CartCommand(ConsoleKey triggerKey, IUserService userService)
        : base(triggerKey, userService) { }

    public override Task Execute(Guid? currentUserId) { }
}
