using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models
{
    public class MergeFile : PropertyChangedBaseModel
    {

        private string _InvoiceNumber;
        private string _TaxNumber;
        private string _InvoiceFileName;
        private string _TaxFileName;
        private string _InvoicePath;
        private string _TaxPath;
        private bool _Match;
        private MergeFileStatus _FileStatus;

        public string InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set 
            {
                if (_InvoiceNumber != value)
                {
                    _InvoiceNumber = value;
                    OnPropertyChanged(nameof(InvoiceNumber));
                }
            }
        }

        public string TaxNumber
        {
            get { return _TaxNumber; }
            set
            {
                if (_TaxNumber != value)
                {
                    _TaxNumber = value;
                    OnPropertyChanged(nameof(TaxNumber));
                    OnPropertyChanged(nameof(TaxNumberFormarted));
                }
            }
        }

        public string InvoiceFileName
        {
            get { return _InvoiceFileName; }
            set
            {
                if (_InvoiceFileName != value)
                {
                    _InvoiceFileName = value;
                    OnPropertyChanged(nameof(InvoiceFileName));
                }
            }
        }

        public string TaxFileName
        {
            get { return _TaxFileName; }
            set
            {
                if (_TaxFileName != value)
                {
                    _TaxFileName = value;
                    OnPropertyChanged(nameof(TaxFileName));
                }
            }
        }

        public string InvoicePath
        {
            get { return _InvoicePath; }
            set
            {
                if (_InvoicePath != value)
                {
                    _InvoicePath = value;
                    OnPropertyChanged(nameof(InvoicePath));
                }
            }
        }

        public string TaxPath
        {
            get { return _TaxPath; }
            set
            {
                if (_TaxPath != value)
                {
                    _TaxPath = value;
                    OnPropertyChanged(nameof(TaxPath));
                }
            }
        }

        public bool Match
        {
            get { return _Match; }
            set
            {
                if (_Match != value)
                {
                    _Match = value;
                    OnPropertyChanged(nameof(Match));
                }
            }
        }

        public MergeFileStatus FileStatus
        {
            get { return _FileStatus; }
            set
            {
                if (_FileStatus != value)
                {
                    _FileStatus = value;
                    OnPropertyChanged(nameof(FileStatus));
                    OnPropertyChanged(nameof(StatusFormated));
                }
            }
        }

        public string TaxNumberFormarted { get { return $"{TaxNumber.Substring(0, 3)}.{TaxNumber.Substring(3, 3)}-{TaxNumber.Substring(6, 2)}.{TaxNumber.Remove(0, 8)}"; } }

        public string StatusFormated 
        { 
            get
            {
                string text = "";

                switch (FileStatus)
                {
                    case MergeFileStatus.NotMatch:
                        text = "Not Found";
                        break;
                    case MergeFileStatus.Match:
                        text = "Ready";
                        break;
                    case MergeFileStatus.Merged:
                        text = "Merged";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

    }

    public enum MergeFileStatus
    {
        NotMatch,
        Match,
        Merged
    }

}

