using MattTools.Dialogs;
using MattTools.Helper;
using MattTools.Models;
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
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using static System.Reflection.Metadata.BlobBuilder;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MattTools.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InvoiceMergerView : Page
    {

        public InvoiceMergerViewModel ViewModel { get; set; }

        public InvoiceMergerView()
        {

            this.InitializeComponent();
            ViewModel = new InvoiceMergerViewModel();
            DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Required;

        }
        

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Window window = WindowHelper.GetWindowForElement(this);

            if (e.ClickedItem is MergeFile mergeFile)
            {
                MergeFileDialog fileDialog = new MergeFileDialog();
                fileDialog.XamlRoot = this.XamlRoot;
                fileDialog.RequestedTheme = (window.Content as FrameworkElement).RequestedTheme;
                fileDialog.InvoiceFileName = mergeFile.InvoiceFileName;
                fileDialog.TaxFileName = mergeFile.TaxFileName;

                DialogHelper.CreateDialog(fileDialog);
                //DialogInvoiceFileBox.Text = mergeFile.InvoiceFileName;
                //DialogTaxFileBox.Text = mergeFile.TaxFileName;
                //await FileDetailDialog.ShowAsync();

            }
        }

        private void InProgress(bool Progress)
        {
            ProgressIndicator.Visibility = Progress ? Visibility.Visible : Visibility.Collapsed;
            ProgressIndicator.IsIndeterminate = Progress;
            MergerListView.IsEnabled = !Progress;
            SelectButton.IsEnabled = !Progress;
            ClearButton.IsEnabled = !Progress;
            MergeButton.IsEnabled = !Progress;

            UpdateCounter();

            if (!Progress && ViewModel.Errors.Count > 0)
            {
                ShowErrorDialog();
            }
        }

        private void UpdateCounter()
        {
            InvoiceCounter.Text = $"Invoices : {ViewModel.InvoicePaths.Count}";
            TaxCounter.Text = $"Tax Invoices : {ViewModel.TaxPaths.Count}";
            //MatchCounter.Text = $"Matches : {ViewModel.MergeFiles.Where( item => item.Match).ToList().Count}";

            EmptyMessage.Visibility = ViewModel.MergeFiles.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void Merge_Click(object sender, RoutedEventArgs e)
        {
            InProgress(true);
            await ViewModel.MergeInvoices();
            InProgress(false);
        }

        private async void Select_Click(object sender, RoutedEventArgs e)
        {
            InProgress(true);
            await ViewModel.PickInvoices(WindowHelper.GetWindowForElement(this));
            InProgress(false);
        }

        private void ShowErrorDialog()
        {
            string text = "";

            foreach (var error in ViewModel.Errors)
            {
                text += $"{error}" + System.Environment.NewLine;
            }

            DialogHelper.CreateDialog("Error", text, this);
        }

        private async void Clear_Click(object sender, RoutedEventArgs e)
        {
            InProgress(true);
            if (ViewModel.MergeFiles.Count > 0)
            {
                await Task.Delay(300);
            }
            ViewModel.ClearAll();
            InProgress(false);
        }
    }


}
