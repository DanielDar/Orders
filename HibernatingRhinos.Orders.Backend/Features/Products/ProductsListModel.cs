using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class ProductsListModel : ModelBase
    {
        private const string Location = "/products/list";

        public ProductsListModel()
        {
            Products = new BindableCollection<Product>(new PrimaryKeyComparer<Product>(x => x.Id));
            Session.Query<Product>().ToListAsync()
                .ContinueOnSuccess(products => Products.Match(products));
        }

        public BindableCollection<Product> Products { get; set; }

        public ICommand Delete { get { return new DeleteCommand(Session, Location); } }
        public ICommand Edit { get { return new EditCommand(Session, Location); } }
    }
}