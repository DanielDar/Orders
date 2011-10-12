using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class OrdersListModel : ModelBase
    {
        public OrdersListModel()
        {
            Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.OrderNumber));
            Session.Query<Order>().ToListAsync()
                .ContinueOnSuccess(orders => Orders.Match(orders));
        }

        public BindableCollection<Order> Orders { get; set; }
        public ICommand Delete { get { return new DeleteProductCommand(Session); } }
        public ICommand Edit { get { return new EditProductCommand(Session); } }

    }
}