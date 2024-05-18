using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;
using Models.Zesthub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace MattTools.Interactions
{
    internal class LineItemListViewBehavior : Behavior<ListView>
    {

        public static readonly DependencyProperty MatchBackgroundProperty = DependencyProperty.Register(
            "MatchBackground",
            typeof(Brush),
            typeof(LineItemListViewBehavior),
            new PropertyMetadata(default(Brush))
            );

        public static readonly DependencyProperty NotMatchBackgroundProperty = DependencyProperty.Register(
            "NotMatchBackground",
            typeof(Brush),
            typeof(LineItemListViewBehavior),
            new PropertyMetadata(default(Brush))
            );

        public static readonly DependencyProperty MatchBorderProperty = DependencyProperty.Register(
            "MatchBorder",
            typeof(Brush),
            typeof(LineItemListViewBehavior),
            new PropertyMetadata(default(Brush))
            );

        public static readonly DependencyProperty NotMatchBorderProperty = DependencyProperty.Register(
            "NotMatchBorder",
            typeof(Brush),
            typeof(LineItemListViewBehavior),
            new PropertyMetadata(default(Brush))
            );

        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            "BorderThickness",
            typeof(Thickness),
            typeof(AlternateListViewBehavior),
            new PropertyMetadata(default(Thickness))
            );


        public Brush MatchBackground
        {
            get { return (Brush)GetValue(MatchBackgroundProperty); }
            set { SetValue(MatchBackgroundProperty, value); }
        }

        public Brush NotMatchBackground
        {
            get { return (Brush)GetValue(NotMatchBackgroundProperty); }
            set { SetValue(NotMatchBackgroundProperty, value); }
        }

        public Brush MatchBorder
        {
            get { return (Brush)GetValue(MatchBorderProperty); }
            set { SetValue(MatchBorderProperty, value); }
        }

        public Brush NotMatchBorder
        {
            get { return (Brush)GetValue(NotMatchBorderProperty); }
            set { SetValue(NotMatchBorderProperty, value); }
        }

        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
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

            if (AssociatedObject.ItemFromContainer(itemContainer) is DataLine lineItem)
            {
                itemContainer.Background = lineItem.NotMatch ? NotMatchBackground : MatchBackground;
                border.Background = lineItem.NotMatch ? NotMatchBackground : MatchBackground;
                border.BorderBrush = lineItem.NotMatch ? NotMatchBorder : MatchBorder;
                border.BorderThickness = BorderThickness;
            }

            //if (ItemIndex % 2 == 0)
            //{
            //    //Even
            //    itemContainer.Background = AltItemBackground;
            //    border.Background = AltItemBackground;
            //    border.BorderBrush = AltItemBorder;
            //    border.BorderThickness = AltItemThickness;
            //}
            //else
            //{
            //    //Odd
            //    itemContainer.Background = null;
            //    border.Background = null;
            //    border.BorderThickness = default;
            //}

        }



    }
}
