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
        public Observable<int> NumberOfPages { get; set; }

        public int PageNumber
        {
            get
            {
                int pageNumber;
                int.TryParse(GetQueryParam("pageNumber"), out pageNumber);
                return pageNumber;
            }
        }

        public OrdersListModel()
        {
            NumberOfPages = new Observable<int>();
            NumberOfPages.PropertyChanged += (sender, args) => OnPropertyChanged("NextPage");

            Orders = new BindableCollection<Order>(new PrimaryKeyComparer<Order>(x => x.Id));


            Search = GetQueryParam("search");

            RavenQueryStatistics stats;
            var query = Session.Query<Orders_Search.ReduceResult>("Orders/Search")
                .Statistics(out stats)
                .Skip(PageNumber * ItemsPerPage)
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
                    NumberOfPages.Value = stats.TotalResults / ItemsPerPage;
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

        public ICommand PreviousPage { get { return new UpdateUrlCommand(CreateUrl(PageNumber - 1), PageNumber > 0); } }

        public ICommand NextPage { get { return new UpdateUrlCommand(CreateUrl(PageNumber + 1), PageNumber < NumberOfPages.Value); } }

        public ICommand SearchUrl { get { return new UpdateUrlCommand(CreateUrl(0), true); } }

        private string CreateUrl(int pageNumber)
        {
            return string.Format("{0}?search={1}&pageNumber={2}", Location, Search, pageNumber);
        }
    }
}