using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Products;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class StringToProductsTypesEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ProductTypes result;
            Enum.TryParse(value.ToString(), out result);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as ComboBoxItem;
            if (item != null)
            {
                return item.Content.ToString();
            }

            return "None";
        }
    }
}