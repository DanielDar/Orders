using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class ChangeDateCommand : ICommand
    {
        private readonly DateTime date;
        private readonly int monthToAdd;

        public ChangeDateCommand(DateTime date,int monthToAdd)
        {
            this.date = date;
            this.monthToAdd = monthToAdd;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            date.AddMonths(monthToAdd);
            Application.Current.Host.NavigationState = "/Home/List?date=" + date.AddMonths(monthToAdd) + Guid.NewGuid();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}