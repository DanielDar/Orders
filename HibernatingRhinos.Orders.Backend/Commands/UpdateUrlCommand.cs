using System;
using System.Windows;
using System.Windows.Input;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class UpdateUrlCommand : ICommand
    {
        private readonly string location;
        private readonly bool canExecute;

        public UpdateUrlCommand(string location, bool canExecute)
        {
            this.location = location;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public void Execute(object parameter)
        {
            Application.Current.Host.NavigationState = location;
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}