using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.Hateoas.Formatters;

namespace Services.Hateoas.Infrastructure
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddHateoas(this IMvcBuilder builder, Action<HateosOptions> configure = null)
        {
            builder.Services.AddTransient<IConfigureOptions<MvcOptions>, JsonHateoasOptionsSetup>();
            builder.Services.AddScoped<RepresentationEnricher>();

            if (configure != null)
            {
                builder.Services.Configure(configure);
            }

            return builder;
        }
    }

    public class HateosOptions
    {
        public bool RemoveSystemJsonFormatter { get; set; }
    }
}