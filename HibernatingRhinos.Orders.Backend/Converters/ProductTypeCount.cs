using System;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using System.Linq;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class ProductTypeCount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pd = value as Products_Stats.ReduceResult;
            if (pd == null)
                return null;

            if ("Total" == (string)parameter)
            {
                return pd.Summary.Sum(x => x.Count);
            }

            var val = (ProductTypes)Enum.Parse(typeof(ProductTypes), (string)parameter, true);

            var firstOrDefault = pd.Summary.FirstOrDefault(x => x.Type == val);
            if (firstOrDefault == null)
                return 0;
            return firstOrDefault.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}