namespace E_commerce_Databaser_i_ett_sammanhang;

public interface ICommand
{
    ConsoleKey TriggerKey { get; }
    Task Execute(Guid? currentUserId);
   
}
