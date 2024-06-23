using MattTools.Models;
using MattTools.Models.Rossum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Services
{
    public interface IRossumService
    {

        Task<AuthenticationResponse> Login(string username, string password, string key);
        Task<AuthenticationResponse> Logout(string key);
        Task<ObservableCollection<Workspace>> GetWorkspaces(string key);
        Task<Queue> GetQueue(string url, string key);
        Task<Document> GetDocumentURL(string url, string key);
        Task<PagingObject<Document>> GetDocument(string name, string key);
        Task<Annotation> GetAnnotationURL(string url, string key);
        Task<PagingObject<Annotation>> GetAnnotation(DateTime from, DateTime to, string queueID, string key);
        Task<string> GetJson(RossumItem rossumItem, string queueID, string key);
        Task<byte[]> GetPdf(RossumItem rossumItem, string key);

    }
}
