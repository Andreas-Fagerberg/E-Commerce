namespace E_commerce_Databaser_i_ett_sammanhang;

public class CheckoutCommands : MenuBaseCommand
{
    private readonly BaseMenu _baseMenu = new BaseMenu();
    private List<string> _menuContent;

    public CheckoutCommands(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
    )
        : base(
            triggerKey,
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService
        )
    {
        _menuContent = new List<string> { "Checkout" };
    }

    public override async Task Execute()
    {
        Guid currentUserId = SessionHandler.GetCurrentUserId();

        _baseMenu.EditContent(_menuContent);
        _baseMenu.Display();

        ConsoleKey input = Console.ReadKey(true).Key;

        switch (input)
        {
            case ConsoleKey.Escape:
                return;
            default:
                break;
        }

        // 1. Retrieve Address
        var address = await HandleAddressOptions(currentUserId);
        if (address is null)
        {
            return;
        }

        Utilities.WriteLineWithPause("Proceeding to the next step...");

        // 2. Payment Handling
        Console.WriteLine(
            """
            Select a payment option:
            1. Pay Now
            2. Pay Later

            ESC. Go Back.
            """
        );

        input = Console.ReadKey(true).Key;

        switch (input)
        {
            case ConsoleKey.Escape:
                return;
            default:
                break;
        }

        PaymentMethod paymentMethod = SelectPaymentMethod(input);

        // 3. Retrieve Cart Data
        await cartService.SaveCartToDatabase(currentUserId);
        var cartData = await cartService.GetShoppingCart(currentUserId);

        if (!cartData.Any())
        {
            Console.WriteLine("Your cart is empty. Add items before checking out.");
            Console.ReadLine();
            return;
        }

        // 4. Prepare Order Data
        var orderProducts = PrepareCartData(cartData);

        // 5. Create Order
        OrderResponse? orderSummary = null;
        try
        {
            orderSummary = await orderService.ProcessOrder(
                currentUserId,
                orderProducts,
                paymentMethod
            );
            Console.WriteLine("Your order has been successfully created");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Order creation failed. Cannot proceed");
            ExceptionHandler.Handle(ex);
            return;
        }

        // 6. Process Payment and Invoice
        try
        {
            var paymentStatus = await paymentService.ProcessPayment(
                orderSummary.OrderId,
                orderSummary.TotalCost,
                paymentMethod
            );
            Console.WriteLine(paymentStatus);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Payment processing failed using method '{paymentMethod}'. Checkout aborted."
            );
            ExceptionHandler.Handle(ex);
            Console.ReadLine();
        }
        Utilities.WriteLineWithPause("Checkout completed successfully.", 2000);

        cartService.RemoveAllItems(currentUserId);
    }

    #region Helper Methods
    private static PaymentMethod SelectPaymentMethod(ConsoleKey input)
    {
        PaymentMethod paymentMethod;
        while (true)
        {
            switch (input)
            {
                case ConsoleKey.D1:
                    paymentMethod = PaymentMethod.PayNow;
                    return paymentMethod;

                case ConsoleKey.D2:
                    paymentMethod = PaymentMethod.PayLater;
                    return paymentMethod;

                default:
                    Console.WriteLine(
                        "Invalid selection. Please press [1] for Pay Now or [2] for Pay Later."
                    );
                    Console.ReadLine();
                    continue;
            }
        }
    }

    /// <summary>
    /// Transforms cart data from a dictionary structure into a list of OrderProductDTO objects.
    /// This method is necessary to convert raw cart data into a standardized format
    /// suitable for order creation, ensuring compatibility with the CreateOrder process.
    /// </summary>
    private static List<OrderProductDTO> PrepareCartData(
        Dictionary<int, (int Quantity, decimal Price, string Name)> cartData
    )
    {
        var orderProducts = new List<OrderProductDTO>();

        foreach (var item in cartData)
        {
            var orderProduct = new OrderProductDTO
            {
                ProductId = item.Key,
                Quantity = item.Value.Quantity,
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
                1. Use existing address
                2. Update address

                ESC. Go Back.

                Current Address:
                {existingAddress.Street}, 
                {existingAddress.City}, 
                {existingAddress.Region}, 
                {existingAddress.PostalCode}, 
                {existingAddress.Country}
                
                """
            );

            while (true)
            {
                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.Escape:
                        return null;
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
