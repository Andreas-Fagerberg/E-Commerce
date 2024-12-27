namespace E_commerce_Databaser_i_ett_sammanhang;

public abstract class Menu
{
    private List<ICommand> commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }

    public abstract void Display();
}
