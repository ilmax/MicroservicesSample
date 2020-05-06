using System;
using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Services.Hateoas.Infrastructure;

namespace Services.Hateoas.Formatters
{
    internal class JsonHateoasOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly MvcNewtonsoftJsonOptions _jsonOptions;
        private readonly ArrayPool<char> _charPool;
        private readonly HateosOptions _hateoasOptions;

        public JsonHateoasOptionsSetup(IOptions<MvcNewtonsoftJsonOptions> jsonOptions, ArrayPool<char> charPool, IOptions<HateosOptions> hateoasOptions)
        {
            if (jsonOptions == null)
            {
                throw new ArgumentNullException(nameof(jsonOptions));
            }

            if (hateoasOptions == null)
            {
                throw new ArgumentNullException(nameof(hateoasOptions));
            }

            _jsonOptions = jsonOptions.Value;
            _charPool = charPool ?? throw new ArgumentNullException(nameof(charPool));
            _hateoasOptions = hateoasOptions.Value;
        }

        public void Configure(MvcOptions options)
        {
            options.OutputFormatters.Add(new JsonHateoasFormatter(_jsonOptions.SerializerSettings, _charPool, options));

            if (_hateoasOptions.RemoveSystemJsonFormatter)
            {
                options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            }
        }
    }
}