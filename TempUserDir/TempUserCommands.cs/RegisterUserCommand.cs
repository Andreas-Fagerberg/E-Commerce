

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
                // 1: Gather input 
                var dto = InputHandler.GetRegistrationInput();

                // 2: Register the user
                var response = await Task.Run(() => userService.RegisterUser(dto));

                //3: Display succes message.
                Console.WriteLine($"User registered successfully!");
                Console.WriteLine($"Welcome, {response.FirstName} {response.LastName}");
                break; // Exit loop on success.
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