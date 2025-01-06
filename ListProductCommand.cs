using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    public class ListProductsCommand : BaseCommand
    {
        private readonly IProductService _productService;

        public ListProductsCommand(ConsoleKey triggerKey, IUserService userService, IProductService productService)
            : base(triggerKey, userService)
        {
            _productService = productService;
        }
        public override async Task Execute(Guid? currentUserId)
        {
            await _productService.GetAllProducts();
        }
    }
}