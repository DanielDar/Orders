using System.Collections.Generic;
using System.Collections.ObjectModel;
using Raven.Client;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class ProductsPresenter
    {
        private readonly IAsyncDocumentSession session = RavenDB.Store.OpenAsyncSession();

        public ProductsPresenter()
        {
            //session.Store(new Product { });
            //session.SaveChangesAsync();

            Products = new ObservableCollection<Product>
            {
                 new Product
                 {
                     Id = "products/1234",
                     ProductType = ProductTypes.LifeTime,
                     StoreId = new List<string>{"12312", "123812312"},
                     Title = "RavenDB Standard"
                 },
                 new Product
                 {
                     Id = "products/121231",
                     ProductType = ProductTypes.LifeTime,
                     StoreId = new List<string>{"112", "112312"},
                     Title = "RavenDB Enterprise"
                 },
                 new Product
                 {
                     Id = "products/89-08",
                     ProductType = ProductTypes.LifeTime,
                     StoreId = new List<string>{"112", "112312"},
                     Title = "RavenDB Enterprise"
                 },
                 new Product
                 {
                     Id = "products/8-9087",
                     ProductType = ProductTypes.LifeTime,
                     StoreId = new List<string>{"112", "112312"},
                     Title = "RavenDB Enterprise"
                 },
                 new Product
                 {
                     Id = "products/34256",
                     ProductType = ProductTypes.LifeTime,
                     StoreId = new List<string>{"112", "112312"},
                     Title = "RavenDB Standard"
                 },
                 new Product
                 {
                     Id = "products/07w45",
                     ProductType = ProductTypes.LifeTime,
                     StoreId = new List<string>{"112", "112312"},
                     Title = "RavenDB Standard"
                 }
            };

            //session.Query<Product>()
            //    .ToListAsync()
            //    .ContinueWith(task =>
            //    {
            //        foreach (var product in task.Result)
            //        {
            //            Products.Add(product);
            //        }
            //    });
        }

        public ObservableCollection<Product> Products { get; set; }
    }
}