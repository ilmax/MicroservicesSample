using Microsoft.AspNetCore.Mvc;
using Services.Hateoas.Models;

namespace Services.Hateoas
{
    public class ResourceController : ControllerBase
    {
        public ActionResult Resource<T>(T resource) => Ok(new ObjectResource<T>(resource));
    }
}