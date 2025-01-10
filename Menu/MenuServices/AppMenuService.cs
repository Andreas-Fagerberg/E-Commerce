namespace E_commerce_Databaser_i_ett_sammanhang;
public class AppMenuService : IMenuService
{
    private Menu menu;
    protected readonly IUserService userService;

    public AppMenuService(IUserService userService)
    {
        this.userService = userService;
        menu = new LoginMenu(userService, this);
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
