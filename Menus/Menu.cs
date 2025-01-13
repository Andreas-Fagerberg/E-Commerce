namespace E_commerce_Databaser_i_ett_sammanhang;

public abstract class Menu
{
    private List<MenuBaseCommand> commands = new List<MenuBaseCommand>();

    public void AddCommand(MenuBaseCommand command)
    {
        commands.Add(command);
    }

    public async Task ExecuteCommand(ConsoleKey input)
    {
        foreach (MenuBaseCommand command in commands)
        {
            if (command.triggerKey.Equals(input))
            {
                await command.Execute();
                return;
            }
        }
        throw new Exception("Command not found.");
    }

    public abstract Task Display();
}
