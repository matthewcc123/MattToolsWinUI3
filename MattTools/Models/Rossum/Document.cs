using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MattTools.Models.Rossum
{
    public class Document
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreateDate { get; set; }
        [JsonProperty("annotations")]
        public List<string> AnnotationsURL { get; set; }
        [JsonProperty("original_file_name")]
        public string FileName;

    }
}
