﻿#pragma checksum "D:\Projects\Visual Studio\Project\Matt's Tools\WinUI3\MattTools\MattTools\Views\RossumExtractorView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9C18FBA5A1F26E6723F4D6E707B86B965ED14F689B1052AB86A5BB4E5F5ED4FF"
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
            case 4: // Views\RossumExtractorView.xaml line 298
                {
                    this.RossumListView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ListView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ListView)this.RossumListView).SelectionChanged += this.RossumItemListView_SelectionChanged;
                }
                break;
            case 5: // Views\RossumExtractorView.xaml line 372
                {
                    this.EmptyMessage = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 13: // Views\RossumExtractorView.xaml line 252
                {
                    this.SelectColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 14: // Views\RossumExtractorView.xaml line 253
                {
                    this.NumberColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 15: // Views\RossumExtractorView.xaml line 254
                {
                    this.NameColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 16: // Views\RossumExtractorView.xaml line 255
                {
                    this.StatusColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 17: // Views\RossumExtractorView.xaml line 256
                {
                    this.DateColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 18: // Views\RossumExtractorView.xaml line 258
                {
                    this.SelectAllListView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.SelectAllListView).Click += this.SelectAllListView_Click;
                }
                break;
            case 19: // Views\RossumExtractorView.xaml line 264
                {
                    this.Index = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 20: // Views\RossumExtractorView.xaml line 233
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
                }
                break;
            case 24: // Views\RossumExtractorView.xaml line 187
                {
                    this.FindButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.FindButton).Click += this.FindButton_Click;
                }
                break;
            case 25: // Views\RossumExtractorView.xaml line 204
                {
                    this.ExtractButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 26: // Views\RossumExtractorView.xaml line 220
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element26 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element26).Click += this.ExtractJson_Click;
                }
                break;
            case 27: // Views\RossumExtractorView.xaml line 224
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element27 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element27).Click += this.ExtractPdf_Click;
                }
                break;
            case 32: // Views\RossumExtractorView.xaml line 108
                {
                    this.LogoutButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 33: // Views\RossumExtractorView.xaml line 120
                {
                    this.LoggedAccountText = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 34: // Views\RossumExtractorView.xaml line 127
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element34 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element34).Click += this.Logout_Click;
                }
                break;
            case 35: // Views\RossumExtractorView.xaml line 83
                {
                    this.LoginProgressBar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ProgressBar>(target);
                }
                break;
            case 36: // Views\RossumExtractorView.xaml line 66
                {
                    this.LoginEmailBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 37: // Views\RossumExtractorView.xaml line 70
                {
                    this.LoginPasswordBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                }
                break;
            case 38: // Views\RossumExtractorView.xaml line 74
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

