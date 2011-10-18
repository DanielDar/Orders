using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Trials;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class DeleteTrialCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Trial trial;

        public DeleteTrialCommand(IAsyncDocumentSession session)
        {
            this.session = session;
        }

        public bool CanExecute(object parameter)
        {
            trial = parameter as Trial;
            return trial != null;
        }

        public void Execute(object parameter)
        {
            session.Delete(trial);
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState += "?"+Guid.NewGuid());
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}