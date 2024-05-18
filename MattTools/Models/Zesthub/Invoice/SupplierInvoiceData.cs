using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Zesthub
{

    public class SupplierInvoiceData
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("bank_id")]
        public int BankId { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("charge_value")]
        public decimal ChargeValue { get; set; }

        [JsonProperty("charge_value_po")]
        public decimal ChargeValuePo { get; set; }

        [JsonProperty("charge_value_received")]
        public decimal ChargeValueReceived { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("data_attachment")]
        public List<Attachment> DataAttachment { get; set; }

        [JsonProperty("data_charge")]
        public List<Charge> DataCharge { get; set; }

        [JsonProperty("data_line")]
        public List<DataLine> DataLine { get; set; }

        [JsonProperty("delta_charges_value")]
        public decimal DeltaChargesValue { get; set; }

        [JsonProperty("delta_discount_value")]
        public decimal DeltaDiscountValue { get; set; }

        [JsonProperty("delta_item_value")]
        public decimal DeltaItemValue { get; set; }

        [JsonProperty("delta_tax_value")]
        public decimal DeltaTaxValue { get; set; }

        [JsonProperty("delta_total_value")]
        public decimal DeltaTotalValue { get; set; }

        [JsonProperty("discount_value_po")]
        public decimal DiscountValuePo { get; set; }

        [JsonProperty("discount_value_received")]
        public decimal DiscountValueReceived { get; set; }

        [JsonProperty("dn_date")]
        public DateTime DnDate { get; set; }

        [JsonProperty("gr_codes")]
        public string GrCodes { get; set; }

        [JsonProperty("inv_date")]
        public DateTime InvDate { get; set; }

        [JsonProperty("invoice_baseline")]
        public string InvoiceBaseline { get; set; }

        [JsonProperty("item_value")]
        public decimal ItemValue { get; set; }

        [JsonProperty("item_value_po")]
        public decimal ItemValuePo { get; set; }

        [JsonProperty("item_value_received")]
        public decimal ItemValueReceived { get; set; }

        [JsonProperty("payment_term_id")]
        public int PaymentTermId { get; set; }

        [JsonProperty("payment_term_name")]
        public string PaymentTermName { get; set; }

        [JsonProperty("po_code")]
        public string PoCode { get; set; }

        [JsonProperty("po_code_external")]
        public string PoCodeExternal { get; set; }

        [JsonProperty("received_date")]
        public DateTime ReceivedDate { get; set; }

        [JsonProperty("supplier_id")]
        public int SupplierId { get; set; }

        [JsonProperty("supplier_invoice_notes")]
        public string SupplierInvoiceNotes { get; set; }

        [JsonProperty("supplier_name")]
        public string SupplierName { get; set; }

        [JsonProperty("tax_value")]
        public decimal TaxValue { get; set; }

        [JsonProperty("tax_value_po")]
        public decimal TaxValuePo { get; set; }

        [JsonProperty("tax_value_received")]
        public decimal TaxValueReceived { get; set; }

        [JsonProperty("total_discount")]
        public decimal TotalDiscount { get; set; }

        [JsonProperty("total_invoice_value")]
        public decimal TotalInvoiceValue { get; set; }

        [JsonProperty("total_po_value")]
        public decimal TotalPoValue { get; set; }

        [JsonProperty("total_received_value")]
        public decimal TotalReceivedValue { get; set; }

        [JsonProperty("tr_date")]
        public DateTime TrDate { get; set; }

        [JsonProperty("tr_number")]
        public string TrNumber { get; set; }

        // Nested classes for DataAttachment, DataCharge, and DataLine with JsonProperty attributes


    }

}
