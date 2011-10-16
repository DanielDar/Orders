using System;
using System.Windows;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class AddYearCommand : Command
    {
        public override void Execute(object parameter)
        {
            var order = parameter as Order;
            if(order != null)
            {
                order.LicenseEndDate = DateTime.Today;
                order.LicenseEndDate = order.LicenseEndDate.AddYears(1);
                order.LicenseEndDate = order.LicenseEndDate.AddDays(3);
            }
        }
    }
}