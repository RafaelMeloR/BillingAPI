using Microsoft.EntityFrameworkCore;

namespace BillingAPI.Models
{
    public class Context : DbContext
    {
        public Context()
        { }

        public Context(DbContextOptions<Context> options): base(options)
        { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersDetails> OrdersDetails { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
