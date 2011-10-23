using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;

        public DeleteCommand(IAsyncDocumentSession session)
        {
            this.session = session;
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            session.Delete(parameter);
            session
                .SaveChangesAsync()
                .Reload();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}