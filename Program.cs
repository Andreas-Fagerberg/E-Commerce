namespace E_commerce_Databaser_i_ett_sammanhang;

class Program
{
    static async void Main(string[] args)
    {
        IMenuService menuService = new AppMenuService();
        menuService.SetMenu(new LoginMenu());
        while (true)
        {
            Console.Clear();
            menuService.GetMenu().Display();
            ConsoleKey input = Console.ReadKey().Key;

            switch (input)
            {
                case ConsoleKey.Escape
                or ConsoleKey.F7:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.LeftArrow
                or ConsoleKey.A:

                    continue;

                case ConsoleKey.RightArrow
                or ConsoleKey.D:

                    continue;
            }
            foreach (ConsoleKey consoleKey in menuService.GetTriggerKeys())
            {
                if (input.Equals(consoleKey))
                {
                    menuService.ChangeMenu(input);
                    continue;
                }
            }
            try
            {
                await menuService.GetMenu().ExecuteCommand(input);
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.Message)
                    ? "Something went wrong, please try again."
                    : ex.Message;
            }
        }
    }
}
