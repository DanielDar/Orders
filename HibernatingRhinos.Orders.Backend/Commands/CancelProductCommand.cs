using System.Windows;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class CancleProductCommand : Command
    {
        public override void Execute(object parameter)
        {
            Application.Current.Host.NavigationState = "/products/list";
        }
    }
}