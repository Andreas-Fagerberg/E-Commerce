namespace E_commerce_Databaser_i_ett_sammanhang;

public interface IMenuService
{
    void SetMenu(Menu menu);
    Menu GetMenu();
    void ChangeMenu(ConsoleKey consoleKey);
    List<ConsoleKey> GetTriggerKeys();
}

public class AppMenuService : IMenuService
{
    private Menu menu = new HomeMenu(IUserService userService);
    protected readonly IUserService userService;

    public AppMenuService(IUserService userService)
    {
        this.userService = userService;
    }

    public Menu GetMenu()
    {
        return menu;
    }

    public void SetMenu(Menu menu)
    {
        this.menu = menu;
    }


}
