using System.Windows;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Commands
{
    public class CancleCommand : Command
    {
        private readonly string returnTo;
        public CancleCommand(string returnTo)
        {
            this.returnTo = returnTo;
        }

        public override void Execute(object parameter)
        {
            Application.Current.Host.NavigationState = returnTo;
        }
    }
}