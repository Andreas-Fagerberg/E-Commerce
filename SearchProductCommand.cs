using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    public class SearchProductsCommand : BaseCommand
    {
        
        private readonly EcommerceContext _context;

        
        public SearchProductsCommand(UserService userService, EcommerceContext context)
            : base(ConsoleKey.?, userService)
        {
            _context = context;
        }
        public override async Task Execute(Guid? currentUserId)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("SEARCH PRODUCTS");
                Console.WriteLine("Enter name (press Enter to skip):");
                string? productName = Console.ReadLine()?.Trim();
                Console.WriteLine("\nEnter category (press Enter to skip):");
                string? category = Console.ReadLine()?.Trim();

                
                var searchProduct = _context.Products.AsQueryable();

                if (!string.IsNullOrWhiteSpace(productName))
                {
                    searchProduct = searchProduct.Where(p => p.Name.ToLower().Contains(productName.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(category))
                {
                    searchProduct = searchProduct.Where(p => p.Category.ToLower().Contains(category.ToLower()));
                }

                // Execute query
                var products = await searchProduct
                    .OrderBy(p => p.Category)
                    .ThenBy(p => p.Name)
                    .ToListAsync();

                // Display results header
                Console.Clear();
                Console.WriteLine($"Search Results ({products.Count} items found)");

                
                if (!products.Any())
                {
                    Console.WriteLine("No products found matching your search criteria.");
                }
                else
                {
                    // Display products grouped by category
                    string currentCategory = "";
                    foreach (var product in products)
                    {
                        // Print category header when switching to a new category
                        if (currentCategory != product.Category)
                        {
                            currentCategory = product.Category ?? "Uncategorized category";
                            Console.WriteLine($"\n{currentCategory.ToUpper()}");
                            Console.WriteLine("---------------");
                        }

                        // Display product details
                        Console.WriteLine($"Name: {product.Name}");
                        Console.WriteLine($"Price: ${product.Price:F2}");
                        Console.WriteLine($"Rating: {product.Rating}");
                        if (!string.IsNullOrEmpty(product.Description))
                        {
                            Console.WriteLine($"Description: {product.Description}");
                        }
                        Console.WriteLine();
                    }
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAn error occurred while searching products.");
                Console.WriteLine($"Error details: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}


// namespace E_commerce_Databaser_i_ett_sammanhang
// {
//     public class SearchMenu : Menu
//     {
//         public SearchMenu(UserService userService, EcommerceContext context)
//         {
//             AddCommand(new SearchProductsCommand(userService, context));
//         }

//         public override void Display()
//         {
//             Console.WriteLine("SearcProductCommand!");
//         }
//     }
// }