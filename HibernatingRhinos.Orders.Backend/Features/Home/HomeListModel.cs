using System;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Home
{
    public class HomeListModel : ModelBase
    {
        private const string Location = "/home/list";

        public DateTime LookAtDate { get; set; }

        public HomeListModel()
        {
            string date = GetQueryParam("date");

            if (date == null)
                LookAtDate = DateTime.Today;
            else
            {
                int year, month;
                int.TryParse(date.Substring(6, 4), out year);
                int.TryParse(date.Substring(3, 2), out month);
                LookAtDate = new DateTime(year, month, 1);
            }

            ProductStats =
                new BindableCollection<Products_Stats.ReduceResult>(
                    new PrimaryKeyComparer<Products_Stats.ReduceResult>(
                        x => Tuple.Create(x.Year, x.Month, x.Total.Currency, x.ProductId)));

            Session.Query<Products_Stats.ReduceResult>("Products/Stats")
                .Where(x => x.Year == LookAtDate.Year && x.Month == LookAtDate.Month)
                .ToListAsync()
                .ContinueOnSuccess(items => ProductStats.Match(items));

            OrderStats =
                new BindableCollection<Orders_Stats.ReduceResult>(
                    new PrimaryKeyComparer<Orders_Stats.ReduceResult>(
                        x => Tuple.Create(x.Year, x.Month, x.Currency)));

            Session.Query<Orders_Stats.ReduceResult>("Orders/Stats")
                .Where(x=>x.Year == LookAtDate.Year)
                .ToListAsync()
                .ContinueOnSuccess(items => OrderStats.Match(items));
        }

        public BindableCollection<Products_Stats.ReduceResult> ProductStats { get; set; }
        public BindableCollection<Orders_Stats.ReduceResult> OrderStats { get; set; }

        public ICommand PreviousMonth { get { return new ChangeDateCommand(LookAtDate, -1); } }
        public ICommand NextMonth { get { return new ChangeDateCommand(LookAtDate, 1); } }
    }
}