using MattTools.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Org.BouncyCastle.Utilities;
using PdfSharp.Pdf;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.PdfEditor
{
    public class PDoc : PropertyChangedBaseModel
    {

        public PdfDocument PdfDoc { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public List<PPage> Pages { get; set; }
        public SolidColorBrush ColorBrush { get; set; }
        public BitmapImage ThumbnailImage { get; set; }

        public string FileSize { get
            {
                if (PdfDoc != null)
                {
                    // Convert bytes to megabytes (base-10)
                    double megabytes = PdfDoc.FileSize / 1_000_000.0;

                    // Format the megabytes value to 2 decimal places
                    string formattedMegabytes = String.Format("{0:F2}", megabytes);

                    // Alternatively, you can use string interpolation
                    string formattedMegabytesInterpolated = $"{megabytes:F2}";

                    return $"{formattedMegabytes} MB";
                }
                else
                {
                    return "-";
                }
            } 
        }

        private int _compressQuality;
        private string _compressedPath;
        private bool _compressing;
        private string _compressedFileSize;

        public PDoc() {
            Pages = new List<PPage>();
            CompressedFileSize = "-";
        }

        public int CompressQuality
        {
            get => _compressQuality;
            set
            {
                if (_compressQuality != value)
                {
                    _compressQuality = value;
                    OnPropertyChanged(nameof(CompressQuality));
                }
            }
        }

        public string CompressedFileSize
        {
            get => _compressedFileSize;
            set
            {
                if (_compressedFileSize != value)
                {
                    _compressedFileSize = value;
                    OnPropertyChanged(nameof(CompressedFileSize));
                }
            }
        }

        public bool IsCompressing
        {
            get => _compressing;
            set
            {
                if (_compressing != value)
                {
                    _compressing = value;
                    OnPropertyChanged(nameof(IsCompressing));
                }
            }
        }

        public bool IsCompressingInvert
        {
            get { return !IsCompressing; }
        }

    }
}
