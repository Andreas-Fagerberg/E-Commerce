namespace E_commerce_Databaser_i_ett_sammanhang;

public abstract class BaseCommand : ICommand
{
    public ConsoleKey TriggerKey { get; init; }
    protected readonly IUserService userService;
    protected readonly IMenuService? menuService;

    protected BaseCommand(ConsoleKey triggerKey, IUserService userService, IMenuService? menuService = null)
    {
        TriggerKey = triggerKey;
        this.userService = userService;
        this.menuService = menuService;
    }

    /* Using abstract method with a Task return type.
    (Task enables async operations to be performed within the method) */
    public abstract Task Execute(Guid? currentUserId);
    

    
}
