﻿using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class EditProductsModel : ModelBase
    {
        private const string Location = "/products/list";

        public EditProductsModel()
        {
            Product = new Observable<Product>();
            var id = GetQueryParam("id");
            if (id == null)
            {
                Product.Value = new Product();
            }
            else
            {
                Session.LoadAsync<Product>(id)
                .ContinueOnSuccess(product => Product.Value = product);
            }
        }

        private ProductTypes productType;

        public ProductTypes ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                OnPropertyChanged();
            }
        }

        public Observable<Product> Product { get; set; }

        public ICommand Save { get { return new SaveCommand(Session, Location); } }
        public ICommand Cancel { get { return new CancleCommand(Location); } }
        public ICommand Delete { get { return new DeleteCommand(Session, Location); } }
    }
}