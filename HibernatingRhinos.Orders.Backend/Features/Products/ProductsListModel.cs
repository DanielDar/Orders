using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class ProductsListModel : ModelBase
    {
        public ProductsListModel()
        {
            Products = new BindableCollection<Product>(new PrimaryKeyComparer<Product>(x => x.Id));
            Session.Query<Product>().ToListAsync()
                .ContinueOnSuccess(products => Products.Match(products));
        }

        public BindableCollection<Product> Products { get; set; }
    }
}