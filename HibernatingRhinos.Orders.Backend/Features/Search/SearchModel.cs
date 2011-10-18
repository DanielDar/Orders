using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Search
{
    public class SearchModel : ModelBase
    {
        public SearchModel()
        {
            var searchValue = GetQueryParam("search");
            if (string.IsNullOrEmpty(searchValue))
            {
                Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.OrderNumber));
                Session.Query<Order>().ToListAsync()
                    .ContinueOnSuccess(orders => Orders.Match(orders));
            }
            else
            {
                searchValue = searchValue.Split('?')[0];
                Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.OrderNumber));

                switch (_searchParameter)
                {
                    case "Email":
                        Session.Query<Order>().Where(x => x.Email == searchValue).ToListAsync()
                            .ContinueOnSuccess(orders => Orders.Match(orders));
                        break;
                    case "Name":
                        Session.Query<Order>().Where(x => x.FirstName == searchValue).ToListAsync()
                            .ContinueOnSuccess(orders => Orders.Match(orders));
                        break;
                    case "Order Number":
                        Session.Query<Order>().Where(x => x.OrderNumber == searchValue).ToListAsync()
                            .ContinueOnSuccess(orders => Orders.Match(orders));
                        break;
                    case "Product Id":
                        Session.Query<Order>().Where(x => x.ProductId == searchValue).ToListAsync()
                            .ContinueOnSuccess(orders => Orders.Match(orders));
                        break;
                }
            }
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

        private static string _searchParameter;
        public string SearchParameter
        {
            get { return _searchParameter; }
            set
            {
                _searchParameter = value;
                OnPropertyChanged();
            }
        }

        public BindableCollection<Order> Orders { get; set; }

        public ICommand Delete { get { return new DeleteOrderCommand(Session); } }
        public ICommand Edit { get { return new EditOrderCommand(Session); } }
        public ICommand AddMonth { get { return new AddMonthCommand(Session); } }
        public ICommand AddYear { get { return new AddYearCommand(Session); } }
    }
}