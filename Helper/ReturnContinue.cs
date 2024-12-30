namespace E_commerce_Databaser_i_ett_sammanhang;

public static class ReturnContinue 
{ 
    // Must have access to all the services needed by menus.

    public static void ContinueReturn(IMenuService menuService)
    {
        ConsoleKey input = Console.ReadKey().Key;
        switch (input)
        {
            case ConsoleKey.D1:
                menuService.SetMenu(new HomeMenu());
                break;
            case ConsoleKey.D2:
                
                break;
        }
    }
}
