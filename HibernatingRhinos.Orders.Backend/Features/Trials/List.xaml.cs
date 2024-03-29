﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public partial class List : Page
    {
        public List()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void SearchByEnter(object sender, KeyEventArgs e)
        {
            var searchBox = sender as TextBox;

            if (e.Key == Key.Enter && searchBox != null)
            {
                var location = string.Format("/Trials/List?search={0}", searchBox.Text);
                Application.Current.Host.NavigationState = location;
            }
        }

    }
}
