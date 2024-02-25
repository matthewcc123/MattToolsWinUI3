using MattTools.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MattTools.Converters
{
    public class RossumStatusIndicatorConverter : DependencyObject , IValueConverter
    {
        public static readonly DependencyProperty DefaultBrushProperty =
            DependencyProperty.Register(nameof(DefaultBrush), typeof(SolidColorBrush), typeof(RossumStatusIndicatorConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty ReviewBrushProperty =
            DependencyProperty.Register(nameof(ReviewBrush), typeof(SolidColorBrush), typeof(RossumStatusIndicatorConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty ConfirmedBrushProperty =
            DependencyProperty.Register(nameof(ConfirmedBrush), typeof(SolidColorBrush), typeof(RossumStatusIndicatorConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty DeletedBrushProperty =
            DependencyProperty.Register(nameof(DeletedBrush), typeof(SolidColorBrush), typeof(RossumStatusIndicatorConverter), new PropertyMetadata(null));

        public SolidColorBrush DefaultBrush
        {
            get { return (SolidColorBrush)GetValue(DefaultBrushProperty); }
            set { SetValue(DefaultBrushProperty, value); }
        }

        public SolidColorBrush ReviewBrush
        {
            get { return (SolidColorBrush)GetValue(ReviewBrushProperty); }
            set { SetValue(ReviewBrushProperty, value); }
        }

        public SolidColorBrush ConfirmedBrush
        {
            get { return (SolidColorBrush)GetValue(ConfirmedBrushProperty); }
            set { SetValue(ConfirmedBrushProperty, value); }
        }

        public SolidColorBrush DeletedBrush
        {
            get { return (SolidColorBrush)GetValue(DeletedBrushProperty); }
            set { SetValue(DeletedBrushProperty, value); }
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            SolidColorBrush Brush = DefaultBrush;

            if (value is string status)
            {
                switch (status)
                {
                    default:
                    case "not_found":
                        break;
                    case "to_review":
                    case "reviewing":
                        Brush = ReviewBrush;
                        break;
                    case "confirmed":
                    case "exported":
                    case "exporting":
                        Brush = ConfirmedBrush;
                        break;
                    case "deleted":
                    case "purged":
                        Brush = DeletedBrush;
                        break;
                }
            }

            return Brush;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
