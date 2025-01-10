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
    CreditCard,
    PayPal,
    Klarna,
    BankTransfer,
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
