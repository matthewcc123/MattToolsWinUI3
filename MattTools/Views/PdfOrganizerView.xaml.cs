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
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using ViewModels;
using MattTools.ViewModels;
using MattTools.Helper;
using Models.PdfEditor;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfOrganizerView : Page
    {

        public PdfEditorViewModel ViewModel { get; set; }

        public PdfOrganizerView()
        {
            this.InitializeComponent();
            ViewModel = new PdfEditorViewModel();
            DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void UpdateView()
        {
            SelectedFilesListContainer.Visibility = ViewModel.Documents.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            EmptyMessage.Visibility = ViewModel.Documents.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            PdfGridView.Visibility = ViewModel.Documents.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            OrganizeButton.IsEnabled = ViewModel.Documents.Count > 0 && ViewModel.Pages.Count > 0;
        }

        private bool isLoading
        {
            set
            {
                SelectButton.IsEnabled = !value;
                OrganizeButton.IsEnabled = !value;
                MainProgressBar.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                MainProgressBar.IsIndeterminate = value;
                PdfGridView.IsEnabled = !value;
                ResetButton.IsEnabled = !value;
                SelectedFileListView.IsEnabled = !value;

                if (value == false)
                    UpdateView();
            }
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            isLoading = true;

            await ViewModel.PickPDFMultiple(WindowHelper.GetWindowForElement(this), true);

            isLoading = false;


        }

        private void RotatePage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PPage page)
            {
                int currentRotation = page.PdfSharpPage.Rotate;
                page.PdfSharpPage.Rotate = (currentRotation + 90) % 360;

                page.ThumbnailImage = PdfHelper.GetPdfThumbnail(page);
            }
        }

        private void DeletePage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PPage page)
            {
                ViewModel.RemovePage(page);
                UpdateView();
            }
        }

        private void ReorderDocument_Completed(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
            ViewModel.RefreshPage();
            UpdateView();
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PDoc document)
            {
                ViewModel.RemoveDocument(document);
                UpdateView();
            }
        }

        private async void OrganizeButton_Click(object sender, RoutedEventArgs e)
        {
            isLoading = true;

            await ViewModel.OrganizePDF(WindowHelper.GetWindowForElement(this));
            UpdateView();

            isLoading = false;

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshPage();
            UpdateView();
        }
    }
}
