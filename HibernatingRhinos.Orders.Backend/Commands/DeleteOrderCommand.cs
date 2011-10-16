using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class DeleteOrderCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Order order;

        public DeleteOrderCommand(IAsyncDocumentSession session)
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
            session.Delete(order);
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/orders/list?"+Guid.NewGuid());
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}