using MattTools.Helper;
using MattTools.Models;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using PdfSharp.Pdf.Content.Objects;
using Microsoft.UI.Xaml;
using System.Data;
using PdfSharp.Pdf;
using MattTools.Settings;
using Newtonsoft.Json;

namespace MattTools.ViewModels
{
    public class InvoiceMergerViewModel
    {

        public ObservableCollection<MergeFile> MergeFiles { get; set; }

        public string MergePath { get; set; }
        public List<string> Errors;
        public List<string> InvoicePaths;
        public List<string> TaxPaths;
        public List<string> SortedTaxPaths;

        public InvoiceMergerViewModel()
        {
            MergeFiles = new ObservableCollection<MergeFile>();
            Errors = new List<string>();
            InvoicePaths = new List<string>();
            TaxPaths = new List<string>();
            SortedTaxPaths = new List<string>();
        }

        public void ClearAll()
        {
            MergePath = null;
            Errors.Clear();
            InvoicePaths.Clear();
            TaxPaths.Clear();
            MergeFiles.Clear();

        }



        public async Task PickInvoices(Window window)
        {
            Errors.Clear();

            try
            {
                // Create a file picker
                var openPicker = new FileOpenPicker();

                // Retrieve the window handle (HWND) of the current WinUI 3 window.
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

                // Initialize the file picker with the window handle (HWND).
                WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

                // Set options for your file picker
                openPicker.ViewMode = PickerViewMode.List;
                openPicker.FileTypeFilter.Add(".pdf");

                // Open the picker for the user to pick a file
                var results = await openPicker.PickMultipleFilesAsync();

                if (results == null || results.Count == 0)
                    return;

                ClearAll();

                //Seperate Invoice File and Tax File
                foreach (var file in results)
                {
                    if (MergePath == null)
                        MergePath = file.Path.Replace(file.Name, String.Empty);

                    SeperateFiles(file);
                }

                //Matching Invoice File and Tax File
                SortedTaxPaths = new List<string>(TaxPaths);
                foreach (var mergeFile in MergeFiles)
                {
                    mergeFile.Match = FindTaxInvoice(mergeFile);
                }

            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
            }

        }

        public void SeperateFiles(StorageFile file)
        {
            Console.WriteLine(file.Name);
            string[] nameSplit;
            MergeFile mergeFile = new();

            try
            {
                //Seperating Invoice and Tax
                nameSplit = file.Name.Replace(".pdf", "").Split('-');
                mergeFile.InvoiceNumber = nameSplit[nameSplit.Length - 2];

                if (mergeFile.InvoiceNumber.Length != 10)
                {
                    //Tax Invoice
                    TaxPaths.Add(file.Path);
                    return;
                }

                //ULI Invoice
                InvoicePaths.Add(file.Path);
                mergeFile.TaxNumber = nameSplit[nameSplit.Length - 1];
                mergeFile.InvoicePath = file.Path;
                mergeFile.InvoiceFileName = file.Name;
                mergeFile.Match = true;
                MergeFiles.Add(mergeFile);

            }
            catch
            {
                Errors.Add($"{file.Name} is invalid file");
            }
        }
        public bool FindTaxInvoice(MergeFile mergeFile)
        {

            for (int i = 0; i < SortedTaxPaths.Count; i++)
            {
                try
                {
                    //Match File Name
                    if (!SortedTaxPaths[i].Replace(MergePath, String.Empty).Contains(mergeFile.TaxNumber))
                    {
                        mergeFile.FileStatus = MergeFileStatus.NotMatch;
                        continue;
                    }

                    //Find TaxNumber inside Pdf
                    var taxPdf = PdfReader.Open(SortedTaxPaths[i], PdfDocumentOpenMode.Import);
                    bool invoiceFound = false;
                    bool taxFound = false;

                    invoiceFound = PdfHelper.FindText(SortedTaxPaths[i], mergeFile.InvoiceNumber);
                    taxFound = PdfHelper.FindText(SortedTaxPaths[i], mergeFile.TaxNumberFormarted);

                    //Find by InvoiceNumber
                    //for (int p = 0; p < taxPdf.PageCount; p++)
                    //{

                    //    if (!invoiceFound)
                    //        invoiceFound = FindText(ContentReader.ReadContent(taxPdf.Pages[p]), mergeFile.InvoiceNumber);

                    //    if (!taxFound)
                    //        taxFound = FindText(ContentReader.ReadContent(taxPdf.Pages[p]), mergeFile.TaxNumberFormarted);
                    //}

                    //If Match
                    if (invoiceFound && taxFound)
                    {
                        mergeFile.TaxFileName = SortedTaxPaths[i].Replace(MergePath, String.Empty);
                        mergeFile.TaxPath = SortedTaxPaths[i];
                        SortedTaxPaths.Remove(SortedTaxPaths[i]);
                        mergeFile.FileStatus = MergeFileStatus.Match;
                        return true;
                    }
                    else if (!taxFound)
                    {
                        mergeFile.FileStatus = MergeFileStatus.NotMatch;
                    }

                    //string taxFoundTxt = taxFound ? "TaxFound" : "No Tax in TAXPDF";
                    //string invoiceFoundTxt = invoiceFound ? "InvoiceFound" : "No Invoice in TAXPDF";
                    //Errors.Add($"{mergeFile.InvoiceNumber} - {taxFoundTxt} - {invoiceFoundTxt}");
                }
                catch (Exception ex)
                {
                    Errors.Add($"{mergeFile.InvoiceNumber} Error: {ex}");
                }

            }

            return false;
        }

