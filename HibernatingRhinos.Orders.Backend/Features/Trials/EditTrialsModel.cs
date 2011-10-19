using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public class EditTrialsModel : ModelBase
    {
        public EditTrialsModel()
        {
            Trial = new Observable<Trial>();
            var id = GetQueryParam("id");
            if (id == null)
            {
                Trial.Value = new Trial();
            }
            else
            {
                Session.LoadAsync<Trial>(id)
                .ContinueOnSuccess(trial => Trial.Value = trial);
            }
        }

        public Observable<Trial> Trial { get; set; }

        public ICommand Save { get { return new SaveTrialCommand(Session); } }
        public ICommand Cancel { get { return new CancleCommand("/trials/list"); } }
    }
}