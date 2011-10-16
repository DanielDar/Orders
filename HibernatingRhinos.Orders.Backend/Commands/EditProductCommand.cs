using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class EditProductCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Product product;

        public EditProductCommand(IAsyncDocumentSession session)
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
            product = parameter as Product;
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/products/edit");
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}