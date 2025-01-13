using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_commerce_Databaser_i_ett_sammanhang;

public interface ICommand
{
    public ConsoleKey TriggerKey { get; set; }
    Task Execute();
}
