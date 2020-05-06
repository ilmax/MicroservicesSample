using Microsoft.AspNetCore.Mvc.Formatters;
using Services.Hateoas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Hateoas.Infrastructure
{
    public class RepresentationEnricher
    {
        private readonly IEnumerable<IEnricher> _enrichers;

        public RepresentationEnricher(IEnumerable<IEnricher> enrichers)
        {
            _enrichers = enrichers ?? throw new ArgumentNullException(nameof(enrichers));
        }

        public async Task OnResultExecutionAsync(OutputFormatterWriteContext context)
        {
            if (context.Object is Resource resource)
            {
                foreach (var enricher in _enrichers)
                {
                    if (await enricher.Match(resource))
                    {
                        await enricher.Process(resource, context.HttpContext);
                    }
                }
            }
        }
    }
}