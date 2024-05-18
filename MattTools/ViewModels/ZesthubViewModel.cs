using Controls;
using MattTools.Helper;
using MattTools.Models;
using MattTools.Models.Rossum;
using MattTools.Models.Zesthub;
using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Models.Zesthub;
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
using Windows.Web.Http;

namespace MattTools.ViewModels
{
    public class ZesthubViewModel : PropertyChangedBaseModel
    {

        private readonly IZesthubService _zesthubService;
        private readonly IUserService _userService;

        //Main Property
        public SupplierInvoiceForm supplierInvoiceFormControl { get; set; }
        public int InvoiceID { get; set; }

        //Highlight Property
        private string _setAccountNumber;
        public string SetAccountNumber
        {
            get => _setAccountNumber;
            set
            {
                if (_setAccountNumber != value)
                {
                    _setAccountNumber = value;
                    supplierInvoiceFormControl.Check();
                    OnPropertyChanged(nameof(SetAccountNumber));
                }
            }
        }

        private DateTime? _setDueDate;
        public DateTime? SetDueDate
        {
            get => _setDueDate;
            set
            {
                if (_setDueDate != value)
                {
                    _setDueDate = value;
                    supplierInvoiceFormControl.Check();
                    OnPropertyChanged(nameof(SetDueDate));
                }
            }
        }

        private DateTime? _setPlanDate;
        public DateTime? SetPlanDate
        {
            get => _setPlanDate;
            set
            {
                if (_setPlanDate != value)
                {
                    _setPlanDate = value;
                    supplierInvoiceFormControl.Check();
                    OnPropertyChanged(nameof(SetPlanDate));
                }
            }
        }

        //Checking Property
        #region UserSettingProperty
        public bool IsLoggedIn { get; set; }
        public bool IsHaveUserKey { get { return _userService.GetUserSetting<string>("Zesthub.Key") != null; } }
        public string SavedUsername { get { return _userService.GetUserSetting<string>("Zesthub.Username"); } }

        public string UserKey { get { return _userService.GetUserSetting<string>("Zesthub.Key"); } }

        public int LoginID { get { return _userService.GetUserSetting<int>("Zesthub.LoginID"); } }
        #endregion

        public ZesthubViewModel()
        {
            //Get Services
            App app = (App)Application.Current;
            _zesthubService = app.ServiceProvider.GetService<IZesthubService>();
            _userService = app.ServiceProvider.GetService<IUserService>();

        }

        public async Task<Response<LoginData>> Login(string username, string password)
        {
            Response<LoginData> response = await _zesthubService.Login(username, password);

            if (response.Code == (int)HttpStatusCode.Ok)
            {
                //Logged In
                _userService.SetUserSetting("Zesthub.Username", username);
                _userService.SetUserSetting("Zesthub.Key", response.Data.Token);
                _userService.SetUserSetting("Zesthub.LoginID", response.Data.LoginID);
                IsLoggedIn = true;

                //Reset Setup
                SetAccountNumber = string.Empty;
                SetDueDate = DateTime.Today.AddDays(7);
                SetPlanDate = DateTime.Today.AddDays(7);

            }
            else
            {
                //Failed Login
                _userService.SetUserSetting("Zesthub.Username", null);
                _userService.SetUserSetting("Zesthub.Key", null);
                _userService.SetUserSetting("Zesthub.LoginID", null);
            }

            return response;

        }

        public async Task<Response<LoginData>> Logout()
        {
            Response<LoginData> response = await _zesthubService.Logout(LoginID, UserKey);

            if (response.Code == (int)HttpStatusCode.Ok)
            {
                //Logged Out
                _userService.SetUserSetting("Zesthub.Key", null);
                _userService.SetUserSetting("Zesthub.LoginID", null);
                IsLoggedIn = false;
            }

            return response;

        }

        public async Task<(int, InvoiceData)> GetInvoice(string name)
        {
            //Reset ID
            InvoiceID = -1;

            var result = await _zesthubService.GetInvoiceID(name, UserKey);

            if (result.Item2 != null)
            {
                InvoiceID = result.Item1;
                return (result.Item1, result.Item2);
            }

            return (InvoiceID,null);
        }

        public async Task<SupplierInvoiceData> GetSupplierInvoiceData()
        {

            var result = await _zesthubService.GetSupplierInvoiceData(InvoiceID, UserKey);

            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<List<Account>> GetAccountList(int supplierID)
        {
            var result = await _zesthubService.GetAccounts(supplierID, UserKey);

            if (result != null)
            {
                return result;
            }

            return null;
        }

    }
}
