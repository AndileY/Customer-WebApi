using customerDemoWebApi.Model;
using Microsoft.EntityFrameworkCore;


namespace customerDemoWebApi.Data
{
    public class CustomerDbContext : DbContext
    {

        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }



    }
   


}
