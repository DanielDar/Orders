using System.Linq;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public class TrialsListModel : PageableModel
    {
        private const string Location = "/trials/list";


        public TrialsListModel()
        {
            Trials = new BindableCollection<Trial>(new PrimaryKeyComparer<Trial>(x => x.Id));

            Search = GetQueryParam("search");

            RavenQueryStatistics stats;
            var query = Session.Query<Trials_Search.ReduceResult>("Trials/Search")
                .Statistics(out stats)
                .Skip(Paging.PageNumber * ItemsPerPage)
                .Take(ItemsPerPage);

            if (string.IsNullOrEmpty(Search) == false)
            {
                query = query

                    .Where(x => x.Query == Search);
            }

            query
                .OrderByDescending(x => x.StartedAt)
                .As<Trial>()
                .ToListAsync()
                .ContinueOnSuccess(trials =>
                {
                    Trials.Match(trials);
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

        public BindableCollection<Trial> Trials { get; set; }

        public ICommand Edit { get { return new EditCommand(Session, Location); } }

        protected override string CreateUrl(int pageNumber)
        {
            return string.Format("{0}?search={1}&pageNumber={2}", Location, Search, pageNumber);
        }

    }
}