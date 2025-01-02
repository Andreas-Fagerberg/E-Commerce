
namespace E_commerce_Databaser_i_ett_sammanhang;

public class SaveUserAddressCommand : BaseCommand
{
    public SaveUserAddressCommand(ConsoleKey triggerKey, IUserService userService)
    : base(triggerKey, userService) { }

    public override async Task Execute(Guid? currentUserId)
    {
        try
        {
            UserValidation.CheckForValidUser(currentUserId);

            var dto = InputHandler.GetAddressInput(currentUserId!.Value);
            var response = await userService.SaveUserAddress(dto);

            Console.WriteLine($"Address registered successfully:");
            Console.WriteLine($"{response.Street}, {response.City}, {response.Region}, {response.Country}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

}