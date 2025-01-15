
namespace E_commerce_Databaser_i_ett_sammanhang;

public class ViewOrdersCommand : MenuBaseCommand
{
    private readonly OrderMenu _orderMenu;


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
    {

        _orderMenu = new OrderMenu();

    }

    public override async Task Execute()
    {
        Guid currentUserId = SessionHandler.GetCurrentUserId();

        var userResponse = await userService.GetUser(currentUserId);
        var addressResponse = await userService.GetUserAddress(currentUserId);
        List<OrderResponse> orderResponses = await orderService.GetUserOrders(currentUserId);

        List<string> pageInformation = new List<string>();
        Console.WriteLine();

        pageInformation.Add($"Name: {userResponse.FirstName} {userResponse.LastName}");
        pageInformation.Add($"Email: {userResponse.Email}");

        if (addressResponse == null)
        {
            pageInformation.Add("No address saved.");
            pageInformation.Add("");
        }
        else
        {
            pageInformation.Add("Address Information:");
            pageInformation.Add($"Street: {addressResponse.Street}");
            pageInformation.Add($"City: {addressResponse.City}");
            pageInformation.Add($"Region: {addressResponse.Region}");
            pageInformation.Add($"Postal Code: {addressResponse.PostalCode}");
            pageInformation.Add($"Country: {addressResponse.Country}");
        }

        foreach (var order in orderResponses)
        {
            if (orderResponses == null)
            {
                pageInformation.Add("You have no order history.");
                pageInformation.Add("");
                break;
            }
            pageInformation.Add($"Order ID: {order.OrderId}");
            pageInformation.Add($"Created At: {order.CreatedAt}");
            pageInformation.Add($"Status: {order.Status}");
            pageInformation.Add($"Total Cost: {order.TotalCost:C}");
        }

        _orderMenu.EditContent(pageInformation, "Your orders");
        _orderMenu.Display();
        Console.ReadLine();
    }
}
