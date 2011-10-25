using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using System.Linq;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class OrderConverter : IValueConverter
    {
        private readonly Dictionary<string, List<Func<Order, string>>> converters = new Dictionary<string, List<Func<Order, string>>>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"name", new List<Func<Order, string>>
            {
                order => order.CompanyName,
                order => order.FirstName +" " + order.LastName
            }},
            {"amount", new List<Func<Order, string>>
            {
                order =>
                {
                    var lastPayment = order.Payments.Last();
                    return string.Format("{0:#,#.00} {1} ", (lastPayment.Total.Amount - lastPayment.VAT.Amount), GetCurrencySymbol(lastPayment));
                }
            } },
            {"date", new List<Func<Order, string>>
            {
                order => order.Payments.Last().At.ToString("dd MMM, yyyy")
            }}
        };

        private static string GetCurrencySymbol(Payment lastPayment)
        {
            switch (lastPayment.Total.Currency)
            {
                case "EUR":
                    return "€";
                case "USD":
                    return "$";
                default:
                    return "???";
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var order = value as Order;
            if (order == null)
                return null;

            List<Func<Order, string>> list;
            if(converters.TryGetValue((string)parameter,out list) == false)
                throw new InvalidOperationException("don't know how to translate: " + parameter);

            return list.Select(func => func(order)).FirstOrDefault(s => string.IsNullOrEmpty(s) == false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}