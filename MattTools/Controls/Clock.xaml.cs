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
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Controls
{
    public sealed partial class Clock : UserControl
    {
        private string time;
        private string date;

        public Clock()
        {
            this.InitializeComponent();

            time = DateTime.Now.ToString("HH:mm");
            date = DateTime.Now.ToString("dddd, MMMM d");
            TimeText.Text = time;
            DateText.Text = date;

            //one second interval
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateTime;
            timer.Start();
        }

        private void UpdateTime(object sender, object e)
        {
            time = DateTime.Now.ToString("HH:mm");
            date = DateTime.Now.ToString("dddd, MMMM d");

            TimeText.Text = time;
            DateText.Text = date;
        }

    }
}
