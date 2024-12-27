

namespace E_commerce_Databaser_i_ett_sammanhang;

public class RegisterUserCommand : BaseCommand
{

    public RegisterUserCommand(ConsoleKey triggerKey, UserService userService)
        : base(triggerKey, userService)
    {
    }

    public override async Task Execute(Guid? currentUserId)
    {
        while (true)
        {
            try
            {
                var dto = InputHandler.GetRegistrationInput();
                var response = await userService.RegisterUser(dto);

                Console.WriteLine($"User registered successfully!");
                Console.WriteLine($"Welcome, {response.FirstName} {response.LastName}");
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            catch (InvalidOperationException ex) // Bussiness logic: e.g. duplicate emails.
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}