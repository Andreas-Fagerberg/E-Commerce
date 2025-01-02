namespace E_commerce_Databaser_i_ett_sammanhang;

public class OrderService : IOrderService
{
    private readonly EcommerceContext ecommerceContext;
    public OrderService(EcommerceContext ecommerceContext)
    {
        this.ecommerceContext = ecommerceContext;
    }

}