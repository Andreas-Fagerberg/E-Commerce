namespace E_commerce_Databaser_i_ett_sammanhang;

public class CheckoutCommands : MenuBaseCommand
{
    public CheckoutCommands(ConsoleKey triggerKey, IUserService userService, IMenuService menuService, ICartService cartService, IOrderService orderService)
        : base(triggerKey, userService, menuService, cartService, orderService) { }

    public override async Task Execute(Guid? currentUserId)
    {
        UserValidation.CheckForValidUser(currentUserId);
        Console.WriteLine("[Checkout Commands]");

        // 1. Retrieve Address
        var address = await HandleAddressOptions(currentUserId);
        if (address == null)
        {
            Console.WriteLine("Checkout cancelled. No address selected.");
            return;
        }

        Utilities.WriteLineWithPause("Proceeding to the next step...");


        // HANDLING OF PAYMENT SHOULD GO HERE
        // HANDLING OF PAYMENT SHOULD GO HERE
        // HANDLING OF PAYMENT SHOULD GO HERE
        // HANDLING OF PAYMENT SHOULD GO HERE


        // 2. Retrieve Cart Data
        var cartData = await cartService.GetShoppingCart(currentUserId);
        if (!cartData.Any())
        {
            Console.WriteLine("Your cart is empty. Add items before checking out.");
            return;
        }

        // 3. Prepare Order Data
        var orderProducts = PrepareCartData(cartData);

        // 4. Create Order
        var createOrderDto = new CreateOrderDTO
        {
            UserId = currentUserId!.Value,
            Products = orderProducts
        };

        try
        {
            var orderResponse = await orderService.CreateOrder(createOrderDto);
            if (orderResponse == null)
            {
                Console.WriteLine("Order creation failed. Please try again");
                return;
            }

            var orderSummary = await orderService.GetOrderDetails(orderResponse.OrderId);
            Console.WriteLine("Your order has been successfully created.");

            // AndreasUI.DisplayOrderConfirmation(orderSummary)
        }
        catch (Exception ex)
        {
            ExceptionHandler.Handle(ex);
        }
    }




    #region Helper Methods

    /// <summary>
    /// Transforms cart data from a dictionary structure into a list of OrderProductDTO objects.
    /// This method is necessary to convert raw cart data into a standardized format 
    /// suitable for order creation, ensuring compatibility with the CreateOrder process.
    /// </summary>
    private static List<OrderProductDTO> PrepareCartData(Dictionary<int, (int Quantity, decimal Price)> cartData)
    {
        var orderProducts = new List<OrderProductDTO>();

        foreach (var item in cartData)
        {
            var orderProduct = new OrderProductDTO
            {
                ProductId = item.Key,
                Quantity = item.Value.Quantity
            };

            orderProducts.Add(orderProduct);
        }

        return orderProducts;
    }


    /// <summary>
    /// Handles the selection or creation of a user's address during the checkout process.
    /// If an address exists, the user can choose to use it or update it. 
    /// If no address exists, the user is prompted to enter a new address.
    /// This method ensures that a valid address is available for the order.
    /// </summary>
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

    #endregion
}
