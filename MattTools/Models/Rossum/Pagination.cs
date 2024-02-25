using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MattTools.Models.Rossum
{
    public class Pagination
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("total_pages")]
        public int Pages { get; set; }
        [JsonProperty("next")]
        public object NextURL { get; set; }
        [JsonProperty("previous")]
        public object PreviousURL { get; set; }
    }

    public class PagingObject<T>
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}
