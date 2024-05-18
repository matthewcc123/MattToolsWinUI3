using MattTools.Models.Zesthub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Zesthub
{
    public class AccountList
    {
        [JsonProperty("account")]
        public List<Account> Accounts { get; set; }
    }
}
