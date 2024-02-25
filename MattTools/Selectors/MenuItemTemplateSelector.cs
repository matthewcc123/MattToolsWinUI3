using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MattTools.Models;
using Newtonsoft.Json.Linq;

namespace MattTools.Selectors
{
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuItemTemplate { get; set; }
        public DataTemplate MenuItemParentTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            return (item as NavItem).HaveChild ? MenuItemParentTemplate : MenuItemTemplate;
        }

    }
}
