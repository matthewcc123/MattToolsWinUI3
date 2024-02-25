using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace MattTools.Interactions
{
    internal class AlternateListViewBehavior : Behavior<ListView>
    {

        public static readonly DependencyProperty AltItemBackgroundProperty = DependencyProperty.Register(
            "AltItemBackground",
            typeof(Brush),
            typeof(AlternateListViewBehavior),
            new PropertyMetadata(default(Brush))
            );

        public static readonly DependencyProperty AltItemBorderProperty = DependencyProperty.Register(
            "AltItemBorder",
            typeof(Brush),
            typeof(AlternateListViewBehavior),
            new PropertyMetadata(default(Brush))
            );

        public static readonly DependencyProperty AltItemThicknessProperty = DependencyProperty.Register(
            "AltItemThickness",
            typeof(Thickness),
            typeof(AlternateListViewBehavior),
            new PropertyMetadata(default(Thickness))
            );

        public Brush AltItemBackground
        {
            get { return (Brush)GetValue(AltItemBackgroundProperty); }
            set { SetValue(AltItemBackgroundProperty, value); }
        }

        public Brush AltItemBorder
        {
            get { return (Brush)GetValue(AltItemBorderProperty); }
            set { SetValue(AltItemBorderProperty, value); }
        }

        public Thickness AltItemThickness
        {
            get { return (Thickness)GetValue(AltItemThicknessProperty); }
            set { SetValue(AltItemThicknessProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ActualThemeChanged += OnThemeChanged;
            AssociatedObject.ContainerContentChanging += OnContainerChanged;

            if (AssociatedObject.Items != null)
            {
                AssociatedObject.Items.VectorChanged += OnItemVectorChanged; ;
            }

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ActualThemeChanged -= OnThemeChanged;
            AssociatedObject.ContainerContentChanging -= OnContainerChanged;

            if (AssociatedObject.Items != null)
            {
                AssociatedObject.Items.VectorChanged -= OnItemVectorChanged; ;
            }
        }
        private void OnItemVectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs args)
        {
            if (args.Index == (sender.Count - 1))
                return;

            if (args.CollectionChange is CollectionChange.ItemInserted or CollectionChange.ItemRemoved)
            {
                for (int i = (int)args.Index; i < sender.Count; i++)
                {
                    if (AssociatedObject.ContainerFromIndex(i) is SelectorItem itemContainer)
                    {
                        UpdateItemColor(itemContainer, i);
                    }
                }
            }
        }


        private void OnThemeChanged(FrameworkElement sender, object args)
        {
            if (AssociatedObject.Items == null) return;
            for (int i = 0; i < AssociatedObject.Items.Count; i++)
            {
                if (AssociatedObject.ContainerFromIndex(i) is SelectorItem itemContainer)
                {
                    UpdateItemColor(itemContainer, i);
                }
            }
        }


        private void OnContainerChanged(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase == 0)
            {
                UpdateItemColor(args.ItemContainer, args.ItemIndex);
            }
                
        }


        private void UpdateItemColor(SelectorItem itemContainer, int ItemIndex)
        {

            Border border = itemContainer.FindDescendant<Border>();

            if (ItemIndex % 2 == 0)
            {
                //Even
                itemContainer.Background = AltItemBackground;
                border.Background = AltItemBackground;
                border.BorderBrush = AltItemBorder;
                border.BorderThickness = AltItemThickness;
            }
            else
            {
                //Odd
                itemContainer.Background = null;
                border.Background = null;
                border.BorderThickness = default;
            }

        }



    }
}
