using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models.Zesthub
{
    public class Record<T>
    {

        [JsonProperty("page_total")]
        public int PageTotal { get; set; }
        [JsonProperty("record_total")]
        public int RecordTotal { get; set; }
        [JsonProperty("record_total_per_page")]
        public int RecordTotalPerPage { get; set; }
        [JsonProperty("record_total_search")]
        public int RecordTotalSearch { get; set; }
        [JsonProperty("records")]
        public List<T> Records { get; set; }

    }
}