        public async Task MergeInvoices()
        {
            Errors.Clear();

            if (MergeFiles.Where(items => items.Match).ToList().Count == 0 || MergeFiles.Count == 0)
            {
                Errors.Add("No files ready for merging.");
                return;
            }

            //Reset Status
            foreach (var mergeFile in MergeFiles)
            {
                if (!mergeFile.Match)
                    continue;

                mergeFile.FileStatus = MergeFileStatus.Match;
            }

            foreach (var mergeFile in MergeFiles)
            {
                if (!mergeFile.Match)
                    continue;

                try
                {

                    //Get the Paths
                    string[] files = { mergeFile.InvoicePath, mergeFile.TaxPath };

                    //Output Doc
                    PdfDocument outputPdf = new();

                    //Merging
                    foreach (var file in files)
                    {
                        PdfDocument inputPdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                        for (int a = 0; a < inputPdf.PageCount; a++)
                        {
                            //Add Page to outputPDF
                            PdfPage page = inputPdf.Pages[a];
                            outputPdf.AddPage(page);
                        }
                    }

                    //Save Document
                    StorageFolder invoiceFolder = await StorageFolder.GetFolderFromPathAsync(MergePath);
                    StorageFolder mergerFolder = await invoiceFolder.CreateFolderAsync("Merged", CreationCollisionOption.OpenIfExists);
                    StorageFile mergedFile = await mergerFolder.CreateFileAsync($"{mergeFile.InvoiceNumber}.pdf", CreationCollisionOption.ReplaceExisting);
                    outputPdf.Save(mergedFile.Path);

                    mergeFile.FileStatus = MergeFileStatus.Merged;

                }
                catch (Exception ex)
                {
                    Errors.Add($"{mergeFile.InvoiceNumber} Error: {ex}");
                }

            }
        }

        private bool FindText(CSequence contents, string searchText)
        {
            // Iterate thru each content items. Each item may or may not contain the entire
            // word if there are different stylings (ex: bold parts of the word) applied to a word.
            // So you may have to replace a character at a time.
            bool found = false;

            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] is COperator)
                {
                    var cOp = contents[i] as COperator;
                    for (int j = 0; j < cOp.Operands.Count; j++)
                    {
                        if (cOp.OpCode.Name == OpCodeName.Tj.ToString() ||
                            cOp.OpCode.Name == OpCodeName.TJ.ToString())
                        {
                            if (cOp.Operands[j] is CString)
                            {
                                var cString = cOp.Operands[j] as CString;
                                if (cString.Value.Contains(searchText))
                                {
                                    found = true;
                                }

                            }
                        }
                    }


                }
            }

            return found;


        }

    }
}
