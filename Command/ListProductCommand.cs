using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_commerce_Databaser_i_ett_sammanhang
{
    public class ListProductsCommand : BaseCommand
    {
        private readonly EcommerceContext _context;

        public ListProductsCommand(ConsoleKey triggerKey, IUserService userService, EcommerceContext context)
            : base(triggerKey, userService)
        {
            _context = context;
        }
        public override async Task Execute(Guid? currentUserId)
        {

        }
    }
}