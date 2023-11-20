using APIarchitecturePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace APIarchitecturePractice.Contexts
{
    public class DeliveryDBContext : DbContext
    {

        public DeliveryDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }


        
    }
}
