using MattTools.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Services
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;
        private NavigationView _navView;
        private Type _notFoundPage;
        private string _currentPath;
        private ObservableCollection<NavItem> _navItems;
        public ObservableCollection<NavItem> NavItems => _navItems;
        public Type CurrentPage => _frame.CurrentSourcePageType;

        public NavigationService()
        {

            _navItems = new ObservableCollection<NavItem>();

        }

        public void AddNavigation(NavItem navItem)
        {

            //Check Have Parent
            List<string> pathList = navItem.Path.Split('/').ToList();
            string pathName = pathList.Last();
            pathList.RemoveAt(pathList.Count - 1);

            if (pathList.Count > 0)
            {
                NavItem parentNavItem = FindNavItemByPath(navItem.Path.Replace("/" + pathName, string.Empty));

                //Add NavItem to Parent
                if (parentNavItem != null)
                {
                    parentNavItem.ChildItems.Add(navItem);
                }
                else
                {
                    Console.WriteLine($"Path {pathName} not found");
                }
            }
            else
            {
                //Add NavItem
                _navItems.Add(navItem);
            }
        }

        private NavItem FindNavItemByPath(string Path)
        {
            List<string> pathList = Path.Split('/').ToList();

            NavItem navItem = null;
            ObservableCollection<NavItem> currentNavItems = _navItems;

            //Get Parent
            foreach (string path in pathList)
            {
                string currentPath = string.Join("/", pathList.Take(pathList.IndexOf(path) + 1));
                NavItem findNavItem = FindNavItemByPath(currentNavItems, currentPath);

                //Break the loop if not found
                if (findNavItem == null)
                {
                    break;
                }

                //Found the item
                navItem = findNavItem;

                //Break Loop if have the same path
                if (Path == navItem.Path)
                    break;

                //Find Next in ChildItems
                currentNavItems = navItem.ChildItems;
            }

            return navItem;

        }

        private NavItem FindNavItemByPath(ObservableCollection<NavItem> items, string path)
        {
            return items.FirstOrDefault(item => item.Path != null && item.Path == path);
        }

        public void Init(Frame frame, NavigationView navigationView, Type notFoundPage)
        {
            _frame = frame;
            _navView = navigationView;
            _notFoundPage = notFoundPage;
        }

        public void Navigate(string path)
        {

            List<string> parents = path.Split('/').ToList();
            NavItem selectedItem = null;
            ObservableCollection<NavItem> findNavItem = _navItems;

            foreach (string parent in parents)
            {
                string currentPath = string.Join("/", parents.Take(parents.IndexOf(parent) + 1));
                var navItem = FindNavItemByTag(findNavItem, currentPath);
                //Debug.WriteLine(currentPath);

                if (navItem == null)
                {
                    break;
                }

                selectedItem = navItem;

                if (navItem.ChildItems != null)
                {
                    findNavItem = navItem.ChildItems;
                }
            }

            if (selectedItem != null)
            {
                if (_currentPath == selectedItem.Path)
                    return;

                _navView.SelectedItem = selectedItem;
                _currentPath = selectedItem.Path;

                if (selectedItem.PageType != null)
                {
                    _frame.Navigate(selectedItem.PageType, null, new DrillInNavigationTransitionInfo());
                }
                else
                {
                    _frame.Navigate(_notFoundPage, null, new DrillInNavigationTransitionInfo());
                }

            }
        }

        public void NavigateByType(Type pageType)
        {
            _currentPath = string.Empty;
            _frame.Navigate(pageType, null, new DrillInNavigationTransitionInfo());
        }

        private NavItem FindNavItemByTag(ObservableCollection<NavItem> items, string tag)
        {
            return items.FirstOrDefault(item => item.Path != null && item.Path == tag);
        }

    }
}
