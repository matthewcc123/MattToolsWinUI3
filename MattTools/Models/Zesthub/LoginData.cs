using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models.Zesthub
{
    public class LoginData
    {

        [JsonProperty("log_login_id")]
        public int LoginID { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }

    }
}
