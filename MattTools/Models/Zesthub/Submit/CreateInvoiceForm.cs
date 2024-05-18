using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Zesthub
{
    public class CreateInvoiceForm
    {

        [JsonProperty("pi_id")]
        public int PiId { get; set; }

        [JsonProperty("inv_code")]
        public string InvCode { get; set; }

        [JsonProperty("inv_date")]
        public DateTime InvDate { get; set; }

        [JsonProperty("due_invoice_period")]
        public int DueInvoicePeriod { get; set; }

        [JsonProperty("reference_purchase_code")]
        public string ReferencePurchaseCode { get; set; }

        [JsonProperty("payment_term_id")]
        public int PaymentTermId { get; set; }

        [JsonProperty("payment_term_name")]
        public string PaymentTermName { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("bank_id")]
        public int BankId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("total_inv_value")]
        public string TotalInvValue { get; set; }

        [JsonProperty("item_value")]
        public string ItemValue { get; set; }

        [JsonProperty("charge_value")]
        public string ChargeValue { get; set; }

        [JsonProperty("tax_value")]
        public string TaxValue { get; set; }

        [JsonProperty("data_line")]
        public List<DataLine> DataLine { get; set; }

        [JsonProperty("data_charge")]
        public List<object> DataCharge { get; set; }

        [JsonProperty("total_discount")]
        public string TotalDiscount { get; set; }

        [JsonProperty("status_submit")]
        public string StatusSubmit { get; set; }

        [JsonProperty("tr_number")]
        public string TrNumber { get; set; }

        [JsonProperty("tr_date")]
        public DateTime TrDate { get; set; }

        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
    }

   
}
