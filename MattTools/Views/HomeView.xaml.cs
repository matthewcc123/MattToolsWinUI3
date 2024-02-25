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
using MattTools.Models;
using MattTools.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ABI.System;
using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        private readonly INavigationService _navigationService;
        public ObservableCollection<NavItem> mainMenus = new ObservableCollection<NavItem>();
        public NavItem noParentItems = new NavItem { Title = "Tools" };

        public HomeView()
        {
            this.InitializeComponent();

            //Get Services
            App app = (App)Application.Current;
            _navigationService = app.ServiceProvider.GetRequiredService<INavigationService>();

            foreach (var navItem in _navigationService.NavItems)
            {
                if (!navItem.VisibleInMenu)
                    continue;

                if (navItem.HaveChild)
                    mainMenus.Add(navItem);
                else
                    noParentItems.ChildItems.Add(navItem);
            }

            mainMenus.Add(noParentItems);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _navigationService.Navigate((button.DataContext as NavItem).Path);

        }

    }
}
