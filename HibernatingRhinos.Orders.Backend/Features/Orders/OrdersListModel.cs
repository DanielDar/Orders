using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;
using System.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class OrdersListModel : ModelBase
    {
        private const string Location = "/orders/list";
        public const int ItemsPerPage = 20;
        public PagingInfo Paging { get; set; }

        public class PagingInfo
        {
            public PagingInfo()
            {
                NumberOfPages = new Observable<int>();
                NumberOfItems = new Observable<int>();

            }

            public int PageNumber { get; set; }
            public Observable<int> NumberOfPages { get; set; }
            public Observable<int> NumberOfItems { get; set; }
        }

        public OrdersListModel()
        {
            int pageNumber;
            int.TryParse(GetQueryParam("pageNumber"), out pageNumber);

            Paging = new PagingInfo
            {
                PageNumber = pageNumber
            };

            Paging.NumberOfPages.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged("NextPage");
                OnPropertyChanged("Paging");
            };
            Paging.NumberOfItems.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged("PageDataConverter");
                OnPropertyChanged("Paging");
            };

            Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.Id));


            Search = GetQueryParam("search");

            RavenQueryStatistics stats;
            var query = Session.Query<Orders_Search.ReduceResult>("Orders/Search")
                .Statistics(out stats)
                .Skip(Paging.PageNumber * ItemsPerPage)
                .Take(ItemsPerPage);



            if (string.IsNullOrEmpty(Search) == false)
            {
                query = query

                    .Where(x => x.Query == Search);
            }

            query
                .OrderByDescending(x => x.LastPaymentDate)
                .As<Order>()
                .ToListAsync()
                .ContinueOnSuccess(orders =>
                {
                    Orders.Match(orders);
                    Paging.NumberOfItems.Value = stats.TotalResults;
                    Paging.NumberOfPages.Value = Paging.NumberOfItems.Value / ItemsPerPage;
                });
        }

        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged();
                OnPropertyChanged("NextPage");
                OnPropertyChanged("PreviousPage");
                OnPropertyChanged("SearchUrl");
            }
        }

        public BindableCollection<Order> Orders { get; set; }

        public ICommand Edit { get { return new EditCommand(Session, Location); } }

        public ICommand PreviousPage { get { return new UpdateUrlCommand(CreateUrl(Paging.PageNumber - 1), Paging.PageNumber > 0); } }

        public ICommand NextPage { get { return new UpdateUrlCommand(CreateUrl(Paging.PageNumber + 1), Paging.PageNumber < Paging.NumberOfPages.Value); } }

        public ICommand SearchUrl { get { return new UpdateUrlCommand(CreateUrl(0), true); } }

        private string CreateUrl(int pageNumber)
        {
            return string.Format("{0}?search={1}&pageNumber={2}", Location, Search, pageNumber);
        }
    }
}