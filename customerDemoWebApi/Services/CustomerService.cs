using customerDemoWebApi.Data;
using customerDemoWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace customerDemoWebApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _customerDbContext;
        public CustomerService(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<Customer>CreateCustomers(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task< Customer> DeleteCustomers(int Id)
        {
            var customer = await _customerDbContext.Customers.FindAsync(Id);

           
            if (customer != null)
            {
                _customerDbContext.Customers.Remove(customer);
                await _customerDbContext.SaveChangesAsync();
            }
            return customer;
       
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _customerDbContext.Customers.ToListAsync();
            return customers;
            
        }

        public async Task<Customer> GetCustomers(int Id)
        {
            var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(x => x.Id == Id);
            return customer;

      
        }

        public async Task<Customer> UpdateCustomers(Customer customer)
        {
            var customerInList = await _customerDbContext.Customers.FindAsync(customer.Id);

            if (customerInList != null)
            {
                customerInList.Fistname = customer.Fistname;
                customerInList.Lastname = customer.Lastname;
                customerInList.City = customer.City;
                await _customerDbContext.SaveChangesAsync();

            }
            return customerInList;
         
        }
    }
}
