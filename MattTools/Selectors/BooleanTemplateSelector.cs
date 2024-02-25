using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Selectors
{
    public class BooleanTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TrueTemplate { get; set; }
        public DataTemplate FalseTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is bool value)
            {
                return value ? TrueTemplate : FalseTemplate;
            }

            return base.SelectTemplateCore(item, container);
        }

    }
}
