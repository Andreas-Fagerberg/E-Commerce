namespace E_commerce_Databaser_i_ett_sammanhang;

public class SelectCategoryCommand : MenuBaseCommand
{
    public SelectCategoryCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService
    )
        : base(triggerKey, userService, menuService) { }

    public override Task Execute(Guid? currentUserId)
    {
        Menu categoryMenu = new CategoryMenu(userService, menuService);
        categoryMenu.Display();
        while (true)
        {
            string? input = Console.ReadLine();
            CategoryMenu.SelectCategory(input);
        }
    }
}
