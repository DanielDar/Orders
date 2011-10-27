using System.Linq;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using Raven.Client.Document;
using Raven.Client.Indexes;
using DateTime = System.DateTime;

namespace HibernatingRhinos.Orders.Backend.Indexes
{
    public class Orders_Stats : AbstractIndexCreationTask<Order, Orders_Stats.ReduceResult>
    {
        public class ReduceResult
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public DateTime Date { get; set; }
        }

        public Orders_Stats()
        {
            Map = orders => from order in orders
                            from payment in order.Payments
                            select new
                            {
                                Amount = (payment.Total.Amount - payment.VAT.Amount), 
                                payment.Total.Currency,
                                Date = new DateTime(order.OrderedAt.Year, order.OrderedAt.Month, 1)
                            };

            Reduce = results => from result in results
                                group result by new {result.Date, result.Currency}
                                into g
                                select new
                                {
                                    g.Key.Date,
                                    g.Key.Currency,
                                    Amount = g.Sum(x => (double) x.Amount)
                                };
        }
    }
}