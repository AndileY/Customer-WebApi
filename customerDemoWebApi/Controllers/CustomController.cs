using customerDemoWebApi.Data;
using customerDemoWebApi.Model;
using customerDemoWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace customerDemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        //private readonly CustomerDbContext _customerDbContext;
        private readonly ICustomerService _customerService;

        public CustomController(ICustomerService customerService)
        {
            _customerService = customerService;
            
        }


      /*  public static List<Customer> customers = new()
        {
            new Customer { Id = 1, Fistname = "Slindokuhle", Lastname="Mosana", City ="Durban"},
            new Customer { Id = 2, Fistname = "Nomthandazo", Lastname = "Nxele", City = "Inanda"},
            new Customer { Id = 3, Fistname = "Melikhaya", Lastname = "Mhlongo", City = "Mashu"}

        };*/

        [HttpGet] 
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult>GetCustomers(int Id)
        {
            var customer = await _customerService.GetCustomers(Id);
  
            if(customer == null)
            {
                return NotFound("customers not found");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCustomers(Customer customer)
        {
            var createCustomers = await _customerService.CreateCustomers(customer);
            return Ok(createCustomers);


        }

        [HttpPut("{Id}")]
        public async Task<IActionResult>UpdateCustomers(Customer customer)
        {
            var customerInList = await _customerService.UpdateCustomers(customer);


            if (customerInList == null)
            {
                return NotFound("invalid customer details");
            }
            return Ok(customerInList);

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomers(int Id)
        {
            var customer = await _customerService.DeleteCustomers(Id);

            if (customer == null)
            {
                return NotFound("customers not found");
            }
           

            return Ok(customer);
            
        }
        
        
    }
}
