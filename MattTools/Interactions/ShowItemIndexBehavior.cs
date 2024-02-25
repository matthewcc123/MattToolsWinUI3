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
    internal class ShowItemIndexBehavior : Behavior<ListViewBase>
    {
        public static DependencyProperty TextBlockNameProperty = DependencyProperty.Register(
            nameof(TextBlockName),
            typeof(string), 
            typeof(ShowItemIndexBehavior), 
            new PropertyMetadata(default(string)));

        public string TextBlockName
        {
            get { return (string)GetValue(TextBlockNameProperty); }
            set { SetValue(TextBlockNameProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.ContainerContentChanging += AssociatedObject_ContainerContentChanging;

            if (AssociatedObject.Items != null)
            {
                AssociatedObject.Items.VectorChanged += Items_VectorChanged;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.ContainerContentChanging -= AssociatedObject_ContainerContentChanging;

            if (AssociatedObject.Items != null)
            {
                AssociatedObject.Items.VectorChanged -= Items_VectorChanged;
            }
        }

        private void AssociatedObject_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase > 0 || args.InRecycleQueue) return;
                UpdateIndex(args.ItemIndex);
        }


        private void Items_VectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs args)
        {
            //Skip if last Index
            if (args.Index == (sender.Count - 1))
                return;

            //Check if Item Inserted or Removed
            if (args.CollectionChange == CollectionChange.ItemInserted || args.CollectionChange == CollectionChange.ItemRemoved)
            {
                for (int i = (int)args.Index; i < sender.Count; i++)
                {
                    UpdateIndex(i);
                }
            }

        }

        private void UpdateIndex(int ItemIndex)
        {

            if (AssociatedObject.ContainerFromIndex(ItemIndex) is SelectorItem itemContainer
                && itemContainer.FindDescendant("Index") is TextBlock textBlock)
            {
                textBlock.Text = (ItemIndex + 1).ToString();
            }

        }

    }
}
