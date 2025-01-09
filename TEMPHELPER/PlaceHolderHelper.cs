namespace E_commerce_Databaser_i_ett_sammanhang;

public class PlaceHolderHelper
{
    public static void MenuHelperProducts(ConsoleKeyInfo key, ProductMenu productMenu, IShoppingCartService shoppingCartService)
    {
        List<List<Product>> allProducts = productMenu.EditContent(products);
        while (true)
        {
            productMenu.Display();

            var key = CustomKeyReader.GetKeyOrBuffered();

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }

            if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
            {
                productMenu.SetPage(key.Key);
                index = productMenu.GetPage();
                continue;
            }

            currentPage = allProducts[index];

            string fullLine = CustomKeyReader.GetBufferedLine();
            if (!int.TryParse(fullLine, out int choice))
            {
                Utilities.WriteLineWithPause("You have to enter a number.");
                continue;
            }

            Product selectedProduct;
            if (choice > 0 && choice <= currentPage.Count)
            {
                selectedProduct = currentPage[choice - 1];
                productMenu.DisplayProduct(selectedProduct);
            }
            else
            {
                Utilities.WriteLineWithPause("Please select a product from the list.");
                continue;
            }

            ConsoleKey input2 = Console.ReadKey().Key;

            if (key.Equals(ConsoleKey.D1))
            {
                shoppingCartService.AddToShoppingCart(selectedProduct.ProductId, selectedProduct.Price);
                break;
            }
            else if (key.Equals(ConsoleKey.Escape))
            {
                break;
            }
            Utilities.WriteLineWithPause("Incorrect input.");
            continue;
        }
    }

}