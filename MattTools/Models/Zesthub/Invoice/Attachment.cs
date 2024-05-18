using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Zesthub
{
    public class Attachment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
