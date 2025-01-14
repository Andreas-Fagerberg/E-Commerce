namespace E_commerce_Databaser_i_ett_sammanhang;
public class MenuService : IMenuService
{
    private Menu menu;
    protected readonly IUserService userService;

    public MenuService(IUserService userService)
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
