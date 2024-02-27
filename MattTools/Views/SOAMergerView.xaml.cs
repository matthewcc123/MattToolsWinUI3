using MattTools.Helper;
using MattTools.Models;
using MattTools.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SOAMergerView : Page
    {

        public SOAMergerViewModel ViewModel { get; set; }

        public SOAMergerView()
        {
            this.InitializeComponent();
            ViewModel = new SOAMergerViewModel();
            DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void InProgress(bool Progress)
        {
            ProgressIndicator.Visibility = Progress ? Visibility.Visible : Visibility.Collapsed;
            ProgressIndicator.IsIndeterminate = Progress;
            SOAListView.IsEnabled = !Progress;
            SelectButton.IsEnabled = !Progress;
            ClearButton.IsEnabled = !Progress;
            MergeButton.IsEnabled = !Progress;

            UpdateCounter();

            //if (!Progress && ViewModel.Errors.Count > 0)
            //{
            //    ShowErrorDialog();
            //}
        }

        private void UpdateCounter()
        {
            SOACounter.Text = $"SOA Files : {ViewModel.SOAfiles.Count}";
            //MatchCounter.Text = $"Matches : {ViewModel.MergeFiles.Where( item => item.Match).ToList().Count}";

            EmptyMessage.Visibility = ViewModel.SOAfiles.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            InProgress(true);

            try
            {
                await ViewModel.SelectSOA(WindowHelper.GetWindowForElement(this));
            }
            catch (Exception ex)
            {
                DialogHelper.CreateDialog("Error", ex.Message, this);
            }


            InProgress(false);
        }


        private async void MergeButton_Click(object sender, RoutedEventArgs e)
        {
            InProgress(true);

            try
            {
                await ViewModel.MergeSOA();
                DialogHelper.CreateDialog("Success", ViewModel.FolderPath + "MergedSOA.xls", this);
            }
            catch (Exception ex)
            {
                DialogHelper.CreateDialog("Error", ex.Message, this);
            }

            InProgress(false);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.DataContext is SOAFile soaFile)
            {
                soaFile.Cabang = textBox.Text;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InProgress(true);
            ViewModel.SOAfiles.Clear();
            InProgress(false);
        }
    }
}
