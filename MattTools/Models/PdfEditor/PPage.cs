using MattTools.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.PdfEditor
{
    public class PPage : PropertyChangedBaseModel
    {

        private BitmapImage _thumbnailImage;
        private PdfPage _pdfSharpPage;

        public PDoc ParentPdf { get; set; }
        public BitmapImage ThumbnailImage
        {
            get => _thumbnailImage;
            set
            {
                if (_thumbnailImage != value)
                {
                    _thumbnailImage = value;
                    OnPropertyChanged(nameof(ThumbnailImage));
                }
            }
        }

        public PdfPage PdfSharpPage
        {
            get => _pdfSharpPage;
            set
            {
                if (_pdfSharpPage != value)
                {
                    _pdfSharpPage = value;
                    OnPropertyChanged(nameof(PdfSharpPage));
                }
            }
        }
        public int PageIndex { get; set; }
        public SolidColorBrush ColorBrush { get; set; }

        public string PageNumber { get { return (PageIndex + 1).ToString(); } }

    }
}
