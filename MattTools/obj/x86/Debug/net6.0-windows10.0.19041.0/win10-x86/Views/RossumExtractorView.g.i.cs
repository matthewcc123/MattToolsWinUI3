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
    partial class RossumExtractorView : global::Microsoft.UI.Xaml.Controls.Page
    {


#pragma warning disable 0169    //  Proactively suppress unused/uninitialized field warning in case they aren't used, for things like x:Name
#pragma warning disable 0649
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.Grid LoginView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.Grid MainView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ListView RossumListView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.StackPanel EmptyMessage; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ColumnDefinition SelectColumn; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ColumnDefinition NumberColumn; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ColumnDefinition NameColumn; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ColumnDefinition StatusColumn; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ColumnDefinition DateColumn; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.CheckBox SelectAllListView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.TextBlock Index; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ProgressBar MainProgressBar; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ComboBox WorkspaceComboBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ComboBox QueueComboBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.TextBox FindTextBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.Button FindButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.DropDownButton ExtractButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.DropDownButton LogoutButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.TextBlock LoggedAccountText; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.ProgressBar LoginProgressBar; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.TextBox LoginEmailBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.PasswordBox LoginPasswordBox; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private global::Microsoft.UI.Xaml.Controls.Button LoginButton; 
#pragma warning restore 0649
#pragma warning restore 0169
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Views/RossumExtractorView.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
        }

        partial void UnloadObject(global::Microsoft.UI.Xaml.DependencyObject unloadableObject);

    }
}


