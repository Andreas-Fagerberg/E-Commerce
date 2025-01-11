namespace E_commerce_Databaser_i_ett_sammanhang;

public enum PaymentMethod
{
    /// <summary>
    /// The user pays immediately during checkout.
    /// Marks the invoice as Paid upon creation.
    /// </summary>
    PayNow,

    /// <summary>
    /// The user defers payment (e.g. 30-day-invoice)
    /// Marks the invoice as Pending upon creation.s
    /// </summary>
    PayLater,
}
