using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public class TrialsListModel : ModelBase
    {
        public TrialsListModel()
        {
            Trials = new BindableCollection<Trial>(new PrimaryKeyComparer<Trial>(x => x.Id));
            Session.Query<Trial>().ToListAsync()
                .ContinueOnSuccess(trials => Trials.Match(trials));
        }

        public BindableCollection<Trial> Trials { get; set; }

        public ICommand Delete { get { return new DeleteTrialCommand(Session); } }
        public ICommand Edit { get { return new EditTrialCommand(Session); } }
        public ICommand AddWeek { get { return new AddWeekCommand(Session); } }
        public ICommand Add2Weeks { get { return new Add2WeeksCommand(Session); } }
    }
}