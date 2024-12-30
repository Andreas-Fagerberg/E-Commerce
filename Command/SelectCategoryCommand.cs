namespace E_commerce_Databaser_i_ett_sammanhang;

public class SelectCategoryCommand : BaseCommand
{
    public SelectCategoryCommand(ConsoleKey triggerKey, IUserService userService)
        : base(triggerKey, userService) { }

    public override Task Execute(Guid? currentUserId)
    {
        Menu categoryMenu = new CategoryMenu(userService);
        categoryMenu.Display();
        while (true)
        {
            string? input = Console.ReadLine();
            CategoryMenu.SelectCategory(input);
        }
    }
}
