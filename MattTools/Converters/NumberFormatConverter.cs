using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Converters
{
    public class NumberFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            string stringFormat = "{0}";

            if (parameter is string && parameter != null)
                stringFormat = parameter as string;

            if (value is decimal decimalValue)
            {
                // Convert the decimal value to accounting format
                string formatValue = string.Format("{0:#,##0.00}", decimalValue);

                return string.Format(stringFormat, formatValue);
            }

            return string.Format(stringFormat, value); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Conversion back is not implemented in this example
            throw new NotImplementedException();
        }
    }
}
