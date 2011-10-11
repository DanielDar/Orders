using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using HibernatingRhinos.Orders.Backend.Features.Products;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class StringToProductsTypesEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (str == null)
                return null;
            if (str == "Lifetime")
                return ProductTypes.LifeTime;
            if (str == "Yearly")
                return ProductTypes.Yearly;
            if (str == "Monthly")
                return ProductTypes.Monthly;
            else
            {
                return ProductTypes.None;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}