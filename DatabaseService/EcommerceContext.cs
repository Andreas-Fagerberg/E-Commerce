using System.Net.Sockets;
using E_commerce_Databaser_i_ett_sammanhang.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class EcommerceContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shopping_Cart> Shopping_Cart { get; set; } // Characters correct?
    public DbSet<ProductOrders> Product_orders { get; set; } // Characters correct?
    public DbSet<Address> Address { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Host=localhost;Database=ECommerce;Username=postgres;Password=password");
    }

    /*
   Om man vill konfigurera modellerna lite extra: fler constraints exempelvis, då kan man använda 'OnModelCreating'.
   protected override void OnModelCreating(ModelBuilder builder)
   {
       builder.Entity<Todo>()
           .Property(todo => todo.Title)
           .IsRequired();
   }
   
    //////////// Is this something we need/want to implement in our application ? ////////////////
   */
}
