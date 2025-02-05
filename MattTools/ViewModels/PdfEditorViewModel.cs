using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using System.Drawing;
using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;
using System.Collections.ObjectModel;
using Models.PdfEditor;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using MattTools.Helper;
using NPOI.POIFS.FileSystem;
using MattTools.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Linq;
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.Provider;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Pdf.AcroForms;
using NPOI.SS.Formula.Functions;
using MattTools.Models.Rossum;
using NPOI.HPSF;
using PdfSharp.Pdf.Advanced;
using Org.BouncyCastle.Asn1.Cmp;
using MattTools.Settings;
using Newtonsoft.Json;


namespace ViewModels
{
    public class PdfEditorViewModel
    {

        public ObservableCollection<PDoc> Documents { get; set; }
        public ObservableCollection<PPage> Pages { get; set; }

        public PdfEditorViewModel()
        {
            Documents = new ObservableCollection<PDoc>();
            Pages = new ObservableCollection<PPage>();

        }

        public async Task PickPDFMultiple(Window window, bool pageThumbnail)
        {

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

                if (results == null && results.Count == 0)
                    return;

                foreach (var file in results)
                {
                    AddPDFFile(file, pageThumbnail);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task PickPDFSingle(Window window, bool pageThumbnail)
        {

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
                var file = await openPicker.PickSingleFileAsync();

                if (file == null)
                    return;

                AddPDFFile(file, pageThumbnail);


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task AddPDFFile(StorageFile file, bool pageThumbnail)
        {
            SolidColorBrush color = RandomizeBorderColor();
            PdfDocument pdf = PdfReader.Open(file.Path, PdfDocumentOpenMode.Import);
            PDoc pdfDoc = new PDoc { PdfDoc = pdf, FileName = file.Name, Path = file.Path, ColorBrush = color, ThumbnailImage = PdfHelper.GetPdfThumbnail(file.Path, 0), CompressQuality = 0 };

            for (int i = 0; i < pdf.PageCount; i++)
            {

                PPage page = new PPage
                {
                    ParentPdf = pdfDoc,
                    PageIndex = i,
                    PdfSharpPage = pdf.Pages[i],
                    ColorBrush = color
                };

                //Get Thumbnails
                if (pageThumbnail)
                {
                    page.ThumbnailImage = PdfHelper.GetPdfThumbnail(page);
                }

                RenameFormFields(page.PdfSharpPage, color.Color.R + color.Color.G + color.Color.B);

                pdfDoc.Pages.Add(page);

                //Add To Pages Collections
                Pages.Add(page);
            }

            //Add To Documents Collection
            Documents.Add(pdfDoc);
            pdf.Dispose();
        }

        public async Task<string> OrganizePDF(Window window)
        {
            string compressResult = null;
            FileSavePicker savePicker = new FileSavePicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hWnd);

            //Settings
            savePicker.SuggestedFileName = $"{Documents[0].FileName.Replace(".pdf",string.Empty)}_organized.pdf";
            savePicker.FileTypeChoices.Add("PDF File", new List<string>() { ".pdf" });

            //StorageFile
            StorageFile file = await savePicker.PickSaveFileAsync();

            if (file == null)
                return null;

            try
            {
                //Output Doc
                PdfDocument outputPdf = new();

                // Merging pages
                foreach (var page in Pages)
                {
                    // Add page to outputPdf
                    PdfPage newPage = outputPdf.AddPage(page.PdfSharpPage);
                }

                //SaveFile
                outputPdf.Save(file.Path);
                compressResult = $"PDF successfully organized as {file.Name}";
            }
            catch
            {
                compressResult = $"Failed to organize PDF.";
            }

            return compressResult;

        }

        public async Task<bool> CompressPDF(string inputPath, string outputPath, PDoc Document)
        {
            bool result = false;

            string appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            StorageFolder roamingFolder = await StorageFolder.GetFolderFromPathAsync(appDataRoamingPath);
            StorageFolder mattFolder = await roamingFolder.CreateFolderAsync("MattTools", CreationCollisionOption.OpenIfExists);
            StorageFolder compressFolder = await mattFolder.CreateFolderAsync("CompressionCache", CreationCollisionOption.OpenIfExists);

            string inputFilePath = Path.Combine(compressFolder.Path, Document.FileName);

            //Output Doc
            PdfDocument outputPdf = new();

            // Merging pages
            foreach (var page in Pages)
            {
                // Add page to outputPdf
                PdfPage newPage = outputPdf.AddPage(page.PdfSharpPage);
            }

            outputPdf.Save(inputFilePath);

            try
            {
                Document.IsCompressing = true;
                string compressQuality = new[] { "prepress", "printer", "ebook" }[Document.CompressQuality];

                string arguments = $"-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/{compressQuality} -dDownsampleColorImages=true -dColorImageResolution=100 -dNOPAUSE -dQUIET -dBATCH -sOutputFile=\"{outputPath}\" \"{inputFilePath}\"";

                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Ghostscript/gswin64c.exe"),
                    Arguments = arguments
                };

                await RunProcessAsync(processStartInfo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                StorageFile file = await compressFolder.GetFileAsync(Document.FileName);
                await file.DeleteAsync();
                Document.IsCompressing = false;
                result = true;
            }

            return result;
        }

        public async Task<string> CompressSinglePDF(PDoc Document, Window window)
        {
            string compressPath = string.Empty;
            string compressResult = null;
            string compressName = string.Empty;

            try
            {
                FileSavePicker savePicker = new FileSavePicker();
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hWnd);
                savePicker.SuggestedFileName = $"{Document.FileName.Replace(".pdf", string.Empty)}_compressed.pdf";
                savePicker.FileTypeChoices.Add("PDF File", new List<string>() { ".pdf" });
                StorageFile file = await savePicker.PickSaveFileAsync();

                if (file == null) return null;
                compressPath = file.Path;
                compressName = file.Name;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            if (compressPath != string.Empty)
            {
                Document.IsCompressing = true;
                string outputPath = compressPath; // You might want to change this to a different output directory
                bool Success = await CompressPDF(Document.Path, outputPath, Document);

                if (Success)
                {
                    PdfDocument pdf = PdfReader.Open(compressPath, PdfDocumentOpenMode.InformationOnly);
                    Document.CompressedFileSize = $"{(pdf.FileSize / 1_000_000.0):F2} MB";
                    pdf.Dispose();
                    compressResult = $"\u2022 {compressName} compressed from {Document.FileSize} to {Document.CompressedFileSize}";
                }
                else
                {
                    compressResult = $"\u2022 Compression of {compressName} failed";
                }

            }

            return compressResult;

        }

        public async Task<string> CompressAllPdf(Window window, int quality)
        {
            string compressPath = string.Empty;
            string compressResult = null;

            try
            {
                var folderPicker = new FolderPicker();

                // Retrieve the window handle (HWND) of the current WinUI 3 window.
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

                // Initialize the file picker with the window handle (HWND).
                WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

                // Set options for folder picker
                folderPicker.ViewMode = PickerViewMode.List;

                //Open FolderPicker
                StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                if (folder == null)
                    return null;

                compressPath = folder.Path;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            if (compressPath != string.Empty)
            {
                foreach (var Document in Documents)
                {
                    Document.CompressQuality = quality;
                    Document.IsCompressing = true;
                }

                foreach (var Document in Documents)
                {
                    string outputPath = Path.Combine(compressPath,$"{Document.FileName.Replace(".pdf", string.Empty)}_compressed.pdf");
                    bool Success = await CompressPDF(Document.Path, outputPath, Document);

                    if (Success)
                    {
                        PdfDocument pdf = PdfReader.Open(outputPath, PdfDocumentOpenMode.InformationOnly);
                        Document.CompressedFileSize = $"{(pdf.FileSize / 1_000_000.0):F2} MB";
                        pdf.Dispose();
                        compressResult += $"\u2022 {Document.FileName} compressed from {Document.FileSize} to {Document.CompressedFileSize}{Environment.NewLine}";
                    }
                    else
                    {
                        compressResult += $"\u2022 {Document.FileName} compression Failed{Environment.NewLine}";
                    }

                }

            }

            return compressResult;

        }

        private PdfDocument OptimizedPDF(string path)
        {
            // Open the original document
            PdfDocument originalDocument = PdfReader.Open(path, PdfDocumentOpenMode.Import);

            // Create a new document
            PdfDocument optimizedDocument = new PdfDocument();

            // Copy each page from the original document to the new document
            foreach (PdfPage page in originalDocument.Pages)
            {
                optimizedDocument.AddPage(page);
            }

            // Save the optimized document
            return optimizedDocument;
        }
        private void RenameFormFields(PdfPage page, int pageIndex)
        {
            //PdfItem annotations = page.Elements.GetDictionary("/Annots");

            foreach (PdfAnnotation annot in page.Annotations)
            {
                var elementValue = annot.Elements.GetValue("/T");
                if (elementValue == null)
                    return;

                string originalName = elementValue.ToString();

                string newName = "(" + originalName.Substring(1,originalName.Length-2) + " - " + pageIndex.ToString() + ")";
                annot.Elements.SetName("/T", newName);
            }
        }

        public void RefreshPage()
        {
            Pages.Clear();

            foreach (var document in Documents)
            {
                foreach (var page in document.Pages)
                {
                    Pages.Add(page);
                }
            }
        }

        public void RemovePage(PPage page)
        {
            Pages.Remove(page);
        }

        public void RemoveDocument(PDoc document)
        {
            Documents.Remove(document);

            var pagesToRemove = Pages.Where(p => p.ParentPdf == document).ToList();

            // Remove these pages from the Pages list
            foreach (var page in pagesToRemove)
            {
                Pages.Remove(page);
            }

        }

        private SolidColorBrush RandomizeBorderColor()
        {
            // Create a random number generator
            Random random = new Random();

            // Generate random RGB values
            byte red = (byte)random.Next(256);
            byte green = (byte)random.Next(256);
            byte blue = (byte)random.Next(256);

            // Create a SolidColorBrush with the random color
            return new SolidColorBrush(Windows.UI.Color.FromArgb(255, red, green, blue));
        }

        public static async Task RunProcessAsync(ProcessStartInfo processStartInfo)
        {
            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                // Use Task.Run to handle the WaitForExit asynchronously
                await Task.Run(() => process.WaitForExit());

                // Optionally, read the output or handle the results here
                if (!processStartInfo.RedirectStandardOutput)
                {
                    string output = await process.StandardOutput.ReadToEndAsync();
                    Console.WriteLine(output);
                }
            }
        }

    }

}

