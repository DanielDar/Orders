using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class ComboBoxItemToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new ComboBoxItem();
            }

            var item = new ComboBoxItem();
            item.Content = value;
            return item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as ComboBoxItem;
            if (item != null)
            {
                return item.Content.ToString();
            }

            return "";
        }
    }
}