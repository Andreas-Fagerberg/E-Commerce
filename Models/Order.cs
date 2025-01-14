namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents a physical order associated with a user.
/// </summary>
public class Order
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; } = Status.Pending;
    public decimal TotalCost { get; set; }

    public User? User { get; set; }
    public Invoice? Invoice { get; set; }
    public ICollection<OrderProduct> OrderProducts = [];
}
