using System.Linq;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace HibernatingRhinos.Orders.Backend.Indexes
{
    public class Orders_Stats : AbstractIndexCreationTask<Order, Orders_Stats.ReduceResult>
    {
        public class ReduceResult
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }

        public Orders_Stats()
        {
            Map = orders => from order in orders
                            from payment in order.Payments
                            select new
                            {
                                Amount = (payment.Total.Amount - payment.VAT.Amount), 
                                payment.Total.Currency,
                                order.OrderedAt.Month,
                                order.OrderedAt.Year
                            };

            Reduce = results => from result in results
                                group result by new {result.Year, result.Month, result.Currency}
                                into g
                                select new
                                {
                                    g.Key.Year, 
                                    g.Key.Month, 
                                    g.Key.Currency,
                                    Amount = g.Sum(x => (double) x.Amount)
                                };
        }
    }
}