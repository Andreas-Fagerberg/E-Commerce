public class BaseMenu
{
    private string _headerContent = string.Empty;
    private List<string> _menuContent = new List<string>();

    public CheckoutMenu() { }

    public void Display()
    {
        int boxWidth = 79;

        // Viktigt

        Console.WriteLine("┌" + new string('─', boxWidth) + "┐");
        Console.WriteLine(
            "│ "
                + _headerContent
                + new string(' ', boxWidth - (_headerContent.Length + 8))
                + "AAAL © │"
        );
        Console.WriteLine("├" + new string('─', boxWidth) + "┤");

        for (int i = 0; i < 40; i++)
        {
            if (i > _menuContent.Count)
            {
                break;
            }
            if (_menuContent.Count > i && i < 9)
            {
                Console.WriteLine(
                    "│  "
                        + (i + 1)
                        + ". "
                        + _menuContent[i]
                        + new string(' ', boxWidth - (_menuContent[i].Length + 6))
                        + " │"
                );
                continue;
            }
            if (_menuContent.Count > i)
            {
                Console.WriteLine(
                    "│ "
                        + (i + 1)
                        + ". "
                        + _menuContent[i]
                        + new string(' ', boxWidth - (_menuContent[i].Length + 6))
                        + " │"
                );
                continue;
            }
            Console.WriteLine(
                """
                │                                                                               │
                │ ESC. Exit application                                                         │
                """
            );
            // Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        }

        Console.WriteLine("├" + new string('─', boxWidth) + "┤");
        Console.WriteLine("│" + new string(' ', boxWidth) + "│");
        Console.WriteLine("└" + new string('─', boxWidth) + "┘");
    }

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
