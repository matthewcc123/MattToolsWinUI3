using MattTools.Helper;
using MattTools.Models;
using MattTools.Models.Rossum;
using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Maps;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace MattTools.ViewModels
{
    public class RossumExtractorViewModel
    {

        private readonly IRossumService _rossumService;
        private readonly IUserService _userService;

        //Observeable Property
        public ObservableCollection<Workspace> Workspaces { get; set; }
        public ObservableCollection<Queue> Queues { get; set; }
        public ObservableCollection<RossumItem> RossumItems { get; set; }


        //Checking Property
        public bool IsLoggedIn { get; set; }
        public bool IsHaveUserKey { get { return _userService.GetUserSetting<string>("Rossum.Key") != null; } }
        public string SavedUsername { get { return _userService.GetUserSetting<string>("Rossum.Username"); } }

        public string UserKey { get { return _userService.GetUserSetting<string>("Rossum.Key"); } }

        public RossumExtractorViewModel()
        {
            //Get Services
            App app = (App)Application.Current;
            _rossumService = app.ServiceProvider.GetService<IRossumService>();
            _userService = app.ServiceProvider.GetService<IUserService>();

            //Observeable
            Workspaces = new ObservableCollection<Workspace>();
            Queues = new ObservableCollection<Queue>();
            RossumItems = new ObservableCollection<RossumItem>();

        }

        public async Task<AuthenticationResponse> Login(string username, string password)
        {
            IsLoggedIn = false;
            AuthenticationResponse response = await _rossumService.Login(username, password, null);

            if (response.Error == null)
            {
                //Logged In
                _userService.SetUserSetting("Rossum.Username", username);
                _userService.SetUserSetting("Rossum.Key", response.Key);
                IsLoggedIn = true;
            }
            else
            {
                //Failed Login
                _userService.SetUserSetting("Rossum.Username", null);
                _userService.SetUserSetting("Rossum.Key", null);
            }

            return response;

        }

        public async Task<AuthenticationResponse> LoginWithKey()
        {
            IsLoggedIn = false;

            AuthenticationResponse response = await _rossumService.Login("","", UserKey);

            if (response.Error != null)
            {
                //Failed Login
                _userService.SetUserSetting("Rossum.Username", null);
                _userService.SetUserSetting("Rossum.Key", null);
            }
            else
            {
                //Logged In
                IsLoggedIn = true;
            }

            return response;

        }

        public async Task<AuthenticationResponse> Logout()
        {
            AuthenticationResponse response = await _rossumService.Logout(UserKey);

            if (response.Error == null)
            {
                //Success Logout
                IsLoggedIn = false;
                _userService.SetUserSetting("Rossum.Username", null);
                _userService.SetUserSetting("Rossum.Key", null);
            }

            return response;

        }

        public async Task GetWorkspaces()
        {
            Workspaces.Clear();

            ObservableCollection<Workspace> NewWorkspaces = await _rossumService.GetWorkspaces(UserKey);

            foreach (var workspace in NewWorkspaces)
            {
                Workspaces.Add(workspace);
            }
        }

        public async Task GetQueues(int workspaceIndex)
        {
            Queues.Clear();

            foreach (var queueURL in Workspaces[workspaceIndex].QueuesURL)
            {
                Queue queue = await _rossumService.GetQueue(queueURL, UserKey);

                if (queue != null && queue.Status == "active")
                {
                    Queues.Add(queue);
                }
            }
        }

        public async Task Find(string[] filters, string queueID)
        {
            RossumItems.Clear();
            List<RossumItem> NewItems = new List<RossumItem>();

            foreach (var filter in filters)
            {
                if (filter == "")
                    continue;

                PagingObject<Document> documents = await _rossumService.GetDocument(filter + ".pdf", UserKey);

                RossumItem rossumItem = new RossumItem
                {
                    Name = filter,
                    Status = "Not Found"
                };

                if (documents != null)
                {

                    if (documents.Results.Count == 0)
                    {
                        //Add to List
                        NewItems.Add(rossumItem);
                        continue;
                    }

                    foreach (var document in documents.Results)
                    {
                        //Get Annotation from document
                        Annotation annotation = await _rossumService.GetAnnotation(document.AnnotationsURL[0], UserKey);

                        //Check if from the same QueueID
                        string annotationQueue = annotation.QueueURL.Split("/").Last();

                        if (queueID != annotationQueue)
                        {
                            //Add to List
                            NewItems.Add(rossumItem);
                            continue;
                        }
                        else
                        {
                            //Got Complete RossumItem
                            rossumItem.DocumentID = document.Id;
                            rossumItem.AnnotationID = annotation.Id;
                            rossumItem.Status = annotation.Status;
                            rossumItem.CreateDate = document.CreateDate.ToLocalTime();
                            //Add To List Complete Item
                            NewItems.Add(rossumItem);
                        }

                    }

                }
            }

            foreach (var item in NewItems)
            {
                RossumItems.Add(item);
            }

        }

        public async Task<string> ExtractJSON(Window window, List<RossumItem> rossumItems, string queueID)
        {

            string resultText = string.Empty;
            string path = "";

            var folderPicker = new FolderPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

            // Set options for folder picker
            folderPicker.ViewMode = PickerViewMode.List;

            //Open FolderPicker
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder == null)
                return null;


            foreach (var rossumItem in rossumItems)
            {
                string json = await _rossumService.GetJson(rossumItem, queueID, UserKey);

                if (json == "")
                {
                    resultText += "\u2022 " + rossumItem.Name + " unable to extract JSON" + System.Environment.NewLine;
                }
                else
                {
                    resultText += "\u2022 " + rossumItem.Name + ".json extracted" + System.Environment.NewLine;
                    string filePath = path + @"\" + rossumItem.Name + ".json";

                    StorageFile jsonFile = await folder.CreateFileAsync($"{rossumItem.Name}.json", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(jsonFile, json);
                }
            }

            return resultText;

        }

        public async Task<string> ExtractPDF(Window window, List<RossumItem> rossumItems)
        {

            string resultText = string.Empty;
            string path = "";

            var folderPicker = new FolderPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

            // Set options for folder picker
            folderPicker.ViewMode = PickerViewMode.List;

            //Open FolderPicker
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder == null)
                return null;


            foreach (var rossumItem in rossumItems)
            {
                byte[] pdfByte = await _rossumService.GetPdf(rossumItem, UserKey);

                if (pdfByte == null)
                {
                    resultText += "\u2022 " + rossumItem.Name + " unable to extract PDF" + System.Environment.NewLine;
                }
                else
                {
                    resultText += "\u2022 " + rossumItem.Name + ".pdf extracted" + System.Environment.NewLine;
                    string filePath = path + @"\" + rossumItem.Name + ".pdf";

                    StorageFile jsonFile = await folder.CreateFileAsync($"{rossumItem.Name}.pdf", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteBytesAsync(jsonFile, pdfByte);
                }
            }

            return resultText;

        }


    }
}
