﻿#pragma checksum "D:\Projects\Visual Studio\Project\Matt's Tools\WinUI3\MattTools\MattTools\Views\RossumExtractorView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EFC7F3F93194E058FDF91287F164DA54C1E4FC241330656FE6F0E3AC8BBF649E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MattTools.Views
{
    partial class RossumExtractorView : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Views\RossumExtractorView.xaml line 2
                {
                    global::Microsoft.UI.Xaml.Controls.Page element1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Page>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Views\RossumExtractorView.xaml line 44
                {
                    this.LoginView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 3: // Views\RossumExtractorView.xaml line 90
                {
                    this.MainView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 4: // Views\RossumExtractorView.xaml line 314
                {
                    this.RossumListView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ListView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ListView)this.RossumListView).SelectionChanged += this.RossumItemListView_SelectionChanged;
                }
                break;
            case 5: // Views\RossumExtractorView.xaml line 388
                {
                    this.EmptyMessage = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 13: // Views\RossumExtractorView.xaml line 268
                {
                    this.SelectColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 14: // Views\RossumExtractorView.xaml line 269
                {
                    this.NumberColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 15: // Views\RossumExtractorView.xaml line 270
                {
                    this.NameColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 16: // Views\RossumExtractorView.xaml line 271
                {
                    this.StatusColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 17: // Views\RossumExtractorView.xaml line 272
                {
                    this.DateColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 18: // Views\RossumExtractorView.xaml line 274
                {
                    this.SelectAllListView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.SelectAllListView).Click += this.SelectAllListView_Click;
                }
                break;
            case 19: // Views\RossumExtractorView.xaml line 280
                {
                    this.Index = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 20: // Views\RossumExtractorView.xaml line 249
                {
                    this.MainProgressBar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ProgressBar>(target);
                }
                break;
            case 21: // Views\RossumExtractorView.xaml line 155
                {
                    this.WorkspaceComboBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ComboBox)this.WorkspaceComboBox).SelectionChanged += this.WorkspaceComboBoxSelected;
                }
                break;
            case 22: // Views\RossumExtractorView.xaml line 167
                {
                    this.QueueComboBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                }
                break;
            case 23: // Views\RossumExtractorView.xaml line 178
                {
                    this.FindTextBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.FindTextBox).SelectionChanged += this.FindTextBox_SelectionChanged;
                }
                break;
            case 24: // Views\RossumExtractorView.xaml line 203
                {
                    this.FindButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.FindButton).Click += this.FindButton_Click;
                }
                break;
            case 25: // Views\RossumExtractorView.xaml line 220
                {
                    this.ExtractButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 26: // Views\RossumExtractorView.xaml line 236
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element26 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element26).Click += this.ExtractJson_Click;
                }
                break;
            case 27: // Views\RossumExtractorView.xaml line 240
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element27 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element27).Click += this.ExtractPdf_Click;
                }
                break;
            case 28: // Views\RossumExtractorView.xaml line 192
                {
                    this.FromCalendar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CalendarDatePicker>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CalendarDatePicker)this.FromCalendar).DateChanged += this.UpdateDate;
                }
                break;
            case 29: // Views\RossumExtractorView.xaml line 197
                {
                    this.ToCalendar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CalendarDatePicker>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CalendarDatePicker)this.ToCalendar).DateChanged += this.UpdateDate;
                }
                break;
            case 34: // Views\RossumExtractorView.xaml line 108
                {
                    this.LogoutButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 35: // Views\RossumExtractorView.xaml line 120
                {
                    this.LoggedAccountText = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 36: // Views\RossumExtractorView.xaml line 127
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element36 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element36).Click += this.Logout_Click;
                }
                break;
            case 37: // Views\RossumExtractorView.xaml line 83
                {
                    this.LoginProgressBar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ProgressBar>(target);
                }
                break;
            case 38: // Views\RossumExtractorView.xaml line 66
                {
                    this.LoginEmailBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 39: // Views\RossumExtractorView.xaml line 70
                {
                    this.LoginPasswordBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                }
                break;
            case 40: // Views\RossumExtractorView.xaml line 74
                {
                    this.LoginButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.LoginButton).Click += this.Login_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

