using System;
using System.Globalization;
using System.Windows.Data;
using HibernatingRhinos.Orders.Backend.Features.Orders;

namespace HibernatingRhinos.Orders.Backend.Converters
{
    public class PageDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as OrdersListModel.PagingInfo;
            if (data == null)
                return null;
            return BuildString(data);
        }

        private static string BuildString(OrdersListModel.PagingInfo data)
        {
            int firstItem = data.PageNumber * OrdersListModel.ItemsPerPage + 1;
            int lastItem = Math.Min(data.NumberOfItems.Value, (data.PageNumber + 1) * OrdersListModel.ItemsPerPage);
            return string.Format("{0} - {1} of {2}", firstItem, lastItem, data.NumberOfItems.Value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}