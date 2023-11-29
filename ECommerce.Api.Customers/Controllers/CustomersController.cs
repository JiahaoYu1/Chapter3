using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Jiahao Yu -2134609
* Date: 		09 Nov 2023
* Class Name: 	CustomersController.cs
* Description: 	The CustomersController class is an ASP.NET Core controller responsible 
* for handling HTTP requests related to customer data by utilizing the functionality provided 
* by the ICustomersProvider interface, offering endpoints for retrieving all customers and a 
* specific customer by ID.
   */

    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;
        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
