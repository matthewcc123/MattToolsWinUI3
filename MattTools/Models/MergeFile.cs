using MattTools.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
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

        private IUserService _userService;

        public MergeFile()
        {
            //Get Services
            App app = (App)Application.Current;
            _userService = app.ServiceProvider.GetService<IUserService>();
        }

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

        public string TaxNumberFormarted { get { return FormatTaxNumber(TaxNumber); } }

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

        private string FormatTaxNumber(string taxNumber)
        {
            string format = _userService.GetUserSetting<string>("Unilever.TaxFormat"); //"{00}"

            StringBuilder result = new StringBuilder();
            int inputIndex = 0;
            bool inFormat = false;

            for (int i = 0; i < format.Length; i++)
            {

                char c = format[i];

                if (c == '{') //START IN FORMAT
                {
                    inFormat = true;
                    continue;
                }

                if (c == '}') //END THE FORMAT
                {
                    inFormat = false;
                    continue;
                }

                if (inFormat && c == '0') //IN FORMAT CHECK ITS 0
                {
                    if (inputIndex < taxNumber.Length) // CHECK TAXNUMBER LENGTH > INPUT INDEX
                    {
                        result.Append(taxNumber[inputIndex]); //ADD TO STRING BUILDER
                        inputIndex++;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    result.Append(c); // IF NOT JUST ADD THE CHAR TO BUILDER
                }

            }

            return result.ToString();

        }

    }

    public enum MergeFileStatus
    {
        NotMatch,
        Match,
        Merged
    }

}

