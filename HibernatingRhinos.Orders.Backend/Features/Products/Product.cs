using System.Collections.Generic;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class Product
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public List<string> StoreId { get; set; }

        public ProductTypes ProductType { get; set; }
    }
}
