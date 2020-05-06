using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Hateoas.Models;

namespace Services.Hateoas.Infrastructure
{
    public abstract class Enricher<T> : IEnricher where T : Resource
    {
        public virtual Task<bool> Match(object target) => Task.FromResult(target is T);
        public Task Process(object resource, HttpContext context) => Process((T)resource, context);
        public abstract Task Process(T resource, HttpContext context);
    }
}