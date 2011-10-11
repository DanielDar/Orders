using System;
using System.Windows.Input;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public abstract class Command : ICommand
    {
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged = delegate { };
    }
}