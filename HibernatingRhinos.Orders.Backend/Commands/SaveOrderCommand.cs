using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class SaveOrderCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Order order;

        public SaveOrderCommand(IAsyncDocumentSession session)
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
            session.Store(order);
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/orders/list");
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}