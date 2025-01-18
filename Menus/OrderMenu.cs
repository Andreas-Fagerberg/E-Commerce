namespace E_commerce_Databaser_i_ett_sammanhang;

public class OrderMenu : Menu
{
    private string _headerContent = string.Empty;
    private List<string> _menuContent = new List<string>();

    public OrderMenu() { }

    public override void Display()
    {
        Console.Clear();
        int boxWidth = 79;

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ "
                + _headerContent
                + new string(' ', boxWidth - (_headerContent.Length + 8))
                + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        int i = 1;
        foreach (string item in _menuContent)
        {
            if (_menuContent is null || _menuContent.Count.Equals(0))
            {
                Console.WriteLine(
                    "│ No Items Found.                                                               │"
                );
                break;
            }
          
            Console.WriteLine("│ " + item + new string(' ', boxWidth - (item.Length + 1)) + "│"); // Add space if needed
            i++;
            continue;
        }

        Console.WriteLine(
            """
            │                                                                               │
            │ Press any key to return.                                                      │
            """
        );

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

    /// <summary>
    ///  Adds content to the BaseMenu display. Default headerContent is: "Select an option below:"
    ///  menuContent must have atleast one string to function.
    /// </summary>
    /// <param name="menuContent"></param>
    /// <param name="headerContent"></param>
    public void EditContent(List<string> menuContent, string headerContent = "")
    {
        _menuContent = menuContent;
        if (string.IsNullOrWhiteSpace(headerContent))
        {
            _headerContent = "Select an option below:";
            return;
        }

        _headerContent = headerContent;
    }
}
