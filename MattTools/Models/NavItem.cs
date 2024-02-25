using CommunityToolkit.WinUI.UI.Controls.TextToolbarSymbols;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbol = Microsoft.UI.Xaml.Controls.Symbol;

namespace MattTools.Models
{
    public class NavItem
    {
        public string Path { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Type PageType { get; set; }
        public string UriSource { get; set; }
        public Symbol Symbol { get; set; }
        public IconElement Icon { get { return GetIconElement(); } set { } }
        public bool VisibleInMenu { get; set; } = true;
        public bool ExtendOnly { get; set; } = false;

        public ObservableCollection<NavItem> ChildItems { get; set; }
        public bool HaveChild
        {
            get
            {
            return ChildItems.Count > 0;
            }
        }

        public NavItem()
        {
            ChildItems = new ObservableCollection<NavItem>();
        }

        public IconElement GetIconElement()
        {
            IconElement iicon = null;

            if (UriSource != null)
            {
                iicon = new BitmapIcon { UriSource = new Uri("ms-appx:///Assets/Icons/" + UriSource) };
            }
            else if (Symbol != 0)
            {
                iicon = new SymbolIcon(Symbol);
            }

            return iicon;
        }


    }
}
