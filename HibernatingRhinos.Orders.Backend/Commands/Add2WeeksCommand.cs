using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Trials;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class Add2WeeksCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private Trial trial;

        public Add2WeeksCommand(IAsyncDocumentSession session)
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
            trial = parameter as Trial;
            if(trial != null)
            {
                trial.LicenseEndDate = DateTime.Today;
                trial.LicenseEndDate = trial.LicenseEndDate.AddDays(14);
            }

            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState = "/trials/list?" + Guid.NewGuid());
        }

        public event EventHandler CanExecuteChanged = delegate {};
    }
}