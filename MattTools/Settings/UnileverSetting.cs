using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Settings
{
    public class UnileverSetting
    {
        public string TaxFormat { get; set; }

        public UnileverSetting()
        {
            TaxFormat = "{000}.{000}-{00}.{00000000}";
        }

    }
}
