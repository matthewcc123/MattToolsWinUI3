using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MattTools.Models.Rossum
{
    public class AuthenticationResponse
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
        [JsonProperty("non_field_errors")]
        public List<string> non_field_errors { get; set; }
        public bool LoggedOut { get; set; }
        public string Error { get; set; }

    }
}
