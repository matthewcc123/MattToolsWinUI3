using MattTools.Models;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Services
{
    public interface INavigationService
    {

        ObservableCollection<NavItem> NavItems { get; }
        Type CurrentPage { get; }
        void Init(Frame frame, NavigationView navigationView, Type notFoundPage);
        void Navigate(string path);
        void NavigateByType(Type pageType);
        void AddNavigation(NavItem navItem);

    }
}
