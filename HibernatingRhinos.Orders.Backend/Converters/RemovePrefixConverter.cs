using System;
using System.Globalization;
using System.Windows.Data;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class RemovePrefixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            int num;
            int.TryParse((string)parameter, out num);
            if (str == null)
                return "";
            return str.Substring(num);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}