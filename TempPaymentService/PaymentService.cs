
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

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
        await ValidateOrderId(dto.OrderId);

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

    public async Task<string> ProcessPayment(Guid orderId, PaymentMethod paymentMethod, decimal totalAmount)
    {
        var invoiceCreationDto = new InvoiceCreationDTO
        {
            OrderId = orderId,
            TotalAmount = totalAmount,
            PaymentMethod = paymentMethod
        };

        try
        {
            await CreateInvoice(invoiceCreationDto);

            return paymentMethod == PaymentMethod.PayNow
                ? "Payment sucessful. Your invoice is marked as Paid"
                : "Payment deferred. Your invoice is marked as Pending.";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Invoice creation failed.", ex);
        }
    }



    #region Helper Methods

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

    private async Task ValidateOrderId(Guid orderId)
    {
        var orderExists = await ecommerceContext.Orders.AnyAsync(o => o.OrderId == orderId);
        if (orderExists == false)
        {
            throw new ArgumentException("Invalid OrderId. No matching order found.");
        }
    }

    #endregion
}