using MattTools.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MattTools.Models;
using System.Diagnostics;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.ComponentModel.Design.Serialization;
using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.WinUI.UI;
using Windows.ApplicationModel.Payments;
using System.Collections;
using Windows.Foundation.Metadata;
using Microsoft.UI.Composition.SystemBackdrops;
using Windows.UI.ViewManagement;
using MattTools.Settings;
using MattTools.Helper;
using System.Threading.Tasks;
using Views;
using WinUIEx;
using MathNet.Numerics;
using System.Drawing;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {

        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public MainWindow()
        {
            this.InitializeComponent();

            //Get Services
            App app = (App)Application.Current;
            _userService = app.ServiceProvider.GetRequiredService<IUserService>();
            _navigationService = app.ServiceProvider.GetRequiredService<INavigationService>();

            //Custom Title bar;
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            //Window Backdrop
            if (MicaController.IsSupported())
            {
                this.SystemBackdrop = new MicaBackdrop();
            }
            else if (DesktopAcrylicController.IsSupported())
            {
                this.SystemBackdrop = new DesktopAcrylicBackdrop();
            }


            // Set the initial theme resources
            FrameworkElement windowFrameWork = (this.Content as FrameworkElement);
            SetThemeResources(windowFrameWork.ActualTheme);

            // Subscribe to the theme change event
            windowFrameWork.ActualThemeChanged += MainWindow_ActualThemeChanged;

            //Set Theme
            SetThemeResources(_userService.GetUserSetting<ElementTheme>("App.Theme"));

            //Setup Navigation
            _navigationService.Init(AppFrame, AppNavView, typeof(PageNotFoundView));
            _navigationService.AddNavigation(new NavItem { Path = "Home", Title = "Home", PageType = typeof(HomeView), VisibleInMenu = false, Symbol = Symbol.Home });
            
            _navigationService.AddNavigation(new NavItem { Path = "Unilever", Title = "Unilever", PageType = null, UriSource = "Unilever.png", ExtendOnly = true });
            _navigationService.AddNavigation(new NavItem { Path = "Unilever/Merger", Title = "Invoice Merger", Description = "Merge Digital Invoice with the correct Tax Invoice.", PageType = typeof(InvoiceMergerView), UriSource = "Merger.png" });
            _navigationService.AddNavigation(new NavItem { Path = "Unilever/SOA", Title = "SOA Merger", Description = "Merge Unilever SOA into Single File.", PageType = typeof(SOAMergerView), UriSource = "SOA.png" });
            
            _navigationService.AddNavigation(new NavItem { Path = "PDF", Title = "PDF Editor", PageType = null, UriSource = "PDF.png", ExtendOnly = true });
            _navigationService.AddNavigation(new NavItem { Path = "PDF/Organizer", Title = "PDF Organizer", Description = "Merge & Organize PDF File", PageType = typeof(PdfOrganizerView), UriSource = "PDF.png" });
            _navigationService.AddNavigation(new NavItem { Path = "PDF/Compresser", Title = "PDF Compresser", Description = "Compress PDF File", PageType = typeof(PdfCompressorView), UriSource = "PDF.png" });
            
            _navigationService.AddNavigation(new NavItem { Path = "Rossum", Title = "Rossum Extractor", Description = "Extract selected Rossum files into Json and Pdf without rename.", PageType = typeof(RossumExtractorView), UriSource = "Rossum.png" });
            //_navigationService.AddNavigation(new NavItem { Path = "Zesthub", Title = "ZestHub+", Description = "Speed up the ZestHub process..", PageType = null, UriSource = "Zesthub.png" });


        }


        private async void AppNavView_Loaded(object sender, RoutedEventArgs e)
        {
            //Navigate to Home
            _navigationService.Navigate("Home");

            //Check Version
            bool updated = await _userService.UpdateExpiredDate();

            if (updated)
            {
                if (DateTime.Now >= _userService.GetUserSetting<DateTime>("App.ExpiredDate"))
                {

                    ContentDialog dialog = new ContentDialog();

                    // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                    if (this.Content is FrameworkElement rootElement)
                    {
                        dialog.XamlRoot = rootElement.XamlRoot;
                        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                        dialog.RequestedTheme = (this.Content as FrameworkElement).RequestedTheme;
                        dialog.Title = "Error";
                        dialog.CloseButtonText = "OK";
                        dialog.DefaultButton = ContentDialogButton.Close;
                        dialog.Content = "Service not available.";
                        dialog.CloseButtonClick += (sender, args) => { Application.Current.Exit(); };

                        DialogHelper.CreateDialog(dialog);
                    }

                }
            }
        }

        private void AppNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //Setting
            if (args.IsSettingsInvoked && _navigationService.CurrentPage != (typeof(SettingView)))
            {
                _navigationService.NavigateByType(typeof(SettingView));
                return;
            }

            //Navigate
            NavItem navItem = (sender.SelectedItem as NavItem);

            if (navItem != null && !navItem.ExtendOnly)
            {
                _navigationService.Navigate(navItem.Path);
            }
        }

        private void MainWindow_ActualThemeChanged(FrameworkElement sender, object args)
        {
            // Update the resources when the theme changes
            SetThemeResources(sender.ActualTheme);
        }

        public void SetThemeResources(ElementTheme theme)
        {
            // Get the merged dictionaries from the application resources
            var mergedDictionaries = ((App)Application.Current).Resources.MergedDictionaries;

            // Determine the URI of the theme resource file
            string themeSource = theme == ElementTheme.Dark ? "ms-appx:///Themes/Dark.xaml" : "ms-appx:///Themes/Light.xaml";

            if (theme == ElementTheme.Default)
            {
                themeSource = Application.Current.RequestedTheme == ApplicationTheme.Dark ? "ms-appx:///Themes/Dark.xaml" : "ms-appx:///Themes/Light.xaml";
            }

            // Check if the theme dictionary already exists and remove it if necessary
            var existingThemeDictionary = mergedDictionaries.FirstOrDefault(dict => dict.Source.ToString().Equals(themeSource));
            if (existingThemeDictionary != null)
            {
                mergedDictionaries.Remove(existingThemeDictionary);
            }

            // Create a new ResourceDictionary for the selected theme
            var newThemeDictionary = new ResourceDictionary { Source = new System.Uri(themeSource) };

            // Add the new theme dictionary to the merged dictionaries
            mergedDictionaries.Add(newThemeDictionary);

            //Change Requested Theme
            (this.Content as FrameworkElement).RequestedTheme = theme;

            AppWindow appWindow = this.AppWindow;

            if (AppWindowTitleBar.IsCustomizationSupported())
            {

                Debug.WriteLine(Application.Current.Resources.MergedDictionaries.Count);

                foreach (var md in Application.Current.Resources.MergedDictionaries)
                {
                    foreach (var item in md)
                    {
                        Debug.WriteLine(item.Key.ToString());
                    }
                }

            }

        }

    }
}
