namespace E_commerce_Databaser_i_ett_sammanhang;

public class CheckoutCommands : MenuBaseCommand
{
    public CheckoutCommands(ConsoleKey triggerKey, IUserService userService, IMenuService menuService, IShoppingCartService cartService, IOrderService orderService)
        : base(triggerKey, userService, menuService) { } //,orderService, cartService) { }

    public override async Task Execute(Guid? currentUserId)
    {
        UserValidation.CheckForValidUser(currentUserId);
        Console.WriteLine("[Checkout Commands]");

        var address = await HandleAddressOptions(currentUserId);

        if (address == null)
        {
            Console.WriteLine("Checkout cancelled. No address selected.");
            return;
        }

        Utilities.WriteLineWithPause("Proceeding to the next step...");


        // HANDLING OF PAYMENT SHOULD GO HERE


        // Method to retrieve current cart for order information

        cartService.
        orderService.CreateOrder(cartDetails);






    }










    private async Task<AddressResponse?> HandleAddressOptions(Guid currentUserId)
    {
        var existingAddress = await userService.GetUserAddress(currentUserId);

        if (existingAddress != null)
        {
            Console.WriteLine(
                $"""
                Select an Address Option:
                [1] Use existing address
                [2] Update address

                Current Address:
                {existingAddress.Street}, 
                {existingAddress.City}, 
                {existingAddress.Region}, 
                {existingAddress.PostalCode}, 
                {existingAddress.Country}
                """);

            while (true)
            {
                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        return existingAddress;

                    case ConsoleKey.D2:
                        var updatedAddressDto = InputHandler.GetAddressInput(currentUserId);
                        return await userService.UpdateUserAddress(updatedAddressDto);

                    default:
                        Console.WriteLine("Invalid selection. Please choose [1] or [2].");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("No saved address found. Please enter a new address.");
            var newAddressDto = InputHandler.GetAddressInput(currentUserId);
            return await userService.SaveUserAddress(newAddressDto);
        }
    }
}
