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
using MattTools.Helper;
using Models.PdfEditor;
using ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfCompressorView : Page
    {
        public PdfEditorViewModel ViewModel { get; set; }

        public PdfCompressorView()
        {
            this.InitializeComponent();
            ViewModel = new PdfEditorViewModel();
            DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void UpdateView()
        {
            EmptyMessage.Visibility = ViewModel.Documents.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            SelectedFileListView.Visibility = ViewModel.Documents.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            CompressAllButton.IsEnabled = ViewModel.Documents.Count > 0 && ViewModel.Pages.Count > 0;
        }

        private bool isLoading
        {
            set
            {
                SelectButton.IsEnabled = !value;
                CompressAllButton.IsEnabled = !value;
                AllQualityComboBox.IsEnabled = !value;
                MainProgressBar.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                MainProgressBar.IsIndeterminate = value;
                SelectedFileListView.IsEnabled = !value;

                if (value == false)
                    UpdateView();
            }
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            isLoading = true;

            await ViewModel.PickPDFMultiple(WindowHelper.GetWindowForElement(this), false);

            isLoading = false;


        }

        private void QualityComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.DataContext is PDoc document)
            {
                document.CompressQuality = comboBox.SelectedIndex;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PDoc document)
            {
                ViewModel.RemoveDocument(document);
                UpdateView();
            }
        }

        private async void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PDoc document)
            {
                //isLoading = true;

                var listViewItemContainer = SelectedFileListView.ContainerFromItem(document) as SelectorItem;

                listViewItemContainer.IsEnabled = false;
                await ViewModel.CompressSinglePDF(document, WindowHelper.GetWindowForElement(this));
                listViewItemContainer.IsEnabled = true;

            }
        }

        private async void CompressAllButton_Click(object sender, RoutedEventArgs e)
        {
            isLoading = true;

            await ViewModel.CompressAllPdf(WindowHelper.GetWindowForElement(this), AllQualityComboBox.SelectedIndex);

            isLoading = false;
        }
    }
}
