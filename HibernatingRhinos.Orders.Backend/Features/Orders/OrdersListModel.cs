using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class OrdersListModel : ModelBase
    {
        private const string Location = "/orders/list";

        public OrdersListModel()
        {
            Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.OrderNumber));
            Session.Query<Order>().ToListAsync()
                .ContinueOnSuccess(orders => Orders.Match(orders));
        }

        public BindableCollection<Order> Orders { get; set; }

        public ICommand Delete { get { return new DeleteCommand(Session); } }
        public ICommand Edit { get { return new EditCommand(Session, Location); } }
        public ICommand AddMonth { get { return new AddTimeCommand(Session, typeof (Order), 0, 1, 0, 3); } }
        public ICommand AddYear { get { return new AddTimeCommand(Session, typeof (Order), 1, 0, 0, 3); } }
    }
}