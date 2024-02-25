using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Settings
{
    public class ToolStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("buildNumber")]
        public string BuildNumber { get; set; }

    }
}
