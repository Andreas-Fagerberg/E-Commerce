namespace E_commerce_Databaser_i_ett_sammanhang;

public class HelpCommand : BaseCommand
{
    public HelpCommand(ConsoleKey triggerKey, IUserService userService) : base(triggerKey, userService)
    {

    }

    public override Task Execute(Guid? currentUserId)
    {
        throw new NotImplementedException();
    }
}