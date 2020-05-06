using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Services.CatalogService.Model;
using Services.Hateoas.Infrastructure;
using Services.Hateoas.Models;

namespace Services.CatalogService.Hypermedia
{
    public class CatalogEnricher : Enricher<ObjectResource<CatalogDto>>
    {
        private readonly LinkGenerator _linkGenerator;

        public CatalogEnricher(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
        }

        public override Task Process(ObjectResource<CatalogDto> resource, HttpContext context)
        {
            resource.Links.Add(new Link("self", _linkGenerator.GetPathByName(context, "catalog#sku", new { sku = resource.Data.Sku })));

            return Task.CompletedTask;
        }
    }
}