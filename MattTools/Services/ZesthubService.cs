using MattTools.Models.Rossum;
using MattTools.Models.Zesthub;
using Models.Zesthub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Services
{
    public class ZesthubService : IZesthubService
    {

        private readonly HttpClient client;

        public ZesthubService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://oms.zesthub.com/api/v1/")
            };
        }

        private async Task<HttpResponseMessage> POST(string url, HttpContent content, string key)
        {

            //Set Header key
            client.DefaultRequestHeaders.Authorization = null;

            if (key != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            //POST
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Return Response
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private async Task<HttpResponseMessage> GET(string url, string key)
        {

            //Set Header key
            client.DefaultRequestHeaders.Authorization = null;

            if (key != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            //GET
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Return Response
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Response<LoginData>> Login(string email, string password)
        {


            //Create Payload
            Dictionary<string, string> payload = new Dictionary<string, string>
            {
                {"email" , email },
                {"password" , password }
            };

            // Serialize the payload to JSON
            string jsonPayload = JsonConvert.SerializeObject(payload);

            // Create the content for the POST request
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                //POST
                HttpResponseMessage response = await POST("login", content, null);

                string result = response.Content.ReadAsStringAsync().Result;
                Response<LoginData> loginResponse = JsonConvert.DeserializeObject<Response<LoginData>>(result);

                return loginResponse;

            }
            catch 
            {
                return null;
            }

        }

        public async Task<Response<LoginData>> Logout(int loginID, string key)
        {
            string url = $"logout/{loginID.ToString()}";

            try
            {
                //GET
                HttpResponseMessage response = await GET(url, key);

                string result = response.Content.ReadAsStringAsync().Result;
                Response<LoginData> logoutResponse = JsonConvert.DeserializeObject<Response<LoginData>>(result);

                return logoutResponse;

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Account>> GetAccounts(int supplierID, string key)
        {

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"warehouse_id", "0" },
                    {"territory_id", "0" },
                    {"order", "name" },
                    {"sort", "asc" } 
                };

                FormUrlEncodedContent dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
                string queryString = await dictFormUrlEncoded.ReadAsStringAsync();

                string url = $"pi/supplier-account/{supplierID}?{queryString}";

                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Response<AccountList> GetBankResponse = JsonConvert.DeserializeObject<Response<AccountList>>(result);

                    if (GetBankResponse.Data != null)
                    {
                        return GetBankResponse.Data.Accounts;
                    }

                    return null;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<(int, InvoiceData)> GetInvoiceID(string invoiceName, string key)
        {

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"length", "5" },
                    {"page",  "1"},
                    {"search", invoiceName },
                    {"order", "id" },
                    {"sort", "desc" },
                    {"territory_id", "0" },
                    {"territory_area_id", "0" },
                    {"status", "0" } //0 Open , 1 Confirmed, 3 Valid
                };

                FormUrlEncodedContent dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
                string queryString = await dictFormUrlEncoded.ReadAsStringAsync();

                string url = $"pi/vendor-invoice?{queryString}";

                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Response<Record<InvoiceData>> invoiceDataRecords = JsonConvert.DeserializeObject<Response<Record<InvoiceData>>>(result);

                    if (invoiceDataRecords.Data != null)
                    {
                        foreach (var invoiceData in invoiceDataRecords.Data.Records)
                        {
                            if (invoiceData.InvCode == invoiceName)
                                return (invoiceData.Id, invoiceData);
                        }
                    }

                    return (-1, null);
                }
                else
                {
                    return (-1, null);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


        }

        public async Task<SupplierInvoiceData> GetSupplierInvoiceData(int invoiceID, string key)
        {

            try
            {

                string url = $"pi/vendor-invoice-line/{invoiceID.ToString()}";

                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Response<SupplierInvoiceData> invoiceResponse = JsonConvert.DeserializeObject<Response<SupplierInvoiceData>>(result);

                    if (invoiceResponse.Data != null)
                    {
                        return invoiceResponse.Data;
                    }

                    return null;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

    }
}
