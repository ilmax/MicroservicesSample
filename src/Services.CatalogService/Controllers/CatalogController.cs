using Microsoft.AspNetCore.Mvc;
using Services.CatalogService.Model;
using Services.Hateoas;

namespace Services.CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ResourceController
    {
        [HttpGet("products")]
        public ActionResult GetProducts()
        {
            return Ok("List of products");
        }

        [HttpGet]
        public ActionResult GetCatalogResource()
        {
            return Resource(new CatalogDto
            {
                Description = "My catalog item",
                Sku = "ABC-123",
                PriceInCent = 1234
            });
        }

        [HttpGet("{sku}", Name = "catalog#sku")]
        public ActionResult GetCatalogItemBySku(string sku)
        {
            return Resource(new CatalogDto
            {
                Description = "My catalog item",
                Sku = sku,
                PriceInCent = 1234
            });
        }
    }
}