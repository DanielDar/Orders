using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private readonly string returnTo;

        public SaveCommand(IAsyncDocumentSession session, string returnTo)
        {
            this.session = session;
            this.returnTo = returnTo;
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            session.Store(parameter);
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = returnTo);
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}