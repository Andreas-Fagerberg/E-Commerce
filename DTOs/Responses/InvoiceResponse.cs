/// <summary>
/// This response object will handle output when fetching or displaying an invoice.
/// </summary>
public class InvoiceResponse
{
    public required int InvoiceId { get; set; }
    public required Guid OrderId { get; set; }
    public required decimal TotalAmount { get; set; }
    public required string PaymentStatus { get; set; }
    public required string PaymentMethod { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; } // Optional, as the invoice might not be paid yet
}
