using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public class TrialsListModel : ModelBase
    {
        private const string Location = "/trials/list";

        public TrialsListModel()
        {
            Trials = new BindableCollection<Trial>(new PrimaryKeyComparer<Trial>(x => x.Id));

            var searchValue = GetQueryParam("search");
            var query = Session.Query<Trials_Search.ReduceResult>("Trials/Search");


            if (string.IsNullOrEmpty(searchValue) == false)
            {
                query = query.Where(x => x.Query == searchValue);
            }

            query
                .OrderByDescending(x => x.StartedAt)
                .As<Trial>()
                .ToListAsync()
                .ContinueOnSuccess(trials => Trials.Match(trials));
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

        public BindableCollection<Trial> Trials { get; set; }

        public ICommand Edit { get { return new EditCommand(Session, Location); } }

    }
}