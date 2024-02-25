using MattTools.Models;
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
    public class MergeStatusIndicatorConverter : DependencyObject , IValueConverter
    {

        public static readonly DependencyProperty ReadyBrushProperty =
            DependencyProperty.Register(nameof(ReadyBrush), typeof(SolidColorBrush), typeof(MergeStatusIndicatorConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty NotMatchBrushProperty =
            DependencyProperty.Register(nameof(NotMatchBrush), typeof(SolidColorBrush), typeof(MergeStatusIndicatorConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty MergedBrushProperty =
            DependencyProperty.Register(nameof(MergedBrush), typeof(SolidColorBrush), typeof(MergeStatusIndicatorConverter), new PropertyMetadata(null));

        public SolidColorBrush ReadyBrush
        {
            get { return (SolidColorBrush)GetValue(ReadyBrushProperty); }
            set { SetValue(ReadyBrushProperty, value); }
        }

        public SolidColorBrush NotMatchBrush
        {
            get { return (SolidColorBrush)GetValue(NotMatchBrushProperty); }
            set { SetValue(NotMatchBrushProperty, value); }
        }

        public SolidColorBrush MergedBrush
        {
            get { return (SolidColorBrush)GetValue(MergedBrushProperty); }
            set { SetValue(MergedBrushProperty, value); }
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            SolidColorBrush Brush = NotMatchBrush;

            if (value is MergeFileStatus fileStatus)
            {
                switch (fileStatus)
                {
                    case MergeFileStatus.NotMatch:
                        Brush = NotMatchBrush;
                        break;
                    case MergeFileStatus.Match:
                        Brush = ReadyBrush;
                        break;
                    case MergeFileStatus.Merged:
                        Brush = MergedBrush;
                        break;
                    default:
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
