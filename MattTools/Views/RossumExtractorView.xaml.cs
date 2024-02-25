using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MattTools.ViewModels;
using System.Diagnostics;
using MattTools.Models.Rossum;
using MattTools.Helper;
using Windows.System;
using Windows.UI.Core;
using Windows.Services.Maps;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RossumExtractorView : Page
    {

        public RossumExtractorViewModel ViewModel { get; set; }

        public RossumExtractorView()
        {
            this.InitializeComponent();
            ViewModel = new RossumExtractorViewModel();
            this.DataContext = ViewModel;

            NavigationCacheMode = NavigationCacheMode.Required;
        }

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
                WorkspaceComboBox.IsEnabled = value;
                QueueComboBox.IsEnabled = value;
                FindTextBox.IsEnabled = value;
                FindButton.IsEnabled = value;
                ExtractButton.IsEnabled = value;
                LogoutButton.IsEnabled = value;
                SelectAllListView.IsEnabled = value;
            }
        }

        private void UpdateView()
        {
            LoginView.Visibility = ViewModel.IsLoggedIn ? Visibility.Collapsed : Visibility.Visible;
            MainView.Visibility = ViewModel.IsLoggedIn ? Visibility.Visible : Visibility.Collapsed;

            WorkspaceComboBox.SelectedIndex = 0;
            QueueComboBox.SelectedIndex = 0;
            LoggedAccountText.Text = ViewModel.SavedUsername;
        }

        private async Task Login(bool key)
        {
            LoginViewIsEnabled = false;
            MainViewIsEnabled = false;
            LoginProgressBar.IsIndeterminate = true;
            LoginProgressBar.Visibility = Visibility.Visible;

            AuthenticationResponse response = !key ? await ViewModel.Login(LoginEmailBox.Text, LoginPasswordBox.Password) : await ViewModel.LoginWithKey();

            if (response != null)
            {

                if (response.Key == null)
                {
                    DialogHelper.CreateDialog("Login Failed", response.Error, this);
                }
                else
                {
                    EmptyMessage.Visibility = Visibility.Visible;

                    //Get Workspace
                    await ViewModel.GetWorkspaces();
                    await ViewModel.GetQueues(0);

                    //Success
                    DialogHelper.CreateDialog("Login Success", $"Welcome {LoginEmailBox.Text}! {System.Environment.NewLine}" +
                                $"Key : {response.Key}", this);
                }

            }

            LoginProgressBar.IsIndeterminate = false;
            LoginProgressBar.Visibility = Visibility.Collapsed;

            UpdateView();
            LoginViewIsEnabled = true;
            MainViewIsEnabled = true;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (ViewModel.IsHaveUserKey && !ViewModel.IsLoggedIn)
            {
                LoginEmailBox.Text = ViewModel.SavedUsername;
                await Login(true);
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (LoginEmailBox.Text == string.Empty || LoginPasswordBox.Password == string.Empty)
            {
                DialogHelper.CreateDialog("Login Failed", "Please fill the form.", this);
                return;
            }

            await Login(false);

        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationResponse response = await ViewModel.Logout();

            if (response != null)
            {

                if (!response.LoggedOut)
                {
                    DialogHelper.CreateDialog("Logout Failed", response.Error, this);
                }

            }

            UpdateView();
        }

        private async void FindButton_Click(object sender, RoutedEventArgs e)
        {
            SelectAllListView.IsChecked = false;
            EmptyMessage.Visibility = Visibility.Collapsed;
            RossumListView.Visibility = Visibility.Visible;

            MainViewIsEnabled = false;
            MainProgressBar.IsIndeterminate = true;
            MainProgressBar.Visibility = Visibility.Visible;

            //Listing Documents to Find
            string filters = FindTextBox.Text;
            filters = filters.Replace("\n", ",");
            filters = filters.Replace("\r", ",");
            filters = filters.Replace("\t", ",");
            string[] filterToFind = filters.Split(',');
            filterToFind = filterToFind.Distinct().ToArray();

            if (filterToFind.Length == 0 || (filterToFind.Length > 0 && filterToFind[0] == ""))
            {
                DialogHelper.CreateDialog("Error", "Fill the search form.", this);
            }
            else
            {
                await ViewModel.Find(filterToFind, ViewModel.Queues[QueueComboBox.SelectedIndex].Id.ToString());
                await Task.Delay(1000);
                EnableRossumItemListView(true);
            }

            EmptyMessage.Visibility = (ViewModel.RossumItems.Count == 0) ? Visibility.Visible : Visibility.Collapsed;

            MainProgressBar.IsIndeterminate = false;
            MainProgressBar.Visibility = Visibility.Collapsed;
            MainViewIsEnabled = true;

        }

        private void SelectAllListView_Click(object sender, RoutedEventArgs e)
        {
            if (SelectAllListView.IsChecked == false)
            {
                RossumListView.SelectedItems.Clear();
            }
            else
            {
                foreach (var item in RossumListView.Items)
                {
                    if (item is RossumItem rossumItem && rossumItem.Status != "Not Found")
                    {
                        RossumListView.SelectedItems.Add(item);
                    }
                }
            }
        }

        private void RossumItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectAllListView.IsEnabled)
                SelectAllListView.IsChecked = RossumListView.SelectedItems.Count == RossumListView.Items.Where(item => item is RossumItem rossumItem && rossumItem.Status != "Not Found").Count();
        }

        private void EnableRossumItemListView(bool enable)
        {

            for (int i = 0; i < RossumListView.Items.Count; i++)
            {
                if (RossumListView.ContainerFromIndex(i) is SelectorItem itemContainer)
                {
                    itemContainer.IsEnabled = enable ? (RossumListView.ItemFromContainer(itemContainer) as RossumItem).Status != "Not Found" : false;
                }
            }
        }

        private async void WorkspaceComboBoxSelected(object sender, SelectionChangedEventArgs e)
        {
            if (!LoginButton.IsEnabled)
                return;

            MainViewIsEnabled = false;
            MainProgressBar.IsIndeterminate = true;
            MainProgressBar.Visibility = Visibility.Visible;

            await ViewModel.GetQueues((sender as ComboBox).SelectedIndex);

            MainProgressBar.IsIndeterminate = false;
            MainProgressBar.Visibility = Visibility.Collapsed;
            QueueComboBox.SelectedIndex = 0;
            MainViewIsEnabled = true;
        }

        public string ToUpperEveryWord(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var words = s.Split(' ');

            var t = "";
            foreach (var word in words)
            {
                t += char.ToUpper(word[0]) + word.Substring(1) + ' ';
            }
            return t.Trim();
        }

        private async void ExtractJson_Click(object sender, RoutedEventArgs e)
        {

            if (RossumListView.SelectedItems.Count == 0)
                return;


            MainViewIsEnabled = false;
            MainProgressBar.IsIndeterminate = true;
            MainProgressBar.Visibility = Visibility.Visible;
            EnableRossumItemListView(false);

            string result = await ViewModel.ExtractJSON(WindowHelper.GetWindowForElement(this), RossumListView.SelectedItems.Cast<RossumItem>().ToList(), ViewModel.Queues[QueueComboBox.SelectedIndex].Id.ToString());

            MainProgressBar.IsIndeterminate = false;
            MainProgressBar.Visibility = Visibility.Collapsed;
            MainViewIsEnabled = true;
            EnableRossumItemListView(true);

            if (result != null)
                DialogHelper.CreateDialog("Result", result, this);

        }

        private async void ExtractPdf_Click(object sender, RoutedEventArgs e)
        {
            if (RossumListView.SelectedItems.Count == 0)
                return;


            MainViewIsEnabled = false;
            MainProgressBar.IsIndeterminate = true;
            MainProgressBar.Visibility = Visibility.Visible;
            EnableRossumItemListView(false);

            string result = await ViewModel.ExtractPDF(WindowHelper.GetWindowForElement(this), RossumListView.SelectedItems.Cast<RossumItem>().ToList());

            MainProgressBar.IsIndeterminate = false;
            MainProgressBar.Visibility = Visibility.Collapsed;
            MainViewIsEnabled = true;
            EnableRossumItemListView(true);

            if (result != null)
                DialogHelper.CreateDialog("Result", result, this);
        }
    }
}
