
namespace E_commerce_Databaser_i_ett_sammanhang;

public class PaymentService : IPaymentService
{

    private readonly EcommerceContext ecommerceContext;
    public PaymentService(EcommerceContext ecommerceContext)
    {
        this.ecommerceContext = ecommerceContext;
    }

    /// <summary>
    ///  Takes in invoiceCreationDTO, creates a new invoice and saves it to the database
    ///  while creating and returning an InvoiceResponse for accessibility.
    /// </summary>
    public async Task<InvoiceResponse> CreateInvoice(InvoiceCreationDTO dto)
    {
        ValidateInvoiceCreationDto(dto);

        var invoice = new Invoice
        {
            OrderId = dto.OrderId,
            TotalAmount = dto.TotalAmount,
            PaymentStatus = PaymentStatus.Pending,
            PaymentMethod = dto.PaymentMethod,
            CreatedAt = DateTime.UtcNow
        };

        await ecommerceContext.AddAsync(invoice);
        await ecommerceContext.SaveChangesAsync();


        return new InvoiceResponse
        {
            InvoiceId = invoice.InvoiceId,
            OrderId = invoice.OrderId,
            TotalAmount = invoice.TotalAmount,
            PaymentStatus = invoice.PaymentStatus.ToString(),
            PaymentMethod = invoice.PaymentMethod.ToString(),
            CreatedAt = invoice.CreatedAt,
            PaidAt = invoice.PaidAt,
        };
    }




    #region Helper Method

    private static void ValidateInvoiceCreationDto(InvoiceCreationDTO dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "InviceCreationDTO cannot be null.");
        }

        if (dto.TotalAmount <= 0)
        {
            throw new ArgumentException("TotalAmount must be greater than zero.");
        }
    }

    #endregion
}