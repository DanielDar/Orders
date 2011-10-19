using System;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Orders;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class AddressToMultiLineStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var address = value as Address;
            if (address == null)
                return null;

            return string.Format(@"Address1: {0}
Address2: {1}
City: {2}
State/Province: {3}
Zip/Postal Code: {4}
Country: {5}", address.Address1, address.Address2, address.City, address.State, address.ZipCode, address.Country);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
