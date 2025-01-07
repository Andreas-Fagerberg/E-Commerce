
namespace E_commerce_Databaser_i_ett_sammanhang;

public class UpdateUserAddressCommand : BaseCommand
{
    public UpdateUserAddressCommand(ConsoleKey triggerKey, IUserService userService)
    : base(triggerKey, userService) { }

    /// <summary>
    /// Handles the execution process of updating a user's existing address.
    /// </summary>
    public override async Task Execute(Guid? currentUserId)
    {
        UserValidation.CheckForValidUser(currentUserId);

        var addressInput = InputHandler.GetAddressInput(currentUserId!.Value);

        try
        {
            var updatedAddress = await userService.UpdateUserAddress(addressInput);
            Console.WriteLine($"Address updated successfully!");
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
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}