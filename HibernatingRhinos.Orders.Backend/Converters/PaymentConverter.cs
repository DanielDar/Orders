using System;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Orders;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class PaymentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var payment = value as Payment;
            if (payment == null)
                return null;
            return string.Format("{0:#,#.00} {1} ", (payment.Total.Amount - payment.VAT.Amount), GetCurrencySymbol(payment));
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