using System;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Trials;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class AddTimeCommand : ICommand
    {
        private readonly IAsyncDocumentSession session;
        private readonly Func<DateTime, DateTime> modifyDate;

        public AddTimeCommand(IAsyncDocumentSession session, Func<DateTime, DateTime>  modifyDate)
        {
            this.session = session;
            this.modifyDate = modifyDate;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is IEnd;
        }

        public void Execute(object parameter)
        {
            var ends = (IEnd) parameter;

            ends.EndsAt = modifyDate(DateTime.Today);

            session
                .SaveChangesAsync()
                .Reload();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}