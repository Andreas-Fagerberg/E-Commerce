
namespace E_commerce_Databaser_i_ett_sammanhang;

public class AdminCommands : BaseCommand
{

    public AdminCommands(ConsoleKey triggerkey, IUserService userService)
        : base(triggerkey, userService) { }
    public override async Task Execute(Guid? currentUserId)
    {
        UserValidation.CheckForValidUser(currentUserId);
        Utilities.ClearAndWriteLine(
            """
            [Admin Commands]
            [1] View All Users
            [2] Search Users
            [3] Update User Role            
            """);

        var users = await userService.GetAllUsers(currentUserId);

        var searchCriteria = InputHandler.GetAdminSearchInput();
        await userService.SearchUsers(searchCriteria);

        var newRole = InputHandler.GetRoleUpdateInput();
        await userService.UpdateUserRole(newRole);
    }
}