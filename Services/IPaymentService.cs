namespace E_commerce_Databaser_i_ett_sammanhang;

public interface IPaymentService
{
    Task<InvoiceResponse> CreateInvoice(InvoiceCreationDTO dto);
    Task<string> ProcessPayment(Guid orderId, decimal totalAmount, PaymentMethod paymentMethod);
}