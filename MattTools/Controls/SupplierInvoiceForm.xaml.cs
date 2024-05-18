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
using Models.Zesthub;
using MattTools.Models.Zesthub;
using System.Collections.ObjectModel;
using MathNet.Numerics;
using MattTools.ViewModels;
using Microsoft.UI;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Controls
{
    public sealed partial class SupplierInvoiceForm : UserControl
    {

        public ZesthubViewModel ViewModel { get; set; }

        public List<Account> Accounts { get; set; }
        public InvoiceData Invoice { get; set; }
        public SupplierInvoiceData SupplierInvoice { get; set; }

        private bool _lineItemDifference;
        private bool _dueDateMatch;
        private bool _accountMatch;

        public SupplierInvoiceForm()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public void SetupData(ZesthubViewModel viewModel, List<Account> accounts, InvoiceData invoice, SupplierInvoiceData supplierInvoice)
        {

            this.Visibility = Visibility.Collapsed;

            ViewModel = viewModel;
            Accounts = accounts;
            Invoice = invoice;
            SupplierInvoice = supplierInvoice;

            //Indicator
            IndicatorInvoice.Text = $"{Invoice.InvCode} ({Invoice.Id})";
            IndicatorCreateDate.Text = $"Created At : {Invoice.CreatedDate.ToShortDateString()}";
            IndicatorCurrency.Text = SupplierInvoice.CurrencyCode;

            //Main
            MainSupplier.Text = SupplierInvoice.SupplierName;
            MainWarehouse.Text = Invoice.WarehouseName;
            MainGR.Text = SupplierInvoice.GrCodes;
            MainPO.Text = $"{SupplierInvoice.PoCode} ({SupplierInvoice.PoCodeExternal})";

            MainInvoiceDate.Date = SupplierInvoice.InvDate;
            MainDueDate.Date = Invoice.DueDate;

            MainTaxNumber.Text = SupplierInvoice.TrNumber;
            MainTaxDate.Date = SupplierInvoice.TrDate;

            MainBank.ItemsSource = Accounts;
            Account selectedAccount = Accounts.FirstOrDefault(bank => bank.AccountNumber == SupplierInvoice.AccountNumber);
            MainBank.SelectedIndex = Accounts.IndexOf(selectedAccount);
            MainBank.Text = Accounts[MainBank.SelectedIndex].AccountComboBoxText;

            //LineItem
            LineItemTotal.Text = $"Line Item : {SupplierInvoice.DataLine.Count}";
            LineItemTotalPO.Text = $"Total Item PO : {SupplierInvoice.DataLine.Count}";
            LineItemTotalReceived.Text = $"Total Item Received : {SupplierInvoice.DataLine.Count}";
            LineItemTotalInvoice.Text = $"Total Item Invoice : {SupplierInvoice.DataLine.Count}";
            LineItemListView.ItemsSource = SupplierInvoice.DataLine;

            _lineItemDifference = false;
            foreach (var item in SupplierInvoice.DataLine)
            {
                if (item.NotMatch)
                {
                    _lineItemDifference = true;
                    continue;
                }
            }

            LineItemAlert.Title = !_lineItemDifference
            ? "Line Item OK."
            : "Check line item for a price difference!";

            LineItemAlert.Background = !_lineItemDifference
            ? (SolidColorBrush)Application.Current.Resources["AlertBlueBackgroundColor"]
            : (SolidColorBrush)Application.Current.Resources["AlertRedBackgroundColor"];

            LineItemAlert.BorderBrush = !_lineItemDifference
            ? (SolidColorBrush)Application.Current.Resources["AlertBlueBorderColor"]
            : (SolidColorBrush)Application.Current.Resources["AlertRedBorderColor"];

            LineItemItemValue.Text = string.Format("{0:#,##0.00}", SupplierInvoice.ItemValue);
            LineItemChargesValue.Text = string.Format("{0:#,##0.00}", SupplierInvoice.ChargeValue);
            LineItemTaxValue.Text = string.Format("{0:#,##0.00}", SupplierInvoice.TaxValue);
            LineItemDiscountValue.Text = string.Format("{0:#,##0.00}", SupplierInvoice.TotalDiscount);
            LineItemTotalInvoiceValue.Text = string.Format("{0:#,##0.00}", SupplierInvoice.TotalInvoiceValue);

            //Notes
            NotesBlock.Text = SupplierInvoice.SupplierInvoiceNotes.Replace("\n", string.Empty).Replace(";", System.Environment.NewLine);

            //Attachment
            AttachmentListView.ItemsSource = SupplierInvoice.DataAttachment;
            AttachmentAlert.Visibility = (SupplierInvoice.DataAttachment.FirstOrDefault(attachment => attachment.Name == $"{Invoice.InvCode}.pdf") == null) ? Visibility.Visible : Visibility.Collapsed;

            UpdateSubmitButton();

            //OK
            this.Visibility = Visibility.Visible;

        }

        private void UpdateSubmitButton()
        {

            //Submit Button
            SubmitButton.IsEnabled = !_lineItemDifference && AttachmentAlert.Visibility == Visibility.Collapsed && _dueDateMatch && _accountMatch;

        }

        private void MainDueDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

            Check();

        }

        private void MainBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Check();

        }

        public void Check()
        {
            //Due Date Check
            _dueDateMatch = MainDueDate.Date != null && MainDueDate.Date.Value.Date == ViewModel.SetDueDate.Value.Date;

            MainDueDate.Background = _dueDateMatch
            ? (SolidColorBrush)Application.Current.Resources["AlertBlueBackgroundColor"]
            : (SolidColorBrush)Application.Current.Resources["AlertRedBackgroundColor"];

            MainDueDate.BorderBrush = _dueDateMatch
            ? (SolidColorBrush)Application.Current.Resources["AlertBlueBorderColor"]
            : (SolidColorBrush)Application.Current.Resources["AlertRedBorderColor"];


            //Bank Account Check
            _accountMatch = false;
            var SelectedItem = MainBank.SelectedItem as Account;

            if (SelectedItem == null)
                return;

            _accountMatch = SelectedItem.AccountNumber == ViewModel.SetAccountNumber;

            MainBank.Background = _accountMatch
            ? (SolidColorBrush)Application.Current.Resources["AlertBlueBackgroundColor"]
            : (SolidColorBrush)Application.Current.Resources["AlertRedBackgroundColor"];

            MainBank.BorderBrush = _accountMatch
            ? (SolidColorBrush)Application.Current.Resources["AlertBlueBorderColor"]
            : (SolidColorBrush)Application.Current.Resources["AlertRedBorderColor"];

            //Done

            UpdateSubmitButton();
        }
    }
}
