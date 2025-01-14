
namespace E_commerce_Databaser_i_ett_sammanhang;

public class ViewOrdersCommand : MenuBaseCommand
{
    public ViewOrdersCommand(
        ConsoleKey triggerkey,
        IUserService userService,
        IMenuService menuService,
        IProductService productService,
        ICartService cartService,
        IOrderService orderService,
        IPaymentService paymentService
        )
            : base(
            triggerkey,
            userService,
            menuService,
            productService,
            cartService,
            orderService,
            paymentService
        )
    { }


    public override async Task Execute()
    {
        Guid currentUserId = SessionHandler.GetCurrentUserId();


        var userResponse = await userService.GetUser(currentUserId);
        var addressResponse = await userService.GetUserAddress(currentUserId);
        List<OrderResponse> orderResponses = await orderService.GetUserOrders(currentUserId);

        PrintUserInfo(userResponse, addressResponse!);
        PrintOrders(orderResponses);
    }

    // MOVE BELOW TO THE ORDER MENU OR SOMETHING? :) :)
    private void PrintUserInfo(UserResponse user, AddressResponse address)
    {
        Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
        Console.WriteLine($"Email: {user.Email}");

        if (address == null)
        {
            Console.WriteLine("No address saved.");
            return;
        }

        Console.WriteLine("Address Information:");
        Console.WriteLine($"Street: {address.Street}");
        Console.WriteLine($"City: {address.City}");
        Console.WriteLine($"Region: {address.Region}");
        Console.WriteLine($"Postal Code: {address.PostalCode}");
        Console.WriteLine($"Country: {address.Country}");
        Console.WriteLine();
    }


    private void PrintOrders(List<OrderResponse> orderResponses)
    {
        if (orderResponses.Any() == false)
        {
            Utilities.WriteLineWithPause("You have no orders.");
            return;
        }

        foreach (var order in orderResponses)
        {
            Console.WriteLine($"Order ID: {order.OrderId}");
            Console.WriteLine($"Created At: {order.CreatedAt}");
            Console.WriteLine($"Status: {order.Status}");
            Console.WriteLine($"Total Cost: {order.TotalCost:C}");
        }
    }

}
