using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                // Convert DateTime to DateTimeOffset
                return new DateTimeOffset(dateTime);
            }

            return null; // Return null if conversion fails
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                // Convert DateTimeOffset to DateTime
                return dateTimeOffset.DateTime;
            }

            return null; // Return null if conversion fails
        }

    }
}
