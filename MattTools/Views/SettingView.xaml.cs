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
using Windows.UI.ViewManagement;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MattTools.Settings;
using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;
using MattTools.Helper;
using MattTools.Dialogs;
using MattTools.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingView : Page
    {

        private IUserService _userService;

        public SettingView()
        {
            this.InitializeComponent();

            //Get Services
            App app = (App)Application.Current;
            _userService = app.ServiceProvider.GetService<IUserService>();
        }

        private void ThemeSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sender is ComboBox comboBox && comboBox.SelectedItem != null && comboBox.IsLoaded)
            {
                ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
                ElementTheme selectedTheme = (ElementTheme)Convert.ToInt32(comboBoxItem.Tag);

                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    if (window.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = selectedTheme;

                        if (window is MainWindow main)
                        {
                            main.SetThemeResources(selectedTheme);
                        }

                    }
                }

                //Update UserSetting
                _userService.SetUserSetting("App.Theme", selectedTheme);
            }
        }

        private void ThemeSetting_Loaded(object sender, RoutedEventArgs e)
        {
            //Theme
            Window window = (Application.Current as App).m_window;
            int themeValue = (int)(window.Content as FrameworkElement).RequestedTheme;

            ComboBox comboBox = sender as ComboBox;

            ComboBoxItem comboBoxItem = comboBox.Items.FirstOrDefault(item => Convert.ToInt32((item as ComboBoxItem).Tag) == themeValue) as ComboBoxItem;
            comboBox.SelectedItem = comboBoxItem;

            //Unilever
            uliTaxNumberBtn.Content = _userService.GetUserSetting<string>("Unilever.TaxFormat");

            //About
            ExpiredSettingCardText.Text = "Expired Date : " + _userService.GetUserSetting<DateTime>("App.ExpiredDate").ToLocalTime().ToString();
        }

        private void Button_UnileverTaxFormatSetting(object sender, RoutedEventArgs e)
        {
            Window window = WindowHelper.GetWindowForElement(this);

            EditTaxNumberDialog editDialog = new EditTaxNumberDialog();
            editDialog.XamlRoot = this.XamlRoot;
            editDialog.RequestedTheme = (window.Content as FrameworkElement).RequestedTheme;
            editDialog.TaxFormat = _userService.GetUserSetting<string>("Unilever.TaxFormat");
            editDialog.PrimaryButtonText = "Save";
            editDialog.CloseButtonText = "Cancel";
            editDialog.DefaultButton = ContentDialogButton.Primary;

            DialogHelper.CreateDialog(editDialog);

            editDialog.Closed += UpdateUnileverTaxFormatSetting;

        }

        private void UpdateUnileverTaxFormatSetting(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            EditTaxNumberDialog editDialog = sender as EditTaxNumberDialog;

            if (args.Result == ContentDialogResult.Primary)
            {
                //Update Setting
                _userService.SetUserSetting("Unilever.TaxFormat", editDialog.TaxFormat);

                //Update UI
                uliTaxNumberBtn.Content = editDialog.TaxFormat;
            }
        }
    }
}
