using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models.Zesthub
{
    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;

    public class InvoiceData
    {
        [JsonProperty("bank_id")]
        public int BankId { get; set; }
        [JsonProperty("code_internal")]
        public string CodeInternal { get; set; }
        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonProperty("data_payment")]
        public DataPayment DataPayment { get; set; }
        [JsonProperty("delivery_number")]
        public string DeliveryNumber { get; set; }
        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }
        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("inv_code")]
        public string InvCode { get; set; }
        [JsonProperty("inv_date")]
        public DateTime InvDate { get; set; }
        [JsonProperty("po_code")]
        public string PoCode { get; set; }
        [JsonProperty("po_code_external")]
        public string PoCodeExternal { get; set; }
        [JsonProperty("po_date")]
        public string PoDate { get; set; }
        [JsonProperty("po_type")]
        public string PoType { get; set; }
        [JsonProperty("posting_date")]
        public DateTime PostingDate { get; set; }
        [JsonProperty("project")]
        public bool Project { get; set; }
        [JsonProperty("reversal_posting_date")]
        public DateTime ReversalPostingDate { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("supplier_address")]
        public string SupplierAddress { get; set; }
        [JsonProperty("supplier_city")]
        public string SupplierCity { get; set; }
        [JsonProperty("supplier_code")]
        public string SupplierCode { get; set; }
        [JsonProperty("supplier_contact_name")]
        public string SupplierContactName { get; set; }
        [JsonProperty("supplier_name")]
        public string SupplierName { get; set; }
        [JsonProperty("supplier_phone")]
        public string SupplierPhone { get; set; }
        [JsonProperty("territory_id")]
        public int TerritoryId { get; set; }
        [JsonProperty("total_invoice_value")]
        public int TotalInvoiceValue { get; set; }
        [JsonProperty("tr_date")]
        public DateTime TrDate { get; set; }
        [JsonProperty("tr_number")]
        public string TrNumber { get; set; }
        [JsonProperty("warehouse_code")]
        public string WarehouseCode { get; set; }
        [JsonProperty("warehouse_name")]
        public string WarehouseName { get; set; }
    }

}
