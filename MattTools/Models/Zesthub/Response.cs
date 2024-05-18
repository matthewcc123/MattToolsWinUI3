using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MattTools.Models.Zesthub.LoginData;

namespace MattTools.Models.Zesthub
{
    public class Response<T>
    {

        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

    }
}
