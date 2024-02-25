using MattTools.Helper;
using MattTools.Services;
using MattTools.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        internal Window m_window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            this.InitializeComponent();

            //Init ServiceProvider
            var services = new ServiceCollection();

            //Register Services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IRossumService, RossumService>();

            //Build Service Provider
            ServiceProvider = services.BuildServiceProvider();

        }


        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            //LoadUserSetting
            await ServiceProvider.GetRequiredService<IUserService>().LoadUserSetting();

            m_window = new MainWindow();
            m_window.Activate();

            WindowHelper.TrackWindow(m_window);
        }
    }
}
