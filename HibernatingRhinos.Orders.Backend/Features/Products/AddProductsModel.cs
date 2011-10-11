﻿using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class AddProductsModel : ModelBase
    {
        public AddProductsModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            InitializeSession();
            Product = new Product();
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

        public Product Product { get; private set; }

        public ICommand Save { get { return new SaveProductCommand(Session); } }
        public ICommand Cancel { get { return new CancleProductCommand(); } }
    }
}