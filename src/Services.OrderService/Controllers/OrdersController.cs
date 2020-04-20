using Microsoft.AspNetCore.Mvc;

namespace Services.OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetOrders()
        {
            return Ok("List of orders");
        }

        [HttpGet("{id:int}")]
        public ActionResult GetOrder(int id)
        {
            return Ok($"Order {id}");
        }
    }
}
