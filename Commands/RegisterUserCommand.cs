namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Handles the execution of the user registration process as part of the command pattern.
/// </summary>
public class RegisterUserCommand : BaseCommand
{

    public RegisterUserCommand(ConsoleKey triggerKey, IUserService userService)
        : base(triggerKey, userService)
    {
    }

    public override async Task Execute(Guid? currentUserId)
    {
        while (true)
        {
            Utilities.ClearAndWriteLine("[Register User]\n");

            try
            {
                var dto = InputHandler.GetRegistrationInput();
                var response = await userService.RegisterUser(dto);

                Console.WriteLine($"User registered successfully!");
                Console.WriteLine($"Welcome, {response.FirstName} {response.LastName}");
                Console.ReadLine();
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
            Console.WriteLine("[DEBUG]");
            Console.ReadLine(); // TEMP: Breaker
        }
    }
}