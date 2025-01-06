namespace E_commerce_Databaser_i_ett_sammanhang;

public interface IMenuService
{
    void SetMenu(Menu menu);
    Menu GetMenu();

}

public class AppMenuService : IMenuService
{
    private Menu menu;
    protected readonly IUserService userService;

    public AppMenuService(IUserService userService)
    {
        this.userService = userService;
        menu = new LoginMenu(userService, this);
    }

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
