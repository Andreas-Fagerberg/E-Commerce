namespace E_commerce_Databaser_i_ett_sammanhang;

public class ExitCommand : BaseCommand
{
    private readonly IMenuService _menuService;

    public ExitCommand(ConsoleKey triggerKey, IUserService userService, IMenuService menuService)
        : base(triggerKey, userService)
    {
        _menuService = menuService;
    }

    public override Task Execute(Guid? currentUserId)
    {
       // _menuService.SetMenu(new HomeMenu(userService));
        return Task.CompletedTask;
    }
}
