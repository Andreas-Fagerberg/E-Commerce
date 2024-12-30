namespace E_commerce_Databaser_i_ett_sammanhang;

public class SelectCategoryCommand : BaseCommand
{
    private readonly IMenuService _menuService;

    public SelectCategoryCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService
    )
        : base(triggerKey, userService)
    {
        _menuService = menuService;
    }

    public override Task Execute(Guid? currentUserId)
    {
        Menu categoryMenu = new CategoryMenu(userService, _menuService);
        categoryMenu.Display();
        while (true)
        {
            string? input = Console.ReadLine();
            CategoryMenu.SelectCategory(input);
        }
    }
}
