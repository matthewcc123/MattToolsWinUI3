using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Helper
{
    public class DialogHelper
    {

        static public bool ContentDialogOpened { get { return _contentDialogOpened; } }
        static private bool _contentDialogOpened;
        static public List<ContentDialog> ContentDialogs { get { return _contentDialogs; } }

        static private List<ContentDialog> _contentDialogs = new List<ContentDialog>();

        static public void CreateDialog(ContentDialog contentDialog)
        {

            contentDialog.Closed += ContentDialog_Closed;

            //Add To List
            _contentDialogs.Add(contentDialog);

            UpdateDialog();

        }


        static public void CreateDialog(string title, string text, UserControl control)
        {
            ContentDialog dialog = new ContentDialog();
            Window window = WindowHelper.GetWindowForElement(control);

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = control.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.RequestedTheme = (window.Content as FrameworkElement).RequestedTheme;
            dialog.Title = title;
            dialog.CloseButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Content = text;

            CreateDialog(dialog);
        }

        private static async void UpdateDialog()
        {
            if (_contentDialogs.Count > 0 && !_contentDialogOpened)
            {
                _contentDialogOpened = true;
                await _contentDialogs[0].ShowAsync();
            }
        }

        private static void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            _contentDialogOpened = false;
            _contentDialogs.Remove(sender);
            UpdateDialog();
        }

        
        static public ContentDialog GetCurrentDialog()
        {
            
            if (ContentDialogs.Count > 0 && ContentDialogOpened)
            {
                return ContentDialogs[0];
            }

            return null;
        }
    }
}
