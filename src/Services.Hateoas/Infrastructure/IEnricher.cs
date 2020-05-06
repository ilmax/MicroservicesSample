using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Hateoas.Infrastructure
{
    public interface IEnricher
    {
        Task<bool> Match(object target);
        Task Process(object resource, HttpContext context);
    }
}