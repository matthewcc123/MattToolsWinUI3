using MattTools.Helper;
using MattTools.Models.Rossum;
using MattTools.Models.Zesthub;
using MattTools.Services;
using MattTools.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Models.Zesthub;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZesthubView : Page
    {
        public ZesthubViewModel ViewModel { get; set; }

        private bool LoginViewIsEnabled
        {
            set
            {
                LoginButton.IsEnabled = value;
                LoginEmailBox.IsEnabled = value;
                LoginPasswordBox.IsEnabled = value;
            }
        }
        private bool MainViewIsEnabled
        {
            set
            {

            }
        }

        private void UpdateView()
        {
            LoginView.Visibility = ViewModel.IsLoggedIn ? Visibility.Collapsed : Visibility.Visible;
            MainView.Visibility = ViewModel.IsLoggedIn ? Visibility.Visible : Visibility.Collapsed;

            //LoggedAccountText.Text = ViewModel.SavedUsername;

        }

        public ZesthubView()
        {
            this.InitializeComponent();
            ViewModel = new ZesthubViewModel();
            this.DataContext = ViewModel;

            ViewModel.supplierInvoiceFormControl = SupplierInvoiceFormControl;

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async Task Login()
        {
            LoginViewIsEnabled = false;
            MainViewIsEnabled = false;
            LoginProgressBar.IsIndeterminate = true;
            LoginProgressBar.Visibility = Visibility.Visible;

            Response<LoginData> response = await ViewModel.Login(LoginEmailBox.Text, LoginPasswordBox.Password);

            if (response != null)
            {

                if (response.Code != (int)HttpStatusCode.OK)
                {
                    DialogHelper.CreateDialog("Login Failed", response.Message, this);
                }
                else
                {
                    //Success
                    DialogHelper.CreateDialog("Login Success", $"Welcome {LoginEmailBox.Text}! {System.Environment.NewLine}" +
                                $"Key : {response.Data.Token}", this);

                    LoggedAccountText.Text = ViewModel.SavedUsername;
                }

            }

            LoginProgressBar.IsIndeterminate = false;
            LoginProgressBar.Visibility = Visibility.Collapsed;

            UpdateView();
            LoginViewIsEnabled = true;
            MainViewIsEnabled = true;
        }


        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (LoginEmailBox.Text == string.Empty || LoginPasswordBox.Password == string.Empty)
            {
                DialogHelper.CreateDialog("Login Failed", "Please fill the form.", this);
                return;
            }

            await Login();

        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            Response<LoginData> response = await ViewModel.Logout();

            if (response != null)
            {

                if (response.Code != (int)HttpStatusCode.OK)
                {
                    DialogHelper.CreateDialog("Logout Failed", response.Message, this);
                }

            }

            UpdateView();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (ViewModel.SavedUsername != null)
            {
                LoginEmailBox.Text = ViewModel.SavedUsername;
            }

            if (ViewModel.IsHaveUserKey && ViewModel.LoginID != 0)
            {
                Response<LoginData> response = await ViewModel.Logout();
            }

        }

        private async void Find_Click(object sender, RoutedEventArgs e)
        {

            List<Account> accounts;
            SupplierInvoiceData supplierInvoice;
            InvoiceData invoiceData;

            //Get Invoice Data && Update ViewModel InvoiceID
            var invoiceDataResult = await ViewModel.GetInvoice(InvoiceSearchBox.Text);

            if (invoiceDataResult.Item2 == null)
                return;

            invoiceData = invoiceDataResult.Item2;

            //Get SupplierInvoice
            supplierInvoice = await ViewModel.GetSupplierInvoiceData();

            if (supplierInvoice == null)
                return;

            //Get AccountList
            accounts = await ViewModel.GetAccountList(supplierInvoice.SupplierId);

            if (accounts == null)
                return;

            //Setup Form
            SupplierInvoiceFormControl.SetupData(ViewModel, accounts, invoiceData, supplierInvoice);
        }

    }
}
