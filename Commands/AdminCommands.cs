
namespace E_commerce_Databaser_i_ett_sammanhang;

public class AdminCommands : MenuBaseCommand
{

    public AdminCommands(ConsoleKey triggerkey, IUserService userService, IMenuService menuService)
        : base(triggerkey, userService, menuService) { }
    public override async Task Execute(Guid? currentUserId)
    {
        await userService.ValidateAdminUser(currentUserId);

        while (true)
        {
            Utilities.ClearAndWriteLine(
            """
            [Admin Commands]
            [1] View All Users
            [2] Search Users
            [3] Update User Role
            [4] Exit Admin Menu            
            """);

            var input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.D1:
                    var users = await userService.GetAllUsers(currentUserId);
                    break;

                case ConsoleKey.D2:
                    var searchCriteria = InputHandler.GetAdminSearchInput();
                    await userService.SearchUsers(searchCriteria, currentUserId);
                    break;

                case ConsoleKey.D3:
                    var newRole = InputHandler.GetRoleUpdateInput();
                    await userService.UpdateUserRole(newRole, currentUserId);
                    break;
            }
        }
    }
}