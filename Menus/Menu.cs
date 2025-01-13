namespace E_commerce_Databaser_i_ett_sammanhang;

public abstract class Menu
{
    private List<ICommand> commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }

    public async Task ExecuteCommand(ConsoleKey input)
    {
        foreach (ICommand command in commands)
        {
            if (command.TriggerKey.Equals(input))
            {
                await command.Execute();
                return;
            }
        }
        throw new Exception("Command not found.");
    }

    public abstract void Display();
}
