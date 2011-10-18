using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class DeleteProductCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Product product;

        public DeleteProductCommand(IAsyncDocumentSession session)
        {
            this.session = session;
        }

        public bool CanExecute(object parameter)
        {
            product = parameter as Product;
            return product != null;
        }

        public void Execute(object parameter)
        {
            session.Delete(product);
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState += "?" + Guid.NewGuid());
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}