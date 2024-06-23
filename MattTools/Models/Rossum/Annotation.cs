using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models.Rossum
{
    public class Annotation
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("queue")]
        public string QueueURL { get; set; }
        [JsonProperty("document")]
        public string DocumentURL { get; set; }
    }
}
