

namespace E_commerce_Databaser_i_ett_sammanhang;

public class SearchCommand : MenuBaseCommand
{
    public SearchCommand(ConsoleKey triggerKey, IUserService userService, IMenuService menuService) : base(triggerKey, userService, menuService)
    {
    }

    public override Task Execute(Guid? currentUserId)
    {
        throw new NotImplementedException();
    }
}