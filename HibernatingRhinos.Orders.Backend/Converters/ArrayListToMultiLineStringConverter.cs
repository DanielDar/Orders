using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class ArrayListToMultiLineStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return string.Join(Environment.NewLine, (IEnumerable<string>) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string) value).Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
