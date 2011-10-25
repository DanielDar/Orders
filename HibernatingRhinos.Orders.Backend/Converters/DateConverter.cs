using System;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Orders;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;

            return date.ToString("dd MMM, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string GetCurrencySymbol(Payment payment)
        {
            switch (payment.Total.Currency)
            {
                case "EUR":
                    return "€";
                case "USD":
                    return "$";
                default:
                    return "???";
            }
        }

    }
}