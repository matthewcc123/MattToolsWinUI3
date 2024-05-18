using MattTools.Models.Zesthub;
using Models.Zesthub;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Services
{
    public interface IZesthubService
    {

        Task<Response<LoginData>> Login(string username, string password);
        Task<Response<LoginData>> Logout(int loginID, string key);
        Task<(int, InvoiceData)> GetInvoiceID(string invoiceName, string key);
        Task<SupplierInvoiceData> GetSupplierInvoiceData(int invoiceID, string key);
        Task<List<Account>> GetAccounts(int supplierID, string key);

    }
}
