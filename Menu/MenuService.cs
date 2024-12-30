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
    private Menu menu = new HomeMenu();

    public List<ConsoleKey> TriggerKeys = new List<ConsoleKey>
    {
        ConsoleKey.F1,
        ConsoleKey.F2,
        ConsoleKey.F3,
        ConsoleKey.F4,
        ConsoleKey.F5,
        ConsoleKey.F6,
    };

    public Menu GetMenu()
    {
        return menu;
    }

    public void SetMenu(Menu menu)
    {
        this.menu = menu;
    }

    public List<ConsoleKey> GetTriggerKeys()
    {
        return TriggerKeys;
    }

    public void ChangeMenu(ConsoleKey consoleKey)
    {
        switch (consoleKey)
        {
            case ConsoleKey.F1:
                SetMenu(new SearchMenu());
                break;

            case ConsoleKey.F2:
                SetMenu(new CategoryMenu());
                break;

            case ConsoleKey.F3:
                SetMenu(new CartMenu());
                break;

            case ConsoleKey.F4:
                SetMenu(new CheckoutMenu());
                break;

            case ConsoleKey.F5:
                SetMenu(new LogOutMenu());
                break;

            case ConsoleKey.F6:
                SetMenu(new HelpMenu());
                break;
        }
    }
}
