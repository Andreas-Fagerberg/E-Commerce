using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    public class SortProductsCommand : BaseCommand
    {
        private readonly EcommerceContext _context;
        public SortProductsCommand(UserService userService, EcommerceContext context)
            : base(ConsoleKey."?", userService)
        {
            _context = context;
        }
        public override async Task Execute(Guid? currentUserId)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\nSort by:");
                Console.WriteLine("1. Price (Low to High)");
                Console.WriteLine("2. Price (High to Low)");
                Console.WriteLine("3. Rating (High to Low)");
                Console.WriteLine("4. Name (A to Z)");
                Console.WriteLine("5. Category");
                Console.Write("\nSelect sort option (1-5): ");


                string? input = Console.ReadLine();

                while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5")
                {
                    Console.WriteLine("Invalid choice. Please select a valid option (1-5).");
                    Console.Write("\nSelect sort option (1-5): ");
                    input = Console.ReadLine();
                }

                // Initialize query for products
                var query = _context.Products.AsQueryable();


                query = input switch
                {
                    "1" => query.OrderBy(p => p.Price),
                    "2" => query.OrderByDescending(p => p.Price),
                    "3" => query.OrderByDescending(p => p.Rating),
                    "4" => query.OrderBy(p => p.Name),
                    "5" => query.OrderBy(p => p.Category)
                           .ThenBy(p => p.Name),
                    _ => query.OrderBy(p => p.Name) // Default sorting by name
                };

                // Execute query and get sorted products
                var products = await query.ToListAsync();


                foreach (var product in products)
                {
                    Console.WriteLine($"Name: {product.Name}");
                    Console.WriteLine($"Category: {product.Category ?? "Uncategorized category"}");
                    Console.WriteLine($"Price: ${product.Price:F2}");
                    Console.WriteLine($"Rating: {product.Rating}");

                    if (!string.IsNullOrEmpty(product.Description))
                    {
                        Console.WriteLine($"Description: {product.Description}");
                    }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAn error occurred while sorting products.");
                Console.WriteLine($"Error details: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}