using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class EditOrderCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Order order;

        public EditOrderCommand(IAsyncDocumentSession session)
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
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/orders/edit");
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}