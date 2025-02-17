using customerDemoWebApi.Model;

namespace customerDemoWebApi.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();

        Task<Customer> GetCustomers(int Id);

        Task<Customer>CreateCustomers(Customer customer);

        Task<Customer>UpdateCustomers(Customer customer);

        Task<Customer> DeleteCustomers(int Id);
    }
}
