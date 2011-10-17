using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Features.Trials;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class SaveTrialCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Trial trial;

        public SaveTrialCommand(IAsyncDocumentSession session)
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
            session.Store(trial);
            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/trials/list");
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}