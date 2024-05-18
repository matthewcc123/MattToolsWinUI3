using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Zesthub
{
    public class Account
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("DeletedAt")]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("AccountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("AccountName")]
        public string AccountName { get; set; }

        [JsonProperty("SupplierID")]
        public int SupplierID { get; set; }

        [JsonProperty("BankID")]
        public int BankID { get; set; }

        [JsonProperty("IsDefault")]
        public int IsDefault { get; set; }

        [JsonProperty("BankCode")]
        public string BankCode { get; set; }

        [JsonProperty("BankName")]
        public string BankName { get; set; }

        [JsonProperty("BankBranch")]
        public string BankBranch { get; set; }

        [JsonProperty("SwiftCode")]
        public string SwiftCode { get; set; }

        [JsonProperty("TerritoryID")]
        public int TerritoryID { get; set; }

        public string AccountComboBoxText { get { return $"{BankName} - {AccountNumber} {AccountName}"; } }

    }
}
