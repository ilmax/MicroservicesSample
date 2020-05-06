using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services.Hateoas.Models
{
    public abstract class Resource
    {
        public Resource(object data)
        {
            Data = data;
        }

        [JsonProperty("data")]
        public virtual object Data { get; }

        [JsonProperty("_links", Order = 2)]
        public virtual List<Link> Links { get; } = new List<Link>();
    }
}