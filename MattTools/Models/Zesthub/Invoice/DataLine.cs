using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Zesthub
{
    public class DataLine
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("item_unit")]
        public string ItemUnit { get; set; }

        [JsonProperty("amount_uom")]
        public int AmountUom { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("discount_inv")]
        public decimal DiscountInv { get; set; }

        [JsonProperty("net")]
        public decimal Net { get; set; }

        [JsonProperty("net_inv")]
        public decimal NetInv { get; set; }

        [JsonProperty("net_po")]
        public decimal NetPo { get; set; }

        [JsonProperty("net_received")]
        public decimal NetReceived { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("price_po")]
        public decimal PricePo { get; set; }

        [JsonProperty("qty_po")]
        public int QtyPo { get; set; }

        [JsonProperty("qty_received")]
        public int QtyReceived { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("tax")]
        public decimal Tax { get; set; }

        [JsonProperty("tax_rate")]
        public decimal TaxRate { get; set; }

        public decimal TaxCalculated { get { return NetInv * (TaxRate / 100); } }
        public decimal TotalPricePO { get { return PricePo * QtyPo; } }
        public decimal TotalPriceInvoice { get { return Price * Quantity; } }

        public bool NotMatch { get { return ((QtyPo != Quantity)
                    || (Math.Round(TotalPricePO, 2) != Math.Round(TotalPriceInvoice, 2))
                    || (Math.Round(Discount, 2) != Math.Round(DiscountInv, 2))
                    || (Math.Round(NetPo, 2) != Math.Round(NetInv, 2))
                    || (Math.Round(Tax, 2) != Math.Round(TaxCalculated, 2))
                    ); } }
    }
}
