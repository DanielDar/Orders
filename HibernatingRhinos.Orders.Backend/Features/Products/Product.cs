using System.Collections.Generic;
using HibernatingRhinos.Orders.Backend.Features.Orders;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class Product : IEditable
    {
        public string Id
        {
            get { return "products/" + Name; }
            set {}
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, ProductTypes> Identifiers { get; set; }
    }
}
