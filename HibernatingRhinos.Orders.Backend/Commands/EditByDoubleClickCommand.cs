using System;
using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Trials;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class EditByDoubleClickCommand : ICommand
    {
        private string location;

        public EditByDoubleClickCommand(string type)
        {
            location = string.Format("/{0}/edit?id=", type);
        }

        public bool CanExecute(object parameter)
        {
            return parameter is IEditable;
        }

        public void Execute(object parameter)
        {
            var item = parameter as IEditable;

            if (item != null)
            {
                location += item.Id;
                Application.Current.Host.NavigationState = location;
            }
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
