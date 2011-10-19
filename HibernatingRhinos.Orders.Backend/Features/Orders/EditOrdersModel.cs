﻿using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public class EditOrdersModel : ModelBase
    {
        private const string Location = "/orders/list";
        public EditOrdersModel()
        {
            Order = new Observable<Order>();
            var orderNumber = GetQueryParam("orderNumber");
            if (orderNumber == null)
            {
                Order.Value = new Order();
            }
            else
            {
                Session.LoadAsync<Order>("orders/" + orderNumber)
                .ContinueOnSuccess(order => Order.Value = order);
            }
        }

        public Observable<Order> Order { get; set; }

        public ICommand Save { get { return new SaveCommand(Session, Location ); } }
        public ICommand Cancel { get { return new CancleCommand(Location); } }
    }
}