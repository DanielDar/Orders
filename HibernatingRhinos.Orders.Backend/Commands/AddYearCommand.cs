using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class AddYearCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Order order;

        public AddYearCommand(IAsyncDocumentSession session)
        {
            this.session = session;
        }

        public bool CanExecute(object parameter)
        {
            order = parameter as Order;
            return order != null;
        }

        public void Execute(object parameter)
        {
            order = parameter as Order;
            if (order != null)
            {
                order.LicenseEndDate = DateTime.Today;
                order.LicenseEndDate = order.LicenseEndDate.AddYears(1);
                order.LicenseEndDate = order.LicenseEndDate.AddDays(3);
            }

            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/orders/list?" + Guid.NewGuid());
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}