using Microsoft.UI.Xaml.Media.Imaging;
using Models.PdfEditor;
using PdfiumViewer;
using System;
using System.Collections.Generic;
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
                PdfRotation pdfiumRotation = PdfRotation.Rotate0;
                switch (page.PdfSharpPage.Rotate)
                {
                    case 90:
                        pdfiumRotation = PdfRotation.Rotate90;
                        break;
                    case 180:
                        pdfiumRotation = PdfRotation.Rotate180;
                        break;
                    case 270:
                        pdfiumRotation = PdfRotation.Rotate270;
                        break;
                }

                pdfDocument.RotatePage(page.PageIndex, pdfiumRotation);

                using (var image = pdfDocument.Render(page.PageIndex, 
                    pdfiumRotation == PdfRotation.Rotate0 || pdfiumRotation == PdfRotation.Rotate180 ? (int)page.PdfSharpPage.Width.Point : (int)page.PdfSharpPage.Height.Point,
                    pdfiumRotation == PdfRotation.Rotate0 || pdfiumRotation == PdfRotation.Rotate180 ? (int)page.PdfSharpPage.Height.Point : (int)page.PdfSharpPage.Width.Point,
                    75, 75, pdfiumRotation, PdfiumViewer.PdfRenderFlags.Annotations))
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

        }


    }
}
