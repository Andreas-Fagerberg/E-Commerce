namespace E_commerce_Databaser_i_ett_sammanhang;

public enum Role
{
    User, // Default
    Admin,
}
public enum Status
{
    Pending, // Default
    Completed,
    Cancelled,
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed,
    Refunded
}

public enum PaymentMethod
{
    /// <summary>
    /// The user pays immediately during checkout.
    /// Marks the invoice as Paid upon creation.
    /// </summary>
    PayNow,
    /// <summary>
    /// The user defers payment (e.g. 30-day-invoice)
    /// Marks the invoice as Pending upon creation.
    /// </summary>
    PayLater
}


public enum Category
{
    Electronics,
    Cleaning,
    Gaming,
    Storage,
    Outdoors,
    Sports,
    Tools,
}
