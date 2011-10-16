using System.Windows;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class CancleOrderCommand : Command
    {
        public override void Execute(object parameter)
        {
            Application.Current.Host.NavigationState = "/orders/list";
        }
    }
}