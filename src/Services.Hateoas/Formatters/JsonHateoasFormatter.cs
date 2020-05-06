using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Services.Hateoas.Infrastructure;
using System.Buffers;
using System.Text;
using System.Threading.Tasks;

namespace Services.Hateoas.Formatters
{
    public class JsonHateoasFormatter : NewtonsoftJsonOutputFormatter
    {
        private const string ContentType = "application/vnd.hateoas+json";

        public JsonHateoasFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool, MvcOptions mvcOptions)
            : base(serializerSettings, charPool, mvcOptions)
        {
            SupportedMediaTypes.Add(ContentType);
        }

        private static T GetService<T>(OutputFormatterWriteContext context)
        {
            return (T)context.HttpContext.RequestServices.GetService(typeof(T));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context.Object is SerializableError)
            {
                await base.WriteResponseBodyAsync(context, selectedEncoding);
                return;
            }

            var resourceEnricher = GetService<RepresentationEnricher>(context);
            await resourceEnricher.OnResultExecutionAsync(context);

            await base.WriteResponseBodyAsync(context, selectedEncoding);
        }
    }
}