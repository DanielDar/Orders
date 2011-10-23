using System.Collections.Generic;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class Product
    {
        public string Id
        {
            get { return "products/" + Name; }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, ProductTypes> Identifiers { get; set; }
    }
}
