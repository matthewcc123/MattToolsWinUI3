using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool invert = parameter?.ToString()?.Equals("invert", StringComparison.OrdinalIgnoreCase) ?? false;

            if (value is bool boolValue)
            {
                if (invert)
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool invert = parameter?.ToString()?.Equals("invert", StringComparison.OrdinalIgnoreCase) ?? false;

            if (value is Visibility visibility)
            {
                if (invert)
                {
                    return visibility == Visibility.Collapsed;
                }
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }

}
