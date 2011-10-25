using System.Linq;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Products;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace HibernatingRhinos.Orders.Backend.Indexes
{
    public class Products_Stats : AbstractIndexCreationTask<Order, Products_Stats.ReduceResult>
    {
        public class ReduceResult
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string ProductId { get; set; }

            public ProductSummary[] Summary { get; set; }

            public class ProductSummary
            {
                public ProductTypes Type { get; set; }
                public int Count { get; set; }
            }
        }

        public Products_Stats()
        {
            Map = orders => from order in orders
                            from payment in order.Payments
                            select new
                            {
                                Amount = (payment.Total.Amount - payment.VAT.Amount),
                                payment.Total.Currency,
                                order.OrderedAt.Month,
                                order.OrderedAt.Year,
                                order.ProductId,
                                Summary = new object[] { new { order.Type, Count = 1 } }
                            };

            Reduce = results => from result in results
                                group result by new { result.Year, result.Month, result.Currency, result.ProductId }
                                    into g
                                    select new
                                    {
                                        g.Key.Year,
                                        g.Key.Month,
                                        g.Key.Currency,
                                        g.Key.ProductId,
                                        Amount = g.Sum(x => (double)x.Amount),
                                        Summary = from item in g.SelectMany(x => x.Summary)
                                                group item by item.Type
                                                    into ig
                                                    select new { Type = ig.Key, Count = ig.Sum(x => x.Count) }
                                    };
        }
    }
}