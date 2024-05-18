using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models.Zesthub
{
    public class DataPayment
    {

        public int ID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Code { get; set; }
        public string InvoiceCode { get; set; }
        public string PurchaseOrderCode { get; set; }
        public string SupplierCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string ReferenceCode { get; set; }
        public string Notes { get; set; }
        public string WarehouseCode { get; set; }
        public int InvoiceValue { get; set; }
        public int PaidInvoiceValue { get; set; }
        public int UnpaidInvoiceValue { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int TerritoryID { get; set; }
        public int TerritoryAreaID { get; set; }
        public int CompanyID { get; set; }
        public int StatusPayment { get; set; }
        public int CetakVoucher { get; set; }
        public int PurchasePaymentRequestID { get; set; }
        public int DebitNoteID { get; set; }
        public int CurrencyExchangeRateID { get; set; }
        public int CurrencyExchangeRate { get; set; }

    }
}
