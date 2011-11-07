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
        private Type type;
        public EditByDoubleClickCommand(Type type)
        {
            location = type == typeof(Order) ? "/orders/edit?id=" : "/trials/edit?id=";
            this.type = type;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is Order;
        }

        public void Execute(object parameter)
        {
            var id = type == typeof(Order) ? (parameter as Order).Id : (parameter as Trial).Id;

            location += id;
            Application.Current.Host.NavigationState = location;
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
