using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Db
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomersDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
