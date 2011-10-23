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
        private string location;

        public DeleteCommand(IAsyncDocumentSession session, string location)
        {
            this.session = session;
            this.location = location;
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
                .GoTo(location);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}