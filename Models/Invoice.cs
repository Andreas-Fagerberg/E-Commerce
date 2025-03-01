namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents an invoice associated with an order,
/// containing details about payment and creation timestamps.
/// </summary>
public class Invoice
{
    public int InvoiceId { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }

    public Order? Order { get; set; }
}
