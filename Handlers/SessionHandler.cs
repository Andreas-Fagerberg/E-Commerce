namespace E_commerce_Databaser_i_ett_sammanhang;

/// <summary>
/// A static class that manages the session for the current user,
/// providing functionality to set, retrieve and clear the current user's ID.
/// </summary>
public static class SessionHandler
{
    private static Guid? _currentUserId;

    /// <summary>
    /// A wrapper that provides encapsulation and allows for additional control 
    /// parameters to be added in the future. Gets or sets the current User ID for the session.
    /// </summary>
    public static Guid? CurrentUserId
    {
        get => _currentUserId;
        set => _currentUserId = value;
    }

    /// <summary>
    /// Clears the current session, effectively logging the user out.
    /// </summary>
    public static void ClearSession()
    {
        _currentUserId = null;
    }
}