﻿#pragma checksum "D:\Projects\Visual Studio\Project\Matt's Tools\WinUI3\MattTools\MattTools\Views\SOAMergerView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E6DC49721EFF1852C85B33264B58CEDA08467D1F8A24043AA036051FB847BC01"
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
    partial class SOAMergerView : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2309")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\SOAMergerView.xaml line 116
                {
                    this.SOAListView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ListView>(target);
                }
                break;
            case 3: // Views\SOAMergerView.xaml line 173
                {
                    this.EmptyMessage = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 4: // Views\SOAMergerView.xaml line 189
                {
                    this.ProgressIndicator = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ProgressBar>(target);
                }
                break;
            case 11: // Views\SOAMergerView.xaml line 147
                {
                    global::Microsoft.UI.Xaml.Controls.TextBox element11 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)element11).TextChanged += this.TextBox_TextChanged;
                }
                break;
            case 12: // Views\SOAMergerView.xaml line 46
                {
                    this.SelectButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.SelectButton).Click += this.SelectButton_Click;
                }
                break;
            case 13: // Views\SOAMergerView.xaml line 62
                {
                    this.ClearButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.ClearButton).Click += this.ClearButton_Click;
                }
                break;
            case 14: // Views\SOAMergerView.xaml line 78
                {
                    this.MergeButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.MergeButton).Click += this.MergeButton_Click;
                }
                break;
            case 15: // Views\SOAMergerView.xaml line 104
                {
                    this.SOACounter = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2309")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

