namespace E_commerce_Databaser_i_ett_sammanhang;

class Program
{
    static void Main(string[] args)
    {
        IMenuService menuService = new AppMenuService();
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
        }
    }
}
