using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class OrdersListModel : ModelBase
    {
        private const string Location = "/orders/list";

        public OrdersListModel()
        {
            Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.Id));
            
            var searchValue = GetQueryParam("search") ;
            var query = Session.Query<Orders_Search.ReduceResult>("Orders/Search");


            if (string.IsNullOrEmpty(searchValue) == false)
            {
                query = query.Where(x => x.Query == searchValue);    
            }

            query
                .OrderByDescending(x => x.OrderedAt)
                .As<Order>()
                .ToListAsync()
                .ContinueOnSuccess(orders => Orders.Match(orders));
        }

        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged();
            }
        }

        public BindableCollection<Order> Orders { get; set; }

        public ICommand Edit { get { return new EditCommand(Session, Location); } }
       
    }
}