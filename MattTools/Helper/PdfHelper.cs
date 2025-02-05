using Microsoft.UI.Xaml.Media.Imaging;
using Models.PdfEditor;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MattTools.Helper
{
    public class PdfHelper
    {

        static public BitmapImage GetPdfThumbnail(string PdfPath, int PageNumber)
        {
            using (var pdfDocument = PdfiumViewer.PdfDocument.Load(PdfPath))
            {
                using (var image = pdfDocument.Render(PageNumber, 75, 75, PdfiumViewer.PdfRenderFlags.Annotations))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(memoryStream.AsRandomAccessStream());
                        return bitmapImage;
                    }
                }
            }
            // Memory stream is automatically disposed after exiting this method
        }

        static public BitmapImage GetPdfThumbnail(PPage page)
        {
            using (var pdfDocument = PdfiumViewer.PdfDocument.Load(page.ParentPdf.Path))
            {


                //Rotate Document to Correct Position
                PdfRotation pdfiumRotation = (PdfRotation)(page.PageRotation / 90);
                pdfDocument.RotatePage(page.PageIndex, pdfiumRotation);

                // Render the page
                using (var image = pdfDocument.Render(page.PageIndex, 75, 75, PdfiumViewer.PdfRenderFlags.Annotations))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.RotateFlip((RotateFlipType)pdfiumRotation);
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        // Create a BitmapImage from the rendered image
                        var bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(memoryStream.AsRandomAccessStream());
                        return bitmapImage;
                    }
                }
            }

        }

        static public bool FindText(string PdfPath, string text)
        {
            using (var pdfDocument = PdfiumViewer.PdfDocument.Load(PdfPath))
            {
                var matches = pdfDocument.Search(text, false, true);
                return matches.Items.Count > 0;
            }
            // Memory stream is automatically disposed after exiting this method
        }


    }
}
