using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class EditOrdersModel : ModelBase
    {
        private const string Location = "/orders/list";
        public EditOrdersModel()
        {
            Order = new Observable<Order>();
            var id = GetQueryParam("id");
            if (id == null)
            {
                Order.Value = new Order();
            }
            else
            {
                Session.LoadAsync<Order>(id)
                .ContinueOnSuccess(order => Order.Value = order);
            }
        }

        public Observable<Order> Order { get; set; }

        public ICommand Save { get { return new SaveCommand(Session, Location ); } }
        public ICommand Cancel { get { return new CancleCommand(Location); } }
        public ICommand AddMonth { get { return new AddTimeCommand(Session, time => time.AddMonths(1).AddDays(3)); } }
        public ICommand AddYear { get { return new AddTimeCommand(Session, time => time.AddYears(1).AddDays(3)); } }
        public ICommand Delete { get { return new DeleteCommand(Session, Location); } }

    }
}