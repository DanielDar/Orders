using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class ArrayListToStringConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(", ", (IEnumerable<string>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}