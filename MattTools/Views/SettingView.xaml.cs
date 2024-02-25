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

            ExpiredSettingCardText.Text = "Expired Date : " + _userService.GetUserSetting<DateTime>("App.ExpiredDate").ToLocalTime().ToString();
        }

        private void ThemeSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Window window = (Application.Current as App).m_window;

            if (sender is ComboBox comboBox && comboBox.SelectedItem != null && comboBox.IsLoaded)
            {
                ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
                ElementTheme selectedTheme = (ElementTheme)Convert.ToInt32(comboBoxItem.Tag);
                (window.Content as FrameworkElement).RequestedTheme = selectedTheme;

                //Update UserSetting
                _userService.SetUserSetting("App.Theme", selectedTheme);
            }
        }

        private void ThemeSetting_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = (Application.Current as App).m_window;
            int themeValue = (int)(window.Content as FrameworkElement).RequestedTheme;

            ComboBox comboBox = sender as ComboBox;

            ComboBoxItem comboBoxItem = comboBox.Items.FirstOrDefault(item => Convert.ToInt32((item as ComboBoxItem).Tag) == themeValue) as ComboBoxItem;
            comboBox.SelectedItem = comboBoxItem;
        }

    }
}
