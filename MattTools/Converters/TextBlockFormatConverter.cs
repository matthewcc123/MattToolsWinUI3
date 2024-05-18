using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converters
{
    public class TextBlockFormatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Ensure value and parameter are valid
            if (value == null || parameter == null)
            {
                return string.Empty;
            }

            // Cast parameter to string (it should be a format string)
            string formatString = parameter.ToString();

            // Return the formatted string using the format string and value
            return string.Format(formatString + " : {0}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
