using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Jiahao Yu -2134609
* Date: 		09 Nov 2023
* Class Name: 	OrdersController.cs
* Description: 	
The OrdersController class is an ASP.NET Core controller responsible 
    for handling HTTP requests related to orders, utilizing the functionality provided
    by the IOrdersProvider interface, and offering an endpoint for retrieving orders associated 
    with a specific customer ID.
  */
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;
        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
