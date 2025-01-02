namespace E_commerce_Databaser_i_ett_sammanhang;

public class FakeCommand : BaseCommand
{
    public FakeCommand(ConsoleKey triggerKey, IUserService userService)
        : base(triggerKey, userService) { }

    public override Task Execute(Guid? currentUserId)
    {
        throw new NotImplementedException();
    }
}
