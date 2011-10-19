using System;
using System.Windows;
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
        private Type type;
        private DateTime licenseEndDate;
        private int years;
        private int months;
        private int weeks;
        private int days;
        private Order order;
        private Trial trial;

        public AddTimeCommand(IAsyncDocumentSession session, Type type, int years, int months, int weeks, int days)
        {
            this.session = session;
            this.type = type;
            this.years = years;
            this.months = months;
            this.weeks = weeks;
            this.days = days;
        }

        public bool CanExecute(object parameter)
        {
            //order = parameter as Order;
            //return order != null;

            return true;
        }

        public void Execute(object parameter)
        {
            order = parameter as Order;
            trial = parameter as Trial;
            if (order != null || trial != null)
            {
                AddTime();

                if (order != null)
                {
                    order.LicenseEndDate = licenseEndDate;
                }
                else
                {
                    trial.LicenseEndDate = licenseEndDate;
                }
            }

            session.SaveChangesAsync()
                .ContinueOnSuccessInTheUiThread(() => Application.Current.Host.NavigationState += "?" + Guid.NewGuid());
        }

        public event EventHandler CanExecuteChanged = delegate { };

        private void AddTime()
        {
            licenseEndDate = DateTime.Today;
            licenseEndDate = licenseEndDate.AddYears(years);
            licenseEndDate = licenseEndDate.AddMonths(months);
            licenseEndDate = licenseEndDate.AddDays(weeks * 7 + days);
        }
    }
}