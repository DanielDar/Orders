using System;
using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Indexes;
using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client.Linq;

namespace HibernatingRhinos.Orders.Backend.Features.Search
{
    public class SearchModel : ModelBase
    {
        private const string Location = "/search/list";

        public SearchModel()
        {
            var searchValue = GetQueryParam("search");

            Session.Query<Orders_Search.ReduceResult>("Orders/Search")
                .Where(x => x.Query == searchValue)
                .OrderByDescending(x => x.OrderedAt)
                .As<Order>()
                .ToListAsync()
                .ContinueOnSuccess(orders => Orders.Match(orders));
        }

        //private string search;
        //public string Search
        //{
        //    get { return search; }
        //    set
        //    {
        //        search = value;
        //        OnPropertyChanged();
        //    }
        //}

        public BindableCollection<Order> Orders { get; set; }

        public ICommand Delete { get { return new DeleteCommand(Session, Location); } }
        public ICommand Edit { get { return new EditCommand(Session, Location); } }
    }
}