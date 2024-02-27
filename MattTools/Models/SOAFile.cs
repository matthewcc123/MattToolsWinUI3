using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Models
{
    public class SOAFile : PropertyChangedBaseModel
    {
        private string _FileName;
        private string _Cabang;

        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }

        public string Cabang
        {
            get
            {
                return _Cabang;
            }
            set
            {
                if (_Cabang != value)
                {
                    _Cabang = value;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }

        public string FilePath { get; set; }
        public ISheet OriginalSheet { get; set; }
        public ISheet Sheet { get; set; }
    }
}
