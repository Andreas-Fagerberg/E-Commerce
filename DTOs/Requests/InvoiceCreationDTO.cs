namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// Represents the data required to create an invoice for an order.
/// </summary>
public class InvoiceCreationDTO
{
    public required Guid OrderId { get; set; }
    public required decimal TotalAmount { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }
}