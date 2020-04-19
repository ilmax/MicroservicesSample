using Microsoft.AspNetCore.Mvc;

namespace Services.CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        [HttpGet("products")]
        public ActionResult GetProducts()
        {
            return Ok("List of products");
        }
    }
}