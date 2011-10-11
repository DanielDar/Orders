using System.Windows;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class EditProductsModel : ModelBase
    {
        public EditProductsModel()
        {
            Product = new Observable<Product>();
            var id = GetQueryParam("id");
            if (id == null)
            {
                Product.Value = new Product();
            }
            else
            {
                Session.LoadAsync<Product>(id)
                .ContinueOnSuccess(product => Product.Value = product);
            }
        }

        private ProductTypes productType;

        public ProductTypes ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                OnPropertyChanged();
            }
        }

        public Observable<Product> Product { get; set; }

        public ICommand Save { get { return new SaveProductCommand(Session); } }
        public ICommand Cancel { get { return new CancleProductCommand(); } }
    }
}