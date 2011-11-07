using System;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Indexes;


namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var money = value as Products_Stats.ReduceResult.Money;
            if (money == null)
                return null;
            return string.Format("{0:#,#.00} {1} ",money.Amount , GetCurrencySymbol(money.Currency));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string GetCurrencySymbol(string currency)
        {
            switch (currency)
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