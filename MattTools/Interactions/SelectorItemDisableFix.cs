using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace MattTools.Interactions
{
    internal class SelectorItemDisableFix : Behavior<ListViewBase>
    {

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.ContainerContentChanging += AssociatedObject_ContainerContentChanging;

            //if (AssociatedObject.Items != null)
            //{
            //    AssociatedObject.Items.VectorChanged += Items_VectorChanged;
            //}
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.ContainerContentChanging -= AssociatedObject_ContainerContentChanging;

            //if (AssociatedObject.Items != null)
            //{
            //    AssociatedObject.Items.VectorChanged -= Items_VectorChanged;
            //}
        }

        private void AssociatedObject_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase == 0)
                UpdateSelectorItem();
        }

        private void UpdateSelectorItem()
        {

            foreach (var item in AssociatedObject.Items)
            {
                if (AssociatedObject.ContainerFromItem(item) is SelectorItem itemContainer)
                {
                    itemContainer.IsEnabled = false;
                }
            }

        }

    }
}
