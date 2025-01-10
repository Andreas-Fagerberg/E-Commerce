namespace E_commerce_Databaser_i_ett_sammanhang;

public interface ICommand
{
    Task Execute(Guid? currentUserId);
}
