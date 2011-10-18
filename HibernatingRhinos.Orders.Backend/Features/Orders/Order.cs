using System;
using System.Collections.Generic;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class Order
    {
        public string Id { get { return "orders/" + OrderNumber; } }

        public string OrderNumber { get; set; }

        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public List<string> Log { get; set; }

        public DateTime LicenseEndDate { get; set; }

        public Order()
        {
            Address = new Address();
            LicenseEndDate = DateTime.Today;
        }
    }
}
