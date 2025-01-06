using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    public class ListProductsCommand : BaseCommand
    {
        private readonly EcommerceContext _context;

        public ListProductsCommand(ConsoleKey triggerkey, IUserService userService, EcommerceContext context)
            : base(triggerkey, userService)
        {
            _context = context;
        }
        public override async Task Execute(Guid? currentUserId)
        {
            try
            {
                var products = await _context.Products
                    .OrderBy(p => p.Category)
                    .ThenBy(p => p.Name)
                    .ToListAsync();

                if (!products.Any())
                {
                    Console.WriteLine("No products found in the catalog.");
                    return;
                }


                Console.Clear();
                Console.WriteLine("PRODUCT CATALOG");

                string currentCategory = "";


                foreach (var product in products)
                {
                    // Check if we've moved to a new category
                    if (currentCategory != product.Category)
                    {
                        // Update the current category
                        // ?? operator provides a default value if Category is null
                        currentCategory = product.Category ?? "Uncategorized category";

                        // Print the new category header
                        Console.WriteLine($"\n{currentCategory.ToUpper()}");
                        Console.WriteLine("---------------");
                    }

                    Console.WriteLine($"Name: {product.Name}");
                    Console.WriteLine($"Price: ${product.Price:F2}");
                    Console.WriteLine($"Rating: {product.Rating}");

                    if (!string.IsNullOrEmpty(product.Description))
                    {
                        Console.WriteLine($"Description: {product.Description}");
                    }
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {

                Console.WriteLine("\nFailed to load the product catalog. Please try again later.");
                Console.WriteLine($"Error details: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}