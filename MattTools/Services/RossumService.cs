using MattTools.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.ObjectModel;
using MattTools.Models.Rossum;
using PdfSharp.Drawing;

namespace MattTools.Services
{
    public class RossumService : IRossumService
    {
        private readonly HttpClient client;

        public RossumService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://elis.app.rossum.ai/api/v1/")
            };
        }

        private async Task<HttpResponseMessage> POST(string url, HttpContent content, string key)
        {

            //Set Header key
            client.DefaultRequestHeaders.Authorization = null;

            if (key != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", key);

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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", key);

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

        public async Task<AuthenticationResponse> Login(string username, string password, string key)
        {
            //Create Payload
            Dictionary<string, string> payload = new Dictionary<string, string>
            {
                {"username" , username },
                {"password" , password }
            };

            // Serialize the payload to JSON
            string jsonPayload = JsonConvert.SerializeObject(payload);

            // Create the content for the POST request
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            //Check Login Using key or Not
            string url = (key == null || key == string.Empty) ? "auth/login" : "organizations/406";

            try
            {
                //POST
                HttpResponseMessage response = await POST(url, content, key);

                string result = response.Content.ReadAsStringAsync().Result;
                AuthenticationResponse authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(result);


                if (response.IsSuccessStatusCode)  //Logged In
                {
                    //Success
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden && key != null)
                {
                    //Login By Key
                    authResponse.Key = key;
                }
                else //Failed
                {

                    //Login By Key but Token Expired
                    if (key != null)
                    {
                        authResponse.Error = "Token Expired";
                    }
                    else
                    {
                        authResponse.Error = authResponse.non_field_errors?[0];
                    }

                }

                return authResponse;

            }
            catch (Exception ex)
            {
                return new AuthenticationResponse { Error = ex.Message };
            }


        }

        public async Task<AuthenticationResponse> Logout(string key)
        {
            string url = "auth/logout";

            //POST
            try
            {
                HttpResponseMessage response = await POST(url, null, key);

                string result = response.Content.ReadAsStringAsync().Result;
                AuthenticationResponse authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(result);

                if (response.IsSuccessStatusCode)
                    authResponse.LoggedOut = true;
                else
                    authResponse.Error = authResponse.Detail;

                return authResponse;
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse { Error = ex.Message };
            }


        }

        public async Task<ObservableCollection<Workspace>> GetWorkspaces(string key)
        {
            ObservableCollection<Workspace> workspaces = new ObservableCollection<Workspace>();

            try
            {

                PagingObject<Workspace> workspacePagination = await GetWorkspacePagination(null, key);

                while (workspacePagination != null)
                {
                    foreach (var workspace in workspacePagination.Results)
                    {
                        workspaces.Add(workspace);
                    }

                    if (workspacePagination.Pagination.NextURL != null)
                    {
                        // Retrieve the next page asynchronously
                        workspacePagination = await GetWorkspacePagination(workspacePagination.Pagination.NextURL.ToString(), key);
                    }
                    else
                    {
                        // No more pages available, exit the loop
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return workspaces;

        }

        private async Task<PagingObject<Workspace>> GetWorkspacePagination(string url, string key)
        {
            PagingObject<Workspace> workspacePagination = null;

            try
            {
                HttpResponseMessage response = await GET(url == null ? "workspaces" : url.Replace(client.BaseAddress.ToString(), string.Empty), key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    workspacePagination = JsonConvert.DeserializeObject<PagingObject<Workspace>>(result);
                }

                return workspacePagination;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<Queue> GetQueue(string url, string key)
        {
            Queue queue = null;

            try
            {
                HttpResponseMessage response = await GET(url.Replace(client.BaseAddress.ToString(), string.Empty), key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    queue = JsonConvert.DeserializeObject<Queue>(result);
                }

                return queue;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<Document> GetDocumentURL(string url, string key)
        {
            Document document = null;

            try
            {
                HttpResponseMessage response = await GET(url.Replace(client.BaseAddress.ToString(), string.Empty), key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    document = JsonConvert.DeserializeObject<Document>(result);
                }

                return document;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<PagingObject<Document>> GetDocument(string name, string key)
        {

            PagingObject<Document> documents = null;

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"original_file_name", name }
            };

                FormUrlEncodedContent dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
                string queryString = await dictFormUrlEncoded.ReadAsStringAsync();

                string url = $"documents?{queryString}";

                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    documents = JsonConvert.DeserializeObject<PagingObject<Document>>(result);
                }

                return documents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<Annotation> GetAnnotationURL(string url, string key)
        {
            Annotation annotation = null;

            try
            {
                HttpResponseMessage response = await GET(url.Replace(client.BaseAddress.ToString(), string.Empty), key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    annotation = JsonConvert.DeserializeObject<Annotation>(result);
                }

                return annotation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<PagingObject<Annotation>> GetAnnotation(DateTime from, DateTime to,string queueID, string key)
        {

            PagingObject<Annotation> annotations = null;

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"queue", queueID },
                {"ordering", "document__original_file_name" },
                {"arrived_at_before", to.ToString("yyyy-MM-dd") },
                {"arrived_at_after", from.ToString("yyyy-MM-dd") }
            };

                FormUrlEncodedContent dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
                string queryString = await dictFormUrlEncoded.ReadAsStringAsync();

                string url = $"annotations?{queryString}";

                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    annotations = JsonConvert.DeserializeObject<PagingObject<Annotation>>(result);
                }

                return annotations;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<string> GetJson(RossumItem rossumItem, string queueID , string key)
        {

            string json = "";

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"format", "json" },
                {"status",  rossumItem.Status},
                {"id", rossumItem.AnnotationID.ToString() }
            };

                FormUrlEncodedContent dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
                string queryString = await dictFormUrlEncoded.ReadAsStringAsync();

                string url = $"queues/{queueID}/export?{queryString}";

                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    json = response.Content.ReadAsStringAsync().Result;
                }

                return json;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


        }

        public async Task<byte[]> GetPdf(RossumItem rossumItem, string key)
        {
            string url = $"documents/{rossumItem.DocumentID}/content";
            byte[] data = null;

            try
            {
                HttpResponseMessage response = await GET(url, key);

                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsByteArrayAsync().Result;
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


        }

    }
}
