using System;
using System.Linq;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using Raven.Client.Indexes;

namespace HibernatingRhinos.Orders.Backend.Indexes
{
    public class Orders_Search : AbstractIndexCreationTask<Order>
    {
        public class ReduceResult
        {
            public string Query { get; set; }
            public DateTime LastPaymentDate { get; set; }
        }
        public Orders_Search()
        {
            Map = orders => from order in orders
                            //let lastPayment = order.Payments[order.Payments.Count-1]
                            select new
                            {
                                Query = new[]
                                {
                                    order.FirstName, 
                                    order.LastName, 
                                    order.OrderNumber, 
                                    order.Email, 
                                    order.CompanyName
                                },
                                LastPaymentDate = order.OrderedAt
                            };
        }
    }
}